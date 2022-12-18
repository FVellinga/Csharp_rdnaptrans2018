using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace RDNAPTRANS2018
{
  public partial class FrmTransform : Form
  {

    #region private variables

    /// <summary>
    /// RDNAPTRAN2018 object containing the transformation functions
    /// </summary>
    private readonly ClsRDnaptrans ObjRdNap;
    /// <summary>
    /// The decimal separator set on the local computer. In the Netherlands a ','.
    /// </summary>
    private readonly string NumberDecimalSeparator;
    /// <summary>
    /// The thousand group separator set on the local computer. In ther Netherlands a '.'
    /// </summary>
    private readonly string NumberGroupSeparator;

    #endregion

    #region constructor

    /// <summary>
    /// Initialize the FormClass
    /// </summary>
    public FrmTransform() {
      InitializeComponent();
      ObjRdNap = new ClsRDnaptrans();
      NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
      NumberGroupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
    } // FrmTransform()

    #endregion

    /// <summary>
    /// Run when the form is loaded
    /// </summary>
    private void FrmTransform_Load(object sender, EventArgs e) {
      this.Cursor = Cursors.WaitCursor;
      Load_settings();
      Run_grid_button(this.BtnGridRdCorr);
      Run_grid_button(this.BtnGridHeight);
      TMR.Enabled = true;                  // timer needed to bypass a display_info() issue in case the input files are not loaded
      this.Cursor = Cursors.Default;
    } // FrmTransform_Load

    /// <summary>
    /// Timer to bypass certain issues with the Webbrowser control
    /// </summary>
    private void TMR_Tick(object sender, EventArgs e) {
      TMR.Enabled = false;
      Display_info();
    } // TMR_Tick()

    /// <summary>
    /// Display general user info about using this application
    /// </summary>
    private void Display_info() {
      StringBuilder SB = new StringBuilder();
      SB.Append("<!DOCTYPE html><html><head><style>");
      SB.Append("body {font-family: consolas;font-size:15px}");
      SB.Append("</style></head><body>");
      SB.Append("<p>Loading the <strong>RD correction Grid File</strong> and <strong>height Grid File </strong> are mandatory. ");
      SB.Append("The other three are only needed to do the <i>Transformation Self-Validation</i> or the <i>Transformation Certification Validation Service</i>. ");
      SB.Append("See <strong><a href='https://www.nsgi.nl'>www.nsgi.nl</a></strong> for details.</p>");
      SB.Append("<p>Use the <i>Transformation Certification Validation Service</i> to get your Transformation application certified and earn the <strong>RDNAPTRANS™</strong> logo.</p>");
      SB.Append("<p>When the text color of the <strong style='color:green'>Load</strong> button is <strong style='color:green'>green</strong></font>, the data-file is loaded. ");
      SB.Append("Otherwise the data-file is not loaded. When the mandatory data-files are not loaded, a datum transformation will <strong>not</strong> give an error. ");
      SB.Append("It returns a wrong results. You can always re-load a data-file.</p>");
      SB.Append("<p><u><strong>Notes:</strong></u><ul><li>The ASCII data-files can be found at <strong><a href='https://www.nsgi.nl'>www.nsgi.nl</a></strong>. ");
      SB.Append("The grid files are part of a <strong><a href='https://www.nsgi.nl/geodetische-infrastructuur/producten/coordinatentransformatie'>download package</a></strong> to request for free.");
      SB.Append("<li><strong>HTML back button: </strong> Double-click on one of the label or text fields at the top of this GUI to see this message again.</li></ul></p>");
      SB.Append("</body></html>");
      WBoutput.DocumentText = SB.ToString();
    } // Display_info()

    /// <summary>
    /// Displays any (error/warning) message in the Webbrowser control
    /// </summary>
    /// <param name="pMsg"></param>
    private void Display_message(string pMsg) {
      StringBuilder SB = new StringBuilder();
      SB.Append("<!DOCTYPE html><html><head><style>");
      SB.Append("body {font-family: consolas;font-size:15px}");
      SB.Append("</style></head><body>");
      SB.Append(pMsg);
      SB.Append("</body></html>");
      WBoutput.DocumentText = SB.ToString();
    } // Display_error()

    /// <summary>
    /// Clear any error message displayed in the Webbrowser control
    /// </summary>
    private void Clear_error() { 
      if (ObjRdNap.errCode != 0) {
        this.WBoutput.DocumentText = "";
      }
    } // Clear_error()

    /// <summary>
    /// Runs when the form is being closed
    /// </summary>
    private void FrmTransform_FormClosing(object sender, FormClosingEventArgs e) {
      Save_settings();
    } // FrmTransform_FormClosing()

    /// <summary>
    /// Displays the info message in the Webbrowser control
    /// </summary>
    private void FldGridRdCorr_MouseDoubleClick(object sender, MouseEventArgs e) {
      Display_info();
    } // FldGridRdCorr_MouseDoubleClick()

    private void Load_settings() {
      this.Width = Properties.Settings.Default.FrmWidth;    // 577
      this.Height = Properties.Settings.Default.FrmHeight;  // 666
      this.FldGridRdCorr.Text = Properties.Settings.Default.rdcorr2018;
      this.FldGridHeight.Text = Properties.Settings.Default.nlgeo2018;
      this.FldSelfVal.Text = Properties.Settings.Default.Z001_ETRS89andRDNAP;
      this.FldEtrs89Val.Text = Properties.Settings.Default.Z002_ETRS89;
      this.FldRdVal.Text = Properties.Settings.Default.Z002_RDNAP;
      if (Double.IsNaN(Properties.Settings.Default.RDx)) { this.FldRDx.Text = ""; }
      else { this.FldRDx.Text = Properties.Settings.Default.RDx.ToString(); }
      if (Double.IsNaN(Properties.Settings.Default.RDy)) {  this.FldRDy.Text = ""; }
      else { this.FldRDy.Text = Properties.Settings.Default.RDy.ToString(); }
      if (Double.IsNaN(Properties.Settings.Default.NAPh)) { this.FldNAPh.Text = ""; }
      else { this.FldNAPh.Text = Properties.Settings.Default.NAPh.ToString(); }
      if (Double.IsNaN(Properties.Settings.Default.ETRSlat)) { this.FldETRSlat.Text = ""; }
      else { this.FldETRSlat.Text = Properties.Settings.Default.ETRSlat.ToString(); }
      if (Double.IsNaN(Properties.Settings.Default.ETRSlon)) { this.FldETRSlon.Text = ""; }
      else { this.FldETRSlon.Text = Properties.Settings.Default.ETRSlon.ToString(); }
      if (Double.IsNaN(Properties.Settings.Default.ETRSh)) { this.FldETRSh.Text = ""; }
      else { this.FldETRSh.Text = Properties.Settings.Default.ETRSh.ToString(); }
    } // Load_setting()

    private void Save_settings() {
      Properties.Settings.Default.FrmWidth = this.Width;
      Properties.Settings.Default.FrmHeight = this.Height;
      Properties.Settings.Default.rdcorr2018 = this.FldGridRdCorr.Text;
      Properties.Settings.Default.nlgeo2018 = this.FldGridHeight.Text;
      Properties.Settings.Default.Z001_ETRS89andRDNAP = this.FldSelfVal.Text;
      Properties.Settings.Default.Z002_ETRS89 = this.FldEtrs89Val.Text;
      Properties.Settings.Default.Z002_RDNAP = this.FldRdVal.Text;
      if (this.FldRDx.Text.Length == 0 ) { Properties.Settings.Default.RDx = double.NaN; }
      else { Properties.Settings.Default.RDx = Convert.ToDouble(this.FldRDx.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator)); }
      if (this.FldRDy.Text.Length == 0) { Properties.Settings.Default.RDy = double.NaN; }
      else { Properties.Settings.Default.RDy = Convert.ToDouble(this.FldRDy.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator)); }
      if ( this.FldNAPh.Text.Length == 0) { Properties.Settings.Default.NAPh = double.NaN; }
      else { Properties.Settings.Default.NAPh = Convert.ToDouble(this.FldNAPh.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator)); }
      if (this.FldETRSlat.Text.Length == 0) { Properties.Settings.Default.ETRSlat = double.NaN; }
      else { Properties.Settings.Default.ETRSlat = Convert.ToDouble(this.FldETRSlat.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator)); }
      if (this.FldETRSlon.Text.Length == 0) { Properties.Settings.Default.ETRSlon = double.NaN; }
      else { Properties.Settings.Default.ETRSlon = Convert.ToDouble(this.FldETRSlon.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator)); }
      if (this.FldETRSh.Text.Length == 0) { Properties.Settings.Default.ETRSh = double.NaN; } 
      else { Properties.Settings.Default.ETRSh = Convert.ToDouble(this.FldETRSh.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator)); }
      Properties.Settings.Default.Save();
    } // Save_settings()

    /// <summary>
    /// Load grid file or validation file
    /// </summary>
    /// <param name="pBtn">The button clicked by the user</param>
    private bool Run_grid_button(Button pBtn) {
      bool retVal;

      // The row values excluded the header rows. The number of data rows are given here
      switch (pBtn.Name) { 
        case "BtnGridRdCorr": retVal = ObjRdNap.Load_grid_file(this.FldGridRdCorr.Text, 144781, 1); break;
        case "BtnGridHeight": retVal = ObjRdNap.Load_grid_file(this.FldGridHeight.Text, 144781, 2); break;
        case "BtnSelfVal": retVal = ObjRdNap.Load_grid_file(this.FldSelfVal.Text, 10000, 3); break;
        case "BtnEtrsValFile": retVal = ObjRdNap.Load_grid_file(this.FldEtrs89Val.Text, 10000, 4); break;
        case "BtnRdValFile": retVal = ObjRdNap.Load_grid_file(this.FldRdVal.Text, 10000, 5); break;
        default: retVal = false; break;
      }
      if (retVal == true) { 
        pBtn.ForeColor = System.Drawing.Color.Green;
        return true;
      }
      else { 
        pBtn.ForeColor = System.Drawing.Color.Red;
        Display_message(ObjRdNap.errMsg);
        return false;
      }
    } // Run_grid_button()
    
    private void BtnGridRdCorr_Click(object sender, EventArgs e) {
      this.Cursor = Cursors.WaitCursor;
      Clear_error();
      Run_grid_button((Button)sender);
      this.Cursor = Cursors.Default;
    } // BtnGridRdCorr_Click()

    /// <summary>
    /// Convert one ETRS89 coordinate pair to RD
    /// </summary>
    private void BtnToRd_Click(object sender, EventArgs e) {
      double lat, lon;
      StringBuilder SB;
      this.FldRDx.Text = "";
      this.FldRDy.Text = "";
      this.FldNAPh.Text = "";

      if (this.FldETRSlat.Text.Length != 0 && this.FldETRSlon.Text.Length != 0 ) { 
        lat = Convert.ToDouble(this.FldETRSlat.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator));
        lon = Convert.ToDouble(this.FldETRSlon.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator));
        ObjRdNap.Rdnaptrans_clear();
        if (this.FldETRSh.Text.Length == 0 ) {
          ObjRdNap.ETRS89_to_RD(lat, lon);
        }
        else { 
          double h = Convert.ToDouble(this.FldETRSh.Text.Replace('.', ','));
          ObjRdNap.ETRS89_to_RD(lat, lon, h);
        }

        this.FldRDx.Text = ObjRdNap.RD_x_lon.ToString();
        this.FldRDy.Text = ObjRdNap.RD_y_lat.ToString();
        this.FldNAPh.Text = ObjRdNap.NAP_height.ToString();
        SB = new StringBuilder();
        SB.Append("<!DOCTYPE html><html><head><style>");
        SB.Append("table, td {border: 2px solid blue; border-collapse: collapse; padding: 2px 15px 2px 5px; text-align: left; font-family: consolas; color: brown;}");
        SB.Append("body {font-family: consolas;font-size:14px}");
        SB.Append("</style></head><body>");
        SB.Append("<strong>ETRS89 to RD NAP Transformation:</strong></br><table>");
        SB.Append("<tr><td>Ellipsoidal geographic ETRS89 latitude</td><td>" + ObjRdNap.ETRS89_lat_dec + " (degrees)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic ETRS89 longitude</td><td>" + ObjRdNap.ETRS89_lon_dec + " (degrees)</td></tr>");
        SB.Append("<tr><td>ETRS89 height</td><td>" + ObjRdNap.ETRS89_height + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian ETRS89 X</td><td>" + ObjRdNap.geoc_cart_ETRS89_X + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian ETRS89 Y</td><td>" + ObjRdNap.geoc_cart_ETRS89_Y + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian ETRS89 Z</td><td>" + ObjRdNap.geoc_cart_ETRS89_Z + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian RD_Bessel X</td><td>" + ObjRdNap.geoc_cart_RD_Bessel_X + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian RD_Bessel Y</td><td>" + ObjRdNap.geoc_cart_RD_Bessel_Y + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian RD_Bessel Z</td><td>" + ObjRdNap.geoc_cart_RD_Bessel_Z + " (meter)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic pseudo Bessel latitude</td><td>" + ObjRdNap.geog_ellips_psdo_Bessel_lat + " (radian)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic pseudo Bessel longitute</td><td>" + ObjRdNap.geog_ellips_psdo_Bessel_lon + " (radian)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic real Bessel latitude</td><td>" + ObjRdNap.geog_ellips_real_Bessel_lat + " (degrees)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic real Bessel longitude</td><td>" + ObjRdNap.geog_ellips_real_Bessel_lon + " (degrees)</td></tr>");
        SB.Append("<tr><td>RD x coordinate (lon)</td><td>" + ObjRdNap.RD_x_lon + " (meter)</td></tr>");
        SB.Append("<tr><td>RD y coordinate (lat)</td><td>" + ObjRdNap.RD_y_lat + " (meter)</td></tr>");
        SB.Append("<tr><td>NAP_height</td><td>" + ObjRdNap.NAP_height + " (meter)</td></tr>");

        SB.Append("</table></body></html>");
        WBoutput.DocumentText = SB.ToString();
      }
      else {
        SB = new StringBuilder();
        SB.Append("<!DOCTYPE html><html><head><style>");
        SB.Append("body {font-family: consolas;font-size:14px}");
        SB.Append("</style></head><body>");
        SB.Append("<strong>ETRS89 to RD NAP Transformation:</strong></br><table>");
        SB.Append("</br></br>No input parameters defined");
        SB.Append("</body></html>");
        WBoutput.DocumentText = SB.ToString();
      }
    } // BtnToRd_Click()

    /// <summary>
    /// Convert one RD coordinate pair the ETRS89 pair
    /// </summary>
    private void BtnToETRS_Click(object sender, EventArgs e) {
      double x, y;
      StringBuilder SB;
      this.FldETRSlat.Text = "";
      this.FldETRSlon.Text = "";
      this.FldETRSh.Text = "";

      if (this.FldRDx.Text.Length != 0 && this.FldRDy.Text.Length != 0 ) { 
        x = Convert.ToDouble(this.FldRDx.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator));
        y = Convert.ToDouble(this.FldRDy.Text.Replace(NumberGroupSeparator, NumberDecimalSeparator));
        ObjRdNap.Rdnaptrans_clear();
        if (this.FldNAPh.Text.Length == 0 ) {
          ObjRdNap.RD_to_ETRS89(x, y);
        }
        else {
          double h = Convert.ToDouble(this.FldNAPh.Text.Replace('.', ','));
          ObjRdNap.RD_to_ETRS89(x, y, h);
        }

        this.FldETRSlat.Text = ObjRdNap.ETRS89_lat_dec.ToString();
        this.FldETRSlon.Text = ObjRdNap.ETRS89_lon_dec.ToString();
        this.FldETRSh.Text = ObjRdNap.ETRS89_height.ToString();
        SB = new StringBuilder();
        SB.Append("<!DOCTYPE html><html><head><style>");
        SB.Append("table, td {border: 2px solid blue; border-collapse: collapse; padding: 2px 15px 2px 5px; text-align: left; font-family: consolas; color: brown;}");
        SB.Append("body {font-family: consolas;font-size:14px}");
        SB.Append("</style></head><body>");
        SB.Append("<strong>RD NAP to ETRS89 transformation:</strong></br><table>");
        SB.Append("<tr><td>RD x coordinate (lon)</td><td>" + ObjRdNap.RD_x_lon + " (meter)</td></tr>");
        SB.Append("<tr><td>RD y coordinate (lat)</td><td>" + ObjRdNap.RD_y_lat + " (meter)</td></tr>");
        SB.Append("<tr><td>NAP_height</td><td>" + ObjRdNap.NAP_height + " (meter)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic sphere latitude</td><td>" + ObjRdNap.geog_sphere_real_Bessel_lat + " (radian)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic sphere longitute</td><td>" + ObjRdNap.geog_sphere_real_Bessel_lon + " (radian)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic real Bessel latitude</td><td>" + ObjRdNap.geog_ellips_real_Bessel_lat + " (radian)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic real Bessel longitude</td><td>" + ObjRdNap.geog_ellips_real_Bessel_lon + " (radian)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic pseudo Bessel latitude</td><td>" + ObjRdNap.geog_ellips_psdo_Bessel_lat + " (degrees)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic pseudo Bessel longitute</td><td>" + ObjRdNap.geog_ellips_psdo_Bessel_lon + " (degrees)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian ETRS89 X</td><td>" + ObjRdNap.geoc_cart_ETRS89_X + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian ETRS89 Y</td><td>" + ObjRdNap.geoc_cart_ETRS89_Y + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian ETRS89 Z</td><td>" + ObjRdNap.geoc_cart_ETRS89_Z + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian RD_Bessel X</td><td>" + ObjRdNap.geoc_cart_RD_Bessel_X + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian RD_Bessel Y</td><td>" + ObjRdNap.geoc_cart_RD_Bessel_Y + " (meter)</td></tr>");
        SB.Append("<tr><td>Geocentric Cartesian RD_Bessel Z</td><td>" + ObjRdNap.geoc_cart_RD_Bessel_Z + " (meter)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic ETRS89 latitude</td><td>" + ObjRdNap.ETRS89_lat_dec + " (degrees)</td></tr>");
        SB.Append("<tr><td>Ellipsoidal geographic ETRS89 longitude</td><td>" + ObjRdNap.ETRS89_lon_dec + " (degrees)</td></tr>");
        SB.Append("<tr><td>ETRS89 height</td><td>" + ObjRdNap.ETRS89_height + " (meter)</td></tr>");
        SB.Append("</table></body></html>");
        WBoutput.DocumentText = SB.ToString();
      }
      else {
        SB = new StringBuilder();
        SB.Append("<!DOCTYPE html><html><head><style>");
        SB.Append("body {font-family: consolas;font-size:14px}");
        SB.Append("</style></head><body>");
        SB.Append("<strong>RD NAP to ETRS89 transformation:</strong></br><table>");
        SB.Append("</br></br>No input parameters defined");
        SB.Append("</body></html>");
        WBoutput.DocumentText = SB.ToString();
      }
    } // BtnToETRS_Click()

    /// <summary>
    /// ETRS89 to RD NAP self-validation
    /// </summary>
    private void BtnEtrsSelfVal_Click(object sender, EventArgs e)  {
      double lat, lon, h;

      this.Cursor = Cursors.WaitCursor;
      double[,] validate = new double[10000, 17];
      Clear_error();
      if (this.BtnSelfVal.ForeColor != System.Drawing.Color.Green) { 
        Run_grid_button(this.BtnSelfVal);
      }
      for (int i=0; i<10000; i++) { 
        for (int j=0; j<7; j++) {
          validate[i,j] = ObjRdNap.z001_etrs89andrdnap[i, j];
        }

        lat = ObjRdNap.z001_etrs89andrdnap[i,1];
        lon = ObjRdNap.z001_etrs89andrdnap[i,2];
        h = ObjRdNap.z001_etrs89andrdnap[i,3];

        ObjRdNap.ETRS89_to_RD(lat, lon, h);
        validate[i,7] = ObjRdNap.RD_x_lon;    // xc
        validate[i,8] = ObjRdNap.RD_y_lat;    // yc
        validate[i,9] = ObjRdNap.NAP_height;  // zc

        validate[i,10] = Math.Abs(validate[i, 4] - validate[i,7]);  // dx
        validate[i,11] = Math.Abs(validate[i, 5] - validate[i,8]);  // dy
        validate[i,12] = Math.Abs(validate[i, 6] - validate[i,9]);  // dz

        if (validate[i,10] < 0.001) { validate[i,13] = 1; }         // conv_x_ok
        else { validate[i,13] = 0; }
        if (validate[i,11] < 0.001) { validate[i,14] = 1; }         // conv_y_ok
        else { validate[i,14] = 0; }
        if (validate[i, 10] < 0.001 && validate[i, 11] < 0.001) { validate[i, 15] = 1; }        // conv_x_and_y_ok
        else { validate[i, 15] = 0; }
        if (validate[i,12] < 0.001 || double.IsNaN(validate[i, 12])) { validate[i,16] = 1; }    // conv_z_ok
        else { validate[i,16] = 0; }
      }

      FileInfo FI = new FileInfo(this.FldSelfVal.Text);
      string fn = "Z001_C#_ETRS89_to_RDNAP.csv";
      string dir = FI.DirectoryName;
      StringBuilder SB = new StringBuilder();
      using (StreamWriter SW  = new StreamWriter(dir + @"\"+ fn)) { 
        for (int i=0; i<10000; i++) {
          SB.Clear();
          for (int j=0; j<17; j++) {
            SB.Append(validate[i,j] + ";");
          }
          SW.WriteLine(SB.ToString());
        }
      }
      
      // Calculate the results.
      SB = new StringBuilder();
      SB.Append("<!DOCTYPE html><html><head><style>");
      SB.Append("table, td {border: 2px solid blue; border-collapse: collapse; padding: 2px 15px 2px 5px; text-align: left; font-family: consolas; color: brown;}");
      SB.Append("body {font-family: consolas;font-size:12px}");
      SB.Append("</style></head><body>");
      SB.Append("<p><strong>ETRS89 to RD NAP self-validation result:</strong></p>");
      SB.Append("<table><tr><td>Xmax</td><td>Ymax</td><td>Hmax</td><td>Xconv</td><td>Yconv</td><td>XYconv</td><td>Hconv</td></tr>");
      SB.Append("<tr>");

      for (int j=0; j<7; j++) { 
        double[] a = new double [10000];
        for (int i=0; i<10000; i++) {
          a[i] = validate[i,10+j];
        }
        if (j < 3 ) { 
          SB.Append("<td>" + a.Max().ToString() + "</td>");
        }
        else {
          SB.Append("<td>" + a.Sum().ToString() + "</td>");
        }
      }
      SB.Append("</tr></table>");
      SB.Append("<p>The maximum values must be smaller than 0.001 and the conversion values must be 10,000. ");
      SB.Append("This means that all 10k ETRS89 coordinates are transfered to the correct RD NAP value.</p>");
      SB.Append("<p>The self-validation result is stored in <strong>" + dir + "\\" + fn + "</strong>.</p>");
      SB.Append("</body></html>");
      this.WBoutput.DocumentText = SB.ToString();
      this.Cursor = Cursors.Default;
    } // BtnEtrsSelfVal_Click();

    /// <summary>
    /// RD NAP to ETRS89 self-validation
    /// </summary>
    private void BtnRdSelfVal_Click(object sender, EventArgs e) {
      double x, y, h;

      this.Cursor = Cursors.WaitCursor;
      double[,] validate = new double[10000, 17];
      Clear_error();
      if (this.BtnSelfVal.ForeColor != System.Drawing.Color.Green) { 
        Run_grid_button(this.BtnSelfVal);
      }

      for (int i=0; i<10000; i++) { 
        for (int j=0; j<7; j++) {
          validate[i,j] = ObjRdNap.z001_etrs89andrdnap[i, j];
        }

        x = ObjRdNap.z001_etrs89andrdnap[i,4];
        y = ObjRdNap.z001_etrs89andrdnap[i,5];
        h = ObjRdNap.z001_etrs89andrdnap[i,6];

        ObjRdNap.RD_to_ETRS89(x, y, h);
        validate[i,7] = ObjRdNap.ETRS89_lat_dec;        // LATc
        validate[i,8] = ObjRdNap.ETRS89_lon_dec;        // LONc
        validate[i,9] = ObjRdNap.ETRS89_height;         // hc

        validate[i,10] = Math.Abs(validate[i, 1] - validate[i,7]);  // dlat
        validate[i,11] = Math.Abs(validate[i, 2] - validate[i,8]);  // dlon
        validate[i,12] = Math.Abs(validate[i, 3] - validate[i,9]);  // dh

        if (validate[i,10] < 0.0000001) { validate[i,13] = 1; }         // conv_lat_ok
        else { validate[i,13] = 0; }
        if (validate[i,11] < 0.0000001) { validate[i,14] = 1; }         // conv_lon_ok
        else { validate[i,14] = 0; }
        if (validate[i, 10] < 0.0000001 && (validate[i, 11] < 0.0000001)) { validate[i, 15] = 1; }     // conv_lat_lon_ok
        else { validate[i, 15] = 0; }
        if (validate[i,12] < 0.001 || Double.IsNaN(validate[i, 12])) { validate[i,16] = 1; }           // conv_h_ok
        else { validate[i,16] = 0; }
      }

      FileInfo FI = new FileInfo(this.FldSelfVal.Text);
      string fn = "Z001_C#_RDNAP_to_ETRS89.csv";
      string dir = FI.DirectoryName;
      StringBuilder SB = new StringBuilder();
      using (StreamWriter SW  = new StreamWriter(dir + @"\"+ fn)) { 
        for (int i=0; i<10000; i++) {
          SB.Clear();
          for (int j=0; j<17; j++) {
            SB.Append(validate[i,j] + ";");
          }
          SW.WriteLine(SB.ToString());
        }
      }
      
      // Calculate the results.
      SB = new StringBuilder();
      SB.Append("<!DOCTYPE html><html><head><style>");
      SB.Append("table, td {border: 2px solid blue; border-collapse: collapse; padding: 2px 15px 2px 5px; text-align: left; font-family: consolas; color: brown;}");
      SB.Append("body {font-family: consolas;font-size:12px}");
      SB.Append("</style></head><body>");
      SB.Append("<p><strong>RD to ETRS89 self-validation result:</strong></p>");
      SB.Append("<table><tr><td>Latmax</td><td>Lonmax</td><td>Hmax</td><td>Latconv</td><td>Lonconv</td><td>LatLonconv</td><td>Hconv</td></tr>");
      SB.Append("<tr>");

      for (int j=0; j<7; j++) { 
        double[] a = new double [10000];
        for (int i=0; i<10000; i++) {
          a[i] = validate[i,10+j];
        }
        if (j < 3 ) { 
          SB.Append("<td>" + a.Max().ToString() + "</td>");
        }
        else {
          SB.Append("<td>" + a.Sum().ToString() + "</td>");
        }
      }
      SB.Append("</tr></table>");
      SB.Append("<p>The maximum values for the lat/long must be smaller than 0.00000001, the maximum value for the height ");
      SB.Append("must be smaller than 0.001, and the conversion values must be 10,000. ");
      SB.Append("This means that all 10k RD NAP coordinates are transfered to the correct ETRS89 value.</p>");
      SB.Append("<p><strong><u>Note:</strong></u> The self-validation file cannot transform all RD height values to the ETRS89 values. ");
      SB.Append("There are rows were it is not possible to store a two-way transformation. So, for some ETRS89 values you will get a NaN value. ");
      SB.Append("This is not shown here. A NaN value is regarded as correct.</p>");
      SB.Append("<p>The self-validation result is stored in <strong>" + dir + "\\" + fn + "</strong>.</p>");
      SB.Append("</body></html>");
      this.WBoutput.DocumentText = SB.ToString();
      this.Cursor = Cursors.Default;
    } // BtnRdSelfVal_Click()

    /// <summary>
    /// Validate the ETRS89 coordinates. For the Trademark approval. ETRS89 to RD NAP transformation 
    /// </summary>
    private void BtnEtrsVal_Click(object sender, EventArgs e) {
      double lat, lon, h;

      this.Cursor = Cursors.WaitCursor;
      Clear_error();
      if (this.BtnEtrsValFile.ForeColor != System.Drawing.Color.Green) { 
        if (!Run_grid_button(this.BtnEtrsValFile)) { this.Cursor = Cursors.Default; return; }
      }

      double[,] validate = new double[10000, 4];
      for (int i=0; i<10000; i++) { 
        for (int j=0; j<7; j++) {
          validate[i,0] = ObjRdNap.z002_etrs89[i, 0];
        }

        lat = ObjRdNap.z002_etrs89[i,1];
        lon = ObjRdNap.z002_etrs89[i, 2];
        h = ObjRdNap.z002_etrs89[i, 3];

        ObjRdNap.ETRS89_to_RD(lat, lon, h);
        validate[i,1] = ObjRdNap.RD_x_lon;    // xc
        validate[i,2] = ObjRdNap.RD_y_lat;    // yc
        validate[i,3] = ObjRdNap.NAP_height;  // zc
      }

      FileInfo FI = new FileInfo(this.FldEtrs89Val.Text);
      string fn = "C#_CERTIFY_ETRS89.txt";
      string dir = FI.DirectoryName;

      StringBuilder SB = new StringBuilder();
      using (StreamWriter SW  = new StreamWriter(dir + @"\" + fn)) { 
        for (int i=0; i<10000; i++) {
          SB.Clear();
          for (int j=0; j<4; j++) {
            SB.Append(validate[i,j] + " ");
          }
          //SB.Replace("-999999","NaN");
          SB.Replace(",",".");
          SW.WriteLine(SB.ToString());
        }
      }
      Display_message("Return file found at: " + dir + @"\" + fn + "</p>Feed the file into <a href='https://www.nsgi.nl/geodetische-infrastructuur/producten/programma-rdnaptrans/validatieservice#etrsresult</p>'>here</a>");
      this.Cursor = Cursors.Default;
    } // BtnEtrsVal_Click()

    /// <summary>
    /// Validate the RD NAP coordinates. For the Trademark approval. RD NAP to ETRS89 transformation 
    /// </summary>
    private void BtnRdVal_Click(object sender, EventArgs e) {
      double x, y, h;

      this.Cursor = Cursors.WaitCursor;
      Clear_error();
      if (this.BtnRdValFile.ForeColor != System.Drawing.Color.Green) { 
        if (!Run_grid_button(this.BtnRdValFile)) { this.Cursor = Cursors.Default; return; }
      }

      double[,] validate = new double[10000, 4];
      for (int i=0; i<10000; i++) { 
        for (int j=0; j<7; j++) {
          validate[i,0] = ObjRdNap.z002_rdnap[i, 0];
        }

        x = ObjRdNap.z002_rdnap[i,1];
        y = ObjRdNap.z002_rdnap[i,2];
        h = ObjRdNap.z002_rdnap[i,3];

        ObjRdNap.RD_to_ETRS89(x, y, h);
        validate[i,1] = ObjRdNap.ETRS89_lat_dec;        // latc
        validate[i,2] = ObjRdNap.ETRS89_lon_dec;        // lonc
        validate[i,3] = ObjRdNap.ETRS89_height;         // hc
      }

      FileInfo FI = new FileInfo(this.FldRdVal.Text);
      string fn = "C#_CERTIFY_RDNAP.txt";
      string dir = FI.DirectoryName;

      StringBuilder SB = new StringBuilder();
      using (StreamWriter SW  = new StreamWriter(dir + @"\" + fn)) { 
        for (int i=0; i<10000; i++) {
          SB.Clear();
          for (int j=0; j<4; j++) {
            SB.Append(validate[i,j] + " ");
          }
          //SB.Replace("-999999","NaN");
          SB.Replace(",",".");
          SW.WriteLine(SB.ToString());
        }
      }
      Display_message("Return File found at: " + dir + @"\" + fn + "</p>Feed the file into <a href='https://www.nsgi.nl/geodetische-infrastructuur/producten/programma-rdnaptrans/validatieservice#etrsresult</p>'>here</a>");
      this.Cursor = Cursors.Default;
    } // BtnRdVal_Click()

  }
}
