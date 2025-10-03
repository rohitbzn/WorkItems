using Microsoft.Extensions.Configuration;
using Serilog;

namespace WorkItems.Client
{
    public partial class MainForm : Form
    {
        private Api.ApiClient _apiClient;

        public MainForm()
        {
            InitializeComponent();

            // Load config from Program.Configuration
            var config = Program.Configuration;
            var baseUrl = config["ApiBaseUrl"];
            var apiKey = config["ApiKey"];
            _apiClient = new Api.ApiClient(baseUrl, apiKey);

            Log.Information("MainForm initialized with API base URL: {BaseUrl}", baseUrl);
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            Log.Information("User clicked Refresh.");
            try
            {
                var items = await _apiClient.GetWorkItemsAsync();
                dataGridViewWorkItems.DataSource = items;
                Log.Information("Work items refreshed. Count: {Count}", items.Length);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to refresh work items.");
                MessageBox.Show("Failed to refresh work items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            Log.Information("User clicked Create Work Item.");
            try
            {
                // Show dialog to get new item details, then:
                var newItem = new WorkItems.Shared.WorkItemDto
                {
                    Title = "New Item",
                    Status = WorkItems.Shared.WorkItemStatus.New,
                    Priority = WorkItems.Shared.WorkItemPriority.Medium
                };
                await _apiClient.CreateWorkItemAsync(newItem);
                Log.Information("Created new work item: {Title}", newItem.Title);
                await RefreshWorkItemsAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to create work item.");
                MessageBox.Show("Failed to create work item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdateStatus_Click(object sender, EventArgs e)
        {
            Log.Information("User clicked Update Status.");
            if (dataGridViewWorkItems.CurrentRow?.DataBoundItem is WorkItems.Shared.WorkItemDto selected)
            {
                try
                {
                    await _apiClient.UpdateWorkItemStatusAsync(selected.Id, WorkItems.Shared.WorkItemStatus.Done);
                    Log.Information("Updated status for work item {Id} to Done.", selected.Id);
                    await RefreshWorkItemsAsync();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to update work item status for {Id}.", selected.Id);
                    MessageBox.Show("Failed to update work item status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Log.Warning("Update Status clicked but no work item was selected.");
            }
        }

        private async Task RefreshWorkItemsAsync()
        {
            try
            {
                var items = await _apiClient.GetWorkItemsAsync();
                dataGridViewWorkItems.DataSource = items;
                Log.Information("Work items refreshed (from RefreshWorkItemsAsync). Count: {Count}", items.Length);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to refresh work items (from RefreshWorkItemsAsync).");
            }
        }
    }
}
