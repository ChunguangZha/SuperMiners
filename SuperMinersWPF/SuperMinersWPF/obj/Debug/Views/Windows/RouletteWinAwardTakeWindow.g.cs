﻿#pragma checksum "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0EB8D2E41C6A7B604C35533DB6B4F58C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SuperMinersWPF.Views.Windows {
    
    
    /// <summary>
    /// RouletteWinAwardTakeWindow
    /// </summary>
    public partial class RouletteWinAwardTakeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblInfo1;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtInfo1;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblInfo2;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtInfo2;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTake;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SuperMinersWPF;component/views/windows/roulettewinawardtakewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblInfo1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.txtInfo1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.lblInfo2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtInfo2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnTake = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\Views\Windows\RouletteWinAwardTakeWindow.xaml"
            this.btnTake.Click += new System.Windows.RoutedEventHandler(this.btnTake_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

