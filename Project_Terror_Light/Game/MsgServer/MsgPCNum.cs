using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.Game.MsgServer
{
    public unsafe struct MsgPCNum
    {
        public ushort Length;
        public ushort PacketID;
        public uint Identifier;
        public string MacAddress;
        [PacketAttribute(Game.GamePackets.MsgPCNum)]
        public unsafe static void CheckPC(Client.GameClient client, ServerSockets.Packet packet)
        {
            client.PcLogin = new MsgPCNum() { Identifier = packet.ReadUInt32(), MacAddress = packet.ReadCString(12) };
            //if (WebServer.Proces.PollConnections.CheckJoinPC(client.PcLogin.Identifier, client.PcLogin.MacAddress) == false)
              //  client.Socket.Disconnect();
        }
    }
}
