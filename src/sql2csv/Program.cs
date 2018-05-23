using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Newtonsoft.Json;
using Veggerby.Sql2Csv.Configuration;

namespace Veggerby.Sql2Csv
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: true);
            app.FullName = "SQL to CSV";
            app.Name = "sql2csv";
            app.HelpOption("-?|-h|--help");

            var verboseOption = app.Option("-v|--verbose", "Show verbose output", CommandOptionType.NoValue);
            var configOption = app.Option("-C|--config <file>", "Specify configuration file", CommandOptionType.SingleValue);

            app.Command("export", command => {
                command.HelpOption("-?|-h|--help");

                var options = new ConfigurationOptions(
                    verboseOption,
                    configOption,
                    command.Option("-c|--connection <connection>", "The connection string", CommandOptionType.SingleValue),
                    command.Option("-i|--input <file>", "Input SQL script file", CommandOptionType.SingleValue),
                    command.Option("-o|--output <file>", "The output file", CommandOptionType.SingleValue),
                    command.Option("-q|--sql <query>", "Input SQL query", CommandOptionType.SingleValue),
                    command.Option("-S|--separator <separator>", "Specify the separator to use, default to ;", CommandOptionType.SingleValue),
                    command.Option("-e|--encoding <encoding>", "Specify the encoding of the output file, default utf-8", CommandOptionType.SingleValue),
                    command.Option("-Q|--quoted", "Specify whether values should be quoted, default to true", CommandOptionType.NoValue),
                    command.Option("-l|--limit <value>", "Specify record limit count", CommandOptionType.SingleValue),
                    command.Option("-f|--filter <file>", "Specify filter file", CommandOptionType.SingleValue),
                    command.Option("-i|--index <col>", "Specify the column index or name to filter by, default 1 (first column)", CommandOptionType.SingleValue));

                command.OnExecute(() => {
                    var configFile = configOption.LoadConfiguration();
                    var config = new ExportConfiguration(options, configFile);

                    var task = new CsvDump(config);
                    return task.Execute();
                });
            });

            app.Execute(args);
        }
    }
}
