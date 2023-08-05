﻿using Project_Terror_Light.Game.MsgServer.Reward;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.Game.MsgServer
{
    public unsafe static partial class MsgBuilder
    {
        public static unsafe ServerSockets.Packet CreateRewardInfoPanel(this ServerSockets.Packet stream, List<Mode> Modes)
        {
            stream.InitWriter();
            stream.Write((ushort)0);
            stream.Write((ushort)Modes.Count);
            for (int x = 0; x < Modes.Count; x++)
            {
                stream.Write((uint)Modes[x]);
                stream.Write((ushort)1);
                stream.ZeroFill(7);
            }
            stream.Finalize(GamePackets.MsgOperatingActInfo);
            return stream;
        }
        public static unsafe ServerSockets.Packet CreateRewardInfoPacket(this ServerSockets.Packet stream, byte Count)
        {
            stream.InitWriter();
            stream.Write((ushort)1);
            stream.Write(Count);

            return stream;
        }
        public static unsafe ServerSockets.Packet AddItemReward(this ServerSockets.Packet stream, Database.ClientRewards.Rewards Reward)
        {
            stream.Write((uint)Reward.Mode);
            stream.Write((ushort)Reward.Claim);
            stream.Write((uint)Reward.Points);
            stream.Write((ushort)0);
            return stream;
        }
        public static unsafe void GetRewardAction(this ServerSockets.Packet stream, out byte Action, out Mode Mode)
        {
            Action = stream.ReadUInt8();
            Mode = (Reward.Mode)stream.ReadUInt32();
        }
        public static unsafe ServerSockets.Packet RewardInfoFinalize(this ServerSockets.Packet stream)
        {
            stream.Finalize(GamePackets.MsgOperatingActInfo);
            return stream;
        }
    }
    public unsafe struct MsgOperatingActInfo
    {
        /*[PacketAttribute(GamePackets.MsgOperatingAct)]
        private static void MsgOperatingActProcess(Client.GameClient user, ServerSockets.Packet stream)
        {
            return;
            byte Act;
            Mode Mode;
            stream.GetRewardAction(out Act, out Mode);
            switch (Act)
            {
                case 0:
                    {
                        RewardSystem.SendReward(user, stream);
                        break;
                    }
                case 2:
                    {
                        Database.ClientRewards.Rewards Own;
                        if (RewardSystem.Rewards.ContainsKey(Mode) && user.Rewards.TryGetValue(Mode, out Own))
                        {
                            if (Own.Points >= RewardSystem.Rewards[Mode][Own.Claim + 1].NeedPoints)
                            {
                                Database.ItemType.DBItem DBItem;
                                if (!Database.Server.ItemsBase.TryGetValue(RewardSystem.Rewards[Mode][Own.Claim + 1].prize, out DBItem))
                                    MyConsole.WriteLine("Error ! Invalid item id " + RewardSystem.Rewards[Mode][Own.Claim + 1].prize);
                                if (RewardSystem.Rewards[Mode][Own.Claim + 1].prizevalue == 0)
                                {
                                    if (user.Inventory.HaveSpace(1))
                                    {
                                        user.Inventory.Add(stream, DBItem.ID);
                                    }
                                    else
                                    {
                                        user.Inventory.AddReturnedItem(stream, DBItem.ID);
                                    }
                                    Own.Claim++;
                                }
                                else
                                {
                                    user.Player.ConquerPoints += RewardSystem.Rewards[Mode][Own.Claim + 1].prizevalue;
                                    Own.Claim++;
                                }
                            }
                        }
                        stream.CreateRewardInfoPacket((byte)RewardSystem.ids.Count);
                        foreach (var id in RewardSystem.ids)
                        {
                            if (user.Rewards.ContainsKey(id))
                            {
                                stream.AddItemReward(user.Rewards[id]);
                            }
                            else
                            {
                                stream.AddItemReward(new Database.ClientRewards.Rewards() { Points = 1, Mode = id, Claim = 0 });
                            }
                        }
                        user.Send(stream.RewardInfoFinalize());
                        break;
                    }
                default: MyConsole.WriteLine("[Reward] Unknown Action: " + Act + " "); break;
            }
        }
        */
    }
    public static class RewardSystem
    {
        public static void SendReward(Client.GameClient user, ServerSockets.Packet stream, bool login = false)
        {
            stream.CreateRewardInfoPacket((byte)RewardSystem.ids.Count);
            foreach (var id in ids)
            {
                if (user.Rewards.ContainsKey(id))
                {
                    stream.AddItemReward(user.Rewards[id]);
                }
                else
                {
                    stream.AddItemReward(new Database.ClientRewards.Rewards() { Points = 1, Mode = id, Claim = 0 });
                }
            }
            user.Send(stream.RewardInfoFinalize());
        }
        public static List<Mode> ids;
        public static Dictionary<Project_Terror_Light.Game.MsgServer.Reward.Mode, Dictionary<uint, Reward.Reward>> Rewards = new Dictionary<Reward.Mode, Dictionary<uint, Reward.Reward>>();
        public static void Load()
        {
            /*WindowsAPI.IniFile IniFile = new WindowsAPI.IniFile("\\OperateActivity.ini");
            int count = IniFile.ReadInt32("OperateActivity", "Num", 0);
            ids = new List<Mode>(count);
            for (int x = 0; x < count; x++)
            {
                ids.Add((Mode)IniFile.ReadUInt32(x.ToString(), "id", 0));
            }
            IniFile = null;
            string[] baseText = File.ReadAllLines(Program.ServerConfig.DbLocation + "operating_prize.txt");
            foreach (var bas_line in baseText)
            {
                Database.DBActions.ReadLine line = new Project_Terror_Light.Database.DBActions.ReadLine(bas_line, ' ');
                Reward.Reward Item = new Reward.Reward();
                Item.UID = line.Read((uint)0);
                Item.ID = (Mode)line.Read((uint)0);
                Item.number = line.Read((uint)0);
                Item.NeedPoints = line.Read((uint)0);
                Item.prize = line.Read((uint)0);
                Item.prizevalue = line.Read((uint)0);
                if (Rewards.ContainsKey(Item.ID))
                {
                    Rewards[Item.ID].Add(Item.number, Item);
                }
                else
                {
                    Rewards.Add(Item.ID, new Dictionary<uint, Reward.Reward>(10));
                    Rewards[Item.ID].Add(Item.number, Item);
                }
            }
            baseText = null;*/
        }
    }
}
