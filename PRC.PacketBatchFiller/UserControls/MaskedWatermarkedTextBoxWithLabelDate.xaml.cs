using System;
using System.Windows;


namespace PRC.PacketBatchFiller.UserControls
{
    public partial class MaskedWatermarkedTextBoxWithLabelDate
    {
        public MaskedWatermarkedTextBoxWithLabelDate()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }


        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
            "LabelText", typeof (string), typeof (MaskedWatermarkedTextBoxWithLabelDate), new PropertyMetadata(default(string)));

        public string LabelText
        {
            get { return (string) GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty KeyboardTabIndexProperty = DependencyProperty.Register(
            "KeyboardTabIndex", typeof (int), typeof (MaskedWatermarkedTextBoxWithLabelDate), new PropertyMetadata(default(int)));

        public int KeyboardTabIndex
        {
            get { return (int) GetValue(KeyboardTabIndexProperty); }
            set { SetValue(KeyboardTabIndexProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof (DateTime?), typeof (MaskedWatermarkedTextBoxWithLabelDate), new FrameworkPropertyMetadata(default(DateTime?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public DateTime? Value
        {
            get { return (DateTime?) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
