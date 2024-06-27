using PlatformerEditor;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public partial class ElementPropertiesForm : Form
{

    public List<TextBox> textBoxes;
    public List<Label> labels;
    private Button buttonOK;
    private ColorDialog colorDialog;
    public GameElement element;
    public Form1 form;
    public Button buttonWeapons, buttonInventory;

    // Constructor
    public ElementPropertiesForm(Form1 form)
    {
        this.form = form;
        InitializeComponent();
        Load += SetLocationRelativeToMainForm;


    }

    private void SetLocationRelativeToMainForm(object sender, EventArgs e)
    {
        StartPosition = FormStartPosition.Manual;
        int offsetX = -10; // Adjust as needed
        int x = form.Location.X + form.Width + offsetX;
        this.Location = new Point(x, 30);
    }

    public void showControls(GameElement element)
    {
        this.element = element;
        InitializeControls(element);
    }

    private void InitializeComponent()
    {
        // Initialize form properties
        this.SuspendLayout();
        this.ClientSize = new System.Drawing.Size(300, 890); // Adjust the size as needed
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Element Properties";

        textBoxes = new List<TextBox>();
        labels = new List<Label>();

        string[] labelTexts = { "X:", "Y:", "Width:", "Height:", "Rotation:", "Class:", "IsStatic:", "IsCollidableWithLive:", "MaxHP", "CurrentHP", "Armor", "MobType", "Level", "Speed", "Jump", "Flight", "BodyDamage", "KnockBackStrength", "EventID", "MemberName", "GroupClass", "IsPlayer", "AgroRange", "UnAgroRange" };


        for (int i = 0; i < labelTexts.Length; i++)
        {

            Label label = new Label { Text = labelTexts[i], Location = new Point(20, 20 + i * 30), AutoSize = true };
            
            labels.Add(label);

            TextBox textBox = new TextBox { Location = new Point(100, 20 + i * 30), Size = new Size(170, 20) };
            
            textBoxes.Add(textBox);

            textBox.Visible = false;
            label.Visible = false;
        }

        textBoxes[5].Enabled = false;

        // Add controls to the form
        this.Controls.AddRange(textBoxes.ToArray());
        this.Controls.AddRange(labels.ToArray());


        buttonWeapons = new Button { Text = "Weapons", Location = new Point(100, 750), Size = new Size(170, 20) };
        buttonWeapons.Click += buttonWeapons_Click;
        buttonWeapons.Visible = false;
        this.Controls.Add(buttonWeapons);

        buttonInventory = new Button { Text = "Inventory", Location = new Point(100, 780), Size = new Size(170, 20) };
        buttonInventory.Click += buttonInventory_Click;
        buttonInventory.Visible = false;
        this.Controls.Add(buttonInventory);



        // Initialize Buttons
        buttonOK = new Button { Text = "Apply", Location = new Point(100, 850), };
        // Subscribe buttons to click events
        buttonOK.Click += buttonOK_Click;

        // Initialize ColorDialog
        colorDialog = new ColorDialog();

        this.Controls.Add(buttonOK);

        this.ResumeLayout(false);
        this.PerformLayout();
    }



    private void InitializeControls(GameElement element)
    {

    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        // Validate the user input and assign it to the properties
        if (ValidateInput(textBoxes[0].Text, out int x)) ;
        if (ValidateInput(textBoxes[1].Text, out int y)) ;
        if (ValidateInput(textBoxes[2].Text, out int width)) ;
        if (ValidateInput(textBoxes[3].Text, out int height)) ;
        if (ValidateInput(textBoxes[4].Text, out int rotationAngle)) ;
        if (ValidateInput(textBoxes[8].Text, out int maxHp)) ;
        if (ValidateInput(textBoxes[9].Text, out int currHP)) ;
        if (ValidateInput(textBoxes[10].Text, out int armor)) ;
        if (ValidateInput(textBoxes[12].Text, out int level)) ;
        if (ValidateInput(textBoxes[13].Text, out int speed)) ;
        if (ValidateInput(textBoxes[14].Text, out int jump)) ;
        if (ValidateInput(textBoxes[15].Text, out int flyForce)) ;
        if (ValidateInput(textBoxes[16].Text, out int bodyDamage)) ;
        if (ValidateInput(textBoxes[17].Text, out int knockBack)) ;
        if (ValidateInput(textBoxes[18].Text, out int eventID)) ;
        if (ValidateInput(textBoxes[22].Text, out int agroRange)) ;
        if (ValidateInput(textBoxes[23].Text, out int unAgroRange)) ;


        bool isStatic = false;
        bool isCollidableWithLive = false;
        bool isPlayer = false;
        // Parse boolean values
        if (textBoxes[6].Text == "True" || textBoxes[6].Text == "False")
        {
            isStatic = bool.Parse(textBoxes[6].Text);
        }
        if (textBoxes[7].Text == "True" || textBoxes[7].Text == "False")
        {
            isCollidableWithLive = bool.Parse(textBoxes[7].Text);
        }
        if (textBoxes[21].Text == "True" || textBoxes[21].Text == "False")
        {
            isPlayer = bool.Parse(textBoxes[21].Text);
        }

        
        

        // Update the selected element
        if (element != null)
        {
            element.X = x;
            element.Y = y;
            element.Width = width;
            element.Height = height;
            element.RotationAngle = rotationAngle;
            element.IsStatic = isStatic;
            element.IsCollidableWithLive = isCollidableWithLive;
            element.MaxHP = maxHp > 0 ? maxHp : 0;
            element.CurrentHP = currHP > 0 ? currHP : 0;
            element.Armor = armor > 0 ? armor : 0;
            element.MobType = textBoxes[11].Text;
            element.Level = level > 0 ? level : 0;
            element.Speed = speed > 0 ? speed : 0;
            element.JumpForce = jump > 0 ? jump : 0;
            element.FlyForce = flyForce > 0 ? flyForce : 0;
            element.BodyDamage = bodyDamage > 0 ? bodyDamage : 0;
            element.KnockbackStrength = knockBack > 0 ? knockBack : 0;
            element.EventID = eventID > -1 ? eventID : -1;
            element.Name = textBoxes[19].Text;
            element.GroupMemberClass = textBoxes[20].Text;
            element.IsPlayer = isPlayer;
            element.AgroRange = agroRange;
            element.UnAgroRange = unAgroRange;
        }

        // Redraw panel
        form.panelLevel.Invalidate();



    }

    private bool ValidateInput(string input, out int result)
    {
        return int.TryParse(input, out result);
    }


    public void UpdateProperties(GameElement element)
    {
        for (int i = 0; i < 5; i++)
        {
            SetVisibility(i, true);
        }
        SetVisibility(5, !string.IsNullOrEmpty(element.Class));
        SetVisibility(6, element.IsStatic != null);
        SetVisibility(7, element.IsCollidableWithLive != null);
        SetVisibility(8, element.MaxHP > 0);
        SetVisibility(9, element.CurrentHP > 0);
        SetVisibility(10, element.Armor > 0);
        SetVisibility(11, !string.IsNullOrEmpty(element.MobType));
        SetVisibility(12, element.Level > 0);
        SetVisibility(13, element.Speed > 0);
        SetVisibility(14, element.JumpForce > 0);
        SetVisibility(15, element.FlyForce > 0);
        SetVisibility(16, element.BodyDamage > 0);
        SetVisibility(17, element.KnockbackStrength > 0);
        SetVisibility(18, element.Class == "Interractive Item");
        SetVisibility(19, !string.IsNullOrEmpty(element.Name));
        SetVisibility(20, !string.IsNullOrEmpty(element.GroupMemberClass));
        SetVisibility(21, element.Class == "GroupMember");
        SetVisibility(22, element.Class == "Mob");
        SetVisibility(23, element.Class == "Mob");
        if(element.Class == "Mob" || element.Class == "NPC" || element.Class == "GroupMember")
        {
            buttonWeapons.Visible = true;
            buttonInventory.Visible = true;
        }
        else
        {
            buttonWeapons.Visible = false;
            buttonInventory.Visible = false;
        }
        

        textBoxes[0].Text = element.X > 0 ? element.X.ToString() : "";
        textBoxes[1].Text = element.Y > 0 ? element.Y.ToString() : "";
        textBoxes[2].Text = element.Width > 0 ? element.Width.ToString() : "";
        textBoxes[3].Text = element.Height > 0 ? element.Height.ToString() : "";
        textBoxes[4].Text = element.RotationAngle.ToString();
        textBoxes[5].Text = element.Class;
        textBoxes[6].Text = element.IsStatic.ToString();
        textBoxes[7].Text = element.IsCollidableWithLive.ToString();
        textBoxes[8].Text = element.MaxHP > 0 ? element.MaxHP.ToString() : "";
        textBoxes[9].Text = element.CurrentHP > 0 ? element.CurrentHP.ToString() : "";
        textBoxes[10].Text = element.Armor > 0 ? element.Armor.ToString() : "";
        textBoxes[11].Text = element.MobType;
        textBoxes[12].Text = element.Level > 0 ? element.Level.ToString() : "";
        textBoxes[13].Text = element.Speed > 0 ? element.Speed.ToString() : "";
        textBoxes[14].Text = element.JumpForce > 0 ? element.JumpForce.ToString() : "";
        textBoxes[15].Text = element.FlyForce > 0 ? element.FlyForce.ToString() : "";
        textBoxes[16].Text = element.BodyDamage > 0 ? element.BodyDamage.ToString() : "";
        textBoxes[17].Text = element.KnockbackStrength > 0 ? element.KnockbackStrength.ToString() : "";
        textBoxes[18].Text = element.EventID > -1 ? element.EventID.ToString() : "";
        textBoxes[19].Text = element.Name;
        textBoxes[20].Text = element.GroupMemberClass;
        textBoxes[21].Text = element.IsPlayer.ToString();
        textBoxes[22].Text = element.AgroRange.ToString();
        textBoxes[23].Text = element.UnAgroRange.ToString();






        this.element = element;
    }

    private void SetVisibility(int index, bool isVisible)
    {
        textBoxes[index].Visible = isVisible;
        labels[index].Visible = isVisible;

    }

    public void ClearProperties()
    {
        foreach (TextBox textBox in textBoxes)
        {
            textBox.Text = "";
        }
        buttonWeapons.Visible = false;
        buttonInventory.Visible = false;
    }





    private void buttonInventory_Click(object sender, EventArgs e)
    {
        InventoryPopupForm inventoryForm = new InventoryPopupForm(0, element);
        inventoryForm.ShowDialog();
    }

    private void buttonWeapons_Click(object sender, EventArgs e)
    {
        InventoryPopupForm weaponsForm = new InventoryPopupForm(1, element);
        weaponsForm.ShowDialog();
    }

}

