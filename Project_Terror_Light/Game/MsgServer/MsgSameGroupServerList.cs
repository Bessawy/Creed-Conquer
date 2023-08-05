using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Project_Terror_Light.Game.MsgServer
{
    public static class MsgSameGroupServerList
    {
        [ProtoContract]
        public class GroupServer
        {
            [ProtoMember(1, IsRequired = true)]
            public Server[] Servers;
        }
        [ProtoContract]
        public class Server
        {

            [ProtoMember(1, IsRequired = true)]
            public uint ServerID = 0;
            [ProtoMember(2, IsRequired = true)]
            public bool MyGroup = false;
            [ProtoMember(3, IsRequired = true)]
            public string Name = "";
            [ProtoMember(4, IsRequired = true)]
            public uint MapID = 0;
            [ProtoMember(5, IsRequired = true)]
            public uint X = 0;
            [ProtoMember(6, IsRequired = true)]
            public uint Y = 0;
            [ProtoMember(7, IsRequired = true)]
            public uint Unknowen = 0;
        }
        public static unsafe ServerSockets.Packet CreateGroupServerList(this ServerSockets.Packet stream, GroupServer obj)
        {
            stream.InitWriter();
            stream.ProtoBufferSerialize(obj);
            stream.Finalize(GamePackets.MsgSameGroupServerList);
            return stream;
        }
        public static byte[] SendPacket(GroupServer obj)
        {
            byte[] buffer;
            using (var ms = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<Server[]>(ms, obj.Servers);
                buffer = ms.ToArray();
            }
            byte[] array = new byte[4 + 8 + buffer.Length];
            System.Buffer.BlockCopy(buffer, 0, array, 4, buffer.Length);
            Poker.Packets.Packet.WriteUInt16((ushort)(array.Length - 8), 0, array);
            Poker.Packets.Packet.WriteUInt16(2500, 2, array);
            return array;
        }
        public static unsafe void GetGroupServerList(this ServerSockets.Packet stream, out GroupServer pQuery)
        {
            pQuery = new GroupServer();
            pQuery = stream.ProtoBufferDeserialize<GroupServer>(pQuery);
        }
    }
}