﻿using Loscate.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Loscate.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinDetailPage : ContentPage
    {
        private readonly PinDetailViewModel pinDetailViewModel;
        public PinDetailPage()
        {
            InitializeComponent();
            this.BindingContext = pinDetailViewModel = new PinDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pinDetailViewModel.OnAppearing();
        }
    }
}