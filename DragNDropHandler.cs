using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PlatformerEditor
{
    public class DragNDropHandler
    {
        Form1 form;
        private GameElement selectedElement;
        public DragNDropHandler(Form1 form) { this.form = form; }

        public void PictureBoxToolboxItem_MouseHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (form._toolTooltips.ContainsKey(pictureBox))
            {
                form.toolTip1.SetToolTip(pictureBox, form._toolTooltips[pictureBox]); // Set the tooltip associated with the tool
            }
        }

       

        public void panelLevel_Paint(object sender, PaintEventArgs e)
        {
            // Draw the grid
            DrawGrid(e.Graphics);

            // Draw game elements
            foreach (var element in form.maps[form.currentMap])
            {
                // Create a brush for filling the element
                using (var brush = new SolidBrush(element.Color))
                {
                    // Save the current graphics state
                    GraphicsState state = e.Graphics.Save();

                    // Translate to the center of the element
                    e.Graphics.TranslateTransform(element.X + element.Width / 2, form.panelLevel.Height - element.Y - element.Height / 2);

                    // Apply rotation
                    e.Graphics.RotateTransform(element.RotationAngle);

                    if (!element.isRound)
                    {
                        e.Graphics.FillRectangle(brush, -element.Width / 2, -element.Height / 2, element.Width, element.Height);
                    }
                    else
                    {
                        e.Graphics.FillEllipse(brush, -element.Width / 2, -element.Height / 2, element.Width, element.Height);
                    }
                    

                    // Restore the graphics state
                    e.Graphics.Restore(state);
                }

                // Check if the current element is the selected one
                if (element == selectedElement)
                {
                    // Draw a border around the selected element
                    using (var pen = new Pen(Color.Red, 2)) // Red color, 2 pixels width
                    {
                        // Save the current graphics state
                        GraphicsState state = e.Graphics.Save();

                        // Translate to the center of the element
                        e.Graphics.TranslateTransform(element.X + element.Width / 2, form.panelLevel.Height - element.Y - element.Height / 2);

                        // Apply rotation
                        e.Graphics.RotateTransform(element.RotationAngle);

                        // Draw the rotated border
                        e.Graphics.DrawRectangle(pen, -element.Width / 2, -element.Height / 2, element.Width, element.Height);

                        // Restore the graphics state
                        e.Graphics.Restore(state);
                    }
                }
            }

            DrawGridNumbers(e.Graphics);
        }


        public void DrawGrid(Graphics g)
        {
            // Define grid line color
            Color gridColor = Color.LightGray;

            // Define grid cell size
            int cellWidth = 20;
            int cellHeight = 20;

            // Calculate the number of horizontal and vertical lines needed
            int horizontalLines = form.panelLevel.Height / cellHeight;
            int verticalLines = form.panelLevel.Width / cellWidth;

            // Draw horizontal grid lines and display coordinates
            for (int i = 0; i <= horizontalLines; i++)
            {
                // Draw horizontal grid line
                g.DrawLine(new Pen(gridColor), 0, form.panelLevel.Height - i * cellHeight, form.panelLevel.Width, form.panelLevel.Height - i * cellHeight);

                // Display y-coordinate on the left side
            }

            // Draw vertical grid lines and display coordinates
            for (int i = 0; i <= verticalLines; i++)
            {
                // Draw vertical grid line
                g.DrawLine(new Pen(gridColor), i * cellWidth, 0, i * cellWidth, form.panelLevel.Height);

                // Display x-coordinate on the top side
            }
        }


        public void DrawGridNumbers(Graphics g) {

            // Define grid cell size
            int cellWidth = 20;
            int cellHeight = 20;

            // Calculate the number of horizontal and vertical lines needed
            int horizontalLines = form.panelLevel.Height / cellHeight;
            int verticalLines = form.panelLevel.Width / cellWidth;

            // Draw horizontal grid lines and display coordinates
            for (int i = 0; i <= horizontalLines; i++)
            {
                // Draw horizontal grid line

                // Display y-coordinate on the left side
                g.DrawString(((i * cellHeight)/10).ToString(), SystemFonts.DefaultFont, Brushes.Black, new PointF(form.viewportLocation.X, form.panelLevel.Height - i * cellHeight));
            }

            // Draw vertical grid lines and display coordinates
            for (int i = 0; i <= verticalLines; i++)
            {
               

                // Display x-coordinate on the top side
                g.DrawString(((i * cellWidth)/10).ToString(), SystemFonts.DefaultFont, Brushes.Black, new PointF(i * cellWidth, form.viewportLocation.Y + 20));
            }

        }


        public void PictureBoxToolboxItem_MouseDown(object sender, MouseEventArgs e)
        {
            var pictureBox = (PictureBox)sender;
            var element = new GameElement { Color = pictureBox.BackColor, Class = form._toolTooltips[pictureBox] };
            element.setDefaults();
            if(pictureBox is OvalPictureBox)
            {
                element.isRound = true;
            }
            else
            {
                element.isRound = false;
            }
            form._draggedElement = element;
            Console.WriteLine("MouseDown: Element dragged");
            pictureBox.DoDragDrop(element, DragDropEffects.Copy);
        }



        public void panelLevel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GameElement)))
            {
                Console.WriteLine("DragEnter: GameElement data present");
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                Console.WriteLine("DragEnter: No GameElement data present");
                e.Effect = DragDropEffects.None;
            }
        }

        public void panelLevel_DragDrop(object sender, DragEventArgs e)
        {
            var element = (GameElement)e.Data.GetData(typeof(GameElement));
            if (element != null)
            {
                // Get the position where the element was dropped
                Point panelMousePosition = form.panelLevel.PointToClient(new Point(e.X, e.Y));

                // Set the element's position to the dropped location
                element.X = panelMousePosition.X;
                element.Y = form.panelLevel.Height - panelMousePosition.Y;

                // Set the default width and height
                int defaultWidth = 20;
                int defaultHeight = 20;


                PictureBox pictureBox = null;

                if (element.isRound)
                {
                    pictureBox = new PictureBox
                    {
                        Size = new Size(defaultWidth, defaultHeight),
                        BackColor = element.Color,
                        Location = new Point(element.X, element.Y)
                    };
                }
                else
                {
                    pictureBox = new OvalPictureBox
                    {
                        Size = new Size(defaultWidth, defaultHeight),
                        BackColor = element.Color,
                        Location = new Point(element.X, element.Y)
                    };
                }
                
                



                // Associate tooltip with the PictureBox representing the dropped element
                form.toolTip1.SetToolTip(pictureBox, element.Class);

                // Add the PictureBox to the panel
                //form.panelLevel.Controls.Add(pictureBox);

                // Update the element's size
                element.Width = defaultWidth;
                element.Height = defaultHeight;

                form.maps[form.currentMap].Add(element); // Add the dropped element to the list
                form.panelLevel.Invalidate(); // Trigger panel redraw
                Console.WriteLine("DragDrop: Element dropped at ({0}, {1})", element.X, element.Y);
            }
            else
            {
                Console.WriteLine("DragDrop: No GameElement data present");
            }
        }

        public void PanelLevel_MouseMove(object sender, MouseEventArgs e)
        {
            if (form._draggedElement != null)
            {
                if (selectedElement != null) // Check if an element is selected
                {

                    if (form.isResizing)
                    {
                        int deltaX = e.X - form.lastMousePosition.X;
                        int deltaY = form.lastMousePosition.Y - e.Y; // Reverse deltaY

                        // Calculate new size
                        int newWidth = Math.Abs(form._draggedElement.Width + deltaX); // Ensure positive width
                        int newHeight = Math.Abs(form._draggedElement.Height + deltaY); // Ensure positive height

                        // Update element's size
                        form._draggedElement.Width = newWidth;
                        form._draggedElement.Height = newHeight;

                        // Update PictureBox size
                        foreach (Control control in form.panelLevel.Controls)
                        {
                            if (control is PictureBox pictureBox && pictureBox.Tag == selectedElement)
                            {
                                pictureBox.Size = new Size(newWidth, newHeight);
                                break;
                            }
                        }

                        form.propertiesForm.UpdateProperties(form._draggedElement);

                        // Redraw panel
                        form.panelLevel.Invalidate();

                        // Update last mouse position
                        form.lastMousePosition = e.Location;
                    }
                    else if (form.isMoving)
                    {
                        int deltaX = e.X - form.lastMousePosition.X;
                        int deltaY = form.lastMousePosition.Y - e.Y; // Reverse deltaY

                        // Calculate new position
                        int newX = form._draggedElement.X + deltaX;
                        int newY = form._draggedElement.Y + deltaY;

                        // Update element's position
                        form._draggedElement.X = newX;
                        form._draggedElement.Y = newY;

                        // Update PictureBox position
                        foreach (Control control in form.panelLevel.Controls)
                        {
                            if (control is PictureBox pictureBox && pictureBox.Tag == selectedElement)
                            {
                                pictureBox.Location = new Point(newX, newY);
                                break;
                            }
                        }

                        form.propertiesForm.UpdateProperties(form._draggedElement);

                        // Redraw panel
                        form.panelLevel.Invalidate();

                        // Update last mouse position
                        form.lastMousePosition = e.Location;
                    }
                }
            }





            // Check if both Ctrl key and left mouse button are down
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.Button == MouseButtons.Left)
            {
                // Calculate the delta movement
                int deltaX = e.X - form.lastMousePosition.X;
                int deltaY = e.Y - form.lastMousePosition.Y;

                // Update the viewport's location based on the delta movement
                form.viewportLocation.X -= deltaX;
                form.viewportLocation.Y -= deltaY;

                // Clamp the viewport location to ensure it stays within bounds
                form.viewportLocation.X = Math.Max(0, Math.Min(form.viewportLocation.X, form.panelLevel.Width - form.ClientSize.Width));
                form.viewportLocation.Y = Math.Max(0, Math.Min(form.viewportLocation.Y, form.panelLevel.Height - form.ClientSize.Height));

                // Set the panel's location to the updated viewport location
                form.panelLevel.Location = new Point(-form.viewportLocation.X, -form.viewportLocation.Y);

                // Update last mouse position
                form.lastMousePosition = e.Location;

                form.panelLevel.Invalidate();
            }





        }


    


        public void PanelLevel_MouseUp(object sender, MouseEventArgs e)
        {

            form.isResizing = false;
            form.isMoving = false;
            //selectedElement = null;
        }

        public void PanelLevel_MouseDown(object sender, MouseEventArgs e)
        {
            bool elementClicked = false;

            // Check if mouse is within bounds of a resizable element
            foreach (var element in form.maps[form.currentMap])
            {
                if (e.Button == MouseButtons.Left &&
                    e.X >= element.X && e.X <= element.X + element.Width &&
                    e.Y >= form.panelLevel.Height - element.Y - element.Height && e.Y <= form.panelLevel.Height - element.Y)
                {
                    // Set the dragged element
                    form._draggedElement = element;
                    form.lastMousePosition = e.Location;
                    form.isResizing = true;
                    elementClicked = true;

                    selectedElement = element;

                    break;
                }
                else if (e.Button == MouseButtons.Right && // Check if right mouse button is clicked
                    e.X >= element.X && e.X <= element.X + element.Width &&
                    e.Y >= form.panelLevel.Height - element.Y - element.Height && e.Y <= form.panelLevel.Height - element.Y)
                {
                    form._draggedElement = element;
                    form.lastMousePosition = e.Location;
                    form.isMoving = true;
                    elementClicked = true;

                    selectedElement = element;

                    break;

                }
            }

            if (!elementClicked)
            {
                // Unselect element
                form.isResizing = false;
                form.isMoving = false;
                form._draggedElement = null;
                selectedElement = null; // Clear selected element
                form.propertiesForm.ClearProperties(); // Clear properties form
                for (int i = 0; i < form.propertiesForm.textBoxes.Count; i++)
                {
                    form.propertiesForm.textBoxes[i].Visible = false;
                    form.propertiesForm.labels[i].Visible = false;
                }
            }
        }



        public void forms_KeyDown(object sender, KeyEventArgs e)
        {


            // Check if an element is selected
            if (selectedElement != null)
            {

                // Get the rotation angle increment (in degrees)
                int rotationIncrement = 5; // Adjust this value as needed

                // Check which arrow key is pressed and update the rotation angle accordingly
                switch (e.KeyCode)
                {
                    case Keys.E:
                        selectedElement.RotationAngle -= rotationIncrement;
                        form.propertiesForm.UpdateProperties(form._draggedElement);
                        e.SuppressKeyPress = true;


                        break;
                    case Keys.Q:
                        selectedElement.RotationAngle += rotationIncrement;
                        form.propertiesForm.UpdateProperties(form._draggedElement);
                        e.SuppressKeyPress = true;
                        break;

                }

                // Redraw the panel to reflect the updated rotation
                form.panelLevel.Invalidate();
            }
        }



        public void RemovePictureBoxToolboxItem_MouseDown(object sender, MouseEventArgs e)
        {
            // Remove the selected element from the panel and the maps[form.currentMap] list
            if (form._draggedElement != null)
            {
                // Remove the PictureBox associated with the dragged element
                PictureBox pictureBoxToRemove = GetPictureBox(form._draggedElement);
                if (pictureBoxToRemove != null)
                {
                    form.panelLevel.Controls.Remove(pictureBoxToRemove);
                }

                // Remove the dragged element from the maps[form.currentMap] list
                form.maps[form.currentMap].Remove(form._draggedElement);

                // Clear the dragged element
                form._draggedElement = null;

                // Redraw the panel
                form.panelLevel.Invalidate();
            }
        }




        public PictureBox GetPictureBox(GameElement element)
        {
            foreach (Control control in form.panelLevel.Controls)
            {
                if (control is PictureBox pictureBox && pictureBox.Tag == element)
                {
                    return pictureBox;
                }
            }
            return null;
        }






    }
}
