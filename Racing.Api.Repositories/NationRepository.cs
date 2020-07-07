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
                using RacingContext context = new RacingContext();
                context.NationList.Add(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Nation> Get()
        {
            using RacingContext context = new RacingContext();
            return context.NationList.ToList();
        }

        public bool Update(Nation item)
        {
            throw new System.NotImplementedException();
        }
    }
}
