using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Utilities;
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
    public sealed partial class PageAjouterAdherent : Page
    {
        MainWindow window;
        Adherent adherent;

        public PageAjouterAdherent()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimeOnly time = new TimeOnly(0, 0);
            object[] objects = new object[2];


            if (e.Parameter is not null)
            {
                objects = (object[])e.Parameter;
                adherent = objects[0] as Adherent;
                if (objects.Count() == 2)
                    window = objects[1] as MainWindow;
            }

            if (adherent != null)
            {
                tbx_id_adherent.Visibility = Visibility.Visible;
                tbx_id_adherent.Text = adherent.Id;
                tbx_id_adherent.IsEnabled = false;

                tbx_age_adherent.Visibility = Visibility.Visible;
                tbx_age_adherent.Text = adherent.Age.ToString();
                tbx_age_adherent.IsEnabled = false;

                tbx_nom_adherent.Text = adherent.Nom;
                tbx_prenom_adherent.Text = adherent.Prenom;
                tbx_adresse.Text = adherent.Adresse;
                date_naissance.SelectedDate = adherent.Date_naissance.ToDateTime(time);

                

            }
            else
            {
                tbx_id_adherent.Visibility = Visibility.Collapsed;
                tbx_age_adherent.Visibility = Visibility.Collapsed;
            }

        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            string id;
            string nom;
            string prenom;
            string adresse;
            DateOnly date_denaissance;

            id = tbx_id_adherent.Text;
            nom = tbx_nom_adherent.Text;
            prenom = tbx_prenom_adherent.Text;
            adresse = tbx_adresse.Text;
            date_denaissance = DateOnly.Parse(date_naissance.SelectedDate.Value.ToString("yyyy-MM-dd"));


            if (adherent != null)
            {
                tbk_confirmation.Text = SingletonRequete.modifierAdherent(id, nom, prenom, adresse, date_denaissance);
            }
            else
            {
                tbk_confirmation.Text = SingletonRequete.ajouterAdherent(nom, prenom, adresse, date_denaissance);
            }


            tbk_confirmation.Visibility = Visibility.Visible;
            window.mainFrame.Navigate(typeof(PageAdherent), window);

        }
    }
}
