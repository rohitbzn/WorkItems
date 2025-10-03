using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkItems.Shared;

namespace WorkItems.Service.Data
{
    [Table("WorkItems")]
    public class WorkItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public WorkItemStatus Status { get; set; }

        [Required]
        public WorkItemPriority Priority { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
