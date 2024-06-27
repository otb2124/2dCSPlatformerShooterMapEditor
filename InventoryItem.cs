using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEditor
{
    public class InventoryItem
    {

        public float Value { get; set; }
        public bool IsStackable { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
    }
}
