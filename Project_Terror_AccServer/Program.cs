// * Created by Senior Mido
// * Copyright © 2018-2019
// * Dark|Light - Project

using System;
using AccServer.Network;
using AccServer.Database;
using System.Windows.Forms;
using AccServer.Network.Sockets;
using AccServer.Network.AuthPackets;
using System.Threading.Tasks;
using AccServer.Network.Protaction;

namespace AccServer
{
    public unsafe class Program
    {
        public static Counter EntityUID;
        public static FastRandom Random = new FastRandom();
        public static ServerSocket AuthServer;
        public static World World;
        public static ushort Port = 9960;//9958
        private static object SyncLogin;
        private static System.Collections.Concurrent.ConcurrentDictionary<uint, int> LoginProtection;
        private const int TimeLimit = 10000;
        private static void WorkConsole()
        {
            while (true)
            {
                try
                {
                    CommandsAI(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        private static void WriteHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"+-----------------------------------------------------------------------------+");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"|                    All Rights Reserved By  Creed                            |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"|                          Copyright (C) 2018 - 2019                          |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"+-----------------------------------------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        public static string ConnectionString = "Server='localhost';Database='acc_server';Username='root';Password='Baskota123';Pooling=true; Max Pool Size = 32; Min Pool Size = 300; Connect Timeout=15";
        static void Main(string[] args)
        {
            Console.Title = "Account Server";
            WriteHeader();
            Database.DataHolder.CreateConnection();
            World = new World();
            World.Init();
            EntityUID = new Counter(0);
            LoginProtection = new System.Collections.Concurrent.ConcurrentDictionary<uint, int>();
            SyncLogin = new object();
            Database.Server.Load();
            Network.Cryptography.AuthCryptography.PrepareAuthCryptography();
            AuthServer = new ServerSocket();
            AuthServer.OnClientConnect += AuthServer_OnClientConnect;
            AuthServer.OnClientReceive += AuthServer_OnClientReceive;
            AuthServer.OnClientDisconnect += AuthServer_OnClientDisconnect;
            AuthServer.Enable(Port, "0.0.0.0");
            Console.WriteLine("Connection Port " + Port);
            Console.WriteLine("");
            Task.Factory.StartNew(() =>
            {
                Socket soc = new Socket();
                soc.init();
            });
            WorkConsole();
            CommandsAI(Console.ReadLine());
        }
        public static void CommandsAI(string command)
        {
            if (command == null) return;
            string[] data = command.Split(' ');
            switch (data[0])
            {
                case "@memo":
                    {
                        var proc = System.Diagnostics.Process.GetCurrentProcess();
                        Console.WriteLine("Thread count: " + proc.Threads.Count);
                        Console.WriteLine("Memory set(MB): " + ((double)((double)proc.WorkingSet64 / 1024)) / 1024);
                        proc.Close();
                        break;
                    }
                case "@a":
                    {
                        Console.Clear();
                        break;
                    }
                case "@restart":
                    {
                        AuthServer.Disable();
                        Application.Restart();
                        Environment.Exit(0);
                        break;
                    }
            }
        }

        private static void AuthServer_OnClientReceive(byte[] buffer, int length, ClientWrapper arg3)
        {
            var player = arg3.Connector as Client.AuthClient;
            player.Cryptographer.Decrypt(buffer, length);
            player.Queue.Enqueue(buffer, length);
            while (player.Queue.CanDequeue())
            {
                byte[] packet = player.Queue.Dequeue();
                ushort len = BitConverter.ToUInt16(packet, 0);
                ushort id = BitConverter.ToUInt16(packet, 2);
                if (len == 312)
                {
                    player.Info = new Authentication();
                    player.Info.Deserialize2(packet);
                    player.Account = new AccountTable(player.Info.Username);
                    Database.ServerInfo Server = null;
                    Forward Fw = new Forward();
                    if (Database.Server.Servers.TryGetValue(player.Info.Server, out Server))
                    {
                        if (!player.Account.exists)
                        {
                            Fw.Type = Forward.ForwardType.WrongAccount;
                        }
                        if (player.Account.Password == player.Info.Password && player.Account.exists)
                        {
                            Fw.Type = Forward.ForwardType.Ready;
                            if (player.Account.EntityID == 0)
                            {
                                using (MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT))
                                {
                                    cmd.Select("configuration");
                                    using (MySqlReader r = new MySqlReader(cmd))
                                    {
                                        if (r.Read())
                                        {
                                            EntityUID = new Counter(r.ReadUInt32("EntityID"));
                                            player.Account.EntityID = EntityUID.Next;
                                            using (MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.UPDATE).Update("configuration")
                                            .Set("EntityID", player.Account.EntityID)) cmd2.Execute();
                                            player.Account.Save();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            player.Info.Deserialize(packet);
                            player.Account = new AccountTable(player.Info.Username);
                            if (player.Account.Password == player.Info.Password && player.Account.exists)
                            {
                                Fw.Type = Forward.ForwardType.Ready;
                                if (player.Account.EntityID == 0)
                                {
                                    using (MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT))
                                    {
                                        cmd.Select("configuration");
                                        using (MySqlReader r = new MySqlReader(cmd))
                                        {
                                            if (r.Read())
                                            {
                                                EntityUID = new Counter(r.ReadUInt32("EntityID"));
                                                player.Account.EntityID = EntityUID.Next;
                                                using (MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.UPDATE).Update("configuration")
                                                .Set("EntityID", player.Account.EntityID)) cmd2.Execute();
                                                player.Account.Save();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Fw.Type != Forward.ForwardType.Ready)
                        {
                            Fw.Type = Forward.ForwardType.InvalidInfo;
                        }
                        if (player.Account.Banned)
                        {
                            Fw.Type = Forward.ForwardType.Banned;
                        }
                        lock (SyncLogin)
                        {
                            if (Fw.Type == Forward.ForwardType.Ready)
                            {
                                try
                                {
                                    using (MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT))
                                    {
                                        //cmd.Select("accounts").Where("EntityID", player.Account.EntityID);
                                        cmd.Select("macs").Where("id", player.Account.EntityID);
                                        using (MySqlReader r = new MySqlReader(cmd, ConnectionString))
                                        {
                                            if (r.Read())
                                            {
                                                using (MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.UPDATE).Update("macs")
                                        .Set("id", player.Account.EntityID).Where("mac", player.Info.Mac)) cmd2.Execute(ConnectionString);
                                                using (MySqlCommand cmd3 = new MySqlCommand(MySqlCommandType.UPDATE).Update("accounts")
                                               .Set("Online", 1).Where("EntityID", player.Account.EntityID)) cmd3.Execute(ConnectionString);

                                                //    using (MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.UPDATE).Update("macs")
                                                //    .Set("mac", player.Info.Mac).Where("id", player.Account.EntityID)) cmd2.Execute(ConnectionString);
                                            }
                                            else
                                            {
                                                using (MySqlCommand cmd3 = new MySqlCommand(MySqlCommandType.UPDATE).Update("accounts")
                                                 .Set("Online", 1).Where("EntityID", player.Account.EntityID)) cmd3.Execute(ConnectionString);

                                                using (MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.INSERT).Insert("macs")
                                                         .Insert("mac", player.Info.Mac).Insert("id", player.Account.EntityID)) cmd2.Execute(ConnectionString);
                                                Console.WriteLine("New Mac Address Added  {0}!.", player.Info.Mac);
                                                //using (MySqlCommand cmd3 = new MySqlCommand(MySqlCommandType.INSERT).Insert("playersonline")
                                                //               .Insert("UID", player.Account.EntityID)
                                                //               .Insert("NickName", player.Info.Username)
                                                //               .Insert("Online",1))
                                                //               cmd3.Execute(ConnectionString);
                                                //Console.WriteLine(""+player.Info.Username+" has been online");

                                                //    using (MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.INSERT).Insert("macs")
                                                //   .Insert("mac", player.Info.Mac).Insert("id", player.Account.EntityID)) cmd2.Execute(ConnectionString);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                                TransferCipher transferCipher = new TransferCipher("EypKhLvYJ3zdLCTyz9Ak8RAgM78tY5F32b7CUXDuLDJDFBH8H67BWy9QThmaN5VS", "MyqVgBf3ytALHWLXbJxSUX4uFEu3Xmz2UAY9sTTm8AScB7Kk2uwqDSnuNJske4BJ", "127.0.0.1");
                                uint[] encrypted = transferCipher.Encrypt(new uint[] { player.Account.EntityID, (uint)player.Account.State });
                                Fw.Identifier = encrypted[0];
                                Fw.State = (uint)encrypted[1];
                                Fw.IP = Server.IP;
                                Fw.Port = Server.Port;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("{0} has been login successfully to server {1}! IP:[{2}].", player.Info.Username, player.Info.Server, player.IP);
                            }
                            player.Send(Fw);
                        }
                    }
                }
            }

        }
        private static void AuthServer_OnClientDisconnect(ClientWrapper obj)
        {
            obj.Disconnect();
        }
        private static void AuthServer_OnClientConnect(ClientWrapper obj)
        {
            Client.AuthClient authState;
            obj.Connector = (authState = new Client.AuthClient(obj));
            authState.Cryptographer = new Network.Cryptography.AuthCryptography();
            Network.AuthPackets.PasswordCryptographySeed pcs = new PasswordCryptographySeed();
            pcs.Seed = Program.Random.Next();
            authState.PasswordSeed = pcs.Seed;
            authState.Send(pcs);
        }
    }
}