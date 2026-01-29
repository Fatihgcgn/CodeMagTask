namespace WinForm.Pages
{
    partial class WorkOrderPage
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
            cmbCustomer = new ComboBox();
            lblCustomer = new Label();
            label1 = new Label();
            txtQuantity = new TextBox();
            label3 = new Label();
            txtBatchNo = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            dtpExpiry = new DateTimePicker();
            cmbStatus = new ComboBox();
            btnProductRehber = new Button();
            txtProductName = new TextBox();
            label2 = new Label();
            btnWorkOrderSave = new Button();
            dgvWorkOrders = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWorkOrders).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvWorkOrders, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 29.1935482F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70.80645F));
            tableLayoutPanel1.Size = new Size(1187, 620);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.3758F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.0116F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.0990982F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.522522F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 143F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450F));
            tableLayoutPanel2.Controls.Add(cmbCustomer, 1, 0);
            tableLayoutPanel2.Controls.Add(lblCustomer, 0, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(txtQuantity, 1, 1);
            tableLayoutPanel2.Controls.Add(label3, 0, 2);
            tableLayoutPanel2.Controls.Add(txtBatchNo, 1, 2);
            tableLayoutPanel2.Controls.Add(label4, 0, 3);
            tableLayoutPanel2.Controls.Add(textBox3, 1, 3);
            tableLayoutPanel2.Controls.Add(label5, 0, 4);
            tableLayoutPanel2.Controls.Add(label6, 0, 5);
            tableLayoutPanel2.Controls.Add(dtpExpiry, 1, 5);
            tableLayoutPanel2.Controls.Add(cmbStatus, 1, 4);
            tableLayoutPanel2.Controls.Add(btnProductRehber, 4, 0);
            tableLayoutPanel2.Controls.Add(txtProductName, 3, 0);
            tableLayoutPanel2.Controls.Add(label2, 2, 0);
            tableLayoutPanel2.Controls.Add(btnWorkOrderSave, 2, 5);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.Size = new Size(1181, 175);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // cmbCustomer
            // 
            cmbCustomer.Dock = DockStyle.Fill;
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "9" });
            cmbCustomer.Location = new Point(81, 3);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(258, 23);
            cmbCustomer.TabIndex = 28;
            cmbCustomer.SelectedIndexChanged += cmbCustomer_SelectedIndexChanged_1;
            // 
            // lblCustomer
            // 
            lblCustomer.Anchor = AnchorStyles.Left;
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(3, 7);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(71, 15);
            lblCustomer.TabIndex = 27;
            lblCustomer.Text = "Müşteri Seç:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 36);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 16;
            label1.Text = "Miktar";
            // 
            // txtQuantity
            // 
            txtQuantity.Dock = DockStyle.Fill;
            txtQuantity.Location = new Point(81, 32);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(258, 23);
            txtQuantity.TabIndex = 15;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 65);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 17;
            label3.Text = "BatchNo";
            // 
            // txtBatchNo
            // 
            txtBatchNo.Dock = DockStyle.Fill;
            txtBatchNo.Location = new Point(81, 61);
            txtBatchNo.Name = "txtBatchNo";
            txtBatchNo.Size = new Size(258, 23);
            txtBatchNo.TabIndex = 18;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(3, 94);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 19;
            label4.Text = "SerialStart";
            // 
            // textBox3
            // 
            textBox3.Dock = DockStyle.Fill;
            textBox3.Location = new Point(81, 90);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(258, 23);
            textBox3.TabIndex = 20;
            textBox3.Text = "1";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(3, 123);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 21;
            label5.Text = "Status";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(3, 152);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 23;
            label6.Text = "ExpiryDate";
            // 
            // dtpExpiry
            // 
            dtpExpiry.Dock = DockStyle.Fill;
            dtpExpiry.Format = DateTimePickerFormat.Short;
            dtpExpiry.Location = new Point(81, 148);
            dtpExpiry.Name = "dtpExpiry";
            dtpExpiry.Size = new Size(258, 23);
            dtpExpiry.TabIndex = 24;
            // 
            // cmbStatus
            // 
            cmbStatus.Dock = DockStyle.Fill;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "9" });
            cmbStatus.Location = new Point(81, 119);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(258, 23);
            cmbStatus.TabIndex = 26;
            // 
            // btnProductRehber
            // 
            btnProductRehber.BackColor = Color.SkyBlue;
            btnProductRehber.Location = new Point(589, 3);
            btnProductRehber.Name = "btnProductRehber";
            btnProductRehber.Size = new Size(113, 23);
            btnProductRehber.TabIndex = 7;
            btnProductRehber.Text = "Ürün Seç";
            btnProductRehber.UseVisualStyleBackColor = false;
            btnProductRehber.Click += btnProductRehber_Click;
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.Location = new Point(457, 3);
            txtProductName.Name = "txtProductName";
            txtProductName.ReadOnly = true;
            txtProductName.Size = new Size(126, 23);
            txtProductName.TabIndex = 9;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(394, 7);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Ürün Adı:";
            // 
            // btnWorkOrderSave
            // 
            btnWorkOrderSave.Anchor = AnchorStyles.Left;
            btnWorkOrderSave.BackColor = Color.Aqua;
            btnWorkOrderSave.Location = new Point(345, 148);
            btnWorkOrderSave.Name = "btnWorkOrderSave";
            btnWorkOrderSave.Size = new Size(95, 23);
            btnWorkOrderSave.TabIndex = 25;
            btnWorkOrderSave.Text = "İş Emri Oluştur";
            btnWorkOrderSave.UseVisualStyleBackColor = false;
            btnWorkOrderSave.Click += btnWorkOrderSave_Click;
            // 
            // dgvWorkOrders
            // 
            dgvWorkOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWorkOrders.Dock = DockStyle.Fill;
            dgvWorkOrders.Location = new Point(3, 184);
            dgvWorkOrders.Name = "dgvWorkOrders";
            dgvWorkOrders.RowTemplate.Height = 25;
            dgvWorkOrders.Size = new Size(1181, 433);
            dgvWorkOrders.TabIndex = 1;
            // 
            // WorkOrderPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1187, 620);
            Controls.Add(tableLayoutPanel1);
            Name = "WorkOrderPage";
            Text = "WorkOrderPage";
            Load += WorkOrderPage_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWorkOrders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Button btnProductRehber;
        private TextBox txtProductName;
        private Label label1;
        private TextBox txtQuantity;
        private Label label3;
        private TextBox txtBatchNo;
        private Label label4;
        private TextBox textBox3;
        private Label label5;
        private Label label6;
        private DateTimePicker dtpExpiry;
        private Button btnWorkOrderSave;
        private ComboBox cmbStatus;
        private Label lblCustomer;
        private ComboBox cmbCustomer;
        private DataGridView dgvWorkOrders;
    }
}