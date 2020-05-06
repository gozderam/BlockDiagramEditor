namespace BlockDiagramEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.englishButton = new System.Windows.Forms.Button();
            this.polishButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.trashButton = new System.Windows.Forms.Button();
            this.startBlockButton = new System.Windows.Forms.Button();
            this.stopBlockButton = new System.Windows.Forms.Button();
            this.linkButton = new System.Windows.Forms.Button();
            this.decisionBlockButton = new System.Windows.Forms.Button();
            this.operationBlockButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openSchemeButton = new System.Windows.Forms.Button();
            this.saveSchemeButton = new System.Windows.Forms.Button();
            this.newSchemeButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Name = "panel1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.englishButton);
            this.groupBox3.Controls.Add(this.polishButton);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // englishButton
            // 
            resources.ApplyResources(this.englishButton, "englishButton");
            this.englishButton.Name = "englishButton";
            this.englishButton.UseVisualStyleBackColor = true;
            this.englishButton.Click += new System.EventHandler(this.englishButton_Click);
            // 
            // polishButton
            // 
            resources.ApplyResources(this.polishButton, "polishButton");
            this.polishButton.Name = "polishButton";
            this.polishButton.UseVisualStyleBackColor = true;
            this.polishButton.Click += new System.EventHandler(this.polishButton_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.trashButton);
            this.groupBox2.Controls.Add(this.startBlockButton);
            this.groupBox2.Controls.Add(this.stopBlockButton);
            this.groupBox2.Controls.Add(this.linkButton);
            this.groupBox2.Controls.Add(this.decisionBlockButton);
            this.groupBox2.Controls.Add(this.operationBlockButton);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // trashButton
            // 
            this.trashButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.trashButton, "trashButton");
            this.trashButton.Name = "trashButton";
            this.trashButton.UseVisualStyleBackColor = false;
            this.trashButton.Click += new System.EventHandler(this.trashButton_Click);
            // 
            // startBlockButton
            // 
            this.startBlockButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.startBlockButton, "startBlockButton");
            this.startBlockButton.Name = "startBlockButton";
            this.startBlockButton.UseVisualStyleBackColor = false;
            this.startBlockButton.Click += new System.EventHandler(this.startBlockButton_Click);
            // 
            // stopBlockButton
            // 
            this.stopBlockButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.stopBlockButton, "stopBlockButton");
            this.stopBlockButton.Name = "stopBlockButton";
            this.stopBlockButton.UseVisualStyleBackColor = false;
            this.stopBlockButton.Click += new System.EventHandler(this.stopBlockButton_Click);
            // 
            // linkButton
            // 
            this.linkButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.linkButton, "linkButton");
            this.linkButton.Name = "linkButton";
            this.linkButton.UseVisualStyleBackColor = false;
            this.linkButton.Click += new System.EventHandler(this.linkButton_Click);
            // 
            // decisionBlockButton
            // 
            resources.ApplyResources(this.decisionBlockButton, "decisionBlockButton");
            this.decisionBlockButton.Name = "decisionBlockButton";
            this.decisionBlockButton.UseVisualStyleBackColor = true;
            this.decisionBlockButton.Click += new System.EventHandler(this.decisionBlockButton_Click);
            // 
            // operationBlockButton
            // 
            this.operationBlockButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.operationBlockButton, "operationBlockButton");
            this.operationBlockButton.Name = "operationBlockButton";
            this.operationBlockButton.UseVisualStyleBackColor = false;
            this.operationBlockButton.Click += new System.EventHandler(this.operationBlockButton_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.openSchemeButton);
            this.groupBox1.Controls.Add(this.saveSchemeButton);
            this.groupBox1.Controls.Add(this.newSchemeButton);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // openSchemeButton
            // 
            resources.ApplyResources(this.openSchemeButton, "openSchemeButton");
            this.openSchemeButton.Name = "openSchemeButton";
            this.openSchemeButton.UseVisualStyleBackColor = true;
            this.openSchemeButton.Click += new System.EventHandler(this.openSchemeButton_Click);
            // 
            // saveSchemeButton
            // 
            resources.ApplyResources(this.saveSchemeButton, "saveSchemeButton");
            this.saveSchemeButton.Name = "saveSchemeButton";
            this.saveSchemeButton.UseVisualStyleBackColor = true;
            this.saveSchemeButton.Click += new System.EventHandler(this.saveSchemeButton_Click);
            // 
            // newSchemeButton
            // 
            resources.ApplyResources(this.newSchemeButton, "newSchemeButton");
            this.newSchemeButton.Name = "newSchemeButton";
            this.newSchemeButton.UseVisualStyleBackColor = true;
            this.newSchemeButton.Click += new System.EventHandler(this.newSchemeButton_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Name = "panel2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button englishButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button decisionBlockButton;
        private System.Windows.Forms.Button operationBlockButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openSchemeButton;
        private System.Windows.Forms.Button saveSchemeButton;
        private System.Windows.Forms.Button newSchemeButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button trashButton;
        private System.Windows.Forms.Button startBlockButton;
        private System.Windows.Forms.Button stopBlockButton;
        private System.Windows.Forms.Button linkButton;
        private System.Windows.Forms.Button polishButton;
    }
}

