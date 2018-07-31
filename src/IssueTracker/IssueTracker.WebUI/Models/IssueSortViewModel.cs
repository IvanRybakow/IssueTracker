using System;
using IssueTracker.WebUI.TagHelpers;

namespace IssueTracker.WebUI.Models
{
    public class IssueSortViewModel
    {
        public string Filter { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }

        public IssueSortViewModel(SortState sortOrder, bool desc, string filter)
        {
            Up = desc;
            Current = sortOrder;
            Filter = filter;

        }
    }
}
