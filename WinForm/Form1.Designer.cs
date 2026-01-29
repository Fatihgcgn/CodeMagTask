namespace WinForm
{
    partial class Form1
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            menuStrip1 = new MenuStrip();
            customerToolStripMenuItem = new ToolStripMenuItem();
            workOrdersToolStripMenuItem = new ToolStripMenuItem();
            logisticUnitsToolStripMenuItem = new ToolStripMenuItem();
            aggregationToolStripMenuItem = new ToolStripMenuItem();
            panelContent = new Panel();
            labelMerhaba = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            panelContent.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panelContent, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.075534F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 93.92447F));
            tableLayoutPanel1.Size = new Size(1133, 609);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1127, 31);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.Fill;
            menuStrip1.Items.AddRange(new ToolStripItem[] { customerToolStripMenuItem, workOrdersToolStripMenuItem, logisticUnitsToolStripMenuItem, aggregationToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1127, 31);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // customerToolStripMenuItem
            // 
            customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            customerToolStripMenuItem.Size = new Size(59, 27);
            customerToolStripMenuItem.Text = "Müşteri";
            customerToolStripMenuItem.Click += customerToolStripMenuItem_Click;
            // 
            // workOrdersToolStripMenuItem
            // 
            workOrdersToolStripMenuItem.Name = "workOrdersToolStripMenuItem";
            workOrdersToolStripMenuItem.Size = new Size(54, 27);
            workOrdersToolStripMenuItem.Text = "İş Emri";
            workOrdersToolStripMenuItem.Click += workOrdersToolStripMenuItem_Click;
            // 
            // logisticUnitsToolStripMenuItem
            // 
            logisticUnitsToolStripMenuItem.Name = "logisticUnitsToolStripMenuItem";
            logisticUnitsToolStripMenuItem.Size = new Size(103, 27);
            logisticUnitsToolStripMenuItem.Text = "Lojistik Birimleri";
            logisticUnitsToolStripMenuItem.Click += logisticUnitsToolStripMenuItem_Click;
            // 
            // aggregationToolStripMenuItem
            // 
            aggregationToolStripMenuItem.Name = "aggregationToolStripMenuItem";
            aggregationToolStripMenuItem.Size = new Size(85, 27);
            aggregationToolStripMenuItem.Text = "Aggregation";
            aggregationToolStripMenuItem.Click += aggregationToolStripMenuItem_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(labelMerhaba);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(3, 40);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1127, 566);
            panelContent.TabIndex = 1;
            // 
            // labelMerhaba
            // 
            labelMerhaba.Anchor = AnchorStyles.None;
            labelMerhaba.AutoSize = true;
            labelMerhaba.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelMerhaba.Location = new Point(420, 211);
            labelMerhaba.Name = "labelMerhaba";
            labelMerhaba.Size = new Size(215, 24);
            labelMerhaba.TabIndex = 0;
            labelMerhaba.Text = "Merhaba Hoş geldiniz !";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1133, 609);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CodeMag";
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem workOrdersToolStripMenuItem;
        private ToolStripMenuItem customerToolStripMenuItem;
        private ToolStripMenuItem aggregationToolStripMenuItem;
        private ToolStripMenuItem logisticUnitsToolStripMenuItem;
        private Panel panelContent;
        private Label labelMerhaba;
    }
}
