using System.Collections.Generic;
using IssueTracker.Common.Models;

namespace IssueTracker.WebUI.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<IssueTransitionEntity> RecentActivities { get; set; }
        public IEnumerable<IssueEntity> LastIssues { get; set; }
    }
}
