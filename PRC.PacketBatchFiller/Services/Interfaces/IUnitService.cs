using System;
using System.Threading.Tasks;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.ViewModels;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;

namespace PRC.PacketBatchFiller.Services.Interfaces
{
    public interface IUnitService
    {
        Task<Unit> OpenUnitWindow(Unit unit);

        void RemoveUnitFromDataContext(Unit unit);
        

        long SaveUnitToContext(LegalEntity legalEntity);
        long SaveUnitToContext(Person person);


        void AddUnitToHistory(Unit unit);

        Unit GetLastUnitFromHistory();

        void SetSearchText(string value);
        string GetSearchText();


        void AddUnitIdToItemInItemOfUnitForRestoreList(Type type, long targetUnitId, long unitIdForRestore);
        void CreateNewItemInItemOfUnitForRestoreList(Type type, long targetUnitId);


        LegalEntity GetUnitFromRestoreList(Type type, long targetUnitId);

        void RemoveUnitForRestoreInSuggestList(Type type);

        AddUnitWindowModel RegisterViewModel(Type type, long targetUnitId);
        LegalEntityWindowModel RegisterViewModel(LegalEntity obj);
        PersonWindowModel RegisterViewModel(Person obj);


        #region Keep context in order

        #region ShareholderAccounts
        void AddToShareholderAccountsListToRemove(long shareholderAccountId);
        //List<long> GetShareholderAccountsListToRemove();
        //void ClearShareholderAccountsListToRemove();
        #endregion

        #region IssuesOfSecurities
        void AddToIssuesOfSecuritiesListToRemove(long issueOfSecuritiesId);
        //List<long> GetIssuesOfSecuritiesListToRemove();
        //void ClearIssuesOfSecuritiesListToRemove();
        #endregion

        #endregion



    }
}