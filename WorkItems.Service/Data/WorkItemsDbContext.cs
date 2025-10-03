using System.Data.Entity;

namespace WorkItems.Service.Data
{
    public class WorkItemsDbContext : DbContext
    {
        public WorkItemsDbContext() : base("name=WorkItemsDb")
        {
        }

        public DbSet<WorkItem> WorkItems { get; set; }
    }
}