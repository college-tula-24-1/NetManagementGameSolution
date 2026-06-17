using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public class Market
    {
        public int Level {  get; private set; }
        public Market() 
        {
            Level = 3;
        }

        public AuctionInfo Auction(int playersCount)
        {
            List<(double Count, int Price)> materialRate = new()
            { ( 1.0, 800 ), ( 1.5, 650 ), ( 2.0, 500 ), ( 2.5, 400 ), ( 3.0, 300 ) };

            List<(double Count, int Price)> productRate = new()
            { ( 3.0, 6500 ), ( 2.5, 6000 ), ( 2.0, 5500 ), ( 1.5, 5000 ), ( 1.0, 4500 ) };

            int[][] levelDistribs = new[]
            {
                new[]{ 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 5 },
                new[]{ 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 5 },
                new[]{ 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5 },
                new[]{ 1, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5 },
                new[]{ 1, 2, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5 },
            };
            Span<int[]> levelSpan = levelDistribs;

            int materialCount = (int)Math.Ceiling(playersCount * materialRate[Level - 1].Count);
            Money materialPrice = new Money(materialRate[Level - 1].Price, 0);

            AuctionPair materialPair = new(materialCount, materialPrice);

            int productCount = (int)Math.Ceiling(playersCount * productRate[Level - 1].Count);
            Money productPrice = new Money(productRate[Level - 1].Price, 0);

            AuctionPair productPair = new(productCount, productPrice);

            Random random = new Random();
            random.Shuffle(levelSpan[Level - 1]);
            Level = levelDistribs[Level - 1][random.Next(levelDistribs[Level - 1].Length)];

            return new AuctionInfo(materialPair, productPair);
        }
    }

    public record struct AuctionPair(int Count, Money Price);
    public record struct AuctionInfo(AuctionPair Materials, AuctionPair Products);
}
