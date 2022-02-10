using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEll
            );



        public Form1() { InitializeComponent(); }


        private void Form1_Load(object sender, EventArgs e) 
        {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 30, 30));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 30, 30));

            textBox1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox1.Width, textBox1.Height, 10, 10));
            textBox2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox2.Width, textBox2.Height, 10, 10));
            textBox3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox3.Width, textBox3.Height, 10, 10));
            textBox4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox4.Width, textBox4.Height, 10, 10));
            textBox5.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox5.Width, textBox5.Height, 10, 10));
            textBox7.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox7.Width, textBox7.Height, 10, 10));
            
        }


        private void Save(object sender, EventArgs e)
        {
            string Gender;
            Questionnaire questionnaire1 = new Questionnaire();

            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0 && textBox3.Text.Length != 0 && textBox4.Text.Length != 0 && textBox5.Text.Length != 0 && maskedTextBox1.Text.Length != 0 && radioButton1.Checked != false || radioButton2.Checked != false)
            {

                if (radioButton1.Checked == true) Gender = "Male";
                else Gender = "Female";

                questionnaire1.Surname = textBox1.Text;
                questionnaire1.Name = textBox2.Text;
                questionnaire1.FatherName = textBox3.Text;
                questionnaire1.Country = textBox4.Text;
                questionnaire1.City = textBox5.Text;
                questionnaire1.Phone = maskedTextBox1.Text;
                questionnaire1.DateOfBrith = dateTimePicker1.Text;
                questionnaire1.Gender = Gender;

                var str = JsonConvert.SerializeObject(questionnaire1, Newtonsoft.Json.Formatting.Indented);
                File.AppendAllText(textBox2.Text + ".json", str);

                MessageBox.Show("Questionnaire Succses", "Notification");
                label9.Text = "";

            }
            else
            {
                label9.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, label9.Width, label9.Height, 10, 10));
                label9.Text = "Don't forget to fill in the blanks !";
                label9.BackColor = Color.White;
                label9.ForeColor = Color.Red;
            }

        }

       

        private void Search(object sender, EventArgs e)
        { 

            if(textBox7.Text.Length != 0)
            {
                Questionnaire questionnaires = new Questionnaire();

                var jsonStr = File.ReadAllText(textBox7.Text + ".json");
                questionnaires = JsonConvert.DeserializeObject<Questionnaire>(jsonStr); 


                textBox1.Text = questionnaires.Surname;
                textBox2.Text = questionnaires.Name;
                textBox3.Text = questionnaires.FatherName;
                textBox4.Text = questionnaires.Country;
                textBox5.Text = questionnaires.City;
                maskedTextBox1.Text = questionnaires.Phone;
                dateTimePicker1.Text = questionnaires.DateOfBrith;

                if (questionnaires.Gender == "Male") { radioButton1.Checked = true; }
                else { radioButton2.Checked = true; }
            }
        }
    }
}
