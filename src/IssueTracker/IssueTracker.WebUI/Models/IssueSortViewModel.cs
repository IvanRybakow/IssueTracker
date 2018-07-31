using System;
using IssueTracker.WebUI.TagHelpers;

namespace IssueTracker.WebUI.Models
{
    public class IssueSortViewModel
    {
        public IssueSortState DateSort { get; set; } = IssueSortState.CreationDateAsc;
        public IssueSortState NameSort { get; set; } = IssueSortState.NameAsc;
        public IssueSortState StatusSort { get; set; } = IssueSortState.StatusAsc;
        public IssueSortState PrioritySort { get; set; } = IssueSortState.PriorityAsc;
        public IssueSortState UrgencySort { get; set; } = IssueSortState.UrgencyAsc;
        public IssueSortState CreatedBySort { get; set; } = IssueSortState.CreatedByAsc;
        public IssueSortState Current { get; set; }
        public bool Up { get; set; }

        public IssueSortViewModel(IssueSortState sortOrder)
        {
            Up = sortOrder.ToString().IndexOf("Desc", StringComparison.InvariantCultureIgnoreCase) >= 0;

            switch (sortOrder)
            {
                case IssueSortState.CreationDateDesc:
                    Current = DateSort = IssueSortState.CreationDateAsc;
                    break;
                case IssueSortState.NameDesc:
                    Current = NameSort = IssueSortState.NameAsc;
                    break;
                case IssueSortState.StatusDesc:
                    Current = StatusSort = IssueSortState.StatusAsc;
                    break;
                case IssueSortState.PriorityDesc:
                    Current = PrioritySort = IssueSortState.PriorityAsc;
                    break;
                case IssueSortState.UrgencyDesc:
                    Current = UrgencySort = IssueSortState.UrgencyAsc;
                    break;
                case IssueSortState.CreatedByDesc:
                    Current = CreatedBySort = IssueSortState.CreatedByAsc;
                    break;
                case IssueSortState.CreationDateAsc:
                    Current = DateSort = IssueSortState.CreationDateDesc;
                    break;
                case IssueSortState.NameAsc:
                    Current = NameSort = IssueSortState.NameDesc;
                    break;
                case IssueSortState.StatusAsc:
                    Current = StatusSort = IssueSortState.StatusDesc;
                    break;
                case IssueSortState.PriorityAsc:
                    Current = PrioritySort = IssueSortState.PriorityDesc;
                    break;
                case IssueSortState.UrgencyAsc:
                    Current = UrgencySort = IssueSortState.UrgencyDesc;
                    break;
                case IssueSortState.CreatedByAsc:
                    Current = CreatedBySort = IssueSortState.CreatedByDesc;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }
        }
    }
}
