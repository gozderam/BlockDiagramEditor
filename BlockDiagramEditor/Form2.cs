using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockDiagramEditor
{
    public partial class Form2 : Form
    {
        public int NewForm1Width { get; private set; } = 500;
        public int NewForm1Height { get; private set; } = 500;
        public bool IfApplyChanges { get; private set; } = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            NewForm1Width = Decimal.ToInt32(widthNumeric.Value);
            NewForm1Height = Decimal.ToInt32(heightNumeric.Value);
            IfApplyChanges = true;
            Close();
        }
    }
}
