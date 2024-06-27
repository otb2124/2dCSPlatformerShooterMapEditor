using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEditor
{
    public class Weapon
    {
        public string Name { get; set; }
        public float Damage { get; set; }
        public float BulletSpeed { get; set; }
        public float AmmotAmount { get; set; }
        public float CurrentAmmoAmoint { get; set; }
        public bool IsReloading { get; set; }
        public float BulletDelay {  get; set; }
        public float BulletLifeCount { get; set; }
        public int ReloadSpeed { get; set; }
        public float Spray {  get; set; }
    }
}
