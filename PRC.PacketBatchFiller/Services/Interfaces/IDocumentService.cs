using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity;


namespace PRC.PacketBatchFiller.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> OpenDocumentEditWindow(Document doc, ObservableCollection<Unit> authorizedUnitsCollection);

        Task<DocumentPackage> OpenDocumentPackageWindow(DocumentPackage documentPackage);
        void RemoveDocumentPackageFromDataContext(DocumentPackage documentPackage);


        //ShareholderQuestionaryEditWindowModel RegisterViewModel(ShareholderQuestionary obj);

    }
}