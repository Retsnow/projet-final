﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class PageStatistique : Page
    {

        


        public ObservableCollection<string> Items { get; set; }  


        public PageStatistique()
        {
            this.InitializeComponent();


            lv_nbAdherentParActivite.ItemsSource = SingletonRequete.nbAdherentParActivite();
            tbk_nbAdherent.Text += " " + SingletonRequete.nbTotalAdherent();
            tbk_nbActivite.Text += " " + SingletonRequete.nbTotalActivite();

            lv_moyenneNoteParActivite.ItemsSource = SingletonRequete.moyenneNoteActivite();

            lv_maxSeance.ItemsSource = SingletonRequete.max_seance();
            lv_prixMoyenActivitePourChaqueParticipant.ItemsSource = SingletonRequete.prixMoyenActivitePourChaqueParticipant();
            lv_profit_activite.ItemsSource = SingletonRequete.profitActivite();
        }


    }
}
