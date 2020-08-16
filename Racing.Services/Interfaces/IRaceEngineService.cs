using Racing.Model;
using System.Collections.Generic;

namespace Racing.Services.Interfaces
{
    public interface IRaceEngineService
    {
        void Go(IList<RacerPerson> racerPersonList, int raceLength);
        IList<RacerPerson> GetFinishRanking();
    }
}
