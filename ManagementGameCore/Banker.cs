namespace ManagementGameCore
{
    public class Banker
    {
        readonly int maxPlayersCount = 2;
        public List<Player> Players { get; private set; } = new();
        public Market Market { get; private set; } = new();
        public AuctionInfo AuctionInfo { get; private set; }

        public Banker() { }

        public int MaxPlayersCount => maxPlayersCount;
        public int PlayersCount => Players.Count;
        public bool AddPlayer(Player player)
        {
            if (Players.Count < maxPlayersCount)
            {
                Players.Add(player);
                return true;
            }
            else return false;
        }
        
        public void MonthlyExpenses()
        {
            Players.ForEach(player => player.MonthlyExpenses());
            this.PlayerBankruptCheck();
        }

        public void Auction()
        {
            AuctionInfo = Market.Auction(PlayersCount);
        }

        public void MaterialAuction()
        {
            BankerMaterialAuctionNotify?.Invoke(this, new BankerEventArgs(this));

            var materialAuctionPlayers = Players.Select(
                player =>
                {
                    var pair = player.MaterialRequest(AuctionInfo.Materials);
                    return new
                    {
                        Player = player,
                        MaterialPair = pair,
                    };
                }).ToList();
            materialAuctionPlayers.Sort(
                (l, r) => r.MaterialPair.Price - l.MaterialPair.Price);

            int materialCount = AuctionInfo.Materials.Count;

            materialAuctionPlayers.ForEach(
                p =>
                {
                    int count = 0;
                    if (materialCount > 0)
                    {
                        if (materialCount >= p.MaterialPair.Count)
                            count = p.MaterialPair.Count;
                        else
                            count = materialCount;
                        materialCount -= count;
                    }
                    p.Player.MaterialResponse(new(count, p.MaterialPair.Price));
                });
            this.PlayerBankruptCheck();
        }

        public void Production()
        {
            Players.ForEach(player => player.Production());
            this.PlayerBankruptCheck();
        }

        public void ProductAuction()
        {
            BankerProductAuctionNotify?.Invoke(this, new BankerEventArgs(this));

            var productAuctionPlayers = Players.Select(
                player =>
                {
                    var pair = player.ProductRequest(AuctionInfo.Materials);
                    return new
                    {
                        Player = player,
                        ProductPair = pair,
                    };
                }).ToList();
            productAuctionPlayers.Sort(
                (l, r) => l.ProductPair.Price - r.ProductPair.Price);

            int productCount = AuctionInfo.Products.Count;

            productAuctionPlayers.ForEach(
                p =>
                {
                    int count = 0;
                    if (productCount > 0)
                    {
                        if (productCount >= p.ProductPair.Count)
                            count = p.ProductPair.Count;
                        else
                            count = productCount;
                        productCount -= count;
                    }
                    p.Player.ProductResponse(new(count, p.ProductPair.Price));
                });
            this.PlayerBankruptCheck();
        }

        public void PaymentCreditRate()
        {
            Players.ForEach(player => player.PaymentCreditRate());
            this.PlayerBankruptCheck();
        }

        public void PaymentCredit()
        {
            Players.ForEach(player => player.PaymentCredit());
            this.PlayerBankruptCheck();
        }

        public void TakeCreadit()
        {
            Players.ForEach(player => player.TakeCredit());
            this.PlayerBankruptCheck();
        }

        public void FactoriesServices()
        {
            Players.ForEach(player => FactoriesServices());
            this.PlayerBankruptCheck();
        }

        public void PlayerBankruptCheck()
        {
            Players.ForEach(player =>
            {
                if (player.Account < 0)
                    player.Bunkrupt();
            });

            Players = Players.Where(player => player.Account >= 0)
                             .ToList();
        }



        public static event BankerEventHandler? BankerMaterialAuctionNotify;
        public static event BankerEventHandler? BankerProductAuctionNotify;

    }


    public class BankerEventArgs : EventArgs
    {
        Banker banker;
        public BankerEventArgs(Banker banker)
        {
            this.banker = banker;
        }
        public AuctionInfo AuctionInfo => banker.AuctionInfo;
    }

    public delegate void BankerEventHandler(Banker banker, BankerEventArgs e);
}
