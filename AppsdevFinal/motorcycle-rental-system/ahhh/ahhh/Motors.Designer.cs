namespace ahhh
{
    partial class Motors
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(Motors));
            this.dataGridView1   = new System.Windows.Forms.DataGridView();
            this.pictureBox1     = new System.Windows.Forms.PictureBox();
            this.label1          = new System.Windows.Forms.Label();
            this.lblCurrentUser  = new System.Windows.Forms.Label();
            this.cmbView         = new System.Windows.Forms.ComboBox();
            this.btnRent         = new System.Windows.Forms.Button();
            this.btnReturn       = new System.Windows.Forms.Button();
            this.btnAddMotor     = new System.Windows.Forms.Button();
            this.btnEditMotor    = new System.Windows.Forms.Button();
            this.btnDeleteMotor  = new System.Windows.Forms.Button();
            this.btnRefresh      = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // dataGridView1 — main data grid (unchanged position/size)
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(55, 319);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1440, 472);
            this.dataGridView1.TabIndex = 0;
            //
            // pictureBox1 — banner (unchanged position/size)
            //
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage =
                ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(405, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(773, 211);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            //
            // label1 — "RENT NOW" heading (unchanged)
            //
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font(
                "Microsoft Sans Serif", 36F,
                ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))),
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.label1.Location = new System.Drawing.Point(656, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 55);
            this.label1.TabIndex = 2;
            this.label1.Text = "RENT NOW";
            //
            // lblCurrentUser — shows logged-in username
            //
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font(
                "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.lblCurrentUser.Location = new System.Drawing.Point(20, 108);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(130, 16);
            this.lblCurrentUser.TabIndex = 3;
            this.lblCurrentUser.Text = "Welcome, User";
            //
            // cmbView — filter: All Motorcycles / Available Only / My Rentals
            //
            this.cmbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmbView.Location = new System.Drawing.Point(20, 135);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(175, 24);
            this.cmbView.TabIndex = 4;
            //
            // btnRent
            //
            this.btnRent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRent.ForeColor = System.Drawing.Color.Black;
            this.btnRent.Location = new System.Drawing.Point(20, 175);
            this.btnRent.Name = "btnRent";
            this.btnRent.Size = new System.Drawing.Size(170, 32);
            this.btnRent.TabIndex = 5;
            this.btnRent.Text = "Rent Selected Motor";
            this.btnRent.UseVisualStyleBackColor = true;
            //
            // btnReturn
            //
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnReturn.ForeColor = System.Drawing.Color.Black;
            this.btnReturn.Location = new System.Drawing.Point(20, 215);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(170, 32);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "Return Motorcycle";
            this.btnReturn.UseVisualStyleBackColor = true;
            //
            // btnAddMotor
            //
            this.btnAddMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnAddMotor.ForeColor = System.Drawing.Color.Black;
            this.btnAddMotor.Location = new System.Drawing.Point(215, 135);
            this.btnAddMotor.Name = "btnAddMotor";
            this.btnAddMotor.Size = new System.Drawing.Size(160, 32);
            this.btnAddMotor.TabIndex = 7;
            this.btnAddMotor.Text = "Add Motorcycle";
            this.btnAddMotor.UseVisualStyleBackColor = true;
            //
            // btnEditMotor
            //
            this.btnEditMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnEditMotor.ForeColor = System.Drawing.Color.Black;
            this.btnEditMotor.Location = new System.Drawing.Point(215, 175);
            this.btnEditMotor.Name = "btnEditMotor";
            this.btnEditMotor.Size = new System.Drawing.Size(160, 32);
            this.btnEditMotor.TabIndex = 8;
            this.btnEditMotor.Text = "Edit Selected";
            this.btnEditMotor.UseVisualStyleBackColor = true;
            //
            // btnDeleteMotor
            //
            this.btnDeleteMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDeleteMotor.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteMotor.Location = new System.Drawing.Point(215, 215);
            this.btnDeleteMotor.Name = "btnDeleteMotor";
            this.btnDeleteMotor.Size = new System.Drawing.Size(160, 32);
            this.btnDeleteMotor.TabIndex = 9;
            this.btnDeleteMotor.Text = "Delete Selected";
            this.btnDeleteMotor.UseVisualStyleBackColor = true;
            //
            // btnRefresh
            //
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(215, 255);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 32);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            //
            // Motors
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1547, 816);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDeleteMotor);
            this.Controls.Add(this.btnEditMotor);
            this.Controls.Add(this.btnAddMotor);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnRent);
            this.Controls.Add(this.cmbView);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.Name = "Motors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RideNow - Motorcycle Rental";
            this.Load += new System.EventHandler(this.Motors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.ComboBox cmbView;
        private System.Windows.Forms.Button btnRent;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnAddMotor;
        private System.Windows.Forms.Button btnEditMotor;
        private System.Windows.Forms.Button btnDeleteMotor;
        private System.Windows.Forms.Button btnRefresh;
    }
}
