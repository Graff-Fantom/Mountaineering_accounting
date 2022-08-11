using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ascent
{
    public partial class ClimberForm : Form
    {
        ApplicationContext db = new ApplicationContext();
        public ClimberForm()
        {
            InitializeComponent();
        }

        private void KlientForm_Load(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                dataGridView1.DataSource = db.Climber.ToList();
            }
        }
        //Добавление данных в таблицу
        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Climber.Add(new Climber() { FIO = textBox1.Text, Phone = textBox2.Text, Address = textBox3.Text, Age=Convert.ToInt32(textBox5.Text) });
                db.SaveChanges();
                dataGridView1.DataSource = db.Climber.ToList();
            }
        }
        //Изменение данных из таблицы
        private void button2_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Climber.ToList().FirstOrDefault(c => c.code_cl ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    tmp.FIO = textBox1.Text;
                    tmp.Phone = textBox2.Text;
                    tmp.Address = textBox3.Text;
                    tmp.Age = Convert.ToInt32(textBox5.Text);

                    db.SaveChanges();
                    dataGridView1.DataSource = db.Climber.ToList();
                }
            }
        }
        //Вывод  данных на экран
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        //Удаление данных из таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Climber.ToList().FirstOrDefault(c => c.code_cl ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    if (MessageBox.Show("Удалить ID " + tmp.code_cl, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.Climber.Remove(tmp);
                        db.SaveChanges();
                        dataGridView1.DataSource = db.Climber.ToList();
                    }
                }
            }
        }
        //Поиск данных
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var res = db.Climber.ToList().Where(x => x.FIO.Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
            x.Address.Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||

            x.Phone.Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)||
            x.Age.ToString().Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();

            dataGridView1.DataSource = res.OrderBy(x => x.code_cl).ToList();
        }
    }
}
