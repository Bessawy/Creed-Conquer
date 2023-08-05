using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Terror_v2.Game.MsgServer.Poker
{
    /// <summary>
    /// https://codereview.stackexchange.com/questions/121973/defining-what-combination-the-user-have-in-poker/122025
    /// </summary>
    public class HandPowerCalculator
    {
        public enum HandPowerTypes : byte
        {
            /// <summary>
            /// Only a high card
            /// </summary>
            HighCard = 0,
            /// <summary>
            /// One Pair
            /// </summary>
            Pair = 1,
            /// <summary>
            /// Two Pair
            /// </summary>
            TwoPair = 2,
            /// <summary>
            /// Three of a kind (Trips)
            /// </summary>
            Trips = 3,
            /// <summary>
            /// Straight
            /// </summary>
            Straight = 4,
            /// <summary>
            /// Flush
            /// </summary>
            Flush = 5,
            /// <summary>
            /// FullHouse
            /// </summary>
            FullHouse = 6,
            /// <summary>
            /// Four of a kind
            /// </summary>
            FourOfAKind = 7,
            /// <summary>
            /// Straight Flush
            /// </summary>
            StraightFlush = 8,
            /// <summary>
            /// Royal Flush
            /// </summary>
            RoyalFlush = 9
        }
        public void CalculateHandPower(Client.GameClient[] clients, Extensions.MyList<Card> TableCards)
        {
            foreach (Client.GameClient client in clients)
            {
                if (client.PokerInfo.Fold)
                    continue;
                List<Card> WorkingList = new List<Card>(7);
                if (client.PokerInfo.Cards.Count == 1)
                {
                    WorkingList.Add(client.PokerInfo.Cards[0]);
                    //client.PokerInfo.SetHandValue((ushort)client.PokerInfo.Cards[0].GameCardID);
                    client.PokerInfo.SetHandValue((ushort)client.PokerInfo.Cards[0].GameCardID);
                    //client.PokerInfo.SetHandValueCardValue((ushort)client.PokerInfo.Cards[0].GameCardID, client.PokerInfo.Cards[0].GameCardType);
                }
                if (client.PokerInfo.Cards.Count == 2)
                {
                    WorkingList.Add(client.PokerInfo.Cards[1]);
                    client.PokerInfo.SetHandValue((ushort)((ushort)client.PokerInfo.Cards[0].GameCardID + (ushort)client.PokerInfo.Cards[1].GameCardID));
                    //client.PokerInfo.SetHandValueCardValue((ushort)client.PokerInfo.Cards[1].GameCardID, client.PokerInfo.Cards[1].GameCardType);
                }
                if (TableCards.Count >= 1)
                {
                    WorkingList.Add(TableCards[0]);
                }
                if (TableCards.Count >= 2)
                {
                    WorkingList.Add(TableCards[1]);
                }
                if (TableCards.Count >= 3)
                {
                    WorkingList.Add(TableCards[2]);
                }
                if (TableCards.Count >= 4)
                {
                    WorkingList.Add(TableCards[3]);
                }
                if (TableCards.Count == 5)
                {
                    WorkingList.Add(TableCards[4]);
                }
                if (this.IsRoyalFlush(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.RoyalFlush;
                    client.PokerInfo.Rank = 900.0;
                    continue;
                }
                else if (this.InStraight(WorkingList))
                {
                    if (this.SameSuit(WorkingList))
                    {
                        client.PokerInfo.HandPower = HandPowerTypes.StraightFlush;
                        client.PokerInfo.Rank = 800.0;
                        continue;
                    }
                    else
                    {
                        client.PokerInfo.HandPower = HandPowerTypes.Straight;
                        client.PokerInfo.Rank = 400.0;
                        continue;
                    }
                }
                else if (this.InFourOfAKind(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.FourOfAKind;
                    client.PokerInfo.Rank = 700.0;
                    continue;
                }
                else if (this.InFullHouse(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.FullHouse;
                    client.PokerInfo.Rank = 600.0;
                    continue;
                }
                else if (this.SameSuit(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.Flush;
                    client.PokerInfo.Rank = 500.0;
                    continue;
                }
                else if (InThreeOfAKind(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.Trips;
                    client.PokerInfo.Rank = 300.0;
                    continue;
                }
                else if (InTwoPairs(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.TwoPair;
                    client.PokerInfo.Rank = 200.0;
                    continue;
                }
                else if (InOnePair(WorkingList))
                {
                    client.PokerInfo.HandPower = HandPowerTypes.Pair;
                    client.PokerInfo.Rank = 100.0;
                    continue;
                }
                else
                {
                    client.PokerInfo.HandPower = HandPowerTypes.HighCard;
                    client.PokerInfo.Rank = 0.0;
                    continue;
                }
            }
        }
        private bool IsRoyalFlush(List<Card> Cards)
        {
            return InHighestSequince(Cards) && SameSuit(Cards);
        }
        private bool IsStraightFlush(List<Card> Cards)
        {
            return InStraight(Cards) && SameSuit(Cards);
        }
        private bool InHighestSequince(List<Card> Cards)
        {
            if (Cards.Count < 5)
                return false;
            ushort[] CardIDs = new ushort[Cards.Count];
            int x = 0;
            foreach (var card in Cards)
            {
                CardIDs[x] = card.GameCardID;
                x++;
            }
            if ((CardIDs.Contains((ushort)12) && CardIDs.Contains((ushort)11) && CardIDs.Contains((ushort)10) && CardIDs.Contains((ushort)9) && CardIDs.Contains((ushort)8)))
                return true;
            return false;
        }
        private bool InStraight(List<Card> Cards)
        {
            if (Cards.Count < 5)
                return false;
            ushort[] CardIDs = new ushort[Cards.Count];
            int x = 0;
            foreach (var card in Cards)
            {
                CardIDs[x] = card.GameCardID;
                x++;
            }
            if ((CardIDs.Contains((ushort)12) && CardIDs.Contains((ushort)11) && CardIDs.Contains((ushort)10) && CardIDs.Contains((ushort)9) && CardIDs.Contains((ushort)8))
                || (CardIDs.Contains((ushort)11) && CardIDs.Contains((ushort)10) && CardIDs.Contains((ushort)9) && CardIDs.Contains((ushort)8) && CardIDs.Contains((ushort)7))
                || (CardIDs.Contains((ushort)10) && CardIDs.Contains((ushort)9) && CardIDs.Contains((ushort)8) && CardIDs.Contains((ushort)7) && CardIDs.Contains((ushort)6))
                || (CardIDs.Contains((ushort)9) && CardIDs.Contains((ushort)8) && CardIDs.Contains((ushort)7) && CardIDs.Contains((ushort)6) && CardIDs.Contains((ushort)5))
                || (CardIDs.Contains((ushort)8) && CardIDs.Contains((ushort)7) && CardIDs.Contains((ushort)6) && CardIDs.Contains((ushort)5) && CardIDs.Contains((ushort)4))
                || (CardIDs.Contains((ushort)7) && CardIDs.Contains((ushort)6) && CardIDs.Contains((ushort)5) && CardIDs.Contains((ushort)4) && CardIDs.Contains((ushort)3))
                || (CardIDs.Contains((ushort)6) && CardIDs.Contains((ushort)5) && CardIDs.Contains((ushort)4) && CardIDs.Contains((ushort)3) && CardIDs.Contains((ushort)2))
                || (CardIDs.Contains((ushort)5) && CardIDs.Contains((ushort)4) && CardIDs.Contains((ushort)3) && CardIDs.Contains((ushort)2) && CardIDs.Contains((ushort)1)))
                return true;
            return false;
        }
        private bool SameSuit(List<Card> Cards)
        {
            if (Cards.Count < 5)
                return false;
            foreach (var card in Cards)
            {
                var CardType = card.GameCardType;
                if (Cards.FindAll(p => p.GameCardType == CardType).Count == 5)
                {
                    return true;
                }
            }
            return false;
        }
        private bool InOnePair(List<Card> Cards)
        {
            #if NewCalculation
            return Cards.GroupBy(c => c.GameCardID).Any(g => g.Count() >= 2);
#endif
            bool Acceptable = false;
            for (int x = 0; x < Cards.Count; x++)
            {
                var Card = Cards[x];
                if (Cards.Where(p => p.GameCardID == Card.GameCardID).Count() == 2)
                {
                    Acceptable = true;
                    break;
                }
                else
                    continue;
            }
            return Acceptable;
        }
        private bool InTwoPairs(List<Card> Cards)
        {
            if (Cards.Count < 4)
                return false;
            #if NewCalculation
            return Cards.GroupBy(c => c.GameCardID).Count() == 2 && Cards.GroupBy(c => c.GameCardID).Any(g => g.Count() == 2);
#endif
            bool Acceptable = false;
            ushort SelectedID = 655;
            for (int x = 0; x < Cards.Count; x++)
            {
                var Card = Cards[x];
                if (Cards.Where(p => p.GameCardID == Card.GameCardID && p.GameCardID != SelectedID).Count() == 2)
                {
                    if (SelectedID == 655)
                    {
                        SelectedID = Card.GameCardID;
                        continue;
                    }
                    else
                    {
                        Acceptable = true;
                        break;
                    }
                }
            }
            return Acceptable;
        }
        private bool InThreeOfAKind(List<Card> Cards)
        {
            if (Cards.Count < 3)
                return false;
            #if NewCalculation
            return Cards.GroupBy(c => c.GameCardID).Any(g => g.Count() == 3);
#endif
            bool Acceptable = false;
            for (int x = 0; x < Cards.Count; x++)
            {
                var Card = Cards[x];
                if (Cards.Where(p => p.GameCardID == Card.GameCardID).Count() == 3)
                {
                    Acceptable = true;
                    break;
                }
                else
                    continue;
            }
            return Acceptable;
        }
        private bool InFourOfAKind(List<Card> Cards)
        {
            if (Cards.Count < 4)
                return false;
            #if NewCalculation
            return Cards.GroupBy(c => c.GameCardID).Any(g => g.Count() >= 4);
#endif
            bool Acceptable = false;
            for (int x = 0; x < Cards.Count; x++)
            {
                var Card = Cards[x];
                if (Cards.Where(p => p.GameCardID == Card.GameCardID).Count() == 4)
                {
                    Acceptable = true;
                    break;
                }
                else
                    continue;
            }
            return Acceptable;
        }
        private bool InFullHouse(List<Card> Cards)
        {
            if (Cards.Count < 5)
                return false;
#if NewCalculation
            return Cards.GroupBy(c => c.GameCardID).Count() == 2 && Cards.GroupBy(c => c.GameCardID).Any(g => g.Count() == 3);
#endif
            bool Acceptable = false;
            ushort SelectedID = 655;
            for (int x = 0; x < Cards.Count; x++)
            {
                var Card = Cards[x];
                if (Cards.Where(p => p.GameCardID == Card.GameCardID && p.GameCardID != SelectedID && SelectedID == 655).Count() == 3)
                {
                    if (SelectedID == 655)
                    {
                        SelectedID = Card.GameCardID;
                        continue;
                    }
                    else
                        break;
                }
                else if (Cards.Where(p => p.GameCardID == Card.GameCardID && p.GameCardID != SelectedID).Count() == 2)
                {
                    if (SelectedID == 655)
                    {
                        Acceptable = false;
                        break;
                    }
                    else
                    {
                        Acceptable = true;
                        break;
                    }
                }
                else
                    return false;
            }
            return Acceptable;
        }
    }
}
