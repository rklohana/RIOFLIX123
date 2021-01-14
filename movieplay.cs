﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace RIOFLIX123
{
    public partial class movieplay : UserControl

    {
        moviedata c2 = new moviedata();
        IFirebaseClient client;
        #region properties
        private string nametext;
        [Category("Customs props")]
        public string Nametext
        {
            get
            {
                return nametext;
            }
            set
            {
                nametext = value;
                name.Text = value;
            }
        }
        private Image icon;
        [Category("Customs props")]
        public Image Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                bunifuImageButton1.Image = value;
            }
        }

        private string id;
        [Category("Customs props")]
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                
            }
        }


        #endregion

        public movieplay()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void movieplay_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(c2.getConfig());
            if (client == null)
            {

                MessageBox.Show("error Connecting");
            }
        }
        
        private async void bunifuImageButton1_Click(object sender, EventArgs e)
        {
           moviedisplay m2 = new moviedisplay();
            try
            {
                FirebaseResponse r = await client.GetAsync("Movie DATA / " + id);
                moviedata obj = r.ResultAs<moviedata>();
                m2.Nametext = obj.Name;
                m2.Directortext = obj.Director;
                m2.Genretext = obj.Genre;
                m2.Startext = obj.Actor;
                m2.Descriptiontext = obj.Description;
                m2.Icon = icon;
                m2.ratetext = obj.Rate;
                bunifuTransition1.ShowSync(m2);
            }
            catch
            {
                MessageBox.Show("Internet Error");
            }
         
        }
    }
}
