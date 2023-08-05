using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Project_Terror_Light.Game.MsgServer;
using Project_Terror_Light.Game;
using Project_Terror_Light.Cryptography;
using ProtoBuf;
using Project_Terror_Light.Database;
using System.Net;


namespace Project_Terror_Light
{
    using PacketInvoker = CachedAttributeInvocation<Action<Client.GameClient, ServerSockets.Packet>, PacketAttribute, ushort>;
    using COServer.Database;
    using Project_Terror_Light.Panels;
    class Program
    {
        public static Extensions.Time32 CurrentTime
        {
            get
            {
                return new Extensions.Time32();
            }
        }
        public static List<byte[]> LoadPackets = new List<byte[]>();
        public static ServerSockets.SocketThread SocketsGroup;
        public static List<uint> ProtectMapSpells = new List<uint>() { 1038, Game.MsgTournaments.MsgSuperGuildWar.MapID };
        public static List<uint> MapCounterHits = new List<uint>() { 1005, 6000 };
        public static bool OnMainternance = false;
        public static string LogginKey = "qL0UVCXB6BY9txb2";
        public static Cryptography.TransferCipher transferCipher;
        public static Extensions.Time32 SaveDBStamp = Extensions.Time32.Now.AddMilliseconds(KernelThread.SaveDatabaseStamp);
        public static List<uint> RankableFamilyIds = new List<uint>() { 20300, 20160, 20070, 29370, 29360, 29300, 29363 };
        public static List<uint> NoDropItems = new List<uint>() { 1764, 700, 3954, 3820 };

        public static List<uint> FreePkMap = new List<uint>() { 10137, 10166, 3998,3071, 6000, 6001,1505, 1005, 1038, 700,1508/*PkWar*/, Game.MsgTournaments.MsgSuperGuildWar.MapID, Game.MsgTournaments.MsgCaptureTheFlag.MapID
        , Game.MsgTournaments.MsgTeamDeathMatch.MapID };
        public static List<uint> BlockAttackMap = new List<uint>() { 3825,3830, 3831, 3832,3834,3826,3827,3828,3829,3833, 9995,1068, 4020, 4000, 4003, 4006, 4008, 4009 , 1860 ,1858, 1801, 1780, 1779/*Ghost Map*/, 9972, 1806, 1002, 3954, 3081, 1036, 1004, 1008, 601, 1006, 1511, 1039, 700, Game.MsgTournaments.MsgEliteGroup.WaitingAreaID, (uint)Game.MsgTournaments.MsgSteedRace.Maps.DungeonRace, (uint)Game.MsgTournaments.MsgSteedRace.Maps.IceRace
        ,(uint)Game.MsgTournaments.MsgSteedRace.Maps.IslandRace, (uint)Game.MsgTournaments.MsgSteedRace.Maps.LavaRace, (uint)Game.MsgTournaments.MsgSteedRace.Maps.MarketRace};
        public static List<uint> BlockTeleportMap = new List<uint>() { 601, 6000, 6001, 1005, 700, 1858, 1860, 3852, Game.MsgTournaments.MsgEliteGroup.WaitingAreaID, 1768 };
        public static Role.Instance.Nobility.NobilityRanking NobilityRanking = new Role.Instance.Nobility.NobilityRanking();
        public static Role.Instance.ChiRank ChiRanking = new Role.Instance.ChiRank();

        public static Role.Instance.Flowers.FlowersRankingToday FlowersRankToday = new Role.Instance.Flowers.FlowersRankingToday();
        public static Role.Instance.Flowers.FlowerRanking GirlsFlowersRanking = new Role.Instance.Flowers.FlowerRanking();
        public static Role.Instance.Flowers.FlowerRanking BoysFlowersRanking = new Role.Instance.Flowers.FlowerRanking(false);

        public static ShowChatItems GlobalItems;
        public static SendGlobalPacket SendGlobalPackets;
        public static PacketInvoker MsgInvoker;
        public static ServerSockets.ServerSocket GameServer;

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleHandlerDelegate handler, bool add);
        private delegate bool ConsoleHandlerDelegate(int type);
        private static ConsoleHandlerDelegate handlerKeepAlive;

