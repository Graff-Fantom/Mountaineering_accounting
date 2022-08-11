using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Ascent
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }
        public int a = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Заполните логин!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Заполните пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            char[] symv = textBox2.Text.ToCharArray();
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (symv[i] == '*' | symv[i] == '&' | symv[i] == '{' | symv[i] == '}' | symv[i] == '|' | symv[i] == '+')
                {

                    MessageBox.Show("Введите правельный пароль: БЕЗ символов '*' '&' '{' '}' '|' '+'", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    a++;
                    break;
                }


            }

            if (a == 0)

            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    string loginUser = Helper.GetMd5Hash(textBox1.Text);
                    var user1 = db.Users.FirstOrDefault(c => c.logins == loginUser);
                    if (user1 == null)
                    {
                        Users user = new Users
                        {
                            logins = Helper.GetMd5Hash(textBox1.Text),
                            Passwords = Helper.GetMd5Hash(textBox2.Text),

                        };

                        db.Users.Add(user);
                        db.SaveChanges();
                        MessageBox.Show("Аккаунт зарегистрирован!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else { MessageBox.Show("Введите другой логин!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }


            }
        }
                        
                       

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {

        }
    }
}
