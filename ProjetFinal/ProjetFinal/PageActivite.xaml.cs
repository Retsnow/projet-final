using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageActivite : Page
    {
        MainWindow window;

        public PageActivite()
        {
            this.InitializeComponent();
            gvActivites.ItemsSource = SingletonRequete.getListeActivite();
            
            if (RoleUtilisateur.Admin)
            {
                btn_export.Visibility = Visibility.Visible;
                btn_ajouter.Visibility = Visibility.Visible;
            }
            else
            {
                btn_export.Visibility = Visibility.Collapsed;
                btn_ajouter.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                window = (MainWindow)e.Parameter;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            gvActivites.SelectedItem = activite;

            SingletonRequete.supprimerActivite(activite.Nom);

            gvActivites.ItemsSource = SingletonRequete.getListeActivite();
        }

        private void gvActivites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(PageSeance), gvActivites.SelectedItem);
        }

        private async void btn_export_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "liste_activite";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });


            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            //écrit dans le fichier
            if (monFichier != null)
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, SingletonRequete.getListeActivite().Select(x => x.StringCSV).ToList(), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            window.mainFrame.Navigate(typeof(PageAjouterActivite));
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
