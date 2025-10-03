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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Work Items";
            
            this.dataGridViewWorkItems = new System.Windows.Forms.DataGridView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonUpdateStatus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewWorkItems
            // 
            this.dataGridViewWorkItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewWorkItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWorkItems.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewWorkItems.Name = "dataGridViewWorkItems";
            this.dataGridViewWorkItems.Size = new System.Drawing.Size(760, 300);
            this.dataGridViewWorkItems.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 320);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(100, 30);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(120, 320);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(120, 30);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Create Work Item";
            this.buttonCreate.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateStatus
            // 
            this.buttonUpdateStatus.Location = new System.Drawing.Point(250, 320);
            this.buttonUpdateStatus.Name = "buttonUpdateStatus";
            this.buttonUpdateStatus.Size = new System.Drawing.Size(120, 30);
            this.buttonUpdateStatus.TabIndex = 3;
            this.buttonUpdateStatus.Text = "Update Status";
            this.buttonUpdateStatus.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.dataGridViewWorkItems);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonUpdateStatus);
            this.Name = "MainForm";
            this.Text = "Work Items";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkItems)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewWorkItems;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonUpdateStatus;
    }
}
