using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformerEditor
{
    public class MenuStripManager
    {

        public Form1 form;
        public string date;
        public string totalTime;
        public float percentage;
        

        public MenuStripManager(Form1 form) { this.form = form; InitializeComponent(); }

        private void InitializeComponent()
        {
            FlowLayoutPanel menuPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Height = 30 // Adjust the height as needed
            };

            // Create the first menu strip for file operations
            MenuStrip fileMenuStrip = new MenuStrip
            {
                BackColor = Color.Transparent
            };
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem saveMenuItem = new ToolStripMenuItem("Save");
            ToolStripMenuItem loadMenuItem = new ToolStripMenuItem("Load");

            // Add click event handlers for save and load menu items
            saveMenuItem.Click += form.im.SaveMenuItem_Click;
            loadMenuItem.Click += form.im.LoadMenuItem_Click;

            // Add menu items to the file menu
            fileMenu.DropDownItems.Add(saveMenuItem);
            fileMenu.DropDownItems.Add(loadMenuItem);

            // Add the file menu to the menu strip
            fileMenuStrip.Items.Add(fileMenu);

            // Set the file menu strip as the form's menu
            form.MainMenuStrip = fileMenuStrip;
            menuPanel.Controls.Add(fileMenuStrip);

            MenuStrip mapMenuStrip = new MenuStrip
            {
                BackColor = Color.Transparent
            };
            ToolStripMenuItem mapMenu = new ToolStripMenuItem("Map");
            ToolStripMenuItem chooseItem = new ToolStripMenuItem("Choose");
            ToolStripMenuItem mapInfoItem = new ToolStripMenuItem("Information");

            // Add click event handler for information menu item
            chooseItem.Click += ChooseMap_Click;

            mapInfoItem.Click += MapInfo_Click;

            // Add menu item to the edit menu
            mapMenu.DropDownItems.Add(chooseItem);
            mapMenu.DropDownItems.Add(mapInfoItem);

            // Add the edit menu to the menu strip
            mapMenuStrip.Items.Add(mapMenu);

            // Add the edit menu strip below the file menu strip
            menuPanel.Controls.Add(mapMenuStrip);



            // Create the second menu strip for edit operations
            MenuStrip editMenuStrip = new MenuStrip
            {
                BackColor = Color.Transparent
            };
            ToolStripMenuItem editMenu = new ToolStripMenuItem("General");
            ToolStripMenuItem informationMenuItem = new ToolStripMenuItem("Information");

            // Add click event handler for information menu item
            informationMenuItem.Click += InformationMenuItem_Click;

            // Add menu item to the edit menu
            editMenu.DropDownItems.Add(informationMenuItem);

            // Add the edit menu to the menu strip
            editMenuStrip.Items.Add(editMenu);

            // Add the edit menu strip below the file menu strip
            menuPanel.Controls.Add(editMenuStrip);

            // Add the menu panel to the form
            form.KeyPreview = true;
            form.Controls.Add(menuPanel);
        }

        public void InformationMenuItem_Click(object sender, EventArgs e)
        {
            using (InformationInputForm inputForm = new InformationInputForm(this))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve data from the input form
                    date = inputForm.TextBox1.Text;
                    totalTime = inputForm.TextBox2.Text;
                    percentage = float.Parse(inputForm.TextBox3.Text);
                }
            }
        }

        public void ChooseMap_Click(object sender, EventArgs e)
        {
            ChooseMapForm inputForm = new ChooseMapForm(this, 0);
            inputForm.Show();
        }

        public void MapInfo_Click(object sender, EventArgs e)
        {
            ChooseMapForm inputForm = new ChooseMapForm(this, 1);
            inputForm.Show();
        }
    }






    partial class ChooseMapForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStripManager msm;
        private int id;

        public ChooseMapForm(MenuStripManager msm, int id)
        {
            this.id = id;
            this.msm = msm;
            InitializeComponent();
            InitializeByIndex();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mapIndexLabel = new System.Windows.Forms.Label();
            this.mapIndexTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mapIndexLabel
            // 
            this.mapIndexLabel.AutoSize = true;
            this.mapIndexLabel.Location = new System.Drawing.Point(12, 15);
            this.mapIndexLabel.Name = "mapIndexLabel";
            this.mapIndexLabel.Size = new System.Drawing.Size(67, 15);
            this.mapIndexLabel.TabIndex = 0;
            this.mapIndexLabel.Text = "Map Index:";
            // 
            // mapIndexTextBox
            // 
            this.mapIndexTextBox.Location = new System.Drawing.Point(85, 12);
            this.mapIndexTextBox.Name = "mapIndexTextBox";
            this.mapIndexTextBox.Size = new System.Drawing.Size(100, 23);
            this.mapIndexTextBox.TabIndex = 1;
            if(msm.form.currentMap > -1)
            {
                mapIndexTextBox.Text = msm.form.currentMap.ToString();
            }
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(66, 45);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ChooseMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 80);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.mapIndexTextBox);
            this.Controls.Add(this.mapIndexLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseMapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label mapIndexLabel;
        private System.Windows.Forms.TextBox mapIndexTextBox;
        private System.Windows.Forms.Button okButton;


        public void InitializeByIndex()
        {
            if(id == 1)
            {
                this.mapIndexLabel.Text = "Map Name";
                this.mapIndexTextBox.Text = msm.form.mapNames[msm.form.currentMap];
            }
            else
            {

            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(id == 0)
            {
                if (int.TryParse(mapIndexTextBox.Text, out int mapIndex))
                {
                    msm.form.currentMap = mapIndex;
                    msm.form.panelLevel.Invalidate();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Please enter a valid integer for the map index.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                msm.form.mapNames[msm.form.currentMap] = mapIndexTextBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            
        }
    }
}
