using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace PlatformerEditor
{

    // In your form class
    public partial class Form1 : Form
    {

        public GameElement _draggedElement;
        public Panel panelLevel;
        public ToolTip toolTip1 = new ToolTip();
        public Dictionary<PictureBox, string> _toolTooltips = new Dictionary<PictureBox, string>();
        public DragNDropHandler dndh;
        public IOManager im;
        public ElementPropertiesForm propertiesForm;
        public MenuStripManager msm;
        public ToolPanelManager tpm;

        public bool isResizing = false;
        public bool isMoving = false;
        public Point lastMousePosition;

        public Point viewportLocation = Point.Empty;
        public Size panelLevelViewPortSize = new Size(1200, 760);

        public int amountOfMaps = 10;
        public List<string> mapNames = new List<string>();
        public int currentMap = 0;
        public List<List<GameElement>> maps = new List<List<GameElement>>();
        

        public Form1()
        {
            InitializeMaps();
            dndh = new DragNDropHandler(this);
            im = new IOManager(this);
            InitializeComponent();
            Load += Form1_Load;
            propertiesForm = new ElementPropertiesForm(this);
            propertiesForm.Show();
            this.panelLevel.Invalidate();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 30);
        }

        private void InitializeComponent()
        {

            this.Location = new Point(0, 0);
            this.ClientSize = new Size(1280, 890);

            

            msm = new MenuStripManager(this);
            tpm = new ToolPanelManager(this);
            




            panelLevel = new DoubleBufferedPanel
            {
                Location = new Point(0, 30),
                Size = new Size(2000, 2000),
                BorderStyle = BorderStyle.FixedSingle,
            };
            panelLevel.AllowDrop = true;
            panelLevel.Paint += dndh.panelLevel_Paint; // Attach the Paint event handler
            panelLevel.DragEnter += dndh.panelLevel_DragEnter;
            panelLevel.DragDrop += dndh.panelLevel_DragDrop;
            Controls.Add(panelLevel);

            panelLevel.Focus();


            panelLevel.MouseMove += dndh.PanelLevel_MouseMove;
            panelLevel.MouseUp += dndh.PanelLevel_MouseUp;
            panelLevel.MouseDown += dndh.PanelLevel_MouseDown;
            this.KeyPreview = true;
            this.KeyDown += dndh.forms_KeyDown;
        }



        private void InitializeMaps()
        {

            for (int i = 0; i < amountOfMaps; i++)
            {
                List<GameElement> map1 = new List<GameElement>();
                maps.Add(map1);
                mapNames.Add("Empty Name");
            }

        }

    }


    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            // Enable double buffering
            DoubleBuffered = true;
        }
    }


}
