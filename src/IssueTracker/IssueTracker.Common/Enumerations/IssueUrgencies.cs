using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Common.Enumerations
{
    public enum IssueUrgencies
    {
        [Display(Name = "Very Urgent")]
        VeryUrgent,
        Urgent,
        [Display(Name = "Not Urgent")]
        NotUrgent,
        [Display(Name = "Least Urgent")]
        LeastUrgent
    }
}
