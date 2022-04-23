using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    internal class LegalEntityNameFormationBehavior : Behavior<TextBox>
    {
        private string _subname;
        

        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
            AssociatedObject.GotFocus += AssociatedObjectOnGotFocus;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (Equals(AssociatedObject, ShortNameTextBox))
            {
                CheckBox.IsChecked = false;
                return;
            }

            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (CheckBox.IsChecked != true) return;
            if (FormOfIncorporation?.FullForm == null || FormOfIncorporation.FullForm == FormOfIncorporation.DefaultValue) return;
            

            if (Equals(AssociatedObject, FullNameTextBox))
            {
                if (AssociatedObject.Text.Length <= FormOfIncorporation.FullForm.Length + 3 || 
                    AssociatedObject.CaretIndex < FormOfIncorporation.FullForm.Length + 3 || 
                    AssociatedObject.Text.Substring(AssociatedObject.Text.Length - 1, 1) != "\"" ||
                    !AssociatedObject.Text.ToLower().Contains(FormOfIncorporation.FullForm.ToLower())) 
                {
                    CheckBox.IsChecked = false;
                    AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
                    return;
                }

                _subname = AssociatedObject.Text.Substring(FormOfIncorporation.FullForm.Length + 2, AssociatedObject.Text.Length - 1 - (FormOfIncorporation.FullForm.Length + 2));
                ShortNameTextBox.Text = $"{FormOfIncorporation.ShortForm} \"{_subname}\"";
            }
        }

        private void AssociatedObjectOnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (FormOfIncorporation == null || FormOfIncorporation.FullForm == FormOfIncorporation.DefaultValue) return;

            if (string.IsNullOrWhiteSpace(AssociatedObject.Text))
            {
                if (AssociatedObject.Name == "FullNameTextBox")
                {
                    AssociatedObject.Text = $"{FormOfIncorporation.FullForm} \"\"";
                    AssociatedObject.CaretIndex = FormOfIncorporation.FullForm.Length + 2;
                }
                else if (AssociatedObject.Name == "ShortNameTextBox")
                {
                    AssociatedObject.Text = $"{FormOfIncorporation.ShortForm} \"\"";
                    AssociatedObject.CaretIndex = FormOfIncorporation.ShortForm.Length + 2;
                }
            }
        }



        protected override void OnDetaching()
        {
            
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
            AssociatedObject.GotFocus -= AssociatedObjectOnGotFocus;
        }

        #region DependencyProperties

        public static readonly DependencyProperty FormOfIncorporationProperty = DependencyProperty.Register(
            "FormOfIncorporation", typeof (FormOfIncorporation), typeof (LegalEntityNameFormationBehavior), new PropertyMetadata(default(FormOfIncorporation)));

        public FormOfIncorporation FormOfIncorporation
        {
            get { return (FormOfIncorporation) GetValue(FormOfIncorporationProperty); }
            set { SetValue(FormOfIncorporationProperty, value); }
        }

        public static readonly DependencyProperty FullNameTextBoxProperty = DependencyProperty.Register(
            "FullNameTextBox", typeof (WatermarkTextBox), typeof (LegalEntityNameFormationBehavior), new PropertyMetadata(default(WatermarkTextBox)));

        public WatermarkTextBox FullNameTextBox
        {
            get { return (WatermarkTextBox) GetValue(FullNameTextBoxProperty); }
            set { SetValue(FullNameTextBoxProperty, value); }
        }

        public static readonly DependencyProperty ShortNameTextBoxProperty = DependencyProperty.Register(
            "ShortNameTextBox", typeof (WatermarkTextBox), typeof (LegalEntityNameFormationBehavior), new PropertyMetadata(default(WatermarkTextBox)));

        public WatermarkTextBox ShortNameTextBox
        {
            get { return (WatermarkTextBox) GetValue(ShortNameTextBoxProperty); }
            set { SetValue(ShortNameTextBoxProperty, value); }
        }

        public static readonly DependencyProperty CheckBoxProperty = DependencyProperty.Register(
            "CheckBox", typeof (CheckBox), typeof (LegalEntityNameFormationBehavior), new PropertyMetadata(default(CheckBox)));

        public CheckBox CheckBox
        {
            get { return (CheckBox) GetValue(CheckBoxProperty); }
            set { SetValue(CheckBoxProperty, value); }
        }

        #endregion
    }
}