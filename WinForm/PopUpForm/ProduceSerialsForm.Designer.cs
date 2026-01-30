namespace WinForm.PopUpForm
{
    partial class ProduceSerialsForm
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
            btnProduce = new Button();
            btnCancel = new Button();
            nudCount = new NumericUpDown();
            lblInfo = new Label();
            txtError = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudCount).BeginInit();
            SuspendLayout();
            // 
            // btnProduce
            // 
            btnProduce.Location = new Point(32, 175);
            btnProduce.Name = "btnProduce";
            btnProduce.Size = new Size(151, 52);
            btnProduce.TabIndex = 0;
            btnProduce.Text = "Üret";
            btnProduce.UseVisualStyleBackColor = true;
            btnProduce.Click += btnProduce_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(208, 175);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(154, 52);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "İptal Et";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // nudCount
            // 
            nudCount.Location = new Point(32, 55);
            nudCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCount.Name = "nudCount";
            nudCount.Size = new Size(330, 23);
            nudCount.TabIndex = 3;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Left;
            lblInfo.Location = new Point(32, 81);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(330, 77);
            lblInfo.TabIndex = 4;
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblInfo.Click += lblInfo_Click;
            // 
            // txtError
            // 
            txtError.Location = new Point(32, 84);
            txtError.Multiline = true;
            txtError.Name = "txtError";
            txtError.ReadOnly = true;
            txtError.Size = new Size(330, 74);
            txtError.TabIndex = 5;
            // 
            // ProduceSerialsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 260);
            Controls.Add(txtError);
            Controls.Add(lblInfo);
            Controls.Add(nudCount);
            Controls.Add(btnCancel);
            Controls.Add(btnProduce);
            Name = "ProduceSerialsForm";
            Text = "ProduceSerialsForm";
            ((System.ComponentModel.ISupportInitialize)nudCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnProduce;
        private Button btnCancel;
        private NumericUpDown nudCount;
        private Label lblInfo;
        private TextBox txtError;
    }
}