using Microsoft.EntityFrameworkCore;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Api.Repositories
{
    public class SettingRepository : IRepository<Setting>
    {
        public bool Create(Setting item)
        {
            try
            {
                using var context = new RacingContext();
                if (!context.SettingList.Any(s => s.Description == item.Description))
                {
                    context.SettingList.Add(item);
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
            throw new System.NotImplementedException();
        }

        public IList<Setting> Get()
        {
            using var context = new RacingContext();
            return context.SettingList.ToList();
        }

        public bool Update(Setting item)
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
