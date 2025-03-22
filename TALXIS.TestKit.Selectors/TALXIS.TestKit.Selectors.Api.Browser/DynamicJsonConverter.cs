// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TALXIS.TestKit.Selectors.Browser
{
    public sealed class DynamicJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jToken = JToken.Load(reader);
            return jToken.Type == JTokenType.Object ? new DynamicJsonObject(jToken.ToObject<IDictionary<string, object>>()) : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object);
        }

        private sealed class DynamicJsonObject : DynamicObject
        {
            private readonly IDictionary<string, object> _dictionary;

            public DynamicJsonObject(IDictionary<string, object> dictionary)
            {
                _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            }

            public override string ToString()
            {
                var sb = new StringBuilder("{");
                var first = true;

                foreach (var pair in _dictionary)
                {
                    if (!first)
                        sb.Append(",");

                    first = false;
                    AppendValue(sb, pair.Key, pair.Value);
                }

                sb.Append("}");
                return sb.ToString();
            }

            private void AppendValue(StringBuilder sb, string key, object value)
            {
                sb.Append($"\"{key}\":");

                switch (value)
                {
                    case string str:
                        sb.Append($"\"{str}\"");
                        break;

                    case IDictionary<string, object> dict:
                        sb.Append(new DynamicJsonObject(dict).ToString());
                        break;

                    case IEnumerable enumerable and not string:
                        sb.Append("[");
                        var firstInArray = true;
                        foreach (var item in enumerable)
                        {
                            if (!firstInArray)
                                sb.Append(",");
                            firstInArray = false;

                            switch (item)
                            {
                                case IDictionary<string, object> dictItem:
                                    sb.Append(new DynamicJsonObject(dictItem).ToString());
                                    break;
                                case string strItem:
                                    sb.Append($"\"{strItem}\"");
                                    break;
                                default:
                                    sb.Append(item);
                                    break;
                            }
                        }
                        sb.Append("]");
                        break;

                    default:
                        sb.Append(value);
                        break;
                }
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                if (!_dictionary.TryGetValue(binder.Name, out result))
                {
                    result = null;
                    return true;
                }

                result = WrapResultObject(result);
                return true;
            }

            public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
            {
                if (indexes.Length != 1 || indexes[0] == null)
                {
                    result = null;
                    return false;
                }

                if (!_dictionary.TryGetValue(indexes[0].ToString(), out result))
                {
                    result = null;
                    return true;
                }

                result = WrapResultObject(result);
                return true;
            }

            private static object WrapResultObject(object result)
            {
                return result switch
                {
                    IDictionary<string, object> dictionary => new DynamicJsonObject(dictionary),
                    IList list when list.Count > 0 => list[0] is IDictionary<string, object>
                        ? list.Cast<IDictionary<string, object>>().Select(x => new DynamicJsonObject(x)).ToList()
                        : list.Cast<object>().ToList(),
                    _ => result
                };
            }
        }
    }
}