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
    public partial class AscentForm : Form
    {
        public AscentForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                dataGridView1.DataSource = db.Ascent.ToList();
                var The_route = db.The_route.Select(i => new { i.Cod_route, i.Code_mount }).ToList();
                if (The_route.Count == 0)
                {
                    MessageBox.Show("Добавьте запись в таблицу Route!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close(); return;
                }
                var Membership = db.Membership.Select(i => new { i.cod_member, i.cod_Climber1 }).ToList();
                if (Membership.Count == 0)
                {
                    MessageBox.Show("Добавьте запись в таблицу Product!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close(); return;
                }

                SortedDictionary<int, string> data1 = new SortedDictionary<int, string>();
                foreach (var item in Membership)
                {
                    data1.Add(item.cod_member, item.cod_Climber1.ToString() );

                }
                comboBox2.DataSource = new BindingSource(data1, null);
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";

                SortedDictionary<int, string> data2 = new SortedDictionary<int, string>();
                foreach (var item in The_route)
                {
                    data2.Add(item.Cod_route, item.Code_mount.ToString() );

                }
                comboBox1.DataSource = new BindingSource(data2, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";

                dataGridView1.Columns["Membership"].Visible = false;
                dataGridView1.Columns["The_route"].Visible = false;
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Ascent.Add(new Ascent()
                {
                    Id_route = Convert.ToInt32(comboBox1.SelectedValue),
                    Id_membership = Convert.ToInt32(comboBox2.SelectedValue),
                    data_start= dateTimePicker1.Value,
                    data_finish = dateTimePicker2.Value
                });

                db.SaveChanges();
                dataGridView1.DataSource = db.Ascent.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Ascent.ToList().FirstOrDefault(c => c.Cod_ascent ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    tmp.Id_route = Convert.ToInt32(comboBox1.SelectedValue);
                    tmp.Id_membership = Convert.ToInt32(comboBox2.SelectedValue);
                    tmp.data_start = dateTimePicker1.Value;
                    tmp.data_finish = dateTimePicker2.Value;

                    db.SaveChanges();
                    dataGridView1.DataSource = db.Ascent.ToList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.Ascent.ToList().FirstOrDefault(c => c.Cod_ascent ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    if (MessageBox.Show("Удалить ID " + tmp.Cod_ascent, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.Ascent.Remove(tmp);
                        db.SaveChanges();
                        dataGridView1.DataSource = db.Ascent.ToList();
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
                    var tmp = db.Ascent.ToList().FirstOrDefault(c => c.Cod_ascent ==
                    Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    if (tmp != null)
                    {
                        comboBox1.SelectedValue = tmp.Id_route;
                        comboBox2.SelectedValue = tmp.Id_membership;
                        dateTimePicker1.Value = tmp.data_start;
                        dateTimePicker2.Value= tmp.data_finish ;
                    }
                }
            }
        }

        

        

        ApplicationContext db = new ApplicationContext();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var res = db.Ascent.ToList().Where(x =>
         x.Cod_ascent.ToString().Contains(textBox1.Text.Trim()) ||
         x.Id_membership.ToString().Contains(textBox1.Text.Trim()) ||
         x.Id_route.ToString().Contains(textBox1.Text.Trim()) ||
         x.data_start.ToString().Contains(textBox1.Text.Trim()) ||
         x.data_finish.ToString().Contains(textBox1.Text.Trim())).ToList();

            dataGridView1.DataSource = res.OrderBy(x => x.Cod_ascent).ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
