using FluentMigrator;

namespace MetricsManager.DAL.Migration
{
    [Migration(1)]
    public class FirstMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Agents")
                .WithColumn("agentId ").AsInt64().PrimaryKey().Identity()
                .WithColumn("agentUrl").AsString();

            Create.Table("CpuMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("agentId").AsInt64()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("DotNetMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("agentId").AsInt64()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("HddMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("agentId").AsInt64()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("NetworkMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("agentId").AsInt64()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("RamMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("agentId").AsInt64()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
            Delete.Table("Agents");
            Delete.Table("CpuMetrics");
            Delete.Table("DotNetMetrics");
            Delete.Table("HddMetrics");
            Delete.Table("NetworkMetrics");
            Delete.Table("RamMetrics");
        }
    }
}
