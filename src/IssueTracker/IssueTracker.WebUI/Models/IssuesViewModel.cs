using System.Collections.Generic;
using IssueTracker.Common.Models;

namespace IssueTracker.WebUI.Models
{
    public class IssuesViewModel
    {
        public IEnumerable<IssueEntity> Issues { get; set; }
        public IssueSortViewModel SortModel { get; set; }
        public string Caption { get; set; }
    }
}
