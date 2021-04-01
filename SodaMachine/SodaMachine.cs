﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
        //Coins: 20 quarters, 10 dimes, 20 nickels, 50 pennies
        for(int i = 20; i > 0; i--)
            {
                Quarter quarter = new Quarter();
                _register.Add(quarter);
            }
            for (int i = 10; i > 0; i--)
            {
                Dime dime = new Dime();
                _register.Add(dime);
            }
            for (int i = 20; i > 0; i--)
            {
                Nickel nickel = new Nickel();
                _register.Add(nickel);
            }
            for (int i = 50; i > 0; i--)
            {
                Penny penny = new Penny();
                _register.Add(penny);
            }



        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
            for (int i = 20; i > 0; i--)
            {
                OrangeSoda orangeSoda = new OrangeSoda();
                Cola cola = new Cola();
                RootBeer rootBeer = new RootBeer();
                _inventory.Add(orangeSoda);
                _inventory.Add(cola);
                _inventory.Add(rootBeer);
            }

        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
           
            //get selection
            string chosenSoda = UserInterface.SodaSelection(_inventory);
            Can can = GetSodaFromInventory(chosenSoda);

            //get payment from customer
            List<Coin> payment = new List<Coin>();

            List<string> coinNamesChosenByCustoemr = new List<string>();
            coinNamesChosenByCustoemr = UserInterface.CoinSelection(can, customer.Wallet.walletCoins);
            List<Coin> coinsChosenByCustomer = new List<Coin>();
            foreach(string coinName in coinNamesChosenByCustoemr)
            {
                Coin coin = customer.GetCoinFromWallet(coinName);
                coinsChosenByCustomer.Add(coin);
            }

            //calculateTransaction
            CalculateTransaction(coinsChosenByCustomer, can, customer);
            
        }
       
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {

            switch (nameOfSoda)
            {
                case "Cola":
                    Cola cola = new Cola();
                    _inventory.Add(cola);
                    bool inStock = DetermineIfCanInInventory(nameOfSoda);
                    if(inStock == true)
                    {
                        Console.WriteLine("Cola in stock!");
                        _inventory.Remove(cola);
                        return cola;

                    }
                    else
                    {
                        return null;
                    }


                case "Orange Soda":
                    OrangeSoda orangeSoda = new OrangeSoda();
                    _inventory.Add(orangeSoda);
                    inStock = DetermineIfCanInInventory(nameOfSoda);
                    if (inStock == true)
                    {
                        Console.WriteLine("Orange soda in stock!");
                        _inventory.Remove(orangeSoda);
                        return orangeSoda;

                    }
                    else
                    {
                        return null;
                    }
                case "Root Beer":
                    RootBeer rootBeer = new RootBeer();
                    _inventory.Add(rootBeer);
                    inStock = DetermineIfCanInInventory(nameOfSoda);
                    if (inStock == true)
                    {
                        Console.WriteLine("RootBeer in stock!");
                        _inventory.Remove(rootBeer);
                        return rootBeer;

                    }
                    else
                    {
                        return null;
                    }
                default:
                    return null;

            }
          
        }
        private bool DetermineIfCanInInventory(string sodaName)
        {
            bool isInStock = false;
            switch (sodaName)
            {
                case "Cola":
                   foreach(Can can in _inventory)
                    {
                        if(can.name == "Cola")
                        {
                            isInStock = true;
                        }
                    }
                    break;

                case "Orange Soda":
                    OrangeSoda orangeSoda = new OrangeSoda();
                    foreach(Can can in _inventory)
                    {
                        if(can.name == "Orange Soda")
                        {
                            isInStock = true;
                        }

                    }
                    break;
                case "Root Beer":
                    foreach (Can can in _inventory)
                    {
                        if (can.name == "RootBeer")
                        {

                            isInStock = true;

                        }

                    }
                    break;
                   

            }
            return isInStock;

        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Despense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Despense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Despense soda.
        //If the payment does not meet the cost of the soda: despense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double paymentValue = TotalCoinValue(payment);
            double change = DetermineChange(paymentValue, chosenSoda.Price);
            double registerValue = TotalCoinValue(_register);
             if(paymentValue > chosenSoda.Price && _inventory.Contains(chosenSoda) && registerValue >= change)
            {
                DispenseSodaAndChange(chosenSoda, change, customer, payment);
            }
            if (paymentValue > chosenSoda.Price && _inventory.Contains(chosenSoda) == false && registerValue >= change)
            {
               foreach(Coin coin in payment)
                {
                    customer.Wallet.walletCoins.Add(coin);
                }
            }
            if(paymentValue > chosenSoda.Price && registerValue < change)
                {
                    foreach(Coin coin in payment)
                    {
                        customer.Wallet.walletCoins.Add(coin);
                    }
                }
             if(paymentValue == chosenSoda.Price && _inventory.Contains(chosenSoda))
            {
                DispenseSoda(chosenSoda, customer, payment);
            }
            if (paymentValue == chosenSoda.Price && _inventory.Contains(chosenSoda) == false)
            {
                foreach(Coin coin in payment)
                {
                    customer.Wallet.walletCoins.Add(coin);
                }
            }
            if (paymentValue < chosenSoda.Price)
            {
                foreach(Coin coin in payment)
                {
                    customer.Wallet.walletCoins.Add(coin);
                }
            }
        }
        private void DispenseSodaAndChange(Can chosenSoda, double change, Customer customer, List<Coin> payment)
        {
            _inventory.Remove(chosenSoda);
            customer.AddCanToBackpack(chosenSoda);
            List<Coin> changeCoins = new List<Coin>();
            changeCoins = GatherChange(change);
            foreach (Coin choin in changeCoins)
            {
                customer.Wallet.walletCoins.Add(choin);
            }
            foreach (Coin coin in payment)
            {
                _register.Add(coin);
            }

        }
        private void DispenseSoda(Can chosenSoda, Customer customer, List<Coin> payment)
        {
            _inventory.Remove(chosenSoda);
            customer.Backpack.cans.Add(chosenSoda);
            foreach (Coin coin in payment)
            {
                _register.Add(coin);
            }
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            //determine coins needed
            List<Coin> gatheredCoins = new List<Coin>();
            double price = changeValue;
            while (price >= .25 && RegisterHasCoin("Quarter"))
            {
                Quarter quarter = new Quarter();
                gatheredCoins.Add(quarter);
                _register.Remove(quarter);
                price -= .25;
            }
            while (price >= .1 && RegisterHasCoin("Dime"))
            {
                Dime dime = new Dime();
                gatheredCoins.Add(dime);
                _register.Remove(dime);
                price -= .1;
            }
            while (price >= .05 && RegisterHasCoin("Nickel"))
            {
                Nickel nickel = new Nickel();
                gatheredCoins.Add(nickel);
                _register.Remove(nickel);
                price -= .05;
            }
            while (price >= .01 && RegisterHasCoin("Penny"))
            {
                Penny penny = new Penny();
                gatheredCoins.Add(penny);
                _register.Remove(penny);
                price -= .01;
            }
            return gatheredCoins;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            switch (name)
            {
                case "Penny":
                    Penny penny = new Penny();
                    bool hasCoin = _register.Contains(penny);
                    return hasCoin;
                case "Nickel":
                    Nickel nickel = new Nickel();
                    bool hasCoin2 = _register.Contains(nickel);
                    return hasCoin2;
                case "Dime":
                    Dime dime = new Dime();
                    bool hasCoin3 =_register.Contains(dime);
                    return hasCoin3;
                case "Quarter":
                    Quarter quarter = new Quarter();
                    bool hasCoin4 = _register.Contains(quarter);
                    return hasCoin4;
                default:
                    return false;
            }

        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            switch (name)
            {
                case "Penny":
                    Penny penny = new Penny();
                    _register.Remove(penny);
                    return penny;
                case "Nickel":
                    Nickel nickel = new Nickel();
                    _register.Remove(nickel);
                    return nickel;
                case "Dime":
                    Dime dime = new Dime();
                    _register.Remove(dime);
                    return dime;
                case "Quarter":
                    Quarter quarter = new Quarter();
                    _register.Remove(quarter);
                    return quarter;
                default:
                    return null;
            }
            
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double change = totalPayment - canPrice;
            return change;
            
        }
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double value = 0.0;
                foreach(Coin coin in payment)
            {
                value += coin.Value;
            }
            return value;
           
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            foreach(Coin coin in coins)
            {
                _register.Add(coin);
            }
           
        }
       
      
       
      
    }
}