public partial class InventoryPopupForm : Form
{
    public Panel panelInventory;
    private Button applyButton;
    private int id;
    public GameElement element;


    public InventoryPopupForm(int id, GameElement element)
    {
        this.id = id;
        this.element = element;
        this.ClientSize = new System.Drawing.Size(500, 400);
        InitializePanel();
        AddPictureBoxesToPanel();
        AddButtons();

    }

    private void InitializePanel()
    {
        panelInventory = new Panel();
        panelInventory.Location = new Point(10, 10);
        panelInventory.Size = new Size(500, 300); // Adjust size as needed
        panelInventory.BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(panelInventory);
    }

    public void AddPictureBoxesToPanel()
    {


        

        if(element != null && (element.Class == "GroupMember" || element.Class == "Mob" || element.Class == "NPC"))
        {

            int rows = 0; int cols = 0;

            if (this.id == 1 && element.Weapons != null)
            {
                rows = element.Weapons.Count; cols = 1;
            }
            else if(this.id == 0 && element.Inventory != null)
            {
                rows = element.Inventory.Count; cols = 1;
            }



            int pictureBoxSize = 50; // Adjust size as needed
            int spacing = 10; // Adjust spacing as needed

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(pictureBoxSize, pictureBoxSize);
                    pictureBox.Location = new Point(col * (pictureBoxSize + spacing), row * (pictureBoxSize + spacing));
                    pictureBox.BorderStyle = BorderStyle.FixedSingle; // Optional
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Adjust size mode as needed

                    // Add click event handler
                    pictureBox.Click += PictureBox_Click;

                    // Add any event handlers or other properties as needed

                    panelInventory.Controls.Add(pictureBox);
                }
            }

        }
       

        
    }

    private void AddButtons()
    {

        applyButton = new Button();
        applyButton.Text = "Add";
        applyButton.Location = new Point(10, 320); // Adjust position as needed
        applyButton.Click += AddButton_Click;
        Controls.Add(applyButton);

        
    }

    private void AddButton_Click(object sender, EventArgs e)
    {


            // Check if the clicked PictureBox corresponds to an inventory item or a weapon
            if (id == 0)
            {
                // Retrieve the corresponding inventory item
                InventoryItem inventoryItem = new InventoryItem();

                // Create a new instance of the PicturePopupForm for the inventory item
                PicturePopupForm popupForm = new PicturePopupForm(inventoryItem, null, this, 1);

                // Show the popup form
                popupForm.ShowDialog();
            }
            else if (id == 1)
            {
            // Retrieve the corresponding weapon
                Weapon weapon = new Weapon();

                // Create a new instance of the PicturePopupForm for the weapon
                PicturePopupForm popupForm = new PicturePopupForm(null, weapon, this, 1);

                // Show the popup form
                popupForm.ShowDialog();
            }
        
    }


    private void PictureBox_Click(object sender, EventArgs e)
    {
        // Get the PictureBox that triggered the event
        PictureBox pictureBox = sender as PictureBox;
        if (pictureBox != null)
        {
            // Get the index of the clicked PictureBox
            int pictureBoxIndex = panelInventory.Controls.IndexOf(pictureBox);

            // Check if the clicked PictureBox corresponds to an inventory item or a weapon
            if (id == 0 && pictureBoxIndex < element.Inventory.Count)
            {
                // Retrieve the corresponding inventory item
                InventoryItem inventoryItem = element.Inventory[pictureBoxIndex];

                // Create a new instance of the PicturePopupForm for the inventory item
                PicturePopupForm popupForm = new PicturePopupForm(inventoryItem, null, this, 0);

                // Show the popup form
                popupForm.ShowDialog();
            }
            else if (id == 1 && pictureBoxIndex < element.Weapons.Count)
            {
                // Retrieve the corresponding weapon
                Weapon weapon = element.Weapons[pictureBoxIndex];

                // Create a new instance of the PicturePopupForm for the weapon
                PicturePopupForm popupForm = new PicturePopupForm(null, weapon, this, 0);

                // Show the popup form
                popupForm.ShowDialog();
            }
        }
    }


}





