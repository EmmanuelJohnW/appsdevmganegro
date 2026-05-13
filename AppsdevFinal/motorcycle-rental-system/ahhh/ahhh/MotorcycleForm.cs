using System;
using System.Windows.Forms;

namespace ahhh
{
    public partial class MotorcycleForm : Form
    {
        private readonly int _editId;

        public MotorcycleForm()
        {
            InitializeComponent();
            Text = "Add Motorcycle";
            lblTitle.Text = "Add Motorcycle";
        }

        public MotorcycleForm(int id, string name, string model, decimal dailyRate)
        {
            InitializeComponent();
            _editId = id;
            Text = "Edit Motorcycle";
            lblTitle.Text = "Edit Motorcycle";
            txtName.Text = name;
            txtModel.Text = model;
            numDailyRate.Value = dailyRate;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string name  = txtName.Text.Trim();
            string model = txtModel.Text.Trim();
            decimal rate = numDailyRate.Value;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a motorcycle name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                bool ok = _editId == 0
                    ? await SupabaseService.AddMotorcycleAsync(name, model, rate)
                    : await SupabaseService.UpdateMotorcycleAsync(_editId, name, model, rate);

                if (ok)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Operation failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
