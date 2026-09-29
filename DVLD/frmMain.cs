using DVLD.Tests;
using DVLD.Applications;
using DVLD.Classes;
using DVLD.Login;
using DVLD.People;
using DVLD.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD.Applications.Local_Driving_License;
using System.Windows.Forms;
using DVLD.Driver;
using DVLD.Applications.International_License;
using DVLD.Applications.Renew_Local_License;
using DVLD.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Licenses.Detain_License;
using DVLD.Applications.Release_Detained_License;
using DVLD.Properties;

namespace DVLD
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        private bool _isDarkTheme = true;   // II
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;

            //II
            ApplyDarkTheme();
        }

        //II
        private void SetMenuItemsForeColor(  ToolStripItemCollection items,Color color)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = color;

                if (item is ToolStripMenuItem menuItem)
                {
                    SetMenuItemsForeColor(menuItem.DropDownItems, color);
                }
            }
        }
        //II
        private void ApplyDarkTheme()
        {
            // خلفية الـ Menu الرئيسية
            msMainMenue.BackColor = Color.FromArgb(31, 31, 31);

            // النصوص كلها أبيض
            SetMenuItemsForeColor(msMainMenue.Items, Color.White);

            // الـ Renderer المخصص
            msMainMenue.RenderMode = ToolStripRenderMode.Professional;

            msMainMenue.Renderer =
                new ToolStripProfessionalRenderer(new CustomMenuColorTable());

            lblLoggedInUser.ForeColor = Color.White;
            lblLoggedInUser.BackColor = Color.FromArgb(31, 31, 31);
            btnChangeTheme.BackColor = Color.FromArgb(31, 31, 31);
            pictureBox1.Image = Resources.vecteezy_ai_generated;

        }
        //II
        private void ApplyOriginalTheme()
        {
            // إذا كان هذا هو اللون القديم عندك
            msMainMenue.BackColor = Color.FromArgb(255, 255, 255);

            // النصوص كلها سوداء
            SetMenuItemsForeColor(msMainMenue.Items, Color.Black);

            // إلغاء الـ Renderer المخصص والرجوع للوضع الطبيعي
            msMainMenue.RenderMode = ToolStripRenderMode.ManagerRenderMode;

            lblLoggedInUser.ForeColor = Color.Black;
            lblLoggedInUser.BackColor = Color.White;
            btnChangeTheme.BackColor = Color.White;
            pictureBox1.Image = Resources.wallpaper;

        }
        //II
        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            if (_isDarkTheme)
            {
                ApplyOriginalTheme();
                _isDarkTheme = false;
            }
            else
            {
                ApplyDarkTheme();
                _isDarkTheme = true;
            }
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        //private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    // ابحث عن أي طفل من نوع Manage_People
        //    var existing = this.MdiChildren
        //                      .OfType<Manage_People>()
        //                      .FirstOrDefault();

        //    if (existing != null)
        //    {
        //        // إذا موجود: أعده إلى الحالة الطبيعية وفعّله
        //        if (existing.WindowState == FormWindowState.Minimized)
        //            existing.WindowState = FormWindowState.Normal;

        //        existing.BringToFront();
        //        existing.Activate();
        //    }
        //    else
        //    {
        //        // إذا غير موجود: أنشئ واحداً جديداً
        //        var frm = new Manage_People();
        //        frm.MdiParent = this;
        //        frm.Show();
        //    }

        //}



        private void frmMain_Load(object sender, EventArgs e)
        {

            this.BackColor = Color.White;     
            lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            this.Refresh();

            //                                          pictureBox  هو استخدم ال 
            //foreach (Control ctrl in this.Controls)                  // مشان تغير لون الفورم الاب بالكود 
            //{
            //    if (ctrl is MdiClient)
            //    {
            //        ctrl.BackColor = Color.Black; // لون الخلفية الذي تريده
            //        break;
            //    }
            //}
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOUtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            // frmLogin frm = new frmLogin();
            //frm.ShowDialog();

            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();          // Main بتعود عال this ال
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestsTypes frm = new frmListTestsTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInterationalLicenseApplications frm = new frmNewInterationalLicenseApplications();
            frm.ShowDialog();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void managedDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm= new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }


    }

    public class CustomMenuColorTable : ProfessionalColorTable
    {
        // Hover
        public override Color MenuItemSelected =>
            Color.FromArgb(51, 51, 51); // #333333

        public override Color MenuItemSelectedGradientBegin =>
            Color.FromArgb(51, 51, 51);

        public override Color MenuItemSelectedGradientEnd =>
            Color.FromArgb(51, 51, 51);

        // عندما تضغط على العنصر / يكون مفتوح
        public override Color MenuItemPressedGradientBegin =>
            Color.FromArgb(120, 70, 20); // برتقالي

        public override Color MenuItemPressedGradientMiddle =>
            Color.FromArgb(120, 70, 20);

        public override Color MenuItemPressedGradientEnd =>
            Color.FromArgb(120, 70, 20);

        // إطار العنصر المحدد
        public override Color MenuItemBorder =>
            Color.FromArgb(120, 70, 20);

        // خلفية القوائم المنسدلة
        public override Color ToolStripDropDownBackground =>
            Color.FromArgb(31, 31, 31);

        // إزالة الألوان الفاتحة الموجودة بجانب الأيقونات
        public override Color ImageMarginGradientBegin =>
            Color.FromArgb(31, 31, 31);

        public override Color ImageMarginGradientMiddle =>
            Color.FromArgb(31, 31, 31);

        public override Color ImageMarginGradientEnd =>
            Color.FromArgb(31, 31, 31);
    }
}
