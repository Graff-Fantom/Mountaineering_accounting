using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ascent
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Main1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TablesForm form = new TablesForm();
            form.ShowDialog();
            this.Show();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = ".xlsx Files (*.xlsx)|*.xlsx";
                saveFileDialog1.OverwritePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(new FileInfo(saveFileDialog1.FileName)))
                    {
                        var ws = package.Workbook.Worksheets.Add("Ascent");
                        ws.Cells[1, 1].Value = "Id_membership";
                        ws.Cells[1, 2].Value = "Id_route";
                        ws.Cells[1, 3].Value = "data_start";
                        ws.Cells[1, 4].Value = "data_finish";
                        
                        

                        using (ApplicationContext db = new ApplicationContext())
                        {
                            var Ascent = db.Ascent.ToList();
                            int index = 2;
                            foreach (var tmp in Ascent)
                            {
                                ws.Cells[index, 1].Value = tmp.Id_membership;
                                ws.Cells[index, 2].Value = tmp.Id_route;
                                ws.Cells[index, 3].Value = tmp.data_start;
                                ws.Cells[index, 4].Value = tmp.data_finish;
                       
                               
                                index++;
                            }
                        }
                        package.Save();
                    }

                    MessageBox.Show("Файл: " + saveFileDialog1.FileName, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла:\r\n" + ex.Message + "\r\n" + ex.StackTrace, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
