using ExtensionMethods;

Console.WriteLine($"{"Bonjour".ToEnglish()}, {"Monde".ToEnglish()} !");
Console.WriteLine($"{"I".ToFrench()} {"Am".ToFrench()} {"Groot".ToFrench()}.\n");

Console.WriteLine($"33 * 3 = {33.MultiplyBy(3)} = {33.MultiplyBy(3).ToWord()}\n");

Color color = Color.Cyan;
Console.WriteLine($"RGB pour {color} => {color.GetRgb()}");
