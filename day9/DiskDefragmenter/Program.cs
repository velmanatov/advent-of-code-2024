ReadDiskMap("input.txt");

static void ReadDiskMap(string filePath)
{
    string contents = File.ReadAllText(filePath);
    List<int?> diskMap1 = new List<int?>();
    Queue<Range> gaps = new Queue<Range>();

    // for Part 2 create a stack of the non-gaps instead. solution ends up a bit messy as I have just used different data structures for the two parts
    Stack<Range> fileBlocks = new Stack<Range>();
    List<Range> gapsList = new List<Range>();

    int j = 0;
    int fileId = 0;
    for (int i = 0; i< contents.Length; i++) {
        int value = int.Parse(contents.Substring(i, 1));
        if (i%2 == 0) {
            fileBlocks.Push(new Range(j, j + value - 1));
            for (int k = 0; k < value; k++) {
                diskMap1.Add(i/2);
                j++;
                fileId++;
            }
        } else if (value > 0) {
            gaps.Enqueue(new Range(j, j + value - 1));
            gapsList.Add(new Range(j, j + value - 1));
            for (int k = 0; k < value; k++) {
                diskMap1.Add(null);
                j++;
            }
        }
    }

    // Create a separate disk map for part 2
    List<int?> diskMap2 = new List<int?>(diskMap1);

    // part 1 - dequeue gaps one by one and fill them in from the end of the list
    while (gaps.TryDequeue(out var gap)) {

        // as we pop items off the end of diskMap we will consume some of the gaps (but not remove them from the queue)
        // so we need to double check if the gap is now outside of the bounds of the list (which suggest we are now done)
        if (gap.Start.Value > diskMap1.Count)
            break;

        for (int i = gap.Start.Value; i <= gap.End.Value; i++) {

            // skip any gaps
            int? poppedValue = null;
            while (poppedValue == null && i < diskMap1.Count) {
                poppedValue = diskMap1[diskMap1.Count -1];
                diskMap1.RemoveAt(diskMap1.Count - 1);
            }

            if (i < diskMap1.Count && poppedValue != null) {
                diskMap1[i] = poppedValue;
            }
        }
    }

    long answer1 = 0;
    for(int i = 0; i < diskMap1.Count; i++) {
        if (!diskMap1[i].HasValue) {
            break; // stop looking if there are gaps at end
        }
        answer1 += i * diskMap1[i].Value;
    }
    Console.WriteLine($"Answer 1 : {answer1}");


    // part 2 - this time it make more sense to start from the end of the list and place file blocks into gaps
    while (fileBlocks.TryPop(out var block)) {
        var blockWidth = block.End.Value - block.Start.Value + 1;
        // find the first gap into which the block will fit
        var gapIndex = gapsList.FindIndex(gap => (gap.Start.Value < block.Start.Value) && (gap.End.Value - gap.Start.Value + 1 >= blockWidth));
        // if there's nowhere it will fit thats fine. We just leave it otherwise adjust the fileMap2 to add the file in the gap
        // and update our gap list
        if (gapIndex >= 0) {
            var gap = gapsList[gapIndex];
            var fileId2 = diskMap2[block.Start];
            for (int i = 0; i < blockWidth; i++) {
                diskMap2[gap.Start.Value + i] = fileId2;
                diskMap2[block.Start.Value + i] = null;
            }
            var gapRemaining = gap.End.Value - gap.Start.Value - blockWidth + 1;
            if (gapRemaining > 0) {
                gapsList[gapIndex] = new Range(gap.Start.Value + blockWidth, gap.End);
            } else {
                gapsList.RemoveAt(gapIndex);
            }
        }
    }

    long answer2 = 0;
    for(int i = 0; i < diskMap2.Count; i++) {
        // this time we can have gaps in the middle - just ignore them
        if (diskMap2[i].HasValue) {
            answer2 += i * diskMap2[i].Value;
        }
    }
    Console.WriteLine($"Answer 2: {answer2}");
}
