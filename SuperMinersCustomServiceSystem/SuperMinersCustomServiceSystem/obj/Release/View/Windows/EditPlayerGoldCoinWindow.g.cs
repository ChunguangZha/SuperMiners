﻿#pragma checksum "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3E683712A78C49FDB2FA22B1DF22FFF0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SuperMinersWPF.MyControl;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace SuperMinersCustomServiceSystem.View.Windows {
    
    
    /// <summary>
    /// EditPlayerGoldCoinWindow
    /// </summary>
    public partial class EditPlayerGoldCoinWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserName;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCurrentGoldCoin;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbOper;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel panelInCharge;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SuperMinersWPF.MyControl.NumericTextBox numGoldCoinValue;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SuperMinersWPF.MyControl.NumericTextBox numGoldCoinChanged;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOK;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/SuperMinersCustomServiceSystem;component/view/windows/editplayergoldcoinwindow.x" +
                    "aml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.txtUserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtCurrentGoldCoin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cmbOper = ((System.Windows.Controls.ComboBox)(target));
            
            #line 22 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
            this.cmbOper.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbOper_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.panelInCharge = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.numGoldCoinValue = ((SuperMinersWPF.MyControl.NumericTextBox)(target));
            return;
            case 6:
            this.numGoldCoinChanged = ((SuperMinersWPF.MyControl.NumericTextBox)(target));
            return;
            case 7:
            this.btnOK = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
            this.btnOK.Click += new System.Windows.RoutedEventHandler(this.btnOK_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\View\Windows\EditPlayerGoldCoinWindow.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
