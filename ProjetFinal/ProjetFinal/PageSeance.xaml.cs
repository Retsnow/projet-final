using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
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
    public sealed partial class PageSeance : Page
    {
        Activite activite;

        List<DateTime> selectableDates = new List<DateTime>();


        public PageSeance()
        {
            this.InitializeComponent();

            foreach (Seance seance in SingletonRequete.getListeSeance(activite.Nom))
            {
                selectableDates.Add(seance.Date);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is not null)
            {
                activite = (Activite)e.Parameter;
            }
        }


        private void CalendarPicker_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item != null)
            {
                DateTime date = args.Item.Date.DateTime;

                // Si la date n'est pas dans la liste des dates sélectionnables, appliquez le style de non-sélection
                if (!selectableDates.Contains(date))
                {
                    args.Item.Style = (Style)this.Resources["NonSelectableDayStyle"];
                }
                else
                {
                    // Si la date est sélectionnable, enlever le style de non-sélection (si besoin)
                    args.Item.ClearValue(CalendarViewDayItem.StyleProperty);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Seance seance = button.DataContext as Seance;

            //permet de s'assurer que nous avons un élément sélectionné
            gvSeances.SelectedItem = seance;

            SingletonRequete.supprimerSeance(seance.Id);

            gvSeances.ItemsSource = SingletonRequete.getListeActivite();
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            gvSeances.SelectedItem = activite;

            SingletonRequete.InscriptionAdherant(RoleUtilisateur.UtilisateurConnecte, activite.Nom);
        }
    }
}