using ManagementGameCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementGameSpectreConoleApp.Widgets
{
    public class BankerWidget
    {
        Banker banker;
        public BankerWidget(Banker banker)
        {
            this.banker = banker;
        }

        public Panel AuctionInfoWidget
        {
            get
            {
                Table bankTable = new Table().NoBorder()
                     .HideHeaders()
                     .AddColumn("")
                     .AddColumn("");
                bankTable.AddRow("Material Count", $"[SkyBlue1]{ banker.AuctionInfo.Materials.Count }[/]")
                         .AddRow("Material Min Price", $"[Red]{ banker.AuctionInfo.Materials.Price }[/]")
                         .AddEmptyRow()
                         .AddRow("Product Count", $"[SkyBlue2]{ banker.AuctionInfo.Products.Count }[/]")
                         .AddRow("Product Max Price", $"[Red3_1]{ banker.AuctionInfo.Products.Price }[/]");

                Panel bankPanel = new Panel(bankTable)
                                  .BorderColor(Color.Gold3_1)
                                  .Expand()
                                  .Header("Auctions Info")
                                  .DoubleBorder();
                return bankPanel;
            }
        }
        public Panel CalendarWidget
        {
            get
            {
                Table calendarTable = new Table().NoBorder()
                         .HideHeaders()
                         .AddColumns("", "");
                calendarTable.AddEmptyRow()
                             .AddRow("Year", $"[DodgerBlue1]{banker.Round / 12}[/]")
                             .AddEmptyRow()
                             .AddRow("Month", $"[Cyan]{banker.Round % 12}[/]");
                Panel calendarPanel = new Panel(calendarTable)
                                         .BorderColor(Color.Gold3_1)
                                         .Expand()
                                         .Header("Calendar")
                                         .DoubleBorder();
                return calendarPanel;
            }
        }
    }
}
