﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for FunnyUserControl.xaml
    /// </summary>
    public partial class FunnyUserControl : UserControl
    {
        public FunnyUserControl()
        {
            InitializeComponent();
        }

        public void AddEventHandlers()
        {
            this.controlGameRoulette.AddEventHandlers();
            this.controlGambleStone.AddEventHandlers();
        }

        public void RemoveEventHandlers()
        {
            this.controlGameRoulette.RemoveEventHandlers();
        }

    }
}
