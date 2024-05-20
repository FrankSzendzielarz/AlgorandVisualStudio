﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoStudio.ABI.ARC32
{
    public class AppDescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AppDescription);
        }
        public override bool CanWrite => false;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Deserialize the JSON to an AppDescription object
            AppDescription appDescription = serializer.Deserialize<AppDescription>(reader);

            
            // Update the Methods based on the Hints:
            foreach (var hint in appDescription.Hints)
            {
                // Find the corresponding method and update it. The method is identified either by its signature for strictly arc4 compliant
                // definitions or by its selector for when the C# compiler allows manual selector specifications.
                var method = appDescription.Contract.Methods.FirstOrDefault(m => (m.ARC4MethodSignature == hint.Key || Encoding.UTF8.GetString(m.Selector) == hint.Key));
                if (method != null)
                {
                    method.OnCompletion = hint.Value.Call_config ;
                    method.Defaults = hint.Value.Default_arguments;
                }
            }

            return appDescription;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
