using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockDiagramEditor
{
    [Serializable]
    class SchemeSize : SchemeElement
    {
        public int Width { get; }
        public int Height { get; }
        public SchemeSize(int width, int height) : base("Scheme size")
        {
            Width = width;
            Height = height;
        }
    }
}
