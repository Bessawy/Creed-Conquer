using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.PoPEdits
{
    public static class MapThreadsController
    {
        private static Extensions.SafeDictionary<uint, MapThreadsItem> MapThreadingItems = new Extensions.SafeDictionary<uint, MapThreadsItem>();
        private static Extensions.SafeDictionary<uint, Extensions.ThreadGroup.ThreadItem> Threads = new Extensions.SafeDictionary<uint, Extensions.ThreadGroup.ThreadItem>();
        public static void LoadController()
        {
            if (Program.ServerConfig.IsInterServer == false)
            {
                if (!File.Exists(Environment.CurrentDirectory + "\\MapThreadItems.txt"))
                {
                    MyConsole.WriteLine("Couldn't Find The MapDefinerList.");
                }
                string[] strs = File.ReadAllLines(Environment.CurrentDirectory + "\\MapThreadItems.txt");
                foreach (string str in strs)
                {
                    uint ID = 0;
                    if (uint.TryParse(str, out ID))
                    {
                        LoadThis(ID);
                    }
                }
                StartThreads();
                MapThreadingItems = null;
            }
            else
            {
                Threads = null;
                MapThreadingItems = null;
            }
        }
        private static void LoadThis(uint ID)
        {
            try
            {
                var type = Type.GetType("Project_Terror_Light.PoPEdits.Items.Map_" + ID);
                MapThreadsItem Item = Activator.CreateInstance(type) as MapThreadsItem;
                Item.AddedVo();
                MapThreadingItems.Add(Item.MapID, Item);
            }
            catch (Exception e) { MyConsole.WriteLine("Couldn't load MapThreadItem By ID => " + ID); MyConsole.WriteException(e); }
        }
        private static void StartThreads()
        {
            foreach (var item in MapThreadingItems.Values)
            {
                if (!Threads.ContainsKey(item.MapID))
                {
                    Threads.Add(item.MapID, new Extensions.ThreadGroup.ThreadItem(item.Interval, item.ItemName, item.ProcessObject));
                }
                else
                {
                    MyConsole.WriteLine("Already Existed Item With MapID =>" + item.MapID);
                }
            }
            foreach (var thread in Threads.Values)
            {
                thread.Open();
            }
        }
        public static ServerSockets.Packet GetStream
        {
            get
            {
                using (var rec = new ServerSockets.RecycledPacket())
                {
                    var stream = rec.GetStream();
                    return stream;
                }
            }
        }
    }
    public interface MapThreadsItem
    {
        bool Added { get; }
        bool Started { get; }
        uint MapID { get; }
        string ItemName { get; }
        int Interval { get; }
        Role.GameMap GameMap { get; }
        void ProcessObject();
        void AddedVo();
    }
}
