﻿using Racing.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing.Repositories
{
    public class XmlSaveGameDivisionRepository : ISaveGameRepository<Division>
    {
        private readonly string path = @".\Racing2020Save";
        private readonly string saveGameFile = $"SaveGame.xml";
        private readonly string saveGameSettingsFile = $"SaveGameSettings.xml";

        public XmlSaveGameDivisionRepository()
        {
            Create();
        }

        public IList<Division> Load()
        {
            // to get nation, use the id and nationcontroller
            var divisions = new List<Division>();
            var fileString = File.ReadAllText(Path.Combine(path, saveGameFile));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return divisions;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(divisions));

                    do
                    {
                        var readDivision = new Division();

                        readDivision.DivisionId = int.Parse(xmlReader.GetAttribute(nameof(Division.DivisionId)));
                        readDivision.Tier = int.Parse(xmlReader.GetAttribute(nameof(Division.Tier)));
                        readDivision.Name = xmlReader.GetAttribute(nameof(Division.Name));

                        readDivision.TeamList = new List<Team>();

                        xmlReader.ReadStartElement(nameof(Division));

                        do
                        {
                            var readTeam = new Team();

                            readTeam.TeamId = int.Parse(xmlReader.GetAttribute(nameof(Team.TeamId)));
                            readTeam.Name = xmlReader.GetAttribute(nameof(Team.Name));
                            readTeam.Budget = int.Parse(xmlReader.GetAttribute(nameof(Team.Budget)));
                            readTeam.TrainingFacility = int.Parse(xmlReader.GetAttribute(nameof(Team.TrainingFacility)));
                            readTeam.YouthFacility = int.Parse(xmlReader.GetAttribute(nameof(Team.YouthFacility)));
                            readTeam.FacilityUpgradePreference = (FacilityUpgradePreference)Enum.Parse(typeof(FacilityUpgradePreference), xmlReader.GetAttribute(nameof(Team.FacilityUpgradePreference)));

                            readTeam.RacerPeople = new List<RacerPerson>();

                            xmlReader.ReadStartElement(nameof(Team));


                            do
                            {
                                var readRacerPerson = new RacerPerson();


                                readRacerPerson.RacerPersonId = Guid.Parse(xmlReader.GetAttribute(nameof(RacerPerson.RacerPersonId)));
                                readRacerPerson.FirstName = xmlReader.GetAttribute(nameof(RacerPerson.FirstName));
                                readRacerPerson.LastName = xmlReader.GetAttribute(nameof(RacerPerson.LastName));
                                readRacerPerson.Nation = new Nation { NationId = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.Nation.NationId))) };
                                readRacerPerson.FlatAbility = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.FlatAbility)));
                                readRacerPerson.FlatPotentialAbility = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.FlatPotentialAbility)));
                                readRacerPerson.ClimbingAbility = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.ClimbingAbility)));
                                readRacerPerson.ClimbingPotentialAbility = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.ClimbingPotentialAbility)));
                                readRacerPerson.DownhillAbility = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.DownhillAbility)));
                                readRacerPerson.DownhillPotentialAbility = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.DownhillPotentialAbility)));
                                readRacerPerson.Age = int.Parse(xmlReader.GetAttribute(nameof(RacerPerson.Age)));

                                readTeam.RacerPeople.Add(readRacerPerson);
                            } while (xmlReader.ReadToNextSibling(nameof(RacerPerson)));

                            readDivision.TeamList.Add(readTeam);
                        } while (xmlReader.ReadToNextSibling(nameof(Team)));

                        divisions.Add(readDivision);
                    } while (xmlReader.ReadToNextSibling(nameof(Division)));
                }
            }

            return divisions;
        }

        public bool Save(IList<Division> divisions)
        {
            var stream = new StringWriter();
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(divisions));

                foreach (var division in divisions)
                {
                    writer.WriteStartElement(nameof(Division));
                    writer.WriteAttributeString(nameof(Division.DivisionId), division.DivisionId.ToString());
                    writer.WriteAttributeString(nameof(Division.Tier), division.Tier.ToString());
                    writer.WriteAttributeString(nameof(Division.Name), division.Name);

                    foreach (var team in division.TeamList)
                    {
                        writer.WriteStartElement(nameof(Team));
                        writer.WriteAttributeString(nameof(Team.TeamId), team.TeamId.ToString());
                        //isn't Id enough?
                        writer.WriteAttributeString(nameof(Team.Name), team.Name);
                        writer.WriteAttributeString(nameof(Team.Budget), team.Budget.ToString());
                        writer.WriteAttributeString(nameof(Team.TrainingFacility), team.TrainingFacility.ToString());
                        writer.WriteAttributeString(nameof(Team.YouthFacility), team.YouthFacility.ToString());
                        writer.WriteAttributeString(nameof(Team.FacilityUpgradePreference), team.FacilityUpgradePreference.ToString());

                        foreach (var racerPerson in team.RacerPeople)
                        {
                            writer.WriteStartElement(nameof(RacerPerson));
                            writer.WriteAttributeString(nameof(RacerPerson.RacerPersonId), racerPerson.RacerPersonId.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.FirstName), racerPerson.FirstName);
                            writer.WriteAttributeString(nameof(RacerPerson.LastName), racerPerson.LastName);
                            writer.WriteAttributeString(nameof(RacerPerson.Nation.NationId), racerPerson.Nation.NationId.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.FlatAbility), racerPerson.FlatAbility.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.FlatPotentialAbility), racerPerson.FlatPotentialAbility.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.ClimbingAbility), racerPerson.ClimbingAbility.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.ClimbingPotentialAbility), racerPerson.ClimbingPotentialAbility.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.DownhillAbility), racerPerson.DownhillAbility.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.DownhillPotentialAbility), racerPerson.DownhillPotentialAbility.ToString());
                            writer.WriteAttributeString(nameof(RacerPerson.Age), racerPerson.Age.ToString());
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.Flush();
            }

            using (StreamWriter streamWriter = File.CreateText(Path.Combine(path, saveGameFile)))
            {
                streamWriter.Write(stream);
            }

            return true;
        }

        public int GetPlayerTeamId()
        {
            int teamId;

            var fileString = File.ReadAllText(Path.Combine(path, saveGameSettingsFile));

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    teamId = int.Parse(xmlReader.GetAttribute(nameof(Team.TeamId)));
                }
            }

            return teamId;
        }

        public bool SaveGameSettings(int playerTeamId)
        {
            var stream = new StringWriter();
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(Team));
                writer.WriteAttributeString(nameof(Team.TeamId), playerTeamId.ToString());
                writer.WriteEndElement();

                writer.Flush();
            }

            using (StreamWriter streamWriter = File.CreateText(Path.Combine(path, saveGameSettingsFile)))
            {
                streamWriter.Write(stream);
            }

            return true;
        }

        private void Create()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(Path.Combine(path, saveGameFile)))
            {
                var createdFile = File.Create(Path.Combine(path, saveGameFile));
                createdFile.Close();
            }

            if (!File.Exists(Path.Combine(path, saveGameSettingsFile)))
            {
                var createdFile = File.Create(Path.Combine(path, saveGameSettingsFile));
                createdFile.Close();
            }
        }
    }
}
