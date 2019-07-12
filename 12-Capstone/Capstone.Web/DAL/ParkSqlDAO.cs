using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private readonly string connectionString;

        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Park GetParkDetails(string parkCode)
        {
            Park park = new Park();
           // parkCode = "CVNP";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"SELECT * FROM Park WHERE parkCode = @parkCode;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        //Create a Forum
                        
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acerage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        park.NumberOfCampSites = Convert.ToInt32(reader["numberOfCampSites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return park;
        }


        public IList<Park> GetParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"SELECT parkCode, parkName, state, parkDescription FROM park ORDER BY parkName ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        //Create a Forum
                        Park park = new Park();
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        parks.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return parks;
        }
    }
}
