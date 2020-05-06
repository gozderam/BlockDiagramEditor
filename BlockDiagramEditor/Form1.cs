using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockDiagramEditor
{

    public partial class Form1 : Form
    {
        List<Block> blockList = new List<Block>();  
        Bitmap bit;

        bool isStartBlockClicked = false;
        bool isStopBlockClicked = false;
        bool isDecisionBlockClicked = false;
        bool isOperationBlockClicked = false;
        bool isLinkClicked = false;
        bool isTrashClicked = false;

        bool ifLineDraw = false;
        Point linkStart;
        Point linkEnd;
        OutLinkButton linkStartButton = null;
        InLinkButton notAllowedInLink = null;
        Block clickedBlock = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics flagGraphics = Graphics.FromImage(bit);
            flagGraphics.Clear(Color.White);
            pictureBox1.Image = bit;

            startBlockButton.Image = new Bitmap(BlockDiagramEditor.Properties.Resources.start, new Size(50, 50));
            stopBlockButton.Image = new Bitmap(BlockDiagramEditor.Properties.Resources.stop, new Size(50, 50));          
            operationBlockButton.Image = new Bitmap(BlockDiagramEditor.Properties.Resources.rect, new Size(50, 50));
            decisionBlockButton.Image = new Bitmap(BlockDiagramEditor.Properties.Resources.rhombus, new Size(50, 50));
            linkButton.Image = new Bitmap(BlockDiagramEditor.Properties.Resources.link, new Size(50, 50));
            trashButton.Image = new Bitmap(BlockDiagramEditor.Properties.Resources.trash, new Size(50, 50));
        }

        void MakeButtonClicked(Button button, ref bool flag)
        {
            isStartBlockClicked = false;
            isStopBlockClicked = false;
            isDecisionBlockClicked = false;
            isOperationBlockClicked = false;
            isLinkClicked = false;
            isTrashClicked = false;

            operationBlockButton.BackColor = Control.DefaultBackColor;
            decisionBlockButton.BackColor = Control.DefaultBackColor;
            startBlockButton.BackColor = Control.DefaultBackColor;
            stopBlockButton.BackColor = Control.DefaultBackColor;
            linkButton.BackColor = Control.DefaultBackColor;
            trashButton.BackColor = Control.DefaultBackColor;

            button.BackColor = Color.PowderBlue;
            flag = true;
        }

        private void operationBlockButton_Click(object sender, EventArgs e)
        {
            MakeButtonClicked((Button)sender, ref isOperationBlockClicked);
        }

        private void decisionBlockButton_Click(object sender, EventArgs e)
        {
            MakeButtonClicked((Button)sender, ref isDecisionBlockClicked);
        }
        private void startBlockButton_Click(object sender, EventArgs e)
        {
            MakeButtonClicked((Button)sender, ref isStartBlockClicked);
        }

        private void stopBlockButton_Click(object sender, EventArgs e)
        {
            MakeButtonClicked((Button)sender, ref isStopBlockClicked);
        }

        private void linkButton_Click(object sender, EventArgs e)
        {
            MakeButtonClicked((Button)sender, ref isLinkClicked);
        }

        private void trashButton_Click(object sender, EventArgs e)
        {
            MakeButtonClicked((Button)sender, ref isTrashClicked);
        }
   
        private void DeleteBlockFromPosition(int x, int y)
        {
            int index = -1;
            for (int i = blockList.Count - 1; i >= 0; i--)
            {
                if (blockList[i].IsUnderMouse(x, y))
                {
                    if (blockList[i] == clickedBlock)
                    {
                        //Block.ClickedBlockIndex = -1;
                        clickedBlock = null;
                        textBox1.Text = "";
                        textBox1.Enabled = false;
                    }
                    if (blockList[i] is StartBlock) StartBlock.IsPresent = false;
                    index = i;
                    break;
                   
                }
            }
            if (index == -1) return;
            blockList[index].RemoveLinks();
            blockList.RemoveAt(index);

            Graphics flagGraphics = Graphics.FromImage(bit);
            flagGraphics.Clear(Color.White);
            pictureBox1.Image = bit;
            pictureBox1.Refresh();
            UpdateScheme();
        }
  
        private void newSchemeButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Point apperancePoint = new Point(this.Location.X + (this.Width / 2) - (form2.Width / 2), this.Location.Y + (this.Height / 2) - (form2.Height / 2));
            form2.Location = apperancePoint;
            form2.ShowDialog();

            if (form2.IfApplyChanges)
            {
                NewScheme(form2.NewForm1Width, form2.NewForm1Height);
            }

        }

        private void NewScheme(int Width, int Height)
        {       
            ifLineDraw = false;
            linkStartButton = null;
            notAllowedInLink = null;
            textBox1.Text = "";
            textBox1.Enabled = false;

            StartBlock.IsPresent = false;
            blockList.Clear();       
            pictureBox1.Size = new Size(Width,  Height);
            bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics flagGraphics = Graphics.FromImage(bit);
            flagGraphics.Clear(Color.White);
            pictureBox1.Image = bit;
            pictureBox1.Refresh();
        }
  
        private void saveSchemeButton_Click(object sender, EventArgs e)
        {
            ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(Form1));
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Block Diagram File |*.diag";
            saveFileDialog1.Title = resourceManager.GetString("saveDiagramTitle");
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                // binary serialization         
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                List<SchemeElement> saveList = new List<SchemeElement>();
                saveList.Add(new SchemeSize(pictureBox1.Width, pictureBox1.Height));
                for(int i =0; i< blockList.Count; i++)
                {
                    saveList.Add(blockList[i]);
                }              
                bf.Serialize(fs, saveList);
                fs.Close();             
            }
        }

        private void openSchemeButton_Click(object sender, EventArgs e)
        {
            ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(Form1));
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {               
                openFileDialog.Filter = "Block Diagram File |*.diag";
                openFileDialog.Title = resourceManager.GetString("openDiagramTitle");
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {   
                    FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    try
                    {
                        List<SchemeElement> savedList = (List<SchemeElement>)bf.Deserialize(fs);
                        fs.Close();
                        MakeSchemeFromFile(savedList);
                    }
                    catch(SerializationException)
                    {
                        fs.Close();
                        MessageBox.Show(resourceManager.GetString("openingFileErrorText"), resourceManager.GetString("openingFileErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }          
                }
            }
        }

        void MakeSchemeFromFile(List<SchemeElement> savedList)
        {          
            SchemeSize ss = savedList[0] as SchemeSize;
            NewScheme(ss.Width, ss.Height);
            for(int i =1; i<savedList.Count; i++)
            {
                if (savedList[i] is StartBlock) StartBlock.IsPresent = true;
                Block block = savedList[i] as Block;
                block.DrawBlockImage(bit, pictureBox1);
                blockList.Add(block);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (clickedBlock !=null)
            {
                clickedBlock.Text = textBox1.Text;
                UpdateScheme();
            }
          
        }

        private void UpdateScheme()
        {         
            for (int i = 0; i < blockList.Count; i++)
            {
                if (blockList[i].IsClicekd == false) blockList[i].DrawBlockImage(bit, pictureBox1);
                else blockList[i].DrawClickedBlockImage(bit, pictureBox1);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(Form1));
                Block newBlock;
                if (isOperationBlockClicked) newBlock = new OperationBlock(e.X, e.Y, resourceManager.GetString("operationBlockText"));
                else if (isDecisionBlockClicked) newBlock = new DecisionBlock(e.X, e.Y, resourceManager.GetString("decisionBlockText"));
                else if (isStartBlockClicked)
                {
                    if (!StartBlock.IsPresent) newBlock = new StartBlock(e.X, e.Y);
                    else
                    {
                        MessageBox.Show(resourceManager.GetString("startCreatingErrorText"), resourceManager.GetString("startCreatingErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (isStopBlockClicked) newBlock = new StopBlock(e.X, e.Y);
                else if (isTrashClicked)
                {
                    DeleteBlockFromPosition(e.X, e.Y);
                    return;
                }
                else if (isLinkClicked)
                {
                    bool canBeDone = false;
                    notAllowedInLink = null;
                    for (int i = blockList.Count - 1; i >= 0; i--)
                    {
                        (canBeDone, linkStartButton, notAllowedInLink) = blockList[i].StartLink(e.X, e.Y);
                        if (canBeDone)
                        {
                            ifLineDraw = true;
                            linkEnd = linkStart = new Point(e.X, e.Y);
                            return;
                        }
                    }
                    return;
                }
                else return;
                blockList.Add(newBlock);
                newBlock.DrawBlockImage(bit, pictureBox1);
            }

            else if (e.Button == MouseButtons.Right)
            {
                Block.ClickedBlockIndex = -1;
                textBox1.Enabled = true;
                for (int i = blockList.Count - 1; i >= 0; i--)
                {
                    if (blockList[i].IsUnderMouse(e.X, e.Y))
                    {
                        blockList[i].IsClicekd = true;
                        Block.ClickedBlockIndex = i;
                        break;
                    }
                }
                if (Block.ClickedBlockIndex == -1)
                {
                    clickedBlock = null;
                    textBox1.Text = "";
                    textBox1.Enabled = false;
                   
                }
                else clickedBlock = blockList[Block.ClickedBlockIndex];
                for (int i = 0; i < blockList.Count; i++)
                {
                    if (i != Block.ClickedBlockIndex)
                    {
                        blockList[i].IsClicekd = false;
                        blockList[i].DrawBlockImage(bit, pictureBox1);
                    }
                    else
                    {
                        blockList[i].IsClicekd = true;
                        blockList[i].DrawClickedBlockImage(bit, pictureBox1);
                        textBox1.Text = blockList[i].Text;
                        if (blockList[i] is StartBlock || blockList[i] is StopBlock) textBox1.Enabled = false;
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(ifLineDraw) 
            {
                linkEnd = new Point(e.X, e.Y);
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (ifLineDraw)
            {
                Pen pen = new Pen(Color.Black, 2);
                e.Graphics.DrawLine(pen, linkStart, linkEnd);
            }
           
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {    
                if (isLinkClicked && ifLineDraw)
                {
                    bool canBeDone = false;
                    InLinkButton inLinkButton;
                    for (int i = blockList.Count - 1; i >= 0; i--)
                    {
                        (canBeDone, inLinkButton) = blockList[i].EndLink(e.X, e.Y, notAllowedInLink);
                        if (canBeDone)
                        {
                            LinkButton.LinkOutToIn(linkStart, linkEnd, linkStartButton, inLinkButton, bit, pictureBox1);
                            UpdateScheme();
                            break; 
                        }

                    }
                }

                ifLineDraw = false;
                linkEnd = linkStart;
                pictureBox1.Refresh();
            }
        }

        private void polishButton_Click(object sender, EventArgs e)
        { 
            bool ifSetEnable = false;
            int oldWidth = Width;
            int oldHeight = Height;
            if (textBox1.Enabled) ifSetEnable = true;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
            ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(Form1));
            resourceManager.ApplyResources(this, "$this");
            ImportStringResources(resourceManager, this.Controls);
            if (ifSetEnable) textBox1.Enabled = true;
            Width = oldWidth;
            Height = oldHeight;
        }

        private void englishButton_Click(object sender, EventArgs e)
        {
            bool ifSetEnable = false;
            int oldWidth = Width;
            int oldHeight = Height;
            if (textBox1.Enabled) ifSetEnable = true;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(Form1));
            resourceManager.ApplyResources(this, "$this");
            ImportStringResources(resourceManager, this.Controls);
            if (ifSetEnable) textBox1.Enabled = true;
            Width = oldWidth;
            Height = oldHeight;
        }

        private void ImportStringResources(ComponentResourceManager resourceManager, Control.ControlCollection controlsCollecion)
        {
            foreach (Control control in controlsCollecion)
            {
                if (control.Name != pictureBox1.Name)
                {
                    resourceManager.ApplyResources(control, control.Name);
                    ImportStringResources(resourceManager, control.Controls);
                }
            }
        }
       
    }



}

