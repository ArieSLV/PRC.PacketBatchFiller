using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.FormOfIncorporationEntity
{
    public class FormOfIncorporationEditWindowModel : ViewModelBase
    {
        public FormOfIncorporationEditWindowModel(Models.LegalEntityEntity.FormOfIncorporation formOfIncorporation)
        {
            FormOfIncorporationModel = formOfIncorporation ?? new Models.LegalEntityEntity.FormOfIncorporation();
        }

        #region ShortForm property

        [ViewModelToModel("FormOfIncorporationModel")]
        public string ShortForm
        {
            get { return GetValue<string>(ShortFormProperty); }
            set { SetValue(ShortFormProperty, value); }
        }

        public static readonly PropertyData ShortFormProperty = RegisterProperty("ShortForm", typeof (string));

        #endregion

        #region FullForm property

        [ViewModelToModel("FormOfIncorporationModel")]
        public string FullForm
        {
            get { return GetValue<string>(FullFormProperty); }
            set { SetValue(FullFormProperty, value); }
        }

        public static readonly PropertyData FullFormProperty = RegisterProperty("FullForm", typeof (string));

        #endregion
        
       
        #region FormOfIncorporationModel model property

        [Model]
        public Models.LegalEntityEntity.FormOfIncorporation FormOfIncorporationModel
        {
            get { return GetValue<Models.LegalEntityEntity.FormOfIncorporation>(FormOfIncorporationModelProperty); }
            private set { SetValue(FormOfIncorporationModelProperty, value); }
        }

        public static readonly PropertyData FormOfIncorporationModelProperty = RegisterProperty("FormOfIncorporationModel", typeof (Models.LegalEntityEntity.FormOfIncorporation));

        #endregion

        public override string Title => "Организационно-правовая форма";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}