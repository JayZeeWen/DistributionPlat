namespace Distribution.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Distribution.Model;

    public partial class DistributionContext : DbContext
    {
        public DistributionContext()
            : base("name=NFineDbContext")
        {
        }

        public virtual DbSet<Agent> t_agent { get; set; }
        public virtual DbSet<AgentRelation> t_agent_relation { get; set; }
        public virtual DbSet<CommConfig> t_common_config { get; set; }
        public virtual DbSet<ScoreDetail> t_score_detail { get; set; }

        public virtual DbSet<LevelConfig> t_level_config { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
