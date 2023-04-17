using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TaxCalculator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaxController : ApiController
    {
        private readonly string connectionString = "Server=localhost;Database=your_database_name;Uid=root;Pwd=admin;";

        [HttpGet]
        public IHttpActionResult GetTaxPayment(decimal income)
        {
            var threshold = 0m;
            var minPay = 0m;
            var rate = 0m;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT MIN(THRESHOLD) FROM brackets WHERE Threshold >= @income";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@income", income);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            threshold = reader.GetDecimal(0);
                        }
                        else
                        {
                            threshold = -1;
                        }
                    }
                }
            }

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT Percent FROM brackets WHERE Threshold = @threshold";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@threshold", threshold);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rate = reader.GetDecimal(0);
                        }
                    }
                }
            }

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT Minimum FROM brackets WHERE Threshold = @threshold";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@threshold", threshold);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            minPay = reader.GetDecimal(0);
                        }
                    }
                }
            }

            if (threshold == -1)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT MAX(THRESHOLD) FROM brackets";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                threshold = reader.GetDecimal(0);
                            }
                        }
                    }
                }
            }

            var payment = minPay + (income - threshold) * rate;

            return Ok(payment);
        }

     
    }
}
