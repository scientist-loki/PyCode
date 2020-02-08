namespace PyCode
{
    partial class wOptions
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
            this.mtab = new MetroFramework.Controls.MetroTabControl();
            this.tabFirstlocation = new MetroFramework.Controls.MetroTabPage();
            this.btnApply = new System.Windows.Forms.Button();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtLocationPath = new System.Windows.Forms.TextBox();
            this.txtPyCodePath = new System.Windows.Forms.TextBox();
            this.tabCompiler = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.btnUpdateCodeHL = new System.Windows.Forms.Button();
            this.eleHighligh = new System.Windows.Forms.Integration.ElementHost();
            this.mtcbox = new MetroFramework.Controls.MetroComboBox();
            this.mtab.SuspendLayout();
            this.tabFirstlocation.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtab
            // 
            this.mtab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.mtab.Controls.Add(this.tabFirstlocation);
            this.mtab.Controls.Add(this.tabCompiler);
            this.mtab.Controls.Add(this.metroTabPage1);
            this.mtab.Location = new System.Drawing.Point(23, 63);
            this.mtab.Multiline = true;
            this.mtab.Name = "mtab";
            this.mtab.SelectedIndex = 0;
            this.mtab.Size = new System.Drawing.Size(654, 314);
            this.mtab.Style = MetroFramework.MetroColorStyle.Purple;
            this.mtab.TabIndex = 0;
            this.mtab.UseSelectable = true;
            // 
            // tabFirstlocation
            // 
            this.tabFirstlocation.BackColor = System.Drawing.SystemColors.Control;
            this.tabFirstlocation.Controls.Add(this.mtcbox);
            this.tabFirstlocation.Controls.Add(this.btnApply);
            this.tabFirstlocation.Controls.Add(this.metroLabel2);
            this.tabFirstlocation.Controls.Add(this.metroLabel1);
            this.tabFirstlocation.Controls.Add(this.txtLocationPath);
            this.tabFirstlocation.Controls.Add(this.txtPyCodePath);
            this.tabFirstlocation.ForeColor = System.Drawing.SystemColors.Control;
            this.tabFirstlocation.HorizontalScrollbarBarColor = true;
            this.tabFirstlocation.HorizontalScrollbarHighlightOnWheel = false;
            this.tabFirstlocation.HorizontalScrollbarSize = 10;
            this.tabFirstlocation.Location = new System.Drawing.Point(4, 4);
            this.tabFirstlocation.Name = "tabFirstlocation";
            this.tabFirstlocation.Size = new System.Drawing.Size(646, 272);
            this.tabFirstlocation.TabIndex = 0;
            this.tabFirstlocation.Text = "首选项";
            this.tabFirstlocation.VerticalScrollbarBarColor = true;
            this.tabFirstlocation.VerticalScrollbarHighlightOnWheel = false;
            this.tabFirstlocation.VerticalScrollbarSize = 10;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnApply.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.Black;
            this.btnApply.Location = new System.Drawing.Point(583, 246);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(60, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.ForeColor = System.Drawing.Color.Coral;
            this.metroLabel2.Location = new System.Drawing.Point(12, 45);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(127, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Python  本地环境：";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.ForeColor = System.Drawing.Color.Coral;
            this.metroLabel1.Location = new System.Drawing.Point(12, 12);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(129, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "PyCode 编译环境：";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLocationPath
            // 
            this.txtLocationPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocationPath.Font = new System.Drawing.Font("Consolas", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocationPath.Location = new System.Drawing.Point(145, 47);
            this.txtLocationPath.Multiline = true;
            this.txtLocationPath.Name = "txtLocationPath";
            this.txtLocationPath.Size = new System.Drawing.Size(350, 17);
            this.txtLocationPath.TabIndex = 2;
            // 
            // txtPyCodePath
            // 
            this.txtPyCodePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPyCodePath.Font = new System.Drawing.Font("Consolas", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPyCodePath.Location = new System.Drawing.Point(145, 14);
            this.txtPyCodePath.Multiline = true;
            this.txtPyCodePath.Name = "txtPyCodePath";
            this.txtPyCodePath.Size = new System.Drawing.Size(350, 17);
            this.txtPyCodePath.TabIndex = 2;
            // 
            // tabCompiler
            // 
            this.tabCompiler.HorizontalScrollbarBarColor = true;
            this.tabCompiler.HorizontalScrollbarHighlightOnWheel = false;
            this.tabCompiler.HorizontalScrollbarSize = 10;
            this.tabCompiler.Location = new System.Drawing.Point(4, 4);
            this.tabCompiler.Name = "tabCompiler";
            this.tabCompiler.Size = new System.Drawing.Size(646, 272);
            this.tabCompiler.TabIndex = 1;
            this.tabCompiler.Text = "编译";
            this.tabCompiler.VerticalScrollbarBarColor = true;
            this.tabCompiler.VerticalScrollbarHighlightOnWheel = false;
            this.tabCompiler.VerticalScrollbarSize = 10;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.btnUpdateCodeHL);
            this.metroTabPage1.Controls.Add(this.eleHighligh);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 4);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(646, 272);
            this.metroTabPage1.TabIndex = 2;
            this.metroTabPage1.Text = "语法高亮解决方案";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // btnUpdateCodeHL
            // 
            this.btnUpdateCodeHL.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F);
            this.btnUpdateCodeHL.Location = new System.Drawing.Point(586, 246);
            this.btnUpdateCodeHL.Name = "btnUpdateCodeHL";
            this.btnUpdateCodeHL.Size = new System.Drawing.Size(60, 23);
            this.btnUpdateCodeHL.TabIndex = 3;
            this.btnUpdateCodeHL.Text = "Apply";
            this.btnUpdateCodeHL.UseVisualStyleBackColor = true;
            this.btnUpdateCodeHL.Click += new System.EventHandler(this.btnUpdateCodeHL_Click);
            // 
            // eleHighligh
            // 
            this.eleHighligh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eleHighligh.Location = new System.Drawing.Point(0, 0);
            this.eleHighligh.Name = "eleHighligh";
            this.eleHighligh.Size = new System.Drawing.Size(646, 240);
            this.eleHighligh.TabIndex = 2;
            this.eleHighligh.Text = "elementHost1";
            this.eleHighligh.Child = null;
            // 
            // mtcbox
            // 
            this.mtcbox.FormattingEnabled = true;
            this.mtcbox.ItemHeight = 23;
            this.mtcbox.Items.AddRange(new object[] {
            "Location Python Compiler Environment",
            "PyCode Python Compiler Environment"});
            this.mtcbox.Location = new System.Drawing.Point(12, 80);
            this.mtcbox.Name = "mtcbox";
            this.mtcbox.Size = new System.Drawing.Size(483, 29);
            this.mtcbox.TabIndex = 5;
            this.mtcbox.UseSelectable = true;
            this.mtcbox.SelectedIndexChanged += new System.EventHandler(this.mtcbox_SelectedIndexChanged);
            this.mtcbox.SelectionChangeCommitted += new System.EventHandler(this.mtcbox_SelectionChangeCommitted);
            // 
            // wOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.mtab);
            this.MaximizeBox = false;
            this.Name = "wOptions";
            this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.mtab.ResumeLayout(false);
            this.tabFirstlocation.ResumeLayout(false);
            this.tabFirstlocation.PerformLayout();
            this.metroTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl mtab;
        private MetroFramework.Controls.MetroTabPage tabFirstlocation;
        private MetroFramework.Controls.MetroTabPage tabCompiler;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.TextBox txtPyCodePath;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Integration.ElementHost eleHighligh;
        private System.Windows.Forms.Button btnUpdateCodeHL;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.TextBox txtLocationPath;
        private MetroFramework.Controls.MetroComboBox mtcbox;
    }
}