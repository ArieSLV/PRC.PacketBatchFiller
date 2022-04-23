using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;

namespace PRC.PacketBatchFiller.ViewModels
{
    //public class AuthorizesDocumentsViewModel : ViewModelBase
    //{
    //    private readonly IUIVisualizerService _uiVisualizerService;
    //    private readonly IUnitService _unitService;
    //    private readonly ICommandManager _commandManager;
    //    private readonly IDocumentService _documentService;

    //    public AuthorizesDocumentsViewModel(ObservableCollection<AuthorizesDocument> authorizesDocuments, IUIVisualizerService uiVisualizerService, IUnitService unitService, ICommandManager commandManager, IDocumentService documentService)
    //    {
    //        _uiVisualizerService = uiVisualizerService;
    //        _unitService = unitService;
    //        _commandManager = commandManager;
    //        _documentService = documentService;
    //        AuthorizesDocumentsCollection = authorizesDocuments ?? new ObservableCollection<AuthorizesDocument>();
    //        AuthorizesDocumentViewModelsCollection = new ObservableCollection<AuthorizesDocumentWindowModel>();

    //        foreach (var authorizesDocument in AuthorizesDocumentsCollection)
    //        {
    //            AuthorizesDocumentViewModelsCollection.Add(new AuthorizesDocumentWindowModel(authorizesDocument, uiVisualizerService, unitService));
    //        }

    //        //AddAuthorizesDocumentCommand = new TaskCommand(AddAuthorizesDocument);

    //    }


    //    #region AuthorizesDocumentsCollection property

    //    public ObservableCollection<AuthorizesDocument> AuthorizesDocumentsCollection
    //    {
    //        get { return GetValue<ObservableCollection<AuthorizesDocument>>(AuthorizesDocumentsCollectionProperty); }
    //        set { SetValue(AuthorizesDocumentsCollectionProperty, value); }
    //    }

    //    public static readonly PropertyData AuthorizesDocumentsCollectionProperty = RegisterProperty("AuthorizesDocumentsCollection", typeof (ObservableCollection<AuthorizesDocument>));

    //    #endregion

    //    #region AuthorizesDocumentViewModelsCollection property

    //    public ObservableCollection<AuthorizesDocumentWindowModel> AuthorizesDocumentViewModelsCollection
    //    {
    //        get { return GetValue<ObservableCollection<AuthorizesDocumentWindowModel>>(AuthorizesDocumentViewModelsCollectionProperty); }
    //        set { SetValue(AuthorizesDocumentViewModelsCollectionProperty, value); }
    //    }

    //    public static readonly PropertyData AuthorizesDocumentViewModelsCollectionProperty = RegisterProperty("AuthorizesDocumentViewModelsCollection", typeof (ObservableCollection<AuthorizesDocumentWindowModel>));

    //    #endregion

    //    //#region AddAuthorizesDocument command

    //    //public TaskCommand AddAuthorizesDocumentCommand { get; set; }

    //    //private async Task AddAuthorizesDocument()
    //    //{
    //    //    if (this.GetRootIViewModel() is LegalEntityWindowModel) _commandManager.GetCommand("SaveAndCloseLegalEntityWindowCommand").Execute(null);
    //    //    else if (this.GetRootIViewModel() is PersonWindowModel) _commandManager.GetCommand("SaveAndClosePersonWindowCommand").Execute(null);

    //    //    var ad = await _documentService.OpenAuthorizesDocumentWindow(new AuthorizesDocument());

    //    //    if (ad != null) AuthorizesDocumentViewModelsCollection.Add(new AuthorizesDocumentWindowModel(ad, _uiVisualizerService, _unitService));
    //    //    await _unitService.OpenUnitWindow(_unitService.GetLastUnitFromHistory());
    //    //}

    //    //#endregion
    //}
}