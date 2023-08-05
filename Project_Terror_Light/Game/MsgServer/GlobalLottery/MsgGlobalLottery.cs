using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.Game.MsgServer
{
    public static unsafe partial class MsgBuilder
    {
        public static unsafe ServerSockets.Packet CreateGlobalLotteryRespond(this ServerSockets.Packet stream, MsgGlobalLotteryPick obj)
        {
            stream.InitWriter();
            stream.ProtoBufferSerialize(obj);
            stream.Finalize(GamePackets.MsgGlobalLottery);

            return stream;
        }
        public static unsafe ServerSockets.Packet CreateGlobalLottery(this ServerSockets.Packet stream, MsgGlobalLottery obj)
        {
            stream.InitWriter();
            stream.ProtoBufferSerialize(obj);
            stream.Finalize(GamePackets.MsgGlobalLottery);

            return stream;
        }
        public static unsafe void GetGlobalLottery(this ServerSockets.Packet stream, out MsgGlobalLottery pQuery)
        {
            pQuery = new MsgGlobalLottery();
            pQuery = stream.ProtoBufferDeserialize<MsgGlobalLottery>(pQuery);
        }
    }
    [ProtoContract]
    public class MsgGlobalLottery
    {
        [ProtoMember(1, IsRequired = true)]
        public BlossomAction Type;
        [ProtoMember(2, IsRequired = true)]
        public uint PrizesType;
        [ProtoMember(3, IsRequired = true)]
        ///0 = Not Opened, 1 = Ready, 2 = Opened, 3 = Ranking
        public uint GUI;
        [ProtoMember(4, IsRequired = true)]
        public uint Time;
        [ProtoMember(5, IsRequired = true)]
        public uint Remain;
        [ProtoMember(6, IsRequired = true)]
        public uint TotalCount;
        /*[PacketAttribute(GamePackets.MsgGlobalLottery)]
        public void ExcuteMsgGlobalLottery(Client.GameClient client, ServerSockets.Packet stream)
        {
            MsgGlobalLottery Lottery;
            stream.GetGlobalLottery(out Lottery);
            switch (Lottery.Type)
            {
                case BlossomAction.Show:
                    {
                        Lottery.GUI = 2;
                        Lottery.PrizesType = Database.GlobalLotteryTable.TodayCondition.ID;
                        client.Send(stream.CreateGlobalLottery(Lottery));
                        break;
                    }
                case BlossomAction.PickUp:
                    {
                        var Prize = Database.GlobalLotteryTable.TodayCondition.PrizeItemID(Database.GlobalLotteryTable.GetRandomPrizeToday());
                        if (!client.Inventory.ContainItemWithStack(Database.GlobalLotteryTable.TodayCondition.CostValue, Prize.Count))
                        {
                            Database.ItemType.DBItem DBItem;
                            if (Database.Server.ItemsBase.TryGetValue(Database.GlobalLotteryTable.TodayCondition.CostValue, out DBItem))
                            {
                                client.Player.MessageBox("Sorry, you don't have " + DBItem.Name + " count " + Prize.Count + ".", null, null);
                            }
                            break;
                        }
                        if (DateTime.Now >= Database.Server.GoldenTreeExpirationDate)
                        {
                            client.SendSysMesage("Sorry, event expire now.");
                            MsgGoldenTree.EndEvent();
                            break;
                        }
                        if (Database.Server.GoldenTreeClaimed >= Database.Server.MaxAvaliableGoldenTreeClaim)
                        {
                            client.SendSysMesage("Sorry, event count claimed all.");
                            break;
                        }
                        client.Inventory.RemoveStackItem(Database.GlobalLotteryTable.TodayCondition.CostValue, Prize.Count, stream);
                        Database.Server.GoldenTreeClaimed++;
                        MsgGlobalLotteryPick Picked = new MsgGlobalLotteryPick() { UID = client.Player.UID, Name = client.Player.Name, TotalCount = Database.Server.MaxAvaliableGoldenTreeClaim, Type = 1 };
                        Picked.Remain = Database.Server.MaxAvaliableGoldenTreeClaim - Database.Server.GoldenTreeClaimed;
                        Picked.PickPrizeID = Prize.ID;
                        client.Inventory.Add(stream, Prize.PrizeItemID, 1, 0, 0, 0, Role.Flags.Gem.NoSocket, Role.Flags.Gem.NoSocket, true);
                        MsgGoldenTree.Update(client.Player.Name, Picked.PickPrizeID);
                        Database.GlobalLotteryTable.TodayPrizeID = 0;
                        MsgGoldenTree.SendWorldMessage(stream.CreateGlobalLotteryRespond(Picked));
                        break;
                    }
            }
        }*/
    }
    [Flags]
    public enum GuiType : uint
    {
        NotOpened,
        Ready,
        Opened,
        Ranking
    }
    [Flags]
    public enum BlossomAction : uint
    {
        None,
        PickUp,
        dwparam,
        dwparam1,
        dwparam2,
        Show
    }
    [ProtoContract]
    public class MsgGlobalLotteryPick
    {
        [ProtoMember(1, IsRequired = true)]
        public uint Type;
        [ProtoMember(2, IsRequired = true)]
        public uint UID;
        [ProtoMember(3, IsRequired = true)]
        public uint GUI;
        [ProtoMember(4, IsRequired = true)]
        public uint PickPrizeID;
        [ProtoMember(5, IsRequired = true)]
        public string Name;
        [ProtoMember(6, IsRequired = true)]
        public uint Remain;
        [ProtoMember(7, IsRequired = true)]
        public uint TotalCount;
    }
}
