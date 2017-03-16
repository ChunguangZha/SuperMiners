using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SuperMinersWPF.MyControl
{
    public class MyTreeView : TreeView
    {
        public event EventHandler GetContainerFinished;

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MyTreeViewItem();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down ||
                e.Key == Key.Right || e.Key == Key.Left ||
                e.Key == Key.PageUp || e.Key == Key.PageDown ||
                e.Key == Key.Home || e.Key == Key.End)
            {
                e.Handled = true;
                return;
            }

            base.OnKeyDown(e);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            if (GetContainerFinished != null)
            {
                GetContainerFinished(this, null);
            }
        }
    }

    public class MyTreeViewItem : TreeViewItem
    {
        public MyTreeViewItem() :
            base()
        {
            Binding bindIsExpanded = new Binding("IsExpanded");
            bindIsExpanded.Mode = BindingMode.TwoWay;
            SetBinding(TreeViewItem.IsExpandedProperty, bindIsExpanded);

            Binding bindVisible = new Binding("Visible");
            SetBinding(TreeViewItem.VisibilityProperty, bindVisible);            
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MyTreeViewItem();
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down ||
                e.Key == Key.Right || e.Key == Key.Left ||
                e.Key == Key.PageUp || e.Key == Key.PageDown ||
                e.Key == Key.Home || e.Key == Key.End)
            {
                e.Handled = true;
                return;
            }

            base.OnKeyDown(e);
        }
    }
}
