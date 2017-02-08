using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.MyControl
{
    /// <summary>
    /// Interaction logic for MyAutoScrollTextBlock.xaml
    /// </summary>
    public partial class MyAutoScrollTextBlock : UserControl
    {
        private Storyboard _move = new Storyboard();
        private bool _isMoving = false;
        private object _locker = new object();

        public MyAutoScrollTextBlock()
        {
            InitializeComponent();

            this._move.RepeatBehavior = RepeatBehavior.Forever;

            this.SizeChanged += new SizeChangedEventHandler(MyTextBlock_SizeChanged);
            this.MouseEnter += new MouseEventHandler(MyTextBlock_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MyTextBlock_MouseLeave);
        }

        private void MyTextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            //lock (this._locker)
            //{
            //    if (this._isMoving)
            //    {
            //        return;
            //    }

            //    if (this.text.ActualWidth > this.ActualWidth)
            //    {
            //        this._isMoving = true;
            //        this._move.Begin();
            //    }
            //}
        }

        private void MyTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            lock (this._locker)
            {
                //this._move.Stop();
                this._isMoving = false;
            }
        }

        private void MyTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, LayoutRoot.ActualWidth, LayoutRoot.ActualHeight);
            LayoutRoot.Clip = rect;

            //this.ResetMove();
        }

        private void ResetMove()
        {
            lock (this._locker)
            {

                this._move.Stop();
                Canvas.SetLeft(this.text, 0);
                this._move.Children.Clear();
                DoubleAnimationUsingKeyFrames ani = new DoubleAnimationUsingKeyFrames();
                ani.KeyFrames.Add(new LinearDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromMilliseconds(this.ActualWidth * 20),
                    Value = this.ActualWidth
                });
                ani.KeyFrames.Add(new DiscreteDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromMilliseconds(this.ActualWidth * 20 + this.text.ActualWidth * 20),
                    Value = -this.text.ActualWidth
                });
                Storyboard.SetTarget(ani, this.text);
                Storyboard.SetTargetProperty(ani, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                this._move.Children.Add(ani);

                //if (this.text.ActualWidth > this.ActualWidth)
                //{
                //}
                //else
                //{
                //    this._move.Stop();
                //    if (this.HorizontalContentAlignment == System.Windows.HorizontalAlignment.Center)
                //    {
                //        Canvas.SetLeft(this.text, (this.ActualWidth - this.text.ActualWidth) / 2);
                //    }
                //    else
                //    {
                //        Canvas.SetLeft(this.text, 0);
                //    }
                //}
            }
        }

        #region Public Properties

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register(
               "Text",
               typeof(string),
               typeof(MyAutoScrollTextBlock),
               new PropertyMetadata(null, (d, o) =>
               {
                   MyAutoScrollTextBlock lb = (MyAutoScrollTextBlock)d;
                   lb.text.Text = (string)o.NewValue;
                   lb.LayoutRoot.Height = lb.text.ActualHeight;

                   lb.ResetMove();
               }));

        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value); 
            }
        }

        #endregion
    }
}
