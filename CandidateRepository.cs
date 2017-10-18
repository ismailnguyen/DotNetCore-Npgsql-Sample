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
    }
}
