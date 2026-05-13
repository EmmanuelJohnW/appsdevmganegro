using System;
using System.Drawing;
using System.Windows.Forms;

namespace ahhh
{
    internal class RentalDateForm : Form
    {
        public DateTime StartDate => dtpStart.Value.Date;
        public DateTime EndDate   => dtpEnd.Value.Date;
        public int      TotalDays => (EndDate - StartDate).Days + 1;

        private readonly decimal _dailyRate;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label lblSummary;

        public RentalDateForm(string motorcycleName, decimal dailyRate)
        {
            _dailyRate = dailyRate;
            BuildUI(motorcycleName);
            UpdateSummary();
        }

        private void BuildUI(string motorcycleName)
        {
            Text            = "Rental Dates";
            Width           = 360;
            Height          = 210;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition   = FormStartPosition.CenterParent;
            MaximizeBox     = false;
            MinimizeBox     = false;

            var lblTitle = new Label
            {
                Text   = motorcycleName,
                Left   = 12, Top = 12, Width = 320,
                Font   = new Font(Font, FontStyle.Bold),
                AutoSize = true
            };

            var lblStart = new Label { Text = "Start Date:", Left = 12, Top = 46, AutoSize = true };
            dtpStart = new DateTimePicker
            {
                Left   = 110, Top = 42, Width = 220,
                Format = DateTimePickerFormat.Short,
                Value  = DateTime.Today
            };

            var lblEnd = new Label { Text = "End Date:", Left = 12, Top = 78, AutoSize = true };
            dtpEnd = new DateTimePicker
            {
                Left   = 110, Top = 74, Width = 220,
                Format = DateTimePickerFormat.Short,
                Value  = DateTime.Today.AddDays(1)
            };

            lblSummary = new Label { Left = 12, Top = 108, Width = 320, Height = 20 };

            var btnOk     = new Button { Text = "Confirm", Left = 160, Top = 136, Width = 80, DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Cancel",  Left = 250, Top = 136, Width = 80, DialogResult = DialogResult.Cancel };

            dtpStart.ValueChanged += (s, e) => UpdateSummary();
            dtpEnd.ValueChanged   += (s, e) => UpdateSummary();
            btnOk.Click           += BtnOk_Click;

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblStart, dtpStart, lblEnd, dtpEnd,
                lblSummary, btnOk, btnCancel
            });
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (StartDate < DateTime.Today)
            {
                MessageBox.Show("Start date cannot be in the past.", "Invalid Date",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            if (EndDate < StartDate)
            {
                MessageBox.Show("End date must be on or after the start date.", "Invalid Date",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }

        private void UpdateSummary()
        {
            int days = Math.Max(1, (dtpEnd.Value.Date - dtpStart.Value.Date).Days + 1);
            decimal total = days * _dailyRate;
            lblSummary.Text = $"{days} day(s)  ×  ₱{_dailyRate:0.##}/day  =  ₱{total:0.##} total";
        }
    }
}
