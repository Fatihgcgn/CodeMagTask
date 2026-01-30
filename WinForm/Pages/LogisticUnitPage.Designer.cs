namespace WinForm.Pages
{
    partial class LogisticUnitPage
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
            label4 = new Label();
            label3 = new Label();
            dgvPallets = new DataGridView();
            dgvPackages = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            cmbLuType = new ComboBox();
            cmbWorkOrders = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            btnCreate = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPallets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPackages).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel1.Size = new Size(1175, 767);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label4, 1, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(dgvPallets, 1, 1);
            tableLayoutPanel2.Controls.Add(dgvPackages, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 118);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 3.86996913F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 96.13003F));
            tableLayoutPanel2.Size = new Size(1169, 646);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(587, 5);
            label4.Name = "label4";
            label4.Size = new Size(579, 15);
            label4.TabIndex = 3;
            label4.Text = "Pallet (Tipi 2)";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 5);
            label3.Name = "label3";
            label3.Size = new Size(578, 15);
            label3.TabIndex = 2;
            label3.Text = "Package (Tipi 1)";
            // 
            // dgvPallets
            // 
            dgvPallets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPallets.Dock = DockStyle.Fill;
            dgvPallets.Location = new Point(587, 28);
            dgvPallets.Name = "dgvPallets";
            dgvPallets.RowTemplate.Height = 25;
            dgvPallets.Size = new Size(579, 615);
            dgvPallets.TabIndex = 1;
            // 
            // dgvPackages
            // 
            dgvPackages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPackages.Dock = DockStyle.Fill;
            dgvPackages.Location = new Point(3, 28);
            dgvPackages.Name = "dgvPackages";
            dgvPackages.RowTemplate.Height = 25;
            dgvPackages.Size = new Size(578, 615);
            dgvPackages.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.474215F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.4701481F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.0395927F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.01605F));
            tableLayoutPanel3.Controls.Add(cmbLuType, 1, 1);
            tableLayoutPanel3.Controls.Add(cmbWorkOrders, 1, 0);
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(label2, 0, 1);
            tableLayoutPanel3.Controls.Add(btnCreate, 2, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(1169, 109);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // cmbLuType
            // 
            cmbLuType.Anchor = AnchorStyles.Left;
            cmbLuType.FormattingEnabled = true;
            cmbLuType.Items.AddRange(new object[] { "1", "2" });
            cmbLuType.Location = new Point(102, 70);
            cmbLuType.Name = "cmbLuType";
            cmbLuType.Size = new Size(114, 23);
            cmbLuType.TabIndex = 2;
            // 
            // cmbWorkOrders
            // 
            cmbWorkOrders.Anchor = AnchorStyles.Left;
            cmbWorkOrders.FormattingEnabled = true;
            cmbWorkOrders.Location = new Point(102, 15);
            cmbWorkOrders.Name = "cmbWorkOrders";
            cmbWorkOrders.Size = new Size(114, 23);
            cmbWorkOrders.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(51, 19);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 0;
            label1.Text = "İş Emri:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(26, 74);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 3;
            label2.Text = "Lojistik Tipi:";
            // 
            // btnCreate
            // 
            btnCreate.Anchor = AnchorStyles.Left;
            btnCreate.Location = new Point(236, 70);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(134, 23);
            btnCreate.TabIndex = 4;
            btnCreate.Text = "Yeni Kaydet";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // LogisticUnitPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1175, 767);
            Controls.Add(tableLayoutPanel1);
            Name = "LogisticUnitPage";
            Text = "LogisticUnitPage";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPallets).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPackages).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label2;
        private Label label1;
        private ComboBox cmbWorkOrders;
        private ComboBox cmbLuType;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private Label label3;
        private DataGridView dgvPallets;
        private DataGridView dgvPackages;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnCreate;
    }
}