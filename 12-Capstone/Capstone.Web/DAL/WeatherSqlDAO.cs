using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAO : IWeatherDAO
    {
        private readonly string connectionString;

        public WeatherSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> GetWeather(string parkCode, string tempUnit)
        {
            List<Weather> list = new List<Weather>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"SELECT * FROM weather WHERE parkCode = @parkCode;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {

                        Weather weather = new Weather();
                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.Low = Convert.ToInt32(reader["low"]);
                        weather.High = Convert.ToInt32(reader["high"]);

                        if (tempUnit == "C")
                        {
                            weather.Low = (int)((((double)weather.Low )- 32) / 1.8);
                            weather.High = (int)((((double)weather.High) - 32) / 1.8);

                        }
                        weather.Forecast = Convert.ToString(reader["forecast"]);
                        list.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return list;
        }

    }
}
    

