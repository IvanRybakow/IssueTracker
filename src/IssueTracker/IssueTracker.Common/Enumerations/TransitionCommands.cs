using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Common.Enumerations
{
    public enum TransitionCommands
    {
        None,
        Enter,
        Open,
        [Display(Name = "Mark as Resolved")]
        Solve,
        [Display(Name = "Reopen Issue")]
        Reopen,
        Close
    }
}
