namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Four : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistrationCertificate",
                c => new
                    {
                        RegistrationCertificateId = c.Long(nullable: false, identity: true),
                        RegistrationCertificateType = c.Int(nullable: false),
                        Number = c.String(),
                        IssueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDirty = c.Boolean(nullable: false),
                        IsReadOnly = c.Boolean(nullable: false),
                        RegistrationCertificateIssuer_RegistrationCertificateIssuerId = c.Long(),
                    })
                .PrimaryKey(t => t.RegistrationCertificateId)
                .ForeignKey("dbo.RegistrationCertificateIssuer", t => t.RegistrationCertificateIssuer_RegistrationCertificateIssuerId)
                .Index(t => t.RegistrationCertificateIssuer_RegistrationCertificateIssuerId);
            
            CreateTable(
                "dbo.RegistrationCertificateIssuer",
                c => new
                    {
                        RegistrationCertificateIssuerId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDirty = c.Boolean(nullable: false),
                        IsReadOnly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegistrationCertificateIssuerId);
            
            CreateTable(
                "dbo.LegalEntity",
                c => new
                    {
                        UnitId = c.Long(nullable: false),
                        RegistrationCertificate_RegistrationCertificateId = c.Long(),
                        ShortName = c.String(),
                        KPP = c.String(),
                    })
                .PrimaryKey(t => t.UnitId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.RegistrationCertificate", t => t.RegistrationCertificate_RegistrationCertificateId)
                .Index(t => t.UnitId)
                .Index(t => t.RegistrationCertificate_RegistrationCertificateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LegalEntity", "RegistrationCertificate_RegistrationCertificateId", "dbo.RegistrationCertificate");
            DropForeignKey("dbo.LegalEntity", "UnitId", "dbo.Units");
            DropForeignKey("dbo.RegistrationCertificate", "RegistrationCertificateIssuer_RegistrationCertificateIssuerId", "dbo.RegistrationCertificateIssuer");
            DropIndex("dbo.LegalEntity", new[] { "RegistrationCertificate_RegistrationCertificateId" });
            DropIndex("dbo.LegalEntity", new[] { "UnitId" });
            DropIndex("dbo.RegistrationCertificate", new[] { "RegistrationCertificateIssuer_RegistrationCertificateIssuerId" });
            DropTable("dbo.LegalEntity");
            DropTable("dbo.RegistrationCertificateIssuer");
            DropTable("dbo.RegistrationCertificate");
        }
    }
}
