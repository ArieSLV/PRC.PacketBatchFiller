using System;
using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.Documents;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity
{
    [InterestedIn(typeof(TransferReasonTypeSearchViewModel))]
    [InterestedIn(typeof(TransferReasonDocumentViewModel))]
    public class TransferReasonDocumentsViewModel : ViewModelBase
    {
        
        public TransferReasonDocumentsViewModel(ObservableCollection<TransferReasonDocument> transferReasonDocuments, IUIVisualizerService uiVisualizerService)
        {
        
            TransferReasonDocumentsCollection = transferReasonDocuments ?? new ObservableCollection<TransferReasonDocument>();
            TransferReasonDocumentViewModelCollection = new ObservableCollection<TransferReasonDocumentViewModel>();

            foreach (var transferReasonDocument in TransferReasonDocumentsCollection)
            {
                TransferReasonDocumentViewModelCollection.Add(new TransferReasonDocumentViewModel(transferReasonDocument));
            }

            TransferReasonTypeSearchViewModel = new TransferReasonTypeSearchViewModel(AddedTransferReasonType, uiVisualizerService, null);

            AddTransferReasonDocumentCommand = new Command(AddTransferReasonDocumentExecute);
        }

        #region AddedTransferReasonType property

        public TransferReasonType AddedTransferReasonType
        {
            get { return GetValue<TransferReasonType>(AddedTransferReasonTypeProperty); }
            set { SetValue(AddedTransferReasonTypeProperty, value); }
        }

        public static readonly PropertyData AddedTransferReasonTypeProperty = RegisterProperty("AddedTransferReasonType", typeof (TransferReasonType));

        #endregion

        #region AddedNumber property

        public string AddedNumber
        {
            get { return GetValue<string>(AddedNumberProperty); }
            set { SetValue(AddedNumberProperty, value); }
        }

        public static readonly PropertyData AddedNumberProperty = RegisterProperty("AddedNumber", typeof (string));

        #endregion

        #region AddedDate property

        public DateTime? AddedDate
        {
            get { return GetValue<DateTime?>(AddedDateProperty); }
            set { SetValue(AddedDateProperty, value); }
        }

        public static readonly PropertyData AddedDateProperty = RegisterProperty("AddedDate", typeof (DateTime?));

        #endregion



        #region TransferReasonDocumentsCollection property

        public ObservableCollection<TransferReasonDocument> TransferReasonDocumentsCollection
        {
            get { return GetValue<ObservableCollection<TransferReasonDocument>>(TransferReasonDocumentsCollectionProperty); }
            set { SetValue(TransferReasonDocumentsCollectionProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentsCollectionProperty = RegisterProperty("TransferReasonDocumentsCollection", typeof (ObservableCollection<TransferReasonDocument>));

        #endregion

        #region TransferReasonDocumentViewModelCollection property

        public ObservableCollection<TransferReasonDocumentViewModel> TransferReasonDocumentViewModelCollection
        {
            get { return GetValue<ObservableCollection<TransferReasonDocumentViewModel>>(TransferReasonDocumentViewModelCollectionProperty); }
            set { SetValue(TransferReasonDocumentViewModelCollectionProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentViewModelCollectionProperty = RegisterProperty("TransferReasonDocumentViewModelCollection", typeof (ObservableCollection<TransferReasonDocumentViewModel>));

        #endregion



        #region TransferReasonTypeSearchViewModel property

        public IViewModel TransferReasonTypeSearchViewModel
        {
            get { return GetValue<IViewModel>(TransferReasonTypeSearchViewModelProperty); }
            set { SetValue(TransferReasonTypeSearchViewModelProperty, value); }
        }

        public static readonly PropertyData TransferReasonTypeSearchViewModelProperty = RegisterProperty("TransferReasonTypeSearchViewModel", typeof (IViewModel));

        #endregion

        #region AddTransferReasonDocument command

        public Command AddTransferReasonDocumentCommand { get; private set; }

        private void AddTransferReasonDocumentExecute()
        {
            if (string.IsNullOrEmpty(AddedNumber)) AddedNumber = "б/н";

            var trd = new TransferReasonDocument
            {
                Date = AddedDate,
                Number = AddedNumber,
                TransferReasonType = AddedTransferReasonType
            };

            var trdvm = new TransferReasonDocumentViewModel(trd);
            TransferReasonDocumentsCollection.Add(trd);
            TransferReasonDocumentViewModelCollection.Add(trdvm);

            AddedNumber = string.Empty;
            AddedDate = null;
            ((TransferReasonTypeSearchViewModel) TransferReasonTypeSearchViewModel).RemoveSelectedItemCommand.Execute();
        }

        #endregion

        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {

            if (propertyName == "TargetEntity" && viewModel is TransferReasonTypeSearchViewModel)
            {
                var vm = (TransferReasonTypeSearchViewModel)viewModel;
                AddedTransferReasonType = vm.TargetEntity;
            }
            else if (propertyName == "EntryNeedDelete" && viewModel is TransferReasonDocumentViewModel)
            {
                var vm = (TransferReasonDocumentViewModel)viewModel;

                TransferReasonDocumentsCollection.Remove(vm.TransferReasonDocumentModel);
                TransferReasonDocumentViewModelCollection.Remove(vm);
            }


        }
        #endregion
    }
}