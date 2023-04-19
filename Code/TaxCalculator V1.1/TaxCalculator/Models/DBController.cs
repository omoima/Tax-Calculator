using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace TaxCalculator.Models
{
    public class DBController
    {
        private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=Tax;Trusted_Connection=True;";

        public DBController()
        {

        }

        public static AgeThreshold getTaxFree(int age)
        {
            AgeThreshold ageRecord = new AgeThreshold();
            string query = @"SELECT TOP (1) AgeID, Age, TaxFree FROM age_threshold WHERE Age >= " + age.ToString() + " ORDER BY Age ASC;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    ageRecord.ID = reader.GetInt32(0);
                    ageRecord.Age = reader.GetInt32(1);
                    ageRecord.MinimumYearlySalary = reader.GetDecimal(2);
                }
            }
            return ageRecord;
        }

        public static List<TaxPercentage> getTaxRates(decimal taxableIncome)
        {
            List<TaxPercentage> rates = new List<TaxPercentage>();
            string query = @"SELECT BracketID, Threshold, [Percent] FROM Brackets WHERE Threshold <= " + taxableIncome.ToString().Replace(',', '.') + ";";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaxPercentage taxPercentage = new()
                    {
                        ID = reader.GetInt32(0),
                        SalaryThreshold = reader.GetDecimal(1),
                        TaxPercent = reader.GetDecimal(2)
                    };

                    rates.Add(taxPercentage);
                }
            }
            return rates;
        }

    }
}
