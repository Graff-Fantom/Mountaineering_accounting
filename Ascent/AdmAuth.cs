using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Ascent
{
    public partial class AdmAuth : Form
    {
        public AdmAuth()
        {
            InitializeComponent();
            passField.MaxLength = 14;
            passField.PasswordChar = '\u25CF';
        }

        private void AdmAuth_Load(object sender, EventArgs e)
        {

        }
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginField.Text))
            {
                MessageBox.Show("Заполните логин!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(passField.Text))
            {
                MessageBox.Show("Заполните пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string loginUser = Helper.GetMd5Hash(loginField.Text);
            string passwordUser = Helper.GetMd5Hash(passField.Text);

            using (ApplicationContext db = new ApplicationContext())
            {

                var user = db.Users.FirstOrDefault(c => c.logins == loginUser && c.Passwords == passwordUser);


                if (user != null)
                {
                    MessageBox.Show("Авторизация прошла успешно!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    AdminMain newForm = new AdminMain();
                    newForm.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            passField.PasswordChar = '\0';
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            passField.PasswordChar = '\u25CF';
            timer1.Stop();
        }

       
    }
}
