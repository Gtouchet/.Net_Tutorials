using EnumeratorCustomAttributes;


/*
* With description and display (limited by the number of available attributes)
*/
DayWithDescriptionAndDisplay dayDescriptionDisplay = DayWithDescriptionAndDisplay.Monday;
Console.WriteLine($"{dayDescriptionDisplay} ({dayDescriptionDisplay.Display()}) : {dayDescriptionDisplay.Description()}\n");


/* 
* With custom attribute (not limited because we can create as many properties in the attribute class as we want)
*/
Day dayAttribute = DayWithCustomAttribute.Monday.GetValuesOf<Day>();
Console.Write($"{DayWithCustomAttribute.Monday} ({dayAttribute.FrenchTraduction}) : {dayAttribute.PositionInWeek}/7 with a pain of {dayAttribute.PainLevel}");
dayAttribute.AdditionalInformations.ToList().ForEach(info => Console.Write($", {info}"));


/*
 * Calling the method on an enum value without an attribute will throw a custom exception "NoAttributeDataException"
 */
try
{
    Day dayWithoutAnAttribute = DayWithCustomAttribute.Sunday.GetValuesOf<Day>();
}
catch (NoAttributeException e)
{
    Console.WriteLine("\n\n" + e.Message);
}


/*
 * Calling the method with the wrong type of attribute for the enum will throw a WrongAttributeException
 */
try
{
    FakeAttribute iWantFakeData = DayWithCustomAttribute.Monday.GetValuesOf<FakeAttribute>();
}
catch (WrongAttributeException e)
{
    Console.WriteLine("\n" + e.Message);
}

class FakeAttribute : Attribute { }
