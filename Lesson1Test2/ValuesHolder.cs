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

        /// <summary>
        /// Метод для вывода прогнозов
        /// </summary>
        /// <returns></returns>
        public string MyToString()
        {
            var rez = "";
            foreach (var dataHolder in Values)
            {
                rez += $"For date {dataHolder.DateForecast.ToShortDateString()} temp is {dataHolder.Forecast}; \n";
            }
            return rez;
        }

        /// <summary>
        /// Добавление нового прогноза
        /// </summary>
        /// <param name="dateForecast"> Дата, к которой принадлежит прогноз</param>
        /// <param name="forecast"> Значение прогноза</param>
        /// <returns></returns>
        public List<DataHolder> AddForecast(DateTime dateForecast, double forecast)
        {
            Values = new List<DataHolder>();
            Values = JsonDeserialize();

            var dataHolder = new DataHolder()
            {
                DateForecast = dateForecast,
                Forecast = forecast
            };

            Values.Add(dataHolder);
            UpdateJson();

            return Values;
        }

        /// <summary>
        /// Удаление прогноза по указанной дате
        /// </summary>
        /// <param name="startDateRange"> Начальный диапазон дат, подлежащих удалению</param>
        /// <param name="endtDateRange"> конечный диапазон дат, подлежащих удалению</param>
        /// <returns></returns>
        public List<DataHolder> DeleteForecast(DateTime startDateRange, DateTime endtDateRange)
        {
            Values = JsonDeserialize();

            Values = Values.Where(w => (w.DateForecast <= startDateRange || w.DateForecast >= endtDateRange)).ToList();

            UpdateJson();

            return Values;
        }

        /// <summary>
        /// Получение прогнозов, в указанном диапазоне дат
        /// </summary>
        /// <param name="startDateRange"> Начальная дата</param>
        /// <param name="endDateRange"> Конечная дата</param>
        /// <returns></returns>
        public List<DataHolder> GetForecastByRange(DateTime startDateRange, DateTime endDateRange)
        {
            Values = JsonDeserialize();

            Values = Values.Where(w => (w.DateForecast >= startDateRange && w.DateForecast <= endDateRange)).ToList();
            
            return Values;
        }

        /// <summary>
        /// Изменение прогноза на указанную дату
        /// </summary>
        /// <param name="dateToUpdate"> Дата, к которой надо изменить прогноз</param>
        /// <param name="newVal"> Новое значение прогноза</param>
        /// <returns></returns>
        public List<DataHolder> UpdateForecast(DateTime dateToUpdate, double newVal)
        {
            Values = JsonDeserialize();

            foreach (var t in Values.Where(t => t.DateForecast == dateToUpdate))
            {
                t.Forecast = newVal;
                break;
            }

            UpdateJson();

            return Values;
        }
    }
}
