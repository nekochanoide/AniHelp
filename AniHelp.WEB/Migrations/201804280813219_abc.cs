namespace AniHelp.WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbAnimalDatas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Filled = c.DateTime(nullable: false),
                    Name = c.String(),
                    SeizurePlace = c.String(),
                    Collar = c.String(),
                    Pregnancy = c.Boolean(nullable: false),
                    CrueltySigns = c.Boolean(nullable: false),
                    EuthanasiaCause = c.String(),
                    Action = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.DbHealthTroubles", "DbAnimalData_Id", "dbo.DbAnimalDatas");
            DropIndex("dbo.DbHealthTroubles", new[] { "DbAnimalData_Id" });
            DropTable("dbo.DbAnimalDatas");
            DropTable("dbo.DbHealthTroubles");
        }
    }

}
