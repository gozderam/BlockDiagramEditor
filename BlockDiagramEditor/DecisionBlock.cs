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
    class DecisionBlock : Block
    {
        public static int HalfDiameter { get; } = 50;
        private Point topPoint;
        private Point rightPoint;
        private Point bottomPoint;
        private Point leftPoint;

        public InLinkButton InLink { get; set; } 
        public OutLinkButton OutLinkLeft { get; set; } // true
        public OutLinkButton OutLinkRight { get; set; } // false;
        public DecisionBlock(int xMouse, int yMouse, string text)
           : base(text)
        {
            topPoint = new Point(xMouse, yMouse - HalfDiameter);
            rightPoint = new Point(xMouse + HalfDiameter, yMouse);
            bottomPoint = new Point(xMouse, yMouse + HalfDiameter);
            leftPoint = new Point(xMouse - HalfDiameter, yMouse);

            InLink = new InLinkButton(topPoint.X - InLinkButton.R, topPoint.Y - InLinkButton.R );
            OutLinkLeft = new OutLinkButton(leftPoint.X - OutLinkButton.R , leftPoint.Y - OutLinkButton.R );
            OutLinkRight = new OutLinkButton(rightPoint.X - OutLinkButton.R, rightPoint.Y - OutLinkButton.R);

        }

        public Point[] GetPointsArray()
        {
            Point[] retPoints = new Point[4];
            retPoints[0] = topPoint;
            retPoints[1] = rightPoint;
            retPoints[2] = bottomPoint;
            retPoints[3] = leftPoint;
            return retPoints;
        }

        public Point MiddlePoint
        {
            get
            {
                return new Point((leftPoint.X + rightPoint.X) / 2, (topPoint.Y + bottomPoint.Y) / 2);
            }
        }

        public override bool IsUnderMouse(int xMouse, int yMouse)
        {
            if(xMouse >= leftPoint.X && xMouse <= rightPoint.X
                && yMouse >= (int)LinearFunction(leftPoint, topPoint, xMouse)
                && yMouse >= (int)LinearFunction(topPoint, rightPoint, xMouse)
                && yMouse <= (int)LinearFunction(rightPoint, bottomPoint, xMouse)
                && yMouse <= (int)LinearFunction(bottomPoint, leftPoint, xMouse))
            {
                return true;
            }
            return false;
            double LinearFunction(Point A, Point B, int x)
            {
                return ((A.Y - B.Y) / (A.X - B.X) * x + A.Y - ((A.Y - B.Y) / (A.X - B.X) * A.X));
            }
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

            if (OutLinkLeft.LinkedTo != null)
                OutLinkLeft.ClearLinkButton(srcBitmap, srcPictureBox);

            if (OutLinkRight.LinkedTo != null)
                OutLinkRight.ClearLinkButton(srcBitmap, srcPictureBox);

            Graphics graphics = Graphics.FromImage(srcBitmap);
            graphics.FillPolygon(Brushes.White, this.GetPointsArray());      
            Pen pen = new Pen(Color.Black, 2);
            pen.Alignment = PenAlignment.Inset;
            if (isDashed)
            {
                float[] dashValues = { 5, 5 };
                pen.DashPattern = dashValues;
            }
            graphics.DrawPolygon(pen, this.GetPointsArray());
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(this.Text, new Font("Tahoma", 8), Brushes.Black,
                new Rectangle(MiddlePoint.X - 3 * (rightPoint.X - leftPoint.X) / 8, MiddlePoint.Y - 3 * (bottomPoint.Y - topPoint.Y) / 8,
                6 * (rightPoint.X - leftPoint.X) / 8, 6 * (bottomPoint.Y - topPoint.Y) / 8),
                sf);

            graphics.DrawString("T", new Font("Tahoma", 12), Brushes.Black, new Rectangle(leftPoint.X - 15, leftPoint.Y - 15, 15, 15),sf);
            graphics.DrawString("F", new Font("Tahoma", 12), Brushes.Black, new Rectangle(rightPoint.X , rightPoint.Y - 15, 15, 15), sf);

            if (OutLinkLeft.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(OutLinkLeft.startLinkPoint, OutLinkLeft.LinkedTo.endLinkPoint, srcBitmap, srcPictureBox);
            else
                OutLinkLeft.DrawLinkButton(srcBitmap, srcPictureBox);

            if (OutLinkRight.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(OutLinkRight.startLinkPoint, OutLinkRight.LinkedTo.endLinkPoint, srcBitmap, srcPictureBox);
            else
                OutLinkRight.DrawLinkButton(srcBitmap, srcPictureBox);

            if (InLink.LinkedTo != null)
                LinkButton.DrawLinkOutToIn(InLink.LinkedTo.startLinkPoint, InLink.endLinkPoint, srcBitmap, srcPictureBox);
            else
                InLink.DrawLinkButton(srcBitmap, srcPictureBox);


            srcPictureBox.Invalidate();
        }


        public override (bool canBeDone, OutLinkButton, InLinkButton) StartLink(int xMouse, int yMouse)
        {
            if (OutLinkLeft.LinkedTo == null && OutLinkLeft.IsUnderMouse(xMouse, yMouse)) return (true, OutLinkLeft, InLink);
            if (OutLinkRight.LinkedTo == null &&  OutLinkRight.IsUnderMouse(xMouse, yMouse)) return (true, OutLinkRight, InLink);
            return (false, null, null);
        }

        public override (bool canBeDone, InLinkButton inLinkButton) EndLink(int xMouse, int yMouse, InLinkButton notAllowedInLink)
        {
            if (InLink.LinkedTo == null && InLink.IsUnderMouse(xMouse, yMouse) && InLink != notAllowedInLink) return (true, InLink);
            return (false, null);
        }

        public override void RemoveLinks()
        {
            if (InLink.LinkedTo != null)  InLink.LinkedTo.LinkedTo = null;
            InLink= null;
            if (OutLinkRight.LinkedTo != null) OutLinkRight.LinkedTo.LinkedTo = null;
            OutLinkRight = null;
            if (OutLinkLeft.LinkedTo != null) OutLinkLeft.LinkedTo.LinkedTo = null;
            OutLinkLeft = null;
        }

    }

}