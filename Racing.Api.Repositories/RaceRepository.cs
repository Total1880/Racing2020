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

                var race = context.RaceList.Include(r => r.RacePointList).Where(r => r.RaceId == id).FirstOrDefault();
                foreach (var racePoint in race.RacePointList)
                {
                    context.Entry(racePoint).State = EntityState.Deleted;
                }

                context.SaveChanges();
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
            return context.RaceList.Include(x => x.RacePointList).ToList();
        }

        public bool Update(Race item)
        {
            try
            {
                using var context = new RacingContext();

                context.Attach(item);
                context.Entry(item).State = EntityState.Modified;
                var copyRacePointList = new List<RacePoint>(item.RacePointList);
                var race = context.RaceList.Include(r => r.RacePointList).Where(r => r.RaceId == item.RaceId).FirstOrDefault();

                foreach (var racePoint in copyRacePointList)
                {
                    if (racePoint.RacePointId == 0)
                    {
                        var oldRacePoint = race.RacePointList.Where(p => p.Position == racePoint.Position && p.RacePointId > 0).FirstOrDefault();

                        if (oldRacePoint != null)
                        {
                            context.Entry(oldRacePoint).State = EntityState.Deleted;
                        }

                        context.Entry(racePoint).State = EntityState.Added;
                    }
                    else
                    {
                        context.Entry(racePoint).State = EntityState.Modified;
                    }
                }

                foreach (var racePoint in race.RacePointList.Where(p => p.Position > copyRacePointList.Max(p => p.Position)).ToList())
                {
                    context.Entry(racePoint).State = EntityState.Deleted;
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
