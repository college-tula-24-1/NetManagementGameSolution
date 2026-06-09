using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public interface IPlayerPlatform
    {
        AuctionPair ReadMaterialInfo(AuctionPair materialPair);
    }
}
