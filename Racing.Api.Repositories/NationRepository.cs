using Microsoft.EntityFrameworkCore;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Api.Repositories
{
    public class NationRepository : IRepository<Nation>
    {
        public bool Create(Nation item)
        {
            try
            {
                using var context = new RacingContext();
                if (!context.NationList.Any(n => n.Name == item.Name))
                {
                    context.NationList.Add(item);
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
                context.NationList.Remove(context.NationList.Find(id));
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<Nation> Get()
        {
            using var context = new RacingContext();
            return context.NationList.ToList();
        }

        public bool Update(Nation item)
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
