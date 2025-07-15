using System;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftCraftingCalculator
{
    public partial class RecipeSelectionItem : UserControl
    {
        public MinecraftRecipe Recipe { get; private set; }

        public bool IsSelected => chkSelect.Checked;

        public int Quantity => (int)numQuantity.Value;

        public RecipeSelectionItem(MinecraftRecipe recipe, int defaultQuantity)
        {
            InitializeComponent();

            Recipe = recipe;

            chkSelect.Text = GenerateRecipeDescription(recipe);
            chkSelect.Checked = true;

            numQuantity.Minimum = 1;
            numQuantity.Maximum = 9999;
            numQuantity.Value = defaultQuantity;
        }

        private string GenerateRecipeDescription(MinecraftRecipe recipe)
        {
            string outputItem = ShortenItemName(recipe.Result?.Item ?? "Unknown item");
            int outputCount = recipe.Result?.Count ?? 1;

            // Collect ingredients depending on recipe type
            List<(string item, int count)> ingredientsList = new();

            if (recipe.Type == null)
                return $"Recipe producing {outputCount} x {outputItem}";

            if (recipe.Type.StartsWith("minecraft:crafting_shaped") && recipe.Key != null && recipe.Pattern != null)
            {
                // Gather counts from recipe.Key dictionary by pattern
                var tempCounts = new Dictionary<string, int>();
                foreach (var row in recipe.Pattern)
                {
                    foreach (var symbol in row)
                    {
                        if (symbol == ' ' || !recipe.Key.ContainsKey(symbol.ToString()))
                            continue;

                        var ingrList = recipe.Key[symbol.ToString()];
                        foreach (var ingr in ingrList)
                        {
                            if (ingr == null) continue;
                            string key = ingr.Item ?? ingr.Tag;
                            if (string.IsNullOrEmpty(key)) continue;

                            if (!tempCounts.ContainsKey(key))
                                tempCounts[key] = 0;
                            tempCounts[key] += ingr.Count;
                        }
                    }
                }

                ingredientsList = tempCounts.Select(kvp => (ShortenItemName(kvp.Key), kvp.Value)).ToList();
            }
            else if (recipe.Type.StartsWith("minecraft:crafting_shapeless") && recipe.Ingredients != null)
            {
                var tempCounts = new Dictionary<string, int>();
                foreach (var ingr in recipe.Ingredients)
                {
                    if (ingr == null) continue;
                    string key = ingr.Item ?? ingr.Tag;
                    if (string.IsNullOrEmpty(key)) continue;

                    if (!tempCounts.ContainsKey(key))
                        tempCounts[key] = 0;
                    tempCounts[key] += ingr.Count;
                }
                ingredientsList = tempCounts.Select(kvp => (ShortenItemName(kvp.Key), kvp.Value)).ToList();
            }
            else if ((recipe.Type == "minecraft:smelting" || recipe.Type == "minecraft:blasting" ||
                      recipe.Type == "minecraft:smoking" || recipe.Type == "minecraft:campfire_cooking" ||
                      recipe.Type == "minecraft:stonecutting") && recipe.Ingredient != null)
            {
                string key = recipe.Ingredient.Item ?? recipe.Ingredient.Tag;
                if (!string.IsNullOrEmpty(key))
                    ingredientsList.Add((ShortenItemName(key), recipe.Ingredient.Count));
            }
            else
            {
                return $"Recipe producing {outputCount} x {outputItem} (no ingredient info)";
            }

            string ingredientsText = ingredientsList.Count > 0
                ? string.Join(", ", ingredientsList.Select(i => $"{i.count} x {i.item}"))
                : "No ingredients";

            return $"Produces {outputCount} x {outputItem} from {ingredientsText}";
        }


        private string ShortenItemName(string itemName)
        {
            if (string.IsNullOrEmpty(itemName)) return itemName;
            int colonIndex = itemName.IndexOf(':');
            if (colonIndex >= 0 && colonIndex < itemName.Length - 1)
                return itemName.Substring(colonIndex + 1);
            return itemName;
        }
    }
}
