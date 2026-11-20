using Spectre.Console;
using ManagementGameCore;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementGameSpectreConoleApp.Widgets
{
    public class PlayerWidget
    {
        Player player;

        public Panel ManufactoriesWidget 
        { 
            get
            {
                BarChart manufactoriesBarChart = new BarChart()
                                 .Width(50)
                                 .AddItem("Factories Ordinary", player.FactoriesSimple().Count, Color.DarkOrange)
                                 .AddItem("Factories Automated", player.FactoriesAutomated().Count, Color.Blue)
                                 .AddItem("Factories Under Construction", player.FactoriesNotRunning, Color.Red)
                                 .AddItem("", 0, Color.Black)
                                 .AddItem("Materials", player.Materials, Color.Green3_1)
                                 .AddItem("Products", player.Products, Color.DeepPink2);
                Panel manufactoriesPanel = new Panel(manufactoriesBarChart)
                                                .BorderColor(Color.DeepPink1)
                                                .Expand()
                                                .Header("Manufactories Info")
                                                .DoubleBorder();
                return manufactoriesPanel;
            }
        }
        public Panel FinanceWidget 
        {
            get 
            {
                var credits = player.Credits.Sum(c => c.Amount);
                var percents = player.Credits.Sum(c => c.Percent());

                Table financeTable = new Table().NoBorder()
                            .HideHeaders()
                            .AddColumns("", "");
                financeTable.AddRow("Account", $"[SteelBlue1]{ player.Account }[/]")
                            .AddEmptyRow()
                            .AddRow("Credits", $"[Red3_1]{ new Money(credits, 0) }[/]")
                            .AddEmptyRow()
                            .AddRow("Crredit Percent per Month", $"[IndianRed1_1]{ new Money(percents, 0)}[/]");

                Panel financePanel = new Panel(financeTable)
                                                .BorderColor(Color.DodgerBlue1)
                                                .Expand()
                                                .Header("Finance Info")
                                                .DoubleBorder();

                return financePanel;
            }
        }


        public PlayerWidget(Player player)
        {
            this.player = player;
        }

        
    }
}
