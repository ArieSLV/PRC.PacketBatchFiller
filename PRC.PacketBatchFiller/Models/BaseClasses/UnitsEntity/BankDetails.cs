using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity
{
    [Table("BankDetails")]
    public class BankDetails : ModelBase
    {
        #region BankDetailsId property

        /// <summary>
        ///     Gets or sets the BankDetailsId value.
        /// </summary>
        public long BankDetailsId
        {
            get { return GetValue<long>(BankDetailsIdProperty); }
            set { SetValue(BankDetailsIdProperty, value); }
        }

        /// <summary>
        ///     BankDetailsId property data.
        /// </summary>
        public static readonly PropertyData BankDetailsIdProperty = RegisterProperty("BankDetailsId", typeof (long));

        #endregion

        #region PersonalAccount property

        /// <summary>
        ///     Gets or sets the PersonalAccount value.
        /// </summary>
        public string PersonalAccount
        {
            get { return GetValue<string>(PersonalAccountProperty); }
            set { SetValue(PersonalAccountProperty, value); }
        }

        /// <summary>
        ///     PersonalAccount property data.
        /// </summary>
        public static readonly PropertyData PersonalAccountProperty = RegisterProperty("PersonalAccount",
            typeof (string));

        #endregion

        #region BankBranchName property

        /// <summary>
        ///     Gets or sets the BankBranchName value.
        /// </summary>
        public string BankBranchName
        {
            get { return GetValue<string>(BankBranchNameProperty); }
            set { SetValue(BankBranchNameProperty, value); }
        }

        /// <summary>
        ///     BankBranchName property data.
        /// </summary>
        public static readonly PropertyData BankBranchNameProperty = RegisterProperty("BankBranchName", typeof (string));

        #endregion

        #region MainAccount property

        /// <summary>
        ///     Gets or sets the MainAccount value.
        /// </summary>
        public string MainAccount
        {
            get { return GetValue<string>(MainAccountProperty); }
            set { SetValue(MainAccountProperty, value); }
        }

        /// <summary>
        ///     MainAccount property data.
        /// </summary>
        public static readonly PropertyData MainAccountProperty = RegisterProperty("MainAccount", typeof (string));

        #endregion

        #region CorrAccount property

        /// <summary>
        ///     Gets or sets the CorrAccount value.
        /// </summary>
        public string CorrAccount
        {
            get { return GetValue<string>(CorrAccountProperty); }
            set { SetValue(CorrAccountProperty, value); }
        }

        /// <summary>
        ///     CorrAccount property data.
        /// </summary>
        public static readonly PropertyData CorrAccountProperty = RegisterProperty("CorrAccount", typeof (string));

        #endregion

        #region BankName property

        /// <summary>
        ///     Gets or sets the BankName value.
        /// </summary>
        public string BankName
        {
            get { return GetValue<string>(BankNameProperty); }
            set { SetValue(BankNameProperty, value); }
        }

        /// <summary>
        ///     BankName property data.
        /// </summary>
        public static readonly PropertyData BankNameProperty = RegisterProperty("BankName", typeof (string));

        #endregion

        #region BankINN property

        /// <summary>
        ///     Gets or sets the BankINN value.
        /// </summary>
        public string BankINN
        {
            get { return GetValue<string>(BankINNProperty); }
            set { SetValue(BankINNProperty, value); }
        }

        /// <summary>
        ///     BankINN property data.
        /// </summary>
        public static readonly PropertyData BankINNProperty = RegisterProperty("BankINN", typeof (string));

        #endregion

        #region BIK property

        /// <summary>
        ///     Gets or sets the BIK value.
        /// </summary>
        public string BIK
        {
            get { return GetValue<string>(BIKProperty); }
            set { SetValue(BIKProperty, value); }
        }

        /// <summary>
        ///     BIK property data.
        /// </summary>
        public static readonly PropertyData BIKProperty = RegisterProperty("BIK", typeof (string));

        #endregion

        #region BankCity property

        /// <summary>
        ///     Gets or sets the BankCity value.
        /// </summary>
        public string BankCity
        {
            get { return GetValue<string>(BankCityProperty); }
            set { SetValue(BankCityProperty, value); }
        }

        /// <summary>
        ///     BankCity property data.
        /// </summary>
        public static readonly PropertyData BankCityProperty = RegisterProperty("BankCity", typeof (string));

        #endregion
    }
}