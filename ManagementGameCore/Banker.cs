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
            Players = Players.Where(player => player.MonthlyExpenses() >= 0)
                             .ToList();
        }

        public void Auction()
        {
            AuctionInfo = Market.Auction(PlayersCount);
            BankerAuctionNotify?.Invoke(this, new BankerEventArgs(this));
        }

        public void MaterialAuction()
        {
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
        }

        public static event BankerEventHandler? BankerAuctionNotify;

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
