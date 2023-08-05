using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_Light.Game.MsgServer.AttackHandler
{
    public class BreathFocus
    {
        public unsafe static void Execute(InteractQuery Attack, Client.GameClient user, ServerSockets.Packet stream, Dictionary<ushort, Database.MagicType.Magic> DBSpells)
        {
            Database.MagicType.Magic DBSpell;
            MsgSpell ClientSpell;
            if (CheckAttack.CanUseSpell.Verified(Attack, user, DBSpells, out ClientSpell, out DBSpell))
            {
                MsgSpellAnimation MsgSpell = new MsgSpellAnimation(user.Player.UID, 0, user.Player.X, user.Player.Y, (ushort)Role.Flags.SpellID.BreathFocus, ClientSpell.Level, ClientSpell.UseSpellSoul);
                int MaxStamina = (byte)(user.Player.HeavenBlessing > 0 ? 150 : 100);
                if (user.Player.Stamina < MaxStamina)
                {
                    user.Player.Stamina = (byte)Math.Min(MaxStamina, (int)(user.Player.Stamina + (int)(DBSpell.Damage)));
                    user.Player.SendUpdate(stream, user.Player.Stamina, MsgUpdate.DataType.Stamina);
                }
                MsgSpell.Targets.Enqueue(new MsgSpellAnimation.SpellObj(user.Player.UID, 0, MsgAttackPacket.AttackEffect.None));
                Updates.UpdateSpell.CheckUpdate(stream, user, Attack, 200, DBSpells);
                MsgSpell.SetStream(stream);
                MsgSpell.Send(user);
            }
        }
    }
}
