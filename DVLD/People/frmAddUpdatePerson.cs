using System;
using DVLD.Controls;
using DVLD.Properties;
using DVLD_Business;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DVLD.Classes;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;


        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;

        //        ctrlPersonCard   هو ماحطها هون بس حطها بال  II
        //public int PersonID
        //{
        //    get { return _PersonID; }
        //}
        //

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _ResetDefaultValues()
        {
            //this will initialize the reset the default values
            _FullCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";   
            }

            //set default image for the person.
            if (rbMale.Checked) 
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (pbPersonImage.Location != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.   // II syria
            //cbCountry.SelectedIndex = 168;                         II
            cbCountry.SelectedIndex = cbCountry.FindString("Syria");          //  he

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";


        }

        private void _FullCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //_FillPersonInfo();   II

            //the following code will not be executed if the person was not found
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;  //  = Convert.ToDateTime(_Person.DateOfBirth);  II

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            /*string _CountryName = clsCountry.Find(_Person.NationalityCountryID).CountryName;  II
            cbCountry.Text = _CountryName;*/
            //cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);   II طريقة تانية
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);   // II      clsPerson جوا ال CountryInfoاستفاد انه بالاصل عرف ال  


            //يمكن لانو اذا فاضيين عادي وهن بالاصل اذا بدهم يكونوا قاضيين معناها انا مالي جابر المستخدم يحطهم فبكون عادي بالنسبة قلي ينحطوا فاضيين Email وال _Person.ThirdName ما شيك عال    II
            //load person image incase it was set.
            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            //hide/show the remove linke incase there is no image for the person.
            //llRemoveImage.Visible = (pbPersonImage.Location != null);         // Property هون عم يشوف الباث من ال  II
            llRemoveImage.Visible = (_Person.ImagePath != null);             // DB هون عم يشوف الباث من ال he

        }

        private void frmAddPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if(_Mode == enMode.Update)
               _LoadData();
        }

        private bool _HandlePersonImage()
        {
            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if(_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        //we could not delete the file.
                        //log it later
                    }
                }


                if(pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if(clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                //Here we dont continue because the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;


            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            //_Person.Gendor = Convert.ToInt16((rbMale.Checked) ? 0 : 1);     II
            if (rbMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;

            _Person.NationalityCountryID = NationalityCountryID;


            //if (txtThirdName.Text != "")                          هو ماحطهم II
            //    _Person.ThirdName = txtThirdName.Text;
            //else
            //    _Person.ThirdName = "";
            //if (txtEmail.Text != "")
            //    _Person.Email = txtEmail.Text;
            //else
            //    _Person.Email = "";   

            if (pbPersonImage.ImageLocation != null)        
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";


            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.  
                DataBack?.Invoke(this, _Person.PersonID);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.ReadOnlyChecked = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            //change the default image to female incase there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            //change the default image to male incase there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        //private void txtFirstName_Validating(object sender, CancelEventArgs e)     II
        //{
        //    if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        //    {
        //        e.Cancel = true;
        //        txtFirstName.Focus();
        //        errorProvider1.SetError(txtFirstName, "FirstName should have a value!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txtFirstName, "");
        //    }
        //}

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validation the email incase it's empty.
            if (txtEmail.Text.Trim() == "") 
                return; 

            //validation email format 
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
            

            //if (!string.IsNullOrWhiteSpace(txtEmail.Text))              II
            //{
            //    if (!txtEmail.Text.EndsWith("@gmail.com"))                //txtEmail.Text.Contains("@gmail.com")
            //    {
            //        e.Cancel = true;
            //        txtEmail.Focus();
            //        errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            //    }
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(txtEmail, "");
            //}
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.isPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                //txtNationalNo.Focus();   II
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
            }
            else
            {
                //e.Cancel = false;   II
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        //private void _FillPersonInfo()        II
        //{
        //    string _CountryName = clsCountry.Find(_Person.NationalityCountryID).CountryName;

        //    lblNA.Text = PersonID.ToString();
        //    txtNationalNo.Text = _Person.NationalNo;
        //    txtFirstName.Text = _Person.FirstName;
        //    txtSecondName.Text = _Person.SecondName;
        //    txtLastName.Text = _Person.LastName;
        //    dtpDateOfBirth.Value = Convert.ToDateTime(_Person.DateOfBirth);
        //    if (_Person.Gendor == 0)
        //        rbMale.Checked = true;
        //    else
        //        rbFemale.Checked = true;

        //    txtAddress.Text = _Person.Address;
        //    txtPhone.Text = _Person.Phone;
        //    cbCountry.Text = _CountryName;


        //    if (_Person.ThirdName != "")
        //        txtThirdName.Text = _Person.ThirdName;
        //    else
        //        txtThirdName.Text = "";

        //    if (_Person.Email != "")
        //        txtEmail.Text = _Person.Email;
        //    else
        //        txtEmail.Text = "";

        //    //if (_Person.ImagePath != "")
        //    //{
        //    //    pictureBox1.ImageLocation = _Person.ImagePath;

        //    //    //AI
        //    //}
        //    //else
        //    //    pictureBox1.ImageLocation = null;


        //    //this will select the country in the combobox.
        //    cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);
        //    _LoadPersonImage();


        //}




    }
}
