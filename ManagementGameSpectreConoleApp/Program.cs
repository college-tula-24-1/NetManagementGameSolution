using ManagementGameSpectreConoleApp.Widgets;
using Spectre.Console;

Console.SetWindowSize(160, 50);

GameLayout gameLayout = new();
AnsiConsole.Write(gameLayout.MainLayout);

//var bankInfo = new Table()
//                   .NoBorder()
//                   .AddColumn("Title")
//                   .AddColumn("Value")
//                   .AddRow("Materials Count: ", "")
//                   .AddRow("Materials Min Price: ", "");

//var bankPanel = new Panel(bankInfo)
//            .Header(" Bank Info ")
//            .Expand()
//            .DoubleBorder()
//            .BorderColor(Color.Blue);


//AnsiConsole.Live(layout)
//    .Start(ctx => 
//    {
//        ctx.Refresh();
//        Console.ReadKey();


//        bankInfo.Rows.Clear();
//        bankInfo.AddRow("Materials Count: ", "5")
//                .AddRow("Materials Min Price: ", "500");

//        ctx.Refresh();
//        Console.ReadKey();
//    }
//    );



