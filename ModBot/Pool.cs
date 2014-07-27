using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    public static class Pool
    {
        private static Dictionary<string, int> winners = new Dictionary<string, int>();
        private static Dictionary<string, PoolUser> bets = new Dictionary<string, PoolUser>();
        private static int maxBet, totalBets;
        private static List<string> options;
        public static bool Running { get; private set; }
        public static bool Locked = false;

        public static void CreatePool(int max, List<string> lOptions)
        {
            maxBet = max;
            options = lOptions;
            totalBets = 0;
            bets.Clear();
            winners.Clear();
            Locked = false;
            Running = true;
        }

        public static bool placeBet(string nick, string option, int amount)
        {
            if (Running && options.Contains(option) && amount <= maxBet)
            {
                if (bets.ContainsKey(nick))
                {
                    if (Database.checkCurrency(nick) + bets[nick].betAmount >= amount)
                    {
                        if (bets[nick].betAmount < amount)
                        {
                            Database.addCurrency(nick, bets[nick].betAmount - amount);
                            bets[nick].betAmount = amount;
                            bets[nick].betOn = option;
                        }
                        else
                        {
                            Database.removeCurrency(nick, amount - bets[nick].betAmount);
                            bets[nick].betAmount = amount;
                            bets[nick].betOn = option;
                        }
                        return true;
                    }
                }
                else
                {
                    if (Database.checkCurrency(nick) >= amount)
                    {
                        Database.removeCurrency(nick, amount);
                        bets.Add(nick, new PoolUser(option, amount));
                        return true;
                    }
                }
            }
            return false;
        }

        public static void closePool(string winBet)
        {
            buildTotalBets();
            buildWinners(winBet);
            Locked = false;
            Running = false;
        }

        private static void buildWinners(string winBet)
        {
            winners.Clear();
            foreach (string nick in bets.Keys)
            {
                if (bets[nick].betOn == winBet)
                {
                    double temp = (double)(totalBets - getTotalBetsOn(winBet)) * ((double)bets[nick].betAmount / getTotalBetsOn(winBet));
                    int payout = (int)Math.Round(temp) + bets[nick].betAmount;
                    //Console.WriteLine("Pre-Round: " + temp + " Post Round and addition = " + payout);
                    if (winners.ContainsKey(nick))
                    {
                        Console.WriteLine(winners[nick].ToString());
                    }
                    else
                    {
                        winners.Add(nick, payout);
                        Database.addCurrency(nick, payout);
                    }
                }
            }
            Running = false;
        }

        private static void buildTotalBets()
        {
            totalBets = 0;
            foreach (string nick in bets.Keys)
            {
                totalBets += bets[nick].betAmount;
            }
        }

        public static int getTotalBets()
        {
            return totalBets;
        }

        public static int getNumberOfBets(string bet)
        {
            int numberOfBets = 0;
            foreach (string nick in bets.Keys)
            {
                if (bets[nick].betOn == bet)
                {
                    numberOfBets++;
                }
            }
            return numberOfBets;
        }

        public static int getTotalBetsOn(string bet)
        {
            int totalBetsOnOption = 0;
            foreach (string nick in bets.Keys)
            {
                if (bets[nick].betOn == bet)
                {
                    totalBetsOnOption += bets[nick].betAmount;
                }
            }

            return totalBetsOnOption;
        }

        public static Dictionary<string, int> getWinners()
        {
            return winners;
        }

        public static int getBetAmount(string nick)
        {
            return bets[nick].betAmount;
        }

        public static string getBetOn(string nick)
        {
            return bets[nick].betOn;
        }

        public static void cancel()
        {
            foreach (string nick in bets.Keys)
            {
                Database.addCurrency(nick, bets[nick].betAmount);
            }
            bets.Clear();
            winners.Clear();
            Running = false;
            Locked = false;
        }

        public static bool isInPool(string nick)
        {
            return bets.ContainsKey(nick);
        }

        public static string GetOptionFromNumber(int optionnumber)
        {
            optionnumber--;
            if (optionnumber >= 0 && optionnumber < options.Count)
            {
                return options[optionnumber];
            }
            return "";
        }
    }

    /// <summary>
    /// UTILITY OBJECT CLASS
    /// </summary>
    class PoolUser
    {
        public string betOn { get; set; }
        public int betAmount {get;set;}

        public PoolUser(string betOn, int betAmount) {
            this.betOn = betOn;
            this.betAmount = betAmount;
        }
    }
}
