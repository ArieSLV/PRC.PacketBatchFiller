using System.Threading.Tasks;
using Catel;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;

namespace PRC.PacketBatchFiller.ViewModels.PersonEntity.CardIDIssuer
{
    public class CardIDIssuerEditWindowModel : ViewModelBase
    {
        public CardIDIssuerEditWindowModel(Models.PersonsEntity.CardIDIssuer cardIdIssuer)
        {
            Argument.IsNotNull(() => cardIdIssuer);
            CardIdIssuerModel = cardIdIssuer;
        }

        #region Properties

        public override string Title => "Редактирование наименования органа выдачи паспорта";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }

        #region Name property

        [ViewModelToModel("CardIdIssuerModel")]
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof (string));

        #endregion

        #region Code property

        [ViewModelToModel("CardIdIssuerModel")]
        public string Code
        {
            get { return GetValue<string>(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly PropertyData CodeProperty = RegisterProperty("Code", typeof (string));

        #endregion

        #endregion

        #region Models

        #region CardIdIssuerModel model property

        [Model, ViewToViewModel("CardIdIssuer")]
        public Models.PersonsEntity.CardIDIssuer CardIdIssuerModel
        {
            get { return GetValue<Models.PersonsEntity.CardIDIssuer>(CardIDIssuerModelProperty); }
            private set { SetValue(CardIDIssuerModelProperty, value); }
        }

        public static readonly PropertyData CardIDIssuerModelProperty = RegisterProperty("CardIdIssuerModel", typeof (Models.PersonsEntity.CardIDIssuer));

        #endregion
        
        #endregion
       
    }
}
