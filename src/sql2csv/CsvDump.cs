using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Veggerby.Sql2Csv.Configuration;

namespace Veggerby.Sql2Csv
{
    public class CsvDump
    {
        private readonly ExportConfiguration _config;

        public CsvDump(ExportConfiguration config)
        {
            _config = config;
        }

        public int Execute()
        {
            if (!_config.IsValid)
            {
                return -1;
            }

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                var query = _config.SqlQuery;

                var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;

                _config.WriteLineVerbose((() => "Query:"));
                _config.WriteLineVerbose(() => query);

                var count = 0;
                var timer = Stopwatch.StartNew();

                var filters = _config.Filter;

                var encoding = Encoding.GetEncoding(_config.Encoding);

                using (var writer = new StreamWriter(_config.OutputFileName, false, encoding))
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    var columns = reader.GetColumnDefinitions();

                    if (_config.Verbose)
                    {
                        foreach (var column in columns)
                        {
                            _config.WriteLineVerbose(() => $"{column.Index}: {column.Name} of type {column.Type.Name}");
                        }
                    }

                    if (_config.FilterColumnIndex != null && _config.FilterColumnIndex > columns.Count())
                    {
                        return -1;
                    }

                    var filterColumn = _config.FilterColumnIndex != null
                        ? columns.FirstOrDefault(x => x.Index == _config.FilterColumnIndex.Value - 1)
                        : columns.FirstOrDefault(x => string.Equals(x.Name, _config.FilterColumnName));

                    writer.WriteLine(string.Join(_config.Separator, columns.Select(x => x.Name.GetQuoted(_config.Quoted))));

                    while (reader.Read() && (_config.Limit == null || count < _config.Limit.Value))
                    {
                        var row = reader.GetRow(columns);

                        if (!row.Filter(filterColumn, filters))
                        {
                            continue;
                        }

                        var line = row.ToString(_config);
                        writer.WriteLine(line);

                        count++;

                        if (_config.Verbose && count % 1000 == 0)
                        {
                            _config.Out.Write(".");
                        }

                        if (_config.Verbose && count % 25000 == 0)
                        {
                            _config.Out.WriteLine($"{count} rows exported");
                        }
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        _config.WriteLineVerbose(() => "Closing connection...");
                        connection.Close();
                    }
                }

                _config.WriteLineVerbose(() => $"Time to run: {timer.Elapsed}");
                _config.WriteLineVerbose(() => $"Records exported: {count}");
            }

            return 0;
        }
    }
}