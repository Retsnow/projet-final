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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouterActivite : Page
    {

        NavigationViewItem nv_activite;

        internal class Categorie
        {
            private  int id;

            public  int Id
            {
                get { return id; }
                set { id = value; }
            }

            private  string nom;

            public  string Nom
            {
                get { return nom; }
                set { nom = value; }
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                nv_activite = (NavigationViewItem)e.Parameter;
            }

        }

        public PageAjouterActivite()
        {
            this.InitializeComponent();

        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            SingletonRequete.ajouterActivite(tbx_nom_activite.Text, Convert.ToDouble(tbx_cout_organisation.Text), Convert.ToDouble(tbx_prix_vente.Text), (cbxCategorie.SelectedItem as Categorie).Id.ToString());
            nv_activite.IsSelected = true;

        }
    }
}
