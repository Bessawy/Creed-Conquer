using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.PoPEdits.Items
{
    public class Map_10137 : MapThreadsItem
    {
        private bool _added = false, _started = false;
        private uint mID = 10137;
        private int interval = 1500;//means 1 min
        private string itemName = "DragonIslandMap";
        public bool Added { get { return this._added; }}

        public bool Started { get { return this._started; }}

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
            if (GameMap.ContainMobID(20160) || GameMap.ContainMobID(20300) || GameMap.ContainMobID(20070))
                return;
            if (DateTime.Now.Hour == Database.Server.DragonIslandSpookHour.AddHours(1).Hour && DateTime.Now.Minute == 00)
            {
                if (!GameMap.ContainMobID(20160))//spook
                {
                    Database.Server.DragonIslandSpookHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 20160, 349, 635, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Thrilling Spook] has appeared in Dragon Island Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[DragonIsland] Thrilling Spook Has Spawned.");
                }
            }
            if (DateTime.Now.Hour == Database.Server.DragonIslandNemsisHour.AddHours(2).Hour && DateTime.Now.Minute == 20)
            {
                if (!GameMap.ContainMobID(20300))//nemsis
                {
                    Database.Server.DragonIslandNemsisHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 20300, 568, 372, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Nemesis Tyrant] has appeared in Dragon Island Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[DragonIsland] Nemesis Tyrant Has Spawned.");
                }
            }
            if (DateTime.Now.Hour == Database.Server.DragonIslandBansheeHour.AddHours(1).Hour && DateTime.Now.Minute == 40)
            {
                if (!GameMap.ContainMobID(20070))//banshee
                {
                    Database.Server.DragonIslandBansheeHour = DateTime.Now;
                    Database.Server.AddMapMonster(MapThreadsController.GetStream, GameMap, 20070, 658, 718, 3, 3, 1, 0, true, Project_Terror_Light.Game.MsgFloorItem.MsgItemPacket.EffectMonsters.None);
                    Program.SendGlobalPackets.Enqueue(new Game.MsgServer.MsgMessage("[Snow Banshee] has appeared in Dragon Island Go and kill it now.", Game.MsgServer.MsgMessage.MsgColor.orange, Game.MsgServer.MsgMessage.ChatMode.SlideCrosTheServer).GetArray(MapThreadsController.GetStream));
                    MyConsole.WriteLine("[DragonIsland] Snow Banshee Has Spawned.");
                }
            }
        }


        public void AddedVo()
        {
            this._added = true;
        }
    }
}
