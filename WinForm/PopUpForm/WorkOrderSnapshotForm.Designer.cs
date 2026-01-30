namespace WinForm.PopUpForm
{
    partial class WorkOrderSnapshotForm
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
            dgvLinks = new DataGridView();
            dgvLogisticUnits = new DataGridView();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dgvSerials = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblWorkOrder = new Label();
            lblProduct = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLinks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLogisticUnits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSerials).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.802548F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 92.19745F));
            tableLayoutPanel1.Size = new Size(1362, 628);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(dgvLinks, 2, 1);
            tableLayoutPanel2.Controls.Add(dgvLogisticUnits, 1, 1);
            tableLayoutPanel2.Controls.Add(label3, 2, 0);
            tableLayoutPanel2.Controls.Add(label2, 1, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(dgvSerials, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 52);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.726004F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 91.2739944F));
            tableLayoutPanel2.Size = new Size(1356, 573);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // dgvLinks
            // 
            dgvLinks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLinks.Dock = DockStyle.Fill;
            dgvLinks.Location = new Point(907, 53);
            dgvLinks.Name = "dgvLinks";
            dgvLinks.RowTemplate.Height = 25;
            dgvLinks.Size = new Size(446, 517);
            dgvLinks.TabIndex = 5;
            // 
            // dgvLogisticUnits
            // 
            dgvLogisticUnits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogisticUnits.Dock = DockStyle.Fill;
            dgvLogisticUnits.Location = new Point(455, 53);
            dgvLogisticUnits.Name = "dgvLogisticUnits";
            dgvLogisticUnits.RowTemplate.Height = 25;
            dgvLogisticUnits.Size = new Size(446, 517);
            dgvLogisticUnits.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(907, 0);
            label3.Name = "label3";
            label3.Size = new Size(266, 45);
            label3.TabIndex = 2;
            label3.Text = "Aggrations-(Bağlantı Linkleri) (Serial-Paket-Palet)\r\nChildType = 1 (Paket)\r\nChildType = 2 (Seri)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(455, 0);
            label2.Name = "label2";
            label2.Size = new Size(174, 45);
            label2.TabIndex = 1;
            label2.Text = "LogisticUnits-(Paket ve Paletler)\r\nType = 1 (Paket)\r\nType = 2 (Palet)\r\n";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(259, 15);
            label1.TabIndex = 0;
            label1.Text = "Serials-(Ürünler) (Agrasyonun en altındaki ürün)\r\n";
            // 
            // dgvSerials
            // 
            dgvSerials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSerials.Dock = DockStyle.Fill;
            dgvSerials.Location = new Point(3, 53);
            dgvSerials.Name = "dgvSerials";
            dgvSerials.RowTemplate.Height = 25;
            dgvSerials.Size = new Size(446, 517);
            dgvSerials.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.899704F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.100296F));
            tableLayoutPanel3.Controls.Add(lblWorkOrder, 0, 0);
            tableLayoutPanel3.Controls.Add(lblProduct, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1356, 43);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // lblWorkOrder
            // 
            lblWorkOrder.Anchor = AnchorStyles.Left;
            lblWorkOrder.AutoSize = true;
            lblWorkOrder.Location = new Point(3, 14);
            lblWorkOrder.Name = "lblWorkOrder";
            lblWorkOrder.Size = new Size(81, 15);
            lblWorkOrder.TabIndex = 0;
            lblWorkOrder.Text = "WorkOrder Id:";
            // 
            // lblProduct
            // 
            lblProduct.Anchor = AnchorStyles.Left;
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(761, 14);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(52, 15);
            lblProduct.TabIndex = 1;
            lblProduct.Text = "Product:";
            // 
            // WorkOrderSnapshotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1362, 628);
            Controls.Add(tableLayoutPanel1);
            Name = "WorkOrderSnapshotForm";
            Text = "WorkOrderSnapshotForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLinks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLogisticUnits).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSerials).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView dgvLinks;
        private DataGridView dgvLogisticUnits;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dgvSerials;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblWorkOrder;
        private Label lblProduct;
    }
}