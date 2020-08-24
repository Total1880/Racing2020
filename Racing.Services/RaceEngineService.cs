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

        public void Go(IList<RacerPerson> racerPersonList, int raceLength)
        {
            Convert(racerPersonList);
            _raceLength = raceLength;
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
                    Ability = racerPerson.Ability,
                    Nation = racerPerson.Nation,
                    RacerPersonId = racerPerson.RacerPersonId,
                    Team = racerPerson.Team,
                    Age = racerPerson.Age,
                    PotentialAbility = racerPerson.PotentialAbility
                };

                _racerList.Add(newRacer);
            }
        }

        private void Sort()
        {
            _racerList = _racerList.OrderByDescending(r => r.RacePosition).ToList();
        }

        private void Move()
        {
            do
            {
                Sort();

                IList<Racer> tempRacerList = _racerList;

                foreach (var racer in tempRacerList)
                {
                    if (racer.RacePosition < _raceLength)
                    {
                        racer.RacePosition += (float)_random.Next(1, racer.Ability)/10;
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
                newRacerPerson.Ability = racer.Ability;
                newRacerPerson.Nation = racer.Nation;
                newRacerPerson.RacerPersonId = racer.RacerPersonId;
                newRacerPerson.Team = racer.Team;

                ranking.Add(newRacerPerson);
            }

            return ranking;
        }
    }
}
