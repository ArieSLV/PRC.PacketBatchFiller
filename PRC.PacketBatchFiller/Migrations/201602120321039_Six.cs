namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Six : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LegalEntityName",
                c => new
                    {
                        LegalEntityNameId = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        ShortName = c.String(),
                        NameFormationMethod = c.Int(nullable: false),
                        FormOfIncorporation_FormOfIncorporationId = c.Long(),
                    })
                .PrimaryKey(t => t.LegalEntityNameId)
                .ForeignKey("dbo.FormOfIncorporation", t => t.FormOfIncorporation_FormOfIncorporationId)
                .Index(t => t.FormOfIncorporation_FormOfIncorporationId);
            
            CreateTable(
                "dbo.FormOfIncorporation",
                c => new
                    {
                        FormOfIncorporationId = c.Long(nullable: false, identity: true),
                        ShortForm = c.String(),
                        FullForm = c.String(),
                    })
                .PrimaryKey(t => t.FormOfIncorporationId);
            
            AddColumn("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId", c => c.Long());
            CreateIndex("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId");
            AddForeignKey("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId", "dbo.LegalEntityName", "LegalEntityNameId");
            DropColumn("dbo.LegalEntity", "ShortName");
            DropColumn("dbo.RegistrationCertificate", "RegistrationCertificateType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegistrationCertificate", "RegistrationCertificateType", c => c.Int(nullable: false));
            AddColumn("dbo.LegalEntity", "ShortName", c => c.String());
            DropForeignKey("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId", "dbo.LegalEntityName");
            DropForeignKey("dbo.LegalEntityName", "FormOfIncorporation_FormOfIncorporationId", "dbo.FormOfIncorporation");
            DropIndex("dbo.LegalEntity", new[] { "LegalEntityName_LegalEntityNameId" });
            DropIndex("dbo.LegalEntityName", new[] { "FormOfIncorporation_FormOfIncorporationId" });
            DropColumn("dbo.LegalEntity", "LegalEntityName_LegalEntityNameId");
            DropTable("dbo.FormOfIncorporation");
            DropTable("dbo.LegalEntityName");
        }
    }
}
