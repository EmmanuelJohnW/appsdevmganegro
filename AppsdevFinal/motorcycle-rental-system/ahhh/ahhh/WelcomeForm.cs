using System;
using System.Windows.Forms;

namespace ahhh
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        // button1 = Register Now
        private void button1_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(this);
            registerForm.Show();
            this.Hide();
        }

        // button2 = Login
        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}