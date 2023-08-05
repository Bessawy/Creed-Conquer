using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.Database
{

    public class CoatStorage
    {
        public class StorageItem
        {
            public uint ID;
            public uint UnKnow;
            public uint Stars;
        }
        public class StorageAttribute
        {
            public uint ID;
            public uint Type;
            public ushort CountReq;
            public Dictionary<byte, uint> Attributes;
        }
        public static Dictionary<uint, StorageItem> Garments = new Dictionary<uint, StorageItem>();
        public static Dictionary<uint, StorageAttribute> StorageAttributes = new Dictionary<uint, StorageAttribute>();
        public static List<StorageItem> GarmentsBig = new List<StorageItem>();
        public static List<StorageItem> MountsBig = new List<StorageItem>();
        public static Dictionary<uint, StorageItem> Mounts = new Dictionary<uint, StorageItem>();
        public static void Load()
        {
            string[] baseText = System.IO.File.ReadAllLines(Program.ServerConfig.DbLocation + "coat_storage_type.txt");
            foreach (var bas_line in baseText)
            {
                var line = bas_line.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
                StorageItem item = new StorageItem();
                item.ID = uint.Parse(line[0]);
                item.UnKnow = uint.Parse(line[1]);
                item.Stars = uint.Parse(line[7]);
                byte Type = byte.Parse(line[2]);

                if (Type == 1)//garment
                {
                    Garments.Add(item.ID, item);

                    if (item.Stars >= 4)
                        GarmentsBig.Add(item);


                }
                else
                {
                    Mounts.Add(item.ID, item);
                    if (item.Stars >= 4)
                        MountsBig.Add(item);
                }
            }
            baseText = System.IO.File.ReadAllLines(Program.ServerConfig.DbLocation + "coat_storage_attr.txt");
            foreach (var bas_line in baseText)
            {
                var line = bas_line.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
                StorageAttribute item = new StorageAttribute();
                item.ID = uint.Parse(line[0]);
                item.Type = uint.Parse(line[1]);
                item.CountReq = ushort.Parse(line[7]);
                item.Type = byte.Parse(line[2]);
                item.Attributes = new Dictionary<byte, uint>();
                for (int x = 3; x < line.Length; )
                {
                    byte y = byte.Parse(line[x++]);
                    uint u = uint.Parse(line[x++]);
                    if (y != 0)
                        item.Attributes.Add(y, u);
                }
                StorageAttributes.Add(item.ID, item);
            }
        }
        public static uint AmountStarGarments(Client.GameClient client, byte Star)
        {
            uint Count = 0;
            foreach (var garment in client.MyWardrobe.Items[(byte)Role.Instance.Wardrobe.ItemsType.Garment].Values)
            {
                StorageItem item;
                if (Garments.TryGetValue(garment.ITEM_ID, out item))
                {
                    if (item.Stars >= Star)
                        Count++;
                }
            }
            return Count;
        }
        public static uint AmountStarMount(Client.GameClient client, byte Star)
        {
            uint Count = 0;
            foreach (var mount in client.MyWardrobe.Items[(byte)Role.Instance.Wardrobe.ItemsType.Mount].Values)
            {
                StorageItem item;
                if (Mounts.TryGetValue(mount.ITEM_ID, out item))
                {
                    if (item.Stars >= Star)
                        Count++;
                }
            }
            return Count;
        }
    }
}