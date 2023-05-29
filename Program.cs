using System.Text;

string version = "v2.0";
string input;
int characterLength = 2000;
string help =
    @"Usage:
    ./LLM-Char [-c | --characters] [-nw | --no-whitespace] [-rc | --remove-char]
    ./LLM-Char [-h | --help]
    ./LLM-Char [-d | --delete]
    ./LLM-Char [-v | --version]
    
Options:
    -h  --help:             Show this screen
    -v  --version:          Show the version number
    -d  --delete:           Delete all output files that match 'output*.txt'
    -c  --characters:       Set the number of characters per output file (default: 2000)
    -nw --no-whitespace:    Remove all whitespace from the input text
    -rc --remove-char:      Remove all instances of a character from the input text. Supports inputting multiple characters one after the other";

try
{
    input = File.ReadAllText("./input/input.txt");
    if (input == null || input == "")
    {
        Console.WriteLine("Input file is empty.");
        Environment.Exit(1);
    }

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
                Console.WriteLine("Cleared output files.");
                Environment.Exit(0);
                break;
            case "-v":
            case "--version":
                Console.WriteLine(version);
                Environment.Exit(0);
                break;
            case "-c":
            case "--characters":
                i++;
                characterLength = int.Parse(args[i]);
                break;
            case "-nw":
            case "--no-whitespace":
                input = removeWhitespace(input);
                break;
            case "-rc":
            case "--remove-char":
                i++;
                char[] argsArr = args[i].ToCharArray();
                foreach (char c in argsArr)
                {
                    input = removeChar(input, c.ToString());
                }
                break;
            case "-rcs":
            case "--remove-chars":
                i++;
                input = removeChar(input, args[i]); // TODO - function checks each character individually
                break;
            default:
                Console.WriteLine("Invalid argument: " + args[i] + "\nRun with -h or --help for help.");
                Environment.Exit(1);
                break;
        }
    }
    Console.WriteLine("Running...");

    List<string>? output = SplitString(input, characterLength);

    if (output == null) throw new Exception("Output is null.");

    clearOutputFiles();

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
        Console.WriteLine("\nError: " + error.Message + "\n" + error.StackTrace);
        Environment.Exit(1);
        return null;
    }
}

string removeWhitespace(string inputStr)
{
    string output = "";
    StringBuilder sb = new StringBuilder();
    foreach (char c in inputStr)
    {
        if (!char.IsWhiteSpace(c)) sb.Append(c);
    }
    output = sb.ToString();
    sb.Clear();
    return output;
}

string removeChar(string inputStr, string charToRemove)
{
    string output = "";
    int charsToRemoveLength = charToRemove.Length;
    StringBuilder sb = new StringBuilder();
    foreach (char input in inputStr)
    {
        // Need to check characters based on charToRemove.Length. e.g. check input[0] + input[1], input[1] + input[2], etc.
        if (input.ToString() != charToRemove) sb.Append(input);
    }

    output = sb.ToString();
    sb.Clear();
    return output;
}

void clearOutputFiles()
{
    try
    {
        string[] files = Directory.GetFiles("./output", "output*.txt");
        foreach (string file in files) File.Delete(file);
    }
    catch (Exception error)
    {
        Console.WriteLine("\nError: " + error.Message + "\n" + error.StackTrace);
        Environment.Exit(1);
    }
}
