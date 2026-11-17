using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public class Player
    {
        public string Name { get; private set; } = null!;
        public List<Factory> Factories { private set; get; } = null!;
        public List<Credit> Credits { private set; get; } = new();
        public Money Account { get; private set; }
        public int Materials { get; private set; }
        public int Products { get; private set; }


        public static IPlayerPlatform? PlayerPlatform { get; set; }

        public Player(string name = "Computer")
        {
            this.Name = name;
            this.Account = new Money(10000, 0);
            this.Materials = 4;
            this.Products = 2;

            Factories = new()
            {
                Factory.CreateDefault(this),
                Factory.CreateDefault(this),
            };
        }

        public void FactoriesMonthlyCheck()
        {
            foreach(var factory in Factories)
                factory.MonthlyCheck();
        }
        public void FactoryCreate(bool automated = false)
        {
            Factories.Add(Factory.Create(this, automated));
        }
        public void FactoryAutomated(Factory factory)
        {
            if(!factory.Automated)
                factory.StartAutomated();
        }
        
        public List<Factory> FactoriesSimple()
        {
            return Factories.Where(factory => factory.Built && !factory.Automated)
                            .ToList();
        }
        public List<Factory> FactoriesAutomated()
        {
            return Factories.Where(factory => factory.Built && factory.Automated)
                            .ToList();
        }

        public Money MonthlyExpenses()
        {
            Money expenses = new Money();

            Factories.ForEach(factory => expenses += new Money(factory.Automated ? 1500 : 1000, 0));
            expenses += new Money(Materials * 300, 0);
            expenses += new Money(Products * 500, 0);

            Account -= expenses;
            PlayerMonthlyExpensesNotify?.Invoke(this, new PlayerEventArgs(this));

            return Account;
        }
        public AuctionPair MaterialRequest(AuctionPair materialPair)
        {
            return PlayerPlatform.ReadMaterialInfo(Name, materialPair);
        }
        public Money MaterialResponse(AuctionPair materialPair)
        {
            Account -= materialPair.Price * materialPair.Count;
            Materials += materialPair.Count;

            PlayerMaterialAuctionNotify?.Invoke(this, new PlayerEventArgs(this));

            return Account;
        }

        public Money Production()
        {
            (int simple, int automated) productionInfo = PlayerPlatform.ReadProductionInfo(Name, Factories);

            // Simple Production
            Materials -= productionInfo.simple;
            Account -= new Money(productionInfo.simple * 2000, 0);
            Products += productionInfo.simple;

            // Automated Production
            Materials -= productionInfo.automated;
            Account -= new Money(productionInfo.automated * 3000, 0);
            Products += productionInfo.automated;

            PlayerProductionNotify?.Invoke(this, new PlayerEventArgs(this));

            return Account;
        }

        public AuctionPair ProductRequest(AuctionPair productPair)
        {
            return PlayerPlatform.ReadProductInfo(Name, productPair);
        }
        public Money ProductResponse(AuctionPair productPair)
        {
            Account += productPair.Price * productPair.Count;
            Products -= productPair.Count;

            PlayerProductAuctionNotify?.Invoke(this, new PlayerEventArgs(this));

            return Account;
        }

        public Money PaymentCreditRate()
        {
            Credits.ForEach(credit =>
            {
                Account -= credit.Percent();
            });

            PlayerPaymentCreditRateNotify?.Invoke(this, new PlayerEventArgs(this));
            return Account;
        }
        public Money PaymentCredit()
        {
            Credits.ForEach(credit =>
            {
                if (credit.PeriodDec())
                    Account -= credit.Amount;
            });

            PlayerPaymentCreditNotify?.Invoke(this, new PlayerEventArgs(this));

            Credits = Credits.Where(credit => credit.Period > 0).ToList();
            return Account;
        }
        public Money TakeCredit()
        {
            Money maxAmount = new();
            Factories.ForEach(factory =>
            {
                maxAmount += factory.Automated ? new Money(10000, 0) : new Money(5000, 0);
            });
            Credits.ForEach(credit =>
            {
                maxAmount -= credit.Amount;
            });

            if(maxAmount > 0)
            {
                Money? creditAmount = PlayerPlatform?.ReadCreaditInfo(maxAmount);
                if (creditAmount > 0)
                {
                    Credits.Add(new Credit(creditAmount, 12));
                    Account += creditAmount;
                }
            }

            return Account;
        }

        public Money FactoriesServices()
        {
            FactoriesMonthlyCheck();

            (int simple, int automated, int toAutomated) constructionInfo =
                PlayerPlatform.ReadFactoryConstructionInfo(this.Account);

            for (int i = 0; i < constructionInfo.simple; i++)
                Factories.Add(new Factory().StartConstruction());

            for(int i = 0; i < constructionInfo.automated; i++)
                Factories.Add(new Factory().StartConstruction(true));

            for(int i = 0; i < = constructionInfo.toAutomated; i++)
                FactoriesSimple()[i].StartAutomated();
        }

        public void Bunkrupt()
        {
            PlayerBankruptNotify?.Invoke(this, new PlayerEventArgs(this));
        }



        //public static event PlayerEventHandler? PlayerMonthlyExpensesNotify;
        public static event EventHandler<PlayerEventArgs>? PlayerMonthlyExpensesNotify;

        public static event PlayerEventHandler? PlayerMaterialAuctionNotify;
        public static event PlayerEventHandler? PlayerBankruptNotify;
        public static event PlayerEventHandler? PlayerProductionNotify;
        public static event PlayerEventHandler? PlayerProductAuctionNotify;
        public static event PlayerEventHandler? PlayerPaymentCreditRateNotify;
        public static event PlayerEventHandler? PlayerPaymentCreditNotify;
    }

    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; private set; }
        public string Name { get => Player.Name; }
        public Money Account { get => Player.Account; }
        public int Materials { get => Player.Materials; }
        public int Products { get => Player.Products; }
        public List<Factory> Factories { get => Player.Factories; }
        public List<Credit> Credits {  get => Player.Credits; }

        public PlayerEventArgs(Player player)
        {
            Player = player;
        }
    }
    public delegate void PlayerEventHandler(Player sender, PlayerEventArgs e);
}
