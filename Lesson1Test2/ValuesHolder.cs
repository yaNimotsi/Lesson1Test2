using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Lesson1Test2
{
    public class ValuesHolder: List<DataHolder>
    {
        
        public List<DataHolder> Values;
        
        public ValuesHolder()
        {
            Values = JsonDeserialize();
        }

        /// <summary>
        /// Проверка существования Json файла
        /// </summary>
        /// <param name="jsonText"> Выходная строка хранящаяся в Json файле</param>
        /// <returns></returns>
        private static bool IsJsonFileExists(out string jsonText)
        {
            var finPath = GetJsonPath();

            if (File.Exists(@""+ finPath) == false) 
                File.WriteAllText(@"" + finPath, "", Encoding.ASCII);

            jsonText = File.ReadAllText(@"" + finPath);

            return jsonText.Length > 0;
        }

        /// <summary>
        /// Получение десириализованной строки
        /// </summary>
        /// <returns></returns>
        private List<DataHolder> JsonDeserialize()
        {
            if (!IsJsonFileExists(out var jsonText)) return new List<DataHolder>();
            var rez = JsonSerializer.Deserialize<List<DataHolder>>(jsonText) ?? new List<DataHolder>();
            return rez;
        }

        /// <summary>
        /// Добавление нового прогноза
        /// </summary>
        public List<DataHolder> AddDataHolder(string newForecast)
        {
            Values = new List<DataHolder>();
            Values = JsonDeserialize();

            var dataHolder = new DataHolder()
            {
                DateForecast = DateTime.Now.ToShortDateString(),
                Forecast = newForecast
            };

            Values.Add(dataHolder);
            UpdateJson();

            return Values;
        }

        /// <summary>
        /// Получение пути файла Json
        /// </summary>
        /// <returns></returns>
        private static string GetJsonPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase ?? string.Empty);
            var path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            return Path.Combine(path ?? string.Empty, "DataHolder.json");
        }

        /// <summary>
        /// Добавление записи в Json
        /// </summary>
        private void UpdateJson()
        {
            var finPath = GetJsonPath();

            var json = JsonSerializer.Serialize(Values.ToArray());

            File.WriteAllText(@"" + finPath, json, Encoding.ASCII);
        }
        

        public List<DataHolder> UpdateForecast(string dateToUpdate, string newVal)
        {
            Values = JsonDeserialize();

            foreach (var t in Values)
            {
                if (t.DateForecast == dateToUpdate)
                {
                    t.Forecast = newVal;
                    break;
                }
            }

            UpdateJson();

            return Values;
        }

        public List<DataHolder> DeleteForecast(string dateForDelete)
        {
            Values = JsonDeserialize();

            Values = Values.Where(w => w.DateForecast != dateForDelete).ToList();

            UpdateJson();

            return Values;
        }

        public string MyToString()
        {
            var rez = "";
            foreach (var dataHolder in Values)
            {
                rez += $"For date {dataHolder.DateForecast} temp is {dataHolder.Forecast}; \n";
            }
            return rez;
        }
    }
}
