using System.Windows;
using PRC.PacketBatchFiller.Models.PersonsEntity;

namespace PRC.PacketBatchFiller.UserControls
{
    public partial class MaskedWatermarkedTextBoxWithLabelString
    {
        public MaskedWatermarkedTextBoxWithLabelString()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
            "LabelText", typeof(string), typeof(MaskedWatermarkedTextBoxWithLabelString), new FrameworkPropertyMetadata(default(string)));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(string), typeof(MaskedWatermarkedTextBoxWithLabelString), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty KeyboardTabIndexProperty = DependencyProperty.Register(
            "KeyboardTabIndex", typeof(int), typeof(MaskedWatermarkedTextBoxWithLabelString), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int KeyboardTabIndex
        {
            get { return (int)GetValue(KeyboardTabIndexProperty); }
            set { SetValue(KeyboardTabIndexProperty, value); }
        }

        public static readonly DependencyProperty MaskProperty = DependencyProperty.Register(
            "Mask", typeof (string), typeof (MaskedWatermarkedTextBoxWithLabelString), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Mask
        {
            get { return (string) GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        public static readonly DependencyProperty CheckValueProperty = DependencyProperty.Register(
            "CheckValue", typeof (string), typeof (MaskedWatermarkedTextBoxWithLabelString), new PropertyMetadata(default(string)));

        public string CheckValue
        {
            get { return (string) GetValue(CheckValueProperty); }
            set { SetValue(CheckValueProperty, value); }
        }
    }
}
