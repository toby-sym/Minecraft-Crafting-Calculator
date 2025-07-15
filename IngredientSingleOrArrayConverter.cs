using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MinecraftCraftingCalculator
{
    public class IngredientSingleOrArrayConverter : JsonConverter<Ingredient>
    {
        public override Ingredient ReadJson(JsonReader reader, Type objectType, Ingredient existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Object)
            {
                return token.ToObject<Ingredient>(serializer);
            }
            else if (token.Type == JTokenType.Array)
            {
                var array = token.ToObject<Ingredient[]>(serializer);
                if (array.Length > 0)
                    return array[0]; // Take first ingredient if array
                else
                    return null;
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token type for ingredient: {token.Type}");
            }
        }

        public override void WriteJson(JsonWriter writer, Ingredient value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
