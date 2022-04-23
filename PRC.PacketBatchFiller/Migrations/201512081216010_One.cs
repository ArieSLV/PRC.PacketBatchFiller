namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        AccountTypeId = c.Long(nullable: false, identity: true),
                        AccountTypeValue = c.String(),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Long(nullable: false, identity: true),
                        Index = c.String(),
                        RegionType = c.String(),
                        RegionName = c.String(),
                        DistrictType = c.String(),
                        DistrictName = c.String(),
                        CityType = c.String(),
                        CityName = c.String(),
                        LocalityType = c.String(),
                        LocalityName = c.String(),
                        StreetType = c.String(),
                        StreetName = c.String(),
                        BuildingType = c.String(),
                        BuildingValue = c.String(),
                        SubBuildingType = c.String(),
                        SubBuildingValue = c.String(),
                        FlatType = c.String(),
                        FlatValue = c.String(),
                        AddressInOneString = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        AccountNumber = c.String(),
                        DividentsPaymentWay = c.Int(nullable: false),
                        OnlyPersonalPresenceFlag = c.Boolean(nullable: false),
                        INN = c.String(),
                        MailingAddressEqualRegistrationAddressFlag = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(precision: 7, storeType: "datetime2"),
                        AccountType_AccountTypeId = c.Long(),
                        AddressRegistration_AddressId = c.Long(),
                        BankDetails_BankDetailsId = c.Long(),
                        Citizenship_CitizenshipId = c.Long(),
                        MailingAddress_AddressId = c.Long(),
                        AuthorizedRepresentative_UnitId = c.Long(),
                    })
                .PrimaryKey(t => t.UnitId)
                .ForeignKey("dbo.AccountType", t => t.AccountType_AccountTypeId)
                .ForeignKey("dbo.Address", t => t.AddressRegistration_AddressId)
                .ForeignKey("dbo.BankDetails", t => t.BankDetails_BankDetailsId)
                .ForeignKey("dbo.Citizenship", t => t.Citizenship_CitizenshipId)
                .ForeignKey("dbo.Address", t => t.MailingAddress_AddressId)
                .ForeignKey("dbo.AuthorizedRepresentative", t => t.AuthorizedRepresentative_UnitId)
                .Index(t => t.AccountType_AccountTypeId)
                .Index(t => t.AddressRegistration_AddressId)
                .Index(t => t.BankDetails_BankDetailsId)
                .Index(t => t.Citizenship_CitizenshipId)
                .Index(t => t.MailingAddress_AddressId)
                .Index(t => t.AuthorizedRepresentative_UnitId);
            
            CreateTable(
                "dbo.BankDetails",
                c => new
                    {
                        BankDetailsId = c.Long(nullable: false, identity: true),
                        PersonalAccount = c.String(),
                        BankBranchName = c.String(),
                        MainAccount = c.String(),
                        CorrAccount = c.String(),
                        BankName = c.String(),
                        BankINN = c.String(),
                        BIK = c.String(),
                        BankCity = c.String(),
                    })
                .PrimaryKey(t => t.BankDetailsId);
            
            CreateTable(
                "dbo.CardID",
                c => new
                    {
                        CardIDId = c.Long(nullable: false, identity: true),
                        Series = c.String(),
                        Number = c.String(),
                        IssueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CardIDIssuer_CardIDIssuerId = c.Long(),
                        CardIDType_CardIDTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.CardIDId)
                .ForeignKey("dbo.CardIDIssuer", t => t.CardIDIssuer_CardIDIssuerId)
                .ForeignKey("dbo.CardIDType", t => t.CardIDType_CardIDTypeId)
                .Index(t => t.CardIDIssuer_CardIDIssuerId)
                .Index(t => t.CardIDType_CardIDTypeId);
            
            CreateTable(
                "dbo.CardIDIssuer",
                c => new
                    {
                        CardIDIssuerId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.CardIDIssuerId);
            
            CreateTable(
                "dbo.CardIDType",
                c => new
                    {
                        CardIDTypeId = c.Long(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.CardIDTypeId);
            
            CreateTable(
                "dbo.Citizenship",
                c => new
                    {
                        CitizenshipId = c.Long(nullable: false, identity: true),
                        CitizenshipValue = c.String(),
                    })
                .PrimaryKey(t => t.CitizenshipId);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        EmailId = c.Long(nullable: false, identity: true),
                        EmailValue = c.String(),
                        EmailComment = c.String(),
                        Unit_UnitId = c.Long(),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .Index(t => t.Unit_UnitId);
            
            CreateTable(
                "dbo.PhoneNumber",
                c => new
                    {
                        PhoneNumberId = c.Long(nullable: false, identity: true),
                        PhoneNumberType = c.Int(nullable: false),
                        PhoneNumberValue = c.String(),
                        Unit_UnitId = c.Long(),
                    })
                .PrimaryKey(t => t.PhoneNumberId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .Index(t => t.Unit_UnitId);
            
            CreateTable(
                "dbo.PlaceOfBirth",
                c => new
                    {
                        PlaceOfBirthId = c.Long(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.PlaceOfBirthId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        UnitId = c.Long(nullable: false),
                        CardID_CardIDId = c.Long(),
                        PlaceOfBirth_PlaceOfBirthId = c.Long(),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsOneOfPODFTFlag = c.Boolean(nullable: false),
                        GotBeneficialOwnerFlag = c.Boolean(nullable: false),
                        IsHeadNonComercialCompanyFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UnitId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.CardID", t => t.CardID_CardIDId)
                .ForeignKey("dbo.PlaceOfBirth", t => t.PlaceOfBirth_PlaceOfBirthId)
                .Index(t => t.UnitId)
                .Index(t => t.CardID_CardIDId)
                .Index(t => t.PlaceOfBirth_PlaceOfBirthId);
            
            CreateTable(
                "dbo.AuthorizedRepresentative",
                c => new
                    {
                        UnitId = c.Long(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UnitId)
                .ForeignKey("dbo.Person", t => t.UnitId)
                .Index(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorizedRepresentative", "UnitId", "dbo.Person");
            DropForeignKey("dbo.Person", "PlaceOfBirth_PlaceOfBirthId", "dbo.PlaceOfBirth");
            DropForeignKey("dbo.Person", "CardID_CardIDId", "dbo.CardID");
            DropForeignKey("dbo.Person", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "AuthorizedRepresentative_UnitId", "dbo.AuthorizedRepresentative");
            DropForeignKey("dbo.PhoneNumber", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "MailingAddress_AddressId", "dbo.Address");
            DropForeignKey("dbo.Email", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "Citizenship_CitizenshipId", "dbo.Citizenship");
            DropForeignKey("dbo.Units", "BankDetails_BankDetailsId", "dbo.BankDetails");
            DropForeignKey("dbo.Units", "AddressRegistration_AddressId", "dbo.Address");
            DropForeignKey("dbo.Units", "AccountType_AccountTypeId", "dbo.AccountType");
            DropForeignKey("dbo.CardID", "CardIDType_CardIDTypeId", "dbo.CardIDType");
            DropForeignKey("dbo.CardID", "CardIDIssuer_CardIDIssuerId", "dbo.CardIDIssuer");
            DropIndex("dbo.AuthorizedRepresentative", new[] { "UnitId" });
            DropIndex("dbo.Person", new[] { "PlaceOfBirth_PlaceOfBirthId" });
            DropIndex("dbo.Person", new[] { "CardID_CardIDId" });
            DropIndex("dbo.Person", new[] { "UnitId" });
            DropIndex("dbo.PhoneNumber", new[] { "Unit_UnitId" });
            DropIndex("dbo.Email", new[] { "Unit_UnitId" });
            DropIndex("dbo.CardID", new[] { "CardIDType_CardIDTypeId" });
            DropIndex("dbo.CardID", new[] { "CardIDIssuer_CardIDIssuerId" });
            DropIndex("dbo.Units", new[] { "AuthorizedRepresentative_UnitId" });
            DropIndex("dbo.Units", new[] { "MailingAddress_AddressId" });
            DropIndex("dbo.Units", new[] { "Citizenship_CitizenshipId" });
            DropIndex("dbo.Units", new[] { "BankDetails_BankDetailsId" });
            DropIndex("dbo.Units", new[] { "AddressRegistration_AddressId" });
            DropIndex("dbo.Units", new[] { "AccountType_AccountTypeId" });
            DropTable("dbo.AuthorizedRepresentative");
            DropTable("dbo.Person");
            DropTable("dbo.PlaceOfBirth");
            DropTable("dbo.PhoneNumber");
            DropTable("dbo.Email");
            DropTable("dbo.Citizenship");
            DropTable("dbo.CardIDType");
            DropTable("dbo.CardIDIssuer");
            DropTable("dbo.CardID");
            DropTable("dbo.BankDetails");
            DropTable("dbo.Units");
            DropTable("dbo.Address");
            DropTable("dbo.AccountType");
        }
    }
}
