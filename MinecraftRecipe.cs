using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MinecraftCraftingCalculator
{
    // Important Get & Set for MinecraftRecipe Storage in Application
    public class MinecraftRecipe
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("pattern")]
        public string[] Pattern { get; set; }

        [JsonProperty("key")]
        [JsonConverter(typeof(KeyIngredientConverter))]
        public Dictionary<string, List<Ingredient>> Key { get; set; }

        [JsonProperty("ingredients")]
        [JsonConverter(typeof(IngredientListConverter))]
        public List<Ingredient> Ingredients { get; set; }

        [JsonProperty("ingredient")]
        [JsonConverter(typeof(IngredientSingleOrArrayConverter))]
        public Ingredient Ingredient { get; set; }

        [JsonProperty("result")]
        private JToken _resultRaw { get; set; }

        [JsonIgnore]
        public Result Result
        {
            get
            {
                if (_resultRaw == null) return null;

                if (_resultRaw.Type == JTokenType.String)
                {
                    return new Result
                    {
                        Item = _resultRaw.ToString(),
                        Count = 1
                    };
                }
                else if (_resultRaw.Type == JTokenType.Object)
                {
                    var result = _resultRaw.ToObject<Result>();
                    if (result.Count == 0)
                        result.Count = 1;
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class Ingredient
    {
        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; } = 1;
    }

    public class Result
    {
        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; } = 1;
    }
}
