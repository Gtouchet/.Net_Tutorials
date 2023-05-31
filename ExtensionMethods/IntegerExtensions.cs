namespace ExtensionMethods;

internal static class IntegerExtensions
{
    public static int MultiplyBy(this int value, int by)
    {
        return value * by;
    }

    public static string ToWord(this int value)
    {
        return value switch
        {
            1 => "Un",
            2 => "Deux",
            3 => "Trois",
            99 => "Quatre-vingt-dix-neuf",
            _ => "Non implémenté",
        };
    }
}
