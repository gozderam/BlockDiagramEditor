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
    class OperationBlock : Block
    {
        public static int Height { get; } = 40;
        public static int Width { get; } = 100;
        public int X { get; }
        public int Y { get; }

        public InLinkButton InLink { get; set; }
        public OutLinkButton OutLink { get; set; }
        public OperationBlock(int xMouse, int yMouse, string text)
            : base(text)
        {
            X = xMouse - Width / 2;
            Y = yMouse - Height / 2;
            InLink = new InLinkButton(X + Width / 2 - InLinkButton.R, Y - InLinkButton.R);
            OutLink = new OutLinkButton(X + Width / 2 - OutLinkButton.R, Y + Height - OutLinkButton.R);
        }

        public override bool IsUnderMouse(int xMouse, int yMouse)
        {
            if (xMouse >= X && xMouse <= X + Width && yMouse >= Y && yMouse <= Y + Height) return true;
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

            if (OutLink.LinkedTo != null)
                OutLink.ClearLinkButton(srcBitmap, srcPictureBox);

            Graphics graphics = Graphics.FromImage(srcBitmap);
            Rectangle rect = new Rectangle(this.X, this.Y, OperationBlock.Width, OperationBlock.Height);
            graphics.FillRectangle(Brushes.White, rect);
            Pen pen = new Pen(Color.Black, 2);
            pen.Alignment = PenAlignment.Inset;
            if (isDashed)
            {
                float[] dashValues = { 5, 5 };
                pen.DashPattern = dashValues;
            }
            graphics.DrawRectangle(pen, rect);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(this.Text, new Font("Tahoma", 8), Brushes.Black, rect, sf);

            if (OutLink.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(OutLink.startLinkPoint, OutLink.LinkedTo.endLinkPoint, srcBitmap, srcPictureBox);
            else
                OutLink.DrawLinkButton(srcBitmap, srcPictureBox);

            if (InLink.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(InLink.LinkedTo.startLinkPoint, InLink.endLinkPoint, srcBitmap, srcPictureBox);
            else
                InLink.DrawLinkButton(srcBitmap, srcPictureBox);


            srcPictureBox.Invalidate();
        }

        public override (bool canBeDone, OutLinkButton, InLinkButton) StartLink(int xMouse, int yMouse)
        {
            if (OutLink.LinkedTo != null || !OutLink.IsUnderMouse(xMouse, yMouse)) return (false, null, null);
            return (true, OutLink, InLink);
        }

        public override (bool canBeDone, InLinkButton inLinkButton) EndLink(int xMouse, int yMouse, InLinkButton notAllowedInLink)
        {
            if (InLink.LinkedTo == null && InLink.IsUnderMouse(xMouse, yMouse) && InLink!= notAllowedInLink) return (true, InLink);
            return (false, null);
        }

        public override void RemoveLinks()
        {
            if(InLink.LinkedTo!=null) InLink.LinkedTo.LinkedTo = null;
            InLink= null;
            if (OutLink.LinkedTo!=null) OutLink.LinkedTo.LinkedTo = null;
            OutLink = null;
        }
    }
}
