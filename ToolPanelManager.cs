using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformerEditor
{
    public class ToolPanelManager
    {

        Form1 form;

        public ToolPanelManager(Form1 form) { this.form = form; InitializeComponent(); }

        private void InitializeComponent()
        {
            // Add TabControl to the panel

            Panel panelToolbox = new Panel
            {
                Location = new Point(0, 600),
                Size = new Size(1280, 100),
                BorderStyle = BorderStyle.FixedSingle,
            };
            form.Controls.Add(panelToolbox);

            TabControl tabControl = new TabControl
            {
                Size = new Size(1280, 100),
                Location = new Point(0, 0)
            };
            panelToolbox.Controls.Add(tabControl);

            // Create the first tab
            TabPage tabPage1 = new TabPage("Physical");
            tabPage1.BackColor = Color.White;
            tabControl.TabPages.Add(tabPage1);

            // Create the third tab
            TabPage tabPage2 = new TabPage("Destroyable");
            tabPage2.BackColor = Color.White;
            tabControl.TabPages.Add(tabPage2);

            // Create the third tab
            TabPage tabPage3 = new TabPage("Live");
            tabPage3.BackColor = Color.White;
            tabControl.TabPages.Add(tabPage3);

            // Create the third tab
            TabPage tabPage4 = new TabPage("Interractive Item");
            tabPage4.BackColor = Color.White;
            tabControl.TabPages.Add(tabPage4);

            TabPage tabPage5 = new TabPage("Platforming");
            tabPage5.BackColor = Color.White;
            tabControl.TabPages.Add(tabPage5);

            TabPage tabPage6 = new TabPage("Decorative");
            tabPage6.BackColor = Color.White;
            tabControl.TabPages.Add(tabPage6);



            createPictureToolBoxItem(Color.Blue, new Point(25, 25), false, false, "Static Rectangle", tabPage1);
            createPictureToolBoxItem(Color.Green, new Point(100, 25), true, false, "Static Circle", tabPage1);
            createPictureToolBoxItem(Color.LightBlue, new Point(175, 25), false, false, "Dynamic Rectangle", tabPage1);
            createPictureToolBoxItem(Color.LightGreen, new Point(250, 25), true, false, "Dynamic Circle", tabPage1);
            createPictureToolBoxItem(Color.Red, new Point(1200, 25), false, true, "Remove Element", tabPage1);

            createPictureToolBoxItem(Color.Blue, new Point(25, 25), false,false, "Destroyable Static Rectangle", tabPage2);
            createPictureToolBoxItem(Color.Green, new Point(100, 25), true, false, "Destroyable Static Circle", tabPage2);
            createPictureToolBoxItem(Color.LightBlue, new Point(175, 25), false, false, "Destroyable Dynamic Rectangle", tabPage2);
            createPictureToolBoxItem(Color.LightGreen, new Point(250, 25), true,false, "Destroyable Dynamic Circle", tabPage2);
            createPictureToolBoxItem(Color.Red, new Point(1200, 25), false, true, "Remove Element", tabPage2);

            createPictureToolBoxItem(Color.DarkRed, new Point(25, 25), false, false, "Mob", tabPage3);
            createPictureToolBoxItem(Color.Pink, new Point(100, 25), false, false, "NPC", tabPage3);
            createPictureToolBoxItem(Color.Yellow, new Point(175, 25), false, false, "GroupMember", tabPage3);
            createPictureToolBoxItem(Color.Red, new Point(1200, 25), false, true, "Remove Element", tabPage3);

            createPictureToolBoxItem(Color.DarkOrchid, new Point(25, 25), false, false, "Interractive Item", tabPage4);
            createPictureToolBoxItem(Color.Red, new Point(1200, 25), false, true, "Remove Element", tabPage4);

            createPictureToolBoxItem(Color.SandyBrown, new Point(25, 25), false, false, "Ladder", tabPage5);
            createPictureToolBoxItem(Color.RosyBrown, new Point(100, 25), false, false, "Platform", tabPage5);
            createPictureToolBoxItem(Color.Red, new Point(1200, 25), false, true, "Remove Element", tabPage5);

            createPictureToolBoxItem(Color.LightYellow, new Point(25, 25), false, false, "Decoration", tabPage6);
            createPictureToolBoxItem(Color.Red, new Point(1200, 25), false, true, "Remove Element", tabPage6);
        }



        public void createPictureToolBoxItem(Color color, Point location, bool isRound, bool isRemove, string toolTip, TabPage tabPage)
        {
            PictureBox pictureBoxToolboxItem;

            if (isRound)
            {
                pictureBoxToolboxItem = new OvalPictureBox
                {
                    Size = new Size(50, 50),
                    BackColor = color, // Example color
                    Location = location, // Adjust the location as needed
                };
            }
            else
            {
                pictureBoxToolboxItem = new PictureBox
                {
                    Size = new Size(50, 50),
                    BackColor = color, // Example color
                    Location = location, // Adjust the location as needed
                };
            }
            

            

            if (!isRemove)
            {
                pictureBoxToolboxItem.MouseDown += form.dndh.PictureBoxToolboxItem_MouseDown;
            }
            else
            {
                pictureBoxToolboxItem.MouseDown += form.dndh.RemovePictureBoxToolboxItem_MouseDown;
            }

            pictureBoxToolboxItem.MouseHover += form.dndh.PictureBoxToolboxItem_MouseHover;
            tabPage.Controls.Add(pictureBoxToolboxItem);
            form._toolTooltips[pictureBoxToolboxItem] = toolTip; // Set tooltip text
        }
    }




    public class OvalPictureBox : PictureBox
    {
        public OvalPictureBox()
        {
            this.BackColor = Color.DarkGray;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                this.Region = new Region(gp);
            }
        }
    }
}
