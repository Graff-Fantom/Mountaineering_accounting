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
    public partial class MembershipForm : Form
    {
        public MembershipForm()
        {
            InitializeComponent();
        }

        private void ZakaziForm_Load(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                dataGridView1.DataSource = db.Membership.ToList();

                var Climber = db.Climber.Select(i => new { i.code_cl, i.FIO }).ToList();
                if (Climber.Count == 0)
                {
                    MessageBox.Show("Добавьте запись в таблицу 'Клуб альпенистов'!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close(); return;
                }
                var Club = db.Club.Select(i => new { i.cod_club, i.Name_club }).ToList();
                if (Club.Count == 0)
                {
                    MessageBox.Show("Добавьте запись в таблицу Product!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close(); return;
                }
               

                SortedDictionary<int, string> data1 = new SortedDictionary<int, string>();
                foreach (var item in Climber    )
                {
                    data1.Add(item.code_cl, item.FIO );

                }
                comboBox3.DataSource = new BindingSource(data1, null);
                comboBox3.DisplayMember = "Value";
                comboBox3.ValueMember = "Key";

               
                SortedDictionary<int, string> data2 = new SortedDictionary<int, string>();
                foreach (var item in Club)
                {
                    data2.Add(item.cod_club, item.Name_club );
                }
                comboBox1.DataSource = new BindingSource(data2, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                
               
                dataGridView1.Columns["Climber"].Visible = false;
                dataGridView1.Columns["Club"].Visible = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Membership.Add(new Membership()
                {
                    id_club = Convert.ToInt32(comboBox1.SelectedValue),
                    cod_Climber1= Convert.ToInt32(comboBox3.SelectedValue),
                   
                   


                });

                db.SaveChanges();
                dataGridView1.DataSource = db.Membership.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Membership.ToList().FirstOrDefault(c => c.cod_member ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    tmp.id_club = Convert.ToInt32(comboBox1.SelectedValue);
                    tmp.cod_Climber1 = Convert.ToInt32(comboBox3.SelectedValue);
                 

                    db.SaveChanges();
                    dataGridView1.DataSource = db.Membership.ToList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Membership.ToList().FirstOrDefault(c => c.cod_member ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    if (MessageBox.Show("Удалить ID " + tmp.cod_member, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.Membership.Remove(tmp);
                        db.SaveChanges();
                        dataGridView1.DataSource = db.Membership.ToList();
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var tmp = db.Membership.ToList().FirstOrDefault(c => c.cod_member ==
                    Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    if (tmp != null)
                    {
                        comboBox1.SelectedValue = tmp.id_club;
                        
                        comboBox3.SelectedValue = tmp.cod_Climber1;
                      


                    }
                }
            }
        }

        ApplicationContext db = new ApplicationContext();
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var res = db.Membership.ToList().Where(x =>
            
            x.cod_member.ToString().Contains(textBox4.Text.Trim()) ||
            x.id_club.ToString().Contains(textBox4.Text.Trim()) ||
            x.cod_Climber1.ToString().Contains(textBox4.Text.Trim())).ToList();

            dataGridView1.DataSource = res.OrderBy(x => x.cod_member).ToList();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}