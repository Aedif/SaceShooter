using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//StartMenu form

namespace SpaceShooter
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            //Hides the StartMenu form.
            this.Hide();
            //Creates and shows the main form.
            Form1 frm = new Form1();
            frm.Show();
        }

        private void btnControls_Click(object sender, EventArgs e)
        {
            //Creates and displays the Controls form.
            Controls frm = new Controls();
            frm.Show();
        }

        private void btnObjective_Click(object sender, EventArgs e)
        {
            //Creates and displays the Objective form.
            Objective frm = new Objective();
            frm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Closes the application.
            Application.Exit();
        }
    }
}
