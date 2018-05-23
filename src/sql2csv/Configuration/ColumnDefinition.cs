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
    public class ColumnDefinition
    {
        public int Index { get; }
        public string Name { get; }
        public Type Type { get; }

        public ColumnDefinition(int index, string name, Type type)
        {
            Index = index;
            Name = name;
            Type = type;
        }
    }
}