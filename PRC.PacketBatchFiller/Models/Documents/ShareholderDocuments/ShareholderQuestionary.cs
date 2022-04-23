using System;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments
{
    public enum QuestionnaireSubmittingReason { Unknown, Open, Edit }
    
    [Serializable]
    [Table("ShareholderQuestionnaires")]
    public class ShareholderQuestionary : Document
    {
        public const string FormName = "Анкета зарегистрированного лица";

        #region ShareholderAccount property

        public virtual ShareholderAccount ShareholderAccount
        {
            get { return GetValue<ShareholderAccount>(ShareholderAccountProperty); }
            set { SetValue(ShareholderAccountProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountProperty = RegisterProperty("ShareholderAccount", typeof (ShareholderAccount));

        #endregion

        #region SubmittingReason property

        public QuestionnaireSubmittingReason SubmittingReason
        {
            get { return GetValue<QuestionnaireSubmittingReason>(SubmittingReasonProperty); }
            set { SetValue(SubmittingReasonProperty, value); }
        }

        public static readonly PropertyData SubmittingReasonProperty = RegisterProperty("SubmittingReason", typeof (QuestionnaireSubmittingReason), QuestionnaireSubmittingReason.Unknown);

        #endregion

        #region NotificationRequiredFlag property

        public bool NotificationRequiredFlag
        {
            get { return GetValue<bool>(NotificationRequiredFlagProperty); }
            set { SetValue(NotificationRequiredFlagProperty, value); }
        }

        public static readonly PropertyData NotificationRequiredFlagProperty = RegisterProperty("NotificationRequiredFlag", typeof (bool), false);

        #endregion

        #region AddressForNotificationAsMailingAddressFlag property

        public bool AddressForNotificationAsMailingAddressFlag
        {
            get { return GetValue<bool>(AddressForNotificationAsMailingAddressFlagProperty); }
            set { SetValue(AddressForNotificationAsMailingAddressFlagProperty, value); }
        }

        public static readonly PropertyData AddressForNotificationAsMailingAddressFlagProperty = RegisterProperty("AddressForNotificationAsMailingAddressFlag", typeof (bool), true);

        #endregion

        #region MailingAddress property

        public virtual Address MailingAddress
        {
            get { return GetValue<Address>(MailingAddressProperty); }
            set { SetValue(MailingAddressProperty, value); }
        }

        public static readonly PropertyData MailingAddressProperty = RegisterProperty("MailingAddress", typeof (Address));

        #endregion

        #region NotificationReceivingMethod property

        public NotificationReceivingMethod NotificationReceivingMethod
        {
            get { return GetValue<NotificationReceivingMethod>(NotificationReceivingMethodProperty); }
            set { SetValue(NotificationReceivingMethodProperty, value); }
        }

        public static readonly PropertyData NotificationReceivingMethodProperty = RegisterProperty("NotificationReceivingMethod", typeof (NotificationReceivingMethod), NotificationReceivingMethod.Personally);

        #endregion
    }
}