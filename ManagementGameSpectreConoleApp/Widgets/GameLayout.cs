using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementGameSpectreConoleApp.Widgets
{
    public class GameLayout
    {
        public Layout MainLayout { get; set; } = new Layout("Main");
        public Layout InfoLayout { get; set; } = new Layout("Info");

        public Layout ContentLayout { get; set; } = new Layout("Content");
        public Layout PlayerLayout { get; set; } = new Layout("Player");

        public Table BankTable { set; get; } = new Table();
        public Table CalendarTable { set; get; } = new Table();

        public GameLayout()
        {
            InfoLayout.Ratio = 1;
            ContentLayout.Ratio = 4;
            PlayerLayout.Ratio = 2;

            MainLayout.SplitRows(InfoLayout, ContentLayout, PlayerLayout);

            InfoLayout.SplitColumns(
                BankLayoutInit(),
                new Layout("").Update(new Panel("").NoBorder()),
                CalendarLayoutInit());
        }


        private Layout BankLayoutInit()
        {
            BankTable.NoBorder()
                     .HideHeaders()
                     .AddColumn("")
                     .AddColumn("");
            BankTable.AddRow("Material Count", "0")
                     .AddRow("Material Min Price", "0")
                     .AddEmptyRow()
                     .AddRow("Product Count", "0")
                     .AddRow("Product Max Price", "0");

            Panel bankPanel = new Panel(BankTable)
                              .Expand()
                              .Header("Bank Info")
                              .DoubleBorder();
            return new Layout(bankPanel);
        }

        private Layout CalendarLayoutInit()
        {
            CalendarTable.NoBorder()
                         .HideHeaders()
                         .AddColumns("", "");
            CalendarTable.AddEmptyRow()
                         .AddRow("Year", "1")
                         .AddEmptyRow()
                         .AddRow("Month", "1");
            Panel calendarPanel = new Panel(CalendarTable)
                                     .Expand()
                                     .Header("Calendar")
                                     .DoubleBorder();
            return new Layout(calendarPanel);
        }
    }
}
