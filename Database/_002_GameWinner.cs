using FluentMigrator;
using colonist_extension.Models;

namespace colonist_extension.Database
{
    [Migration(2)]
    public class _002_SetupDatabase : Migration
    {
        public override void Up()
        {
            Alter.Table("game_user")
                .AddColumn("Winner").AsBoolean().WithDefaultValue(false).NotNullable();
        }

        public override void Down()
        {
        }
    }
}