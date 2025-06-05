using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TALXIS.TestKit.Bindings
{
    public class TestRecord
    {
        [JsonPropertyName("statecode")]
        public int StateCode { get; set; }

        [JsonPropertyName("statuscode")]
        public int StatusCode { get; set; }
    }
}
