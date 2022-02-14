using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Objectsonscreen
{
    public partial class SQUAREFORM : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        public SQUAREFORM()
        {
            InitializeComponent();
            new Thread(() =>
            {
                while (!passthis.CloseApp)
                {
                    if (!last && passthis.clickDisable)
                    {
                        last = true;

                        /*out of this thread error*/

                        uint initialStyle = GetWindowLong(this.Handle, -20);
                        SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
                        return;
                    }
                    else if (last && !passthis.clickDisable)
                    {
                        last = false;
                        System.Windows.Forms.MessageBox.Show("Test");
                    }
                }
                Thread.Sleep(100);
                
            }).Start();
        }

        // initialize -> this.SetStyle(ControlStyles.ResizeRedraw, true);
        // then use protected override void OnPaint(PaintEventArgs e){}
        private const int vGrip = 50;
        private int cGrip = 16;

        bool last = false;
        protected override void WndProc(ref Message m)
        {
            //any thing mouse does then set to click trough or not.
            if (!last && passthis.clickDisable)
            {
                last = true;
                /* no error this void WndProc is called in this thread */
                uint initialStyle = GetWindowLong(this.Handle, -20);
                SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
                return;
            }

            //double click
            if (m.Msg == 0x00A3)
            {
                this.Close();
                this.Dispose();
                return;
            }

            if (m.Msg == 0x84)
            {

                if (ClientSize.Width < 150)
                {
                    cGrip = (ClientSize.Width / 4) + 2;
                }
                else
           if (ClientSize.Height < 150)
                {
                    cGrip = (ClientSize.Height / 4) + 2;
                }
                else
                    cGrip = vGrip;


                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                //rezize
                //corners
                if (pos.X >= this.ClientSize.Width - cGrip 
                    && pos.Y >= this.ClientSize.Height - cGrip){
                    m.Result = (IntPtr)17; return; }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y <= cGrip){
                    m.Result = (IntPtr)14; return; }
                if (pos.X <= cGrip && pos.Y <= cGrip){
                    m.Result = (IntPtr)13; return; }
                if (pos.X <= cGrip && pos.Y >= this.ClientSize.Height - cGrip){
                    m.Result = (IntPtr)16; return;}
               //sides 
                if (pos.X <= cGrip) {
                    m.Result = (IntPtr)10; return; }
                if (pos.X >= this.ClientSize.Width - cGrip) {
                    m.Result = (IntPtr)11; return; }
                //bottom
                if (pos.Y <= cGrip){
                    m.Result = (IntPtr)12;return; }
                if (pos.Y >= this.ClientSize.Height - cGrip) {
                    m.Result = (IntPtr)15; return;}
                //Move
                //intire form
                m.Result = (IntPtr)2; return;
              
            }
            base.WndProc(ref m);
        }
       
        private void SQUAREFORM_Load(object sender, EventArgs e)
        {
            label1.Text = Text;
            this.BackColor = ForeColor;
  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!last && passthis.clickDisable)
            {
                last = true;

                /*out of this thread error*/
                uint initialStyle = GetWindowLong(this.Handle, -20);
                SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
                return;
            }
            else if (last && !passthis.clickDisable)
            {
                last = false;
                System.Windows.Forms.MessageBox.Show("Test");
            }
        }
    }
}
