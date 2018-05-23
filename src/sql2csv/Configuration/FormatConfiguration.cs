using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace Veggerby.Sql2Csv.Configuration
{
    public class FormatConfiguration
    {
        [JsonProperty("separator")]
        public string Separator { get; set; }

        [JsonProperty("quoted")]
        public bool Quoted { get; set; }

        [JsonProperty("encoding")]
        public string Encoding { get; set; }
    }
}