using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;

namespace PRC.PacketBatchFiller.ViewModels.BaseClasses
{
    public class DocumentViewModelBase : ViewModelBase
    {
        public DocumentViewModelBase()
        {
            RemoveDocumentCommand = new Command(RemoveDocument);
            EditShareholderDocumentCommand = new Command(EditShareholderDocumentCommandExecute);
        }

        #region FormName property

        public string FormName
        {
            get { return GetValue<string>(FormNameProperty); }
            set { SetValue(FormNameProperty, value); }
        }

        public static readonly PropertyData FormNameProperty = RegisterProperty("FormName", typeof(string));

        #endregion


        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof(bool));

        #endregion

        #region RemoveDocument command

        public Command RemoveDocumentCommand { get; set; }

        private void RemoveDocument()
        {
            EntryNeedDelete = !EntryNeedDelete;
        }

        #endregion

        #region EditShareholderDocument command

        public Command EditShareholderDocumentCommand { get; set; }

        protected virtual void EditShareholderDocumentCommandExecute() {}

        #endregion

        #region AuthorizedDocumentsCollection property
        
        public ObservableCollection<AuthorizesDocument> AuthorizedDocumentsCollection
        {
            get { return GetValue<ObservableCollection<AuthorizesDocument>>(AuthorizedDocumentsCollectionProperty); }
            set { SetValue(AuthorizedDocumentsCollectionProperty, value); }
        }

        public static readonly PropertyData AuthorizedDocumentsCollectionProperty = RegisterProperty("AuthorizedDocumentsCollection", typeof (ObservableCollection<AuthorizesDocument>));

        #endregion
       
    }



}