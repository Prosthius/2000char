﻿using System.Text;

string input;
int i = 1;

try
{
    Console.WriteLine("Running...");

    input = File.ReadAllText("./input/input.txt");
    if (input == "") clearOutputFiles();

    List<string>? output = SplitString(input, 2000);

    if (output == null) throw new Exception("Output text is null");

    foreach (string block in output)
    {
        File.WriteAllText($"./output/output{i}.txt", block);
        i++;
    }
    Console.WriteLine("Done! \nTo delete all output files, delete all text from input.txt and run the program again.");
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
        char response = 'n';

        Console.Write("input.txt is empty. Do you want to delete the contents of the output folder? (y/N): ");
        response = char.ToLower(Console.ReadKey().KeyChar);
        Console.WriteLine();
        if (response != 'y')
        {
            Console.WriteLine("Files were not modified.\nExiting...");
            Environment.Exit(1);
        }

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
        Environment.Exit(1);
    }
    catch (Exception error)
    {
        Console.WriteLine("\nError: " + error.Message + "\n" + error.StackTrace);
        Environment.Exit(1);
    }
}