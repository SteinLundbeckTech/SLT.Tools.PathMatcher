/*
    @Date                 : 09.05.2026
    @Author               : Stein Lundbeck
    @Description          : null
    @Version              : 1.0.0.2
    @Latest               : 10.05.2026
*/

using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using TextCopy;

Console.WriteLine("Path Matcher 1.0");
WriteSectionLine();

Matcher mather = new(StringComparison.OrdinalIgnoreCase);

if (args.Any(g => g.ToLower().Equals("-help")) || args.Any(g => g.Equals("-?")))
{
    Console.WriteLine("Help - Commands");
    WriteSectionLine();
    Console.WriteLine("\t-path <path>, -p <path> : Specify the path to search in.");
    Console.WriteLine("\t-patterns <list> : Comma-separated list of patterns to include (e.g., ./**/*.txt, ./*.cs).");
    Console.WriteLine("\t-exclude <list>, -e <list> : Comma-separated list of patterns to exclude.");
    Console.WriteLine("\t-wrap <string>, -w <string> : Wrap the matched result items with specified string.");
    Console.WriteLine("\t-list, -l : List the matched file paths.");
    Console.WriteLine("\t-copy, -c : Copy the matched file paths to the clipboard.");
}
else
{
    string path = Environment.CurrentDirectory;
    string wrap = string.Empty;

    if (args.Any(a => a.ToLower().Equals("-path")) || args.Any(a => a.ToLower().Equals("-p")))
    {
        int _ = args.IndexOf("-path") > -1 ? args.IndexOf("-path") : args.IndexOf("-p");

        if (args.Length > _)
        {
            if (Path.Exists(args[_ + 1]))
            {
                path = args[_ + 1];
            }
            else
            {
                Console.WriteLine("Path doesn't exist. Using current directory.");
            }
        }
        else
        {
            Console.WriteLine("No path provided after '-path' argument. Using current directory.");
        }
    }
    else
    {
        Console.WriteLine("No path provided. Using current directory.");
    }

    if (args.Any(a => a.ToLower().Equals("-patterns") && args.IndexOf("-patterns") < args.Length))
    {
        int _ = args.IndexOf("-patterns");
        string patterns = args[_ + 1];

        mather.AddIncludePatterns(args[_ + 1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    }
    else if (args.Any(g => g.ToLower().Equals("-patterns")))
    {
        Console.WriteLine("No pattern provided after '-patterns' argument.");
    }

    if (args.Any(a => a.ToLower().Equals("-exclude")) || args.Any(a => a.ToLower().Equals("-e")))
    {
        int _ = args.IndexOf("-exclude") > -1 ? args.IndexOf("-exclude") : args.IndexOf("-e");

        if (args.Length > _)
        {
            mather.AddExcludePatterns(args[_ + 1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }
    }

    if (args.Any(a => a.ToLower().Equals("-wrap")) || args.Any(a => a.ToLower().Equals("-w")))
    {
        int _ = args.IndexOf("-wrap") > -1 ? args.IndexOf("-wrap") : args.IndexOf("-w");

        if (args.Length > _)
        {
            wrap = args[_ + 1];
        }
    }

    DirectoryInfo dir = new(path);

    if (dir.Exists)
    {
        PatternMatchingResult rs = mather.Execute(new DirectoryInfoWrapper(dir));

        if (rs.HasMatches)
        {
            Console.WriteLine($"Found {rs.Files.Count()} matches.");
            Console.WriteLine();
            ICollection<string> paths = [];

            foreach (FilePatternMatch file in rs.Files)
            {
                paths.Add($"{Path.Combine(dir.FullName, file.Path.Replace("/", "\\"))}");
            }

            if (args.Any(a => a.Equals("-list")) || args.Any(a => a.Equals("-l")))
            {
                Console.WriteLine("Matches:");
                WriteSectionLine();

                foreach (string pth in paths)
                {
                    Console.WriteLine($"\t{pth}");
                }
            }

            if ((args.Any(a => a.Equals("-copy")) || args.Any(a => a.Equals("-c"))))
            {
                string toCopy = string.Empty;

                foreach (string p in paths)
                {
                    toCopy += $"{wrap}{p}{wrap}, ";
                }

                toCopy = toCopy.TrimEnd().TrimEnd(',');

                ClipboardService.SetText(toCopy);
                WriteSectionLine();
                Console.WriteLine("Copied to clipboard.");
            }
        }
        else
        {
            Console.WriteLine($"No matches found.");
        }
    }
    else
    {
        Console.WriteLine("Directory not found: " + path);
    }

    Console.WriteLine("Press any key to exit.");
    Console.ReadLine();
    Console.WriteLine("Bye");
    Console.WriteLine();
}

void WriteSectionLine() => Console.WriteLine("----------------------");
