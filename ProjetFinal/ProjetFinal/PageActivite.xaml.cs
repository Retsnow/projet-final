using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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
        NavigationViewItem nv_activite;


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
            object[] objects = new object[2];

            if (e.Parameter is not null)
            {
                objects = (object[])e.Parameter;
                window = objects[0] as MainWindow;
                nv_activite = objects[1] as NavigationViewItem;
            }

        }

        private async void btnDelete_ClickAsync(object sender, RoutedEventArgs e)
        {
            PermissionDeleteDialog dialog = new PermissionDeleteDialog();
            ContentDialogResult resultat = ContentDialogResult.None;

            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            gvActivites.SelectedItem = activite;

            if (SingletonRequete.getListeSeance(activite.Nom).Count() > 0)
            {
                dialog.XamlRoot = gvActivites.XamlRoot;
                dialog.Title = "Supprimer Activité";
                dialog.Message = "Êtes-vous sûr de vouloir supprimer l'activité selectionné ainsi que les séances et inscriptions lié à cette activité?";
                dialog.CloseButtonText = "Annuler";
                dialog.PrimaryButtonText = "Supprimer";
                dialog.DefaultButton = ContentDialogButton.Close;

                resultat = await dialog.ShowAsync();

                if (dialog.Supprimer)
                {
                    foreach (Seance seance in SingletonRequete.getListeSeance(activite.Nom))
                    {
                        SingletonRequete.supprimerInscription(seance.Id);
                        SingletonRequete.supprimerSeance(seance.Id);
                    }

                    SingletonRequete.supprimerActivite(activite.Nom);
                }
            }
            else
            {
                dialog.XamlRoot = gvActivites.XamlRoot;
                dialog.Title = "Supprimer Activité";
                dialog.Message = "Êtes-vous sûr de vouloir supprimer l'activité selectionné?";
                dialog.CloseButtonText = "Annuler";
                dialog.PrimaryButtonText = "Supprimer";
                dialog.DefaultButton = ContentDialogButton.Close;

                resultat = await dialog.ShowAsync();

                if (dialog.Supprimer)
                    SingletonRequete.supprimerActivite(activite.Nom);
            }

            gvActivites.ItemsSource = SingletonRequete.getListeActivite();
        }

        private void gvActivites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!RoleUtilisateur.Admin)
            {
                Activite activite = gvActivites.SelectedItem as Activite;

                window.mainFrame.Navigate(typeof(PageSeance), new object[3] { activite, window, nv_activite });
            }
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
            window.mainFrame.Navigate(typeof(PageAjouterActivite), new object[2] { nv_activite, null });
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            gvActivites.SelectedItem = activite;

            window.mainFrame.Navigate(typeof(PageAjouterActivite), new object[2] { nv_activite, activite });
        }

        private void btnSeance_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            gvActivites.SelectedItem = activite;

            window.mainFrame.Navigate(typeof(PageSeance), new object[3] { activite, window, nv_activite });
        }
    }
}