public partial class PicturePopupForm : Form
{

    private List<Label> labels;
    private List<TextBox> textBoxes;
    private Button applyButton;
    private InventoryItem inventoryItem;
    private Weapon weapon;
    private InventoryPopupForm form;
    private int id;
    public PicturePopupForm(InventoryItem inventoryItem, Weapon weapon, InventoryPopupForm form, int id)
    {
        this.id = id;
        InitializeControls();
        this.ClientSize = new System.Drawing.Size(300, 500);
        // Store the inventory item or weapon
        this.inventoryItem = inventoryItem;
        this.weapon = weapon;
        this.form = form;
        
        // Update the text boxes with the inventory item or weapon details
        UpdateTextBoxes();
    }


    private void InitializeControls()
    {
        textBoxes = new List<TextBox>();
        labels = new List<Label>();

        string[] labelTexts = { "Item Name:", "Value:", "IsStackable:", "Amount:", "CurrentAmount:", "IsReloading:", "BulletDelay:", "BulletLifeCount:", "ReloadSpeed:", "Spray:" };


        for (int i = 0; i < labelTexts.Length; i++)
        {

            Label label = new Label { Text = labelTexts[i], Location = new Point(20, 20 + i * 30), AutoSize = true };
            labels.Add(label);

            TextBox textBox = new TextBox { Location = new Point(100, 20 + i * 30), Size = new Size(170, 20) };
            textBoxes.Add(textBox);
        }

        this.Controls.AddRange(textBoxes.ToArray());
        this.Controls.AddRange(labels.ToArray());
        // Initialize Apply button
        applyButton = new Button();
        applyButton.Text = "Change";
        applyButton.Location = new Point(10, 450); // Adjust position as needed
        applyButton.Click += ApplyButton_Click;
        if(id == 0)
        {
            Controls.Add(applyButton);
        }
        

        applyButton = new Button();
        applyButton.Text = "Remove";
        applyButton.Location = new Point(110, 450); // Adjust position as needed
        applyButton.Click += RemoveButton_Click;
        if (id == 0)
        {
            Controls.Add(applyButton);
        }

        applyButton = new Button();
        applyButton.Text = "Add";
        applyButton.Location = new Point(210, 450); // Adjust position as needed
        applyButton.Click += AddButton_Click;
        if (id == 1)
        {
            Controls.Add(applyButton);
        }


    }
    private void UpdateTextBoxes()
    {
        // Update text boxes with inventory item or weapon details
        if (inventoryItem != null)
        {
            textBoxes[0].Text = inventoryItem.Name;
            textBoxes[1].Text = inventoryItem.Value.ToString();
            textBoxes[2].Text = inventoryItem.IsStackable.ToString();
            textBoxes[3].Text = inventoryItem.Amount.ToString();

            labels[1].Text = "Value";
            labels[2].Text = "IsStackable";
            labels[3].Text = "Amount";


            for (int i = 4; i < 10; i++)
            {
                textBoxes[i].Visible = false;
                labels[i].Visible = false;
            }
        }
        else if (weapon != null)
        {
            for (int i = 4; i < 10; i++)
            {
                textBoxes[i].Visible = true;
                labels[i].Visible = true;
            }
            textBoxes[0].Text = weapon.Name;
            textBoxes[1].Text = weapon.Damage.ToString();
            textBoxes[2].Text = weapon.BulletSpeed.ToString();
            textBoxes[3].Text = weapon.AmmotAmount.ToString();
            textBoxes[4].Text = weapon.CurrentAmmoAmoint.ToString();
            textBoxes[5].Text = weapon.IsReloading.ToString();
            textBoxes[6].Text = weapon.BulletDelay.ToString();
            textBoxes[7].Text = weapon.BulletLifeCount.ToString();
            textBoxes[8].Text = weapon.ReloadSpeed.ToString();
            textBoxes[9].Text = weapon.Spray.ToString();

            labels[1].Text = "Damage";
            labels[2].Text = "BulletSpeed";
            labels[3].Text = "AmmoAmount";

        }
    }
    private void ApplyButton_Click(object sender, EventArgs e)
    {
        // Apply changes to the inventory item or weapon
        if (inventoryItem != null)
        {
            // Update inventory item properties
            inventoryItem.Name = textBoxes[0].Text;
            inventoryItem.Value = Convert.ToInt32(textBoxes[1].Text);
            inventoryItem.IsStackable = Convert.ToBoolean(textBoxes[2].Text);
            inventoryItem.Amount = Convert.ToInt32(textBoxes[3].Text);
        }
        else if (weapon != null)
        {
            // Update weapon properties
            weapon.Name = textBoxes[0].Text;
            weapon.Damage = Convert.ToInt32(textBoxes[1].Text);
            weapon.BulletSpeed = Convert.ToInt32(textBoxes[2].Text);
            weapon.AmmotAmount = Convert.ToInt32(textBoxes[3].Text);
            weapon.CurrentAmmoAmoint = Convert.ToInt32(textBoxes[4].Text);
            weapon.IsReloading = Convert.ToBoolean(textBoxes[5].Text);
            weapon.BulletDelay = Convert.ToInt32(textBoxes[6].Text);
            weapon.BulletLifeCount = Convert.ToInt32(textBoxes[7].Text);
            weapon.ReloadSpeed = Convert.ToInt32(textBoxes[8].Text);
            weapon.Spray = Convert.ToSingle(textBoxes[9].Text); // Convert to float
        }

        // Close the form
        this.Close();
    }


