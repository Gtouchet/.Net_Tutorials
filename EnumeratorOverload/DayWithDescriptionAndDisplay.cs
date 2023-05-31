using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnumeratorCustomAttributes;

internal enum DayWithDescriptionAndDisplay
{
    [Description("1/7"), Display(Name = "Lundi")]
    Monday,
    [Description("2/7"), Display(Name = "Mardi")]
    Tuesday,
    [Description("3/7"), Display(Name = "Mercredi")]
    Wednesday,
    [Description("4/7"), Display(Name = "Jeudi")]
    Thursday,
    [Description("5/7"), Display(Name = "Vendredi")]
    Friday,
    [Description("6/7"), Display(Name = "Samedi")]
    Saturday,
    [Description("7/7"), Display(Name = "Dimanche")]
    Sunday,
}
