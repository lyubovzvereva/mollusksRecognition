using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System.Data;

namespace WpfDrawing
{
    public static class Commands
    {
        public static CommandCreateDb CommandCreateDb = new CommandCreateDb();
        public static CommandAddToDb CommandAddToDb = new CommandAddToDb();
        public static CommandOpenDB CommandOpenDB = new CommandOpenDB();
    }
    public class CommandBase : ICommand
    {
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
        protected void RaiseEvent()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }


    public class CommandCreateDb : CommandBase
    {
        public override void Execute(object parameter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Database file (*.db3) |*.db3";
            if (sfd.ShowDialog() == true)
            {
                SqliteDataProvider dataProvider = new SqliteDataProvider();
                dataProvider.Open(sfd.FileName);

               // string sql = "CREATE TABLE Fossils (id INTEGER PRIMARY KEY AUTOINCREMENT, fossilname TEXT, Descrip TEXT, area REAL, circularity REAL, elongation REAL, perimeter REAL, irregularity REAL , bitmap BLOB)";
                string sql = "CREATE TABLE Authors (Author_id INTEGER PRIMARY KEY AUTOINCREMENT, Author_name TEXT)";
                dataProvider.ExecuteNonQuery(sql);
                string sql1 = "CREATE TABLE Description (Description_id INTEGER PRIMARY KEY AUTOINCREMENT, synonymy TEXT, shell TEXT, init_shell TEXT, crop TEXT, key_edge TEXT, front_end TEXT, rear_end TEXT, ventral_margin TEXT, growth_lines TEXT, sculpture TEXT, compare TEXT)";
                dataProvider.ExecuteNonQuery(sql1);
                string sql2 = "CREATE TABLE Genus (genus_id INTEGER PRIMARY KEY AUTOINCREMENT, genus_name TEXT)";
                dataProvider.ExecuteNonQuery(sql2);
                string sql3 = "CREATE TABLE GenusToAuthor (genus_id INTEGER, author_id INTEGER)";
                dataProvider.ExecuteNonQuery(sql3);
                string sql4 = "CREATE TABLE Parameters (parameters_id INTEGER PRIMARY KEY AUTOINCREMENT, init_shell TEXT, front_rear_end TEXT, age TEXT, length TEXT, sculpture TEXT, h_l REAL)";
                dataProvider.ExecuteNonQuery(sql4);
                string sql5 = "CREATE TABLE Photo (id INTEGER PRIMARY KEY AUTOINCREMENT, square REAL, vertex_x REAL, vertex_y REAL, furrow REAL, bitmap BLOB)";
                dataProvider.ExecuteNonQuery(sql5);
                string sql6 = "CREATE TABLE Species (species_id INTEGER PRIMARY KEY AUTOINCREMENT,species_name TEXT, genus_id INTEGER, description_id INTEGER)";
                dataProvider.ExecuteNonQuery(sql6);
                string sql7 = "CREATE TABLE SpeciesToAuthor (species_id INTEGER, author_id INTEGER)";
                dataProvider.ExecuteNonQuery(sql7);
                string sql8 = "CREATE TABLE Mollusk (mollusk_id INTEGER PRIMARY KEY AUTOINCREMENT, species_id INTEGER, photo_id INTEGER, parameters_id INTEGER)";
                dataProvider.ExecuteNonQuery(sql8);
                UIManager.man.Provider = dataProvider;
            }


        }
    }
    public class CommandAddToDb : CommandBase
    {
        public override void Execute(object parameter)
        {
            DataEditWindow dew = new DataEditWindow();
            dew.Owner = Application.Current.MainWindow;
            dew.ShowDialog();
        }
    }
    public class CommandOpenDB : CommandBase
    {
        public override void Execute(object parameter)
        {
            OpenFileDialog sfd = new OpenFileDialog();
           // sfd.Filter = "Database file (*.db3) |*.db3";
            if (sfd.ShowDialog() == true)
            {
                SqliteDataProvider dataProvider = new SqliteDataProvider();
                dataProvider.Open(sfd.FileName);
                UIManager.man.Provider = dataProvider;
                //Здесь изменить запрос
              //string sql = string.Format("SELECT Species.Species_name, Genus.Genus_name, Photo.bitmap, d.synonymy, d.shell, d.init_shell, d.crop, d.key_edge, d.front_end, d.rear_end, d.ventral_margin, d.growth_lines, d.sculpture, d.compare FROM Species, Genus, Photo, Description d, Mollusk, Parameters p WHERE Mollusk.species_id=Species.species_id AND Species.genus_id=Genus.genus_id AND Mollusk.photo_id=Photo.id AND Species.description_id=d.description_id AND Mollusk.parameters_id=p.parameters_id AND p.init_shell='{0}' AND p.front_rear_end='{1}' AND p.age='{2}' AND p.length='{3}' AND p.sculpture='{4}' AND p.h_l='{5}'",
              //      MainWindow.itis.init_shell.Text,
              //      MainWindow.itis.front_rear_end.Text,
              //      MainWindow.itis.age.Text,
              //      MainWindow.itis.length.Text,
              //      MainWindow.itis.length.Text,
              //      MainWindow.itis.h_l.Text);

                string sql = "SELECT Photo.id, Species.Species_name, Genus.Genus_name, Photo.bitmap, d.synonymy, d.shell, d.init_shell, d.crop, d.key_edge, d.front_end, d.rear_end, d.ventral_margin, d.growth_lines, d.sculpture, d.compare FROM Mollusk LEFT OUTER JOIN Species ON Mollusk.species_id=Species.species_id LEFT OUTER JOIN Genus ON Species.genus_id=Genus.genus_id LEFT OUTER JOIN Photo ON Mollusk.photo_id=Photo.id LEFT OUTER JOIN Description d ON Species.description_id=d.description_id";
                DataTable data = dataProvider.ExecuteQuery(sql);

                UIManager.man.GetDataGrid().ItemsSource = data.DefaultView;
            }
        }
    }
}
