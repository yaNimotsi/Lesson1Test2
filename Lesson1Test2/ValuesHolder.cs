using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Lesson1Test2
{
    public class ValuesHolder
    {
        
        public List<DataHolder> Values;
        
        public ValuesHolder()
        {
            if (!IsJsonFileExists(out var jsonText)) return;

            Values = JsonSerializer.Deserialize<List<DataHolder>>(jsonText);
        }

        /// <summary>
        /// Проверка существования Json файла
        /// </summary>
        /// <param name="jsonText"> Выходная строка хранящаяся в Json файле</param>
        /// <returns></returns>
        private static bool IsJsonFileExists(out string jsonText)
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            var finPath = Path.Combine(path, "DataHolder.json");



            if (File.Exists(@""+ finPath) == false) 
                File.WriteAllText(@"" + finPath, "", Encoding.ASCII);

            jsonText = File.ReadAllText(@"" + finPath);

            return jsonText.Length > 0;
        }

        /// <summary>
        /// Добавление нового прогноза
        /// </summary>
        public void AddDataHolder(string newForecast)
        {
            var dataHolder = new DataHolder()
            {
                DateForecast = DateTime.Now.ToShortDateString(),
                Forecast = newForecast
            };
            Values.Add(dataHolder);
            AddForecastToJson();
        }

        private void AddForecastToJson()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            var finPath = Path.Combine(path, "DataHolder.json");


            var json = JsonSerializer.Serialize(Values.ToArray());

            File.WriteAllText(@"" + finPath, json, Encoding.ASCII);
        }

        public void UpdateForecast()
        {

        }
    }
}
