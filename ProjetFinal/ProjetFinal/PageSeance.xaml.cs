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
        NavigationViewItem nv_activite;

        int idSeance;

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
                nv_activite = objects[2] as NavigationViewItem;
            }
            cbxDate.ItemsSource = SingletonRequete.getListeSeance(activite.Nom);
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SingletonRequete.supprimerSeance(idSeance);
            cbxDate.ItemsSource = SingletonRequete.getListeSeance(activite.Nom);
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            if (RoleUtilisateur.UtilisateurConnecte != null)
            {
                string temp = SingletonRequete.InscriptionAdherantSeance(RoleUtilisateur.UtilisateurConnecte, idSeance);

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
                cbxDate.ItemsSource = SingletonRequete.getListeSeance(activite.Nom);
            }
        }

        private void ratingControl_ValueChanged(RatingControl sender, object args)
        {
            SingletonRequete.AdherentNoteSeance(RoleUtilisateur.UtilisateurConnecte, ratingControl.Value, idSeance);
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            window.mainFrame.Navigate(typeof(PageAjouterSeance), new object[3] { nv_activite, activite, SingletonRequete.getSeance(idSeance) });
        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            window.mainFrame.Navigate(typeof(PageAjouterSeance), new object[3] { nv_activite, activite, null });
        }

        private void cbxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxDate.SelectedIndex != -1)
            {
                DateOnly date = (cbxDate.SelectedItem as Seance).Date;

                foreach (Seance seanceTemp in SingletonRequete.getListeSeance(activite.Nom))
                {
                    if (seanceTemp.Date == date)
                    {
                        txtHeure.Text = seanceTemp.Heure.ToString();
                        string u = RoleUtilisateur.UtilisateurConnecte;
                        ratingControl.IsEnabled = true;

                        idSeance = SingletonRequete.TrouverIdSeance(activite.Nom,
                            new DateTime(date.Year, date.Month, date.Day));

                        ratingControl.Value = SingletonRequete.prendreNote(u, idSeance);
                    }
                }

                if (RoleUtilisateur.UtilisateurConnecte != null)
                {
                    if (SingletonRequete.UtilisateurEstInscritSeance(RoleUtilisateur.UtilisateurConnecte, idSeance))
                    {
                        ratingControl.Visibility = Visibility.Visible;
                        btnInscription.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        ratingControl.Visibility = Visibility.Collapsed;
                        btnInscription.Visibility = Visibility.Visible;
                    }
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
    }
}