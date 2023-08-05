using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_v2.WebServer
{
    public class ConnectionPoll
    {

        public class Connection
        {
            public uint UID;
            public uint hash;
            public string EarthAddress;
            public DateTime TimerJoin = new DateTime();
            public Connection(uint _uid, uint str, string mac)
            {
                hash = str;
                UID = _uid;
                EarthAddress = mac;
                TimerJoin = DateTime.Now;
            }
        }

        public Extensions.SafeDictionary<uint, Connection> Poll = new Extensions.SafeDictionary<uint, Connection>();


        public void Add(uint UID, uint hash, string macaddress)
        {
         
            if (Poll.ContainsKey(UID))
            {
                Poll[UID].UID = UID;
                Poll[UID].TimerJoin = DateTime.Now;
                Poll[UID].hash = hash;
                Poll[UID].EarthAddress = macaddress;
            }
            else
            {
                Connection conn = new Connection(UID, hash, macaddress);
                Poll.Add(conn.UID, conn);
            }
        }
        public bool CheckJoinPC(uint UID, string mac)
        {
            Connection conn;
            if (Poll.TryGetValue(UID, out conn))
            {
                if (conn.EarthAddress == mac)
                {
                    return true;
                }
            }

            return false;
        }
        public bool CheckJoin(uint UID, uint hash)
        {
            string str = null;
            Connection conn;
            if (Poll.TryGetValue(UID, out conn))
            {
                str += "UID : " + UID + " Found ";
                if (conn.hash == hash)
                {
                    str += "Hash Equals ";
                    if (conn.TimerJoin.AddSeconds(120) > DateTime.Now)
                    {
                        str += "Can Login";
                        MyConsole.WriteLine(str);
                        return true;
                    }
                }
            }
            str = "Couldn't Find UID :" + UID;
            MyConsole.WriteLine(str);
            return false;
        }
    
    }
}
