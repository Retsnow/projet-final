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
    public sealed partial class PageActivite : Page
    {
        public PageActivite()
        {
            this.InitializeComponent();
            gvActivites.ItemsSource = SingletonRequete.getListeActivite();
            if (RoleUtilisateur.Admin)
                (this.gvActivites.FindName("btnDelete") as Button).Visibility = Visibility.Visible;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            gvActivites.SelectedItem = activite;

            SingletonRequete.supprimer(activite.Nom);

            gvActivites.ItemsSource = SingletonRequete.getListeActivite();
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public static bool InscriptionAdherant(string idAdherent, string nomActivite)
        {
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select id from activite where id = " + idEntree;
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
            }
            //if ()
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return true;
        }

    }
}
