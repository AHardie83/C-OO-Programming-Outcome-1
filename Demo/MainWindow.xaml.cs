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

//Author name: Andrew Hardie
//Description of class purpose: Code to run program, allows user to enter student details, to view, delete and to save.
//Date last modified: 25/10/2017

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModuleList store = new ModuleList();
        Window1 win = new Window1();

        //Allows use of Student class functions
        Student s = new Student();

        //Variable list
        private int _matricNumber;
        private int _cMark;
        private int _eMark;
        private double _cp;
        private double _ep;
        private double _fp;
        private int _matricCounter = 10001;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Starts validation process then adds student if all fields are validated
        private void AddStudent_btn_Click(object sender, RoutedEventArgs e)
        {
            //Calls validateFirstName method
            validateFirstName();
        }

        //Verifies Firstname field
        public void validateFirstName()
        {

            //Verifies that Firstname textbox isnt blank
            try
            {
                s.FName = txtBox_Firstname.Text;
                validateSurname();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
                txtBox_Firstname.Text = "ENTER FIRST NAME...";
            }
        }


        //Validates surname field
        public void validateSurname()
        {
            //Verifies that Surname textbox isnt blank
            try
            {
                s.SName = txtBox_Surname.Text;
                validateDOB();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
                txtBox_Surname.Text = "ENTER SURNAME...";
            }
        }

        //Validates Date of Birth field
        public void validateDOB()
        {
            //Verifies that DOB textbox isnt blank
            try
            {
                //sets student Date of birth to textbox value
                s.DOB = DOB_datepick.Text;

                //Calls validateMatric method
                validateMatric();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        //Validates Matriculaiton Number field
        public void validateMatric()
        {
            //Verifies that MatricNo textbox isnt blank
            if (txtBox_MatricNo.Text == "")
            {
                MessageBox.Show("Matriculation Number is blank");
            }
            //Checks to see if MatricNo textbox contains any letters
            else if (!int.TryParse(txtBox_MatricNo.Text, out _matricNumber))
            {
                MessageBox.Show("Matriculation Number must contain only numbers");
                return;
            }
            else
            {
                try
                {
                    //Converts textbox String to Int
                    _matricNumber = Convert.ToInt32(txtBox_MatricNo.Text);
                    //Checks to make sure txtbox contains only numbers
                    _matricNumber = int.Parse(txtBox_MatricNo.Text);

                    //Checks to see if matriculation number is already in use
                    if (lstbox_MatricNumbers.Items.Contains(_matricNumber))
                    {
                        MessageBox.Show("Matriculaiton Number already in use" + "\n" + "Please use a different matric number");
                    }
                    else
                    {
                        s.Matric = _matricNumber;
                        validateCourseworkMark();
                    }
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message);
                    txtBox_MatricNo.Clear();
                }
            }
        }

        //Validates Coursework Mark field
        public void validateCourseworkMark()
        {
            //Verifies that CourseworkMark textbox isnt blank
            if (txtBox_CourseworkMark.Text == "")
            {
                MessageBox.Show("Coursework Mark is blank");
            }
            else
            {
                //Tries to run the code below and cathes any expetion thrown
                try
                {
                    //Converts CourseworkMark textbox to int
                    _cMark = Convert.ToInt32(txtBox_CourseworkMark.Text);
                    //Checks to make sure txtbox contains only numbers
                    _cMark = int.Parse(txtBox_CourseworkMark.Text);
                    s.CWorkMark = _cMark;
                    validateExamMark();
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message);
                    txtBox_CourseworkMark.Clear();
                }
            }
        }

        //Validates Exam Mark field
        public void validateExamMark()
        {
            //Student s = new Student();
            //Verifies that ExamMark textbox isnt blank
            if (txtBox_ExamMark.Text == "")
            {
                MessageBox.Show("Exam mark is blank");
            }
            else
            {
                //Tries to run the code below and cathes any expetion thrown
                try
                {
                    //Converts ExamMark textbox value to int
                    _eMark = Convert.ToInt32(txtBox_ExamMark.Text);
                    _eMark = int.Parse(txtBox_ExamMark.Text);
                    s.EXMark = _eMark;
                    overallGrade();
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message);
                    txtBox_ExamMark.Clear();
                }
            }
        }

        //Calculates overall grade %
        public void overallGrade()
        {
            _cp = (Convert.ToDouble(txtBox_CourseworkMark.Text) / 20) / 2;
            _ep = (Convert.ToDouble(txtBox_ExamMark.Text) / 40) / 2;
            _fp = (_cp + _ep) * 100;
            //Shows messagebox with students overall grade %
            MessageBox.Show("Students Overall Grade is: " + Math.Round(_fp) + "%");

            //Calls createStudent method
            createStudent();
        }

        //Creates student object
        public void createStudent()
        {
            //Allows use of Student class functions
            Student s = new Student();
            s.FName = txtBox_Firstname.Text;
            s.SName = txtBox_Surname.Text;
            s.DOB = DOB_datepick.Text;
            s.Matric = _matricNumber;
            s.CWorkMark = _cMark;
            s.EXMark = _eMark;
            s.GetMark = _fp;

            //Adds student to all students list on next form
            win.lstbox_AllStudents.Items.Add(s.getAllStudents());

            //Creates a message box to alert user that student has been added
            MessageBox.Show("Student has been added");

            //Adds matric number to listbox on form
            lstbox_MatricNumbers.Items.Add(s.Matric);

            //Adds student object to store
            store.add(s);

            //Calls getNextMatric method
            getNextMatric();
            
        }

        //Increases Matric +1 number every time student is added.
        public void getNextMatric()
        {
            _matricCounter++;
            txtBox_MatricNo.Text = _matricCounter.ToString();

            //Calls clearForm method
            clearForm();
        }

        //Clears text boxes and datepicker in form
        public void clearForm()
        {
            txtBox_CourseworkMark.Clear();
            txtBox_ExamMark.Clear();
            txtBox_Firstname.Clear();
            txtBox_Surname.Clear();
            DOB_datepick.Text = "";
        }

        //Sets max field length to 5 characters
        private void txtBox_MatricNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Sets textbox character length to 5
            txtBox_MatricNo.MaxLength = 5;
        }

        private void Find_btn_Click(object sender, RoutedEventArgs e)
        {
            //Calls find function from ModuleList
            if (txtBox_MatricFind.Text == "")
            {
                MessageBox.Show("Please enter a matriculation number to find");
            }
            else
            {
                //Uses find function in ModuleList to search matric numbers using matric supplies in txtbox
                Student x = store.find(Convert.ToInt32(txtBox_MatricFind.Text));

                //Returns a message if record isnt found and return is null.
                if (store.find(Convert.ToInt32(txtBox_MatricFind.Text)) == null)
                {
                    MessageBox.Show("Record not found");
                    txtBox_MatricFind.Clear();
                }
                else
                {
                    //if record is found then uses getdetails function in student to show student object information
                    MessageBox.Show(x.getDetails());
                    //clears the txtbox
                    txtBox_MatricFind.Clear();
                }
            }
        }

        //Sets max field length to 5 characters
        private void txtBox_MatricFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Sets textbox character length to 5
            txtBox_MatricFind.MaxLength = 5;
        }

        //Deletes student object from list
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_MatricFind.Text != "")
            {
                //Displays dialog box to user
                MessageBoxResult result = MessageBox.Show("Delete Student Record?", "WARNINIG!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    //Use delete method in ModuleList class to delete student record
                    store.delete(Convert.ToInt32(txtBox_MatricFind.Text));
                    //Displays message to user confirming record has been deleted.
                    MessageBox.Show("Record Deleted");
                }
            }
            else
            {
                //Displays dialog box to user
                MessageBoxResult result = MessageBox.Show("Delete Student Record?", "WARNINIG!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    store.delete(Convert.ToInt32(lstbox_MatricNumbers.SelectedItem));
                    //Displays message to user confirming record has been deleted.
                    MessageBox.Show("Record Deleted");
                }
            }
        }

        //Allows user to select matriculation number from  listbox and view student object details
        private void lstbox_MatricNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Uses find function in ModuleList to search matric numbers using matric supplies in txtbox
                Student x = store.find(Convert.ToInt32(lstbox_MatricNumbers.SelectedItem));
                MessageBox.Show(x.getDetails());
            }
            catch
            {
                MessageBox.Show("Record has been deleted");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win.Show();
        }
    }
}
