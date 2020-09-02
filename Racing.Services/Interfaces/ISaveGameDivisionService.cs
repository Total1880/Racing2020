using Racing.Model;
using System.Collections;
using System.Collections.Generic;

namespace Racing.Services.Interfaces
{
    public interface ISaveGameDivisionService
    {
        bool SaveDivisions(IList<Division> divisions);
        IList<Division> GetDivisions();
    }
}
