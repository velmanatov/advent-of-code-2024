var map = ReadMapFromFile("input.txt");
CheckForAntinodes(map, false);
CheckForAntinodes(map, true);

static void CheckForAntinodes(CityAntennaMap map, bool isPart2) {
    var distinctFrequencies = map.DistinctAntennaeFrequencies;
    // set to track unique antinode positions identified
    HashSet<(int, int)> antinodes = [];

    foreach(var frequency in distinctFrequencies) {
        var antennae = map.Antennae[frequency].ToList();
        // pair all combinations of antennae with the same frequencies... check for antinodes
        if (antennae.Count >= 2) {
            for(int i = 0; i < antennae.Count - 1; i++) {
                for(int j = i + 1; j < antennae.Count; j++) {

                    var antenna1 = antennae.ElementAt(i);
                    var antenna2 = antennae.ElementAt(j);

                    bool inMapBounds = true;

                    // for part two the two antenna are always antinodes
                    if (isPart2) {
                        antinodes.Add((antenna1.X, antenna1.Y));
                        antinodes.Add((antenna2.X, antenna2.Y));
                    }

                    int dx = antenna2.X - antenna1.X;
                    int dy = antenna2.Y - antenna1.Y;

                    AddAntinodesInDirection(map, antenna1.X, antenna1.Y, dx, dy, antinodes, isPart2);
                    AddAntinodesInDirection(map, antenna2.X, antenna2.Y, -dx, -dy, antinodes, isPart2);
                }
            }  
        }
    }

    var mapStr = map.GetMapStringWithAntinodes(antinodes);

    Console.WriteLine(mapStr);
    Console.WriteLine($"Answer {(isPart2 ? 2 : 1)}: {antinodes.Count}");
}

static void AddAntinodesInDirection(CityAntennaMap map, int startX, int startY, int dx, int dy, HashSet<(int, int)> antinodes, bool isPart2) {
    bool inMapBounds = true;
    int multiple = 1;
    while (inMapBounds) {
        var possibleAntinode1 = new { X = startX- (multiple * dx), Y = startY - (multiple * dy) };

        inMapBounds = map.InMapBounds(possibleAntinode1.X, possibleAntinode1.Y);
        if (inMapBounds) {
            antinodes.Add((possibleAntinode1.X, possibleAntinode1.Y));                            
        }

        multiple++;
        // for Part 2 we can go multiple dx, dy away from the node but for Part 1 e will just break
        if (!isPart2)
            break;
    }
}

static CityAntennaMap ReadMapFromFile(string filePath)
{
    string? line;
    var reader = new StreamReader(filePath);

    line = reader.ReadLine();

    int y = 0;

    CityAntennaMap map = new CityAntennaMap { MapWidth = line.Length };
    List<Antenna> antennae = new List<Antenna>();

    while (line != null)
    {
        for (int x = 0; x< line.Length; x++) {
            char c = line[x];
            if (c != '.') {
                antennae.Add(new Antenna { Frequency = c, X = x, Y = y });
            }
        }
        line = reader.ReadLine();
        y++;
    }

    map.Antennae = antennae.ToLookup(a => a.Frequency);
    map.MapHeight = y;

    reader.Close();

    return map;
}
