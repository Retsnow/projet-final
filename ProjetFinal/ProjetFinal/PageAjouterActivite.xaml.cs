using Microsoft.UI;
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
    public sealed partial class PageAjouterActivite : Page
    {

        NavigationViewItem nv_activite;
        Activite activite;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            object[] objects = new object[2];

            if (e.Parameter is not null)
            {
                objects = (object[])e.Parameter;
                nv_activite = objects[0] as NavigationViewItem;
                if (objects.Count() == 2)
                    activite = objects[1] as Activite;
            }

            if (activite != null)
            {
                tbx_nom_activite.Text = activite.Nom;
                tbx_nom_activite.IsEnabled = false;
                tbx_cout_organisation.Text = activite.Cout_organisation.ToString();
                tbx_prix_vente.Text = activite.Prix_vente.ToString();

                foreach (var item in cbxCategorie.Items)
                    if ((item as Categorie).Id == activite.Id_categorie)
                        cbxCategorie.SelectedItem = item; 

            }
        }

        public PageAjouterActivite()
        {
            this.InitializeComponent();

            cbxCategorie.ItemsSource = SingletonRequete.getListeCategorie();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (activite != null)
                SingletonRequete.modifierActivite(tbx_nom_activite.Text, Convert.ToDouble(tbx_cout_organisation.Text), Convert.ToDouble(tbx_prix_vente.Text), (cbxCategorie.SelectedItem as Categorie).Id.ToString());
            else
                SingletonRequete.ajouterActivite(tbx_nom_activite.Text, Convert.ToDouble(tbx_cout_organisation.Text), Convert.ToDouble(tbx_prix_vente.Text), (cbxCategorie.SelectedItem as Categorie).Id.ToString());

            nv_activite.IsSelected = false;
            nv_activite.IsSelected = true;

        }
    }
}
