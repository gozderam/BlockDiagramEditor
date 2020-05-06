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
    abstract class SchemeElement
    {
        private string textField = "";
        public string Text
        {
            get { return textField; }
            set
            {
                textField = value.Substring(0, Math.Min(value.Length, 28));
            }
        }
        public SchemeElement(string text)
        {
            Text = text;
        }

    }

    [Serializable]
    abstract class LinkButton : SchemeElement
    {
        public static int R { get; } = 4;
        public int X { get; set; }
        public int Y { get; set; }

        
        public LinkButton(int x, int y) : base("Link Button")
        {
            X = x;
            Y = y;
        }
        public abstract void DrawLinkButton(Bitmap srcBitmap, PictureBox srcPictureBox);
        public abstract void ClearLinkButton(Bitmap srcBitmap, PictureBox srcPictureBox);
        public bool IsUnderMouse(int xMouse, int yMouse)
        {
            Rectangle myEllipse = new Rectangle(this.X, this.Y, 2*R, 2*R);
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(myEllipse);
            if (myPath.IsVisible(new Point(xMouse, yMouse)))
            {
                return true;
            }
            return false;
        }

        public static void LinkOutToIn(Point A, Point B, OutLinkButton outLinkButton, InLinkButton inLinkButton, Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            outLinkButton.LinkedTo = inLinkButton;
            inLinkButton.LinkedTo = outLinkButton;

            outLinkButton.startLinkPoint = A;
            inLinkButton.endLinkPoint = B;

            DrawLinkOutToIn(A, B, srcBitmap, srcPictureBox);
        }

        public static void DrawLinkOutToIn(Point A, Point B, Bitmap srcBitmap, PictureBox srcPictureBox)
        {
            Graphics graphics = Graphics.FromImage(srcBitmap);
            Pen pen = new Pen(Color.Black, 2);
            AdjustableArrowCap arrow = new AdjustableArrowCap(7, 10);
            pen.CustomEndCap = arrow;
            graphics.DrawLine(pen, A, B);
            srcPictureBox.Invalidate();
        }
    }

    [Serializable]
    abstract class Block : SchemeElement
    {
        public static int ClickedBlockIndex { get; set; } = -1;
        public bool IsClicekd { get; set; } = false;
        public Block(string text) : base(text) { }

        public abstract void DrawBlockImage(Bitmap srcBitmap, PictureBox srcPictureBox);
        public abstract bool IsUnderMouse(int xMouse, int yMouse);
        public abstract void DrawClickedBlockImage(Bitmap srcBitmap, PictureBox srcPictureBox);
        public abstract void RemoveLinks();
        public abstract (bool canBeDone, OutLinkButton, InLinkButton) StartLink(int xMouse, int yMouse);
        public abstract (bool canBeDone, InLinkButton inLinkButton) EndLink(int xMouse, int yMouse, InLinkButton notAllowedInLink);
    }
}
