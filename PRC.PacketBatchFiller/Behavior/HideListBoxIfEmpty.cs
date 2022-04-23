using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PRC.PacketBatchFiller.Behavior
{
    class HideListBoxIfEmpty : TriggerAction<UIElement>
    {
        public static readonly DependencyProperty ListBoxProperty = DependencyProperty.Register(
            "ListBox", typeof (ListBox), typeof (HideListBoxIfEmpty), new PropertyMetadata(default(ListBox)));

        public ListBox ListBox
        {
            get { return (ListBox) GetValue(ListBoxProperty); }
            set { SetValue(ListBoxProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (ListBox.Items.Count == 1)
            {
                ListBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                ListBox.Visibility = Visibility.Visible;
            }
        }
    }
}
