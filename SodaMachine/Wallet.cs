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
    }
}
