using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockDiagramEditor
{

    [Serializable]
    class InLinkButton : LinkButton
    {

        public Point endLinkPoint;

        public OutLinkButton LinkedTo { get; set; } = null;
        public InLinkButton(int x, int y) : base(x, y) { }
        public override void DrawLinkButton(Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, 2 * R, 2 * R);
            graphics.FillEllipse(Brushes.White, rect);
            Pen pen = new Pen(Color.Black, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawEllipse(pen, rect);
        }
        public override void ClearLinkButton(Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, 2 * R, 2 * R);
            graphics.FillEllipse(Brushes.White, rect);
            Pen pen = new Pen(Color.White, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawEllipse(pen, rect);
        }

    }

    [Serializable]
    class OutLinkButton : LinkButton
    {
        public Point startLinkPoint;
        public InLinkButton LinkedTo { get; set; } = null;
        public OutLinkButton(int x, int y) : base(x, y) { }
        public override void DrawLinkButton(Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, 2 * R, 2 * R);
            graphics.FillEllipse(Brushes.Black, rect);
            Pen pen = new Pen(Color.Black, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawEllipse(pen, rect);
        }
        public override void ClearLinkButton(Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, 2 * R, 2 * R);
            graphics.FillEllipse(Brushes.White, rect);
            Pen pen = new Pen(Color.White, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawEllipse(pen, rect);
        }
    }
}




