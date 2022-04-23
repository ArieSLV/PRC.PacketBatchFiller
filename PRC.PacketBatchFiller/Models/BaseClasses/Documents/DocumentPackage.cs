using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.Documents
{
    [Table("DocumentPackages")]
    public class DocumentPackage : ModelBase
    {
        #region DocumentPackageId property

        public long DocumentPackageId
        {
            get { return GetValue<long>(DocumentPackageIdProperty); }
            set { SetValue(DocumentPackageIdProperty, value); }
        }

        public static readonly PropertyData DocumentPackageIdProperty = RegisterProperty("DocumentPackageId", typeof (long));

        #endregion

        #region Name property

        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof (string));

        #endregion

        #region DocumentsCollection property

        public ObservableCollection<Document> DocumentsCollection
        {
            get { return GetValue<ObservableCollection<Document>>(DocumensCollectionProperty); }
            set { SetValue(DocumensCollectionProperty, value); }
        }

        public static readonly PropertyData DocumensCollectionProperty = RegisterProperty("DocumentsCollection", typeof(ObservableCollection<Document>));

        #endregion

        #region TimeStamp property

        public DateTime? TimeStamp
        {
            get { return GetValue<DateTime?>(TimeStampProperty); }
            set { SetValue(TimeStampProperty, value); }
        }

        public static readonly PropertyData TimeStampProperty = RegisterProperty("TimeStamp", typeof (DateTime?));

        #endregion
    }
}