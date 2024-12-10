ReadDiskMap("input.txt");

static void ReadDiskMap(string filePath)
{
    string contents = File.ReadAllText(filePath);

    //Console.WriteLine(contents);

    List<int?> diskMap = new List<int?>();
    Queue<Range> gaps = new Queue<Range>(); 
    
    int j = 0;
    int fileId = 0;
    for (int i = 0; i< contents.Length; i++) {
        int value = int.Parse(contents.Substring(i, 1));
        if (i%2 == 0) {
            for (int k = 0; k < value; k++) {
                diskMap.Add((int)(i/2));
                j++;
                fileId++;
            }
        } else if (value > 0) {
            gaps.Enqueue(new Range(j, j + value - 1));
            for (int k = 0; k < value; k++) {
                diskMap.Add(null);
                j++;
            }
        }
    }

    //Console.WriteLine(string.Join(", ", diskMap));
    //Console.WriteLine(string.Join(", ", gaps));

    while (gaps.TryDequeue(out var gap)) {

        // as we pop items off the end of diskMap we will consume some of the gaps (but not remove them from the queue)
        // so we need to double check if the gap is now outside of the bounds of the list (which suggest we are now done)
        if (gap.Start.Value > diskMap.Count)
            break;

        for (int i = gap.Start.Value; i <= gap.End.Value; i++) {

            // skip any gaps
            int? poppedValue = null;
            while (poppedValue == null && i < diskMap.Count) {
                poppedValue = diskMap[diskMap.Count -1];
                diskMap.RemoveAt(diskMap.Count - 1);
            }

            //Console.WriteLine($"inserting at {i} [string length is {diskMap.Count}]");
            if (i < diskMap.Count && poppedValue != null) {
                diskMap[i] = poppedValue;
            }
        }
    }

    //Console.WriteLine(string.Join(", ", diskMap));
    //Console.WriteLine(diskMap.Count);
    long answer1 = 0;
    for(int i = 0; i < diskMap.Count; i++) {
        if (!diskMap[i].HasValue) {
            break; // stop looking if there are gaps at end
        }
        answer1 += i * diskMap[i].Value;
    }
    Console.WriteLine(answer1);
}
