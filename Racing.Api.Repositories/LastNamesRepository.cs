using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Racing.Model;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Api.Repositories
{
    public class LastNamesRepository : INamesRepository<LastNames>
    {
        public bool CreateNames(IList<LastNames> names)
        {
            try
            {
                using var context = new RacingContext();

                var uniquelist = new List<LastNames>();

                foreach (var name in names)
                {
                    if (!context.LastNamesList.Any(n => n.LastName == name.LastName && n.Nation == name.Nation))
                    {
                        var nation = context.NationList.SingleOrDefault(n => n.NationId == name.Nation.NationId);
                        name.Nation = nation;
                        uniquelist.Add(name);
                    }
                }

                context.LastNamesList.AddRange(uniquelist);
                context.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }
}
