﻿#pragma checksum "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D2CAA6C73058D992D176EAF0141B900"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SuperMinersCustomServiceSystem.MyControl;
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


namespace SuperMinersCustomServiceSystem.View.Controls.TradeSystem {
    
    
    /// <summary>
    /// VirtualShoppingItemListControl
    /// </summary>
    public partial class VirtualShoppingItemListControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgRecords;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddItem;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateItem;
        
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
            System.Uri resourceLocater = new System.Uri("/SuperMinersCustomServiceSystem;component/view/controls/tradesystem/virtualshoppi" +
                    "ngitemlistcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
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
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
            this.btnSearch.Click += new System.Windows.RoutedEventHandler(this.btnSearch_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgRecords = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnAddItem = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
            this.btnAddItem.Click += new System.Windows.RoutedEventHandler(this.btnAddItem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnUpdateItem = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\..\View\Controls\TradeSystem\VirtualShoppingItemListControl.xaml"
            this.btnUpdateItem.Click += new System.Windows.RoutedEventHandler(this.btnUpdateItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

