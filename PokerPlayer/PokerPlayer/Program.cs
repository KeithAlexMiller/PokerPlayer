using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
    class PokerPlayer
    {

        List<Card> playerHand = new List<Card>();

        public void DrawHand(List<Card> cards)
        {
            playerHand = cards;
        }

        // Enum of different hand types
        public enum HandType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }
        // Rank of hand that player holds
        public HandType HandRank
        {
            get
            {
                if (this.HasRoyalFlush())
                {
                    return HandType.RoyalFlush;
                }

                else if (this.HasStraightFlush())
                {
                    return HandType.StraightFlush;
                }
                else if (this.HasFourOfAKind())
                {
                    return HandType.FourOfAKind;
                }
                else if (this.HasFullHouse())
                {
                    return HandType.FullHouse;
                }
                else if (this.HasFlush())
                {
                    return HandType.Flush;
                }
                else if (this.HasStraight())
                {
                    return HandType.Straight;
                }
                else if (this.HasThreeOfAKind())
                {
                    return HandType.ThreeOfAKind;
                }
                else if (this.HasTwoPair())
                {
                    return HandType.TwoPair;
                }
                else if (this.HasPair())
                {
                    return HandType.OnePair;
                }
                else
                {
                    return HandType.HighCard;
                }
            }
        }
        // Constructor that isn't used
        public PokerPlayer() { }

        public Card HighCard()
        {
            return playerHand.OrderByDescending(x => x.Rank).First();
        }

        public bool HasPair()
        {
            return playerHand.GroupBy(x => x.Rank).Where(x => x.Count() == 2).Count() == 1;
        }
        public bool HasTwoPair()
        {
            return playerHand.GroupBy(x => x.Rank).Where(x => x.Count() == 2).Count() == 2;
        }
        public bool HasThreeOfAKind()
        {
            return playerHand.GroupBy(x => x.Rank).Where(x => x.Count() == 3).Count() == 1;
        }
        public bool HasStraight()
        {

            if (playerHand.Contains(Rank.Ace))
            {
                return (playerHand.Distinct().OrderBy(x => x.Rank).First().Rank - playerHand.Distinct().OrderBy(x => x.Rank).Select(4).Rank) == 3;
            }

            return (playerHand.Distinct().OrderByDescending(x => x.Rank).First().Rank - playerHand.Distinct().OrderByDescending(x => x.Rank).Last().Rank) == 4;

        }
        public bool HasFlush()
        {
            return playerHand.GroupBy(x => x.Suit).Where(x => x.Count() == 5).Count() == 1;
        }
        public bool HasFullHouse()
        {
            return HasPair() && HasThreeOfAKind();
        }
        public bool HasFourOfAKind()
        {
            return playerHand.GroupBy(x => x.Rank).Where(x => x.Count() == 4).Count() == 1;
        }
        public bool HasStraightFlush()
        {
            return HasStraight() && HasFlush();
        }
        public bool HasRoyalFlush()
        {
            return (playerHand.Distinct().OrderByDescending(x => x.Rank).First().Rank - playerHand.Distinct().OrderByDescending(x => x.Rank).Last().Rank) == 1 && playerHand.GroupBy(x => x.Suit).Where(x => x.Count() == 5).Count() == 1 && playerHand.OrderByDescending(x => x.Rank).First().Equals(Rank.Ace);
        }

    }
    class Deck
    {
        public int CardsRemaining
        {
            get
            {
                return DeckOfCards.Count();
            }
        }

        public List<Card> DeckOfCards;
        public List<Card> DiscardedCards;

        //calls number of decks
        public Deck()
            : this(1)
        {

        }

        public Deck(int numberOfDecks)
        {
            DeckOfCards = new List<Card>();
            DiscardedCards = new List<Card>();

            int suitCounter = 1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    DeckOfCards.Add(new Card(j, i));
                }
                suitCounter++;
            }
        }

        public List<Card> Deal(int numberOfCards)
        {
            List<Card> dealtCards = DeckOfCards.Take(numberOfCards).ToList<Card>();
            foreach (Card oneCard in dealtCards)
            {
                DeckOfCards.Remove(oneCard);
            }
            return dealtCards;
        }

        public void Shuffle()
        {
            Random rng = new Random();
            List<Card> tempDeck = new List<Card>();

            if (DeckOfCards.Count > 0)
            {
                Card tempCard = DeckOfCards.ElementAt(rng.Next(DeckOfCards.Count));
                DeckOfCards.Remove(tempCard);
                tempDeck.Add(tempCard);
            }
            DeckOfCards = tempDeck;
        }

        public void Discard(List<Card> cards)
        {
            DiscardedCards = new List<Card>();

            foreach (Card oneCard in cards)
            {
                DeckOfCards.Remove(oneCard);
                DiscardedCards.Add(oneCard);
            }
        }

    }

    // What makes a card?
    //     A card is comprised of it’s suit and its rank.  Both of which are enumerations.
    //     These enumerations should be "Suit" and "Rank"
    class Card
    {
        public Rank Rank
        {
            get;
            set;
        }
        public Suit Suit
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("Card is a {0} of {1}", this.Rank, this.Suit);
        }

        public Card(int rank, int suit) //needs to be data type of Rank and Suit
        {
            this.Rank = (Rank)rank;
            this.Suit = (Suit)suit;
        }
    }
            public enum Rank
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
        public enum Suit
        {
            Club,
            Heart,
            Diamond,
            Spade
        }

}
