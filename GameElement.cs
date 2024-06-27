using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEditor
{
    public class GameElement
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Class { get; set; }
        public Color Color { get; set; }
        public int RotationAngle { get; set; }
        public bool IsStatic { get; set; }
        public bool IsCollidableWithLive { get; set; }


        public int Level { get; set; }
        public string MobType { get; set; }
        public int AgroRange { get; set; }
        public int UnAgroRange { get; set; }
        public List<Weapon> Weapons { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public float MaxHP { get; set; }
        public float CurrentHP { get; set; }
        public float Armor { get; set; }
        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public float FlyForce { get; set; }
        public float BodyDamage { get; set; }
        public float KnockbackStrength { get; set; }


        public int EventID { get; set; }


        public string Name { get; set; }
        public string GroupMemberClass { get; set; }
        public bool IsPlayer { get; set; }


        public bool isRound { get; set; }

        public void setDefaults()
        {
            switch (this.Class)
            {
                case "Static Rectangle":
                    IsStatic = true;
                    IsCollidableWithLive = true;
                    break;
                case "Static Circle":
                    IsStatic = true;
                    IsCollidableWithLive = true;
                    break;
                case "Dynamic Rectangle":
                    IsStatic = false;
                    IsCollidableWithLive = true;
                    break;
                case "Dynamic Circle":
                    IsStatic = false;
                    IsCollidableWithLive = true;
                    break;



                case "Destroyable Static Rectangle":
                    IsStatic = true;
                    IsCollidableWithLive = true;
                    MaxHP = 100;
                    CurrentHP = 100;
                    break;
                case "Destroyable Static Circle":
                    IsStatic = true;
                    IsCollidableWithLive = true;
                    MaxHP = 100;
                    CurrentHP = 100;
                    break;
                case "Destroyable Dynamic Rectangle":
                    IsStatic = false;
                    IsCollidableWithLive = true;
                    MaxHP = 100;
                    CurrentHP = 100;
                    break;
                case "Destroyable Dynamic Circle":
                    IsStatic = false;
                    IsCollidableWithLive = true;
                    MaxHP = 100;
                    CurrentHP = 100;
                    break;



                case "Mob":
                    IsStatic = false;
                    IsCollidableWithLive = true;
                    MaxHP = 100;
                    CurrentHP = 100;
                    Level = 1;
                    Speed = 1;
                    JumpForce = 1;
                    FlyForce = 1;
                    MobType = "WalkingMob";
                    BodyDamage = 10;
                    KnockbackStrength = 5;
                    Armor = 1;
                    AgroRange = 10;
                    UnAgroRange = 10;
                    Inventory = new List<InventoryItem>();
                    Inventory.Add(new InventoryItem { Amount = 1, IsStackable = false, Name = "Gold", Value = 1 });
                    Inventory.Add(new InventoryItem { Amount = 3, IsStackable = true, Name = "Bones", Value = 1 });
                    Inventory.Add(new InventoryItem { Amount = 1, IsStackable = false, Name = "Piece of Cake", Value = 100 });
                    break;
                case "NPC":
                    IsStatic = false;
                    IsCollidableWithLive = false;
                    MaxHP = 100;
                    CurrentHP = 100;
                    Level = 1;
                    Speed = 1;
                    JumpForce = 1;
                    FlyForce = 1;
                    Armor = 1;
                    Inventory = new List<InventoryItem>();
                    Inventory.Add(new InventoryItem { Amount = 1, IsStackable = false, Name = "Gold", Value = 1 });
                    Inventory.Add(new InventoryItem { Amount = 3, IsStackable = true, Name = "Bones", Value = 1 });
                    Inventory.Add(new InventoryItem { Amount = 1, IsStackable = false, Name = "Piece of Cake", Value = 100 });
                    Weapons = new List<Weapon>();
                    Weapons.Add(new Weapon { BulletSpeed = 1, BulletDelay = 1, BulletLifeCount = 10, AmmotAmount = 60, CurrentAmmoAmoint = 60, Damage = 1, IsReloading = false, ReloadSpeed = 30, Spray = 0.1f });
                    Weapons.Add(new Weapon { BulletSpeed = 10, BulletDelay = 10, BulletLifeCount = 10, AmmotAmount = 5, CurrentAmmoAmoint = 60, Damage = 1, IsReloading = false, ReloadSpeed = 30, Spray = 0.1f });
                    break;
                case "GroupMember":
                    IsStatic = false;
                    IsCollidableWithLive = false;
                    MaxHP = 100;
                    CurrentHP = 100;
                    Level = 1;
                    Speed = 1;
                    JumpForce = 1;
                    FlyForce = 1;
                    Armor = 1;
                    Inventory = new List<InventoryItem>();
                    Inventory.Add(new InventoryItem { Amount = 1, IsStackable = false, Name = "Gold", Value = 1 });
                    Inventory.Add(new InventoryItem { Amount = 3, IsStackable = true, Name = "Bones", Value = 1 });
                    Inventory.Add(new InventoryItem { Amount = 1, IsStackable = false, Name = "Piece of Cake", Value = 100 });
                    Weapons = new List<Weapon>();
                    Weapons.Add(new Weapon { Name = "AK47", BulletSpeed = 1, BulletDelay = 1, BulletLifeCount = 10, AmmotAmount = 60, CurrentAmmoAmoint = 60, Damage = 1, IsReloading = false, ReloadSpeed = 30, Spray = 0.1f });
                    Weapons.Add(new Weapon { Name = "AK48", BulletSpeed = 10, BulletDelay = 10, BulletLifeCount = 10, AmmotAmount = 5, CurrentAmmoAmoint = 60, Damage = 1, IsReloading = false, ReloadSpeed = 30, Spray = 0.1f });
                    GroupMemberClass = "Tank";
                    Name = "Orest";
                    IsPlayer = false;
                    break;
                case "Interractive Item":
                    IsStatic = false;
                    IsCollidableWithLive = true;
                    EventID = 0;
                    break;
                case "Ladder":
                    IsStatic = true;
                    break;
                case "Platform":
                    IsStatic = true;
                    break;
                case "Decoration":
                    IsStatic = true;
                    break;
            }

            if (Class != "Interractive Item")
            {
                EventID = -1;
            }
        }
    }


    public class SaveData
    {
        public string saveDate { get; set; }
        public string inGameTime { get; set; }
        public GameProgress gameProgress { get; set; }
        public MapInfo mapInfo { get; set; }
    }

    public class GameProgress
    {
        public float percentage { get; set; }
    }

    public class MapInfo
    {
        public int currentMap { get; set; }
        public List<MapData> maps { get; set; }
    }

    public class MapData
    {
        public string name { get; set; }

        public List<GameElement> setData { get; set; }
        public List<GameElement> updatableData { get; set; }
    }
}