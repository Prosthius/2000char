using System.Text;

string input;
int characterLength = 2000;
string help =
    @"Usage:
    ./LLM-Char [-c | --characters]
    ./LLM-Char [-h | --help]
    ./LLM-Char [-d | --delete]
    
Options:
    -h --help:          Show this screen
    -d --delete:        Delete all output files
    -c --characters:    Set the number of characters per output file (default: 2000)";

try
{
    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i])
        {
            case "-h":
            case "--help":
                Console.WriteLine(help);
                Environment.Exit(0);
                break;
            case "-d":
            case "--delete":
                clearOutputFiles();
                break;
            case "-c":
            case "--characters":
                i++;
                characterLength = int.Parse(args[i]);
                break;
            default:
                Console.WriteLine("Invalid argument: " + args[i] + "\nRun with -h or --help for help.");
                Environment.Exit(1);
                break;
        }
    }
    Console.WriteLine("Running...");

    input = File.ReadAllText("./input/input.txt");
    if (input == null || input == "")
    {
        Console.WriteLine("Input file is empty.");
        Environment.Exit(0);
    }

    List<string>? output = SplitString(input, characterLength);

    if (output == null) throw new Exception("Output is null.");

    for (int i = 0; i < output.Count; i++)
    {
        string block = output[i];
        File.WriteAllText($"./output/output{i + 1}.txt", block);
    }

    Console.WriteLine("Done! \nTo delete all output files, run the program with the -d or --delete flag.");
}
catch (FileNotFoundException)
{
    Console.WriteLine("Error reading input file. Please create ./input/input.txt.");
    return;
}
catch (Exception error)
{
    Console.WriteLine("Error: " + error.Message + "\n" + error.StackTrace);
    return;
}

List<string>? SplitString(string inputStr, int charLength)
{
    try
    {
        List<string> output = new List<string>();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < inputStr.Length; i += charLength)
        {
            if (i + charLength > inputStr.Length) charLength = inputStr.Length - i;
            sb.Append(inputStr.Substring(i, charLength));
            output.Add(sb.ToString());
            sb.Clear();
        }
        return output;
    }
    catch (Exception error)
    {
        Console.WriteLine("Error: " + error.Message);
        Environment.Exit(1);
        return null;
    }
}

void clearOutputFiles()
{
    try
    {
        string[] files = Directory.GetFiles("./output");
        foreach (string file in files)
        {
            // Prevents hidden files from being deleted
            if ((File.GetAttributes(file) & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                File.Delete(file);
            }
        }
        Console.WriteLine("Cleared output files.");
        Environment.Exit(0);
    }
    catch (Exception error)
    {
        Console.WriteLine("\nError: " + error.Message + "\n" + error.StackTrace);
        Environment.Exit(1);
    }
}
