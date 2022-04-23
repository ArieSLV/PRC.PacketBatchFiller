using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;

namespace PRC.PacketBatchFiller.ViewModels.Documents
{
    public class AuthorizesDocumentTypeEditWindowModel : ViewModelBase
    {
        public AuthorizesDocumentTypeEditWindowModel(AuthorizesDocumentType authorizesDocumentType)
        {
            AuthorizesDocumentTypeModel = authorizesDocumentType ?? new AuthorizesDocumentType();
        }

        #region Value property

        [ViewModelToModel("AuthorizesDocumentTypeModel")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof(string));

        #endregion


        #region AuthorizesDocumentTypeModel model property

        [Model]
        public AuthorizesDocumentType AuthorizesDocumentTypeModel
        {
            get { return GetValue<AuthorizesDocumentType>(AuthorizesDocumentTypeModelProperty); }
            private set { SetValue(AuthorizesDocumentTypeModelProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeModelProperty = RegisterProperty("AuthorizesDocumentTypeModel", typeof (AuthorizesDocumentType));

        #endregion

        public override string Title => "Тип документа";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}