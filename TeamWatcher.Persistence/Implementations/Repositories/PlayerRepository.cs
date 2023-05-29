using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWatcher.Application.Interfaces.Repositories;
using TeamWatcher.Domain.Entities;
using TeamWatcher.Persistence.Context;


namespace TeamWatcher.Persistence.Implementations.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(IConfiguration configuration)
        {
            _connectionString =  configuration.GetConnectionString("TeamWatcherCnStr");
        }
        public async Task<Player> CreateAsync(Player entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "INSERT INTO Players (Name, Surname, Age, TeamId) VALUES (@Name, @Surname, @Age, @TeamId)";
                    using (SqlCommand SqlCommand = new SqlCommand(query, connection))
                    {
                        SqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                        SqlCommand.Parameters.AddWithValue("@Surname", entity.Surname);
                        SqlCommand.Parameters.AddWithValue("@Age", entity.Age);
                        SqlCommand.Parameters.AddWithValue("@TeamId", entity.TeamId);
                        connection.Open();
                        SqlCommand.ExecuteNonQuery();
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
                    string query = "DELETE FROM Players WHERE Id = @Id";
                    using (SqlCommand SqlCommand = new SqlCommand(query, connection))
                    {
                        SqlCommand.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        await SqlCommand.ExecuteNonQueryAsync();
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

        public async Task<List<Player>> GetAllAsync(int teamId)
        {
            List<Player> players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Players WHERE TeamId = @TeamId";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@TeamId", teamId);
                connection.Open();
                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Player player = new Player
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Surname = (string)reader["Surname"],
                        Age = (int)reader["Age"],
                        TeamId = (int)reader["TeamId"]
                    };
                    players.Add(player);
                }
                reader.Close();
            }
            return players;
        }

        public async Task<Player> GetSingleAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Players WHERE Id = @Id";
                Player player = new Player();
                using (SqlCommand SqlCommand = new SqlCommand(query, connection))
                {
                    SqlCommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    SqlDataReader reader = await SqlCommand.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        player.Id = Convert.ToInt32(reader["Id"]);
                        player.Name = reader["Name"].ToString();
                        player.Age = Convert.ToInt32(reader["Age"]);
                    }
                    reader.Close();
                }
                return player;
            }
        }

        public async Task<Player> UpdateAsync(Player entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "UPDATE Players SET Name = @Name, Surname = @Surname, Age = @Age WHERE Id = @Id";
                    using (SqlCommand SqlCommand = new SqlCommand(query, connection))
                    {
                        SqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                        SqlCommand.Parameters.AddWithValue("@Surname", entity.Surname);
                        SqlCommand.Parameters.AddWithValue("@Age", entity.Age);
                        SqlCommand.Parameters.AddWithValue("@Id", entity.Id);

                        connection.Open();
                        await SqlCommand.ExecuteNonQueryAsync();
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
