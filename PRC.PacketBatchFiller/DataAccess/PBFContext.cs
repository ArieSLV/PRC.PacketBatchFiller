using System;
using System.Data.Entity;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;

namespace PRC.PacketBatchFiller
{
    public class PBFContext : DbContext
    {
        // Контекст настроен для использования строки подключения "PBFContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "PRC.PacketBatchFiller.PBFContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "PBFContext" 
        // в файле конфигурации приложения.
        public PBFContext()
            : base("name=PBFContext")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<BankDetails> BankDetailses { get; set; }
        public virtual DbSet<Citizenship> Citizenships { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<CardID> CardIDs { get; set; }
        public virtual DbSet<CardIDIssuer> CardIDIssuers { get; set; }
        public virtual DbSet<CardIDType> CardIDTypes { get; set; }
        public virtual DbSet<PlaceOfBirth> PlaceOfBirths { get; set; }

        public virtual DbSet<LegalEntity> LegalEntities { get; set; }
        public virtual DbSet<RegistrationCertificate> RegistrationCertificates { get; set; }
        public virtual DbSet<RegistrationCertificateIssuer> RegistrationCertificateIssuers { get; set; }
        public virtual DbSet<FormOfIncorporation> FormsOfIncorporation { get; set; }

        public virtual DbSet<ShareholderAccount> ShareholderAccounts { get; set; }
        public virtual DbSet<IssueOfSecurities> IssuesOfSecurities { get; set; }

        public virtual DbSet<DocumentPackage> DocumentPackages { get; set; }
        public virtual DbSet<ShareholderDocumentPackage> ShareholderDocumentPackages { get; set; }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<AuthorizesDocument> AuthorizesDocuments { get; set; }
        public virtual DbSet<ShareholderAuthorizesDocument> ShareholderAuthorizesDocuments { get; set; }

        public virtual DbSet<AuthorizesDocumentType> AuthorizesDocumentTypes { get; set; }
        public virtual DbSet<ShareholderQuestionary> ShareholderQuestionaries { get; set; }

        public virtual DbSet<ShareholderTransferOrder> ShareholderTransferOrders { get; set; }
        public virtual DbSet<TransferReasonType> TransferReasonTypes { get; set; }
        public virtual DbSet<TransferReasonDocument> TransferReasonDocuments { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(one => one.HasColumnType("datetime2"));
            modelBuilder.Properties<DateTime?>().Configure(one => one.HasColumnType("datetime2"));

            modelBuilder.Entity<Unit>().IgnoreCatelProperties();
            modelBuilder.Entity<Address>().IgnoreCatelProperties();
            modelBuilder.Entity<BankDetails>().IgnoreCatelProperties();
            modelBuilder.Entity<Citizenship>().IgnoreCatelProperties();
            modelBuilder.Entity<Email>().IgnoreCatelProperties();
            modelBuilder.Entity<PhoneNumber>().IgnoreCatelProperties();

            modelBuilder.Entity<CardID>().IgnoreCatelProperties();
            modelBuilder.Entity<CardIDIssuer>().IgnoreCatelProperties();
            modelBuilder.Entity<CardIDType>().IgnoreCatelProperties();
            modelBuilder.Entity<PlaceOfBirth>().IgnoreCatelProperties();

            modelBuilder.Entity<RegistrationCertificate>().IgnoreCatelProperties();
            modelBuilder.Entity<RegistrationCertificateIssuer>().IgnoreCatelProperties();
            modelBuilder.Entity<FormOfIncorporation>().IgnoreCatelProperties();

            modelBuilder.Entity<ShareholderAccount>().IgnoreCatelProperties();
            modelBuilder.Entity<IssueOfSecurities>().IgnoreCatelProperties();

            modelBuilder.Entity<Document>().IgnoreCatelProperties();
            modelBuilder.Entity<AuthorizesDocumentType>().IgnoreCatelProperties();
            modelBuilder.Entity<DocumentPackage>().IgnoreCatelProperties();

            modelBuilder.Entity<TransferReasonDocument>().IgnoreCatelProperties();
            modelBuilder.Entity<TransferReasonType>().IgnoreCatelProperties();

            //modelBuilder.Entity<Unit>().HasMany(o => o.AuthorizedDocumentsCollection).WithMany(o => o.AuthorizedUnits);
        }
    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}