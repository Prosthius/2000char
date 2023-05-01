string input = File.ReadAllText("./input/input.txt");
int i = 1;

List<string> output = SplitString(input, 2000);

foreach (string block in output)
{
	File.WriteAllText($"./output/output{i}.txt", block);
	i++;
}

List<string> SplitString(string str, int blockSize)
{
    List<string> output = new List<string>();
    for (int i = 0; i < str.Length; i += blockSize)
    {
        if (i + blockSize > str.Length) blockSize = str.Length - i;
        output.Add(str.Substring(i, blockSize));
    }
    return output;
}