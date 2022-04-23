using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Catel.Data;
using Catel.IoC;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;
using PRC.PacketBatchFiller.ViewModels.Suggest;

namespace PRC.PacketBatchFiller.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IMessageService _messageService;
        


        public UnitService(IUIVisualizerService uiVisualizerService, IMessageService messageService)
        {
            _uiVisualizerService = uiVisualizerService;
            _messageService = messageService;
            _itemOfUnitForRestoreList = new List<ItemOfUnitForRestore>();
            UnitsForRestore = new List<Unit>();
            ShareholderAccountsListToRemove = new List<long>();
            IssuesOfSecuritiesListToRemove = new List<long>();
        }

        #region Unit openned windows history

        private List<Unit> UnitsForRestore { get; }

        public void AddUnitToHistory(Unit unit)
        {
            UnitsForRestore.Add(unit);
        }

        public Unit GetLastUnitFromHistory()
        {
            if (UnitsForRestore.Count <= 0) return null;

            var unitForRestore = UnitsForRestore[UnitsForRestore.Count - 1];
            UnitsForRestore.Remove(unitForRestore);

            return unitForRestore;
        }

        #endregion
        
        #region Suggest items restoring

        private class ItemOfUnitForRestore
        {
            public long UnitIdForRestoreInSuggestList { get; set; }
            public Type UnitType { get; set; }
            public long TargetUnitId { get; set; }
        }
        
        private readonly List<ItemOfUnitForRestore> _itemOfUnitForRestoreList;

        private string SearchText { get; set; }

        public void SetSearchText(string value) { SearchText = value; }
        public string GetSearchText() { return SearchText; }

        private static class LevenshteinDistance
        {
            public static int Compute(string s, string t)
            {
                if (string.IsNullOrEmpty(s))
                {
                    if (string.IsNullOrEmpty(t))
                        return 0;
                    return t.Length;
                }

                if (string.IsNullOrEmpty(t))
                {
                    return s.Length;
                }

                var n = s.Length;
                var m = t.Length;
                var d = new int[n + 1, m + 1];

                // initialize the top and right of the table to 0, 1, 2, ...
                for (var i = 0; i <= n; d[i, 0] = i++) ;
                for (var j = 1; j <= m; d[0, j] = j++) ;

                for (var i = 1; i <= n; i++)
                {
                    for (var j = 1; j <= m; j++)
                    {
                        var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                        var min1 = d[i - 1, j] + 1;
                        var min2 = d[i, j - 1] + 1;
                        var min3 = d[i - 1, j - 1] + cost;
                        d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                    }
                }
                return d[n, m];
            }
        }

        public async void CreateNewItemInItemOfUnitForRestoreList(Type type, long targetUnitId)
        {
            _itemOfUnitForRestoreList.Add(new ItemOfUnitForRestore {UnitType = type, TargetUnitId = targetUnitId});

            if (type == typeof (FirstPersonOfCompanyViewModel))
            {
                var unitWindowModel = RegisterViewModel(type, targetUnitId);
                if (await _uiVisualizerService.ShowDialogAsync(unitWindowModel) ?? false) return;
            }


            else if (type == typeof (SecuritiesIssuerSearchViewModel))
            {

                FormOfIncorporation formOfIncorporation = null;
                var levenshteinDistance = int.MaxValue;

                using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
                {
                    foreach (var entity in dbContextManager.Context.FormsOfIncorporation.ToList())
                    {
                        if (!SearchText.ToLower().Contains(entity.FullForm.ToLower())) continue;

                        var newLevenshteinDistance = LevenshteinDistance.Compute(SearchText.ToLower(),
                            entity.FullForm.ToLower());
                        if (levenshteinDistance <= newLevenshteinDistance) continue;

                        levenshteinDistance = newLevenshteinDistance;
                        formOfIncorporation = entity;
                    }
                }

                var asFullName = string.Empty;
                var asShortName = string.Empty;
                
                if (formOfIncorporation != null)
                {
                    asFullName = SearchText;
                    asShortName =
                        $"{formOfIncorporation.ShortForm} \"{asFullName.Substring(formOfIncorporation.FullForm.Length + 2, asFullName.Length - 1 - (formOfIncorporation.FullForm.Length + 2))}\"";
                }

                var newLegalEntity = new LegalEntity
                {
                    FullName = asFullName,
                    ShortName = asShortName,
                    FormOfIncorporation = formOfIncorporation,
                    RoleIsSecuritiesIssuerFlag = true
                };

                var legalEntity = (LegalEntity) await OpenUnitWindow(newLegalEntity);


                AddUnitIdToItemInItemOfUnitForRestoreList(type, targetUnitId, legalEntity.UnitId);

            }

            await OpenUnitWindow(GetLastUnitFromHistory());
        }



        public void AddUnitIdToItemInItemOfUnitForRestoreList(Type type, long targetUnitId, long unitIdForRestore)
        {
            foreach (var itemOfUnitForRestore in _itemOfUnitForRestoreList)
            {
                if (itemOfUnitForRestore.TargetUnitId == targetUnitId && itemOfUnitForRestore.UnitType == type)
                {
                    itemOfUnitForRestore.UnitIdForRestoreInSuggestList = unitIdForRestore;
                }
                
            }
            
        }

        public LegalEntity GetUnitFromRestoreList(Type type, long targetUnitId)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                foreach (var itemOfUnitForRestore in _itemOfUnitForRestoreList)
                {
                    if (itemOfUnitForRestore.TargetUnitId == targetUnitId && itemOfUnitForRestore.UnitType == type)
                    {
                        var entity = dbContextManager.Context.LegalEntities.Find(itemOfUnitForRestore.UnitIdForRestoreInSuggestList);
                        _itemOfUnitForRestoreList.Remove(itemOfUnitForRestore);
                        return entity;
                    }
                }
                return null;
            }
        }

        
        public void RemoveUnitForRestoreInSuggestList(Type type)
        {
            foreach (var itemOfUnitForRestore in _itemOfUnitForRestoreList.Where(itemOfUnitForRestore => itemOfUnitForRestore.UnitType == type))
            {
                _itemOfUnitForRestoreList.RemoveAt(_itemOfUnitForRestoreList.IndexOf(itemOfUnitForRestore));
                return;
            }
        }
        
        #endregion

        #region Unit Context actions

        public async Task<Unit> OpenUnitWindow(Unit unit)
        {
            Unit unitToReturn = null;

            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                #region LegalEntity

                if (unit is LegalEntity)
                {
                    var legalEntity = dbContextManager.Context.LegalEntities.Find(unit.UnitId);

                    foreach (var itemOfUnitForRestore in _itemOfUnitForRestoreList
                        .Where(itemOfUnitForRestore => 
                                itemOfUnitForRestore.TargetUnitId == unit.UnitId &&    
                                itemOfUnitForRestore.UnitType == typeof (FirstPersonOfCompanyViewModel)))
                    {
                        legalEntity.FirstPersonOfCompany = dbContextManager.Context.Units.Find(itemOfUnitForRestore.UnitIdForRestoreInSuggestList);
                    }

                    if (legalEntity != null)
                    {
                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.RegistrationCertificate).Load();
                        dbContextManager.Context.Entry(legalEntity.RegistrationCertificate).Reference(o => o.RegistrationCertificateIssuer).Load();

                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.FormOfIncorporation).Load();
                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.AddressRegistration).Load();
                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.MailingAddress).Load();
                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.BankDetails).Load();
                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.Citizenship).Load();
                        dbContextManager.Context.Entry(legalEntity).Reference(o => o.FirstPersonOfCompany).Load();
                        

                        dbContextManager.Context.Entry(legalEntity).Collection(o => o.PhoneNumbers).Load();
                        dbContextManager.Context.Entry(legalEntity).Collection(o => o.Emails).Load();
                        if (legalEntity.RoleIsShareHolderFlag) dbContextManager.Context.Entry(legalEntity).Collection(o => o.ShareholderAccounts).Load();
                        if (legalEntity.RoleIsSecuritiesIssuerFlag) dbContextManager.Context.Entry(legalEntity).Collection(o => o.IssuesOfSecurities).Load();
                    }
                    else
                    {
                        legalEntity = (LegalEntity) unit;
                    }

                    var legalEntityWindowViewModel = RegisterViewModel(legalEntity);

                    if (await _uiVisualizerService.ShowDialogAsync(legalEntityWindowViewModel) ?? false)
                    {
                        SaveUnitToContext(legalEntityWindowViewModel.LegalEntityModel);
                    }

                    unitToReturn = legalEntityWindowViewModel.LegalEntityModel;

                }
                    #endregion

                    #region Person

                else if (unit is Person)
                {
                    var person = dbContextManager.Context.Persons.Find(unit.UnitId);

                    if (person != null)
                    {
                        dbContextManager.Context.Entry(person).Reference(o => o.PlaceOfBirth).Load();

                        dbContextManager.Context.Entry(person).Reference(o => o.CardID).Load();
                        dbContextManager.Context.Entry(person.CardID).Reference(o => o.CardIDType).Load();
                        dbContextManager.Context.Entry(person.CardID).Reference(o => o.CardIDIssuer).Load();

                        dbContextManager.Context.Entry(person).Reference(o => o.AddressRegistration).Load();
                        dbContextManager.Context.Entry(person).Reference(o => o.MailingAddress).Load();
                        dbContextManager.Context.Entry(person).Reference(o => o.BankDetails).Load();
                        dbContextManager.Context.Entry(person).Reference(o => o.Citizenship).Load();

                        dbContextManager.Context.Entry(person).Collection(o => o.PhoneNumbers).Load();
                        dbContextManager.Context.Entry(person).Collection(o => o.Emails).Load();
                        dbContextManager.Context.Entry(person).Collection(o => o.ShareholderAccounts).Load();
                    }
                    else
                    {
                        person = (Person) unit;
                    }

                    var personWindowViewModel = RegisterViewModel(person);


                    if (await _uiVisualizerService.ShowDialogAsync(personWindowViewModel) ?? false)
                    {
                        SaveUnitToContext(personWindowViewModel.PersonModel);
                    }

                    unitToReturn = personWindowViewModel.PersonModel;
                   
                }

                #endregion
            }
            
            return unitToReturn;
        }


        public long SaveUnitToContext(LegalEntity legalEntity)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                CheckLegalEntiesFlags(legalEntity);

                var le = dbContextManager.Context.LegalEntities.Find(legalEntity.UnitId);
                var isNew = le == null;

                var legalEntityModel = legalEntity;
                

                dbContextManager.Context.Citizenships.Attach(legalEntityModel.Citizenship);
                dbContextManager.Context.FormsOfIncorporation.Attach(legalEntityModel.FormOfIncorporation);
                dbContextManager.Context.RegistrationCertificateIssuers.Attach(legalEntityModel.RegistrationCertificate.RegistrationCertificateIssuer);

                if (legalEntityModel.FirstPersonOfCompany != null) dbContextManager.Context.Units.Attach(legalEntityModel.FirstPersonOfCompany);

                legalEntityModel.TimeStamp = DateTime.Now;

                if (isNew) dbContextManager.Context.LegalEntities.Add(legalEntityModel);
                
                dbContextManager.Context.SaveChanges();

                KeepContextInOrder();

                return legalEntityModel.UnitId;
            }
        }

        public long SaveUnitToContext(Person person)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                CheckLegalEntiesFlags(person);

                var le = dbContextManager.Context.Persons.Find(person.UnitId);
                var isNew = le == null;

                var personModel = person;

                dbContextManager.Context.PlaceOfBirths.Attach(personModel.PlaceOfBirth);
                dbContextManager.Context.Citizenships.Attach(personModel.Citizenship);
                dbContextManager.Context.CardIDTypes.Attach(personModel.CardID.CardIDType);
                dbContextManager.Context.CardIDIssuers.Attach(personModel.CardID.CardIDIssuer);
                personModel.TimeStamp = DateTime.Now;

                if (isNew) dbContextManager.Context.Persons.Add(personModel);
                
                dbContextManager.Context.SaveChanges();

                KeepContextInOrder();

                return personModel.UnitId;
            }
        }

        public void RemoveUnitFromDataContext(Unit unit)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                Unit unitToRemove = null;

                if (unit is Person)
                {
                    unitToRemove = dbContextManager.Context.Persons
                        .Include(o => o.CardID)
                        .Include(o => o.BankDetails)
                        .Include(o => o.AddressRegistration)
                        .Include(o => o.Emails)
                        .Include(o => o.MailingAddress)
                        .Include(o => o.PhoneNumbers)
                        .Include(o => o.ShareholderAccounts)
                        .FirstOrDefault(o => o.UnitId == unit.UnitId);
                }

                else if (unit is LegalEntity)
                {
                    unitToRemove = dbContextManager.Context.LegalEntities
                        .Include(o => o.RegistrationCertificate)
                        .Include(o => o.BankDetails)
                        .Include(o => o.AddressRegistration)
                        .Include(o => o.MailingAddress)
                        .Include(o => o.Emails)
                        .Include(o => o.PhoneNumbers)
                        .Include(o => o.ShareholderAccounts)
                        .FirstOrDefault(o => o.UnitId == unit.UnitId);
                }

                if (unitToRemove == null) return;

                foreach (var shareholderAccount in unitToRemove.ShareholderAccounts.ToList())
                {
                    dbContextManager.Context.ShareholderAccounts.Remove(shareholderAccount);
                }

                dbContextManager.Context.Units.Remove(unitToRemove);

                try
                {
                    dbContextManager.Context.SaveChanges();
                }
                catch (Exception c)
                {
                    var messageText = new StringBuilder();
                    messageText.AppendLine(c.Message);
                    messageText.Append(Environment.NewLine);

                    var innerException = c.InnerException;
                    while (innerException != null)
                    {
                        messageText.AppendLine("Внутреннее сообщение:");
                        messageText.AppendLine(innerException.Message);
                        messageText.Append(Environment.NewLine);
                        innerException = innerException.InnerException;
                    }
                    

                    _messageService.ShowAsync(messageText.ToString(), "Ошибка", MessageButton.OK, MessageImage.Error);
                }
                
            }
        }

        #endregion

        #region Keep context in order

        #region ShareholderAccounts

        private List<long> ShareholderAccountsListToRemove { get; }

        public void AddToShareholderAccountsListToRemove(long shareholderAccountId)
        {
            ShareholderAccountsListToRemove.Add(shareholderAccountId);
        }

        //public List<long> GetShareholderAccountsListToRemove()
        //{
        //    return ShareholderAccountsListToRemove;
        //}

        //public void ClearShareholderAccountsListToRemove()
        //{
        //    ShareholderAccountsListToRemove.Clear();
        //}
        #endregion

        #region IssuesOfSecurities

        private List<long> IssuesOfSecuritiesListToRemove { get; }

        public void AddToIssuesOfSecuritiesListToRemove(long issueOfSecuritiesId)
        {
            IssuesOfSecuritiesListToRemove.Add(issueOfSecuritiesId);
        }

        //public List<long> GetIssuesOfSecuritiesListToRemove()
        //{
        //    return IssuesOfSecuritiesListToRemove;
        //}

        //public void ClearIssuesOfSecuritiesListToRemove()
        //{
        //    IssuesOfSecuritiesListToRemove.Clear();
        //}
        #endregion

        #endregion

        #region Methods

        private void CheckLegalEntiesFlags(Unit unit)
        {
            if (unit.MailingAddressEqualRegistrationAddressFlag)
            {
                unit.MailingAddress = unit.AddressRegistration;
            }

            if (!unit.RoleIsShareHolderFlag)
            {
                foreach (var shareholderAccount in unit.ShareholderAccounts)
                {
                    AddToShareholderAccountsListToRemove(shareholderAccount.ShareholderAccountId);
                }
            }

            var asLegalEntity = unit as LegalEntity;
            if (asLegalEntity != null && !asLegalEntity.RoleIsSecuritiesIssuerFlag)
            {
                foreach (var issueOfSecuritiese in asLegalEntity.IssuesOfSecurities)
                {
                    AddToIssuesOfSecuritiesListToRemove(issueOfSecuritiese.IssueOfSecuritiesId);
                }
            }

            if (unit.DividentsPaymentWay == DividentsPaymentWays.ByMail && !(unit is LegalEntity))
            {
                unit.BankDetails = null;
            }
        }

        private void KeepContextInOrder()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                foreach (var entry in ShareholderAccountsListToRemove)
                {
                    var sa = dbContextManager.Context.ShareholderAccounts.Find(entry);
                    if (sa != null) dbContextManager.Context.ShareholderAccounts.Remove(sa);
                }

                foreach (var entry in IssuesOfSecuritiesListToRemove)
                {
                    var ios = dbContextManager.Context.IssuesOfSecurities.Find(entry);
                    if (ios != null) dbContextManager.Context.IssuesOfSecurities.Remove(ios);
                }

                ShareholderAccountsListToRemove.Clear();
                IssuesOfSecuritiesListToRemove.Clear();

                dbContextManager.Context.SaveChanges();
            }
        }

        public PersonWindowModel RegisterViewModel(Person obj)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof (PersonWindowModel));
            _uiVisualizerService.Register(typeof (PersonWindowModel), typeof (Views.PersonWindow));

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<PersonWindowModel>(obj);
        }

        public LegalEntityWindowModel RegisterViewModel(LegalEntity obj)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof(LegalEntityWindowModel));
            _uiVisualizerService.Register(typeof(LegalEntityWindowModel), typeof(Views.LegalEntityWindow));

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<LegalEntityWindowModel>(obj);
        }

        public AddUnitWindowModel RegisterViewModel(Type type, long targetUnitId)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof (AddUnitWindowModel));
            _uiVisualizerService.Register(typeof (AddUnitWindowModel), typeof (Views.AddUnitWindow));
            
            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<AddUnitWindowModel>(type, targetUnitId);
        }


        #endregion
    }
}