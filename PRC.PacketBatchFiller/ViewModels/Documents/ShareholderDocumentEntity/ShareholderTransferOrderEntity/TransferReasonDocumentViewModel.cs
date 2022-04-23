using System;
using System.Drawing.Drawing2D;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity
{
    public class TransferReasonDocumentViewModel : ViewModelBase
    {
        public TransferReasonDocumentViewModel(TransferReasonDocument transferReasonDocument)
        {
            TransferReasonDocumentModel = transferReasonDocument ?? new TransferReasonDocument();

            RemoveTransferReasonDocumentCommand = new Command(RemoveTransferReasonDocument);
        }

        #region TransferReasonType property

        [ViewModelToModel("TransferReasonDocumentModel")]
        public virtual TransferReasonType TransferReasonType
        {
            get { return GetValue<TransferReasonType>(TransferReasonTypeProperty); }
            set { SetValue(TransferReasonTypeProperty, value); }
        }

        public static readonly PropertyData TransferReasonTypeProperty = RegisterProperty("TransferReasonType", typeof(TransferReasonType));

        #endregion

        #region Number property

        [ViewModelToModel("TransferReasonDocumentModel")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof(string));

        #endregion

        #region Date property

        [ViewModelToModel("TransferReasonDocumentModel")]
        public DateTime? Date
        {
            get { return GetValue<DateTime?>(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly PropertyData DateProperty = RegisterProperty("Date", typeof(DateTime?));

        #endregion


        #region TransferReasonDocumentModel model property

        [Model]
        public TransferReasonDocument TransferReasonDocumentModel
        {
            get { return GetValue<TransferReasonDocument>(TransferReasonDocumentModelProperty); }
            private set { SetValue(TransferReasonDocumentModelProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentModelProperty = RegisterProperty("TransferReasonDocumentModel", typeof (TransferReasonDocument));

        #endregion




        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof(bool));

        #endregion

        #region RemoveTransferReasonDocument command

        public Command RemoveTransferReasonDocumentCommand { get; set; }

        private void RemoveTransferReasonDocument()
        {
            EntryNeedDelete = !EntryNeedDelete;
        }

        #endregion

        
        


    }


}