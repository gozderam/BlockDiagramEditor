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
    class StartBlock : Block
    {
        public static bool IsPresent { get; set; } = false;

        public static int Height { get; } = 60;
        public static int Width { get; } = 90;
        public int X { get; }
        public int Y { get; }
        public OutLinkButton OutLink { get; set; }

        public StartBlock(int xMouse, int yMouse) : base("START")
        {
            X = xMouse - Width / 2;
            Y = yMouse - Height / 2;
            IsPresent = true;
            OutLink = new OutLinkButton(X + Width / 2 - OutLinkButton.R, Y + Height - OutLinkButton.R);
        }

        public Point Middle
        {
            get
            {
                return new Point(X + Width / 2, Y + Height / 2);
            }
        }
   
        public override bool IsUnderMouse(int xMouse, int yMouse)
        {
            Rectangle myEllipse = new Rectangle(this.X, this.Y, StartBlock.Width, StartBlock.Height);
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
            if (OutLink.LinkedTo != null)
                OutLink.ClearLinkButton(srcBitmap, srcPictureBox);

            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, StartBlock.Width, StartBlock.Height);
            graphics.FillEllipse(Brushes.White, rect);
            Pen pen = new Pen(Color.Green, 2);
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

            if (OutLink.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(OutLink.startLinkPoint, OutLink.LinkedTo.endLinkPoint, srcBitmap, srcPictureBox);
            else
                OutLink.DrawLinkButton(srcBitmap, srcPictureBox);

            srcPictureBox.Invalidate();
        }

        public override (bool canBeDone, OutLinkButton, InLinkButton) StartLink(int xMouse, int yMouse)
        {
            if (OutLink.LinkedTo!=null || !OutLink.IsUnderMouse(xMouse, yMouse)) return (false, null, null);
            return (true, OutLink, null);
        }

        public override (bool canBeDone, InLinkButton inLinkButton) EndLink(int xMouse, int yMouse, InLinkButton notAllowedInLink)
        {
            return (false, null);
        }

        public override void RemoveLinks()
        {
            if (OutLink.LinkedTo != null) OutLink.LinkedTo.LinkedTo = null;
            OutLink = null;
        }
    }


}
