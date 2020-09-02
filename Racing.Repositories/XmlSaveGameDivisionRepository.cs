using Racing.Model;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing.Repositories
{
    public class XmlSaveGameDivisionRepository : ISaveGameRepository<Division>
    {
        private readonly string path = @".\Racing2020Save";
        private readonly string file = $"SaveGame.xml";

        public XmlSaveGameDivisionRepository()
        {
            Create();
        }

        public IList<Division> Load()
        {
            // to get nation, use the id and nationcontroller
            throw new System.NotImplementedException();
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
                    writer.WriteStartElement(nameof(division));
                    writer.WriteAttributeString(nameof(division.DivisionId), division.DivisionId.ToString());
                    writer.WriteAttributeString(nameof(division.Tier), division.Tier.ToString());
                    writer.WriteAttributeString(nameof(division.Name), division.Name);

                    foreach (var team in division.TeamList)
                    {
                        writer.WriteStartElement(nameof(team));
                        writer.WriteAttributeString(nameof(team.TeamId), team.TeamId.ToString());
                        //isn't Id enough?
                        writer.WriteAttributeString(nameof(team.Name), team.Name);

                        foreach (var racerPerson in team.RacerPeople)
                        {
                            writer.WriteStartElement(nameof(racerPerson));
                            writer.WriteAttributeString(nameof(racerPerson.RacerPersonId), racerPerson.RacerPersonId.ToString());
                            writer.WriteAttributeString(nameof(racerPerson.FirstName), racerPerson.FirstName);
                            writer.WriteAttributeString(nameof(racerPerson.LastName), racerPerson.LastName);
                            writer.WriteAttributeString(nameof(racerPerson.Nation.NationId), racerPerson.Nation.NationId.ToString());
                            writer.WriteAttributeString(nameof(racerPerson.Ability), racerPerson.Ability.ToString());
                            writer.WriteAttributeString(nameof(racerPerson.PotentialAbility), racerPerson.PotentialAbility.ToString());
                            writer.WriteAttributeString(nameof(racerPerson.Age), racerPerson.Age.ToString());
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.Flush();
            }

            using (StreamWriter streamWriter = File.CreateText(Path.Combine(path, file)))
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

            if (!File.Exists(Path.Combine(path, file)))
            {
                var createdFile = File.Create(Path.Combine(path, file));
                createdFile.Close();
            }
        }
    }
}
