public class Antenna {
    public char Frequency { get; set;}
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString() {
        return $"{Frequency}: ({X}, {Y})";
    }
}