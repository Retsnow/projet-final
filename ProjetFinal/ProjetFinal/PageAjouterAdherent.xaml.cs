using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
        Adherent adherent;

        public PageAjouterAdherent()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                adherent = (Adherent)e.Parameter;
            }

        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            string nom;
            string prenom;
            string adresse;
            DateOnly date_denaissance;

            nom = tbx_nom_adherent.Text;
            prenom = tbx_prenom_adherent.Text;
            adresse = tbx_adresse.Text;
            date_denaissance = DateOnly.Parse(date_naissance.SelectedDate.Value.ToString("yyyy-MM-dd"));

            SingletonRequete.ajouterAdherent(nom, prenom, adresse, date_denaissance);

            // Message de validation
        }
    }
}
