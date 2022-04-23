using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Services.Watermark
{
    public static class WatermarkService
    {
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark",
            typeof (object),
            typeof (WatermarkService),
            new FrameworkPropertyMetadata(null, OnWatermarkChanged));

        //#region Private Fields

        //private static readonly Dictionary<object, ItemsControl> itemsControls = new Dictionary<object, ItemsControl>();

        //#endregion

        public static object GetWatermark(DependencyObject d)
        {
            return d.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject d, object value)
        {
            d.SetValue(WatermarkProperty, value);
        }

        private static bool _gotFocus = true;


        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Control) d;
            control.Loaded += ControlOnLostKeyboardFocus;
            

            if (d is TextBox || d is PasswordBox)
            {
                control.GotKeyboardFocus += ControlOnGotKeyboardFocus;
                control.LostKeyboardFocus += ControlOnLostKeyboardFocus;
                ((TextBoxBase) d).TextChanged += TextBoxOnTextChanged;
                ((TextBoxBase)d).SelectionChanged += OnSelectionChanged;
            }

           

            //else if (d is ComboBox)
            //{
            //    control.GotKeyboardFocus += ControlOnGotKeyboardFocus;
            //    control.LostKeyboardFocus += ControlOnLostKeyboardFocus;
            //    (d as ComboBox).SelectionChanged += SelectionChanged;
            //}
            //else if (d is ItemsControl)
            //{
            //    ItemsControl i = (ItemsControl) d;

            //    // for Items property  
            //    i.ItemContainerGenerator.ItemsChanged += ItemsChanged;
            //    itemsControls.Add(i.ItemContainerGenerator, i);

            //    // for ItemsSource property  
            //    DependencyPropertyDescriptor prop = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, i.GetType());
            //    prop.AddValueChanged(i, ItemsSourceChanged);
            //}
        }

        
        private static void OnSelectionChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            if (string.IsNullOrEmpty(((MaskedTextBox)sender).GetRawText()) && (((MaskedTextBox)sender).CaretIndex != 0))
            {
                ((MaskedTextBox)sender).CaretIndex = 0;
            }
        }
        
        private static void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var c = (Control) sender;

            if (ShouldShowWatermark(c))
            {
                ShowWatermark(c);
            }
            else RemoveWatermark(c);
        }


        //private static void SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var control = (Control) sender;

        //    if (ShouldShowWatermark(control)) ShowWatermark(control);
        //    else RemoveWatermark(control);
        //}

        #region Event Handlers

        private static void ControlOnGotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            _gotFocus = true;
            var control = (Control) sender;

            if (ShouldShowWatermark(control)) ShowWatermark(control);
        }

        private static void ControlOnLostKeyboardFocus(object sender, RoutedEventArgs e)
        {
            _gotFocus = false;
            var control = (Control)sender;

            if (ShouldShowWatermark(control)) ShowWatermark(control);
        }

        //private static void ItemsSourceChanged(object sender, EventArgs e)
        //{
        //    var c = (ItemsControl) sender;
        //    if (c.ItemsSource != null)
        //    {
        //        if (ShouldShowWatermark(c)) ShowWatermark(c);
        //        else RemoveWatermark(c);
        //    }
        //    else ShowWatermark(c);
        //}

        //private static void ItemsChanged(object sender, ItemsChangedEventArgs e)
        //{
        //    ItemsControl control;
        //    if (itemsControls.TryGetValue(sender, out control))
        //    {
        //        if (ShouldShowWatermark(control)) ShowWatermark(control);
        //        else RemoveWatermark(control);
        //    }
        //}

        #endregion

        #region Helper Methods

        private static void ShowWatermark(UIElement control)
        {
            var layer = AdornerLayer.GetAdornerLayer(control);
            var opacity = _gotFocus ? 0.5 : 1;
            var adorner = new WatermarkAdorner(control, GetWatermark(control), opacity);

            // layer could be null if control is no longer in the visual tree
            layer?.Add(adorner);

            

            
        }

        private static void RemoveWatermark(UIElement control)
        {
            var layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null)
            {
                var adorners = layer.GetAdorners(control);
                if (adorners == null)
                {
                    return;
                }

                foreach (Adorner adorner in adorners)
                {
                    if (adorner is WatermarkAdorner)
                    {
                        adorner.Visibility = Visibility.Hidden;
                        layer.Remove(adorner);
                    }
                }
            }
            ((MaskedTextBox)control).Foreground = SystemColors.WindowTextBrush;
            ((MaskedTextBox)control).SelectionBrush = SystemColors.HighlightBrush;
            
        }

        private static bool ShouldShowWatermark(Control c)
        {
            var comboBox = c as ComboBox;
            if (comboBox != null) return comboBox.SelectedItem == null;

            var passwordBox = c as PasswordBox;
            if (passwordBox != null) return passwordBox.Password == string.Empty;

            var itemsControl = c as ItemsControl;
            if (itemsControl != null) return itemsControl.Items.Count == 0;

            var maskedTextBox = c as MaskedTextBox;
            if (maskedTextBox != null) return maskedTextBox.GetRawText() == string.Empty;

            if (c is TextBoxBase) return ((TextBox) c).Text == string.Empty;

            return false;
        }

        #endregion
    }
}