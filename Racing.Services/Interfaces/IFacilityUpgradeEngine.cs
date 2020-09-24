using Racing.Model;
using System.Collections.Generic;

namespace Racing.Services.Interfaces
{
    public interface IFacilityUpgradeEngine
    {
        IList<Division> Upgrade(IList<Division> divisions);
    }
}
