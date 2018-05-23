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
    public class FilterConfiguration
    {
        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("column")]
        public int Column { get; set; }
    }
}