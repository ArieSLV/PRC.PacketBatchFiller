using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.Documents;

namespace PRC.PacketBatchFiller.ViewModels.Documents
{
    public class TransferReasonTypeEditWindowModel : ViewModelBase
    {
        public TransferReasonTypeEditWindowModel(TransferReasonType transferReasonType)
        {
            TransferReasonTypeModel = transferReasonType ?? new TransferReasonType();
        }

        #region Value property

        [ViewModelToModel("TransferReasonTypeModel")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof(string));

        #endregion

        #region TransferReasonTypeModel model property

        [Model]
        public TransferReasonType TransferReasonTypeModel
        {
            get { return GetValue<TransferReasonType>(TransferReasonTypeModelProperty); }
            private set { SetValue(TransferReasonTypeModelProperty, value); }
        }

        public static readonly PropertyData TransferReasonTypeModelProperty = RegisterProperty("TransferReasonTypeModel", typeof (TransferReasonType));

        #endregion

        public override string Title => "Тип документа-основания";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}