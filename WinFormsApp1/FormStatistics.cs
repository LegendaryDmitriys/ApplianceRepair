namespace WinFormsApp1;

public partial class FormStatistics : Form
{
        public FormStatistics(int completedRequestsCount, TimeSpan averageCompletionTime, Dictionary<string, int> faultTypeStatistics)
        {
            InitializeComponent();
            InitializeComponents();

            labelCompletedRequests.Text = $"Количество выполненных заявок: {completedRequestsCount}";
            
            labelAverageTime.Text = $"Среднее время выполнения заявки: {averageCompletionTime.Days} дней";
            
            listViewFaultTypes.Items.Clear();
            foreach (var kvp in faultTypeStatistics)
            {
                ListViewItem item = new ListViewItem(new[] { kvp.Key, kvp.Value.ToString() });
                listViewFaultTypes.Items.Add(item);
            }
        }

        private void InitializeComponents()
        {
            this.labelCompletedRequests = new System.Windows.Forms.Label();
            this.labelAverageTime = new System.Windows.Forms.Label();
            this.listViewFaultTypes = new System.Windows.Forms.ListView();
            this.columnHeaderProblem = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCount = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            
            this.labelCompletedRequests.AutoSize = true;
            this.labelCompletedRequests.Location = new System.Drawing.Point(12, 9);
            this.labelCompletedRequests.Name = "labelCompletedRequests";
            this.labelCompletedRequests.Size = new System.Drawing.Size(150, 17);
            this.labelCompletedRequests.TabIndex = 0;
            this.labelCompletedRequests.Text = "Количество выполненных заявок:";
            
            this.labelAverageTime.AutoSize = true;
            this.labelAverageTime.Location = new System.Drawing.Point(12, 36);
            this.labelAverageTime.Name = "labelAverageTime";
            this.labelAverageTime.Size = new System.Drawing.Size(150, 17);
            this.labelAverageTime.TabIndex = 1;
            this.labelAverageTime.Text = "Среднее время выполнения заявки:";
            
            this.listViewFaultTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderProblem,
            this.columnHeaderCount});
            this.listViewFaultTypes.GridLines = true;
            this.listViewFaultTypes.Location = new System.Drawing.Point(12, 60);
            this.listViewFaultTypes.Name = "listViewFaultTypes";
            this.listViewFaultTypes.Size = new System.Drawing.Size(300, 200);
            this.listViewFaultTypes.TabIndex = 2;
            this.listViewFaultTypes.UseCompatibleStateImageBehavior = false;
            this.listViewFaultTypes.View = System.Windows.Forms.View.Details;
            
            this.columnHeaderProblem.Text = "Тип неисправности";
            this.columnHeaderProblem.Width = 150;


            this.columnHeaderCount.Text = "Количество";
            this.columnHeaderCount.Width = 80;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 272);
            this.Controls.Add(this.listViewFaultTypes);
            this.Controls.Add(this.labelAverageTime);
            this.Controls.Add(this.labelCompletedRequests);
            this.Name = "FormStatistics";
            this.Text = "Статистика работы отдела обслуживания";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelCompletedRequests;
        private System.Windows.Forms.Label labelAverageTime;
        private System.Windows.Forms.ListView listViewFaultTypes;
        private System.Windows.Forms.ColumnHeader columnHeaderProblem;
        private System.Windows.Forms.ColumnHeader columnHeaderCount;
        
}

