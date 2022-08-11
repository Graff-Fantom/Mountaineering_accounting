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
    public partial class MountainForm : Form
    {
        public MountainForm()
        {
            InitializeComponent();
        
        }

       
                                                                //Добавление данных в таблицу
        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Mountain.Add(new Mountain() { Name_mount = textBox1.Text, Height_mount = textBox2.Text, Location_mount= textBox3.Text });
                db.SaveChanges();
                dataGridView1.DataSource = db.Mountain.ToList();
            }
        }
                                                                //Изменение данных из таблицы
        private void button2_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Mountain.ToList().FirstOrDefault(c => c.id_mointain ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    tmp.Name_mount = textBox1.Text;
                    tmp.Height_mount= textBox2.Text;
                    tmp.Location_mount = textBox3.Text;

                    db.SaveChanges();
                    dataGridView1.DataSource = db.Mountain.ToList();
                }
            }
        }
                                                                //Удаление данных из таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Mountain.ToList().FirstOrDefault(c => c.id_mointain ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    if (MessageBox.Show("Удалить ID " + tmp.id_mointain, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.Mountain.Remove(tmp);
                        db.SaveChanges();
                        dataGridView1.DataSource = db.Mountain.ToList();
                    }
                }
            }
        }
                                                                //Вывод  данных на экран
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        ApplicationContext db = new ApplicationContext();
                                                                //Поиск данных
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var res = db.Mountain.ToList().Where(x => x.Height_mount.Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
         x.Name_mount.Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
         x.Location_mount.Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
         x.id_mointain.ToString().Contains(textBox4.Text.Trim())).ToList();

            dataGridView1.DataSource = res.OrderBy(x => x.id_mointain).ToList();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void MountainForm_Load(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                dataGridView1.DataSource = db.Mountain.ToList();
            }
        }
    }
}
