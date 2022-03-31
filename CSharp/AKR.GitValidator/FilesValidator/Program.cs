using CommandLine;
using System;

namespace FilesValidator
{
    public class Program
    {
        public class Options
        {
            [Option('p', "path", Required = true, HelpText = "Path under which the json files need to search")]
            public string GitPath
            {
                get; set;
            }

            [Option('r', "recurse", Required = false, HelpText = "Recurse the sub-folders and find the files. Default = true")]
            public bool Recurse { get; set; } = true;
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>{
            }
            Console.WriteLine("Hello World!");
        }
    }
}