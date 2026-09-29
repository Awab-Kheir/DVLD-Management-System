using DVLD.Classes;
using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememeberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememeberUsernameAndPassword("", "");

                }

                //incase the user is not active
                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
              

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";
            if(clsGlobal.GetStoredCredential(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
                btnShowPassword.Visible = true;   //  II
            }
            else
                chkRememberMe.Checked = false;
            


                                                                                 //   II

            lblDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd   hh:mm:ss tt");   //("yyyy/MM/dd HH:mm:ss");  
            trClock.Start(); // يعمل Timer للتأكد أن  

            txtPassword.UseSystemPasswordChar = true;
            //txtPassword.PasswordChar = '*';               هي بدل يلي فوقها

            if(!chkRememberMe.Checked)
            {
                txtPassword.Text = "PIN";
                txtPassword.ForeColor = Color.Gray;
            }
            
        }
        

                                                                                    //  II

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            btnShowPassword.BackgroundImage = Resources.eye;
            //txtPassword.PasswordChar = '\0';           او هي الطريقة

            //UseSystemPasswordChar  بعض المطورين يفضلون 
            //(يظهر نقاط بدلاً من نجوم Windows 10/11) لأنه يتكيف مع شكل النظام 
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnShowPassword.BackgroundImage = Resources.hidden;
            //txtPassword.PasswordChar = '*';           او هي الطريقة
        }

        private void trClock_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd   hh:mm:ss tt");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnShowPassword.Visible = (txtPassword.Text != "" ? true : false);        

            /*if(txtPassword.Text == "")
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "PIN";
                txtPassword.ForeColor = Color.Gray;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }*/
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            /*if (txtPassword.Text == "PIN")
            {
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }*/
        }

    }
}
