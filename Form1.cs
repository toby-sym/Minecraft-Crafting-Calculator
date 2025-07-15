using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftCraftingCalculator
{
    public partial class Form1 : Form
    {
        private Dictionary<string, MinecraftRecipe> _recipes = new();

        // Store desired Item Quantity pairs
        private List<(string ItemName, int Quantity)> craftingItems = new();

        public Form1()
        {
            InitializeComponent();

            // Disable GUI until recipes loaded
            txtItemName.Enabled = false;
            numQuantity.Enabled = false;
            btnAddItem.Enabled = false;
            btnCalculate.Enabled = false;

            // Hook up Events
            btnAddItem.Click += BtnAddItem_Click;
            btnCalculate.Click += BtnCalculate_Click;
        }

        private void btnLoadRecipes_Click(object sender, EventArgs e)
        {
            // Load JSON files via File Dialogue
            using (var ofd = new OpenFileDialog())
            {
                // Specifies JSON files
                ofd.Multiselect = true;
                ofd.Filter = "JSON files (*.json)|*.json";

                // Show File Dialogue
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Initialise Recipe Load Count
                    int loadedCount = 0;
                    foreach (var file in ofd.FileNames)
                    {
                        try
                        {
                            var json = File.ReadAllText(file);
                            var recipe = JsonConvert.DeserializeObject<MinecraftRecipe>(json);

                            // Input Validation of JSON file
                            if (recipe?.Result?.Item != null)
                            {
                                _recipes[recipe.Result.Item] = recipe;
                                
                                // Increment Recipe Load Count

                                loadedCount++;
                            }
                        }
                        // File Load Failure Exception
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to load {Path.GetFileName(file)}:\n{ex.Message}");
                        }
                    }

                    // Recipe Count
                    lblRecipeCount.Text = $"Loaded {loadedCount} recipes.";
                    
                    // Re-Enable GUI
                    txtItemName.Enabled = true;
                    numQuantity.Enabled = true;
                    btnAddItem.Enabled = true;
                    btnCalculate.Enabled = true;

                    // Auto-Complete Text Input Box
                    var autoComplete = new AutoCompleteStringCollection();
                    autoComplete.AddRange(_recipes.Keys.ToArray());
                    txtItemName.AutoCompleteCustomSource = autoComplete;
                    txtItemName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            // Input Validation
            string itemName = txtItemName.Text.Trim();
            if (string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }
            if (!_recipes.ContainsKey(itemName))
            {
                MessageBox.Show("Item not found in recipes.");
                return;
            }

            int quantity = (int)numQuantity.Value;
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be at least 1.");
                return;
            }

            craftingItems.Add((itemName, quantity));
            lstCraftingList.Items.Add($"{quantity} x {itemName}");

            // Reset Text Input Box for next entry
            txtItemName.Clear();
            numQuantity.Value = 1;
            txtItemName.Focus();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // Clear previous results
            lstRawMaterials.Items.Clear();

            // Total ingredient counts across all craftingItems
            var totalCounts = new Dictionary<string, int>();

            foreach (var (itemName, quantity) in craftingItems)
            {
                Dictionary<string, int> ingredients;
                try
                {
                    ingredients = CalculateIngredients(itemName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error calculating ingredients for {itemName}: {ex.Message}");
                    continue;
                }

                foreach (var kvp in ingredients)
                {
                    if (totalCounts.ContainsKey(kvp.Key))
                        totalCounts[kvp.Key] += kvp.Value * quantity;
                    else
                        totalCounts[kvp.Key] = kvp.Value * quantity;
                }
            }

            // Display total counts nicely sorted by name
            foreach (var kvp in totalCounts.OrderBy(k => k.Key))
            {
                lstRawMaterials.Items.Add($"{kvp.Value} x {kvp.Key}");
            }
        }

        // Calculate Ingredients based on Minecraft JSON Recipe Structure
        public Dictionary<string, int> CalculateIngredients(string itemName)
        {
            if (!_recipes.ContainsKey(itemName))
                throw new ArgumentException($"Recipe for {itemName} not found");

            var recipe = _recipes[itemName];
            var counts = new Dictionary<string, int>();

            switch (recipe.Type)
            {
                case "minecraft:crafting_shaped":
                    if (recipe.Pattern == null || recipe.Key == null)
                        throw new Exception("Malformed shaped recipe");

                    foreach (var row in recipe.Pattern)
                    {
                        foreach (var symbol in row)
                        {
                            if (symbol == ' ' || !recipe.Key.ContainsKey(symbol.ToString()))
                                continue;

                            var ingredientsList = recipe.Key[symbol.ToString()];
                            foreach (var ingredient in ingredientsList)
                            {
                                if (ingredient == null)
                                    continue;

                                string key = ingredient.Item ?? ingredient.Tag;
                                if (string.IsNullOrEmpty(key))
                                    continue;

                                AddCount(counts, key, ingredient.Count);
                            }
                        }
                    }
                    break;

                // Edge Case of Shapeless Recipes
                case "minecraft:crafting_shapeless":
                    if (recipe.Ingredients == null)
                        throw new Exception("Malformed shapeless recipe");

                    foreach (var ingredient in recipe.Ingredients)
                    {
                        if (ingredient == null)
                            continue;

                        string key = ingredient.Item ?? ingredient.Tag;
                        if (string.IsNullOrEmpty(key))
                            continue;

                        AddCount(counts, key, ingredient.Count);
                    }
                    break;

                // Edge Case of Non-Craftable Recipes
                case "minecraft:smelting":
                case "minecraft:blasting":
                case "minecraft:smoking":
                case "minecraft:campfire_cooking":
                case "minecraft:stonecutting":
                    if (recipe.Ingredient != null)
                    {
                        string key = recipe.Ingredient.Item ?? recipe.Ingredient.Tag;
                        if (!string.IsNullOrEmpty(key))
                        {
                            AddCount(counts, key, recipe.Ingredient.Count);
                        }
                        else
                        {
                            throw new Exception($"Malformed {recipe.Type} recipe");
                        }
                    }
                    else
                    {
                        throw new Exception($"Malformed {recipe.Type} recipe");
                    }
                    break;

                default:
                    throw new NotSupportedException($"Recipe type '{recipe.Type}' not supported");
            }

            return counts;
        }

        private void AddCount(Dictionary<string, int> dict, string key, int count)
        {
            if (dict.ContainsKey(key))
                dict[key] += count;
            else
                dict[key] = count;
        }
    }
}
