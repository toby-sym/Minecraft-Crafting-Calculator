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
            btnClearItems.Enabled = false; // Disable clear button initially

            // Hook up Events
            btnAddItem.Click += BtnAddItem_Click;
            btnCalculate.Click += BtnCalculate_Click;
            btnClearItems.Click += BtnClearItems_Click;    // Clear button event
            lstRawMaterials.DoubleClick += LstRawMaterials_DoubleClick;
            lstCraftingList.DoubleClick += LstCraftingList_DoubleClick; // Double-click to remove input item
        }

        private void btnLoadRecipes_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "JSON files (*.json)|*.json";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    int loadedCount = 0;
                    foreach (var file in ofd.FileNames)
                    {
                        try
                        {
                            var json = File.ReadAllText(file);
                            var recipe = JsonConvert.DeserializeObject<MinecraftRecipe>(json);

                            if (recipe?.Result?.Item != null)
                            {
                                if (!_recipes.ContainsKey(recipe.Result.Item))
                                {
                                    _recipes[recipe.Result.Item] = recipe;
                                }
                                // else ignore duplicates for now

                                loadedCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to load {Path.GetFileName(file)}:\n{ex.Message}");
                        }
                    }

                    lblRecipeCount.Text = $"Loaded {loadedCount} recipes.";

                    txtItemName.Enabled = true;
                    numQuantity.Enabled = true;
                    btnAddItem.Enabled = true;
                    btnCalculate.Enabled = true;
                    btnClearItems.Enabled = true; // Enable Clear button now

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

            txtItemName.Clear();
            numQuantity.Value = 1;
            txtItemName.Focus();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            lstRawMaterials.Items.Clear();

            var totalCounts = new Dictionary<string, int>();

            foreach (var (itemName, quantity) in craftingItems)
            {
                try
                {
                    var ingredients = CalculateIngredients(itemName);
                    foreach (var kvp in ingredients)
                    {
                        if (totalCounts.ContainsKey(kvp.Key))
                            totalCounts[kvp.Key] += kvp.Value * quantity;
                        else
                            totalCounts[kvp.Key] = kvp.Value * quantity;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error calculating ingredients for {itemName}: {ex.Message}");
                }
            }

            foreach (var kvp in totalCounts.OrderBy(k => k.Key))
            {
                lstRawMaterials.Items.Add($"{kvp.Value} x {kvp.Key}");
            }
        }

        // Clear button handler
        private void BtnClearItems_Click(object sender, EventArgs e)
        {
            craftingItems.Clear();
            lstCraftingList.Items.Clear();
            lstRawMaterials.Items.Clear();
        }

        // Double-click to remove item from crafting input list
        private void LstCraftingList_DoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = lstCraftingList.SelectedIndex;
            if (selectedIndex < 0) return;

            craftingItems.RemoveAt(selectedIndex);
            lstCraftingList.Items.RemoveAt(selectedIndex);
        }

        // CalculateIngredients method unchanged
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

        private void LstRawMaterials_DoubleClick(object sender, EventArgs e)
        {
            if (lstRawMaterials.SelectedItem == null) return;

            var selectedText = lstRawMaterials.SelectedItem.ToString();
            var parts = selectedText.Split(new string[] { " x " }, StringSplitOptions.None);
            if (parts.Length < 2) return;

            if (!int.TryParse(parts[0], out int quantity)) return;
            string itemName = parts[1];

            var allRecipes = _recipes.Values.Where(r => r.Result?.Item == itemName).ToList();
            if (allRecipes.Count == 0)
            {
                MessageBox.Show($"No recipes found for {itemName}");
                return;
            }

            using (var recipeForm = new RecipeSelectionForm(itemName, quantity, allRecipes))
            {
                if (recipeForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedRecipes = recipeForm.SelectedRecipes;

                    var totalCounts = new Dictionary<string, int>();

                    foreach (var (recipe, qtyMultiplier) in selectedRecipes)
                    {
                        var ingrDict = CalculateIngredientsByRecipe(recipe, qtyMultiplier);
                        foreach (var kvp in ingrDict)
                        {
                            if (totalCounts.ContainsKey(kvp.Key))
                                totalCounts[kvp.Key] += kvp.Value;
                            else
                                totalCounts[kvp.Key] = kvp.Value;
                        }
                    }

                    int selectedIndex = lstRawMaterials.SelectedIndex;
                    if (selectedIndex < 0) return;

                    lstRawMaterials.BeginUpdate();
                    lstRawMaterials.Items.RemoveAt(selectedIndex);

                    foreach (var kvp in totalCounts.OrderBy(k => k.Key))
                    {
                        lstRawMaterials.Items.Insert(selectedIndex, $"{kvp.Value} x {kvp.Key}");
                        selectedIndex++;
                    }

                    lstRawMaterials.EndUpdate();
                }
            }
        }

        private Dictionary<string, int> CalculateIngredientsByRecipe(MinecraftRecipe recipe, int desiredOutputQuantity)
        {
            var counts = new Dictionary<string, int>();

            int outputCount = recipe.Result?.Count ?? 1;
            int craftsNeeded = (int)Math.Ceiling((double)desiredOutputQuantity / outputCount);

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

                                AddCount(counts, key, ingredient.Count * craftsNeeded);
                            }
                        }
                    }
                    break;

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

                        AddCount(counts, key, ingredient.Count * craftsNeeded);
                    }
                    break;

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
                            AddCount(counts, key, recipe.Ingredient.Count * craftsNeeded);
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
    }
}
