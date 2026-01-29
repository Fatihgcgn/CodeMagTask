namespace WinForm.Pages
{
    partial class CustomerPage
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
            dgvProducts = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            txtProductGtin = new TextBox();
            label4 = new Label();
            txtProductName = new TextBox();
            label5 = new Label();
            txProdAciklama = new TextBox();
            cmbCustomers = new ComboBox();
            btnProductSave = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblCustomerCreate = new Label();
            label1 = new Label();
            txtCustomerName = new TextBox();
            txtCustomerGln = new TextBox();
            labelDesc = new Label();
            txtCustomerDesc = new TextBox();
            btnCustomerSave = new Button();
            dgvCustomers = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(dgvProducts, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvCustomers, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 19.9391174F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80.06088F));
            tableLayoutPanel1.Size = new Size(1346, 657);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(676, 134);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowTemplate.Height = 25;
            dgvProducts.Size = new Size(667, 520);
            dgvProducts.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.5067511F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.98351F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.52324F));
            tableLayoutPanel3.Controls.Add(label2, 0, 0);
            tableLayoutPanel3.Controls.Add(label3, 0, 1);
            tableLayoutPanel3.Controls.Add(txtProductGtin, 1, 1);
            tableLayoutPanel3.Controls.Add(label4, 0, 2);
            tableLayoutPanel3.Controls.Add(txtProductName, 1, 2);
            tableLayoutPanel3.Controls.Add(label5, 0, 3);
            tableLayoutPanel3.Controls.Add(txProdAciklama, 1, 3);
            tableLayoutPanel3.Controls.Add(cmbCustomers, 1, 0);
            tableLayoutPanel3.Controls.Add(btnProductSave, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(676, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Size = new Size(667, 125);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(3, 8);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 0;
            label2.Text = "Müşteri Seç:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 39);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 1;
            label3.Text = "GTIN:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtProductGtin
            // 
            txtProductGtin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductGtin.Location = new Point(93, 35);
            txtProductGtin.Name = "txtProductGtin";
            txtProductGtin.Size = new Size(213, 23);
            txtProductGtin.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(3, 70);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 4;
            label4.Text = "Ürün Adı:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.Location = new Point(93, 66);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(213, 23);
            txtProductName.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(3, 101);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 6;
            label5.Text = "Aciklama:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txProdAciklama
            // 
            txProdAciklama.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txProdAciklama.Location = new Point(93, 97);
            txProdAciklama.Name = "txProdAciklama";
            txProdAciklama.Size = new Size(213, 23);
            txProdAciklama.TabIndex = 7;
            // 
            // cmbCustomers
            // 
            cmbCustomers.Dock = DockStyle.Fill;
            cmbCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomers.FormattingEnabled = true;
            cmbCustomers.Location = new Point(93, 3);
            cmbCustomers.Name = "cmbCustomers";
            cmbCustomers.Size = new Size(213, 23);
            cmbCustomers.TabIndex = 8;
            // 
            // btnProductSave
            // 
            btnProductSave.Anchor = AnchorStyles.Left;
            btnProductSave.Location = new Point(312, 4);
            btnProductSave.Name = "btnProductSave";
            btnProductSave.Size = new Size(75, 23);
            btnProductSave.TabIndex = 9;
            btnProductSave.Text = "Kaydet";
            btnProductSave.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.6926537F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.98051F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.3268356F));
            tableLayoutPanel2.Controls.Add(lblCustomerCreate, 0, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(txtCustomerName, 1, 0);
            tableLayoutPanel2.Controls.Add(txtCustomerGln, 1, 1);
            tableLayoutPanel2.Controls.Add(labelDesc, 0, 2);
            tableLayoutPanel2.Controls.Add(txtCustomerDesc, 1, 2);
            tableLayoutPanel2.Controls.Add(btnCustomerSave, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Size = new Size(667, 125);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // lblCustomerCreate
            // 
            lblCustomerCreate.Anchor = AnchorStyles.Left;
            lblCustomerCreate.AutoSize = true;
            lblCustomerCreate.Location = new Point(3, 13);
            lblCustomerCreate.Name = "lblCustomerCreate";
            lblCustomerCreate.Size = new Size(75, 15);
            lblCustomerCreate.TabIndex = 0;
            lblCustomerCreate.Text = "Müşteri İsim:";
            lblCustomerCreate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 54);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 1;
            label1.Text = "GLN:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCustomerName
            // 
            txtCustomerName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerName.Location = new Point(101, 9);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(254, 23);
            txtCustomerName.TabIndex = 2;
            // 
            // txtCustomerGln
            // 
            txtCustomerGln.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerGln.Location = new Point(101, 50);
            txtCustomerGln.Name = "txtCustomerGln";
            txtCustomerGln.Size = new Size(254, 23);
            txtCustomerGln.TabIndex = 3;
            // 
            // labelDesc
            // 
            labelDesc.Anchor = AnchorStyles.Left;
            labelDesc.AutoSize = true;
            labelDesc.Location = new Point(3, 96);
            labelDesc.Name = "labelDesc";
            labelDesc.Size = new Size(59, 15);
            labelDesc.TabIndex = 4;
            labelDesc.Text = "Aciklama:";
            labelDesc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCustomerDesc
            // 
            txtCustomerDesc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerDesc.Location = new Point(101, 92);
            txtCustomerDesc.Name = "txtCustomerDesc";
            txtCustomerDesc.Size = new Size(254, 23);
            txtCustomerDesc.TabIndex = 5;
            // 
            // btnCustomerSave
            // 
            btnCustomerSave.Anchor = AnchorStyles.Left;
            btnCustomerSave.Location = new Point(361, 9);
            btnCustomerSave.Name = "btnCustomerSave";
            btnCustomerSave.Size = new Size(75, 23);
            btnCustomerSave.TabIndex = 8;
            btnCustomerSave.Text = "Kaydet";
            btnCustomerSave.UseVisualStyleBackColor = true;
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(3, 134);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowTemplate.Height = 25;
            dgvCustomers.Size = new Size(667, 520);
            dgvCustomers.TabIndex = 3;
            // 
            // CustomerPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1346, 657);
            Controls.Add(tableLayoutPanel1);
            Name = "CustomerPage";
            Text = "CustomerPage";
            Load += CustomerPage_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblCustomerCreate;
        private Label label1;
        private TextBox txtCustomerName;
        private TextBox txtCustomerGln;
        private Label labelDesc;
        private DataGridView dgvProducts;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private TextBox txtProductGtin;
        private Label label4;
        private TextBox txtProductName;
        private Label label5;
        private TextBox txProdAciklama;
        private TextBox txtCustomerDesc;
        private DataGridView dgvCustomers;
        private Button musteriRhbr;
        private Button btnCustomerSave;
        private ComboBox cmbCustomers;
        private Button btnProductSave;
    }
}