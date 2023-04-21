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
            string query = "SELECT TOP (1) AgeID, Age, TaxFree FROM age_threshold WHERE Age >= @age ORDER BY Age ASC;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ageRecord.ID = reader.GetInt32(0);
                    ageRecord.Age = reader.GetInt32(1);
                    ageRecord.MinimumYearlySalary = reader.GetDecimal(2);
                }
                cmd.Connection.Close();
                conn.Close();
                reader.Close();
            }
            return ageRecord;

        }

        public static List<TaxPercentage> getTaxRates(decimal taxableIncome)
        {
            List<TaxPercentage> rates = new List<TaxPercentage>();
            string query = @"SELECT BracketID, Threshold, TaxRate FROM Brackets WHERE Threshold <= @taxableIncome";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@taxableIncome", taxableIncome);
                conn.Open();
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
                conn.Close();
                reader.Close();
            }

            return rates;

        }

        public static Dictionary<string, Deduction> getDeductions()
        {
            Dictionary<string, Deduction> deductions = new Dictionary<string, Deduction>();

            string query = @"SELECT DeductionID, DeductionDescription, DeductionRateMax, DeductionAmountMax, DeductionRate FROM Deduction;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Deduction deduction = new()
                    {
                        DeductionID = reader.GetInt32(0),
                        DeductionDescription = reader.GetString(1),
                        DeductionRateMax = reader.GetDecimal(2),
                        DeductionAmountMax = reader.GetDecimal(3),
                        DeductionRate = reader.GetDecimal(4)
                    };
                    deductions.Add(deduction.DeductionDescription, deduction); 
                }
                cmd.Connection.Close();
                conn.Close();
                reader.Close();
            }

            return deductions;
        }

    }
}
