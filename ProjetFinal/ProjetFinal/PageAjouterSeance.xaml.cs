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
    public sealed partial class PageAjouterSeance : Page
    {

        NavigationViewItem nv_activite;
        Activite activite;
        Seance seance;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            object[] objects = new object[2];

            if (e.Parameter is not null)
            {
                objects = (object[])e.Parameter;
                nv_activite = objects[0] as NavigationViewItem;
                if (objects.Count() > 1)
                    activite = objects[1] as Activite;
                if (objects.Count() > 2)
                    seance = objects[2] as Seance;
            }

            if (seance != null)
            {
                idCategorie.Value = seance.Id_categorie;
                txtActivite.Text = seance.Nom_activite;
                calendarPicker.Date = new DateTimeOffset(seance.Date);
                timePickerHeure.Time = seance.Heure.ToTimeSpan();
                nbPlaceDisponible.Value = seance.Nb_place_disponible;
            }
            else if(activite != null)
            {
                idCategorie.Value = activite.Id_categorie;
                txtActivite.Text = activite.Nom;
            }
        }

        public PageAjouterSeance()
        {
            this.InitializeComponent();
            calendarPicker.MinDate = DateTimeOffset.Now;
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (seance != null)
                SingletonRequete.modifierSeance(seance.Id, seance.Date, seance.Heure.ToTimeSpan(), seance.Nb_place_disponible, seance.Nom_activite, seance.Id_categorie);
            else
                SingletonRequete.ajouterSeance(seance.Date, seance.Heure.ToTimeSpan(), seance.Nb_place_disponible, seance.Nom_activite, seance.Id_categorie);

            nv_activite.IsSelected = false;
            nv_activite.IsSelected = true;
        }
    }
}
