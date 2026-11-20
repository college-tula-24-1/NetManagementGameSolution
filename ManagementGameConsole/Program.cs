using ManagementGameCore;

Player player = new Player("Bobby");
Player playerJoe = new Player("Jonny");
Player.PlayerPlatform = new ConsolePlatform();


Banker banker = new Banker();
banker.AddPlayer(player);
banker.AddPlayer(playerJoe);

Banker.BankerMaterialAuctionNotify += (b, e) =>
{
    Console.WriteLine($"Materials: Count: {e.AuctionInfo.Materials.Count}, " +
        $"Min Price: {e.AuctionInfo.Materials.Price}");
};

Banker.BankerProductAuctionNotify += (b, e) =>
{
    Console.WriteLine($"Products: Count: {e.AuctionInfo.Products.Count}, " +
        $"Max Price: {e.AuctionInfo.Products.Price}");
};

Player.PlayerMaterialAuctionNotify += (Player p, PlayerEventArgs e) =>
{
    Console.WriteLine($"Player: {p.Name}");
    Console.WriteLine($"\tAcoount: {e.Account}");
    Console.WriteLine($"\tMaterials: {e.Materials}");
    Console.WriteLine($"\tProducts: {e.Products}");
};

Player.PlayerBankruptNotify += (Player player, PlayerEventArgs e) =>
{
    Console.WriteLine($"Player: {player.Name} Bankrupt!");
    Console.WriteLine($"\tAcoount: {e.Account}");
};

Player.PlayerProductionNotify += (Player player, PlayerEventArgs e) =>
{
    Console.WriteLine($"Player: {player.Name} Production!");
    Console.WriteLine($"\tAcoount: {e.Account}");
    Console.WriteLine($"\tMaterials: {e.Materials}");
    Console.WriteLine($"\tProducts: {e.Products}");
};

Player.PlayerProductAuctionNotify += (Player p, PlayerEventArgs e) =>
{
    Console.WriteLine($"Player: {p.Name}");
    Console.WriteLine($"\tAcoount: {e.Account}");
    Console.WriteLine($"\tMaterials: {e.Materials}");
    Console.WriteLine($"\tProducts: {e.Products}");
};


banker.Auction();
banker.MaterialAuction();
banker.Production();
banker.ProductAuction();

Console.WriteLine("End");


class ConsolePlatform : IPlayerPlatform
{
    public Money ReadCreaditInfo(Money maxAmount)
    {
        throw new NotImplementedException();
    }

    public (int simple, int automated, int toAutomated) ReadFactoryConstructionInfo(Money account)
    {
        throw new NotImplementedException();
    }

    public AuctionPair ReadMaterialInfo(string name, AuctionPair materialPair)
    {
        Console.WriteLine($"Player {name} Material Auction");
        Console.Write("Input count: ");
        int count = Int32.Parse(Console.ReadLine());
        Console.Write("Input price: ");
        int dollars = Int32.Parse(Console.ReadLine());

        return new(count, new Money(dollars, 0));
    }

    public AuctionPair ReadProductInfo(string name, AuctionPair materialPair)
    {
        Console.WriteLine($"Player {name} Product Auction");
        Console.Write("Input count: ");
        int count = Int32.Parse(Console.ReadLine());
        Console.Write("Input price: ");
        int dollars = Int32.Parse(Console.ReadLine());

        return new(count, new Money(dollars, 0));
    }

    public (int simple, int automated) ReadProductionInfo(string name, List<Factory> factories)
    {
        Console.WriteLine($"Player {name} Production");
        int simpleFactories = factories.Where(factory => !factory.Automated).Count();
        int automatedFactories = factories.Where(factory => factory.Automated).Count();

        Console.Write($"You have {simpleFactories} Simple Factories. Input production count: ");
        int simple = Int32.Parse(Console.ReadLine());
        Console.Write($"You have {automatedFactories} Automated Factories. Input production count: ");
        int automated = Int32.Parse(Console.ReadLine());

        return (simple, automated);
    }
}