using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AllAgentsResponse
    {
        public List<AgentsDto> Metrics { get; set; }
    }

    public class AgentsDto
    {
        public string AgentPath { get; set; }
        public int Id { get; set; }
    }
}
