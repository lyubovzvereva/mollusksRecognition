using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
//using Emgu.CV;
//using Emgu.CV.UI;
//using Emgu.CV.Structure;
using System.IO;
//using System.Data.SQLite;
using System.Reflection;

//using System.Drawing;

namespace WpfDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
   
   
    public partial class MainWindow : Window //, Singleton<MainWindow>
    {
        //private List<Point> list;
        private Point crown_points;
        private List<Point> stripes_points;
        private List<Point> mollusk_points;


        private TransformGroup trGrp;
        //private SkewTransform trSkw;
        private RotateTransform trRot;
        private TranslateTransform trTns;
        private ScaleTransform trScl;
        public static MainWindow itis;

        //public Point crop;

        public MainWindow()
        {
            InitializeComponent();
            trScl = new ScaleTransform(1, 1);
            //trSkw = new SkewTransform(0, 0);
            trRot = new RotateTransform(0);
            trTns = new TranslateTransform(0, 0);
            itis = this;

            trGrp = new TransformGroup();
            trGrp.Children.Add(trRot);
            //trGrp.Children.Add(trSkw);
            trGrp.Children.Add(trTns);
            trGrp.Children.Add(trScl);
            Application.Current.MainWindow = this;
            //Molusk_image.Width = Molusk_image.Source.Width;
            //Molusk_image.Height = Molusk_image.Source.Height;
            //Molusk_image.Stretch = Stretch.Uniform;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = new SplineEditor();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = null;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = new RectEditor();
        }

        private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            drawHost.clear();
        }

        private void background_Checked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = new FillEditor(new Pen(Brushes.SpringGreen, 10));
        }

        private void background_Unchecked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = null;
        }

        private void foreground_Checked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = new FillEditor(new Pen(Brushes.LightPink, 10));
        }

        private void foreground_Unchecked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = null;
        }

        private void drawHost_MouseDown(object sender, MouseButtonEventArgs e)
        {
            drawHost.CaptureMouse();
        }

        private void drawHost_MouseUp(object sender, MouseButtonEventArgs e)
        {
            drawHost.ReleaseMouseCapture();
        }
       
        private void load_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog open_dialog = new OpenFileDialog();
            Nullable<bool> result = open_dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    BitmapImage myBitmapImage = new BitmapImage(new Uri(open_dialog.FileName, UriKind.Absolute));


                    Molusk_image.Visibility = System.Windows.Visibility.Visible;
                    //convert to grayscale

                    FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
                    newFormatedBitmapSource.BeginInit();
                    newFormatedBitmapSource.Source = myBitmapImage;
                    newFormatedBitmapSource.DestinationFormat = PixelFormats.Gray8;
                    newFormatedBitmapSource.EndInit();


                    double dpi = 96;
                    int width = newFormatedBitmapSource.PixelWidth;
                    int height = newFormatedBitmapSource.PixelHeight;

                    int stride = width; // 4 bytes per pixel
                    byte[] pixelData = new byte[stride * height];
                    newFormatedBitmapSource.CopyPixels(pixelData, stride, 0);
                    BitmapSource bmpSource = BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Gray8, null, pixelData, stride);

                    Molusk_image.Source = bmpSource;


                    Molusk_image.RenderTransform = trGrp;


                }
                catch
                {

                    MessageBox.Show("произошла ошибка! Это не изображение! Попробуйте снова.");
                }
            }

        }
        private void weight(byte[] bits, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int s = bits[i * width + j];

                }
            }
        }
        private void getPoints_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mollusk_points = drawHost.getPoints();
                MessageBox.Show(mollusk_points.Count.ToString());
            }
            catch
            {
                MessageBox.Show("Пожалуйста, отметьте область");
            }
        }

        //private void GrabCut_Click(object sender, RoutedEventArgs e)
        //{

        //    Image<Bgr, byte> src = new Image<Bgr, byte>("C:\\Users\\Lyubov\\Pictures\\диплом\\цель.jpg");

        //    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(5, 5, src.Width - 5, src.Height - 5);
        //    Image<Gray, byte> res = src.GrabCut(rect, 3);

        //    MemoryStream ms = new MemoryStream();

        //    res.Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //    BmpBitmapDecoder dec = new BmpBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
        //    BitmapFrame frame = dec.Frames[0];
        //    PngBitmapEncoder enc = new PngBitmapEncoder();
        //    enc.Frames.Add(frame);
        //    using (FileStream fs = new FileStream("result.png", FileMode.Create))
        //    {
        //        enc.Save(fs);
        //    }
        //}

        private void scissors_Click(object sender, RoutedEventArgs e)
        {
            //get byte[]
            BitmapSource img = (BitmapSource)Molusk_image.Source;//я не понимаю сути этого!
            int width = img.PixelWidth;
            int height = img.PixelHeight;
            int stride = width * (img.Format.BitsPerPixel + 7) / 8;

            byte[] bits = new byte[height * stride];
            img.CopyPixels(bits, stride, 0);

            int bpp = img.Format.BitsPerPixel / 8;
            byte[,] bitmap_im = new byte[width,height];

            //делаем двумерный массив!!!!!!
            //------------------------->x
            // |
            // |
            // |
            // |
            // |
            // |
            //\ /
            // -
            // y
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    bitmap_im[i, j] = bits[i * bpp + stride * j];
                }
            }
           // MessageBox.Show("Преобразование в двумерный массив завершено");
           

            //наконец-то заполняем граф

            drawHost.CurrentEditor = new ScissorEditor(bitmap_im);
            MessageBox.Show("Я закончил");
            

        }

        private void Molusk_image_MouseMove(object sender, MouseEventArgs e)
        {
            //h_l.Text = e.GetPosition(drawHost).ToString();
        }

        private void Rotate_left_Click(object sender, RoutedEventArgs e)
        {
            trRot.Angle -= 2;
            trRot.CenterX = Molusk_image.ActualWidth/2;
            trRot.CenterY = Molusk_image.ActualHeight/2;
        }

        private void Rotate_right_Click(object sender, RoutedEventArgs e)
        {
            trRot.Angle += 2;
            trRot.CenterX = Molusk_image.ActualWidth / 2;
            trRot.CenterY = Molusk_image.ActualHeight / 2;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            //1. scale
            //trScl.ScaleX = 0.5;
            try
            {
                System.Windows.Media.PointCollection pts = new System.Windows.Media.PointCollection(mollusk_points);
                Polygon pol = new Polygon();
                pol.Points = pts;
                TransformGroup gr = new TransformGroup();
                gr.Children.Add(trRot);
                pol.RenderTransform = gr;
                //conteiner.Children.Add(pol);
                Parametrs param = new Parametrs();
                double sq = param.scale(pol.Points);
                this.square.Text = sq.ToString();
                this.h_l.Text = (param.heid / param.wid).ToString();
                this.vertex.Text = (crown_points.X - param.minx).ToString();
                this.vertexy.Text = (crown_points.Y - param.miny).ToString();
                this.count.Text = param.stripes(stripes_points).ToString();
            }
            catch
            {
            }
            
        }

        private void Invert_Click(object sender, RoutedEventArgs e)
        {
            if (trScl.ScaleX == 1)
            {
                trScl.ScaleX = -1;
                trTns.X -= Molusk_image.ActualWidth;
            }
            else
            {
                trScl.ScaleX = 1;
                trTns.X += Molusk_image.ActualWidth;
            }
            //trScl.ScaleY = 1;
            
        }

        private void Invert1_Click(object sender, RoutedEventArgs e)
        {
            //trScl.ScaleX = 1;
            if (trScl.ScaleY == 1)
            {
                trScl.ScaleY = -1;
                trTns.Y -= Molusk_image.ActualHeight;
            }
            else
            {
                trScl.ScaleY = 1;
                trTns.Y += Molusk_image.ActualHeight;
            }
        }

       

        private void Crown_Checked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = new PointEditor(new Pen(Brushes.Indigo, 10));
        }

        private void Crown_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                crown_points = drawHost.getPoints()[0];
                MessageBox.Show(crown_points.ToString());
            }
            catch
            {
                
            }
            drawHost.CurrentEditor = null;
        }

        private void Stripes_Checked(object sender, RoutedEventArgs e)
        {
            drawHost.CurrentEditor = new PointEditor(new Pen(Brushes.OldLace, 10));
        }

        private void Stripes_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                stripes_points = drawHost.getPoints();
                MessageBox.Show(stripes_points.Count.ToString());
            }
            catch
            {
               
            }
            drawHost.CurrentEditor = null;
          
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            DB win = new DB();
            win.Show();
            
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //string s = this.front_rear_end.Text;
            ////this.front_rear_end.
            Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
            double dx = m.M11;
            double dy = m.M22;
           double m_dpi = 96.0 * dx;
            MessageBox.Show(m_dpi.ToString());
            //MessageBox.Show(Molusk_image.ActualHeight.ToString());
        }

    }
}