        public static bool ProcessConsoleEvent(int type)
        {
            try
            {
                if (ServerConfig.IsInterServer)
                {
                    foreach (var client in Database.Server.GamePoll.Values)
                    {
                        try
                        {
                            if (client.Socket != null)//for my fake accounts !
                                client.Socket.Disconnect();
                        }
                        catch (Exception e)
                        {
                            MyConsole.WriteLine(e.ToString());
                        }
                    }
                    return true;
                }
                try
                {
                    /*if (WebServer.Proces.AccServer != null)
                    {
                        WebServer.Proces.Close();
                        WebServer.Proces.AccServer.Close();
                    }*/
                    if (GameServer != null)
                        GameServer.Close();


                }
                catch (Exception e) { MyConsole.SaveException(e); }

                /*try
                {
                    foreach (var user in WebServer.LoaderServer.Clients.Values)
                        user.Disconnect();
                }
                catch (Exception e)
                {
                    MyConsole.SaveException(e);
                }*/
                MyConsole.WriteLine("Saving Database...");


                foreach (var client in Database.Server.GamePoll.Values)
                {
                    try
                    {
                        if (client.Socket != null)//for my fake accounts !
                            client.Socket.Disconnect();
                    }
                    catch (Exception e)
                    {
                        MyConsole.WriteLine(e.ToString());
                    }
                }
                Role.Instance.Clan.ProcessChangeNames();

                Database.Server.SaveDatabase();
                if (Database.ServerDatabase.LoginQueue.Finish())
                {
                    System.Threading.Thread.Sleep(1000);
                    MyConsole.WriteLine("Database Save Succefull.");
                }
            }
            catch (Exception e)
            {
                MyConsole.SaveException(e);
            }
            return true;
        }
        public static Extensions.Time32 ResetRandom = new Extensions.Time32();
        public static Extensions.SafeRandom GetRandom = new Extensions.SafeRandom();
        public static Extensions.RandomLite LiteRandom = new Extensions.RandomLite();
        public static class ServerConfig
        {
            public static string CO2Folder = "";
#if Encore
           public static string XtremeTopLink = "http://www.xtremetop100.com/in.php?site=1132352308&postback=";
            public static string Chatbox = "https://www.facebook.com/NightConquer/";
            public static string ChangePassword = "https://www.facebook.com/NightConquer/";
            public static string StorePage = "https://www.facebook.com/NightConquer/";

#else
            public static string XtremeTopLink = "http://www.xtremetop100.com/in.php?site=1132355001";
#endif
            public static uint ServerID = 0;
            public static string IPAddres = "192.168.1.26";
            public static ushort GamePort = 30020;
            public static string ServerName = "Realm1";
            // سيبو هو بيقرأ من ا لكونفج ا مسحه هنا سيبو وخلاص ملهوش لزمه عامل
            public static string OfficialWebSite = "Realm1.com";
            public static ushort Port_BackLog;
            public static ushort Port_ReceiveSize = 4096;
            public static ushort Port_SendSize = 2048;
            //Database// هتعدل اللي ف شيل ؟ ا ها بس نا قص ا لى بقولك عليها  دي 16 هيا ف شيل لا امال فين سوكت ابعتها فيس 
            public static string DbLocation = "";
            public static uint ExpRateSpell = 1;
            public static uint ExpRateProf = 1;
            public static uint UserExpRate = 1;
            public static uint PhysicalDamage = 100;// + 150%
            //interServer
            public static string InterServerAddress = "192.168.1.26";
            public static ushort InterServerPort = 0;
            public static bool IsInterServer = false;
        }
        //You do not have 500 silvers with you.
        //Sorry, but you don`t have enough CPs.
        //Please come back when you will have 1 Star Crystal in your inventory.
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
        static int CutTrail(int x, int y) { return (x >= y) ? x : y; }
        static int AdjustDefence(int nDef, int power2, int bless)
        {
            int nAddDef = 0;
            nAddDef += Game.MsgServer.AttackHandler.Calculate.Base.MulDiv(nDef, 100 - power2, 100) - nDef;
            return Game.MsgServer.AttackHandler.Calculate.Base.MulDiv(nDef + nAddDef, 100 - power2, 100);
            return nDef + nAddDef;
        }
        public static void TESTT()
        {
            double base_d_factor = 130;
            double scaled_d_factor = 0.5;
            double dif = 139500 - 25000;
            double sign_dif = Math.Sign(dif);
            double scale = 1.0 + (-1.0 / (sign_dif + dif / (base_d_factor + 25000 * scaled_d_factor)) + sign_dif);
            double ttt = 139500 * scale;
        }
        public class sorine
        {
            public uint uid = 333;
        }
        static byte[] DecryptString(char[] str)
        {
            int i = 0;
            byte[] nstr = new byte[1000];
            do
            {
                nstr[i] = Convert.ToByte(str[i + 1] ^ 0x34);
            } while (nstr[i++] != 0);
            return nstr;
        }
        public static void writetext(string tes99)
        {
            char[] tg = new char[tes99.Length];
            for (int x = 0; x < tes99.Length; x++)
                tg[x] = tes99[x];
            var hhhh = DecryptString(tg);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(hhhh));
        }
        public static int n, sol;
        public static int[] v = new int[100];
        public static void afisare()
        {
            Console.WriteLine("");
            int i, j, x;
            sol++;

            Console.WriteLine("sol: " + sol);
            for (i = 1; i <= n; i++)
            {
                Console.Write(v[i] + " ");
                /* for (j = 1; j <= n; j++)
              
                     /*if (v[i] == j)
                         Console.Write("D ");
                     else
                         Console.Write("_ ");
                 Console.Write(Environment.NewLine);*/
            }
            Console.Write(Environment.NewLine);
        }
        public static int valid(int k)
        {
            int i;
            for (i = 1; i <= k - 1; i++)
                if ((v[k] <= v[i]))//|| (Math.Abs(v[k] - v[i]) == (k - i)))
                    return 0;
            return 1;
        }
        public static int solutie(int k)
        {
            if (k == n)
                return 1;
            return 0;
        }
        public static void BK(int k)
        {
            for (int i = 1; i <= n; i++)
            {
                v[k] = i;
                if (valid(k) == 1)
                {
                    if (solutie(k) == 1)
                        afisare();
                    else
                        BK(k + 1);
                }
            }
        }
        public static unsafe void Main(string[] args)
        {

            try
            {

                MyConsole.DissableButton();

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                ServerSockets.Packet.SealString = "TQServer";



                Console.ForegroundColor = ConsoleColor.White;
                MyConsole.WriteLine("--------- Project_Terror_v2 -----------");
                MyConsole.WriteLine("This source was writen by Ceausu Sorin.");
                MyConsole.WriteLine("---------------------------------------\n");
                COServer.Database.DataHolder.CreateConnection();


                MsgInvoker = new PacketInvoker(PacketAttribute.Translator);
                Cryptography.DHKeyExchange.KeyExchange.CreateKeys();

                Game.MsgTournaments.MsgSchedules.Create();

                Database.Server.Initialize();

                SendGlobalPackets = new SendGlobalPacket();

                Cryptography.AuthCryptography.PrepareAuthCryptography();

                Database.Server.LoadDatabase();
              //  Poker.Database.Load();
                //BossDatabase.Load();
                handlerKeepAlive = ProcessConsoleEvent;
                SetConsoleCtrlHandler(handlerKeepAlive, true);
                TransferCipher.Key = Encoding.ASCII.GetBytes("EypKhLvYJ3zdLCTyz9Ak8RAgM78tY5F32b7CUXDuLDJDFBH8H67BWy9QThmaN5VS");
                TransferCipher.Salt = Encoding.ASCII.GetBytes("MyqVgBf3ytALHWLXbJxSUX4uFEu3Xmz2UAY9sTTm8AScB7Kk2uwqDSnuNJske4BJ");
                transferCipher = new TransferCipher("127.0.0.1");

                //WebServer.LoaderServer.Init();
                //WebServer.Proces.Init();

                if (ServerConfig.IsInterServer == false)
                {
                    GameServer = new ServerSockets.ServerSocket(
                        new Action<ServerSockets.SecuritySocket>(p => new Client.GameClient(p))
                        , Game_Receive, Game_Disconnect);
                    GameServer.Initilize(ServerConfig.Port_SendSize, ServerConfig.Port_ReceiveSize, 1, 3);
                    GameServer.Open(ServerConfig.IPAddres, ServerConfig.GamePort, ServerConfig.Port_BackLog);

                }

                GlobalItems = new ShowChatItems();

                Database.NpcServer.LoadServerTraps();

                MsgInterServer.PipeServer.Initialize();

                ThreadPoll.Create();


                // ServerSockets.SocketPoll.Create("ConquerSockets");

                SocketsGroup = new ServerSockets.SocketThread("ConquerServer"
                    , GameServer, MsgInterServer.PipeServer.Server
                    );//,

                SocketsGroup.Start();

                MsgInterServer.StaticConnexion.Create();

                Game.MsgTournaments.MsgSchedules.ClanWar = new Game.MsgTournaments.MsgClanWar();



                Console.WriteLine("Logging Path: " + Project_Terror_Light.Logging.ExecuteLogging.Path);

                new KernelThread(500, "ConquerServer2").Start();
                new MapGroupThread(100, "ConquerServer3").Start();
                new MapItemThread(300, "ConquerServer4").Start();


                //    Database.ServerDatabase.Testtt();

            }
            catch (Exception e) { MyConsole.WriteException(e); }

            for (; ; )
                ConsoleCMD(MyConsole.ReadLine());
        }

        public static void SaveDBPayers(Extensions.Time32 clock)
        {

            if (clock > SaveDBStamp)
            {
                if (Database.Server.FullLoading && !Program.ServerConfig.IsInterServer)
                {
                    foreach (var user in Database.Server.GamePoll.Values)
                    {
                        if (user.OnInterServer)
                            continue;
                        if ((user.ClientFlag & Client.ServerFlag.LoginFull) == Client.ServerFlag.LoginFull)
                        {
                            user.ClientFlag |= Client.ServerFlag.QueuesSave;
                            Database.ServerDatabase.LoginQueue.TryEnqueue(user);
                        }
                    }
                    Database.Server.SaveDatabase();
                    MyConsole.WriteLine("Database got saved ! ");
                }
                SaveDBStamp.Value = clock.Value + KernelThread.SaveDatabaseStamp;
            }

        }
        public unsafe static void ConsoleCMD(string cmd)
        {
            try
            {
                string[] line = cmd.Split(' ');

                switch (line[0])
                {
                    case "Mido1":
                        {
                            var ChatCon = new Panels.ChatPanal();
                            ChatCon.ShowDialog();
                            break;
                        }
                    case "Mido":
                        {
                            Controlpanel cp = new Controlpanel();
                            cp.ShowDialog();
                            break;
                        }
                    case "clear":
                        {
                            Console.Clear();
                            break;
                        }
                    case "cpst":
                        {
                            if (line.Length < 2)
                            {
                                MyConsole.WriteLine("Message Can't be Deququed.");
                                break;
                            }
                            uint Bet = Convert.ToUInt32(line[1]);
                            foreach (var user in Server.GamePoll.Values)
                            {
                                user.Player.MessageBox("Cps Tournment War Started Bet " + Bet + " Cps", p =>
                                {
                                    if (Game.MsgTournaments.MsgSchedules.ArenaCpsBattle.SignUp(p, Bet) == false)
                                    {
                                        p.SendSysMesage("don't have requierments.", MsgMessage.ChatMode.Service, MsgMessage.MsgColor.yellow);
                                    }
                                }, null, 15);
                            }
                            break;
                        }
                    case "goldentree":
                        {
                            if (Database.GlobalLotteryTable.Loaded)
                            {
                                Database.Server.GoldenTreeClaimed = 0;
                                Database.Server.GoldenTreeExpirationDate = DateTime.Now.AddHours(double.Parse(line[1]));
                                Database.Server.MaxAvaliableGoldenTreeClaim = uint.Parse(line[2]);
                            }
                            else
                                MyConsole.WriteLine("Couldn't Load Global Lottery Files.");
                            break;
                        }
                    case "powerarena":
                        {
                            Game.MsgTournaments.MsgSchedules.PowerArena.Start();
                            break;
                        }
                    case "squidward":
                        {
                            Game.MsgTournaments.MsgSchedules.SquidwardOctopus.Start();
                            break;
                        }
                    case "save":
                        {
                            Database.Server.SaveDatabase();
                            if (Database.Server.FullLoading && !Program.ServerConfig.IsInterServer)
                            {
                                foreach (var user in Database.Server.GamePoll.Values)
                                {
                                    if (user.OnInterServer)
                                        continue;
                                    if ((user.ClientFlag & Client.ServerFlag.LoginFull) == Client.ServerFlag.LoginFull)
                                    {
                                        user.ClientFlag |= Client.ServerFlag.QueuesSave;
                                        Database.ServerDatabase.LoginQueue.TryEnqueue(user);
                                    }
                                }
                                MyConsole.WriteLine("Database got saved ! ");
                            }
                            if (Database.ServerDatabase.LoginQueue.Finish())
                            {
                                System.Threading.Thread.Sleep(1000);
                                MyConsole.WriteLine("Database saved successfully.");
                            }
                            break;
                        }

                    case "steed":
                        {
                            Game.MsgTournaments.MsgSchedules.SteedRace.Create();
                            break;
                        }
                    case "ctfon":
                        {
                            Game.MsgTournaments.MsgSchedules.CaptureTheFlag.Start();
                            break;
                        }
                    case "teamon":
                        {
                            Game.MsgTournaments.MsgSchedules.TeamPkTournament.Start();
                            break;
                        }
                    case "kick":
                        {

                            foreach (var user in Database.Server.GamePoll.Values)
                            {
                                if (user.Player.Name.Contains(line[1]))
                                {
                                    user.EndQualifier();
                                }
                            }
                            break;
                        }

                    case "pk":
                        {
                            Game.MsgTournaments.MsgSchedules.ElitePkTournament.Start();

                            foreach (var clients in Database.Server.GamePoll.Values)
                            {
                                Game.MsgTournaments.MsgSchedules.ElitePkTournament.SignUp(clients);
                            }
                            break;
                        }
                    case "teampk":
                        {
                            Game.MsgTournaments.MsgSchedules.SkillTeamPkTournament.Start();
                            var array = Database.Server.GamePoll.Values.ToArray();


                            for (int x = 0; x < array.Length - 5; x += 5)
                            {
                                if (array[x].Team == null)
                                {
                                    try
                                    {
                                        array[x].Team = new Role.Instance.Team(array[x]);
                                        Game.MsgTournaments.MsgSchedules.SkillTeamPkTournament.SignUp(array[x]);
                                        using (var rec = new ServerSockets.RecycledPacket())
                                        {
                                            var stream = rec.GetStream();
                                            array[x + 1].Team = array[0].Team;
                                            array[x].Team.Add(stream, array[x + 1]);
                                            Game.MsgTournaments.MsgSchedules.SkillTeamPkTournament.SignUp(array[x + 1]);

                                            array[x + 2].Team = array[0].Team;
                                            array[x].Team.Add(stream, array[x + 2]);
                                            Game.MsgTournaments.MsgSchedules.SkillTeamPkTournament.SignUp(array[x + 2]);

                                            array[x + 3].Team = array[0].Team;
                                            array[x].Team.Add(stream, array[x + 3]);
                                            Game.MsgTournaments.MsgSchedules.SkillTeamPkTournament.SignUp(array[x + 3]);

                                            array[x + 4].Team = array[0].Team;
                                            array[x].Team.Add(stream, array[x + 4]);
                                            Game.MsgTournaments.MsgSchedules.SkillTeamPkTournament.SignUp(array[x + 4]);
                                        }

                                    }
                                    catch { }
                                }
                            }
                            break;
                        }
                    case "search":
                        {
                            WindowsAPI.IniFile ini = new WindowsAPI.IniFile("");
                            foreach (string fname in System.IO.Directory.GetFiles(Program.ServerConfig.DbLocation + "\\Users\\"))
                            {
                                ini.FileName = fname;

                                string Name = ini.ReadString("Character", "Name", "None");
                                if (Name.ToLower() == line[1].ToLower() || Name.Contains(line[1]))
                                {
                                    Console.WriteLine(ini.ReadUInt32("Character", "UID", 0));
                                    break;
                                }

                            }
                            break;
                        }
                    case "resetnobility":
                        {
                            WindowsAPI.IniFile ini = new WindowsAPI.IniFile("");
                            foreach (string fname in System.IO.Directory.GetFiles(Program.ServerConfig.DbLocation + "\\Users\\"))
                            {
                                ini.FileName = fname;

                                ulong nobility = ini.ReadUInt64("Character", "DonationNobility", 0);
                                nobility = nobility * 30 / 100;
                                nobility = 0;
                                ini.Write<ulong>("Character", "DonationNobility", nobility);
                            }

                            break;
                        }
                    case "check":
                        {
                            WindowsAPI.IniFile ini = new WindowsAPI.IniFile("");
                            foreach (string fname in System.IO.Directory.GetFiles(Program.ServerConfig.DbLocation + "\\Users\\"))
                            {
                                ini.FileName = fname;

                                long nobility = ini.ReadInt64("Character", "Money", 0);
                                if (nobility < 0)
                                {
                                    Console.WriteLine("");
                                }

                            }
                            break;
                        }
                    case "fixedgamemap":
                        {
                            Dictionary<int, string> maps = new Dictionary<int, string>();
                            using (var gamemap = new BinaryReader(new FileStream(Path.Combine(Program.ServerConfig.CO2Folder, "ini/gamemap.dat"), FileMode.Open)))
                            {

                                var amount = gamemap.ReadInt32();
                                for (var i = 0; i < amount; i++)
                                {

                                    var id = gamemap.ReadInt32();
                                    var fileName = Encoding.ASCII.GetString(gamemap.ReadBytes(gamemap.ReadInt32()));
                                    var puzzleSize = gamemap.ReadInt32();
                                    if (id == 1017)
                                    {
                                        Console.WriteLine(puzzleSize);
                                    }
                                    if (!maps.ContainsKey(id))
                                        maps.Add(id, fileName);
                                    else
                                        maps[id] = fileName;
                                }
                            }
                            break;
                        }


                    case "startgw":
                        {
                            Game.MsgTournaments.MsgSchedules.GuildWar.Proces = Game.MsgTournaments.ProcesType.Alive;
                            Game.MsgTournaments.MsgSchedules.GuildWar.Start();
                            break;
                        }
                    case "startsgw":
                        {
                            Game.MsgTournaments.MsgSchedules.SuperGuildWar.Start();
                            break;
                        }
                    case "finishsgw":
                        {
                            Game.MsgTournaments.MsgSchedules.SuperGuildWar.Proces = Game.MsgTournaments.ProcesType.Dead;
                            Game.MsgTournaments.MsgSchedules.SuperGuildWar.CompleteEndGuildWar();
                            break;
                        }
                    case "finishgw":
                        {
                            Game.MsgTournaments.MsgSchedules.GuildWar.Proces = Game.MsgTournaments.ProcesType.Dead;
                            Game.MsgTournaments.MsgSchedules.GuildWar.CompleteEndGuildWar();
                            break;
                        }

                    case "exit":
                        {
                            new Thread(new ThreadStart(Maintenance)).Start();
                            break;
                        }
                    case "forceexit":
                        {
                            ProcessConsoleEvent(0);

                            Environment.Exit(0);
                            break;
                        }
                    case "restart":
                        {
                            ProcessConsoleEvent(0);

                            System.Diagnostics.Process hproces = new System.Diagnostics.Process();
                            hproces.StartInfo.FileName = "Project_Terror_Light.exe";
                            hproces.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                            hproces.Start();

                            Environment.Exit(0);

                            break;
                        }

                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
        public static void Maintenance()
        {
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                OnMainternance = true;
                MyConsole.WriteLine("The server will be brought down for maintenance in (5 Minutes). Please log off immediately to avoid data loss.");
#if Arabic
                  MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 5minute0second. Please exitthe game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
              
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (5 Minutes). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (4 Minutes & 30 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                  MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 4minute30second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
               
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (4 Minutes & 30 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (4 Minutes & 00 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                  MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 4minute0second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
              
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (4 Minutes & 00 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (3 Minutes & 30 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                       MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 3minute30second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
         
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (3 Minutes & 30 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (3 Minutes & 00 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                  MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 3minute0second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
              
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (3 Minutes & 00 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (2 Minutes & 30 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                  MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 2minute30second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
              
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (2 Minutes & 30 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (2 Minutes & 00 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                        MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 2minute0second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
         
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (2 Minutes & 00 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (1 Minutes & 30 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                   MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 1minute30second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
             
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (1 Minutes & 30 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (1 Minutes & 00 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                 MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 1minute0second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
               
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (1 Minutes & 00 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 30);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                MyConsole.WriteLine("The server will be brought down for maintenance in (0 Minutes & 30 Seconds). Please log off immediately to avoid data loss.");
#if Arabic
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in 0minute30second. Please exit the game now.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
                
#else
                MsgMessage msg = new MsgMessage("The server will be brought down for maintenance in (0 Minutes & 30 Seconds). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 20);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
#if Arabic
                  MsgMessage msg = new MsgMessage("Server maintenance(2 minutes). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);
              
#else
                MsgMessage msg = new MsgMessage("Server maintenance(few minutes). Please log off immediately to avoid data loss.", "ALLUSERS", "GM", MsgMessage.MsgColor.red, MsgMessage.ChatMode.Center);

#endif
                SendGlobalPackets.Enqueue(msg.GetArray(stream));
            }
            Thread.Sleep(1000 * 10);
            ProcessConsoleEvent(0);

            Environment.Exit(0);
        }

        public unsafe static void Game_Receive(ServerSockets.SecuritySocket obj, ServerSockets.Packet stream)//ServerSockets.Packet data)
        {
            if (!obj.SetDHKey)
                CreateDHKey(obj, stream);
            else
            {
                try
                {
                    if (obj.Game == null)
                        return;
                    ushort PacketID = stream.ReadUInt16();

                    if (obj.Game.Player.CheckTransfer)
                        goto jmp;
                    if (obj.Game.PipeClient != null && PacketID != Game.GamePackets.Achievement)
                    {
                        if (PacketID == (ushort)Game.GamePackets.MsgOsShop
             || PacketID == (ushort)Game.GamePackets.SecondaryPassword
                      || PacketID >= (ushort)Game.GamePackets.LeagueOpt && PacketID <= (ushort)Game.GamePackets.LeagueConcubines
                      || PacketID == (ushort)Game.GamePackets.LeagueRobOpt)
                            goto jmp;

                        stream.Seek(stream.Size);
                        obj.Game.PipeClient.Send(stream);

                        if (PacketID != 1009)
                        {

                            return;
                        }
                        stream.Seek(4);
                    }

#if TEST
                    MyConsole.WriteLine("Receive -> PacketID: " + PacketID);
#endif

                //   Database.ServerDatabase.LoginQueue.Enqueue("[CallStack]" + MyConsole.log1(obj.Game.Player.Name, stream.Memory, stream.Size));
                jmp:
                    if (PacketID == 2171 || PacketID == 2088 || PacketID == 2096 || PacketID == 2090 || PacketID == 2093)
                    {
                        PokerHandler.Handler(obj.Game, stream);
                    }
                    else
                    {
                        Action<Client.GameClient, ServerSockets.Packet> hinvoker;
                        if (MsgInvoker.TryGetInvoker(PacketID, out hinvoker))
                        {
                            hinvoker(obj.Game, stream);
                        }
                        else
                        {
#if TEST
                        MyConsole.WriteLine("Not found the packet ----> " + PacketID);
#endif
                        }
                    }

                }
                catch (Exception e) { MyConsole.WriteException(e); }
                finally
                {
                    ServerSockets.PacketRecycle.Reuse(stream);
                }
            }

        }
        public unsafe static void CreateDHKey(ServerSockets.SecuritySocket obj, ServerSockets.Packet Stream)
        {
            try
            {
                byte[] buffer = new byte[36];
                bool extra = false;
                string text = System.Text.ASCIIEncoding.ASCII.GetString(obj.DHKeyBuffer.buffer, 0, obj.DHKeyBuffer.Length());
                if (!text.EndsWith("TQClient"))
                {
                    System.Buffer.BlockCopy(obj.EncryptedDHKeyBuffer.buffer, obj.EncryptedDHKeyBuffer.Length() - 36, buffer, 0, 36);
                    extra = true;
                }
                //                MyConsole.PrintPacketAdvanced(Stream.Memory, Stream.Size);

                string key;
                if (Stream.GetHandshakeReplyKey(out key))
                {
                    obj.SetDHKey = true;
                    obj.Game.DHKey.HandleResponse(key);
                    var compute_key = obj.Game.DHKeyExchance.PostProcessDHKey(obj.Game.DHKey.ToBytes());
                    //obj.Game.Crypto.SetIVs(new byte[8], new byte[8]);
                    obj.Game.Crypto.GenerateKey(compute_key);
                    obj.Game.Crypto.Reset();
                }
                else
                {
                    obj.Disconnect();
                    return;
                }
                if (extra)
                {

                    Stream.Seek(0);
                    obj.Game.Crypto.Decrypt(buffer, 0, Stream.Memory, 0, 36);
                    Stream.Size = buffer.Length;





                    Stream.Size = buffer.Length;
                    Stream.Seek(2);
                    ushort PacketID = Stream.ReadUInt16();
                    Action<Client.GameClient, ServerSockets.Packet> hinvoker;
                    if (MsgInvoker.TryGetInvoker(PacketID, out hinvoker))
                    {
                        hinvoker(obj.Game, Stream);
                    }
                    else
                    {
                        obj.Disconnect();

                        MyConsole.WriteLine("DH KEY Not found the packet ----> " + PacketID);

                    }
                }

            }
            catch (Exception e) { MyConsole.WriteException(e); }
        }
        public unsafe static void Game_Disconnect(ServerSockets.SecuritySocket obj)
        {

            if (obj.Game != null && obj.Game.Player != null)
            {
                try
                {
                    Console.WriteLine("[" + obj.Game.Player.UID + "-" + obj.Game.ClientFlag + "] " + obj.Game.Player.Name + " Try to disconnect");
                    Client.GameClient client;
                    if (Database.Server.GamePoll.TryGetValue(obj.Game.Player.UID, out client))
                    {
                        try
                        {
                            PokerHandler.Shutdown(client);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        if (client.OnInterServer)
                            return;
                        if ((client.ClientFlag & Client.ServerFlag.LoginFull) == Client.ServerFlag.LoginFull)
                        {
                            if (obj.Game.PipeClient != null)
                                obj.Game.PipeClient.Disconnect();
                            MyConsole.WriteLine("Client " + client.Player.Name + " was loggin out.");
                            using (var rec = new ServerSockets.RecycledPacket())
                            {
                                var stream = rec.GetStream();

                                try
                                {
                                    if (client.Player.InUnion)
                                    {
                                        client.Player.UnionMemeber.Owner = null;
                                    }
                                    client.CheckRouletteDisconnect();
                                    client.EndQualifier();




                                    if (client.Team != null)
                                        client.Team.Remove(client, true);
                                    if (client.Player.MyClanMember != null)
                                        client.Player.MyClanMember.Online = false;
                                    if (client.IsVendor)
                                        client.MyVendor.StopVending(stream);
                                    if (client.InTrade)
                                        client.MyTrade.CloseTrade();
                                    if (client.Player.MyGuildMember != null)
                                        client.Player.MyGuildMember.IsOnline = false;

                                    if (client.Player.ObjInteraction != null)
                                    {
                                        client.Player.InteractionEffect.AtkType = Game.MsgServer.MsgAttackPacket.AttackID.InteractionStopEffect;

                                        InteractQuery action = InteractQuery.ShallowCopy(client.Player.InteractionEffect);

                                        client.Send(stream.InteractionCreate(&action));

                                        client.Player.ObjInteraction.Player.OnInteractionEffect = false;
                                        client.Player.ObjInteraction.Player.ObjInteraction = null;
                                    }


                                    client.Player.View.Clear(stream);

                                }
                                catch (Exception e)
                                {
                                    MyConsole.WriteException(e);
                                    client.Player.View.Clear(stream);
                                }
                                finally
                                {
                                    client.ClientFlag &= ~Client.ServerFlag.LoginFull;
                                    client.ClientFlag |= Client.ServerFlag.Disconnect;
                                    client.ClientFlag |= Client.ServerFlag.QueuesSave;
                                    Database.ServerDatabase.LoginQueue.TryEnqueue(client);
                                }

                                try
                                {
                                    client.Player.Associate.OnDisconnect(stream, client);

                                    //remove mentor and apprentice
                                    if (client.Player.MyMentor != null)
                                    {
                                        Client.GameClient me;
                                        client.Player.MyMentor.OnlineApprentice.TryRemove(client.Player.UID, out me);
                                        client.Player.MyMentor = null;
                                    }
                                    client.Player.Associate.Online = false;
                                    lock (client.Player.Associate.MyClient)
                                        client.Player.Associate.MyClient = null;
                                    foreach (var clien in client.Player.Associate.OnlineApprentice.Values)
                                        clien.Player.SetMentorBattlePowers(0, 0);
                                    client.Player.Associate.OnlineApprentice.Clear();
                                    //done remove
                                }
                                catch (Exception e) { MyConsole.WriteLine(e.ToString()); }
                            }
                        }
                    }
                }
                catch (Exception e) { MyConsole.WriteLine(e.ToString()); }
            }
            else if (obj.Game != null)
            {
                if (obj.Game.ConnectionUID != 0)
                {

                    try
                    {
                        PokerHandler.Shutdown(obj.Game);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    Client.GameClient client;
                    Database.Server.GamePoll.TryRemove(obj.Game.ConnectionUID, out client);
                }
            }
        }


        public static bool NameStrCheck(string name, bool ExceptedSize = true)
        {
            if (name == null)
                return false;
            if (name == "")
                return false;
            string ValidChars = "[^A-Za-z0-9ء-ي*~.&.$]$";
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(ValidChars);
            if (r.IsMatch(name))
                return false;

            if (name.Contains('/'))
                return false;
            if (name.Contains(@"\"))
                return false;
            if (name.Contains(@"'"))
                return false;
            //    if (name.Contains('#'))
            //      return false;
            if (name.Contains("GM") ||
                name.Contains("PM") ||
                name.Contains("SYSTEM") ||
                name.Contains("{") || name.Contains("}") || name.Contains("[") || name.Contains("]"))
                return false;
            if (name.Length > 16 && ExceptedSize)
                return false;
            for (int x = 0; x < name.Length; x++)
                if (name[x] == 25)
                    return false;
            return true;
        }
        public static bool StringCheck(string pszString)
        {
            for (int x = 0; x < pszString.Length; x++)
            {
                if (pszString[x] > ' ' && pszString[x] <= '~')
                    return false;
            }
            return true;
        }
    }
}
