using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MinecraftCraftingCalculator
{
    public partial class RecipeSelectionForm : Form
    {
        private string _itemName;
        private int _baseQuantity;
        private List<MinecraftRecipe> _recipes;

        public List<(MinecraftRecipe recipe, int quantity)> SelectedRecipes { get; private set; } = new();

        public RecipeSelectionForm(string itemName, int baseQuantity, List<MinecraftRecipe> recipes)
        {
            InitializeComponent();

            _itemName = itemName;
            _baseQuantity = baseQuantity;
            _recipes = recipes;

            lblItemName.Text = $"Select recipe for {_itemName} (base quantity: {_baseQuantity})";

            checkedListBoxRecipes.Items.Clear();

            foreach (var recipe in _recipes)
            {
                checkedListBoxRecipes.Items.Add(GetRecipeDescription(recipe), false);
            }

            // Enforce single selection in checked list box
            checkedListBoxRecipes.ItemCheck += CheckedListBoxRecipes_ItemCheck;
        }

        private string GetRecipeDescription(MinecraftRecipe recipe)
        {
            int outputCount = recipe.Result?.Count ?? 1;
            string outputItem = recipe.Result?.Item ?? "Unknown";

            string inputDescription = "";

            switch (recipe.Type)
            {
                case "minecraft:crafting_shaped":
                case "minecraft:crafting_shapeless":
                    var firstIngredient = GetFirstIngredient(recipe);
                    if (firstIngredient != null)
                    {
                        inputDescription = $"{firstIngredient.Count} {firstIngredient.Item ?? firstIngredient.Tag}";
                    }
                    else
                    {
                        inputDescription = "unknown input";
                    }
                    break;

                case "minecraft:smelting":
                case "minecraft:blasting":
                case "minecraft:smoking":
                case "minecraft:campfire_cooking":
                case "minecraft:stonecutting":
                    if (recipe.Ingredient != null)
                        inputDescription = $"{recipe.Ingredient.Count} {recipe.Ingredient.Item ?? recipe.Ingredient.Tag}";
                    else
                        inputDescription = "unknown input";
                    break;

                default:
                    inputDescription = "unknown input";
                    break;
            }

            return $"Crafting Recipe: {inputDescription} produces {outputCount} {outputItem}";
        }

        private Ingredient GetFirstIngredient(MinecraftRecipe recipe)
        {
            if (recipe.Type == "minecraft:crafting_shaped" && recipe.Key != null)
            {
                foreach (var keyPair in recipe.Key)
                {
                    var ingredients = keyPair.Value;
                    if (ingredients != null && ingredients.Count > 0)
                    {
                        foreach (var ingredient in ingredients)
                        {
                            if (ingredient != null && !string.IsNullOrEmpty(ingredient.Item ?? ingredient.Tag))
                                return ingredient;
                        }
                    }
                }
            }
            else if (recipe.Type == "minecraft:crafting_shapeless" && recipe.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (ingredient != null && !string.IsNullOrEmpty(ingredient.Item ?? ingredient.Tag))
                        return ingredient;
                }
            }
            return null;
        }

        private void CheckedListBoxRecipes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxRecipes.Items.Count; i++)
                {
                    if (i != e.Index && checkedListBoxRecipes.GetItemChecked(i))
                    {
                        checkedListBoxRecipes.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedRecipes.Clear();

            for (int i = 0; i < checkedListBoxRecipes.Items.Count; i++)
            {
                if (checkedListBoxRecipes.GetItemChecked(i))
                {
                    // Multiply quantity by base quantity from constructor
                    SelectedRecipes.Add((_recipes[i], _baseQuantity));
                    break; // only one allowed
                }
            }

            if (SelectedRecipes.Count == 0)
            {
                MessageBox.Show("Please select one recipe.");
                this.DialogResult = DialogResult.None; // prevent close
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
