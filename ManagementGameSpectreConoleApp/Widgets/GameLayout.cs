using ManagementGameCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementGameSpectreConoleApp.Widgets
{
    public class GameLayout
    {
        PlayerWidget playerWidget = new(new ManagementGameCore.Player());
        BankerWidget bankerWidget = new(new ManagementGameCore.Banker());

        List<Player> opponents = new()
        {
            new("Bobby"),
            new("Sammy"),
            new("Mikky"),
            new("Bobby"),
            new("Sammy"),
            new("Mikky"),
        };

        List<OpponentWidget> opponentsWidgets = new();
        public Layout MainLayout { get; set; } = new Layout("Main");

        


        public GameLayout()
        {
            Layout infoLayout = new("Info");
            Layout playerLayout = new("Player");
            Layout opponentsLayout = new Layout("Opponents");

            infoLayout.Ratio = 3;
            opponentsLayout.Ratio = 9;
            playerLayout.Ratio = 5;

            MainLayout.SplitRows(infoLayout, opponentsLayout, playerLayout);

            infoLayout.SplitColumns(
                new Layout(bankerWidget.AuctionInfoWidget),
                new Layout("").Update(new Panel("").NoBorder()),
                new Layout(bankerWidget.CalendarWidget));

            playerLayout.SplitColumns(
                new Layout(playerWidget.ManufactoriesWidget).Ratio(4),
                new Layout("").Ratio(3).Update(new Panel("").NoBorder()),
                new Layout(playerWidget.FinanceWidget).Ratio(4));

            foreach (Player player in opponents)
                opponentsWidgets.Add(new(player));

            Grid opponentsGrid = new Grid().Expand().AddColumns(3);

            List<Panel> opponentsRow = new();
            int count = 0;
            foreach(var w in opponentsWidgets)
            {
                opponentsRow.Add(w.OpponentInfo);
                count++;
                if(count >= 3)
                {
                    opponentsGrid.AddRow(opponentsRow.ToArray());
                    count = 0;
                    opponentsRow.Clear();
                }
            }
            opponentsGrid.AddRow(opponentsRow.ToArray());

            opponentsLayout.Update(opponentsGrid);

        }


    }
}