    private void RemoveButton_Click(object sender, EventArgs e)
    {
        // Remove the item from the element's inventory or weapons list
        if (inventoryItem != null)
        {
            // Assuming element.Inventory is a list of InventoryItem objects
            if (form.element.Inventory.Contains(inventoryItem))
            {
                form.element.Inventory.Remove(inventoryItem);
            }
        }
        else if (weapon != null)
        {
            // Assuming element.Weapons is a list of Weapon objects
            if (form.element.Weapons.Contains(weapon))
            {
                form.element.Weapons.Remove(weapon);
            }
        }

        // Clear existing PictureBoxes from the panel
        form.panelInventory.Controls.Clear();

        // Add new PictureBoxes based on the updated inventory/weapon list
        form.AddPictureBoxesToPanel();

        // Close the form
        this.Close();
    }



    private void AddButton_Click(object sender, EventArgs e)
    {
        // Check if the form is handling inventory items or weapons
        if (inventoryItem != null)
        {
            // Create a new InventoryItem
            InventoryItem newItem = new InventoryItem();

            // Update properties based on user input
            newItem.Name = textBoxes[0].Text;
            newItem.Value = Convert.ToInt32(textBoxes[1].Text);
            newItem.IsStackable = Convert.ToBoolean(textBoxes[2].Text);
            newItem.Amount = Convert.ToInt32(textBoxes[3].Text);

            // Add the new item to the inventory
            form.element.Inventory.Add(newItem);
        }
        else if (weapon != null)
        {
            // Create a new Weapon
            Weapon newWeapon = new Weapon();

            // Update properties based on user input
            newWeapon.Name = textBoxes[0].Text;
            newWeapon.Damage = Convert.ToInt32(textBoxes[1].Text);
            newWeapon.BulletSpeed = Convert.ToInt32(textBoxes[2].Text);
            newWeapon.AmmotAmount = Convert.ToInt32(textBoxes[3].Text);
            newWeapon.CurrentAmmoAmoint = Convert.ToInt32(textBoxes[4].Text);
            newWeapon.IsReloading = Convert.ToBoolean(textBoxes[5].Text);
            newWeapon.BulletDelay = Convert.ToInt32(textBoxes[6].Text);
            newWeapon.BulletLifeCount = Convert.ToInt32(textBoxes[7].Text);
            newWeapon.ReloadSpeed = Convert.ToInt32(textBoxes[8].Text);
            newWeapon.Spray = Convert.ToSingle(textBoxes[9].Text); // Convert to float

            // Add the new weapon to the list
            form.element.Weapons.Add(newWeapon);
        }

        // Refresh the panel with the updated items
        form.panelInventory.Controls.Clear();
        form.AddPictureBoxesToPanel();

        // Close the form
        this.Close();
    }


}
