namespace WinForm.Pages
{
    partial class SimulationPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            btnRefresh = new Button();
            cmbWorkOrders = new ComboBox();
            nudAutoGenerateCount = new NumericUpDown();
            lblStatus = new Label();
            chkAutoGenerate = new CheckBox();
            lblInfo = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            btnSimStop = new Button();
            btnSimStart = new Button();
            dgvSerials = new DataGridView();
            txtLog = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAutoGenerateCount).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSerials).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Controls.Add(dgvSerials, 0, 2);
            tableLayoutPanel1.Controls.Add(txtLog, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4.437531F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 32.05805F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.92348F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5329819F));
            tableLayoutPanel1.Size = new Size(975, 629);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(969, 21);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(369, 15);
            label1.TabIndex = 0;
            label1.Text = "Simülasyon etmek için WorkOrder değişin yada Start butonuna basın";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.34252F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 30);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 40.3508759F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 59.6491241F));
            tableLayoutPanel3.Size = new Size(969, 195);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.7822F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.2178F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 618F));
            tableLayoutPanel4.Controls.Add(btnRefresh, 0, 1);
            tableLayoutPanel4.Controls.Add(cmbWorkOrders, 0, 0);
            tableLayoutPanel4.Controls.Add(nudAutoGenerateCount, 1, 1);
            tableLayoutPanel4.Controls.Add(lblStatus, 2, 1);
            tableLayoutPanel4.Controls.Add(chkAutoGenerate, 1, 0);
            tableLayoutPanel4.Controls.Add(lblInfo, 2, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(963, 72);
            tableLayoutPanel4.TabIndex = 6;
            tableLayoutPanel4.Paint += tableLayoutPanel4_Paint;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Left;
            btnRefresh.Location = new Point(3, 42);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(196, 23);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // cmbWorkOrders
            // 
            cmbWorkOrders.Anchor = AnchorStyles.Left;
            cmbWorkOrders.FormattingEnabled = true;
            cmbWorkOrders.Location = new Point(3, 6);
            cmbWorkOrders.Name = "cmbWorkOrders";
            cmbWorkOrders.Size = new Size(196, 23);
            cmbWorkOrders.TabIndex = 5;
            // 
            // nudAutoGenerateCount
            // 
            nudAutoGenerateCount.Anchor = AnchorStyles.Left;
            nudAutoGenerateCount.Location = new Point(205, 42);
            nudAutoGenerateCount.Name = "nudAutoGenerateCount";
            nudAutoGenerateCount.Size = new Size(120, 23);
            nudAutoGenerateCount.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(347, 46);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 0;
            // 
            // chkAutoGenerate
            // 
            chkAutoGenerate.Anchor = AnchorStyles.Left;
            chkAutoGenerate.AutoSize = true;
            chkAutoGenerate.Location = new Point(205, 8);
            chkAutoGenerate.Name = "chkAutoGenerate";
            chkAutoGenerate.Size = new Size(102, 19);
            chkAutoGenerate.TabIndex = 6;
            chkAutoGenerate.Text = "Auto Generate";
            chkAutoGenerate.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Left;
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(347, 10);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(0, 15);
            lblInfo.TabIndex = 8;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.8522263F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.14777F));
            tableLayoutPanel5.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 81);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(963, 111);
            tableLayoutPanel5.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnSimStop);
            groupBox1.Controls.Add(btnSimStart);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(339, 99);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Simulation";
            // 
            // btnSimStop
            // 
            btnSimStop.Anchor = AnchorStyles.Left;
            btnSimStop.Location = new Point(168, 22);
            btnSimStop.Name = "btnSimStop";
            btnSimStop.Size = new Size(159, 63);
            btnSimStop.TabIndex = 2;
            btnSimStop.Text = "Stop";
            btnSimStop.UseVisualStyleBackColor = true;
            // 
            // btnSimStart
            // 
            btnSimStart.Anchor = AnchorStyles.Left;
            btnSimStart.Location = new Point(3, 22);
            btnSimStart.Name = "btnSimStart";
            btnSimStart.Size = new Size(159, 63);
            btnSimStart.TabIndex = 1;
            btnSimStart.Text = "Start";
            btnSimStart.UseVisualStyleBackColor = true;
            // 
            // dgvSerials
            // 
            dgvSerials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSerials.Dock = DockStyle.Fill;
            dgvSerials.Location = new Point(3, 231);
            dgvSerials.Name = "dgvSerials";
            dgvSerials.RowTemplate.Height = 25;
            dgvSerials.Size = new Size(969, 314);
            dgvSerials.TabIndex = 2;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Location = new Point(3, 551);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(969, 75);
            txtLog.TabIndex = 3;
            // 
            // SimulationPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(975, 629);
            Controls.Add(tableLayoutPanel1);
            Name = "SimulationPage";
            Text = "SimulationPage";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAutoGenerateCount).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSerials).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox1;
        private Button btnRefresh;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblStatus;
        private ComboBox cmbWorkOrders;
        private DataGridView dgvSerials;
        private Button btnSimStop;
        private Button btnSimStart;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox txtLog;
        private CheckBox chkAutoGenerate;
        private NumericUpDown nudAutoGenerateCount;
        private Label lblInfo;
    }
}