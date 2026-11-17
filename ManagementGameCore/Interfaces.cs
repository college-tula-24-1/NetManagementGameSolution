using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public interface IPlayerPlatform
    {
        AuctionPair ReadMaterialInfo(string name, AuctionPair materialPair);

        (int simple, int automated) ReadProductionInfo(string name, List<Factory> factories);

        AuctionPair ReadProductInfo(string name, AuctionPair materialPair);

        Money ReadCreaditInfo(Money maxAmount);

        (int simple, int automated, int toAutomated ) ReadFactoryConstructionInfo(Money account);
        
    }
}
