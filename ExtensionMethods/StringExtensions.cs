namespace ExtensionMethods;

internal static class StringExtensions
{
    private readonly static Dictionary<string, string> _traductions = new()
    {
        { "Bonjour", "Hello" },
        { "Monde", "World" },

        { "Je", "I" },
        { "S'appelle", "Am" },
        { "Groot", "Groot" },
    };

    public static string ToEnglish(this string frWord)
    {
        return _traductions.ContainsKey(frWord) ?
            _traductions[frWord] :
            "I don't speak French sorry";
    }

    public static string ToFrench(this string enWord)
    {
        return _traductions.ContainsValue(enWord) ?
            _traductions.First(key => key.Value.Equals(enWord)).Key :
            "Je ne parle pas Anglais désolé";
    }
}
