using Racing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            catch (Exception)
            {

                return false;
            }
        }

        public IList<LastNames> GenerateNames(int numberOfNames)
        {
            try
            {
                using var context = new RacingContext();

                var lastNames = new List<LastNames>();
                var rand = new Random();

                for (int i = 0; i < numberOfNames; i++)
                {
                    var toSkip = rand.Next(context.LastNamesList.Count());

                    lastNames.Add(context.LastNamesList.Include(c => c.Nation).Skip(toSkip).Take(1).FirstOrDefault());
                }

                return lastNames;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
