using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_v2.Game.MsgServer.Poker
{
   public static class MsgPokerPlayerInfo
    {
       public static unsafe ServerSockets.Packet PokerPlayerInfoCreate(this ServerSockets.Packet stream, PlayerInfo user)
       {
           stream.InitWriter();
           stream.Write((byte)1);
           stream.Write((ushort)user.State);
           stream.Write((ushort)user.Location);
           stream.Write(user.Owner.MyPokerTable.Noumber);
           stream.Write(user.Owner.Player.UID);
           stream.Write(0);
           stream.Write(0);
           stream.Write((uint)(user.Owner.MyPokerTable.Noumber > 200 ? 2 : 1));
           stream.Finalize(GamePackets.PokerPlayerInfo);
           return stream;
       }
       
    }
   public class PlayerInfo
   {
       
       public Client.GameClient Owner;
       public bool AllIn = false;
       public bool Check = false;
       public bool Fold = false;

       public bool WasBetted = false;
       public ushort State = 0;
       public uint Location = 0;
       public ulong MyBet = 0;
       public ulong RoundBet = 0;
       public double Rank = 0;
       public bool SetMyBet = false;
       public ulong MyReward = 0;
       public ushort FinishInRound = 0;
       public ulong MyRewardLost = 0;
       public ushort HandCardValue { get; private set; }
       public void SetHandValue(ushort Value)
       {
           this.HandCardValue = 0;
           this.HandCardValue = Value;
       }
       public void SetHandValueCardValue(ushort Value, Project_Terror_v2.Game.MsgServer.Poker.Card.CardType CardType)
       {
           //this.HandCardValue = 0;
           if (Value == 0)
               return;
           switch (CardType)
           {
               case Card.CardType.Spades:
                   {
                       this.HandCardValue += 3;
                       this.HandCardValue += Value;
                       break;
                   }
               case Card.CardType.Hearts:
                   {
                       this.HandCardValue += 2;
                       this.HandCardValue += Value;
                       break;
                   }
               case Card.CardType.Diamonds:
                   {
                       this.HandCardValue += 1;
                       this.HandCardValue += Value;
                       break;
                   }
               
               case Card.CardType.Clubs:
                   {
                       this.HandCardValue += 0;
                       this.HandCardValue += Value;
                       break;
                   }
               default:
                   {
                       break;
                   }
           }
       }
       public uint UID = 0;
       public MsgPokerHand.PokerCallTypes HandType = MsgPokerHand.PokerCallTypes.Call;
        public HandPowerCalculator.HandPowerTypes HandPower = HandPowerCalculator.HandPowerTypes.HighCard;
       public List<Card> Cards = new List<Card>();
       public Card DeadCard = null;

       public uint ConquerPoints
       {
           get { return Owner.Player.ConquerPoints; }
       }
       public long Money
       {
           get { return Owner.Player.Money; }
       }
       public PlayerInfo(Client.GameClient _Onwer)
       {
           UID = _Onwer.Player.UID;
           Owner = _Onwer;
       }

   }
}
