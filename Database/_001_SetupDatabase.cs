using FluentMigrator;
using colonist_extension.Models;

namespace colonist_extension.Database
{
    [Migration(1)]
    public class _001_SetupDatabase : Migration
    {
        public override void Up()
        {
            Create.Table("event")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("JSON").AsCustom("TEXT").NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("game")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("EventId").AsInt64().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("user")
                .WithColumn("Id").AsString(25).PrimaryKey()
                .WithColumn("Username").AsString(200).NotNullable()
                .WithColumn("IsLoggedIn").AsBoolean().NotNullable();

            Create.Table("game_user")
                .WithColumn("GameId").AsInt64().PrimaryKey()
                .WithColumn("UserId").AsString(25).PrimaryKey();
        }
         
        public override void Down()
        {
            Delete.Table("event");
            Delete.Table("game");
            Delete.Table("user");
            Delete.Table("game_user");
        }
    }
}