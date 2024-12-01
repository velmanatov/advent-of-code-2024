
// Day 1
// Part 1 asks us to order two lists of corresponding numbers, find the difference between corresponding pairs and sum the difference.
// Part 2 asks us to sum the number from the left list multiplied by how many times it appears in the right list
var lists = ReadInputLists("input.txt");

// Order the lists to simplify part 1 in particular
var leftList = lists.Item1.Order().ToArray();
var rightList = lists.Item2.Order().ToArray();

if (leftList.Length > rightList.Length)
    throw new Exception("Lists were not of equal length");

var answerPart1 = 0;
var answerPart2 = 0;

// Given that we've ordered the lists, it is efficient to just iterate through the right hand list and do the counts of occurrences up front
// numberCounts is a Dictionary mapping numbers found in right list to counts of those same numbers
var numberCounts = CountOccurrencesInList(rightList);

// Iterate through the left list, calculating the answer to both parts of the puzzle as we go
for (var i = 0; i < leftList.Length; i++) {
    var leftNum = leftList[i];
    answerPart1 += Math.Abs(leftNum - rightList[i]);

    if (numberCounts.TryGetValue(leftNum, out var rightNumCount)) {
        answerPart2 += leftNum * rightNumCount;
    }
}

Console.WriteLine($"Answer to Part 1 Is: {answerPart1}");
Console.WriteLine($"Answer to Part 2 Is: {answerPart2}");

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

// Accepts an ordered list and iterates through it counting occurrences of each number found
// Returns a dictionary mapping the number found to the count of that number
static Dictionary<int, int> CountOccurrencesInList(int[] list) {
    var numberCounts = new Dictionary<int, int>();

    int? numBeingCounted = null;
    var numCount = 0;

    for (var i = 0; i < list.Length; i++) {
        var num = list[i];
        if (num != numBeingCounted) {
            // When we find a new num...
            if (numBeingCounted != null) {
                // Store the count of the number we were counting if there is one...
                numberCounts.Add(numBeingCounted.Value, numCount);
                // Reset count
                numCount = 0;

            }
            // Deals with last number in list occurring only once
            if (i == list.Length - 1) {
                numberCounts.Add(num, 1);
            }
        }
        numCount ++;
        numBeingCounted = num;
    }

    return numberCounts;
}
