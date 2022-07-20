namespace DummyFileCreator.App;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.outputBrowsePanel = new System.Windows.Forms.Panel();
            this.textBoxOfOutputPath = new System.Windows.Forms.TextBox();
            this.buttonToBrowseOutputPath = new System.Windows.Forms.Button();
            this.buttonToCreate = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.numericUpDownOfOutputSize = new System.Windows.Forms.NumericUpDown();
            this.comboBoxOfOutputSize = new System.Windows.Forms.ComboBox();
            this.panelOfOutputSize = new System.Windows.Forms.Panel();
            this.panelOfBufferSize = new System.Windows.Forms.Panel();
            this.numericUpDownOfBufferSize = new System.Windows.Forms.NumericUpDown();
            this.comboBoxOfBufferSize = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelOfOutputSize = new System.Windows.Forms.Label();
            this.labelOfOutputPath = new System.Windows.Forms.Label();
            this.labelOfBufferSize = new System.Windows.Forms.Label();
            this.labelOfOutputType = new System.Windows.Forms.Label();
            this.panelOfOutputType = new System.Windows.Forms.Panel();
            this.radioButtonOfZeros = new System.Windows.Forms.RadioButton();
            this.radioButtonOfRandom = new System.Windows.Forms.RadioButton();
            this.outputBrowsePanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOfOutputSize)).BeginInit();
            this.panelOfOutputSize.SuspendLayout();
            this.panelOfBufferSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOfBufferSize)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelOfOutputType.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputBrowsePanel
            // 
            this.outputBrowsePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBrowsePanel.Controls.Add(this.textBoxOfOutputPath);
            this.outputBrowsePanel.Controls.Add(this.buttonToBrowseOutputPath);
            this.outputBrowsePanel.Location = new System.Drawing.Point(109, 3);
            this.outputBrowsePanel.Name = "outputBrowsePanel";
            this.outputBrowsePanel.Size = new System.Drawing.Size(422, 23);
            this.outputBrowsePanel.TabIndex = 14;
            // 
            // textBoxOfOutputPath
            // 
            this.textBoxOfOutputPath.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxOfOutputPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOfOutputPath.Location = new System.Drawing.Point(0, 0);
            this.textBoxOfOutputPath.Name = "textBoxOfOutputPath";
            this.textBoxOfOutputPath.ReadOnly = true;
            this.textBoxOfOutputPath.Size = new System.Drawing.Size(396, 23);
            this.textBoxOfOutputPath.TabIndex = 0;
            // 
            // buttonToBrowseOutputPath
            // 
            this.buttonToBrowseOutputPath.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonToBrowseOutputPath.Location = new System.Drawing.Point(396, 0);
            this.buttonToBrowseOutputPath.Name = "buttonToBrowseOutputPath";
            this.buttonToBrowseOutputPath.Size = new System.Drawing.Size(26, 23);
            this.buttonToBrowseOutputPath.TabIndex = 1;
            this.buttonToBrowseOutputPath.Text = "...";
            this.buttonToBrowseOutputPath.UseVisualStyleBackColor = true;
            // 
            // buttonToCreate
            // 
            this.buttonToCreate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonToCreate.Location = new System.Drawing.Point(396, 133);
            this.buttonToCreate.Name = "buttonToCreate";
            this.buttonToCreate.Size = new System.Drawing.Size(135, 23);
            this.buttonToCreate.TabIndex = 15;
            this.buttonToCreate.Text = "ダミーファイル生成";
            this.buttonToCreate.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 159);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(534, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(417, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // numericUpDownOfOutputSize
            // 
            this.numericUpDownOfOutputSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownOfOutputSize.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownOfOutputSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownOfOutputSize.Name = "numericUpDownOfOutputSize";
            this.numericUpDownOfOutputSize.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownOfOutputSize.TabIndex = 17;
            this.numericUpDownOfOutputSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownOfOutputSize.ThousandsSeparator = true;
            this.numericUpDownOfOutputSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // comboBoxOfOutputSize
            // 
            this.comboBoxOfOutputSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBoxOfOutputSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOfOutputSize.FormattingEnabled = true;
            this.comboBoxOfOutputSize.Items.AddRange(new object[] {
            "B",
            "KB",
            "MB",
            "GB"});
            this.comboBoxOfOutputSize.Location = new System.Drawing.Point(66, 0);
            this.comboBoxOfOutputSize.Name = "comboBoxOfOutputSize";
            this.comboBoxOfOutputSize.Size = new System.Drawing.Size(69, 23);
            this.comboBoxOfOutputSize.TabIndex = 18;
            // 
            // panelOfOutputSize
            // 
            this.panelOfOutputSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panelOfOutputSize.Controls.Add(this.numericUpDownOfOutputSize);
            this.panelOfOutputSize.Controls.Add(this.comboBoxOfOutputSize);
            this.panelOfOutputSize.Location = new System.Drawing.Point(109, 32);
            this.panelOfOutputSize.Name = "panelOfOutputSize";
            this.panelOfOutputSize.Size = new System.Drawing.Size(135, 23);
            this.panelOfOutputSize.TabIndex = 19;
            // 
            // panelOfBufferSize
            // 
            this.panelOfBufferSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panelOfBufferSize.Controls.Add(this.numericUpDownOfBufferSize);
            this.panelOfBufferSize.Controls.Add(this.comboBoxOfBufferSize);
            this.panelOfBufferSize.Location = new System.Drawing.Point(109, 61);
            this.panelOfBufferSize.Name = "panelOfBufferSize";
            this.panelOfBufferSize.Size = new System.Drawing.Size(135, 23);
            this.panelOfBufferSize.TabIndex = 20;
            // 
            // numericUpDownOfBufferSize
            // 
            this.numericUpDownOfBufferSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownOfBufferSize.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownOfBufferSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownOfBufferSize.Name = "numericUpDownOfBufferSize";
            this.numericUpDownOfBufferSize.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownOfBufferSize.TabIndex = 17;
            this.numericUpDownOfBufferSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownOfBufferSize.ThousandsSeparator = true;
            this.numericUpDownOfBufferSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // comboBoxOfBufferSize
            // 
            this.comboBoxOfBufferSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBoxOfBufferSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOfBufferSize.FormattingEnabled = true;
            this.comboBoxOfBufferSize.Items.AddRange(new object[] {
            "KB",
            "MB"});
            this.comboBoxOfBufferSize.Location = new System.Drawing.Point(66, 0);
            this.comboBoxOfBufferSize.Name = "comboBoxOfBufferSize";
            this.comboBoxOfBufferSize.Size = new System.Drawing.Size(69, 23);
            this.comboBoxOfBufferSize.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelOfOutputSize, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.outputBrowsePanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelOfOutputSize, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelOfOutputPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelOfBufferSize, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelOfBufferSize, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelOfOutputType, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panelOfOutputType, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonToCreate, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 159);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // labelOfOutputSize
            // 
            this.labelOfOutputSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOfOutputSize.Location = new System.Drawing.Point(3, 34);
            this.labelOfOutputSize.Name = "labelOfOutputSize";
            this.labelOfOutputSize.Size = new System.Drawing.Size(100, 19);
            this.labelOfOutputSize.TabIndex = 22;
            this.labelOfOutputSize.Text = "出力サイズ";
            this.labelOfOutputSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOfOutputPath
            // 
            this.labelOfOutputPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOfOutputPath.Location = new System.Drawing.Point(3, 3);
            this.labelOfOutputPath.Name = "labelOfOutputPath";
            this.labelOfOutputPath.Size = new System.Drawing.Size(100, 23);
            this.labelOfOutputPath.TabIndex = 21;
            this.labelOfOutputPath.Text = "出力先パス";
            this.labelOfOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOfBufferSize
            // 
            this.labelOfBufferSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOfBufferSize.Location = new System.Drawing.Point(3, 63);
            this.labelOfBufferSize.Name = "labelOfBufferSize";
            this.labelOfBufferSize.Size = new System.Drawing.Size(100, 19);
            this.labelOfBufferSize.TabIndex = 23;
            this.labelOfBufferSize.Text = "バッファサイズ";
            this.labelOfBufferSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOfOutputType
            // 
            this.labelOfOutputType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOfOutputType.Location = new System.Drawing.Point(3, 92);
            this.labelOfOutputType.Name = "labelOfOutputType";
            this.labelOfOutputType.Size = new System.Drawing.Size(100, 19);
            this.labelOfOutputType.TabIndex = 24;
            this.labelOfOutputType.Text = "出力データ種別";
            this.labelOfOutputType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelOfOutputType
            // 
            this.panelOfOutputType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOfOutputType.Controls.Add(this.radioButtonOfZeros);
            this.panelOfOutputType.Controls.Add(this.radioButtonOfRandom);
            this.panelOfOutputType.Location = new System.Drawing.Point(109, 90);
            this.panelOfOutputType.Name = "panelOfOutputType";
            this.panelOfOutputType.Size = new System.Drawing.Size(422, 23);
            this.panelOfOutputType.TabIndex = 25;
            // 
            // radioButtonOfZeros
            // 
            this.radioButtonOfZeros.AutoSize = true;
            this.radioButtonOfZeros.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButtonOfZeros.Location = new System.Drawing.Point(86, 0);
            this.radioButtonOfZeros.Name = "radioButtonOfZeros";
            this.radioButtonOfZeros.Size = new System.Drawing.Size(78, 23);
            this.radioButtonOfZeros.TabIndex = 1;
            this.radioButtonOfZeros.TabStop = true;
            this.radioButtonOfZeros.Text = "0x00 データ";
            this.radioButtonOfZeros.UseVisualStyleBackColor = true;
            // 
            // radioButtonOfRandom
            // 
            this.radioButtonOfRandom.AutoSize = true;
            this.radioButtonOfRandom.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButtonOfRandom.Location = new System.Drawing.Point(0, 0);
            this.radioButtonOfRandom.Name = "radioButtonOfRandom";
            this.radioButtonOfRandom.Size = new System.Drawing.Size(86, 23);
            this.radioButtonOfRandom.TabIndex = 0;
            this.radioButtonOfRandom.TabStop = true;
            this.radioButtonOfRandom.Text = "ランダムデータ";
            this.radioButtonOfRandom.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 181);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 220);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Dummy File Creator";
            this.outputBrowsePanel.ResumeLayout(false);
            this.outputBrowsePanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOfOutputSize)).EndInit();
            this.panelOfOutputSize.ResumeLayout(false);
            this.panelOfBufferSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOfBufferSize)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelOfOutputType.ResumeLayout(false);
            this.panelOfOutputType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Panel outputBrowsePanel;
    private TextBox textBoxOfOutputPath;
    private Button buttonToBrowseOutputPath;
    private Button buttonToCreate;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripProgressBar toolStripProgressBar1;
    private NumericUpDown numericUpDownOfOutputSize;
    private ComboBox comboBoxOfOutputSize;
    private Panel panelOfOutputSize;
    private Panel panelOfBufferSize;
    private NumericUpDown numericUpDownOfBufferSize;
    private ComboBox comboBoxOfBufferSize;
    private TableLayoutPanel tableLayoutPanel1;
    private Label labelOfOutputSize;
    private Label labelOfOutputPath;
    private Label labelOfOutputType;
    private Label labelOfBufferSize;
    private Panel panelOfOutputType;
    private RadioButton radioButtonOfZeros;
    private RadioButton radioButtonOfRandom;
}
