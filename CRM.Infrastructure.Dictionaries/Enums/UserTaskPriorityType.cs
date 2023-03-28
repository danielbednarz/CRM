using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Dictionaries
{
    public enum UserTaskPriorityType
    {
        [Display(Name = "Niski")]
        Low = 1,
        [Display(Name = "Średni")]
        Medium = 2,
        [Display(Name = "Wysoki")]
        High = 3,
        [Display(Name = "Krytyczny")]
        Critical = 4
    }
}
