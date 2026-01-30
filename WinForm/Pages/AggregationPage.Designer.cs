namespace WinForm.Pages
{
    partial class AggregationPage
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
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            label3 = new Label();
            cmbWorkorders = new ComboBox();
            cmbAggregation = new ComboBox();
            label1 = new Label();
            btnAggregate = new Button();
            label2 = new Label();
            label4 = new Label();
            txChild = new TextBox();
            txParent = new TextBox();
            tvAgg = new TreeView();
            tableLayoutPanel4 = new TableLayoutPanel();
            cmbWorkTree = new ComboBox();
            lblSummary = new Label();
            label5 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.61151F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.38849F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Controls.Add(tvAgg, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.5678234F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 89.4321747F));
            tableLayoutPanel1.Size = new Size(1390, 759);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(dataGridView2, 1, 0);
            tableLayoutPanel2.Controls.Add(dataGridView1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 83);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(906, 673);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(456, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(447, 667);
            dataGridView2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(447, 667);
            dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 6;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.8571434F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.1428566F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 176F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.Controls.Add(label3, 0, 1);
            tableLayoutPanel3.Controls.Add(cmbWorkorders, 1, 0);
            tableLayoutPanel3.Controls.Add(cmbAggregation, 1, 1);
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(btnAggregate, 5, 1);
            tableLayoutPanel3.Controls.Add(label2, 3, 0);
            tableLayoutPanel3.Controls.Add(label4, 4, 0);
            tableLayoutPanel3.Controls.Add(txChild, 3, 1);
            tableLayoutPanel3.Controls.Add(txParent, 4, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(906, 74);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 37);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 5;
            label3.Text = "Aggregation Tipi:";
            // 
            // cmbWorkorders
            // 
            cmbWorkorders.FormattingEnabled = true;
            cmbWorkorders.Location = new Point(168, 3);
            cmbWorkorders.Name = "cmbWorkorders";
            cmbWorkorders.Size = new Size(121, 23);
            cmbWorkorders.TabIndex = 1;
            // 
            // cmbAggregation
            // 
            cmbAggregation.FormattingEnabled = true;
            cmbAggregation.Items.AddRange(new object[] { "Serial-Package", "Package-Pallet" });
            cmbAggregation.Location = new Point(168, 40);
            cmbAggregation.Name = "cmbAggregation";
            cmbAggregation.Size = new Size(121, 23);
            cmbAggregation.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 3;
            label1.Text = "WorkOrder:";
            // 
            // btnAggregate
            // 
            btnAggregate.Location = new Point(828, 40);
            btnAggregate.Name = "btnAggregate";
            btnAggregate.Size = new Size(75, 23);
            btnAggregate.TabIndex = 6;
            btnAggregate.Text = "Kaydet";
            btnAggregate.UseVisualStyleBackColor = true;
            btnAggregate.Click += btnAggregate_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(516, 0);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 7;
            label2.Text = "Child";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(652, 0);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 8;
            label4.Text = "Parent";
            // 
            // txChild
            // 
            txChild.Location = new Point(516, 40);
            txChild.Name = "txChild";
            txChild.ReadOnly = true;
            txChild.Size = new Size(100, 23);
            txChild.TabIndex = 9;
            // 
            // txParent
            // 
            txParent.Location = new Point(652, 40);
            txParent.Name = "txParent";
            txParent.ReadOnly = true;
            txParent.Size = new Size(100, 23);
            txParent.TabIndex = 10;
            // 
            // tvAgg
            // 
            tvAgg.Dock = DockStyle.Fill;
            tvAgg.FullRowSelect = true;
            tvAgg.HideSelection = false;
            tvAgg.Location = new Point(915, 83);
            tvAgg.Name = "tvAgg";
            tvAgg.Size = new Size(472, 673);
            tvAgg.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.9491539F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.0508461F));
            tableLayoutPanel4.Controls.Add(label5, 0, 0);
            tableLayoutPanel4.Controls.Add(cmbWorkTree, 1, 0);
            tableLayoutPanel4.Controls.Add(lblSummary, 1, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(915, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(472, 74);
            tableLayoutPanel4.TabIndex = 11;
            // 
            // cmbWorkTree
            // 
            cmbWorkTree.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbWorkTree.FormattingEnabled = true;
            cmbWorkTree.Location = new Point(201, 7);
            cmbWorkTree.Name = "cmbWorkTree";
            cmbWorkTree.Size = new Size(268, 23);
            cmbWorkTree.TabIndex = 2;
            cmbWorkTree.SelectedIndexChanged += cmbWorkTree_SelectedIndexChanged;
            // 
            // lblSummary
            // 
            lblSummary.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSummary.AutoSize = true;
            lblSummary.Location = new Point(201, 59);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(0, 15);
            lblSummary.TabIndex = 8;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(102, 11);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 9;
            label5.Text = "WorkOrder Tree:";
            // 
            // AggregationPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1390, 759);
            Controls.Add(tableLayoutPanel1);
            Name = "AggregationPage";
            Text = "AggregationPage";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel3;
        private ComboBox cmbAggregation;
        private Label label3;
        private ComboBox cmbWorkorders;
        private Label label1;
        private Button btnAggregate;
        private Label label2;
        private Label label4;
        private TextBox txChild;
        private TextBox txParent;
        private ComboBox cmbWorkTree;
        private TreeView tvAgg;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblSummary;
        private Label label5;
    }
}