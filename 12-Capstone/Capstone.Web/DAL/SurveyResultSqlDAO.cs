using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveyResultSqlDAO : ISurveyResultDAO
    {

        private readonly string connectionString;

        public SurveyResultSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public void AddSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //TODO: Finish this query
                    conn.Open();

                    // Start a transaction
                    SqlTransaction transaction = conn.BeginTransaction();

                    string sql = $"Insert Into survey_result(parkCode, emailAddress, state, activityLevel) Values (@parkcode, @emailAddress, @state, @activityLevel);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public IList<Survey> GetSurveys()
        {
            List<Survey> surveys = new List<Survey>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"SELECT * from survey_result";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        //Create a Forum
                        Survey survey = new Survey();
                        survey.SurveyId = Convert.ToInt32(reader["surveyId"]);
                        survey.ParkCode = Convert.ToString(reader["parkCode"]);
                        survey.EmailAddress = Convert.ToString(reader["emailAddress"]);
                        survey.State = Convert.ToString(reader["state"]);
                        survey.ActivityLevel = Convert.ToString(reader["activityLevel"]);
                        surveys.Add(survey);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return surveys;
        }


    }
}
