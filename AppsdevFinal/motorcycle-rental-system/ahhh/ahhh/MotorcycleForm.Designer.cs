namespace ahhh
{
    partial class MotorcycleForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle      = new System.Windows.Forms.Label();
            this.label1        = new System.Windows.Forms.Label();
            this.txtName       = new System.Windows.Forms.TextBox();
            this.label2        = new System.Windows.Forms.Label();
            this.txtModel      = new System.Windows.Forms.TextBox();
            this.label3        = new System.Windows.Forms.Label();
            this.numDailyRate  = new System.Windows.Forms.NumericUpDown();
            this.btnSave       = new System.Windows.Forms.Button();
            this.btnCancel     = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDailyRate)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.lblTitle.Location = new System.Drawing.Point(82, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(170, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Motorcycle";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.label1.Location = new System.Drawing.Point(30, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            //
            // txtName
            //
            this.txtName.Location = new System.Drawing.Point(33, 84);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(278, 20);
            this.txtName.TabIndex = 2;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.label2.Location = new System.Drawing.Point(30, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Model:";
            //
            // txtModel
            //
            this.txtModel.Location = new System.Drawing.Point(33, 134);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(278, 20);
            this.txtModel.TabIndex = 4;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.label3.Location = new System.Drawing.Point(30, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Daily Rate (₱):";
            //
            // numDailyRate
            //
            this.numDailyRate.DecimalPlaces = 2;
            this.numDailyRate.Location = new System.Drawing.Point(33, 184);
            this.numDailyRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numDailyRate.Name = "numDailyRate";
            this.numDailyRate.Size = new System.Drawing.Size(120, 20);
            this.numDailyRate.TabIndex = 6;
            //
            // btnSave
            //
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(88, 228);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(184, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // MotorcycleForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(344, 278);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numDailyRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MotorcycleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Motorcycle";
            ((System.ComponentModel.ISupportInitialize)(this.numDailyRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numDailyRate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
