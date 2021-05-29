using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AgentsResponse
    {
        public List<AgentDto> Metrics { get; set; }
    }

    public class AgentDto
    {
        public int AgentId { get; set; }
        public string AgentUri { get; set; }
    }
}
