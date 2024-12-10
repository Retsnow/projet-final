using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAdherent : Page
    {
        MainWindow window;

        public PageAdherent()
        {
            this.InitializeComponent();
            lv_adherent.ItemsSource = SingletonRequete.getListeAdherent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                window = (MainWindow)e.Parameter;
            }
        }

        private async void btn_export_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "liste_adherent";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });


            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            //écrit dans le fichier
            if (monFichier != null)
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, SingletonRequete.getListeAdherent().Select(x => x.StringCSV).ToList(), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            Adherent adherent = null;
            window.mainFrame.Navigate(typeof(PageAjouterAdherent), new object[2] { adherent, window });
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Adherent adherent = button.DataContext as Adherent;

            lv_adherent.SelectedItem = adherent;

            window.mainFrame.Navigate(typeof(PageAjouterAdherent), new object[2] { adherent, window });
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Adherent adherent = button.DataContext as Adherent;

            PermissionDeleteDialog dialog = new PermissionDeleteDialog();
            dialog.XamlRoot = sv_main.XamlRoot;
            dialog.Title = "Supprimer Adherent";
            dialog.CloseButtonText = "Annuler";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Message = "Voulez-vous vraiment supprimer l'adhérent?\nSes inscriptions aux activités seront aussi supprimer.";
            

            ContentDialogResult resultat = await dialog.ShowAsync();

            

            lv_adherent.SelectedItem = adherent;

            if (dialog.Supprimer)
            SingletonRequete.supprimerAdherent(adherent.Id);

            lv_adherent.ItemsSource = SingletonRequete.getListeAdherent();

        }
    }
}
