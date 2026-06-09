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

        public void FactoriesMonthleCheck()
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
            return Factories.Where(factory => !factory.Automated)
                            .ToList();
        }
        public List<Factory> FactoriesAutomated()
        {
            return Factories.Where(factory => factory.Automated)
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
            return PlayerPlatform.ReadMaterialInfo(materialPair);
        }

        public Money MaterialResponse(AuctionPair materialPair)
        {
            Account -= materialPair.Price * materialPair.Count;
            Materials += materialPair.Count;

            PlayerMaterialAuctionNotify?.Invoke(this, new PlayerEventArgs(this));

            return Account;
        }


        public static PlayerEventHandler? PlayerMonthlyExpensesNotify;
        public static PlayerEventHandler? PlayerMaterialAuctionNotify;
    }

    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; private set; }
        public string Name { get => Player.Name; }
        public Money Account { get => Player.Account; }
        public int Materials { get => Player.Materials; }
        public int Products { get => Player.Products; }
        public List<Factory> Factories { get => Player.Factories; }

        public PlayerEventArgs(Player player)
        {
            Player = player;
        }
    }
    public delegate void PlayerEventHandler(Player sender, PlayerEventArgs e);
}
