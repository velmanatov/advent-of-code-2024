var map = ReadMapFromFile("input.txt");

var distinctFrequencies = map.DistinctAntennaeFrequencies;

// set to track unique antinode positions identified
HashSet<(int, int)> antinodes = [];

foreach(var frequency in distinctFrequencies) {
    var antennae = map.Antennae[frequency].ToList();
    // pair all combinations of antennae with the same frequencies... check for antinodes
    for(int i = 0; i < antennae.Count - 1; i++) {
        for(int j = i + 1; j < antennae.Count; j++) {
            var antenna1 = antennae.ElementAt(i);
            var antenna2 = antennae.ElementAt(j);

            var possibleAntinode1 = new {
                X = 2 * antenna1.X - antenna2.X,
                Y = 2 * antenna1.Y - antenna2.Y
            };

            var possibleAntinode2 = new {
                X = 2 * antenna2.X - antenna1.X,
                Y = 2 * antenna2.Y - antenna1.Y                
            };
     
            if (map.InMapBounds(possibleAntinode1.X, possibleAntinode1.Y)) {
                antinodes.Add((possibleAntinode1.X, possibleAntinode1.Y));
            }
            if (map.InMapBounds(possibleAntinode2.X, possibleAntinode2.Y)) {
                antinodes.Add((possibleAntinode2.X, possibleAntinode2.Y));
            }       
        }
    }  
}

Console.WriteLine(map.GetMapStringWithAntinodes(antinodes));
Console.WriteLine();
Console.WriteLine($"Answer 1: {antinodes.Count}");

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
        y++;
        for (int x = 0; x< line.Length; x++) {
            char c = line[x];
            if (c != '.') {
                antennae.Add(new Antenna { Frequency = c, X = x, Y = y });
            }
        }
        line = reader.ReadLine();
    }

    map.Antennae = antennae.ToLookup(a => a.Frequency);
    map.MapHeight = y;

    reader.Close();

    return map;
}
