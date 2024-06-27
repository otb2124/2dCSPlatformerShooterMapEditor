using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformerEditor
{
    public class InformationInputForm : Form
    {
        public TextBox TextBox1 { get; private set; }
        public TextBox TextBox2 { get; private set; }
        public TextBox TextBox3 { get; private set; }

        private MenuStripManager msm;
        public InformationInputForm(MenuStripManager msm)
        {
            this.msm = msm;
            InitializeComponent();
            SetPlaceholderText();
        }

        private void InitializeComponent()
        {
            this.Text = "Information Input";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(300, 200);

            TextBox1 = new TextBox
            {
                Text = "Save Date",
            Location = new Point(20, 20),
                Size = new Size(250, 20)
            };
            this.Controls.Add(TextBox1);

            TextBox2 = new TextBox
            {
                Text = "Total Time",
                Location = new Point(20, 50),
                Size = new Size(250, 20)
            };
            this.Controls.Add(TextBox2);

            TextBox3 = new TextBox
            {
                Text = "Percentage",
                Location = new Point(20, 80),
                Size = new Size(250, 20)
            };
            this.Controls.Add(TextBox3);

            Button okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(120, 120)
            };
            this.Controls.Add(okButton);

            this.AcceptButton = okButton;


            if(msm.date != null)
            {
                TextBox1.Text = msm.date.ToString();
            }
            if(msm.totalTime != null)
            {
                TextBox2.Text = msm.totalTime.ToString();
            }
            if(msm.percentage > 0)
            {
                TextBox3.Text = msm.percentage.ToString();
            }
        }



        private void SetPlaceholderText()
        {
            
            TextBox1.ForeColor = SystemColors.GrayText;
            TextBox1.GotFocus += (sender, e) => { if (TextBox1.Text == "Save Date") { TextBox1.Text = ""; TextBox1.ForeColor = SystemColors.WindowText; } };
            TextBox1.LostFocus += (sender, e) => { if (TextBox1.Text == "") { TextBox1.Text = "Save Date"; TextBox1.ForeColor = SystemColors.GrayText; } };

            TextBox2.ForeColor = SystemColors.GrayText;
            TextBox2.GotFocus += (sender, e) => { if (TextBox2.Text == "Total Time") { TextBox2.Text = ""; TextBox2.ForeColor = SystemColors.WindowText; } };
            TextBox2.LostFocus += (sender, e) => { if (TextBox2.Text == "") { TextBox2.Text = "Total Time"; TextBox2.ForeColor = SystemColors.GrayText; } };

            TextBox3.ForeColor = SystemColors.GrayText;
            TextBox3.GotFocus += (sender, e) => { if (TextBox3.Text == "Percentage") { TextBox3.Text = ""; TextBox3.ForeColor = SystemColors.WindowText; } };
            TextBox3.LostFocus += (sender, e) => { if (TextBox3.Text == "") { TextBox3.Text = "Percentage"; TextBox3.ForeColor = SystemColors.GrayText; } };
        }
    }
}
