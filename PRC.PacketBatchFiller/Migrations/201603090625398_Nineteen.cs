namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nineteen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Address", newName: "Addresses");
            RenameTable(name: "dbo.CardIDIssuer", newName: "CardIDIssuers");
            RenameTable(name: "dbo.CardIDType", newName: "CardIDTypes");
            RenameTable(name: "dbo.Citizenship", newName: "Citizenships");
            RenameTable(name: "dbo.Email", newName: "Emails");
            RenameTable(name: "dbo.FormOfIncorporation", newName: "FormOfIncorporations");
            RenameTable(name: "dbo.LegalEntity", newName: "LegalEntities");
            RenameTable(name: "dbo.Person", newName: "Persons");
            RenameTable(name: "dbo.PhoneNumber", newName: "PhoneNumbers");
            RenameTable(name: "dbo.PlaceOfBirth", newName: "PlaceOfBirths");
            RenameTable(name: "dbo.RegistrationCertificate", newName: "RegistrationCertificates");
            RenameTable(name: "dbo.RegistrationCertificateIssuer", newName: "RegistrationCertificateIssuers");
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DocumentId);
            
            CreateTable(
                "dbo.AuthorizesDocumentTypes",
                c => new
                    {
                        AuthorizesDocumentTypeId = c.Long(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.AuthorizesDocumentTypeId);
            
            CreateTable(
                "dbo.AuthorizesDocuments",
                c => new
                    {
                        DocumentId = c.Long(nullable: false),
                        AuthorizesDocumentType_AuthorizesDocumentTypeId = c.Long(),
                        WhoGivingAuthority_UnitId = c.Long(),
                        Number = c.String(),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.AuthorizesDocumentTypes", t => t.AuthorizesDocumentType_AuthorizesDocumentTypeId)
                .ForeignKey("dbo.Units", t => t.WhoGivingAuthority_UnitId)
                .Index(t => t.DocumentId)
                .Index(t => t.AuthorizesDocumentType_AuthorizesDocumentTypeId)
                .Index(t => t.WhoGivingAuthority_UnitId);
            
            AddColumn("dbo.Units", "AuthorizesDocument_DocumentId", c => c.Long());
            CreateIndex("dbo.Units", "AuthorizesDocument_DocumentId");
            AddForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments", "DocumentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorizesDocuments", "WhoGivingAuthority_UnitId", "dbo.Units");
            DropForeignKey("dbo.AuthorizesDocuments", "AuthorizesDocumentType_AuthorizesDocumentTypeId", "dbo.AuthorizesDocumentTypes");
            DropForeignKey("dbo.AuthorizesDocuments", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropIndex("dbo.AuthorizesDocuments", new[] { "WhoGivingAuthority_UnitId" });
            DropIndex("dbo.AuthorizesDocuments", new[] { "AuthorizesDocumentType_AuthorizesDocumentTypeId" });
            DropIndex("dbo.AuthorizesDocuments", new[] { "DocumentId" });
            DropIndex("dbo.Units", new[] { "AuthorizesDocument_DocumentId" });
            DropColumn("dbo.Units", "AuthorizesDocument_DocumentId");
            DropTable("dbo.AuthorizesDocuments");
            DropTable("dbo.AuthorizesDocumentTypes");
            DropTable("dbo.Documents");
            RenameTable(name: "dbo.RegistrationCertificateIssuers", newName: "RegistrationCertificateIssuer");
            RenameTable(name: "dbo.RegistrationCertificates", newName: "RegistrationCertificate");
            RenameTable(name: "dbo.PlaceOfBirths", newName: "PlaceOfBirth");
            RenameTable(name: "dbo.PhoneNumbers", newName: "PhoneNumber");
            RenameTable(name: "dbo.Persons", newName: "Person");
            RenameTable(name: "dbo.LegalEntities", newName: "LegalEntity");
            RenameTable(name: "dbo.FormOfIncorporations", newName: "FormOfIncorporation");
            RenameTable(name: "dbo.Emails", newName: "Email");
            RenameTable(name: "dbo.Citizenships", newName: "Citizenship");
            RenameTable(name: "dbo.CardIDTypes", newName: "CardIDType");
            RenameTable(name: "dbo.CardIDIssuers", newName: "CardIDIssuer");
            RenameTable(name: "dbo.Addresses", newName: "Address");
        }
    }
}
