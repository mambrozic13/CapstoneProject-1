using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.DAL
{
    public class SurveyResultSqlDAO : ISurveyResultDAO
    {

        private readonly string connectionString;

        public SurveyResultSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }


      
        public int AddSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //TODO: Finish this query
                    conn.Open();

                    // Start a transaction
           

                    string sql = $"Insert Into survey_result(parkCode, emailAddress, state, activityLevel) Values (@parkcode, @emailAddress, @state, @activityLevel); Select @@Identity";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);
                    
                    int surveyId = Convert.ToInt32(cmd.ExecuteScalar());
                  
                    return surveyId;
                    
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

           
        }


        //public IList<Survey> GetSurveys()
        //{
        //    List<Survey> surveys = new List<Survey>();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            string sql = $"SELECT * from survey_result";

        //            SqlCommand cmd = new SqlCommand(sql, conn);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            // Loop through each row
        //            while (reader.Read())
        //            {
        //                //Create a Forum
        //                Survey survey = new Survey();
        //                survey.SurveyId = Convert.ToInt32(reader["surveyId"]);
        //                survey.ParkCode = Convert.ToString(reader["parkCode"]);
        //                survey.EmailAddress = Convert.ToString(reader["emailAddress"]);
        //                survey.State = Convert.ToString(reader["state"]);
        //                survey.ActivityLevel = Convert.ToString(reader["activityLevel"]);
        //                surveys.Add(survey);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    return surveys;
        //}

        public IList<SurveyResult> GetSurveyResults()
        {
            List<SurveyResult> surveyResults = new List<SurveyResult>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"SELECT survey_result.parkCode, park.parkName, Count(*) As count FROM survey_result INNER JOIN park ON survey_result.parkCode = park.parkCode GROUP BY survey_result.parkCode, park.parkName ORDER BY count DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        //Create a Forum
                        SurveyResult survey = new SurveyResult()
                        {
                            ParkName = Convert.ToString(reader["parkName"]),
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            Count = Convert.ToInt32(reader["count"]),
                        };
                        surveyResults.Add(survey);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return surveyResults;
        }

    }
}
