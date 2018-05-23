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
    public class ConfigurationOptions
    {
        public CommandOption VerboseOption { get; }
        public CommandOption ConfigFileOption { get; }
        public CommandOption ConnectionOption { get; }
        public CommandOption InputScriptFileOption { get; }
        public CommandOption OutputFileOption { get; }
        public CommandOption InputSqlQueryOption { get; }
        public CommandOption SeparatorOption { get; }
        public CommandOption EncodingOption { get; }
        public CommandOption QuotedOption { get; }
        public CommandOption LimitOptions { get; }
        public CommandOption FilterFileOption { get; }
        public CommandOption FilterColumnOption { get; }

        public ConfigurationOptions(
            CommandOption verboseOption,
            CommandOption configFileOption,
            CommandOption connectionOption,
            CommandOption inputScriptFileOption,
            CommandOption outputFileOption,
            CommandOption inputSqlQueryOption,
            CommandOption separatorOption,
            CommandOption encodingOption,
            CommandOption quotedOption,
            CommandOption limitOptions,
            CommandOption filterFileOption,
            CommandOption filterColumnOption)
        {
            VerboseOption = verboseOption;
            ConfigFileOption = configFileOption;
            ConnectionOption = connectionOption;
            InputScriptFileOption = inputScriptFileOption;
            OutputFileOption = outputFileOption;
            InputSqlQueryOption = inputSqlQueryOption;
            SeparatorOption = separatorOption;
            EncodingOption = encodingOption;
            QuotedOption = quotedOption;
            LimitOptions = limitOptions;
            FilterFileOption = filterFileOption;
            FilterColumnOption = filterColumnOption;
        }
    }
}