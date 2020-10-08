using Racing.Model;
using Racing.Model.Enums;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Services
{
    public class RaceEngineServiceV2 : IRaceEngineService
    {
        private IList<Racer> _racerList;
        private Race _race;
        private Queue<Racer> _finishingPositions;
        private Random _random = new Random();

        public IList<RacerPerson> GetFinishRanking()
        {
            IList<RacerPerson> ranking = new List<RacerPerson>();

            foreach (var racer in _finishingPositions)
            {
                RacerPerson newRacerPerson = new RacerPerson();

                newRacerPerson.FirstName = racer.FirstName;
                newRacerPerson.LastName = racer.LastName;
                newRacerPerson.FlatAbility = racer.FlatAbility;
                newRacerPerson.ClimbingAbility = racer.ClimbingAbility;
                newRacerPerson.DownhillAbility = racer.DownhillAbility;
                newRacerPerson.Age = racer.Age;
                newRacerPerson.Nation = racer.Nation;
                newRacerPerson.RacerPersonId = racer.RacerPersonId;
                newRacerPerson.Team = racer.Team;
                newRacerPerson.Jersey = racer.Jersey;

                ranking.Add(newRacerPerson);
            }

            return ranking;
        }

        public void Go(IList<RacerPerson> racerPersonList, Race race)
        {
            Convert(racerPersonList);
            _race = race;
        }

        public void Move()
        {
            Sort();

            foreach (var racer in _racerList)
            {
                if (racer.RacePosition < _race.Length)
                {
                    var racePart = _race.RacePartList.Where(rp => racer.RacePosition >= rp.Start && racer.RacePosition <= rp.End).FirstOrDefault();

                    if (racePart == null)
                    {
                        racePart = _race.RacePartList[0];
                    }
                    switch (racePart.Part)
                    {
                        case RacePartEnum.Flat:
                            racer.RacePosition += (float)_random.Next(1, racer.FlatAbility) / 10;
                            break;
                        case RacePartEnum.Uphill:
                            racer.RacePosition += (float)_random.Next(1, racer.ClimbingAbility) / 10;
                            break;
                        case RacePartEnum.Downhill:
                            racer.RacePosition += (float)_random.Next(1, racer.DownhillAbility) / 10;
                            break;
                        default:
                            racer.RacePosition += 1;
                            break;
                    }
                }
                else
                {
                    if (!_finishingPositions.Contains(racer))
                    {
                        _finishingPositions.Enqueue(racer);
                    }
                }
            }
        }

        private void Convert(IList<RacerPerson> racerPersonList)
        {
            _racerList = new List<Racer>();
            _finishingPositions = new Queue<Racer>();

            foreach (var racerPerson in racerPersonList)
            {
                Racer newRacer = new Racer
                {
                    FirstName = racerPerson.FirstName,
                    LastName = racerPerson.LastName,
                    FlatAbility = racerPerson.FlatAbility,
                    ClimbingAbility = racerPerson.ClimbingAbility,
                    DownhillAbility = racerPerson.DownhillAbility,
                    Nation = racerPerson.Nation,
                    RacerPersonId = racerPerson.RacerPersonId,
                    Team = racerPerson.Team,
                    Age = racerPerson.Age,
                    FlatPotentialAbility = racerPerson.FlatPotentialAbility,
                    ClimbingPotentialAbility = racerPerson.ClimbingPotentialAbility,
                    DownhillPotentialAbility = racerPerson.DownhillPotentialAbility,
                    Jersey = racerPerson.Jersey
                };

                _racerList.Add(newRacer);
            }
        }

        private void Sort()
        {
            _racerList = _racerList.OrderByDescending(r => r.RacePosition).ToList();
        }

        public IList<Racer> GetRacerList()
        {
            return _racerList;
        }
    }
}
