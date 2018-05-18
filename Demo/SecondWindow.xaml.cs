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
using BusinessObjects;
using System.Threading.Tasks;
using System.ComponentModel;
namespace Demo

//Author name: Andrew Hardie
//Description of class purpose: Second form that allows user to view all studnet objects
//Date last modified: 25/10/2017
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();        
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            //Hides form
            this.Hide();
        }
    
        //Removes selected object from list when user clicks delete record button.
        private void btn_DeleteRecord_Click_1(object sender, RoutedEventArgs e)
        {
            //Displays dialog box to user
            MessageBoxResult result = MessageBox.Show("Record will be lost forever", "WARNINIG!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                //Deletes student record from list
                lstbox_AllStudents.Items.Remove(lstbox_AllStudents.SelectedItem);
            }
        }
    }
}
