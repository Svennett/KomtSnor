using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using KomtSnor.Domain;
using KomtSnor.Domain.Users;

namespace KomtSnor.Gateways
{
    public class SQLServerGateway
    {
        public ArrayList ExecuteSelectCommand(SqlCommand sqlCommand)
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KomtSnorDatabase"].ConnectionString;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                sqlCommand.Connection = connection;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                ArrayList allRows = getValues(sqlDataReader);
                return allRows;
            }
        }

        private ArrayList getValues(SqlDataReader reader)
        {
            ArrayList allValues = new ArrayList();
            while (reader.Read())
            {
                Object[] values = new object[reader.FieldCount];
                int fieldCount = reader.GetValues(values);
                allValues.Add(values);
            }
           
            return allValues;
        }

        public void ExecuteInsertCommand(SqlCommand sqlCommand)
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KomtSnorDatabase"].ConnectionString;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                sqlCommand.Connection = connection;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            }
        }
    }
}