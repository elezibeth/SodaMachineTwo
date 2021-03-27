using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> walletCoins;
        //Constructor (Spawner)
        public Wallet()
        {
            walletCoins = new List<Coin>();
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillRegister()
        {
            for (int i = 10; i > 0; i--)
            {
                Quarter quarter = new Quarter();
                walletCoins.Add(quarter);
            }
            for (int i = 10; i > 0; i--)
            {
                Dime dime = new Dime();
                walletCoins.Add(dime);
            }
            for (int i = 20; i > 0; i--)
            {
                Nickel nickel = new Nickel();
                walletCoins.Add(nickel);
            }
            for (int i = 50; i > 0; i--)
            {
                Penny penny = new Penny();
                walletCoins.Add(penny);
            }

        }
        public Coin getPenny()
        {
            Penny penny = new Penny();
            bool coinExists = walletCoins.Contains(penny);
            if (coinExists == true)
            {
                walletCoins.Remove(penny);
                return penny;
            }
            else
            {
                return null;
            }
                    
        }
        public Coin getNickel()
        {
            Nickel nickel = new Nickel();
            bool coinExists = walletCoins.Contains(nickel);
            if (coinExists == true)
            {
                walletCoins.Remove(nickel);
                return nickel;
            }
            else
            {
                return null;
            }

        }
        public Coin getQuarter()
        {
            Quarter quarter = new Quarter();
            bool coinExists = walletCoins.Contains(quarter);
            if (coinExists == true)
            {
                walletCoins.Remove(quarter);
                return quarter;
            }
            else
            {
                return null;
            }

        }
        public Coin getDime()
        {
            Dime dime = new Dime();
            bool coinExists = walletCoins.Contains(dime);
            if (coinExists == true)
            {
                walletCoins.Remove(dime);
                return dime;
            }
            else
            {
                return null;
            }

        }
    }
}
