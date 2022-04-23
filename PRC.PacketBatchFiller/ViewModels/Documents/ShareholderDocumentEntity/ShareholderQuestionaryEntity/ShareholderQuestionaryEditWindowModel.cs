using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.UnitEntity;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity
{
    public class ShareholderQuestionaryEditWindowModel : ShareholderQuestionaryViewModel
    {
        public ShareholderQuestionaryEditWindowModel(ShareholderQuestionary shareholderQuestionary, IDocumentService documentService, IPleaseWaitService pleaseWaitService, ICommandManager commandManager) : base(shareholderQuestionary, documentService)
        {
            MailingAddress = ShareholderQuestionaryModel.MailingAddress ?? new Address();

            MailingAddressViewModel = new MailingAddressViewModel("Адрес для направления уведомления", MailingAddress, pleaseWaitService, commandManager, "");
        }

        #region SubmittingReason property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public QuestionnaireSubmittingReason SubmittingReason
        {
            get { return GetValue<QuestionnaireSubmittingReason>(SubmittingReasonProperty); }
            set { SetValue(SubmittingReasonProperty, value); }
        }

        public static readonly PropertyData SubmittingReasonProperty = RegisterProperty("SubmittingReason", typeof(QuestionnaireSubmittingReason), QuestionnaireSubmittingReason.Unknown);

        #endregion

        #region NotificationRequiredFlag property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public bool NotificationRequiredFlag
        {
            get { return GetValue<bool>(NotificationRequiredFlagProperty); }
            set { SetValue(NotificationRequiredFlagProperty, value); }
        }

        public static readonly PropertyData NotificationRequiredFlagProperty = RegisterProperty("NotificationRequiredFlag", typeof(bool), false);

        #endregion

        #region AddressForNotificationAsMailingAddressFlag property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public bool AddressForNotificationAsMailingAddressFlag
        {
            get { return GetValue<bool>(AddressForNotificationAsMailingAddressFlagProperty); }
            set { SetValue(AddressForNotificationAsMailingAddressFlagProperty, value); }
        }

        public static readonly PropertyData AddressForNotificationAsMailingAddressFlagProperty = RegisterProperty("AddressForNotificationAsMailingAddressFlag", typeof(bool), true);

        #endregion

        #region AddressForNotification property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public virtual Address MailingAddress
        {
            get { return GetValue<Address>(MailingAddressProperty); }
            set { SetValue(MailingAddressProperty, value); }
        }

        public static readonly PropertyData MailingAddressProperty = RegisterProperty("MailingAddress", typeof(Address));

        #endregion

        #region Signatory property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public virtual Person Signatory
        {
            get { return GetValue<Person>(SignatoryProperty); }
            set { SetValue(SignatoryProperty, value); }
        }

        public static readonly PropertyData SignatoryProperty = RegisterProperty("Signatory", typeof(Person));

        #endregion

        #region NotificationReceivingMethod property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public NotificationReceivingMethod NotificationReceivingMethod
        {
            get { return GetValue<NotificationReceivingMethod>(NotificationReceivingMethodProperty); }
            set { SetValue(NotificationReceivingMethodProperty, value); }
        }

        public static readonly PropertyData NotificationReceivingMethodProperty = RegisterProperty("NotificationReceivingMethod", typeof(NotificationReceivingMethod), NotificationReceivingMethod.Personally);

        #endregion



       



        #region MailingAddressViewModel property

        public IViewModel MailingAddressViewModel
        {
            get { return GetValue<IViewModel>(MailingAddressViewModelProperty); }
            set { SetValue(MailingAddressViewModelProperty, value); }
        }

        public static readonly PropertyData MailingAddressViewModelProperty = RegisterProperty("MailingAddressViewModel", typeof (IViewModel));

        #endregion
    }
}