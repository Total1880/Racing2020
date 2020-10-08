using Racing.Model;
using System.Collections.Generic;

namespace Racing.Services.Interfaces
{
    public interface IRaceEngineService
    {
        void Go(IList<RacerPerson> racerPersonList, Race race);
        void Move();
        IList<RacerPerson> GetFinishRanking();
    }
}
