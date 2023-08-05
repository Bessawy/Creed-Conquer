using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.PoPEdits.Items
{
    public class Map_10166 : MapThreadsItem
    {
        private bool _added = false, _started = false;
        private uint mID = 10166;
        private int interval = 1500;//means 1 min
        private string itemName = "GiantChasmMap";
        public bool Added { get { return this._added; } }

        public bool Started { get { return this._started; } }

        public uint MapID { get { return this.mID; } }

        public string ItemName { get { return this.itemName; } }

        public int Interval { get { return this.interval; } }

        public Role.GameMap GameMap
        {
            get
            {
                return Database.Server.ServerMaps[this.mID];
            }
        }
        public void ProcessObject()
        {
            if (!this._started)
                this._started = true;
            if (GameMap.ContainMobID(29370) || GameMap.ContainMobID(29360) || GameMap.ContainMobID(29300) || GameMap.ContainMobID(29363))
                return;
            if (DateTime.Now.Hour != Database.Server.ChasmBloodyBansheeHour.Hour && DateTime.Now.Minute == 10)
            {
                if (!GameMap.ContainMobID(29370))//Bloody Banshee
                {
                    Database.Server.ChasmBloodyBansheeHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 29370, 238, 165, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Bloody Banshee] has appeared in Giant Ghasm Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[GiantGhasm] Bloody Banshee Has Spawned.");
                }
            }
            if (DateTime.Now.Hour != Database.Server.ChasmChillingSpookHour.Hour && DateTime.Now.Minute == 20)
            {
                if (!GameMap.ContainMobID(29360))//Chilling Spook
                {
                    Database.Server.ChasmChillingSpookHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 29360, 214, 96, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Chilling Spook] has appeared in Giant Ghasm Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[GiantGhasm] Chilling Spook Has Spawned.");
                }
            }
            if (DateTime.Now.Hour != Database.Server.ChasmNetherTyrantHour.Hour && DateTime.Now.Minute == 40)
            {
                if (!GameMap.ContainMobID(29300))//Nether Tyrant
                {
                    Database.Server.ChasmNetherTyrantHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 29300, 257, 136, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Nether Tyrant] has appeared in Giant Ghasm Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[GiantGhasm] Nether Tyrant Has Spawned.");
                }
            }
            if (DateTime.Now.Hour != Database.Server.ChasmDragonWraithHour.Hour && DateTime.Now.Minute == 50)
            {
                if (!GameMap.ContainMobID(29363))//Dragon Wraith
                {
                    Database.Server.ChasmDragonWraithHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 29363, 204, 138, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Dragon Wraith] has appeared in Giant Ghasm Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[GiantGhasm] Dragon Wraith Has Spawned.");
                }
            }
        }
        public void AddedVo()
        {
            this._added = true;
        }
    }
}
