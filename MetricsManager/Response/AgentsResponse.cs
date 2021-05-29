using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AgentsResponse
    {
        public List<AgentcDto> Metrics { get; set; }
    }

    public class AgentcDto
    {
        public int AgentId { get; set; }
        public string AgentUri { get; set; }
    }
}
