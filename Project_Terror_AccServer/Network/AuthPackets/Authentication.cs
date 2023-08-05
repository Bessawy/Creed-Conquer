using System;
using System.IO;
using System.Text;


namespace AccServer.Network.AuthPackets
{
    public unsafe class Authentication : Interfaces.IPacket
    {
        public string Username;
        public string Password;
        public string Server;
        public string Mac;

        public Authentication()
        {
        }
        private byte[] _array;
        public string ReadString(int length, int offset)
        {
            // Read the value:
            fixed (byte* ptr = _array)
                return new string((sbyte*)ptr, offset, length, Encoding.GetEncoding(1252)).TrimEnd('\0');
        }
        public void Deserialize(byte[] buffer)
        {
            if (buffer.Length == 312)
            {
                ushort length = BitConverter.ToUInt16(buffer, 0);

                if (length == 312)
                {
                    ushort type = BitConverter.ToUInt16(buffer, 2);
                    byte[] temp = new byte[16];
                    if (type == 1542)
                    {
                        MemoryStream MS = new MemoryStream(buffer);
                        BinaryReader BR = new BinaryReader(MS);
                        BR.ReadUInt16();
                        BR.ReadUInt16();
                        Username = Encoding.Default.GetString(BR.ReadBytes(32));
                        Username = Username.Replace("\0", "");
                        //BR.ReadBytes(36);
                        BR.ReadBytes(37);
                        byte[] PasswordArray = BR.ReadBytes(32);
                        Password = Encoding.Default.GetString(PasswordArray);
                        BigBOs_AccountServer.Packets.Cryptography.LoaderEncryption.Decrypt(PasswordArray);
                        Password = Encoding.Default.GetString(PasswordArray);
                        Password = Password.Replace("\0", "");
                        BR.ReadBytes(31);
                        Server = Encoding.Default.GetString(BR.ReadBytes(16));
                        Server = Server.Replace("\0", "");
                        Mac = Encoding.Default.GetString(BR.ReadBytes(16));
                        Mac = Mac.Replace("\0", "");
                        BR.Close();
                        MS.Close();

                        //MemoryStream MS = new MemoryStream(buffer);
                        //BinaryReader BR = new BinaryReader(MS);
                        //BR.ReadUInt16();
                        //BR.ReadUInt16();
                        //Username = Encoding.Default.GetString(BR.ReadBytes(32));
                        //Username = Username.Replace("\0", "");
                        //BR.ReadBytes(36);
                        //Password = Encoding.Default.GetString(BR.ReadBytes(32));
                        //Password = Password.Replace("\0", "");
                        //BR.ReadBytes(32);
                        //Server = Encoding.Default.GetString(BR.ReadBytes(16));
                        //Server = Server.Replace("\0", "");
                        //Mac = Encoding.Default.GetString(BR.ReadBytes(16));
                        //Mac = Mac.Replace("\0", "");
                        //BR.Close();
                        //MS.Close();
                    }
                }
            }
        }
        public void Deserialize2(byte[] buffer)
        {
            if (buffer.Length == 312)
            {
                ushort length = BitConverter.ToUInt16(buffer, 0);

                if (length == 312)
                {
                    ushort type = BitConverter.ToUInt16(buffer, 2);
                    byte[] temp = new byte[16];
                    if (type == 1542)
                    {
                        MemoryStream MS = new MemoryStream(buffer);
                        BinaryReader BR = new BinaryReader(MS);

                        BR.ReadUInt16();
                        BR.ReadUInt16();
                        Username = Encoding.Default.GetString(BR.ReadBytes(32));
                        Username = Username.Replace("\0", "");
                        BR.ReadBytes(36);
                        var PasswordArray = BR.ReadBytes(32);
                        TQ_Host_ME.Network.Cryptography.LoaderEncryption.Decrypt(PasswordArray, 32);
                        Password = Encoding.Default.GetString(PasswordArray);
                        Password = Password.Replace("\0", "");
                        BR.ReadBytes(32);
                        Server = Encoding.Default.GetString(BR.ReadBytes(16));
                        Server = Server.Replace("\0", "");
                        Mac = Encoding.Default.GetString(BR.ReadBytes(16));
                        Mac = Mac.Replace("\0", "");
                        BR.Close();
                        MS.Close();
                    }
                }
            }
        }
        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }


    }
}