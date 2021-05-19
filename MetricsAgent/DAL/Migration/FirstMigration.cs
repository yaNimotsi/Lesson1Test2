using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace MetricsAgent.DAL.Migration
{
    [Migration(1)]
    public class FirstMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("CpuMetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            Create.Table("DotNetMetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            Create.Table("HddMetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            Create.Table("NetworkMetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            Create.Table("RamMetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
        }

        public override void Down()
        {
            Delete.Table("CpuMetrics");
            Delete.Table("DotNetMetrics");
            Delete.Table("HddMetrics");
            Delete.Table("NetworkMetrics");
            Delete.Table("RamMetrics");
        }
    }
}
