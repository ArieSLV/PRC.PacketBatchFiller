using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Catel.ApiCop;
using Catel.ApiCop.Listeners;
using Catel.IoC;
using Catel.Logging;
using Catel.MVVM;
using Catel.Windows;
using PRC.PacketBatchFiller.Services;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller
{
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            #if DEBUG
            LogManager.AddDebugListener();
            #endif

            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("ru-RU");

            Log.Info("Starting application");

            // To force the loading of all assemblies at startup, uncomment the lines below:

            //Log.Info("Preloading assemblies");
            //AppDomain.CurrentDomain.PreloadAssemblies();

            #region Catel's improve performance

            // Want to improve performance? Uncomment the lines below. Note though that this will disable
            // some features. 
            //
            // For more information, see https://catelproject.atlassian.net/wiki/display/CTL/Performance+considerations

            Log.Info("Improving performance");
            Catel.Data.ModelBase.DefaultSuspendValidationValue = true;
            Catel.Windows.Controls.UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            Catel.Windows.Controls.UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;
            
            #endregion
            
            #region Register custom types in the ServiceLocator

            var serviceLocator = ServiceLocator.Default;
            serviceLocator.RegisterType<IUnitService, UnitService>();
            serviceLocator.RegisterType<IDocumentService, DocumentService>();

            #endregion

            #region Register custom naming conventions

            var viewModelLocator = serviceLocator.ResolveType<IViewModelLocator>();
            var viewLocator = serviceLocator.ResolveType<IViewLocator>();

            string[] folderNames =
            {
                "Suggest",
                "Documents",
                    "Documents.AuthorizesDocuments",
                    "Documents.ShareholderDocument", "Documents.ShareholderDocumentEntity",
                        "Documents.ShareholderDocument.ShareholderQuestionaryEntity",        "Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity",
                        "Documents.ShareholderDocument.ShareholderAuthorizesDocumentEntity", "Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity",
                        "Documents.ShareholderDocument.ShareholderTransferOrderEntity", "Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity",


                "LegalEntity", "LegalEntityEntity",
                    "LegalEntity.FormOfIncorporation", "LegalEntityEntity.FormOfIncorporationEntity",
                    "LegalEntity.RegistrationCertificateIssuer", "LegalEntityEntity.RegistrationCertificateIssuer",
                    "LegalEntity.IssuesOfSecurities", "LegalEntityEntity.IssuesOfSecurities",
                "Person", "PersonEntity",
                    "Person.CardIDIssuer", "PersonEntity.CardIDIssuer",
                    "Person.PlaceOfBirth", "PersonEntity.PlaceOfBirth",
                "Unit", "UnitEntity",
                    "Unit.ShareholderAccounts", "UnitEntity.ShareholderAccounts",
                    "Unit.Emails", "UnitEntity.Emails",
                    "Unit.PhoneNumbers", "UnitEntity.PhoneNumbers",
                    "Unit.CitizenshipEntity", "UnitEntity.CitizenshipEntity",
            };


            foreach (var folderName in folderNames)
            {
                viewModelLocator.NamingConventions.Add($"[AS].ViewModels.{folderName}.[VW]ViewModel");
                viewModelLocator.NamingConventions.Add($"[AS].ViewModels.{folderName}.[VW]WindowModel");
                viewLocator.NamingConventions.Add($"[AS].Views.{folderName}.[VM]View");
                viewLocator.NamingConventions.Add($"[AS].Views.{folderName}.[VM]Window");
            }
            

            #endregion

            #region Command management  

            var commandManager = ServiceLocator.Default.ResolveType<ICommandManager>();

            commandManager.CreateCommand("GetFocusOnMailingAddressEqualRegistrationAddressCheckBoxCommand");
            commandManager.CreateCommand("GetFocusOnDividentsPaymentWayByMailRadioButtonCommand");
            commandManager.CreateCommand("AddCharToPhoneCommand");

            commandManager.CreateCommand("SaveAndCloseLegalEntityWindowCommand");
            commandManager.CreateCommand("SaveAndClosePersonWindowCommand");

            //commandManager.CreateCommand("");
            //commandManager.CreateCommand("");
            //commandManager.CreateCommand("");
            //commandManager.CreateCommand("");

            #endregion

            StyleHelper.CreateStyleForwardersForDefaultStyles();

            Log.Info("Calling base.OnStartup");
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            ApiCopManager.AddListener(new ConsoleApiCopListener());
            ApiCopManager.WriteResults();

            base.OnExit(e);
        }

        
    }
}