using Microsoft.EntityFrameworkCore;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }
}
