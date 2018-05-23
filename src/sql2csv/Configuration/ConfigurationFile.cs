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
    public class ConfigurationFile
    {
        [JsonProperty("verbose")]
        public bool Verbose { get; set; }

        [JsonProperty("connection")]
        public string Connection { get; set; }

        [JsonProperty("script")]
        public string InputScriptFile { get; set; }

        [JsonProperty("query")]
        public string InputSqlQuery { get; set; }

        [JsonProperty("connections")]
        public ConnectionConfiguration[] Connections { get; set; }

        [JsonProperty("format")]
        public FormatConfiguration Format { get; set; }

        [JsonProperty("filter")]
        public FilterConfiguration Filter { get; set; }
    }
}