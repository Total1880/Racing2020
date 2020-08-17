using Microsoft.EntityFrameworkCore;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Api.Repositories
{
    public class RaceRepository : IRepository<Race>
    {
        public bool Create(Race item)
        {
            try
            {
                using var context = new RacingContext();
                if (!context.RaceList.Any(r => r.Name == item.Name))
                {
                    context.RaceList.Add(item);
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
                context.RaceList.Remove(context.RaceList.Find(id));
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<Race> Get()
        {
            using var context = new RacingContext();
            return context.RaceList.ToList();
        }

        public bool Update(Race item)
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
