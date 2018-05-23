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
    public class ColumnValue
    {
        public ColumnDefinition Column { get; }
        public string StringValue => this.GetValueAsString();
        public object RawValue { get; }

        public ColumnValue(ColumnDefinition column, object rawValue)
        {
            Column = column;
            RawValue = rawValue;
        }
    }
}