
// Day 1 - Part 1 asks us to order two lists of corresponding numbers, find the difference between corresponding pairs and sum the difference.
var lists = ReadInputLists("input.txt");

var leftList = lists.Item1.Order().ToArray();
var rightList = lists.Item2.Order().ToArray();

if (leftList.Length > rightList.Length)
    throw new Exception("Lists were not of equal length");

var sum = 0;

for (var i = 0; i < leftList.Length; i++) {
    Console.WriteLine($"i: {leftList[i]}, {rightList[i]}");
    sum += Math.Abs(leftList[i] - rightList[i]);
}

Console.WriteLine($"Answer Is: {sum}");

Console.ReadLine();


// Parses the file at the specified filePath. Expects to find sets of lines containing two integers separated by whitespace
// Returns a tupe containnig a list of the left-most and a list of the right-most integers
static (List<int>, List<int>) ReadInputLists(string filePath)
{
    string? line;
    var reader = new StreamReader(filePath);

    line = reader.ReadLine();

    var leftList = new List<int>();
    var rightList = new List<int>();

    while (line != null)
    {
        var parts = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) {
            throw new Exception($"There were not exactly 2 parts in the line: {line}");
        }
        leftList.Add(int.Parse(parts[0]));
        rightList.Add(int.Parse(parts[1]));
        line = reader.ReadLine();
    }
    reader.Close();

    return (leftList, rightList);
}
