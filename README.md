# Veggerby SQL Tools

Various SQL tools

## SQL 2 CSV

    Commandline utility that dumps a SQL query directly to a CSV file.

    SQL to CSV

    Usage: sql2csv [options] [command]

    Options:
    -?|-h|--help        Show help information
    -v|--verbose        Show verbose output
    -C|--config <file>  Specify configuration file

    Commands:
    export

    Use "sql2csv [command] --help" for more information about a command.

### SQL 2 CSV Export

    Usage: sql2csv export [options]

    Options:
    -?|-h|--help                  Show help information
    -c|--connection <connection>  The connection string
    -i|--input <file>             Input SQL script file
    -o|--output <file>            The output file
    -q|--sql <query>              Input SQL query
    -S|--separator <separator>    Specify the separator to use, default to ;
    -e|--encoding <encoding>      Specify the encoding of the output file, default utf-8
    -Q|--quoted                   Specify whether values should be quoted, default to true
    -l|--limit <value>            Specify record limit count
    -f|--filter <file>            Specify filter file
    -i|--index <col>              Specify the column index or name to filter by, default 1 (first column)

Config file format:

    {
        "verbose": true,
        "connection": "connection string - either named or explicit",
        "script": "input SQL script file - mutually exclusive with query",
        "query": "input SQL query - mutually exclusive with script file",
        "output": "output file",
        "connections": {
            "con1": "named connection string 1",
            "con2": "named connection string 2"
        },
        "format": {
            "quoted": true,
            "separator": "CSV separator, default to ; (semicolon)",
            "encoding": "output file encoding"
        },
        "filter": {
            "file": "file of filter values, one per line. only columns with these values will be exported",
            "column": "the index of the column to filter by, default 1"
        }
    }