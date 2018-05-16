using System.IO;
using System.Xml.Serialization;

namespace AniHelp
{
    public static class BlankDtoHelper
    {
        private static readonly XmlSerializer Xs = new XmlSerializer(typeof(AnimalDataDto));
        public static void WriteToFile(string fileName, AnimalDataDto data)
        {
            using (var fileStream = File.Create(fileName))
            {
                Xs.Serialize(fileStream, data);
            }
        }

        public static AnimalDataDto LoadFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return (AnimalDataDto)Xs.Deserialize(fileStream);
            }
        }
    }
}