using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
        MainWindow window;
        
        int idSeance;

        List<DateTime> selectableDates = new List<DateTime>();
        List<DateTime> heuresSelectionnables = new List<DateTime>();



        public PageSeance()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            object[] objects = new object[2];

            if (e.Parameter is not null)
            {
                objects = (object[])e.Parameter;
                activite = objects[0] as Activite;
                window = objects[1] as MainWindow;
            }

            CalendarPicker.MinDate = DateTimeOffset.Now;

            foreach (Seance seanceTemp in SingletonRequete.getListeSeance(activite.Nom))
            {
                if (seanceTemp.UtilisateurInscrit)
                {
                    selectableDates.Add(seanceTemp.Date);
                    heuresSelectionnables.Add(new DateTime(seanceTemp.Date.Year, seanceTemp.Date.Month, seanceTemp.Date.Day, seanceTemp.Heure.Hour, seanceTemp.Heure.Minute, 0));
                }
            }
        }


        private void CalendarPicker_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Item != null)
            {
                DateTime date = new DateTime(args.Item.Date.DateTime.Year, args.Item.Date.DateTime.Month, args.Item.Date.DateTime.Day);

                // Si la date n'est pas dans la liste des dates sélectionnables, appliquez le style de non-sélection
                if (!selectableDates.Contains(date))
                {
                    args.Item.IsEnabled = false;
                    args.Item.Foreground = new SolidColorBrush(Colors.Gray);
                }
                else if (selectableDates.Contains(date))
                {
                    // Si la date est sélectionnable, enlever le style de non-sélection (si besoin)
                    args.Item.ClearValue(CalendarViewDayItem.StyleProperty);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SingletonRequete.supprimerSeance(idSeance);
            CalendarPicker.Date = null;
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            SingletonRequete.InscriptionAdherantSeance(RoleUtilisateur.UtilisateurConnecte, idSeance);

            if (RoleUtilisateur.UtilisateurConnecte != null)
            {
                if (SingletonRequete.UtilisateurEstInscritSeance(RoleUtilisateur.UtilisateurConnecte, idSeance))
                {
                    ratingControl.Visibility = Visibility.Visible;
                    btnInscription.Visibility = Visibility.Collapsed;
                }
                else 
                { 
                    btnInscription.Visibility = Visibility.Visible;
                    ratingControl.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ratingControl_ValueChanged(RatingControl sender, object args)
        {
            SingletonRequete.AdherentNoteSeance(RoleUtilisateur.UtilisateurConnecte, ratingControl.Value, idSeance);
        }

        private void CalendarPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (CalendarPicker.Date != null)
            {
                DateTime date = new DateTime(CalendarPicker.Date.Value.Year, CalendarPicker.Date.Value.Month, CalendarPicker.Date.Value.Day);

                foreach (Seance seanceTemp in SingletonRequete.getListeSeance(activite.Nom))
                {
                    if (seanceTemp.Date == date)
                    {
                        txtHeure.Text = seanceTemp.Heure.ToString();
                        string u = RoleUtilisateur.UtilisateurConnecte;
                        ratingControl.IsEnabled = true;

                        idSeance = SingletonRequete.TrouverIdSeance(u, activite.Nom,
                            new DateTime(CalendarPicker.Date.Value.Year, CalendarPicker.Date.Value.Month, CalendarPicker.Date.Value.Day));

                        ratingControl.Value = SingletonRequete.prendreNote(u, idSeance);
                    }
                }

                if (RoleUtilisateur.UtilisateurConnecte != null)
                {
                    if (SingletonRequete.UtilisateurEstInscritSeance(RoleUtilisateur.UtilisateurConnecte, idSeance))
                        ratingControl.Visibility = Visibility.Visible;
                    else
                        btnInscription.Visibility = Visibility.Visible;
                }
                else
                {
                    ratingControl.Visibility = Visibility.Collapsed;
                    btnInscription.Visibility = Visibility.Collapsed;
                }
                if (RoleUtilisateur.Admin)
                {
                    btnDelete.Visibility = Visibility.Visible;
                    btnModifier.Visibility = Visibility.Visible;
                }

            }
            else
            {
                txtHeure.Text = "";
                ratingControl.Visibility = Visibility.Collapsed;
                btnInscription.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnModifier.Visibility = Visibility.Collapsed;
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}