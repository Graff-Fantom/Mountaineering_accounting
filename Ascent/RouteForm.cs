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
    public partial class RouteForm : Form
    {
        public RouteForm()
        {
            InitializeComponent();
        }

        private void RouteForm_Load(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var r = db.The_route.ToList();
                dataGridView1.DataSource = db.The_route.ToList();
                var tmp = db.Mountain.Select(i => new { i.id_mointain , i.Name_mount}).ToList();
                if (tmp.Count == 0)
                {
                    MessageBox.Show("Добавьте запись в таблицу Mountain!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close(); return;
                }
                SortedDictionary<int, string> data1 = new SortedDictionary<int, string>();
                foreach (var item in tmp)
                {
                    data1.Add(item.id_mointain, item.Name_mount);

                }
                 comboBox1.DataSource = new BindingSource(data1, null);
                    comboBox1.DisplayMember = "Value";
                    comboBox1.ValueMember = "Key";
                    dataGridView1.Columns["Mountain"].Visible = false;
                
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                db.The_route.Add(new The_route()
                {

                    Name_route = textBox3.Text,
                    Leanght = textBox2.Text,
                    Route_danger = textBox1.Text,
                    Code_mount = Convert.ToInt32(comboBox1.SelectedValue)
                }) ;

                db.SaveChanges();
                dataGridView1.DataSource = db.The_route.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.The_route.ToList().FirstOrDefault(c => c.Cod_route ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    tmp.Mountain = db.Mountain.FirstOrDefault(c => c.id_mointain == Convert.ToInt32(comboBox1.SelectedValue.ToString()));
                    tmp.Name_route = textBox3.Text;
                  tmp.Leanght = textBox2.Text;
                    tmp.Route_danger = textBox1.Text;
                    db.SaveChanges();
                    dataGridView1.DataSource = db.The_route.ToList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tmp = db.The_route.ToList().FirstOrDefault(c => c.Cod_route ==
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                if (tmp != null)
                {
                    if (MessageBox.Show("Удалить ID " + tmp.Cod_route, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.The_route.Remove(tmp);
                        db.SaveChanges();
                        dataGridView1.DataSource = db.The_route.ToList();
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

                    var tmp = db.Mountain.ToList().FirstOrDefault(c => c.id_mointain ==
                   Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value.ToString()));
                    var tmp1 = db.The_route.ToList().FirstOrDefault(c => c.Cod_route ==
                   Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    comboBox1.Text = tmp.Name_mount;
                    textBox3.Text = tmp1.Name_route;
                    textBox2.Text= tmp1.Leanght;
                    textBox1.Text = tmp1.Route_danger;
                }
            }
        }

        ApplicationContext db = new ApplicationContext();
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var res = db.The_route.ToList().Where(x => x.Cod_route.ToString().Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
         x.Code_mount.ToString().Contains(textBox4.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
         x.Cod_route.ToString().Contains(textBox4.Text.Trim()) ||
         x.Name_route.ToString().Contains(textBox4.Text.Trim()) ||
         x.Leanght.ToString().Contains(textBox4.Text.Trim()) ||
         x.Route_danger.ToString().Contains(textBox4.Text.Trim())).ToList();

            dataGridView1.DataSource = res.OrderBy(x => x.Cod_route).ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
