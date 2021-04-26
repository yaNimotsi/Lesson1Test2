using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Lesson1Test2
{
    public class ValuesHolder
    {
        
        public static List<DataHolder> Values;
        
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
        private static List<DataHolder> JsonDeserialize()
        {
            if (!IsJsonFileExists(out var jsonText)) return null;
            var rez = JsonSerializer.Deserialize<List<DataHolder>>(jsonText);
            return rez;
        }

        /// <summary>
        /// Добавление нового прогноза
        /// </summary>
        public static List<DataHolder> AddDataHolder(string newForecast)
        {
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
        public static string GetJsonPath()
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
        private static void UpdateJson()
        {
            var finPath = GetJsonPath();

            var json = JsonSerializer.Serialize(Values.ToArray());

            File.WriteAllText(@"" + finPath, json, Encoding.ASCII);
        }
        

        public static List<DataHolder> UpdateForecast(string dateToUpdate, string newVal)
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

        public static List<DataHolder> DeleteForecast(string dateForDelete)
        {
            Values = JsonDeserialize();

            Values = Values.Where(w => w.DateForecast != dateForDelete).ToList();

            UpdateJson();

            return Values;
        }
        
    }
}
