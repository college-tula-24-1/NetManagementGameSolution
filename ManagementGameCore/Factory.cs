using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public class Factory
    {
        Player player = null!;
        Factory(Player player)
        {
            this.player = player;
        }

        public string PlayerName { get => player.Name; }

        bool automated = false;
        public bool Automated
        {
            private set => automated = value;
            get => automated && FreezingPeriod == 0;
        }
        public bool Built { private set; get; } = true;
        public int FreezingPeriod { private set; get; } = 0;
        public Factory StartAutomated()
        {
            if(Built && !Automated)
            {
                Automated = true;
                FreezingPeriod = 9;
            }
            return this;
        }
        public Factory StartConstruction(bool automated = false)
        {
            Built = false;
            Automated = automated;
            FreezingPeriod = Automated ? 7 : 5;
            return this;
        }
        public void MonthlyCheck()
        {
            if (FreezingPeriod > 0)
            {
                FreezingPeriod--;
                if(FreezingPeriod == 0)
                    if(!Built)
                    {
                        FactoryBuiltedNotify?.Invoke(this, new FactoryEventArgs(this));
                        Built = true;
                    }
                    else
                        FactoryAutomatedNotify?.Invoke(this, new FactoryEventArgs(this));
            }            
        }
        public static Factory CreateDefault(Player player)
        {
            return new Factory(player);
        }

        public static Factory Create(Player player, bool automated = false)
        {
            return new Factory(player).StartConstruction(automated);
        }

        public static event FactoryEventHandler? FactoryBuiltedNotify;
        public static event FactoryEventHandler? FactoryAutomatedNotify;
    }

    public class FactoryEventArgs : EventArgs
    {
        public string PlayerName { get; }
        public bool Automated { get; }
        public bool Built { get; }
        public int FreezingPeriod { get; }

        public FactoryEventArgs(Factory factory)
        {
            PlayerName = factory.PlayerName;
            Automated = factory.Automated;
            Built = factory.Built;
            FreezingPeriod = factory.FreezingPeriod;
        }
    }

    public delegate void FactoryEventHandler(object sender, FactoryEventArgs e);
}
