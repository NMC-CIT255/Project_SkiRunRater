using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration.Assemblies;


namespace SkiRunRater
{
    public class SkiRunRepositorySQL : ISkiRunRepository
    {
        private IList<SkiRun> _skiRuns = new List<SkiRun>();

        public SkiRunRepositorySQL()
        {
            _skiRuns = ReadAllSkiRuns();
        }

        private IList<SkiRun> ReadAllSkiRuns()
        {
            IList<SkiRun> skiRuns = new List<SkiRun>();

            string connString = GetConnectionString();
            string sqlCommandString = "SELECT * from SkiRuns";
            //SqlConnection sqlConn = new SqlConnection(connString);
            //SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn);

            //using (SqlConnection sqlConn = new SqlConnection(connString))
            //{
            //    try
            //    {
            //        sqlConn.Open();
            //        using (sqlCommand)
            //        {
            //            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            //            {
            //                if (reader != null)
            //                {
            //                    while (reader.Read())
            //                    {
            //                        SkiRun skiRun = new SkiRun();
            //                        skiRun.ID = Convert.ToInt32(reader["ID"]);
            //                        skiRun.Name = reader["Name"].ToString();
            //                        skiRun.Vertical = Convert.ToInt32(reader["Vertical"]);
            //                        skiRuns.Add(skiRun);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (SqlException sqlEx)
            //    {
            //        Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
            //        Console.WriteLine(sqlCommandString);
            //    }
            //}

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn))
            {
                try
                {
                    sqlConn.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                SkiRun skiRun = new SkiRun();
                                skiRun.ID = Convert.ToInt32(reader["ID"]);
                                skiRun.Name = reader["Name"].ToString();
                                skiRun.Vertical = Convert.ToInt32(reader["Vertical"]);
                                skiRuns.Add(skiRun);
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }

            return skiRuns;
        }

        /// <summary>
        /// method to return a ski run given the ID
        /// uses a DataSet to hold ski run info
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun SelectById(int Id)
        {
            return _skiRuns.Where(sr => sr.ID == Id).FirstOrDefault();
        }

        /// <summary>
        /// method to return a list of ski runs
        /// uses a DataSet to hold ski run info
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> SelectAll()
        {
            return _skiRuns as List<SkiRun>;
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void Insert(SkiRun skiRun)
        {
            string connString = GetConnectionString();

            // build out SQL command
            var sb = new StringBuilder("INSERT INTO SkiRuns");
            sb.Append(" ([ID],[Name],[Vertical])");
            sb.Append(" Values (");
            sb.Append("'").Append(skiRun.ID).Append("',");
            sb.Append("'").Append(skiRun.Name).Append("',");
            sb.Append("'").Append(skiRun.Vertical).Append("')");
            string sqlCommandString = sb.ToString();

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            using (sqlConn)
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            string connString = GetConnectionString();

            // build out SQL command
            var sb = new StringBuilder("DELETE FROM SkiRuns");
            sb.Append(" WHERE ID = ").Append(ID);
            string sqlCommandString = sb.ToString();

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            using (sqlConn)
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void Update(SkiRun skiRun)
        {
            string connString = GetConnectionString();

            // build out SQL command
            var sb = new StringBuilder("UPDATE SkiRuns SET ");
            sb.Append("Name = '").Append(skiRun.Name).Append("', ");
            sb.Append("Vertical = ").Append(skiRun.Vertical).Append(" ");
            sb.Append("WHERE ");
            sb.Append("ID = ").Append(skiRun.ID);
            string sqlCommandString = sb.ToString();

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            using (sqlConn)
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.UpdateCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        /// <summary>
        /// method to query the data by the vertical of each ski run in feet
        /// </summary>
        /// <param name="minimumVertical">int minimum vertical</param>
        /// <param name="maximumVertical">int maximum vertical</param>
        /// <returns></returns>
        public IEnumerable<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            return _skiRuns.Where(sr => sr.Vertical >= minimumVertical && sr.Vertical <= maximumVertical);
        }

        /// <summary>
        /// get the connection string by name
        /// </summary>
        /// <returns>string connection string</returns>
        private static string GetConnectionString()
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            var settings = ConfigurationManager.ConnectionStrings["SkiRunRater_Local"];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _skiRuns = null;
        }
    }
}
