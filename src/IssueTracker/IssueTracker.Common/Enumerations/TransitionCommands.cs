using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Common.Enumerations
{
    public enum TransitionCommands
    {
        [Display(Name = "Save Changes")]
        Save,
        Enter,
        Open,
        [Display(Name = "Mark as Resolved")]
        Solve,
        [Display(Name = "Reopen Issue")]
        Reopen,
        Close
    }
}
