using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MinecraftCraftingCalculator
{
    public class IngredientListConverter : JsonConverter<List<Ingredient>>
    {
        public override List<Ingredient> ReadJson(JsonReader reader, Type objectType, List<Ingredient> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var ingredients = new List<Ingredient>();

            var token = JToken.Load(reader);

            // Case of if J<T> is an Array of Objects
            if (token.Type == JTokenType.Array)
            {
                foreach (var child in token)
                {
                    if (child.Type == JTokenType.Array)
                    {
                        // Deserialize each element as an Ingredient
                        var nested = child.ToObject<List<Ingredient>>(serializer);
                        ingredients.AddRange(nested);
                    }
                    else if (child.Type == JTokenType.Object)
                    {
                        var ingredient = child.ToObject<Ingredient>(serializer);
                        ingredients.Add(ingredient);
                    }
                    else
                    {
                        throw new JsonSerializationException($"Unexpected token type in ingredients: {child.Type}");
                    }
                }
            }
            // Case of if J<T> is a Single Object
            else if (token.Type == JTokenType.Object)
            {
                // Single ingredient object
                var ingredient = token.ToObject<Ingredient>(serializer);
                ingredients.Add(ingredient);
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token type for ingredients: {token.Type}");
            }

            return ingredients;
        }

        public override void WriteJson(JsonWriter writer, List<Ingredient> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
