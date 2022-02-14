using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Objectsonscreen
{
    public partial class POLYGONFORM : Form
    {
        public POLYGONFORM()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        // initialize -> this.SetStyle(ControlStyles.ResizeRedraw, true);
        // then use protected override void OnPaint(PaintEventArgs e){}
        private const int vGrip = 80;
        private int cGrip = 16;
        protected override void WndProc(ref Message m)
        {
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
               /* 
                * needs ajustment 
                * 
                * if (pos.X <= cGrip) {
                    m.Result = (IntPtr)10; return; }
                if (pos.X >= this.ClientSize.Width - cGrip) {
                    m.Result = (IntPtr)11; return; }
                //bottom
                if (pos.Y <= cGrip){
                    m.Result = (IntPtr)12;return; }
                if (pos.Y >= this.ClientSize.Height - cGrip) {
                    m.Result = (IntPtr)15; return;}
               */

                //Move
                //intire form
                m.Result = (IntPtr)2; return;
              
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SolidBrush redBrush = new SolidBrush(ForeColor);
            GraphicsPath graphPath = new GraphicsPath();
            double middle = 5;
            List<Point> path = new List<Point>();
            path.Add(new Point(0, 0));
            path.Add(new Point(Width/2, (int)(Height / middle)));
            path.Add(new Point(Width, 0));
            path.Add(new Point(Width-(int)(Width/ middle), Height/2));
            path.Add(new Point(Width,Height));
            path.Add(new Point(Width/2, Height- (int)(Height/ middle)));
            path.Add(new Point(0,Height));
            path.Add(new Point((int)(Width / middle), Height / 2));
            path.Add(new Point(0, 0));
            graphPath.AddPolygon(path.ToArray());
            e.Graphics.FillPath(redBrush, graphPath);
        }

    
    }
}
