namespace ExtensionMethods;

internal enum Color
{
    Rouge =   255_000_000,
    Vert =    000_255_000,
    Bleu =    000_000_255,
    Noir =    000_000_000,
    Blanc =   255_255_255,
    Jaune =   255_255_000,
    Magenta = 255_000_255,
    Cyan =    000_255_255,
}

internal class Rgb
{
    public int Red { get; init; }
    public int Green { get; init; }
    public int Blue { get; init; }

    public override string ToString()
    {
        return $"R:{Red} G:{Green} B:{Blue}";
    }
}

internal static class ColorEnumExtensions
{
    public static Rgb GetRgb(this Color color)
    {
        int rgb = (int)color;
        return new()
        {
            Red = rgb / 1_000_000,
            Green = rgb % 1_000_000 / 1_000,
            Blue = rgb % 1_000,
        };
    }
}
