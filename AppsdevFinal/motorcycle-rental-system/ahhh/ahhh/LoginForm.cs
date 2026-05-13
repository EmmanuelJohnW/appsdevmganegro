using System;
using System.Windows.Forms;

namespace ahhh
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            button1.Enabled = false;
            button1.Text = "Logging in...";

            try
            {
                bool success = await SupabaseService.LoginAsync(username, password);

                if (success)
                {
                    Session.Username = username;
                    Motors motorsForm = new Motors();
                    motorsForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Clear();
                    textBox2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
                button1.Text = "Login Now";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}
