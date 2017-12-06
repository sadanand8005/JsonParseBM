using Newtonsoft.Json;

namespace JsonParseBM
{
    public class SimpleObjectDeserialiser
    {
        public SimpleObject Deserialise(JsonTextReader reader)
        {

            var currentProperty = string.Empty;
            var response = new SimpleObject();

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        currentProperty = reader.Value.ToString();
                        continue;
                    }

                    switch (currentProperty)
                    {
                        case "Name":
                            response.Name = reader.Value.ToString();
                            break;
                        case "Count":
                            int _count;
                            int.TryParse(reader.Value.ToString(), out _count);
                            response.Count = _count;
                            break;
                    }
                }
                else
                {
                }

                if (reader.TokenType == JsonToken.EndObject) break;
            }

            return response;
        }
    }
}
