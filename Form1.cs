using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Objectsonscreen
{

    public partial class Form1 : Form
    {

        List<Thread> jt = new List<Thread>(); 
        List <Form> jf = new List<Form>(); 

        public Form1()
        {
            InitializeComponent();
        }

        void addform(Form form)
        {
         
            for (int tl = 0; tl <= jf.Count; tl++)
            {
                //look for empty slot
                if (tl < jf.Count)
                {
                    if (jf[tl] != null) continue;
                }

                Thread newThread = new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    //set color to object on screen
                    //without changing transparent background
                    jf[tl].ForeColor = pictureBox1.BackColor;
                    Application.Run(jf[tl]);
                    //clear thread after close
                    jt[tl] = null;
                    //clear item in list after close
                    jf[tl] = null;
                });

                //if is emty slot 
                if (tl < jf.Count)
                {
                    jf[tl] = form;
                    jt[tl] = newThread;
                    jt[tl].Start();
                }
                else
                {
                    //create new slot
                    jf.Add(form);
                    jt.Add(newThread);
                    jt[jt.Count-1].Start(); 
                }

                newThread= null;
                break;
            }
        }

   

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            passthis.CloseApp = true;
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ROUNDFORM form = new ROUNDFORM();
            addform(form);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            POLYGONFORM form = new POLYGONFORM();
            addform(form);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBox1.BackColor = colorDialog1.Color;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBox1.BackColor = colorDialog1.Color;
        }
        bool clicktrh = false;

        private void button5_Click(object sender, EventArgs e)
        {
            clicktrh = !clicktrh;
            passthis.clickDisable = clicktrh;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SQUAREFORM form = new SQUAREFORM();
            addform(form);
        }
    }

    /* for all open forms read this. */
    public static class passthis
    {
        public static bool clickDisable = false;
        public static bool CloseApp = false;
    }

}
