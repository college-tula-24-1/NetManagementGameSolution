using ManagementGameCore;

Player player = new Player("Bobby");
Player playerJoe = new Player("Jonny");
Player.PlayerPlatform = new ConsolePlatform();


Banker banker = new Banker();
banker.AddPlayer(player);
banker.AddPlayer(playerJoe);

Banker.BankerAuctionNotify += (b, e) =>
{
    Console.WriteLine($"Materials: Count: {e.AuctionInfo.Materials.Count}, " +
        $"Min Price: {e.AuctionInfo.Materials.Price}");
};

Player.PlayerMaterialAuctionNotify += (Player p, PlayerEventArgs e) =>
{
    Console.WriteLine($"Player: {p.Name}");
    Console.WriteLine($"\tAcoount: {e.Account}");
    Console.WriteLine($"\tMaterials: {e.Materials}");
    Console.WriteLine($"\tProducts: {e.Products}");
};


banker.Auction();
banker.MaterialAuction();

Console.WriteLine("End");


class ConsolePlatform : IPlayerPlatform
{
    public AuctionPair ReadMaterialInfo(AuctionPair materialPair)
    {
        Console.Write("Input count: ");
        int count = Int32.Parse(Console.ReadLine());
        Console.Write("Input price: ");
        int dollars = Int32.Parse(Console.ReadLine());

        return new(count, new Money(dollars, 0));
    }
}