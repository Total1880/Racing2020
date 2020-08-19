using Racing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Racing.Api.Repositories
{
    public class FirstNamesRepository : INamesRepository<FirstNames>
    {
        public bool CreateNames(IList<FirstNames> names)
        {
            try
            {
                using var context = new RacingContext();

                var uniquelist = new List<FirstNames>();

                foreach (var name in names)
                {
                    if (!context.FirstNamesList.Any(n => n.FirstName == name.FirstName && n.Nation == name.Nation))
                    {
                        var nation = context.NationList.SingleOrDefault(n => n.NationId == name.Nation.NationId);
                        name.Nation = nation;
                        uniquelist.Add(name);
                    }
                }

                context.FirstNamesList.AddRange(uniquelist);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IList<FirstNames> GenerateNames(int numberOfNames)
        {
            try
            {
                using var context = new RacingContext();

                var firstNames = new List<FirstNames>();
                var rand = new Random();

                for (int i = 0; i < numberOfNames; i++)
                {
                    var toSkip = rand.Next(context.FirstNamesList.Count());

                    firstNames.Add(context.FirstNamesList.Include(c => c.Nation).Skip(toSkip).Take(1).FirstOrDefault());
                }

                return firstNames;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
