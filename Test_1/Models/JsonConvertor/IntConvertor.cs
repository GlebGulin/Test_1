using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.JsonConvertor
{
    public class IntToStringConvertor : JsonConverter
    {
       
        public override bool CanRead => false;
        public override bool CanWrite => true;
        public override bool CanConvert(Type type) => type == typeof(int);

        public override void WriteJson(
            JsonWriter writer, object value, JsonSerializer serializer)
        {
            int number = (int)value;
            writer.WriteValue(number.ToString());
        }

        public override object ReadJson(
            JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }

}

