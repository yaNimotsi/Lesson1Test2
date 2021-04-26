using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lesson1Test2
{
    [Serializable]
    public class ValuesHolder :IFormatter
    {
        public List<string> Values;
        public DateTime Date { get; set; }
        public int temp { get; set; }

        [NonSerialized]
        private BinaryFormatter _formatter = new BinaryFormatter();

        public object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            throw new NotImplementedException();
        }

        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }

        public ValuesHolder()
        {
            using (FileStream fs = new FileStream("temp.dat", FileMode.OpenOrCreate))
            {
                var desiarilezeStrings = (List<string>) _formatter.Deserialize(fs);
                Values = (List<string>) _formatter.Deserialize(fs);
            }
        }

        private void ToSerialize(List<string> Values)
        {
            using (FileStream fs = new FileStream("temp.dat", FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, Values);
            }
        }

        private void AddValues(string val)
        {
            string date = DateTime.Now.ToShortDateString();
            Values.Add($"Temperature for date {date} is {val}");
        }

        /// <summary>
        /// Find and update string by user date
        /// </summary>
        /// <param name="stringToUpdate"></param>
        /// <param name="newVal"></param>
        private void UpdateVal(string stringToUpdate, string newVal)
        {
            for (var i = 0; i < Values.Count; i++)
            {
                var strings = Values[i].Split(' ');

                if (strings[3] == stringToUpdate)
                    Values[i] = $"Temperature for date {strings[3]} is {newVal}";
            }
        }
    }
}
