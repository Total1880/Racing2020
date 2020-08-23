using Microsoft.EntityFrameworkCore;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Api.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        public bool Create(Team item)
        {
            try
            {
                using var context = new RacingContext();
                if (!context.TeamList.Any(r => r.Name == item.Name))
                {
                    context.TeamList.Add(item);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using var context = new RacingContext();

                if (context.RacerPersonList.Any(p => p.Team.TeamId == id))
                {
                    return false;
                }

                context.TeamList.Remove(context.TeamList.Find(id));
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<Team> Get()
        {
            using var context = new RacingContext();
            return context.TeamList.ToList();
        }

        public bool Update(Team item)
        {
            try
            {
                using var context = new RacingContext();
                context.Attach(item);
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
