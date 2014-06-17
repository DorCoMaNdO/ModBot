using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    class Raffle
    {
        private Dictionary<string, int> entrants = new Dictionary<string, int>();
        private List<string> pot = new List<string>();
        private List<string> winners = new List<string>();
        private Database db;
        private int ticketCost, maxTickets, totalTicketsPurchased, winnerToGet = 0;

        public Raffle(Database db, int ticketCost, int maxTickets)  {
            this.ticketCost = ticketCost;
            this.maxTickets = maxTickets;
            this.db = new Database();
        }

        public void buyEntries(String nick, int amount)
        {
            //check database to see if user has sufficient coins
            if (entrants.ContainsKey(nick) && amount <= maxTickets)
            {
                if (db.checkCurrency(nick) + (entrants[nick] * ticketCost) >= amount * ticketCost)
                {
                    if (entrants[nick] > amount)
                    {
                        db.addCurrency(nick, (entrants[nick] - amount) * ticketCost);
                        totalTicketsPurchased = totalTicketsPurchased - (entrants[nick] - amount);
                        entrants[nick] = amount;
                    }
                    else
                    {
                        db.removeCurrency(nick, (amount - entrants[nick]) * ticketCost);
                        totalTicketsPurchased = totalTicketsPurchased + (amount - entrants[nick]);
                        entrants[nick] = amount;
                    }
                }
            }
            else if (db.checkCurrency(nick) >= ticketCost * amount && amount <= maxTickets) {
                db.removeCurrency(nick, ticketCost * amount);
                entrants.Add(nick, amount);
                totalTicketsPurchased += amount;
            }
        }

        public void endRaffle()
        {
            rebuildParticipants();
            Random random = new Random();

            int i = random.Next(0, pot.Count);
            winners.Add(pot.ElementAt(i));
        }

        private void rebuildParticipants()
        {
            pot.Clear();
            if (winners.Count == 0)
            {
                foreach (var pair in entrants)
                {
                    for (int i = 0; i < pair.Value; i++)
                    {
                        pot.Add(pair.Key);
                    }
                }
            }
            else
            {
                foreach (var pair in entrants)
                {                  
                    for (int i = 0; i < pair.Value; i++)
                    {
                        if (!winners.Contains(pair.Key))
                        {
                            pot.Add(pair.Key);
                        }
                    }
                }
            }
        }

        public void cancel()
        {
            foreach (var pair in entrants)
            {
                db.addCurrency(pair.Key, pair.Value * ticketCost);
            }
            entrants.Clear();
        }

        public String getWinner()
        {
            string winner;
            if (winners.Count == winnerToGet)
            {
                winner = winners.ElementAt(winnerToGet - 1);
            }
            else
            {
                winner = winners.ElementAt(winnerToGet);
                winnerToGet++;
            }
            return winner;
        }

        public int getTotalTicketsPurchased()
        {

            return totalTicketsPurchased;
        }

        public int getPersonalTicketsPurchased(String nick)
        {
            return entrants[nick];
        }

        public int getMaxTickets()
        {
            return maxTickets;
        }

        public int getTicketCost()
        {
            return ticketCost;
        }

        public bool isInRaffle(String nick)
        {
            return entrants.ContainsKey(nick);
        }

        public int maxDraw()
        {
            return entrants.Count - winners.Count;
        }
    }
}
