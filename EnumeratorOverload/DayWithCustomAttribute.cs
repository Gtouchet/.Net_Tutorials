namespace EnumeratorCustomAttributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal class Day : Attribute
{
    public string FrenchTraduction;
    public int PositionInWeek;
    public int PainLevel;
    public string[] AdditionalInformations = new string[] { };
    
    public Day(string frenchTraduction, int positionInWeek, int painLevel, string[] additionalInformations)
    {
        this.FrenchTraduction = frenchTraduction;
        this.PositionInWeek = positionInWeek;
        this.PainLevel = painLevel;
        this.AdditionalInformations = additionalInformations;
    }
    
    public Day(string frenchTraduction, int positionInWeek, int painLevel)
    {
        this.FrenchTraduction = frenchTraduction;
        this.PositionInWeek = positionInWeek;
        this.PainLevel = painLevel;
    }
}

internal enum DayWithCustomAttribute
{
    [Day(
        frenchTraduction: "Lundi",
        positionInWeek: 1,
        painLevel: 10,
        additionalInformations: new string[] { "remote working", "tired of the weekend" })]
    Monday,
    [Day("Mardi", 2, 7, new string[] { "stuck in public transports" })]
    Tuesday,
    [Day("Mercredi", 3, 6)]
    Wednesday,
    [Day("Jeudi", 4, 4)]
    Thursday,
    [Day("Vendredi", 5, 3)]
    Friday,
    [Day("Samedi", 6, 2)]
    Saturday,
    // This one does not have any attribute for testing purpose
    Sunday,
}
