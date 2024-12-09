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
            window.mainFrame.Navigate(typeof(PageAjouterAdherent));
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Adherent adherent = button.DataContext as Adherent;

            //permet de s'assurer que nous avons un élément sélectionné
            lv_adherent.SelectedItem = adherent;

            SingletonRequete.supprimerAdherent(adherent.Id);

            lv_adherent.ItemsSource = SingletonRequete.getListeAdherent();
        }
    }
}
