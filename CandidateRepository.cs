using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace dotnetcore_postgresql_sample
{
    public class CandidateRepository
    {
        private string connectionString;

        public CandidateRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IDbConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(this.connectionString);

            connection.Open();

            return connection;
        }

        public IEnumerable<Candidate> FindAll()
        {
            using (var connection = OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    var query = "select * from candidates";
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader != null)
                        {
                            yield return new Candidate()
                            {
                                Id = reader.GetInt32(0),
                                Email =  reader.GetString(1)
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Candidate> Find(string email)
        {
            using (var connection = OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    var query = "select * from candidates where email like @email";
                    command.CommandText = query;

                    var emailParameter = new NpgsqlParameter("@email", email);

                    command.Parameters.Add(emailParameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader != null)
                        {
                            yield return new Candidate()
                            {
                                Id = reader.GetInt32(0),
                                Email =  reader.GetString(1)
                            };
                        }
                    }
                }
            }
        }

        public void Insert(string email)
        {
            
        }
    }
}
