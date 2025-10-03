using System;

namespace WorkItems.Shared
{
    public enum WorkItemStatus { New, InProgress, Done, Stale }
    public enum WorkItemPriority { Low, Medium, High }

    public class WorkItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public WorkItemStatus Status { get; set; }
        public WorkItemPriority Priority { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}