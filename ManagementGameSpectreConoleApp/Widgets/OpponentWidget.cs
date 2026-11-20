using ManagementGameCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementGameSpectreConoleApp.Widgets
{
    public class OpponentWidget
    {
        Player player;
        public OpponentWidget(Player player)
        {
            this.player = player;
        }

        public Panel OpponentInfo
        {
            get
            {
                var credits = player.Credits.Sum(c => c.Amount);
                var percents = player.Credits.Sum(c => c.Percent());

                Table opponentTable = new Table()
                                 .AddColumns("", "")
                                 .HideHeaders()
                                 .NoBorder()
                                 .AddRow("Factories Ordinary", $"[DarkOrange]{player.FactoriesSimple().Count}[/]")
                                 .AddRow("Factories Automated", $"[Blue]{player.FactoriesAutomated().Count}[/]")
                                 .AddRow("Factories Under Construction", $"[Red]{player.FactoriesNotRunning}[/]")
                                 .AddEmptyRow()
                                 .AddRow("Materials", $"[Green3_1]{player.Materials}[/]")
                                 .AddRow("Products", $"[DeepPink2]{player.Products}[/]")
                                 .AddEmptyRow()
                                 .AddRow("Account", $"[SteelBlue1]{player.Account}[/]")
                                 .AddRow("Credits", $"[Red3_1]{credits}[/]")
                                 .AddRow("Percents", $"[IndianRed1_1]{percents}[/]");

                Color[] colors = 
                { 
                    Color.DarkOliveGreen2,
                    Color.Aqua,
                    Color.Blue3,
                    Color.CadetBlue,
                    Color.Chartreuse2_1,
                    Color.CornflowerBlue,
                    Color.Cyan3,
                    Color.DeepPink1,
                    Color.DeepSkyBlue3_1
                };

                Random random = new Random();

                Color borderColor = colors[random.Next(colors.Length)];


                Panel opponentPanel = new Panel(opponentTable)
                                                .BorderColor(borderColor)
                                                .Header($"Player {player.Name}")
                                                .DoubleBorder();

                return opponentPanel;
            }
        }
    }
}
