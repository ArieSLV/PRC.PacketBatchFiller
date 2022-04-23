using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    internal class DynamicMaskExpand : Behavior<MaskedTextBox>
    {
        private static string _newString;

        public static readonly DependencyProperty CheckValueProperty = DependencyProperty.Register(
            "CheckValue", typeof(string), typeof(DynamicMaskExpand), new PropertyMetadata(default(string)));

        public string CheckValue
        {
            get { return (string)GetValue(CheckValueProperty); }
            set { SetValue(CheckValueProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += AssociatedObjectOnPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (CheckValue == null)
            {
                if (AssociatedObject?.Value == null || AssociatedObject.SelectedText != string.Empty) return;

                var request = new TraversalRequest(FocusNavigationDirection.Next) { Wrapped = true };
                AssociatedObject.MoveFocus(request);
                return;
            }

            switch (CheckValue)
            {
                case "Российская Федерация":
                case "Паспорт гражданина РФ":
                case "Загранпаспорт гражданина РФ":
                    if (AssociatedObject?.Value == null || AssociatedObject.SelectedText != string.Empty) return;

                    var request = new TraversalRequest(FocusNavigationDirection.Next) {Wrapped = true};
                    AssociatedObject.MoveFocus(request);
                    break;
            }
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (AssociatedObject.Value != null)
            {
                switch (CheckValue)
                {
                    case "Российская Федерация":
                    case "Паспорт гражданина РФ":
                    case "Загранпаспорт гражданина РФ":
                        break;
                    default:
                        AssociatedObject.PreviewTextInput -= AssociatedObjectOnPreviewTextInput;
                        switch (keyEventArgs.Key)
                        {
                            case Key.Back:
                            {
                                var selectionStart = AssociatedObject.SelectionStart;
                                var selectionLenght = AssociatedObject.SelectionLength;
                                var caretIndex = AssociatedObject.CaretIndex;
                                var rawText = AssociatedObject.GetRawText();
                                var tmpMask = AssociatedObject.Mask;

                                if (selectionLenght == 0 && caretIndex != 0)
                                {
                                    AssociatedObject.Value = string.Empty;
                                    AssociatedObject.Mask = tmpMask.Substring(0, tmpMask.Length - 1);
                                    AssociatedObject.Value =
                                        $"{rawText.Substring(0, caretIndex - 1)}{rawText.Substring(caretIndex, rawText.Length - caretIndex)}";
                                    AssociatedObject.CaretIndex = caretIndex - 1;
                                }
                                else if (selectionLenght > 0)
                                {
                                    AssociatedObject.Value = string.Empty;
                                    AssociatedObject.Mask = tmpMask.Substring(0, tmpMask.Length - selectionLenght);
                                    AssociatedObject.Value =
                                        $"{rawText.Substring(0, selectionStart)}{rawText.Substring(selectionStart + selectionLenght, rawText.Length - selectionStart - selectionLenght)}";
                                    AssociatedObject.CaretIndex = selectionStart;
                                }
                            }
                                keyEventArgs.Handled = true;
                                break;
                            case Key.Delete:
                            {
                                var selectionStartOnDelete = AssociatedObject.SelectionStart;
                                var selectionLenghtOnDelete = AssociatedObject.SelectionLength;
                                var caretIndexOnDelete = AssociatedObject.CaretIndex;
                                var rawTextOnDelete = AssociatedObject.GetRawText();
                                var tmpMaskOnDelete = AssociatedObject.Mask;

                                if (selectionLenghtOnDelete == 0 && caretIndexOnDelete != rawTextOnDelete.Length)
                                {
                                    AssociatedObject.Value = string.Empty;
                                    AssociatedObject.Mask = tmpMaskOnDelete.Substring(0, tmpMaskOnDelete.Length - 1);
                                    AssociatedObject.Value =
                                        $"{rawTextOnDelete.Substring(0, caretIndexOnDelete)}{rawTextOnDelete.Substring(caretIndexOnDelete + 1, rawTextOnDelete.Length - caretIndexOnDelete - 1)}";
                                    AssociatedObject.CaretIndex = caretIndexOnDelete;
                                }
                                else if (selectionLenghtOnDelete > 0)
                                {
                                    AssociatedObject.Value = string.Empty;
                                    AssociatedObject.Mask = tmpMaskOnDelete.Substring(0,
                                        tmpMaskOnDelete.Length - selectionLenghtOnDelete);
                                    AssociatedObject.Value =
                                        $"{rawTextOnDelete.Substring(0, selectionStartOnDelete)}{rawTextOnDelete.Substring(selectionStartOnDelete + selectionLenghtOnDelete, rawTextOnDelete.Length - selectionStartOnDelete - selectionLenghtOnDelete)}";
                                    AssociatedObject.CaretIndex = selectionStartOnDelete;
                                }
                            }
                                keyEventArgs.Handled = true;
                                break;
                        }

                        AssociatedObject.PreviewTextInput += AssociatedObjectOnPreviewTextInput;
                        break;
                }
            }
        }

        
        private void AssociatedObjectOnPreviewTextInput(object sender, TextCompositionEventArgs textCompositionEventArgs)
        {
            
            if (textCompositionEventArgs.Text == "\r") return; //Не воспринимать нажатие на Enter

            _newString = AssociatedObject.GetRawText();

            if (AssociatedObject.IsMaskFull)
            {
                var rawText = AssociatedObject.GetRawText();
                var caretIndex = AssociatedObject.CaretIndex;

                switch (CheckValue)
                {
                    case "Российская Федерация":
                    case "Паспорт гражданина РФ":
                    case "Загранпаспорт гражданина РФ":
                        break;
                    default:
                        AssociatedObject.Mask += "A";
                        AssociatedObject.Value =
                            $"{rawText.Substring(0, caretIndex)}{textCompositionEventArgs.Text}{rawText.Substring(caretIndex, rawText.Length - caretIndex)}";
                        AssociatedObject.CaretIndex = caretIndex + 1;

                        break;
                }
            }
        }


        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= AssociatedObjectOnPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }
    }
}