﻿using Aboriginal_Art_Gallery_of_Australia.Models.Database_Models;
using Aboriginal_Art_Gallery_of_Australia.Models.DTOs;
using Aboriginal_Art_Gallery_of_Australia.Persistence.Interfaces;
using BCrypt.Net;
using Npgsql;

namespace Aboriginal_Art_Gallery_of_Australia.Persistence.Implementations.ADO
{
    public class UserADO : IUserDataAccess
    {
        private readonly IConfiguration _configuration;

        public UserADO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<UserOutputDto> GetUsers()
        {
            var users = new List<UserOutputDto>();
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresSQL"));
            {
                connection.Open();
                using var cmd = new NpgsqlCommand("SELECT * FROM account", connection);
                {
                    using var dr = cmd.ExecuteReader();
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var userID = (int)dr["account_id"];
                                var firstName = (string)dr["first_name"];
                                var lastName = (string)dr["last_name"];
                                var email = (string)dr["email"];
                                var passwordHash = (string)dr["password_hash"];
                                var role = (string)dr["role"];
                                var activeAt = (DateTime)dr["active_at"];
                                var modifiedAt = (DateTime)dr["modified_at"];
                                var createdAt = (DateTime)dr["created_at"];
                                users.Add(new UserOutputDto(userID, firstName, lastName, email, passwordHash, role, activeAt, modifiedAt, createdAt));
                            }
                        }
                        return users;
                    }
                }
            }
        }

        public UserOutputDto? GetUserById(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresSQL"));
            {
                connection.Open();
                using var cmd = new NpgsqlCommand("SELECT * FROM account WHERE account_id = @accountId", connection);
                {
                    cmd.Parameters.AddWithValue("@userId", id);
                    using var dr = cmd.ExecuteReader();
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var userID = (int)dr["accountId"];
                                var firstName = (string)dr["first_name"];
                                var lastName = (string)dr["last_name"];
                                var email = (string)dr["email"];
                                var passwordHash = (string)dr["password_hash"];
                                var role = (string)dr["role"];
                                var activeAt = (DateTime)dr["active_at"];
                                var modifiedAt = (DateTime)dr["modified_at"];
                                var createdAt = (DateTime)dr["created_at"];
                                return new UserOutputDto(userID, firstName, lastName, email, passwordHash, role, activeAt, modifiedAt, createdAt);
                            }
                        }
                        return null;
                    }
                }
            }
        }

        public UserInputDto? InsertUser(UserInputDto user)
        {

            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresSQL"));
            {
                connection.Open();
                using var cmd = new NpgsqlCommand("INSERT INTO account(first_name, last_name, email, password_hash, role, active_at, modified_at, created_at) " +
                                                  "VALUES (@firstName, @lastName, @email, @passwordHash, @role, @activeAt current_timestamp, current_timestamp);", connection);
                {
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@passwordHash", BC.EnhancedHashPassword(user.Password, hashType: HashType.SHA384));
                    cmd.Parameters.AddWithValue("@role", "Member");
                    cmd.Parameters.AddWithNullableValue("@activeAt", null);
                    var result = cmd.ExecuteNonQuery();
                    return result is 1 ? user : null;
                }
            }
        }

        public UserInputDto? UpdateUser(int id, UserInputDto user)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresSQL"));
            {
                connection.Open();
                using var cmd = new NpgsqlCommand("UPDATE account " +
                                                  "SET first_name = @firstName, " +
                                                      "last_name = @lastName, " +
                                                      "email = @email, " +
                                                      "password_hash = @password_hash, " +
                                                      "modified_at = current_timestamp " +
                                                  "WHERE account_id = @accountId", connection);
                {
                    cmd.Parameters.AddWithValue("@accountId", id);
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@passwordHash", BC.EnhancedHashPassword(user.Password, hashType: HashType.SHA384));
                    var result = cmd.ExecuteNonQuery();
                    return result is 1 ? user : null;
                }
            }
        }

        public bool DeleteUser(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresSQL"));
            {
                connection.Open();
                using var cmd = new NpgsqlCommand("DELETE FROM account WHERE account_id = @accountId", connection);
                {
                    cmd.Parameters.AddWithValue("@accountId", id);
                    var result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
        }
    }
}