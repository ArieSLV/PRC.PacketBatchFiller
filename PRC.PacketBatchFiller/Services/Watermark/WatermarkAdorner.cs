using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Services.Watermark
{
    internal class WatermarkAdorner : Adorner
    {
        #region Private Fields

        private readonly ContentPresenter _contentPresenter;

        #endregion

        #region Constructor

        public WatermarkAdorner(UIElement adornedElement, object watermark, double opacity) : base(adornedElement)
        {
            ((MaskedTextBox)Control).Foreground = SystemColors.WindowBrush;
            ((MaskedTextBox)Control).SelectionBrush = SystemColors.WindowBrush;


            var feWatermark = watermark as FrameworkElement;
            if (feWatermark != null && feWatermark.DataContext == null)
            {
                feWatermark.DataContext = Control.DataContext;
            }

            IsHitTestVisible = false;

            _contentPresenter = new ContentPresenter
            {
                Content = watermark,
                Opacity=opacity,
                Margin =
                    new Thickness(Control.Margin.Left + Control.Padding.Left, Control.Margin.Top + Control.Padding.Top,
                        0, 0)
            };


            if (Control is ItemsControl && !(Control is ComboBox))
            {
                _contentPresenter.VerticalAlignment = VerticalAlignment.Center;
                _contentPresenter.HorizontalAlignment = HorizontalAlignment.Center;
            }

            // Hide the control adorner when the adorned element is hidden
            var binding = new Binding("IsVisible")
            {
                Source = adornedElement,
                Converter = new BooleanToVisibilityConverter()
            };

            SetBinding(VisibilityProperty, binding);

            


        }

        #endregion

        #region Protected Properties

        protected override int VisualChildrenCount => 2;

        #endregion

        #region Private Properties

        private Control Control
        {
            get { return (Control) AdornedElement; }
        }

        #endregion

        #region Protected Overrides

        protected override Visual GetVisualChild(int index)
        {
            return _contentPresenter;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            // Here's the secret to getting the adorner to cover the whole control
            _contentPresenter.Measure(Control.RenderSize);
            InvalidateMeasure();
            return Control.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _contentPresenter.Arrange(new Rect(finalSize));
            return finalSize;
        }

        #endregion
    }
}