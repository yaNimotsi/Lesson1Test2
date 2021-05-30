using Dapper;

using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    public interface IAgentsRepository : IAgents<AgentModel>
    {

    }

    public class AgentsRepository : IAgentsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;
        
        public List<AgentModel> GetAllAgents()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var rez = connection.Query<AgentModel>("SELECT AgentId, AgentUri FROM Agents").ToList();
                return connection.Query<AgentModel>("SELECT * FROM Agents").ToList();
            }
        }
    }
}