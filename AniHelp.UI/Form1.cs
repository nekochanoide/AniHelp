using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AniHelp.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AnimalDataDto GetModelFromUI()
        {
            return new AnimalDataDto()
            {
                Filled = dateTimePicker1.Value,
                Name = textBox1.Text,
                SeizurePlace = textBox2.Text,
                Status = new AnimalStatus
                {
                    Collar = checkBox4.Checked ? textBox3.Text : null,
                    Pregnancy = checkBox1.Checked,
                    CrueltySigns = checkBox2.Checked,
                    HealthTroubles = listBox1.Items.OfType<string>().ToList(),
                    EuthanasiaCause = checkBox3.Checked ? textBox5.Text : null
                },
                Action = (Actions)comboBox1.SelectedIndex
            };
        }
        private void SetModelToUI(AnimalDataDto dto)
        {
            dateTimePicker1.Value = dto.Filled;
            textBox1.Text = dto.Name;
            textBox2.Text = dto.SeizurePlace;
            listBox1.Items.Clear();
            if (dto.Status != null)
            {
                if (dto.Status.Collar != null)
                {
                    checkBox4.Checked = true;
                    textBox3.Text = dto.Status.Collar;
                }
                checkBox1.Checked = dto.Status.Pregnancy;
                checkBox2.Checked = dto.Status.CrueltySigns;
                foreach (var e in dto.Status.HealthTroubles)
                {
                    listBox1.Items.Add(e);
                }
                if (dto.Status.EuthanasiaCause != null)
                {
                    checkBox3.Checked = true;
                    textBox5.Text = dto.Status.EuthanasiaCause;
                }
            }
            comboBox1.SelectedIndex = (int)dto.Action;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Файл данных о животных|*.anih" };
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = GetModelFromUI();
                BlankDtoHelper.WriteToFile(sfd.FileName, dto);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Файл данных о животных|*.anih" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = BlankDtoHelper.LoadFromFile(ofd.FileName);
                SetModelToUI(dto);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox5.Text = null;
            }
        }
        //кнопка добавить
        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox4.Text);
            textBox4.Text = null;
        }
        //
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var si = listBox1.SelectedIndex;
            listBox1.Items.RemoveAt(si);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox5.Text = null;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Text = null;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
