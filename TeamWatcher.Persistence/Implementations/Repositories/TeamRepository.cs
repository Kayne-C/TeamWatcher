using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWatcher.Application.Interfaces.Repositories;
using TeamWatcher.Domain.Entities;

namespace TeamWatcher.Persistence.Implementations.Repositories
{
    public class TeamRepository : ITeamRepository
    {

        private readonly string _connectionString;

        public TeamRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TeamWatcherCnStr");
        }

        public async Task<Team> CreateAsync(Team entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Teams (Name, LogoUrl) VALUES (@Name, @LogoUrl)";
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                        sqlCommand.Parameters.AddWithValue("@LogoUrl", entity.LogoUrl);
                        connection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();
                        return entity;
                    }
                }
                catch (Exception exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "DELETE FROM Teams WHERE Id = @Id";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<List<Team>> GetAllAsync()
        {
            List<Team> teams = new List<Team>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {

                    string query = "SELECT * FROM Teams WHERE IsActive = 1";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                        while (reader.Read())
                        {
                            Team team = new Team
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                LogoUrl = reader["LogoUrl"].ToString()
                            };
                            teams.Add(team);
                        }
                        reader.Close();
                    }
                }
                catch (Exception exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close();
                }
                return teams;
            }
        }

        public async Task<Team> GetSingleAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                Team team = null;
                try
                {

                    string query = "SELECT * FROM Teams WHERE Id = @Id";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                        if (reader.Read())
                        {
                            team = new Team
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                LogoUrl = reader["LogoUrl"].ToString()
                            };
                        }
                        reader.Close();
                    }
                }
                catch (Exception exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close();
                }

                return team;
            }
        }

        public async Task<Team> UpdateAsync(Team entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {

                    string query = "UPDATE Teams SET Name = @Name, LogoUrl = @LogoUrl WHERE Id = @Id";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    { 
                        sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                        sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                        sqlCommand.Parameters.AddWithValue("@LogoUrl", entity.LogoUrl);
                        connection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();
                        return entity;
                    }
                }
                catch (Exception exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
