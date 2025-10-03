namespace WorkItems.Client
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewWorkItems = new DataGridView();
            buttonRefresh = new Button();
            buttonCreate = new Button();
            buttonUpdateStatus = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewWorkItems).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewWorkItems
            // 
            dataGridViewWorkItems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewWorkItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewWorkItems.Location = new Point(12, 12);
            dataGridViewWorkItems.Name = "dataGridViewWorkItems";
            dataGridViewWorkItems.Size = new Size(760, 300);
            dataGridViewWorkItems.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(12, 320);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(100, 30);
            buttonRefresh.TabIndex = 1;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonCreate
            // 
            buttonCreate.Location = new Point(120, 320);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(120, 30);
            buttonCreate.TabIndex = 2;
            buttonCreate.Text = "Create Work Item";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // buttonUpdateStatus
            // 
            buttonUpdateStatus.Location = new Point(250, 320);
            buttonUpdateStatus.Name = "buttonUpdateStatus";
            buttonUpdateStatus.Size = new Size(120, 30);
            buttonUpdateStatus.TabIndex = 3;
            buttonUpdateStatus.Text = "Update Status";
            buttonUpdateStatus.UseVisualStyleBackColor = true;
            buttonUpdateStatus.Click += buttonUpdateStatus_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 361);
            Controls.Add(dataGridViewWorkItems);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonCreate);
            Controls.Add(buttonUpdateStatus);
            Name = "MainForm";
            Text = "Work Items";
            ((System.ComponentModel.ISupportInitialize)dataGridViewWorkItems).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewWorkItems;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonUpdateStatus;
    }
}
