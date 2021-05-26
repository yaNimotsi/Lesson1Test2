using Dapper;

using MetricsManager.DAL.ConnectionString;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    public interface IAgentsRepository: IAgentRepository<AgentsModel>
    {

    }

    public class AgentsRepository : IAgentsRepository
    {
        private static readonly string ConnectionString = ConnectionToDB.ConnectionString;
        
        public List<AgentsModel> GetAllAgent()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentsModel>("SELECT AgentId, AgentUrl FROM Agents").ToList();
            }
        }
    }
}