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
    public class ConnectionConfiguration
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("connection")]
        public string Connection { get; set; }
    }
}