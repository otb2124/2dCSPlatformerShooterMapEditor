using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PlatformerEditor
{
    public class IOManager
    {
        Form1 form;
        public IOManager(Form1 form) { this.form = form; }

        public void SaveMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Save JSON File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveToJson(saveFileDialog.FileName);
            }
        }

        public void LoadMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.Title = "Load JSON File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFromJson(openFileDialog.FileName);
            }
        }

        public void SaveToJson(string filePath)
        {
            // Create a SaveData object to represent your JSON structure
            SaveData saveData = new SaveData
            {
                saveDate = form.msm.date,
                inGameTime = form.msm.totalTime,
                gameProgress = new GameProgress { percentage = form.msm.percentage },
                mapInfo = new MapInfo
                {
                    currentMap = -1,
                    
                    maps = new List<MapData>()
                }
            };

            int index = 0;
            foreach (var gameElements in form.maps)
            {
                MapData mapData = new MapData
                {
                    name = form.mapNames[index],

                    setData = gameElements.Where(element => element.Class == "Static Rectangle" || element.Class == "Static Circle" || element.Class == "Dynamic Rectangle" || element.Class == "Dynamic Circle" || element.Class == "Decoration" || element.Class == "Ladder" || element.Class == "Platform").ToList(),
                    updatableData = gameElements.Where(element => element.Class.Contains("Destroyable") || element.Class == "Mob" || element.Class == "NPC" || element.Class == "Interractive Item" || element.Class == "GroupMember").ToList(),
                };

                saveData.mapInfo.maps.Add(mapData);

                if (mapData.updatableData.Any())
                {
                    for (int i = 0; i < mapData.updatableData.Count; i++)
                    {
                        if (mapData.updatableData[i].IsPlayer == true)
                        {
                            if(saveData.mapInfo.currentMap == -1)
                            {
                                saveData.mapInfo.currentMap = index;
                            }
                            else
                            {
                                MessageBox.Show($"Error: player dublicate detected. MapID of second found isPlayer: {saveData.mapInfo.currentMap} . CurrentMap set to first found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                    }
                    
                }

                index++;
            }

            // Serialize SaveData object to JSON and save to file
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public void LoadFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    // Read JSON from file
                    string json = File.ReadAllText(filePath);
                    // Deserialize JSON to SaveData object
                    SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);

                    // Use saveData object to populate the form
                    form.msm.date = saveData.saveDate;
                    form.msm.totalTime = saveData.inGameTime;
                    form.msm.percentage = saveData.gameProgress.percentage;
                    form.currentMap = saveData.mapInfo.currentMap;

                    // Clear existing maps
                    form.maps.Clear();

                    // Populate maps with loaded data
                    foreach (var mapData in saveData.mapInfo.maps)
                    {
                        List<GameElement> gameElements = new List<GameElement>();
                        gameElements.AddRange(mapData.setData);
                        gameElements.AddRange(mapData.updatableData);

                        form.maps.Add(gameElements);
                    }

                    form.panelLevel.Invalidate();
                    // Refresh the form or any other necessary operations
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("JSON file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
