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
    public static class Extensions
    {
        public static ConfigurationFile LoadConfiguration(this CommandOption configFile)
        {
            if (!configFile.HasValue())
            {
                return null;
            }

            if (!File.Exists(configFile.Value()))
            {
                throw new ArgumentException("Specified configuration file not exist", nameof(configFile));
            }

            var config = File.ReadAllText(configFile.Value());
            var conf = JsonConvert.DeserializeObject<ConfigurationFile>(config);
            return conf;
        }

        public static string GetQuoted(this string value, bool quoted)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (quoted)
            {
                return $"\"{value}\"";
            }

            return value;
        }

        public static IEnumerable<ColumnDefinition> GetColumnDefinitions(this IDataReader reader)
        {
            var columns = new List<ColumnDefinition>();
            for(int i = 0; i < reader.FieldCount; i++)
            {
                var column = new ColumnDefinition(i, reader.GetName(i), reader.GetFieldType(i));
                columns.Add(column);
            }

            return columns;
        }

        public static Row GetRow(this IDataReader reader, IEnumerable<ColumnDefinition> columns)
        {
            var values = columns.Select(x => reader.GetColumnValue(x)).ToList();
            return new Row(values);
        }

        public static ColumnValue GetColumnValue(this IDataReader reader, ColumnDefinition column)
        {
            var rawValue = reader.GetValue(column.Index);
            if (DBNull.Value.Equals(rawValue))
            {
                rawValue = null;
            }

            return new ColumnValue(column, rawValue);
        }

        public static string GetValueAsString(this ColumnValue value)
        {
            if (value.RawValue == null)
            {
                return null;
            }

            if (value.RawValue is string)
            {
                return (string)value.RawValue;
            }

            if (value.RawValue is Guid)
            {
                return ((Guid)value.RawValue).ToString();
            }

            if (value.RawValue is DateTime)
            {
                return ((DateTime)value.RawValue).ToString("o");
            }

            if (value.RawValue is int)
            {
                return ((int)value.RawValue).ToString(CultureInfo.InvariantCulture);
            }

            if (value.RawValue is double)
            {
                return ((double)value.RawValue).ToString(CultureInfo.InvariantCulture);
            }

            if (value.RawValue is float)
            {
                return ((float)value.RawValue).ToString(CultureInfo.InvariantCulture);
            }

            if (value.RawValue is bool)
            {
                return (bool)value.RawValue ? "TRUE" : "FALSE";
            }

            if (value.RawValue is decimal)
            {
                return ((decimal)value.RawValue).ToString(CultureInfo.InvariantCulture);
            }

            throw new Exception($"Column {value.Column.Name} with type {value.Column.Type.Name} is not mapped");
        }

        public static string ToString(this Row row, ExportConfiguration config)
        {
            return string.Join(config.Separator, row.Values.OrderBy(x => x.Column.Index).Select(x => x.StringValue.GetQuoted(config.Quoted)));
        }

        public static int? ParseInt(this string s)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }

            return null;
        }
    }
}