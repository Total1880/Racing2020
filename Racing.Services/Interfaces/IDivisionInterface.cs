using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services.Interfaces
{
    public interface IDivisionInterface
    {
        Task<IList<Division>> GenerateDivisions(int numberOfDivisions, int numberOfNewTeams, int numberOfNewRacers);
    }
}
