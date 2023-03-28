using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Dictionaries
{
    public enum UserTaskStepType
    {
        [Display(Name = "Zarejestrowano")]
        Start = 0,
        [Display(Name = "W realizacji")]
        Middle = 1,
        [Display(Name = "Potwierdzenie")]
        Confirmation = 2,
        [Display(Name = "Zakończone")]
        End = 3,
        [Display(Name = "Anulowane")]
        Cancel = 4,
    }
}
