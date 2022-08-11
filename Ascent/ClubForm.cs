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
    public partial class ClubForm : Form
    {
        public ClubForm()
        {
            InitializeComponent(); 
            using (ApplicationContext db = new ApplicationContext())
            {
                dataGridView1.DataSource = db.Club.ToList();
            }
        }

        private void SotrudnikiForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Club.Add(new Club()
                {
                    Name_club = textBox1.Text,
                    Name_Sotr = textBox2.Text,
                    Address = textBox5.Text,
                    Phone = textBox6.Text,
                });
                db.SaveChanges();
                dataGridView1.DataSource = db.Club.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Club.ToList().FirstOrDefault(c => c.cod_club ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    tmp.Name_club = textBox1.Text;
                    tmp.Name_Sotr = textBox2.Text;               
                    tmp.Address = textBox5.Text;
                    tmp.Phone = textBox6.Text;

                    db.SaveChanges();
                    dataGridView1.DataSource = db.Club.ToList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Club.ToList().FirstOrDefault(c => c.cod_club ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    if (MessageBox.Show("Удалить ID " + tmp.cod_club, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.Club.Remove(tmp);
                        db.SaveChanges();
                        dataGridView1.DataSource = db.Club.ToList();
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        ApplicationContext db = new ApplicationContext();
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            var res = db.Club.ToList().Where(x => x.Address.Contains(textBox7.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
         x.cod_club.ToString().Contains(textBox7.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
          x.Name_club.Contains(textBox7.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
          x.Name_Sotr.Contains(textBox7.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
          x.Phone.Contains(textBox7.Text.Trim())).ToList();

            dataGridView1.DataSource = res.OrderBy(x => x.cod_club).ToList();

        }

      
    }
}