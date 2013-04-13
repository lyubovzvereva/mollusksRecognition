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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using System.Data;


namespace WpfDrawing
{
    /// <summary>
    /// Interaction logic for DataEditWindow.xaml
    /// </summary>
    public partial class DataEditWindow : Window
    {
        string m_bitmap_file;
        public DataEditWindow()
        {
            InitializeComponent();
            this.preview.Source = MainWindow.itis.Molusk_image.Source;
            this.txtInitShell.Text = MainWindow.itis.init_shell.Text;
            this.txtfront.Text = MainWindow.itis.front_rear_end.Text;
            this.txtAge.Text = MainWindow.itis.age.Text;
            this.txtLength.Text = MainWindow.itis.length.Text;
            this.txtSculpture.Text = MainWindow.itis.sculpture.Text;
            this.txtHL.Text = MainWindow.itis.h_l.Text;
            this.txtVertex.Text = MainWindow.itis.vertex.Text;
            this.txtVertexy.Text = MainWindow.itis.vertexy.Text;
            this.txtLines.Text = MainWindow.itis.count.Text;
            this.txtSquare.Text = MainWindow.itis.square.Text;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == true)
            {
                m_bitmap_file = ofd.FileName;
                preview.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          
            SqliteDataProvider dataProvider = UIManager.man.Provider;
            //string path="C:\\Users\\Lyubov\\Desktop\\WpfDrawing\\WpfDrawing\\WpfDrawing\\bin\\Debug\\mollusk.s3db";
            string species, init_shell, front_rear, age, length, sculpture,genus;
            species = this.txtSpecies.Text;
            init_shell = this.txtInitShell.Text;
            front_rear = this.txtfront.Text;
            age = txtAge.Text;
            length = txtLength.Text;
            sculpture = txtSculpture.Text;
            genus=txtGenus.Text;

            double hl, vertexx, vertexy, lines, square;
            double.TryParse(txtHL.Text,out hl);
            double.TryParse(txtVertex.Text, out vertexx);
            double.TryParse(txtVertexy.Text, out vertexy);
            double.TryParse(txtLines.Text, out lines);
            double.TryParse(txtSquare.Text, out square);
          
            long photo_id=0;
            bool therephoto = false;
            if (this.preview.Source != null)
            {
                therephoto = true;
                //записать в таблицу Photo (photo_id, square, vertex_x, vertex_y, furrow, bitmap)
                string sql = string.Format("INSERT INTO Photo (square, vertex_x, vertex_y, furrow, bitmap) VALUES ({0}, {1}, {2}, {3}, null)",
                    square.ToString(CultureInfo.InvariantCulture),
                    vertexx.ToString(CultureInfo.InvariantCulture),
                    vertexy.ToString(CultureInfo.InvariantCulture),
                    lines.ToString(CultureInfo.InvariantCulture));
                dataProvider.ExecuteNonQuery(sql);
                photo_id = dataProvider.GetLastRowID();
                using (FileStream fs = new FileStream(m_bitmap_file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    //Если файл больше 2 Гб то будет попа.
                    Stream stream = dataProvider.WriteData(photo_id, "Photo", "bitmap", (int)fs.Length);
                    byte[] buffer = new byte[0xFFFF];
                    int readed = 0;
                    do
                    {
                        readed = fs.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, readed);
                    }
                    while (readed > 0);
                    stream.Close();
                }
            }
            
            long species_id;
            string sql_to_species = string.Format("SELECT species_id FROM Species WHERE species_name='{0}'",
                species);
            DataTable data = dataProvider.ExecuteQuery(sql_to_species);
            if (data.Rows.Count != 0)
            {
                //записать айди вида
                species_id = (int)data.Rows[0].ItemArray[0];// Field<int>(data.Columns[0]);
            }
            else
            {
                string sql_genus = string.Format("SELECT genus_id FROM Genus WHERE genus_name='{0}'",
                    genus);
                DataTable data2 = dataProvider.ExecuteQuery(sql_genus);
                long genus_id;
                if (data2.Rows.Count != 0)
                {
                    genus_id = (int)data2.Rows[0].ItemArray[0];
                }
                else
                {
                    string sql_genus1 = string.Format("INSERT INTO Genus (genus_name) VALUES ('{0}')",
                        genus);
                    DataTable data3 = dataProvider.ExecuteQuery(sql_genus1);
                    genus_id = dataProvider.GetLastRowID();
                }
                string sql_species = string.Format("INSERT INTO Species (species_name, genus_id) VALUES ('{0}', '{1}')",
                    species,
                    genus_id.ToString(CultureInfo.InvariantCulture));
                dataProvider.ExecuteNonQuery(sql_species);
                species_id = dataProvider.GetLastRowID();
            }
            string sql_parameters = string.Format("SELECT parameters_id FROM Parameters WHERE (init_shell='{0}' AND front_rear_end='{1}' AND age='{2}' AND length='{3}' AND sculpture='{4}' AND ((h_l- '{5}' <0.05* {5} )OR(h_l=null)))",
                init_shell,
                front_rear,
                age,
                length,
                sculpture,
                hl.ToString(CultureInfo.InvariantCulture));
            DataTable data1 = dataProvider.ExecuteQuery(sql_parameters);
            long parameters_id;
            if (data1.Rows.Count != 0)
            {
                parameters_id = (int)data1.Rows[0].ItemArray[0];
            }
            else
            {
                string sql_param = string.Format("INSERT INTO Parameters(init_shell, front_rear_end, age, lenght,sculptute, h_l) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5})",
                    init_shell,
                    front_rear,
                    age,
                    length,
                    sculpture,
                    hl.ToString(CultureInfo.InvariantCulture));
                dataProvider.ExecuteNonQuery(sql_param);
                parameters_id = dataProvider.GetLastRowID();
            }
            if (therephoto)//есть фотография
            {
                string sql_moll = string.Format("INSERT INTO Mollusk(species_id, photo_id, parameters_id) VALUES ('{0}', '{1}', '{2}')",
                    species_id,
                    photo_id,
                    parameters_id);
                dataProvider.ExecuteNonQuery(sql_moll);
            }
            else//добавление вида пока что без фотографии
            {
                string sql_moll = string.Format("INSERT INTO Mollusk(species_id, photo_id, parameters_id) VALUES ('{0}', null, '{1}')",
                    species_id,
                    parameters_id);
                dataProvider.ExecuteNonQuery(sql_moll);
            }
          
            this.Close();
            
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.preview.Source = null;
        }
    }
}
