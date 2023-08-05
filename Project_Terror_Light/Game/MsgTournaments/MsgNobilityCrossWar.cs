using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.Game.MsgTournaments
{
    public class NobilityCrossWar : ITournament
    {

        public const uint King = 300000;
        public const uint Prince = 200000;
        public const uint Duke = 100000;

        public ProcesType Process { get; set; }
        public DateTime StartTimer = new DateTime();
        public DateTime InfoTimer = new DateTime();
        public uint Secounds = 60;
        public Role.GameMap Map1;
        public Role.GameMap Map2;
        public Role.GameMap Map3;
        public uint DinamicMap1 = 0;
        public uint DinamicMap2 = 0;
        public uint DinamicMap3 = 0;
        public KillerSystem KillSystem;
        public TournamentType Type { get; set; }
        public NobilityCrossWar(TournamentType _type)
        {
            Type = _type;
            Process = ProcesType.Dead;
        }

        public void Open()
        {
            if (Process == ProcesType.Dead)
            {
                KillSystem = new KillerSystem();
                StartTimer = DateTime.Now;
                using (var x = new ServerSockets.RecycledPacket())
                {
                    var stream = x.GetStream();
                    stream.Seek(4);
                    stream.Write(1);
                    stream.Seek(stream.Size - 8);
                    stream.Finalize(MsgInterServer.PacketTypes.InterServer_NobilityRank);
                    MsgInterServer.PipeServer.Send(stream);
                }
                if (Map1 == null)
                {
                    Map1 = Database.Server.ServerMaps[700];
                    DinamicMap1 = Map1.GenerateDynamicID();
                }
                if (Map2 == null)
                {
                    Map2 = Database.Server.ServerMaps[700];
                    DinamicMap2 = Map2.GenerateDynamicID();
                }
                if (Map3 == null)
                {
                    Map3 = Database.Server.ServerMaps[700];
                    DinamicMap3 = Map3.GenerateDynamicID();
                }
                InfoTimer = DateTime.Now;
                Secounds = 60;
                Process = ProcesType.Idle;
            }
        }
        public bool Join(Client.GameClient user, ServerSockets.Packet stream)
        {
            if (Process == ProcesType.Idle)
            {
                user.Player.SetPkMode(Role.Flags.PKMode.PK);
                if (user.Player.NobilityRank == Role.Instance.Nobility.NobilityRank.King)
                {
                    ushort x = 0;
                    ushort y = 0;
                    Map1.GetRandCoord(ref x, ref y);
                    user.Teleport(x, y, Map1.ID, DinamicMap1); return true;
                }
                else if (user.Player.NobilityRank == Role.Instance.Nobility.NobilityRank.Prince)
                {
                    ushort x = 0;
                    ushort y = 0;
                    Map2.GetRandCoord(ref x, ref y);
                    user.Teleport(x, y, Map2.ID, DinamicMap2); return true;
                }
                else if (user.Player.NobilityRank == Role.Instance.Nobility.NobilityRank.Duke)
                {
                    ushort x = 0;
                    ushort y = 0;
                    Map3.GetRandCoord(ref x, ref y);
                    user.Teleport(x, y, Map3.ID, DinamicMap3); return true;
                }

            }
            return false;
        }
        public void CheckUp()
        {
            if (Process == ProcesType.Idle)
            {
                if (DateTime.Now > StartTimer.AddMinutes(1))
                {
                    using (var x = new ServerSockets.RecycledPacket())
                    {
                        var stream = x.GetStream();
                        stream.Seek(4);
                        stream.Write(2);
                        stream.Seek(stream.Size - 8);
                        stream.Finalize(MsgInterServer.PacketTypes.InterServer_NobilityRank);
                        MsgInterServer.PipeServer.Send(stream);
                    }
                    MsgSchedules.SendSysMesage("NobiltyCrossWar has started! signup are now closed.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.red);
                    Process = ProcesType.Alive;
                    StartTimer = DateTime.Now;
                }
                else if (DateTime.Now > InfoTimer.AddSeconds(10))
                {
                    Secounds -= 10;
                    MsgSchedules.SendSysMesage("[NobiltyCrossWar] Fight starts in " + Secounds.ToString() + " Seconds.", MsgServer.MsgMessage.ChatMode.TopLeftSystem, MsgServer.MsgMessage.MsgColor.red);
                    InfoTimer = DateTime.Now;
                }
            }
            if (Process == ProcesType.Alive)
            {
                if (DateTime.Now > StartTimer.AddMinutes(60))
                {
                    foreach (var user in MapPlayers1())
                    {
                        MsgSchedules.SendSysMesage("NobiltyCrossWar has ends no one win.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.red);
                        user.Socket.Disconnect();
                    }
                    foreach (var user in MapPlayers2())
                    {
                        MsgSchedules.SendSysMesage("NobiltyCrossWar has ends no one win.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.red);
                        user.Socket.Disconnect();
                    }
                    foreach (var user in MapPlayers3())
                    {
                        MsgSchedules.SendSysMesage("NobiltyCrossWar has ends no one win.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.red);
                        user.Socket.Disconnect();
                    }
                    Process = ProcesType.Dead;
                }
                if (MapPlayers1().Length == 1)
                {
                    var winner = MapPlayers1().First();
                    MsgSchedules.SendSysMesage("" + winner.Player.Name + " has Won King~NobiltyCrossWar , he received " + King.ToString() + " ConquerPoints.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.white);
                    winner.Player.ConquerPoints += King;
                    winner.Teleport(295, 145, 1002, 0);
                    winner.Socket.Disconnect();
                }
                if (MapPlayers2().Length == 1)
                {
                    var winner = MapPlayers2().First();
                    MsgSchedules.SendSysMesage("" + winner.Player.Name + " has Won Prince~NobiltyCrossWar  he received " + Prince.ToString() + " ConquerPoints.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.white);
                    winner.Player.ConquerPoints += Prince;
                    winner.Socket.Disconnect();
                }
                if (MapPlayers3().Length == 1)
                {
                    var winner = MapPlayers3().First();
                    MsgSchedules.SendSysMesage("" + winner.Player.Name + " has Won Duke~NobiltyCrossWar , he received " + Duke.ToString() + " ConquerPoints.", MsgServer.MsgMessage.ChatMode.Center, MsgServer.MsgMessage.MsgColor.white);
                    winner.Player.ConquerPoints += Duke;
                    winner.Socket.Disconnect();
                }
                Extensions.Time32 Timer = Extensions.Time32.Now;
                foreach (var user in MapPlayers1())
                {
                    if (user.Player.Alive == false)
                    {
                        if (user.Player.DeadStamp.AddSeconds(4) < Timer)
                        {
                            using (var rec = new ServerSockets.RecycledPacket())
                            {
                                var stream = rec.GetStream();
                                user.Player.Revive(stream);
                            }
                            user.Socket.Disconnect();
                        }
                    }
                }
                foreach (var user in MapPlayers2())
                {
                    if (user.Player.Alive == false)
                    {
                        if (user.Player.DeadStamp.AddSeconds(4) < Timer)
                        {
                            using (var rec = new ServerSockets.RecycledPacket())
                            {
                                var stream = rec.GetStream();
                                user.Player.Revive(stream);
                            }
                            user.Socket.Disconnect();
                        }
                    }
                }
                foreach (var user in MapPlayers3())
                {
                    if (user.Player.Alive == false)
                    {
                        if (user.Player.DeadStamp.AddSeconds(4) < Timer)
                        {
                            using (var rec = new ServerSockets.RecycledPacket())
                            {
                                var stream = rec.GetStream();
                                user.Player.Revive(stream);
                            }
                            user.Socket.Disconnect();
                        }
                    }
                }
                if (MapPlayers1().Length == 0 && MapPlayers2().Length == 0 && MapPlayers3().Length == 0)
                {
                    Process = ProcesType.Dead;
                }
            }
        }
        public Client.GameClient[] MapPlayers1()
        {
            return Map1.Values.Where(p => p.Player.DynamicID == DinamicMap1 && p.Player.Map == Map1.ID).ToArray();
        }
        public Client.GameClient[] MapPlayers2()
        {
            return Map2.Values.Where(p => p.Player.DynamicID == DinamicMap2 && p.Player.Map == Map2.ID).ToArray();
        }
        public Client.GameClient[] MapPlayers3()
        {
            return Map3.Values.Where(p => p.Player.DynamicID == DinamicMap3 && p.Player.Map == Map3.ID).ToArray();
        }
        public bool InTournament(Client.GameClient user)
        {
            if (Map1 == null) return false;
            if (Map2 == null) return false;
            if (Map3 == null) return false;
            return user.Player.Map == Map1.ID && user.Player.DynamicID == DinamicMap1 || user.Player.Map == Map2.ID && user.Player.DynamicID == DinamicMap2 || user.Player.Map == Map3.ID && user.Player.DynamicID == DinamicMap3;
        }
    }
}
