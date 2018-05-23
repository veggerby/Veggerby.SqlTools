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
    public class Row
    {
        public IEnumerable<ColumnValue> Values { get; }

        public ColumnValue this[ColumnDefinition column] => Values.Single(x => x.Column.Index == column.Index);

        public bool Filter(ColumnDefinition filterColumn, IEnumerable<string> filters)
        {
            return filters == null || filterColumn == null || filters.Contains(this[filterColumn].StringValue);
        }

        public Row(IEnumerable<ColumnValue> values)
        {
            Values = (values ?? Enumerable.Empty<ColumnValue>()).ToList();
        }
    }
}