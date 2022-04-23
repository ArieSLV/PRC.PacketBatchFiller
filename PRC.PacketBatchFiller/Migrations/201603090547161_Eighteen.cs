namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eighteen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Units", "AccountType_AccountTypeId", "dbo.AccountType");
            DropForeignKey("dbo.Units", "AuthorizedRepresentative_UnitId", "dbo.AuthorizedRepresentative");
            DropForeignKey("dbo.AuthorizedRepresentative", "UnitId", "dbo.Person");
            DropIndex("dbo.Units", new[] { "AccountType_AccountTypeId" });
            DropIndex("dbo.Units", new[] { "AuthorizedRepresentative_UnitId" });
            DropIndex("dbo.AuthorizedRepresentative", new[] { "UnitId" });
            DropColumn("dbo.Units", "AccountNumber");
            DropColumn("dbo.Units", "AccountType_AccountTypeId");
            DropColumn("dbo.Units", "AuthorizedRepresentative_UnitId");
            DropTable("dbo.AccountType");
            DropTable("dbo.AuthorizedRepresentative");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AuthorizedRepresentative",
                c => new
                    {
                        UnitId = c.Long(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        AccountTypeId = c.Long(nullable: false, identity: true),
                        AccountTypeValue = c.String(),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            AddColumn("dbo.Units", "AuthorizedRepresentative_UnitId", c => c.Long());
            AddColumn("dbo.Units", "AccountType_AccountTypeId", c => c.Long());
            AddColumn("dbo.Units", "AccountNumber", c => c.String());
            CreateIndex("dbo.AuthorizedRepresentative", "UnitId");
            CreateIndex("dbo.Units", "AuthorizedRepresentative_UnitId");
            CreateIndex("dbo.Units", "AccountType_AccountTypeId");
            AddForeignKey("dbo.AuthorizedRepresentative", "UnitId", "dbo.Person", "UnitId");
            AddForeignKey("dbo.Units", "AuthorizedRepresentative_UnitId", "dbo.AuthorizedRepresentative", "UnitId");
            AddForeignKey("dbo.Units", "AccountType_AccountTypeId", "dbo.AccountType", "AccountTypeId");
        }
    }
}
