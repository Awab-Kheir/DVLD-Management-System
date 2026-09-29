using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmUserInfo : Form
    {
        private int _UserID;  //  he

        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            //ctrlUserCard1.LoadUserInfo(UserID);          II

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)   
        {
            ctrlUserCard1.LoadUserInfo(_UserID);   // he

        }
    }
}
