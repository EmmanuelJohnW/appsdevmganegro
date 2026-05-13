using System;
using System.Windows.Forms;

namespace ahhh
{
    public partial class RegisterForm : Form
    {
        private readonly WelcomeForm _welcome;

        public RegisterForm(WelcomeForm welcome)
        {
            InitializeComponent();
            _welcome = welcome;
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username        = textBox1.Text.Trim();
            string password        = textBox2.Text.Trim();
            string confirmPassword = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }

            button1.Enabled = false;
            button1.Text = "Registering...";

            try
            {
                bool exists = await SupabaseService.UsernameExistsAsync(username);
                if (exists)
                {
                    MessageBox.Show("This username is already taken.", "Duplicate Username",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                bool ok = await SupabaseService.RegisterUserAsync(username, password);
                if (ok)
                {
                    MessageBox.Show("Registration successful! Please log in.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _welcome.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
                button1.Text = "Register Now";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _welcome.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void RegisterForm_Load(object sender, EventArgs e) { }
    }
}
