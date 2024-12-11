using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private NavigationViewItem itemPrecedent;
        public MainWindow()
        {
            this.InitializeComponent();
            nv_main.BackRequested += Nv_main_BackRequested;
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "fr-FR";
        }

        private void Nv_main_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            // À modiffier pour etre plus userfriendly
            if (MainFrame.CanGoBack)
            {
                if (!RoleUtilisateur.Admin && RoleUtilisateur.UtilisateurConnecte == "" && (itemPrecedent.Tag as string == "pageconnexion" || itemPrecedent.Tag as string == "pageactivite" || itemPrecedent.Tag as string == "pageseance"))
                    MainFrame.GoBack();
                if (!RoleUtilisateur.Admin && RoleUtilisateur.UtilisateurConnecte != "" && (itemPrecedent.Tag as string == "pagedeconnexion" || itemPrecedent.Tag as string == "pageactivite" || itemPrecedent.Tag as string == "pageseance"))
                    MainFrame.GoBack();
                if (RoleUtilisateur.Admin && itemPrecedent.Tag as string != "pageconnexion")
                    MainFrame.GoBack();

            }
        }

        public Frame mainFrame
        {
            get { return MainFrame; }
        }



        private void nv_main_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            itemPrecedent = nv_main.SelectedItem as NavigationViewItem;
            if (nv_activite.IsSelected)
            {
                MainFrame.Navigate(typeof(PageActivite), new object[2] { this, nv_activite });
            }
            else if (nv_connexion.IsSelected)
            {
                MainFrame.Navigate(typeof(PageConnection), nv_activite);
            }
            else if (nv_deconnexion.IsSelected)
            {
                MainFrame.Navigate(typeof(PageDeconnexion), nv_activite);
            }
            else if (nv_statistique.IsSelected)
            {
                MainFrame.Navigate(typeof(PageStatistique));
            }
            else if (nv_adherent.IsSelected)
            {
                MainFrame.Navigate(typeof(PageAdherent), this);
            }


            if (RoleUtilisateur.UtilisateurConnecte != null && RoleUtilisateur.UtilisateurConnecte != "")
            {
                nv_user.Content = RoleUtilisateur.UtilisateurNom;
                nv_user.Visibility = Visibility.Visible;
                nv_connexion.Visibility = Visibility.Collapsed;
                nv_statistique.Visibility = Visibility.Collapsed;
                nv_adherent.Visibility = Visibility.Collapsed;
                nv_deconnexion.Visibility = Visibility.Visible;
            }
            else if (RoleUtilisateur.Admin)
            {
                nv_user.Content = "Admin";
                nv_user.Visibility = Visibility.Visible;
                nv_connexion.Visibility = Visibility.Collapsed;
                nv_deconnexion.Visibility = Visibility.Visible;
                nv_statistique.Visibility = Visibility.Visible;
                nv_adherent.Visibility = Visibility.Visible;
            }
            else
            {
                nv_user.Visibility = Visibility.Collapsed;
                nv_deconnexion.Visibility = Visibility.Collapsed;
                nv_statistique.Visibility = Visibility.Collapsed;
                nv_adherent.Visibility = Visibility.Collapsed;
                nv_connexion.Visibility = Visibility.Visible;
            }

        }


    }
}
