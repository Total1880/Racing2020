using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Services
{
    class RaceEngineService : IRaceEngineService
    {
        private IList<Racer> _racerList;
        private Queue<Racer> _finishingPositions;
        private int _raceLength;
        private Random _random = new Random();

        public void Go(IList<RacerPerson> racerPersonList, Race race)
        {
            Convert(racerPersonList);
            _raceLength = race.Length;
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

        public void Move()
        {
            do
            {
                Sort();

                IList<Racer> tempRacerList = _racerList;

                foreach (var racer in tempRacerList)
                {
                    if (racer.RacePosition < _raceLength)
                    {
                        racer.RacePosition += (float)_random.Next(1, racer.FlatAbility)/10;
                    }
                    else
                    {
                        if (!_finishingPositions.Contains(racer))
                        {
                            _finishingPositions.Enqueue(racer);
                        }
                    }
                }
            } while (_racerList.Count > _finishingPositions.Count);
        }

        public IList<RacerPerson> GetFinishRanking()
        {
            Move();

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

        public IList<Racer> GetRacerList()
        {
            throw new NotImplementedException();
        }
    }
}
