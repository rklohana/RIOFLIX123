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
using FireSharp.Interfaces;
using FireSharp.Response;
namespace RIOFLIX123
{
    public partial class recent : UserControl
    {
        Panel p2;
        public recent(Panel p1)
        {
            p2 = p1;
            InitializeComponent();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public IFirebaseClient client;


        protected IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "y4RjXMGpXFsmuG4T0pMLmWIBtcQ6V84ke4uJ3hCT",
            BasePath = "https://rioflix-default-rtdb.firebaseio.com/"
        };
        counter1 c1 = new counter1();
        SingleLinkedList l1 = new SingleLinkedList();
        private async void recent_Load(object sender, EventArgs e)
        {
            
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {

                MessageBox.Show("Not Connected");
            }
            //   MessageBox.Show("Loading");
            FirebaseResponse response = await client.GetAsync("Counter DATA/");
            c1 = response.ResultAs<counter1>();
            MessageBox.Show(c1.Mov_id.ToString());
            int n = c1.Mov_id;
            for (int i = 0; i < n; i++)
            {

                FirebaseResponse response1 = await client.GetAsync("Movie DATA/" + i.ToString());
                moviedata m1 = response1.ResultAs<moviedata>();
                //  MessageBox.Show(m1.Name+" "+i.ToString() );
                l1.InsertLast(m1);

            }
            populatepanel1();
        }
        void populatepanel1()
        {
            MessageBox.Show("called");
            movieplay[] m2 = new movieplay[l1.getcount()];
            SNode temp = l1.gethead();

            //MessageBox.Show(temp.next.next.data.Name);
            // MessageBox.Show(l1.getcount().ToString());
            for (int i = 0; i < l1.getcount(); i++)
            {
                //  MessageBox.Show(temp.data.Name);
                m2[i] = new movieplay(p2);
                m2[i].Nametext = temp.data.Name;
                m2[i].Icon = temp.data.photoback(temp.data.Imagefile);
                m2[i].ID = temp.data.M_id.ToString();

                temp = temp.next;
                if (flowLayoutPanel1.Controls.Count < 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                }
                else
                {
                    m2[i].Show();
                    flowLayoutPanel1.Controls.Add(m2[i]);
                }


            }


        }
    }
}