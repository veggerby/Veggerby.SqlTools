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
    public class ExportConfiguration
    {
        private readonly ConfigurationOptions _options;
        private readonly ConfigurationFile _configFile;

        public bool Verbose => _options.VerboseOption.HasValue() || (_configFile?.Verbose ?? false);

        private string SqlScriptFileName => _options.InputScriptFileOption.HasValue() ? _options.InputScriptFileOption.Value() : _configFile?.InputScriptFile;

        public string SqlQuery =>
                (_options.InputSqlQueryOption.HasValue() ? _options.InputSqlQueryOption.Value() : _configFile?.InputSqlQuery) ??
                (string.IsNullOrEmpty(SqlScriptFileName) && File.Exists(SqlScriptFileName) ? File.ReadAllText(SqlScriptFileName) : null);

        public string Separator => _options.SeparatorOption.HasValue() ? _options.SeparatorOption.Value() : (_configFile?.Format?.Separator ?? ";");
        public bool Quoted => _options.QuotedOption.HasValue() || (_configFile?.Format?.Quoted ?? true);
        public string Encoding => _options.EncodingOption.HasValue() ? _options.EncodingOption.Value() : _configFile.Format?.Encoding ?? "utf-8";

        private string FilterFileName => _options.FilterFileOption.HasValue() ? _options.FilterFileOption.Value() : _configFile.Filter?.File;
        public IEnumerable<string> Filter => !string.IsNullOrEmpty(FilterFileName) && File.Exists(FilterFileName) ? File.ReadAllLines(FilterFileName) : null;
        public int? FilterColumnIndex => _options.FilterColumnOption.HasValue() ? _options.FilterColumnOption.Value().ParseInt() : 1;
        public string FilterColumnName => _options.FilterColumnOption.HasValue() && FilterColumnIndex == null ? _options.FilterColumnOption.Value() : null;
        public string OutputFileName => _options.OutputFileOption.Value();

        public int? Limit => _options.LimitOptions.HasValue() ? (int?)int.Parse(_options.LimitOptions.Value()) : null;

        public string ConnectionString => GetConnectionString();

        public bool IsValid => !string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(SqlQuery) && !string.IsNullOrEmpty(OutputFileName);

        public TextWriter Out => Console.Out;

        public void WriteVerbose(Func<string> func)
        {
            if (!Verbose)
            {
                return;
            }

            Out.Write(func());
        }

        public void WriteLineVerbose(Func<string> func)
        {
            if (!Verbose)
            {
                return;
            }

            Out.WriteLine(func());
        }

        private string GetConnectionString()
        {
            var connectionOptionValue = _options.ConnectionOption.Value();

            if (_configFile?.Connections != null && _configFile.Connections.Any(x => x.Key.Equals(connectionOptionValue)))
            {
                return _configFile.Connections.First(x => x.Key.Equals(connectionOptionValue)).Value;
            }

            return connectionOptionValue;
        }

        public ExportConfiguration(ConfigurationOptions options, ConfigurationFile configFile)
        {
            _options = options;
            _configFile = configFile;
        }
    }
}