using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using PRC.PacketBatchFiller.UserControls;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;

namespace PRC.PacketBatchFiller.Behavior
{
    public class NullBindingIfMaskNotFull : Behavior<MaskedTextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObjectOnPreviewKeyDown;
        }

        

        private void AssociatedObjectOnPreviewKeyDown(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            //var input = sender as MaskedTextBox;

            //if (AssociatedObject.IsMaskFull) return;
            ////if (input?.Value == null) return; 
            //if (input.Value != null && (DateTime) input.Value == default (DateTime)) return;
            
            //AssociatedObject.Value = default(DateTime);
            //AssociatedObject.Text = input.Text;


            //var bindingExpression = BindingOperations.GetBindingExpression(AssociatedObject, ValueRangeTextBox.ValueProperty);
            //if (bindingExpression == null) return;

            //var dataItem = bindingExpression.DataItem;
            //var type = dataItem.GetType();
            //var property = type.GetProperty(bindingExpression.ParentBinding.Path.Path);
            //property?.SetValue(bindingExpression.DataItem, default(DateTime), null);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObjectOnPreviewKeyDown;
        }
    }
}