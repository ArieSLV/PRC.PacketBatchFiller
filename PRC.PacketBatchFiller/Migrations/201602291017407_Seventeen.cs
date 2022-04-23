namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventeen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LegalEntityName", "FormOfIncorporation_FormOfIncorporationId", "dbo.FormOfIncorporation");
            DropForeignKey("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId", "dbo.LegalEntityName");
            DropIndex("dbo.LegalEntityName", new[] { "FormOfIncorporation_FormOfIncorporationId" });
            DropIndex("dbo.LegalEntity", new[] { "LegalEntityName_LegalEntityNameId" });
            AddColumn("dbo.LegalEntity", "FormOfIncorporation_FormOfIncorporationId", c => c.Long());
            AddColumn("dbo.LegalEntity", "ShortName", c => c.String());
            CreateIndex("dbo.LegalEntity", "FormOfIncorporation_FormOfIncorporationId");
            AddForeignKey("dbo.LegalEntity", "FormOfIncorporation_FormOfIncorporationId", "dbo.FormOfIncorporation", "FormOfIncorporationId");
            DropColumn("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId");
            DropTable("dbo.LegalEntityName");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LegalEntityName",
                c => new
                    {
                        LegalEntityNameId = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        ShortName = c.String(),
                        FullSubname = c.String(),
                        ShortSubname = c.String(),
                        NameFormationMethod = c.Int(nullable: false),
                        FormOfIncorporation_FormOfIncorporationId = c.Long(),
                    })
                .PrimaryKey(t => t.LegalEntityNameId);
            
            AddColumn("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId", c => c.Long());
            DropForeignKey("dbo.LegalEntity", "FormOfIncorporation_FormOfIncorporationId", "dbo.FormOfIncorporation");
            DropIndex("dbo.LegalEntity", new[] { "FormOfIncorporation_FormOfIncorporationId" });
            DropColumn("dbo.LegalEntity", "ShortName");
            DropColumn("dbo.LegalEntity", "FormOfIncorporation_FormOfIncorporationId");
            CreateIndex("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId");
            CreateIndex("dbo.LegalEntityName", "FormOfIncorporation_FormOfIncorporationId");
            AddForeignKey("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId", "dbo.LegalEntityName", "LegalEntityNameId");
            AddForeignKey("dbo.LegalEntityName", "FormOfIncorporation_FormOfIncorporationId", "dbo.FormOfIncorporation", "FormOfIncorporationId");
        }
    }
}
