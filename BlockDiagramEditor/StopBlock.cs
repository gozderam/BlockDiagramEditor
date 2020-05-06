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
    class StopBlock : Block
    {

        public static int Height { get; } = 60;
        public static int Width { get; } = 90;
        public int X { get; }
        public int Y { get; }

        public InLinkButton InLink { get; set; }
        public StopBlock(int xMouse, int yMouse)
            : base("STOP")
        {
            X = xMouse - Width / 2;
            Y = yMouse - Height / 2;
            InLink = new InLinkButton(X + Width / 2 - InLinkButton.R, Y - InLinkButton.R);
        }

        public override bool IsUnderMouse(int xMouse, int yMouse)
        {
            Rectangle myEllipse = new Rectangle(this.X, this.Y, StopBlock.Width, StopBlock.Height);
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(myEllipse);
            if (myPath.IsVisible(new Point(xMouse, yMouse)))
            {
                return true;
            }
            return false;
        }

        public override void DrawBlockImage(Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            ImageDrawing(srcBitmap, srcPictureBox, false);
        }

        public override void DrawClickedBlockImage(Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            ImageDrawing(srcBitmap, srcPictureBox, true);
        }

        private void ImageDrawing(Bitmap srcBitmap, PictureBox srcPictureBox, bool isDashed)
        {
            if (InLink.LinkedTo != null)
                InLink.ClearLinkButton(srcBitmap, srcPictureBox);
      
            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, StopBlock.Width, StopBlock.Height);
            graphics.FillEllipse(Brushes.White, rect);
            Pen pen = new Pen(Color.Red, 2);
            pen.Alignment = PenAlignment.Inset;
            if (isDashed)
            {
                float[] dashValues = { 5, 5 };
                pen.DashPattern = dashValues;
            }
            graphics.DrawEllipse(pen, rect);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(this.Text, new Font("Tahoma", 8), Brushes.Black, rect, sf);

            if (InLink.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(InLink.LinkedTo.startLinkPoint, InLink.endLinkPoint, srcBitmap, srcPictureBox);
            else
                InLink.DrawLinkButton(srcBitmap, srcPictureBox);

            srcPictureBox.Invalidate();
        }

         public override (bool canBeDone, OutLinkButton, InLinkButton) StartLink(int xMouse, int yMouse)
        {           
            return (false, null, null);
        }

        public override (bool canBeDone, InLinkButton inLinkButton) EndLink(int xMouse, int yMouse, InLinkButton notAllowedInLink)
        {
            if (InLink.LinkedTo == null && InLink.IsUnderMouse(xMouse, yMouse)) return (true, InLink);
            return (false, null);
        }

        public override void RemoveLinks()
        {
            if(InLink.LinkedTo!=null) InLink.LinkedTo.LinkedTo = null;
            InLink= null;
        }
    }


}
