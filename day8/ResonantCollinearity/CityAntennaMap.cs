public class CityAntennaMap {
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public ILookup<char, Antenna>? Antennae { get; set; }

    public List<char>? DistinctAntennaeFrequencies => Antennae?.Select(x => x.Key).ToList();

    public bool InMapBounds(int x, int y) {
        return x < MapWidth && x >= 0 && y < MapHeight && y >= 0;
    }

    public string GetMapStringWithAntinodes(IEnumerable<(int, int)> antinodes) {
        var result = "";
        for (var y = 0; y < MapHeight; y++) {
            for (var x = 0; x < MapWidth; x++) {
                char charHere = '.';
                if (antinodes.Any(a => a.Item1 == x && a.Item2 == y)) {
                    charHere = '#';
                } else {
                    var antenna = Antennae.SelectMany(x => x).FirstOrDefault(a => a.X == x && a.Y == y);
                    if (antenna != null) {
                        charHere = antenna.Frequency;
                    }
                }

                result += charHere;
            }
            result += "\n";
        }
        return result;
    }
}