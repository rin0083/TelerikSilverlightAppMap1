﻿#pragma checksum "C:\Users\Administrator\Desktop\一千遍\TelerikSilverlightAppMap1\TelerikSilverlightAppMap1\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8EAB61937C62320916634BB3A3E2DC9B"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TelerikSilverlightAppMap1 {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ColumnDefinition c1;
        
        internal System.Windows.Controls.ColumnDefinition c2;
        
        internal Telerik.Windows.Controls.RadMap RadMap1;
        
        internal Telerik.Windows.Controls.Map.InformationLayer informationLayer;
        
        internal Telerik.Windows.Controls.Map.InformationLayer informationLayer2;
        
        internal Telerik.Windows.Controls.Map.InformationLayer informationLayer3;
        
        internal Telerik.Windows.Controls.Map.InformationLayer informationLayer4;
        
        internal Telerik.Windows.Controls.RadButton findRouteButton;
        
        internal Telerik.Windows.Controls.RadButton showAmbulance;
        
        internal Telerik.Windows.Controls.RadButton clearMark;
        
        internal Telerik.Windows.Controls.RadButton clearButton;
        
        internal System.Windows.Controls.TextBlock ErrorSummary;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TelerikSilverlightAppMap1;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.c1 = ((System.Windows.Controls.ColumnDefinition)(this.FindName("c1")));
            this.c2 = ((System.Windows.Controls.ColumnDefinition)(this.FindName("c2")));
            this.RadMap1 = ((Telerik.Windows.Controls.RadMap)(this.FindName("RadMap1")));
            this.informationLayer = ((Telerik.Windows.Controls.Map.InformationLayer)(this.FindName("informationLayer")));
            this.informationLayer2 = ((Telerik.Windows.Controls.Map.InformationLayer)(this.FindName("informationLayer2")));
            this.informationLayer3 = ((Telerik.Windows.Controls.Map.InformationLayer)(this.FindName("informationLayer3")));
            this.informationLayer4 = ((Telerik.Windows.Controls.Map.InformationLayer)(this.FindName("informationLayer4")));
            this.findRouteButton = ((Telerik.Windows.Controls.RadButton)(this.FindName("findRouteButton")));
            this.showAmbulance = ((Telerik.Windows.Controls.RadButton)(this.FindName("showAmbulance")));
            this.clearMark = ((Telerik.Windows.Controls.RadButton)(this.FindName("clearMark")));
            this.clearButton = ((Telerik.Windows.Controls.RadButton)(this.FindName("clearButton")));
            this.ErrorSummary = ((System.Windows.Controls.TextBlock)(this.FindName("ErrorSummary")));
        }
    }
}

