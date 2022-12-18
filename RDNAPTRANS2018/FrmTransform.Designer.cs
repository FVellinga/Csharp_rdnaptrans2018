namespace RDNAPTRANS2018
{
  partial class FrmTransform
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
      this.components = new System.ComponentModel.Container();
      this.BtnToRd = new System.Windows.Forms.Button();
      this.BtnEtrsSelfVal = new System.Windows.Forms.Button();
      this.BtnEtrsVal = new System.Windows.Forms.Button();
      this.LabelGridRdCorr = new System.Windows.Forms.Label();
      this.FldGridRdCorr = new System.Windows.Forms.TextBox();
      this.BtnGridRdCorr = new System.Windows.Forms.Button();
      this.BtnGridHeight = new System.Windows.Forms.Button();
      this.FldGridHeight = new System.Windows.Forms.TextBox();
      this.LabelGridHeight = new System.Windows.Forms.Label();
      this.BtnSelfVal = new System.Windows.Forms.Button();
      this.FldSelfVal = new System.Windows.Forms.TextBox();
      this.LabelSelfVal = new System.Windows.Forms.Label();
      this.BtnEtrsValFile = new System.Windows.Forms.Button();
      this.FldEtrs89Val = new System.Windows.Forms.TextBox();
      this.LabelEtrs89Val = new System.Windows.Forms.Label();
      this.BtnRdValFile = new System.Windows.Forms.Button();
      this.FldRdVal = new System.Windows.Forms.TextBox();
      this.LabelRdVal = new System.Windows.Forms.Label();
      this.BtnToETRS = new System.Windows.Forms.Button();
      this.BtnRdSelfVal = new System.Windows.Forms.Button();
      this.BtnRdVal = new System.Windows.Forms.Button();
      this.FldRDx = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.FldRDy = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.FldNAPh = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.FldETRSh = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.FldETRSlon = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.FldETRSlat = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.WBoutput = new System.Windows.Forms.WebBrowser();
      this.label11 = new System.Windows.Forms.Label();
      this.LabelInfo = new System.Windows.Forms.Label();
      this.TT = new System.Windows.Forms.ToolTip(this.components);
      this.TMR = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // BtnToRd
      // 
      this.BtnToRd.BackColor = System.Drawing.SystemColors.Info;
      this.BtnToRd.DialogResult = System.Windows.Forms.DialogResult.No;
      this.BtnToRd.FlatAppearance.BorderSize = 0;
      this.BtnToRd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnToRd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnToRd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnToRd.Location = new System.Drawing.Point(255, 207);
      this.BtnToRd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.BtnToRd.Name = "BtnToRd";
      this.BtnToRd.Size = new System.Drawing.Size(80, 25);
      this.BtnToRd.TabIndex = 0;
      this.BtnToRd.Text = "To RD NAP";
      this.TT.SetToolTip(this.BtnToRd, "Transform to RD NAP");
      this.BtnToRd.UseVisualStyleBackColor = false;
      this.BtnToRd.Click += new System.EventHandler(this.BtnToRd_Click);
      // 
      // BtnEtrsSelfVal
      // 
      this.BtnEtrsSelfVal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnEtrsSelfVal.Location = new System.Drawing.Point(10, 144);
      this.BtnEtrsSelfVal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.BtnEtrsSelfVal.Name = "BtnEtrsSelfVal";
      this.BtnEtrsSelfVal.Size = new System.Drawing.Size(110, 25);
      this.BtnEtrsSelfVal.TabIndex = 1;
      this.BtnEtrsSelfVal.Text = "ETRS89 SV";
      this.TT.SetToolTip(this.BtnEtrsSelfVal, "Self-validation of the ETRS89 to RDNAP transformation");
      this.BtnEtrsSelfVal.UseVisualStyleBackColor = true;
      this.BtnEtrsSelfVal.Click += new System.EventHandler(this.BtnEtrsSelfVal_Click);
      // 
      // BtnEtrsVal
      // 
      this.BtnEtrsVal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnEtrsVal.Location = new System.Drawing.Point(250, 144);
      this.BtnEtrsVal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.BtnEtrsVal.Name = "BtnEtrsVal";
      this.BtnEtrsVal.Size = new System.Drawing.Size(110, 25);
      this.BtnEtrsVal.TabIndex = 2;
      this.BtnEtrsVal.Text = "ETRS89 Validation";
      this.TT.SetToolTip(this.BtnEtrsVal, "Validate the ETRS89 coordinates. For the Trademark approval. ETRS89 to RDNAP tran" +
        "sformation ");
      this.BtnEtrsVal.UseVisualStyleBackColor = true;
      this.BtnEtrsVal.Click += new System.EventHandler(this.BtnEtrsVal_Click);
      // 
      // LabelGridRdCorr
      // 
      this.LabelGridRdCorr.Location = new System.Drawing.Point(10, 20);
      this.LabelGridRdCorr.Name = "LabelGridRdCorr";
      this.LabelGridRdCorr.Size = new System.Drawing.Size(120, 19);
      this.LabelGridRdCorr.TabIndex = 3;
      this.LabelGridRdCorr.Text = "RD correction grid";
      this.LabelGridRdCorr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.LabelGridRdCorr, "The location of the RD correction grid");
      this.LabelGridRdCorr.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // FldGridRdCorr
      // 
      this.FldGridRdCorr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.FldGridRdCorr.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldGridRdCorr.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldGridRdCorr.Location = new System.Drawing.Point(131, 20);
      this.FldGridRdCorr.MaxLength = 255;
      this.FldGridRdCorr.Multiline = true;
      this.FldGridRdCorr.Name = "FldGridRdCorr";
      this.FldGridRdCorr.Size = new System.Drawing.Size(397, 19);
      this.FldGridRdCorr.TabIndex = 4;
      this.FldGridRdCorr.Text = "C:\\Werk\\RDNAPTRANS\\rdcorr2018.txt";
      this.TT.SetToolTip(this.FldGridRdCorr, "The location of the RD correction grid");
      this.FldGridRdCorr.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // BtnGridRdCorr
      // 
      this.BtnGridRdCorr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnGridRdCorr.BackColor = System.Drawing.Color.LightGray;
      this.BtnGridRdCorr.FlatAppearance.BorderSize = 0;
      this.BtnGridRdCorr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnGridRdCorr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnGridRdCorr.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnGridRdCorr.ForeColor = System.Drawing.Color.Red;
      this.BtnGridRdCorr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BtnGridRdCorr.Location = new System.Drawing.Point(537, 20);
      this.BtnGridRdCorr.Name = "BtnGridRdCorr";
      this.BtnGridRdCorr.Size = new System.Drawing.Size(50, 19);
      this.BtnGridRdCorr.TabIndex = 5;
      this.BtnGridRdCorr.Text = "Load";
      this.TT.SetToolTip(this.BtnGridRdCorr, "Load the RD correction grid into memory");
      this.BtnGridRdCorr.UseVisualStyleBackColor = false;
      this.BtnGridRdCorr.Click += new System.EventHandler(this.BtnGridRdCorr_Click);
      // 
      // BtnGridHeight
      // 
      this.BtnGridHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnGridHeight.BackColor = System.Drawing.Color.LightGray;
      this.BtnGridHeight.FlatAppearance.BorderSize = 0;
      this.BtnGridHeight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnGridHeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnGridHeight.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnGridHeight.ForeColor = System.Drawing.Color.Red;
      this.BtnGridHeight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BtnGridHeight.Location = new System.Drawing.Point(537, 40);
      this.BtnGridHeight.Name = "BtnGridHeight";
      this.BtnGridHeight.Size = new System.Drawing.Size(50, 19);
      this.BtnGridHeight.TabIndex = 8;
      this.BtnGridHeight.Text = "Load";
      this.TT.SetToolTip(this.BtnGridHeight, "Load the NAP quasi-geoid height grid into memory");
      this.BtnGridHeight.UseVisualStyleBackColor = false;
      this.BtnGridHeight.Click += new System.EventHandler(this.BtnGridRdCorr_Click);
      // 
      // FldGridHeight
      // 
      this.FldGridHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.FldGridHeight.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
      this.FldGridHeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldGridHeight.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldGridHeight.Location = new System.Drawing.Point(131, 40);
      this.FldGridHeight.MaxLength = 255;
      this.FldGridHeight.Multiline = true;
      this.FldGridHeight.Name = "FldGridHeight";
      this.FldGridHeight.Size = new System.Drawing.Size(397, 19);
      this.FldGridHeight.TabIndex = 7;
      this.FldGridHeight.Text = "C:\\Werk\\RDNAPTRANS\\nlgeo2018.txt";
      this.TT.SetToolTip(this.FldGridHeight, "The location of the NAP quasi-geoid height grid");
      this.FldGridHeight.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // LabelGridHeight
      // 
      this.LabelGridHeight.Location = new System.Drawing.Point(10, 40);
      this.LabelGridHeight.Name = "LabelGridHeight";
      this.LabelGridHeight.Size = new System.Drawing.Size(120, 19);
      this.LabelGridHeight.TabIndex = 6;
      this.LabelGridHeight.Text = "Height grid";
      this.LabelGridHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.LabelGridHeight, "The location of the NAP quasi-geoid height grid");
      this.LabelGridHeight.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // BtnSelfVal
      // 
      this.BtnSelfVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSelfVal.BackColor = System.Drawing.Color.LightGray;
      this.BtnSelfVal.FlatAppearance.BorderSize = 0;
      this.BtnSelfVal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnSelfVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnSelfVal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnSelfVal.ForeColor = System.Drawing.Color.Red;
      this.BtnSelfVal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BtnSelfVal.Location = new System.Drawing.Point(537, 60);
      this.BtnSelfVal.Name = "BtnSelfVal";
      this.BtnSelfVal.Size = new System.Drawing.Size(50, 19);
      this.BtnSelfVal.TabIndex = 11;
      this.BtnSelfVal.Text = "Load";
      this.TT.SetToolTip(this.BtnSelfVal, "Load the self-validation file into memory");
      this.BtnSelfVal.UseVisualStyleBackColor = false;
      this.BtnSelfVal.Click += new System.EventHandler(this.BtnGridRdCorr_Click);
      // 
      // FldSelfVal
      // 
      this.FldSelfVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.FldSelfVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldSelfVal.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldSelfVal.Location = new System.Drawing.Point(131, 60);
      this.FldSelfVal.MaxLength = 255;
      this.FldSelfVal.Multiline = true;
      this.FldSelfVal.Name = "FldSelfVal";
      this.FldSelfVal.Size = new System.Drawing.Size(397, 19);
      this.FldSelfVal.TabIndex = 10;
      this.FldSelfVal.Text = "C:\\Werk\\RDNAPTRANS\\Z001_ETRS89andRDNAP.txt";
      this.TT.SetToolTip(this.FldSelfVal, "The location of the self-validation file");
      this.FldSelfVal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // LabelSelfVal
      // 
      this.LabelSelfVal.Location = new System.Drawing.Point(10, 60);
      this.LabelSelfVal.Name = "LabelSelfVal";
      this.LabelSelfVal.Size = new System.Drawing.Size(120, 19);
      this.LabelSelfVal.TabIndex = 9;
      this.LabelSelfVal.Text = "Self-validation";
      this.LabelSelfVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.LabelSelfVal, "The location of the self-validation file");
      this.LabelSelfVal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // BtnEtrsValFile
      // 
      this.BtnEtrsValFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnEtrsValFile.BackColor = System.Drawing.Color.LightGray;
      this.BtnEtrsValFile.FlatAppearance.BorderSize = 0;
      this.BtnEtrsValFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnEtrsValFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnEtrsValFile.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnEtrsValFile.ForeColor = System.Drawing.Color.Red;
      this.BtnEtrsValFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BtnEtrsValFile.Location = new System.Drawing.Point(537, 80);
      this.BtnEtrsValFile.Name = "BtnEtrsValFile";
      this.BtnEtrsValFile.Size = new System.Drawing.Size(50, 19);
      this.BtnEtrsValFile.TabIndex = 14;
      this.BtnEtrsValFile.Text = "Load";
      this.TT.SetToolTip(this.BtnEtrsValFile, "Load the ETRS89 certify validation file (ETRS89 to RDNAP) into memory");
      this.BtnEtrsValFile.UseVisualStyleBackColor = false;
      this.BtnEtrsValFile.Click += new System.EventHandler(this.BtnGridRdCorr_Click);
      // 
      // FldEtrs89Val
      // 
      this.FldEtrs89Val.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.FldEtrs89Val.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldEtrs89Val.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldEtrs89Val.Location = new System.Drawing.Point(131, 80);
      this.FldEtrs89Val.MaxLength = 255;
      this.FldEtrs89Val.Multiline = true;
      this.FldEtrs89Val.Name = "FldEtrs89Val";
      this.FldEtrs89Val.Size = new System.Drawing.Size(397, 19);
      this.FldEtrs89Val.TabIndex = 13;
      this.FldEtrs89Val.Text = "C:\\Werk\\RDNAPTRANS\\002_ETRS89.txt";
      this.TT.SetToolTip(this.FldEtrs89Val, "The location of the ETRS89 certify validation file. (ETRS89 to RDNAP)");
      this.FldEtrs89Val.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // LabelEtrs89Val
      // 
      this.LabelEtrs89Val.Location = new System.Drawing.Point(10, 80);
      this.LabelEtrs89Val.Name = "LabelEtrs89Val";
      this.LabelEtrs89Val.Size = new System.Drawing.Size(120, 19);
      this.LabelEtrs89Val.TabIndex = 12;
      this.LabelEtrs89Val.Text = "ETRS89 validation";
      this.LabelEtrs89Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.LabelEtrs89Val, "The location of the ETRS89 certify validation file. (ETRS89 to RDNAP)");
      this.LabelEtrs89Val.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // BtnRdValFile
      // 
      this.BtnRdValFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnRdValFile.BackColor = System.Drawing.Color.LightGray;
      this.BtnRdValFile.FlatAppearance.BorderSize = 0;
      this.BtnRdValFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnRdValFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnRdValFile.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnRdValFile.ForeColor = System.Drawing.Color.Red;
      this.BtnRdValFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BtnRdValFile.Location = new System.Drawing.Point(537, 100);
      this.BtnRdValFile.Name = "BtnRdValFile";
      this.BtnRdValFile.Size = new System.Drawing.Size(50, 19);
      this.BtnRdValFile.TabIndex = 17;
      this.BtnRdValFile.Text = "Load";
      this.TT.SetToolTip(this.BtnRdValFile, "Load the RDNAP certify validation file (RDNAP to ETRS89) into memory");
      this.BtnRdValFile.UseVisualStyleBackColor = false;
      this.BtnRdValFile.Click += new System.EventHandler(this.BtnGridRdCorr_Click);
      // 
      // FldRdVal
      // 
      this.FldRdVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.FldRdVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldRdVal.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldRdVal.Location = new System.Drawing.Point(131, 100);
      this.FldRdVal.MaxLength = 255;
      this.FldRdVal.Multiline = true;
      this.FldRdVal.Name = "FldRdVal";
      this.FldRdVal.Size = new System.Drawing.Size(397, 19);
      this.FldRdVal.TabIndex = 16;
      this.FldRdVal.Text = "C:\\Werk\\RDNAPTRANS\\002_RDNAP.txt";
      this.TT.SetToolTip(this.FldRdVal, "The location of the RDNAP certify validation file. (RDNAP to ETRS89)");
      this.FldRdVal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // LabelRdVal
      // 
      this.LabelRdVal.Location = new System.Drawing.Point(10, 100);
      this.LabelRdVal.Name = "LabelRdVal";
      this.LabelRdVal.Size = new System.Drawing.Size(120, 19);
      this.LabelRdVal.TabIndex = 15;
      this.LabelRdVal.Text = "RDNAP validation";
      this.LabelRdVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.LabelRdVal, "The location of the RDNAP certify validation file. (RDNAP to ETRS89)");
      this.LabelRdVal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FldGridRdCorr_MouseDoubleClick);
      // 
      // BtnToETRS
      // 
      this.BtnToETRS.BackColor = System.Drawing.SystemColors.Info;
      this.BtnToETRS.FlatAppearance.BorderSize = 0;
      this.BtnToETRS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
      this.BtnToETRS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnToETRS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnToETRS.Location = new System.Drawing.Point(255, 241);
      this.BtnToETRS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.BtnToETRS.Name = "BtnToETRS";
      this.BtnToETRS.Size = new System.Drawing.Size(80, 25);
      this.BtnToETRS.TabIndex = 19;
      this.BtnToETRS.Text = "To ETRS89";
      this.TT.SetToolTip(this.BtnToETRS, "Transform to ETRS89");
      this.BtnToETRS.UseVisualStyleBackColor = false;
      this.BtnToETRS.Click += new System.EventHandler(this.BtnToETRS_Click);
      // 
      // BtnRdSelfVal
      // 
      this.BtnRdSelfVal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnRdSelfVal.Location = new System.Drawing.Point(130, 144);
      this.BtnRdSelfVal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.BtnRdSelfVal.Name = "BtnRdSelfVal";
      this.BtnRdSelfVal.Size = new System.Drawing.Size(110, 25);
      this.BtnRdSelfVal.TabIndex = 20;
      this.BtnRdSelfVal.Text = "RDNAP SV";
      this.TT.SetToolTip(this.BtnRdSelfVal, "Self-validation of the RDNAP to ETRS89 transformation");
      this.BtnRdSelfVal.UseVisualStyleBackColor = true;
      this.BtnRdSelfVal.Click += new System.EventHandler(this.BtnRdSelfVal_Click);
      // 
      // BtnRdVal
      // 
      this.BtnRdVal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BtnRdVal.Location = new System.Drawing.Point(370, 144);
      this.BtnRdVal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.BtnRdVal.Name = "BtnRdVal";
      this.BtnRdVal.Size = new System.Drawing.Size(110, 25);
      this.BtnRdVal.TabIndex = 21;
      this.BtnRdVal.Text = "RDNAP Validation";
      this.TT.SetToolTip(this.BtnRdVal, "Validate the RDNAP coordinates. For the Trademark approval. RDNAP to ETRS89 trans" +
        "formation ");
      this.BtnRdVal.UseVisualStyleBackColor = true;
      this.BtnRdVal.Click += new System.EventHandler(this.BtnRdVal_Click);
      // 
      // FldRDx
      // 
      this.FldRDx.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldRDx.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldRDx.Location = new System.Drawing.Point(101, 207);
      this.FldRDx.MaxLength = 32;
      this.FldRDx.Multiline = true;
      this.FldRDx.Name = "FldRDx";
      this.FldRDx.Size = new System.Drawing.Size(150, 19);
      this.FldRDx.TabIndex = 23;
      this.TT.SetToolTip(this.FldRDx, "The RD x coordinate (meters)");
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(10, 207);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(95, 19);
      this.label5.TabIndex = 22;
      this.label5.Text = "RD x (lon)";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.label5, "The RD x coordinate (meters)");
      // 
      // FldRDy
      // 
      this.FldRDy.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldRDy.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldRDy.Location = new System.Drawing.Point(101, 227);
      this.FldRDy.MaxLength = 32;
      this.FldRDy.Multiline = true;
      this.FldRDy.Name = "FldRDy";
      this.FldRDy.Size = new System.Drawing.Size(150, 19);
      this.FldRDy.TabIndex = 25;
      this.TT.SetToolTip(this.FldRDy, "The RD y coordinate (meters)");
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(10, 227);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(95, 19);
      this.label6.TabIndex = 24;
      this.label6.Text = "RD y (lat)";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.label6, "The RD y coordinate (meters)");
      // 
      // FldNAPh
      // 
      this.FldNAPh.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldNAPh.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldNAPh.Location = new System.Drawing.Point(101, 247);
      this.FldNAPh.MaxLength = 32;
      this.FldNAPh.Multiline = true;
      this.FldNAPh.Name = "FldNAPh";
      this.FldNAPh.Size = new System.Drawing.Size(150, 19);
      this.FldNAPh.TabIndex = 27;
      this.TT.SetToolTip(this.FldNAPh, "The NAP height (meters)");
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(10, 247);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(95, 19);
      this.label7.TabIndex = 26;
      this.label7.Text = "NAP height";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.label7, "The NAP height (meters)");
      // 
      // FldETRSh
      // 
      this.FldETRSh.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldETRSh.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldETRSh.Location = new System.Drawing.Point(436, 247);
      this.FldETRSh.MaxLength = 32;
      this.FldETRSh.Multiline = true;
      this.FldETRSh.Name = "FldETRSh";
      this.FldETRSh.Size = new System.Drawing.Size(150, 19);
      this.FldETRSh.TabIndex = 33;
      this.TT.SetToolTip(this.FldETRSh, "The ETRS89 height (meters)");
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(340, 247);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(95, 19);
      this.label8.TabIndex = 32;
      this.label8.Text = "ETRS89 h";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.label8, "The ETRS89 height (meters)");
      // 
      // FldETRSlon
      // 
      this.FldETRSlon.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldETRSlon.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldETRSlon.Location = new System.Drawing.Point(436, 227);
      this.FldETRSlon.MaxLength = 32;
      this.FldETRSlon.Multiline = true;
      this.FldETRSlon.Name = "FldETRSlon";
      this.FldETRSlon.Size = new System.Drawing.Size(150, 19);
      this.FldETRSlon.TabIndex = 31;
      this.TT.SetToolTip(this.FldETRSlon, "The ETRS89 longitude (decimal degrees)");
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(340, 227);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(95, 19);
      this.label9.TabIndex = 30;
      this.label9.Text = "ETRS89 lon (x)";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.label9, "The ETRS89 longitude (decimal degrees)");
      // 
      // FldETRSlat
      // 
      this.FldETRSlat.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.FldETRSlat.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FldETRSlat.Location = new System.Drawing.Point(436, 207);
      this.FldETRSlat.MaxLength = 32;
      this.FldETRSlat.Multiline = true;
      this.FldETRSlat.Name = "FldETRSlat";
      this.FldETRSlat.Size = new System.Drawing.Size(150, 19);
      this.FldETRSlat.TabIndex = 29;
      this.TT.SetToolTip(this.FldETRSlat, "The ETRS89 latitude (decimal degrees)");
      // 
      // label10
      // 
      this.label10.Location = new System.Drawing.Point(340, 207);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(95, 19);
      this.label10.TabIndex = 28;
      this.label10.Text = "ETRS89 lat (y)";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TT.SetToolTip(this.label10, "The ETRS89 latitude (decimal degrees)");
      // 
      // WBoutput
      // 
      this.WBoutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.WBoutput.Location = new System.Drawing.Point(0, 275);
      this.WBoutput.MinimumSize = new System.Drawing.Size(20, 20);
      this.WBoutput.Name = "WBoutput";
      this.WBoutput.Size = new System.Drawing.Size(597, 345);
      this.WBoutput.TabIndex = 34;
      this.WBoutput.Url = new System.Uri("http://local", System.UriKind.Absolute);
      // 
      // label11
      // 
      this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label11.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.label11.Location = new System.Drawing.Point(0, 185);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(601, 5);
      this.label11.TabIndex = 35;
      this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // LabelInfo
      // 
      this.LabelInfo.AutoSize = true;
      this.LabelInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.LabelInfo.Location = new System.Drawing.Point(131, 120);
      this.LabelInfo.Name = "LabelInfo";
      this.LabelInfo.Size = new System.Drawing.Size(334, 13);
      this.LabelInfo.TabIndex = 36;
      this.LabelInfo.Text = "(Double-click on any label or text field above diplays help info)";
      this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // TMR
      // 
      this.TMR.Enabled = true;
      this.TMR.Tick += new System.EventHandler(this.TMR_Tick);
      // 
      // FrmTransform
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(598, 627);
      this.Controls.Add(this.LabelInfo);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.WBoutput);
      this.Controls.Add(this.FldETRSh);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.FldETRSlon);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.FldETRSlat);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.FldNAPh);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.FldRDy);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.FldRDx);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.BtnRdVal);
      this.Controls.Add(this.BtnRdSelfVal);
      this.Controls.Add(this.BtnToETRS);
      this.Controls.Add(this.BtnRdValFile);
      this.Controls.Add(this.FldRdVal);
      this.Controls.Add(this.LabelRdVal);
      this.Controls.Add(this.BtnEtrsValFile);
      this.Controls.Add(this.FldEtrs89Val);
      this.Controls.Add(this.LabelEtrs89Val);
      this.Controls.Add(this.BtnSelfVal);
      this.Controls.Add(this.FldSelfVal);
      this.Controls.Add(this.LabelSelfVal);
      this.Controls.Add(this.BtnGridHeight);
      this.Controls.Add(this.FldGridHeight);
      this.Controls.Add(this.LabelGridHeight);
      this.Controls.Add(this.BtnGridRdCorr);
      this.Controls.Add(this.FldGridRdCorr);
      this.Controls.Add(this.LabelGridRdCorr);
      this.Controls.Add(this.BtnEtrsVal);
      this.Controls.Add(this.BtnEtrsSelfVal);
      this.Controls.Add(this.BtnToRd);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "FrmTransform";
      this.Text = "C# RDNAPTRANS™2018";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTransform_FormClosing);
      this.Load += new System.EventHandler(this.FrmTransform_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button BtnToRd;
    private System.Windows.Forms.Button BtnEtrsSelfVal;
    private System.Windows.Forms.Button BtnEtrsVal;
    private System.Windows.Forms.Label LabelGridRdCorr;
    private System.Windows.Forms.TextBox FldGridRdCorr;
    private System.Windows.Forms.Button BtnGridRdCorr;
    private System.Windows.Forms.Button BtnGridHeight;
    private System.Windows.Forms.TextBox FldGridHeight;
    private System.Windows.Forms.Label LabelGridHeight;
    private System.Windows.Forms.Button BtnSelfVal;
    private System.Windows.Forms.TextBox FldSelfVal;
    private System.Windows.Forms.Label LabelSelfVal;
    private System.Windows.Forms.Button BtnEtrsValFile;
    private System.Windows.Forms.TextBox FldEtrs89Val;
    private System.Windows.Forms.Label LabelEtrs89Val;
    private System.Windows.Forms.Button BtnRdValFile;
    private System.Windows.Forms.TextBox FldRdVal;
    private System.Windows.Forms.Label LabelRdVal;
    private System.Windows.Forms.Button BtnToETRS;
    private System.Windows.Forms.Button BtnRdSelfVal;
    private System.Windows.Forms.Button BtnRdVal;
    private System.Windows.Forms.TextBox FldRDx;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox FldRDy;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox FldNAPh;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox FldETRSh;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox FldETRSlon;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox FldETRSlat;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.WebBrowser WBoutput;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label LabelInfo;
    private System.Windows.Forms.ToolTip TT;
    private System.Windows.Forms.Timer TMR;
  }
}

