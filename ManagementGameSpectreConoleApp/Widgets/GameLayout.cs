using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementGameSpectreConoleApp.Widgets
{
    public class GameLayout
    {
        public Layout MainLayout { get; set; } = new Layout("Main");

        public Layout ContentLayout { get; set; } = new Layout("Content");
        //public Layout PlayerLayout { get; set; } = new Layout("Player");

        public Table BankTable { set; get; } = new Table();
        public Table CalendarTable { set; get; } = new Table();

        public BarChart ManufactoriesBarChart { set; get; } = new BarChart();
        public Table FinanceTable { set; get; } = new Table();


        public GameLayout()
        {
            Layout infoLayout = new("Info");
            Layout playerLayout = new("Player");

            infoLayout.Ratio = 1;
            ContentLayout.Ratio = 4;
            playerLayout.Ratio = 2;

            MainLayout.SplitRows(infoLayout, ContentLayout, playerLayout);

            infoLayout.SplitColumns(
                BankLayoutInit(),
                new Layout("").Update(new Panel("").NoBorder()),
                CalendarLayoutInit());

            playerLayout.SplitColumns(
                ManufactoriesLayoutInit(),
                new Layout("").Update(new Panel("").NoBorder()),
                FinanceLayoutInit());
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

        private Layout ManufactoriesLayoutInit()
        {
            //ManufactoriesTable.NoBorder()
            //                  .HideHeaders()
            //                  .AddColumns("", "");
            //ManufactoriesTable.AddRow("Factories Ordinary", "3")
            //                  .AddRow("Factories Automated", "2")
            //                  .AddRow("Factories Under Construction", "1")
            //                  .AddEmptyRow()
            //                  .AddRow("Materials", "4")
            //                  .AddRow("Products", "2");

            ManufactoriesBarChart
                                 .Width(60)
                                 .AddItem("Factories Ordinary", 4, Color.Green)
                                 .AddItem("Factories Automated", 2, Color.Blue)
                                 .AddItem("Factories Under Construction", 3, Color.Red)
                                 .AddItem("", 0, Color.Black)
                                 .AddItem("Materials", 12, Color.White)
                                 .AddItem("Products", 3, Color.Yellow);


            Panel manufactoriesPanel = new Panel(ManufactoriesBarChart)
                                            .Expand()
                                            .Header("Manufactories Info")
                                            .DoubleBorder();

            return new Layout(manufactoriesPanel);
        }

        private Layout FinanceLayoutInit()
        {
            FinanceTable.NoBorder()
                        .HideHeaders()
                        .AddColumns("", "");
            FinanceTable.AddRow("Account", "12000")
                        .AddEmptyRow()
                        .AddRow("Credits", "5000")
                        .AddEmptyRow()
                        .AddRow("Crredit Percent per Month", "120");

            Panel financePanel = new Panel(FinanceTable)
                                            .Expand()
                                            .Header("Finance Info")
                                            .DoubleBorder();
            
            return new Layout(financePanel);
        }
    }
}
