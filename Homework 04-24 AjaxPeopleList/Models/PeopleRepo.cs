﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Homework_04_24_AjaxPeopleList.Models
{

    public class PeopleRepo
    {
        private string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<Person> GetAll()
        {
            var list = new List<Person>();

            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }

        public void Add(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES (@first, @last, @age)";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People " +
                              "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();

            cmd.ExecuteNonQuery();
        }


        public Person GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People " +
                              "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();

            var reader = cmd.ExecuteReader();

            if(!reader.Read())
            {
                return null;
            }

            return new(){
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            };

        }


        public void Update(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People " +
                              "SET FirstName = @firstName, " +
                              "LastName = @lastName, " +
                              "Age = @age " +
                              "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@firstName", p.FirstName);
            cmd.Parameters.AddWithValue("@lastName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            cmd.Parameters.AddWithValue("@id", p.Id);
            connection.Open();

            cmd.ExecuteNonQuery();
        }


    }
}


