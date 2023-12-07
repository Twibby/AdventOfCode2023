using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_07 : DayScript2023
{

    class Hand
    {
        public string cards;
        public int bid;

        private string evalCards;
        public string EvalCards {  get { return evalCards; } }
        public string EvalCardsToInt
        {
            get
            {
                string result = "";
                foreach (char c in EvalCards) { result += ((int)c).ToString(); }
                return result;
            }
        }

        public Hand(string input, bool isP1)
        {
            string[] datas = input.Split(' ');
            
            this.cards = datas[0]; 
            this.bid = int.Parse(datas[1]);

            this.evalCards = isP1 ? evaluateCardsP1(cards) : evaluateCardsP2(cards);
        }

        public static string replaceLetters(string cards, bool isP1)
        {
            return cards.Replace('T', ':').Replace('J', isP1 ? ';' : '/').Replace('Q', '<').Replace('K', '=').Replace('A', '>');
        }

        /// <summary>
        /// Add a prefix to card string depengind on what it worths as poker combination
        /// </summary>
        string evaluateCardsP1(string cards)
        {
            cards = replaceLetters(cards, true);

            // get a list of distinct chars in cards with number of occurences
            var groups = cards.GroupBy(c => c).Select(c => (c.Key, c.Count())).ToList();
            if (groups.Count() == 1)    // 5 same
                return "9" + cards;

            if (groups.Count() == 5)    // 5 different - high card
                return "1" + cards;

            groups.Sort(delegate ((char,int) o1, (char, int) o2) { return o2.Item2.CompareTo(o1.Item2); });
            if (groups[0].Item2 == 4)   // 4 of a kind
                return "8" + cards;

            if (groups[0].Item2 == 3 && groups[1].Item2 == 2)   // full house
                return "7" + cards;

            if (groups[0].Item2 == 3) // 3 of a kind
                return "6" + cards;

            if (groups[0].Item2 == 2 && groups[1].Item2 == 2) // 2 pairs
                return "5" + cards;

            if (groups[0].Item2 == 2)   // 1 pair
                return "4" + cards;


            Debug.LogError("wtf " + cards);
            return "0" + cards;
        }

        string evaluateCardsP2(string cards)
        {
            if (!cards.Contains("J"))   // if no wild card, same eval as P1
                return evaluateCardsP1(cards);

            // count number of wild wards
            int jCount = cards.Count(c => c == 'J');
            
            // get a list of distinct chars in cards with number of occurences
            var groups = cards.GroupBy(c => c).Select(c => (c.Key, c.Count())).ToList();
            groups.RemoveAll(x => x.Key == 'J');    // remove wild cards (to add it later in biggest group)

            cards = replaceLetters(cards, false);

            if (groups.Count() <= 1)    // 5 same
                return "9" + cards;

            groups.Sort(delegate ((char, int) o1, (char, int) o2) { return o2.Item2.CompareTo(o1.Item2); });
            groups[0] = (groups[0].Key, groups[0].Item2 + jCount);

            if (groups[0].Item2 == 4)   // 4 of a kind
                return "8" + cards;

            if (groups[0].Item2 == 3 && groups[1].Item2 == 2)   // full house
                return "7" + cards;

            if (groups[0].Item2 == 3) // 3 of a kind
                return "6" + cards;

            if (groups[0].Item2 == 2 && groups[1].Item2 == 2)   // 2 pairs
                return "5" + cards;

            if (groups[0].Item2 == 2)  // 1 pair
                return "4" + cards;

            Debug.LogError("wtf " + cards);
            return "0" + cards;
        }

        public override string ToString() => cards + " -- " + bid.ToString();

    }

    protected override string part_1()
    {
        List<Hand> hands = new List<Hand>();
        foreach (string instruction in _input.Split('\n'))
        {
            hands.Add(new Hand(instruction, true));
        }

        hands.Sort(delegate (Hand h1, Hand h2) { return h1.EvalCardsToInt.CompareTo(h2.EvalCardsToInt); });

        long result = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            result += (i+1) * hands[i].bid;
        }

        return result.ToString();
    }
    
    protected override string part_2()
    {
        List<Hand> hands = new List<Hand>();
        foreach (string instruction in _input.Split('\n'))
        {
            hands.Add(new Hand(instruction, false));
        }

        hands.Sort(delegate (Hand h1, Hand h2) { return h1.EvalCardsToInt.CompareTo(h2.EvalCardsToInt); });

        long result = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            result += (i + 1) * hands[i].bid;
        }

        return result.ToString();
    }
}
