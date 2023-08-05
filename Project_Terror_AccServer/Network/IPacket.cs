// * Created by Senior Mido
// * Copyright © 2018-2019
// * Dark|Light - Project

namespace AccServer.Interfaces
{
    public unsafe interface IPacket
    {
        byte[] ToArray();
        void Deserialize(byte[] buffer);
    }
}