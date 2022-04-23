using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;
using Catel.Services;
using PRC.PacketBatchFiller.DataAccess.Clients;
using PRC.PacketBatchFiller.DataAccess.Models;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.ViewModels
{
    public class BankDetailsViewModel : ViewModelBase
    {
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly SuggestClient _api = new SuggestClient("cc45a1775877d4e29e1652c226fcb36c76b573e6", "dadata.ru", "https");
        

        public BankDetailsViewModel(BankDetails bankDetails, IPleaseWaitService pleaseWaitService)
        {
            _pleaseWaitService = pleaseWaitService;
            BankDetails = bankDetails ?? new BankDetails();

            SelectedItemToItemCommand = new Command(OnSelectedItemToItemExecute);
        }

        #region BankDetails model & properties

        #region PersonalAccount property

        /// <summary>
        /// Gets or sets the PersonalAccount value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string PersonalAccount
        {
            get { return GetValue<string>(PersonalAccountProperty); }
            set { SetValue(PersonalAccountProperty, value); }
        }

        /// <summary>
        /// PersonalAccount property data.
        /// </summary>
        public static readonly PropertyData PersonalAccountProperty = RegisterProperty("PersonalAccount", typeof (string), null);

        #endregion

        #region BankBranchName property

        /// <summary>
        /// Gets or sets the BankBranchName value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string BankBranchName
        {
            get { return GetValue<string>(BankBranchNameProperty); }
            set { SetValue(BankBranchNameProperty, value); }
        }

        /// <summary>
        /// BankBranchName property data.
        /// </summary>
        public static readonly PropertyData BankBranchNameProperty = RegisterProperty("BankBranchName", typeof (string), null);

        #endregion

        #region MainAccount property

        /// <summary>
        /// Gets or sets the MainAccount value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string MainAccount
        {
            get { return GetValue<string>(MainAccountProperty); }
            set { SetValue(MainAccountProperty, value); }
        }

        /// <summary>
        /// MainAccount property data.
        /// </summary>
        public static readonly PropertyData MainAccountProperty = RegisterProperty("MainAccount", typeof (string), null);

        #endregion

        #region CorrAccount property

        /// <summary>
        /// Gets or sets the CorrAccount value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string CorrAccount
        {
            get { return GetValue<string>(CorrAccountProperty); }
            set { SetValue(CorrAccountProperty, value); }
        }

        /// <summary>
        /// CorrAccount property data.
        /// </summary>
        public static readonly PropertyData CorrAccountProperty = RegisterProperty("CorrAccount", typeof (string), null);

        #endregion

        #region BankName property

        /// <summary>
        /// Gets or sets the BankName value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string BankName
        {
            get { return GetValue<string>(BankNameProperty); }
            set { SetValue(BankNameProperty, value); }
        }

        /// <summary>
        /// BankName property data.
        /// </summary>
        public static readonly PropertyData BankNameProperty = RegisterProperty("BankName", typeof (string), null);

        #endregion

        #region BankINN property

        /// <summary>
        /// Gets or sets the BankINN value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string BankINN
        {
            get { return GetValue<string>(BankINNProperty); }
            set { SetValue(BankINNProperty, value); }
        }

        /// <summary>
        /// BankINN property data.
        /// </summary>
        public static readonly PropertyData BankINNProperty = RegisterProperty("BankINN", typeof (string), null);

        #endregion

        #region BIK property

        /// <summary>
        /// Gets or sets the BIK value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string BIK
        {
            get { return GetValue<string>(BIKProperty); }
            set { SetValue(BIKProperty, value); }
        }

        /// <summary>
        /// BIK property data.
        /// </summary>
        public static readonly PropertyData BIKProperty = RegisterProperty("BIK", typeof (string), null);

        #endregion

        #region BankCity property

        /// <summary>
        /// Gets or sets the BankCity value.
        /// </summary>
        [ViewModelToModel("BankDetails")]
        public string BankCity
        {
            get { return GetValue<string>(BankCityProperty); }
            set { SetValue(BankCityProperty, value); }
        }

        /// <summary>
        /// BankCity property data.
        /// </summary>
        public static readonly PropertyData BankCityProperty = RegisterProperty("BankCity", typeof (string), null);

        #endregion



        #region BankDetails model property

        /// <summary>
        /// Gets or sets the BankDetailsModel value.
        /// </summary>
        [Model, ViewToViewModel("BankDetails")]
        public BankDetails BankDetails
        {
            get { return GetValue<BankDetails>(BankDetailsProperty); }
            private set { SetValue(BankDetailsProperty, value); }
        }

        /// <summary>
        /// BankDetailsModel property data.
        /// </summary>
        public static readonly PropertyData BankDetailsProperty = RegisterProperty("BankDetails", typeof (BankDetails));

        #endregion

        #endregion



        #region UI properties

        #region SearchText property

        public string SearchText
        {
            get { return GetValue<string>(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly PropertyData SearchTextProperty = RegisterProperty("SearchText", typeof (string), null,
            (sender, e) => ((BankDetailsViewModel) sender).OnSearchTextChanged());

        private void OnSearchTextChanged()
        {
            _pleaseWaitService.Show("Поиск банка");
            var query = SearchText;
            var response = _api.QueryBank(query);
            SuggestCollection = response.suggestionss;
            if (SuggestCollection.Count > 0)
            {
                CorrAccount = SuggestCollection[0].data.correspondent_account;
                BankName = SuggestCollection[0].data.name.payment;
                BIK = SuggestCollection[0].data.bic;
            }
            else
            {
                CorrAccount = null;
                BankName = null;
                BIK = null;
            }
            _pleaseWaitService.Hide();
            
        }

        #endregion

        #region SuggestCollection property

        /// <summary>
        /// Gets or sets the SuggestCollection value.
        /// </summary>
        public List<SuggestBankResponse.Suggestions> SuggestCollection
        {
            get { return GetValue<List<SuggestBankResponse.Suggestions>>(SuggestCollectionProperty); }
            set { SetValue(SuggestCollectionProperty, value); }
        }

        /// <summary>
        /// SuggestCollection property data.
        /// </summary>
        public static readonly PropertyData SuggestCollectionProperty = RegisterProperty("SuggestCollection", typeof (List<SuggestBankResponse.Suggestions>));

        #endregion
        #endregion


        #region SelectedItemToItem command

        public Command SelectedItemToItemCommand { get; private set; }

        private void OnSelectedItemToItemExecute()
        {
           if (SuggestCollection != null) SuggestCollection = null;
        }

        #endregion

    }
}