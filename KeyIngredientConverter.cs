using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MinecraftCraftingCalculator
{
    public class KeyIngredientConverter : JsonConverter<Dictionary<string, List<Ingredient>>>
    {
        // JSON Interpreter for Recipe Files
        public override Dictionary<string, List<Ingredient>> ReadJson(JsonReader reader, Type objectType, Dictionary<string, List<Ingredient>> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<string, List<Ingredient>>();
            var jo = JObject.Load(reader);

            foreach (var prop in jo.Properties())
            {
                if (prop.Value.Type == JTokenType.Array)
                {
                    var ingredients = prop.Value.ToObject<List<Ingredient>>(serializer);
                    result[prop.Name] = ingredients;
                }
                else if (prop.Value.Type == JTokenType.Object)
                {
                    var ingredient = prop.Value.ToObject<Ingredient>(serializer);
                    result[prop.Name] = new List<Ingredient> { ingredient };
                }
                else
                {
                    throw new JsonSerializationException($"Unexpected token type in key for '{prop.Name}': {prop.Value.Type}");
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, Dictionary<string, List<Ingredient>> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
