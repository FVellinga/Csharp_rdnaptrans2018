using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RDNAPTRANS2018
{

  /*
  RDNAPTRANS2018

  Coordinate transformation to and from Stelsel van de Rijksdriehoeksmeting and Normaal Amsterdams Peil
  - Transformation from ETRS89 to RD.
  - Transformation form RD to ETRS89 (called the inverse transformation).

  This implementation follows the flow as described in RDNAPTRANS2018 document found at www.nsgi.nl
  All formulas and variables are explained there.

  Example:
    ClsRDnaptrans ObjRdNap = new ClsRDnaptrans();
    ObjRdNap.rdcorr2018_file = "C:\RDNAPTRANS\grid\rdcorr2018.txt";
    ObjRdNap.nlgeo2018_file = "C:\RDNAPTRANS\grid\nlgeo2018.txt";
    ObjRdNap.Load_grid_file(ObjRdNap.rdcorr2018_file, 1);
    ObjRdNap.Load_grid_file(ObjRdNap.nlgeo2018_file, 2);
    ObjRdNap.ETRS89_to_RD("52 9 22,178", "5 23 15,500", 72.6882);

  Version:
  1.0 - 20191002: Fred Vellinga, Initial version. 
  1.1 - 20221222: Fred Vellinga, DLL version.
  */

  public class ClsRDnaptrans
  {

    #region constructor

    /// <summary>
    /// Initialize an instance of the ClsRDnaptrans class and set all parameters
    /// </summary>
    public ClsRDnaptrans() {
      Rdnaptrans_ini();
    } // ClsRDnaptrans()

    #endregion

    #region public variables

    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 latitude in degrees minutes and seconds
    /// </summary>
    public string ETRS89_lat_dgr;
    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 longitude in degrees minutes and seconds. The ellipsoidal geographic ETRS89 coordinates 
    /// </summary>
    public string ETRS89_lon_dgr;
    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 latitude in decimal degrees
    /// </summary>
    public double ETRS89_lat_dec;
    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 longitude in decimal degrees
    /// </summary>
    public double ETRS89_lon_dec;
    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 latitude in radian
    /// </summary>
    public double ETRS89_lat_rad;
    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 longitude in radian
    /// </summary>
    public double ETRS89_lon_rad;
    /// <summary>
    /// The ellipsoidal geographic ETRS89 coordinates: ETRS89 height in meter
    /// </summary>
    public double ETRS89_height;
    /// <summary>
    /// The RD x coordinate in meter
    /// </summary>
    public double RD_x_lon = 0;
    /// <summary>
    /// The RD y coordinate in meter
    /// </summary>
    public double RD_y_lat = 0;
    /// <summary>
    /// The RD NAP height value in meter
    /// </summary>
    public double NAP_height = 0;

    public double geoc_cart_ETRS89_X = 0;           // the geocentric cartesian ETRS89 coordinates(meter)
    public double geoc_cart_ETRS89_Y = 0;
    public double geoc_cart_ETRS89_Z = 0;

    public double geoc_cart_RD_Bessel_X = 0;        // the geocentric cartesian RD datum coordinates(meter)
    public double geoc_cart_RD_Bessel_Y = 0;
    public double geoc_cart_RD_Bessel_Z = 0;

    public double geog_ellips_psdo_Bessel_lat = 0;  // the lat/lon pair in ellipsoidal geographic pseudo Bessel coordinates(radian)
    public double geog_ellips_psdo_Bessel_lon = 0;

    public double geog_ellips_real_Bessel_lat = 0;  // the lat/lon pair in ellipsoidal geographic real Bessel coordinates(degrees)
    public double geog_ellips_real_Bessel_lon = 0;

    public double geog_sphere_real_Bessel_lat = 0;  // the lat/lon pair in sphere geographic real Bessel coordinates(radian)
    public double geog_sphere_real_Bessel_lon = 0;

    public string rdcorr2018_file = "";             // path to the grid files and validation files
    public string nlgeo2018_file = "";
    public string z001_etrs89andrdnap_file = "";
    public string z002_etrs89_file = "";
    public string z002_rdnap_file = "";

    public double[,] rdcorr2018 = new double[144781,4];  // arrays containing the grid files and validation files data
    public double[,] nlgeo2018 = new double[144781,3];
    public double[,] z001_etrs89andrdnap;
    public double[,] z002_etrs89;
    public double[,] z002_rdnap;

    /// <summary>
    /// Error code set by this class. 0 means no error. Other codes have no meaning
    /// </summary>
    public int errCode = 0;
    /// <summary>
    /// Error message set by this class. Some functionaly has error trapping. Not all
    /// </summary>
    public string errMsg;

    /// <summary>
    /// Indicator telling the transformation took place for an out of bound grid coordinate. Not used.
    /// </summary>
    public bool out_of_bound = false;

    #endregion

    #region private variables

    /// <summary>
    /// GRS80 ellipsoid: Half major(equator) axis of GRS80 ellipsoid in meter
    /// </summary>
    private double GRS80_a;
    /// <summary>
    /// GRS80 ellipsoid: Flattening of GRS80 ellipsoid(dimensionless)
    /// </summary>
    private double GRS80_f;
    /// <summary>
    /// Ellipsoidal ETRS89 height approximately corresponding to 0 m in NAP(m)
    /// </summary>
    private double ETRS89_h0;
    /// <summary>
    /// Inverse transformation, RD to ETRS89: 𝜀 = precision threshold for termination of iteration, corresponding to 0.0001 m
    /// </summary>
    private double epsilon_GRS80_threshold;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Translation in direction of X axis meter
    /// </summary>
    private double tX;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Translation in direction of Y axis meter
    /// </summary>
    private double tY;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Translation in direction of Z axis meter
    /// </summary>
    private double tZ;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Rotation angle around X axis(−1.91513*10−6 rad)
    /// </summary>
    private double alpha;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Rotation angle around Y axis(+1.60365*10−6 rad)
    /// </summary>
    private double beta;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Rotation angle around Z axis(−9.09546*10−6 rad)
    /// </summary>
    private double gamma;
    /// <summary>
    /// 3D similarity transformation from ETRS89 to RD: Scale difference(dimensionless, −4.07242*10−6)
    /// </summary>
    private double delta;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Translation in direction of X axis meter
    /// </summary>
    private double tX_inv;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Translation in direction of Y axis meter
    /// </summary>
    private double tY_inv;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Translation in direction of Z axis meter
    /// </summary>
    private double tZ_inv;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Rotation angle around X axis(−1.91513 ∙ 10−6 rad)
    /// </summary>
    private double alpha_inv;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Rotation angle around Y axis(+1.60365 ∙ 10−6 rad)
    /// </summary>
    private double beta_inv;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Rotation angle around Z axis(−9.09546 ∙ 10−6 rad)
    /// </summary>
    private double gamma_inv;
    /// <summary>
    /// 3D similarity transformation from RD to ETRS89, the inverse transformation: Scale difference(dimensionless, −4.07242 ∙ 10−6)
    /// </summary>
    private double delta_inv;
    // Two parameters of Bessel 1841 ellipsoid in the conversion to ellipsoidal geographic pseudo Bessel coordinates
    /// <summary>
    /// Bessel 1841 ellipsoid: Half major(equator) axis of Bessel 1841 ellipsoid in meter
    /// </summary>
    private double B1841_a;
    /// <summary>
    /// Bessel 1841 ellipsoid: Flattening of Bessel 1841 ellipsoid(dimensionless)
    /// </summary>
    private double B1841_f;
    /// <summary>
    /// Bessel 1841 ellipsoid:  𝜀 = precision threshold for termination of iteration, corresponding to 0.0001 m
    /// </summary>
    private double epsilon_B1841_threshold;
    /// <summary>
    // RD to ETRS9: Fixed ellipsoidal height used in the conversion to geocentric Cartesian Bessel. Ellipsoidal RD Bessel height approximately corresponding to 0 m in NAP(m)
    /// </summary>
    private double RD_Bessel_h0;
    /// <summary>
    /// RD correction grid. Value in degrees: 𝜑𝑚𝑖𝑛 = 50° latitude of southern bound of correction grid
    /// </summary>
    private double phi_min;
    /// <summary>
    /// RD correction grid. Value in degrees: 𝜑𝑚𝑎𝑥 = 56° latitude of northern bound of correction grid
    /// </summary>
    private double phi_max;
    /// <summary>
    /// RD correction grid. Value in degrees: 𝜆𝑚𝑖𝑛 = 2° longitude of western bound of correction grid
    /// </summary>
    private double labda_min;
    /// <summary>
    /// RD correction grid. Value in degrees: 𝜆𝑚𝑎𝑥 = 8° longitude of eastern bound of correction grid
    /// </summary>
    private double labda_max;
    /// <summary>
    /// RD correction grid. Value in degrees: Δ𝜑= 0.0125° = 45″ latitude spacing of correction grid, corresponding to about 1.4 km
    /// </summary>
    private double phi_delta;
    /// <summary>
    /// RD correction grid. Value in degrees: Δ𝜆 = 0.02° = 72″ longitude spacing of correction grid, corresponding to about 1.4 km
    /// </summary>
    private double labda_delta;
    /// <summary>
    /// RD correction grid. Value in degrees: 𝑐0 = 0.00000000 ° correction value outside bounds of correction grid
    /// </summary>
    private double c0;
    /// <summary>
    /// RD correction grid. Value in degrees: 𝜀 = 0.000 000 001° precision threshold for termination of iteration, corresponding to 0.0001 m
    /// </summary>
    private double epsilon_RD_threshold;
    /// <summary>
    /// RD map projection. Value in degrees: Latitude of central point Amersfoort on Bessel ellipsoid
    /// </summary>
    private double phi0_amersfoort;
    /// RD map projection. Value in degrees: Longitude of central point Amersfoort on Bessel ellipsoid
    /// </summary>
    private double labda0_amersfoort;
    /// <summary>
    /// RD map projection: Scale factor(dimensionless)
    /// </summary>
    private double k_amersfoort;
    /// <summary>
    /// RD map projection. Value in meters: False Easting 
    /// </summary>
    private double x0_amersfoort;
    /// <summary>
    /// RD map projection. Value in meters: False Northing
    /// </summary>
    private double y0_amersfoort; 

    #endregion

    #region constants

    private readonly double const_90_degrees = Math.PI/2;     //  90 degrees in radian
    private readonly double const_180_degrees = Math.PI;      // 180 degrees in radian
    private readonly double const_360_degrees = 2 * Math.PI;  // 360 degrees in radian
    
    #endregion

    #region local functions

    /// <summary>
    /// Convert a degree value in minutes second notation to radian.
    /// </summary>
    /// <param name="pD">Degree in D M S notation. A string. Example: 52 9 22.178</param>
    /// <returns> Degree in radian</returns>
    private double Conv_degr_dms_to_rad(string pD) {
        string[] DMS = pD.Split(' ');
        double degree = Convert.ToDouble(DMS[0]) + Convert.ToDouble(DMS[1])/60 + Convert.ToDouble(DMS[2])/3600;
        return (degree*Math.PI)/180;
    } // Conv_degr_dms_to_rad

    /// <summary>
    /// Convert a degree value in minutes second notation to decimal degrees.
    /// </summary>
    /// <param name="pD">Degree in D M S notation. A string. Example: 52 9 22.178</param>
    /// <returns> Degree in decimal degrees</returns>
    private double Conv_degr_dms_to_degr_dec(string pD) {
      string[] DMS = pD.Split(' ');
      return (Convert.ToDouble(DMS[0]) + Convert.ToDouble(DMS[1])/60 + Convert.ToDouble(DMS[2])/3600);
    } // Conv_degr_dms_to_degr_dec

    /// <summary>
    /// Convert the the latitude and longitude coordinate pair from decimal degree notation to radian notation.
    /// </summary>
    /// <param name="pLat">Latitude in decimal notation. A double. Example: 53.155951112</param>
    /// <param name="pLon">Longitude in decimal notation. A double. Example: 3.439678554</param>
    private void Conv_dec_to_rad(double pLat, double pLon) { 
      /*
      The following public variables are set:
      - ETRS89_lat_rad: The latitude in radian.
      - ETRS89_lon_rad: The longitude in radian.
      */
      ETRS89_lat_rad = pLat * Math.PI/180;
      ETRS89_lon_rad = pLon * Math.PI/180;
    } // Conv_dec_to_rad()

    /// <summary>
    /// Check if file exists.
    /// </summary>
    /// <param name="pFullFile">Full File name</param>
    private bool File_exist(string pFullFile) { 
      if (pFullFile.Length > 0 ) {
        FileInfo FI = new FileInfo(pFullFile);
        return FI.Exists;
      }
      else { 
        return false;
     }
    } // File_exist()

    /// <summary>
    /// Clear the error message
    /// </summary>
    private void Clear_error() {
      errCode = 0;
      errMsg = "";
    } // Clear_error()

    #endregion

    #region initialisation functions

    /// <summary>
    /// Re-Initialize all the parameters.
    /// </summary>
    public void Rdnaptrans_ini() {
      GRS80_a = 6378137;
      GRS80_f = (1 / 298.257222101);
      ETRS89_h0 = 43;

      epsilon_GRS80_threshold = 0.00000000002;
      tX = -565.7346;
      tY = -50.4058;
      tZ = -465.2895;
      alpha = Conv_degr_dms_to_rad("0 0 -0,395023");
      beta = Conv_degr_dms_to_rad("0 0 0,330776");
      gamma = Conv_degr_dms_to_rad("0 0 -1,876073");
      delta = -0.00000407242;

      tX_inv = 565.7381;
      tY_inv = 50.4018;
      tZ_inv = 465.2904;
      alpha_inv = Conv_degr_dms_to_rad("0 0 0,395026");
      beta_inv = Conv_degr_dms_to_rad("0 0 -0,330772");
      gamma_inv = Conv_degr_dms_to_rad("0 0 1,876074");
      delta_inv = 0.00000407244;

      B1841_a = 6377397.155;
      B1841_f = (1 / 299.1528128);
      epsilon_B1841_threshold = 0.00000000002;

      RD_Bessel_h0 = 0;
      phi_min = 50;
      phi_max = 56;
      labda_min = 2;
      labda_max = 8;
      phi_delta = 0.0125;
      labda_delta = 0.02;
      c0 = 0;
      epsilon_RD_threshold = 0.000000001;

      k_amersfoort = 0.9999079;
      x0_amersfoort = 155000;
      y0_amersfoort = 463000;
      phi0_amersfoort = Conv_degr_dms_to_rad("52 9 22,178");
      labda0_amersfoort = Conv_degr_dms_to_rad("5 23 15,500");
  } // Rdnaptrans_ini()

    /// <summary>
    /// Clear or reset the output variables
    /// </summary>
    public void Rdnaptrans_clear() {
        ETRS89_lat_dgr = "";
        ETRS89_lon_dgr = "";
        ETRS89_lat_dec = 0;
        ETRS89_lon_dec = 0;
        ETRS89_lat_rad = 0;
        ETRS89_lon_rad = 0;
        ETRS89_height = 0;
        geoc_cart_ETRS89_X = 0;
        geoc_cart_ETRS89_Y = 0;
        geoc_cart_ETRS89_Z = 0;
        geoc_cart_RD_Bessel_X = 0;
        geoc_cart_RD_Bessel_Y = 0;
        geoc_cart_RD_Bessel_Z = 0;
        geog_ellips_psdo_Bessel_lat = 0;
        geog_ellips_psdo_Bessel_lon = 0;
        geog_ellips_real_Bessel_lat = 0;
        geog_ellips_real_Bessel_lon = 0;
        geog_sphere_real_Bessel_lat = 0;
        geog_sphere_real_Bessel_lon = 0;
        RD_x_lon = 0;
        RD_y_lat = 0;
        NAP_height = 0; 
  } // Rdnaptrans_clear()

    #endregion

    #region Transformation functions

    /// <summary>
    /// Load a grid file or validation file and store in 2D-array
    /// </summary>
    /// <param name="pFullFileName">The full file name where the grid file is stored</param>
    /// <param name="pRows">Number of rows to read. Most likely 144781</param>
    /// <param name="pGrid">The grid type; 1=rdcorr2018, 2=nlgeo2018, 3=z001_etrs89andrdnap, 4=ETRS validation, 5=RD validation</param>
    public bool Load_grid_file(string pFullFileName, int pRows, int pGrid) {
      FileStream FSin = null;
      StreamReader SRin = null;
      Encoding Enc;
      StringBuilder SB;
      Int32 HeaderLines=1;                   // how many headers does the file have. Those must be skipped. Default 1.
      Int32 ReadIntChar;                     // the single character that is read as an integer number
      char ReadChar;                         // the character equivalent
      Int32 RowPointer;                      // the row or line pointer keeps track of which row number is processed. Forward only
      Int32 DelimCounter;                    // the counted field separators per row; DelimCounter=FldDelimRow
      int DelimInt = Convert.ToInt32('\t');  // the delimiter; default a tab.


    Clear_error();
      if (File_exist(pFullFileName) == false) {
        errCode = 1;
        errMsg = "File " + pFullFileName + " not found.";
        return false;
      }

      // You can add byte order marks to the front (pre-amble) of the file to detected encoding automatically
      try {
        switch (pGrid) {
          case 1: rdcorr2018_file = pFullFileName; break;
          case 2: nlgeo2018_file = pFullFileName; break;
          case 3: z001_etrs89andrdnap_file = pFullFileName; HeaderLines = 0; z001_etrs89andrdnap = new double[10000, 7]; break;
          case 4: z002_etrs89_file = pFullFileName; DelimInt = Convert.ToInt32(' '); z002_etrs89 = new double[10000, 4]; break;
          case 5: z002_rdnap_file = pFullFileName; DelimInt = Convert.ToInt32(' '); z002_rdnap = new double[10000, 4]; break;
        }
        FSin = new FileStream(pFullFileName, FileMode.Open, FileAccess.Read);
        SRin = new StreamReader(FSin, Encoding.GetEncoding("iso-8859-1"));        // you cannot leave out the encoding option
        // Find out what the 'exact' encoding of the file is. When it is not iso-8859-1 then 
        // close the file stream and re-open it using correct character coding schema
        SRin.Peek();
        Enc = SRin.CurrentEncoding;
        if (Enc.BodyName != "iso-8859-1") {
          SRin.Close();
          SRin.Dispose();
          FSin.Close();
          FSin.Dispose();
          FSin = new FileStream(pFullFileName, FileMode.Open, FileAccess.Read);
          SRin = new StreamReader(FSin, Encoding.GetEncoding(Enc.BodyName));
        }
        // Initialize some variables
        RowPointer = 0 - HeaderLines;
        DelimCounter = 0;
        SB = new StringBuilder();
        // Read in all rows
        while (SRin.EndOfStream != true && RowPointer != pRows) {
          ReadIntChar = SRin.Read();
          if (ReadIntChar == DelimInt)  {
            if (RowPointer >= 0) {
              while (SRin.Peek() == DelimInt) { SRin.Read(); }
              switch (pGrid) {
                case 1: rdcorr2018[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                case 2: nlgeo2018[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                case 3: if (DelimCounter == 6 && SB.ToString() == "*") {
                         z001_etrs89andrdnap[RowPointer, DelimCounter] = Double.NaN;
                        }
                        else {
                          z001_etrs89andrdnap[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString());
                        }
                        break;
                case 4: z002_etrs89[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                case 5: z002_rdnap[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                default: break;
              }
            }
            SB = SB.Clear();
            DelimCounter++;
          }
          else { 
            if (ReadIntChar == 13) { //  13=cr, 10=lf
              ReadIntChar = SRin.Read();
              if (RowPointer >= 0) {
                switch(pGrid) {
                  case 1: rdcorr2018[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                  case 2: nlgeo2018[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                  case 3: if (DelimCounter == 6 && SB.ToString() == "*") {
                            z001_etrs89andrdnap[RowPointer, DelimCounter] = Double.NaN;
                          }
                          else {
                            z001_etrs89andrdnap[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString());
                          }
                          break;
                  case 4: z002_etrs89[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                  case 5: z002_rdnap[RowPointer, DelimCounter] = Convert.ToDouble(SB.ToString()); break;
                  default: break;
                }
              }
              SB.Clear();
              RowPointer++;
              DelimCounter = 0;
            }
            else {
              ReadChar = Convert.ToChar(ReadIntChar);
              if (ReadChar == '.') { 
                ReadChar = ',';
              }
              SB.Append(ReadChar);
            }
          }
        }
        SRin.Close();
        SRin.Dispose();
        FSin.Close();
        FSin.Dispose();
        return true;
      }
      catch (Exception ex) {
        SRin.Close();
        SRin.Dispose();
        FSin.Close();
        FSin.Dispose();
        errCode = 1;
        errMsg = ex.Message;
        MessageBox.Show(MethodBase.GetCurrentMethod().Name + "\r\n" + ex.Message, "RDNAPTRAN2018", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return false;
      }
    } // Load_grid_file ()

    /// <summary>
    /// Transform ETRS coordinates to geograhic ellipsoidal pseudo Bessel coordinates.
    /// </summary>
    /// <param name="pLat">Latitude in radian notation. A double. Example: 0.155951112 </param>
    /// <param name="pLon">Longitude in radian notation. A double. Example: 0.439678554 </param>
    private void RD_datum_transformation(double pLat, double pLon) {
      /*
      Three steps:  
      - Conversion from ellipsoidal geographic ETRS89 coordinates to geocentric Cartesian ETRS89 coordinates to 
        be able to apply a 3D similarity transformation.
      - The 3D similarity transformation itself.
      - Conversion from geocentric Cartesian Bessel coordinates to ellipsoidal geographic Bessel coordinates, also called
        pseudo Bessel coordinates.
      Note: Some variables are re-used. The output of a step is used as input for the next step.

      The following public variables are set:
      - geoc_cart_ETRS89_X         : 
      - geoc_cart_ETRS89_Y         : 
      - geoc_cart_ETRS89_Z         : 
      - geoc_cart_RD_Bessel_X      : 
      - geoc_cart_RD_Bessel_Y      :  
      - geoc_cart_RD_Bessel_Z      : 
      - geog_ellips_psdo_Bessel_lat: The geograhic ellipsoidal pseudo Bessel latitude in radian.
      - geog_ellips_psdo_Bessel_lon: The geograhic ellipsoidal pseudo Bessel longitude in radian.
      */
      double a_grs80, f_grs80;
      double h;
      double RN, X, Y, Z, phi, labda;
      double e_square_grs80;

      double X1, Y1, Z1;
      double s;
      double R11, R12, R13, R21, R22, R23, R31, R32, R33;
      double X2, Y2, Z2;
  
      double a_b1841, f_b1841, epsilon;
      double e_square_b1841;
      double i, phi1;    // phi1 the new calculated one

      phi   = pLat;                      // 𝜑 phi
      labda = pLon;                      // 𝜆 labda

      /*
      Conversion to geocentric cartesian ETRS89 coordinates. The ellipsoidal geographic ETRS89 coordinates must be converted to 
      geocentric Cartesian ETRS89 coordinates to be able to apply a 3D similarity transformation.
      */
      a_grs80 = GRS80_a;
      f_grs80 = GRS80_f;
      h = ETRS89_h0;

      e_square_grs80 = f_grs80 * (2-f_grs80);
      RN = a_grs80/Math.Sqrt(1 - (e_square_grs80 * Math.Pow(Math.Sin(phi),2)));
 
      X = (RN + h) * Math.Cos(phi) * Math.Cos(labda);
      Y = (RN + h) * Math.Cos(phi) * Math.Sin(labda);
      Z = ((RN* (1 - e_square_grs80)) + h) * Math.Sin(phi);

      geoc_cart_ETRS89_X = X;
      geoc_cart_ETRS89_Y = Y;
      geoc_cart_ETRS89_Z = Z;
    
      /*
      Rigorous 3D similarity transformation of geocentric Cartesian coordinates.
      The formula for a 3D similarity transformation must be applied to the geocentric Cartesian ETRS89 coordinates of the
      point of interest. The obtained geocentric Cartesian coordinates are in the geodetic datum of RD. The geodetic datum
      is often referred to as RD Bessel or just Bessel, even though geocentric Cartesian coordinates do not use the Bessel ellipsoid.
      */
      X1 = X;
      Y1 = Y;
      Z1 = Z;

      s = 1 + delta;
      R11 = Math.Cos(gamma) * Math.Cos(beta);
      R12 = Math.Cos(gamma) * Math.Sin(beta) * Math.Sin(alpha)  + Math.Sin(gamma) * Math.Cos(alpha);
      R13 = -Math.Cos(gamma) * Math.Sin(beta) * Math.Cos(alpha) + Math.Sin(gamma) * Math.Sin(alpha);
      R21 = -Math.Sin(gamma) * Math.Cos(beta);
      R22 = -Math.Sin(gamma) * Math.Sin(beta) * Math.Sin(alpha) + Math.Cos(gamma) * Math.Cos(alpha);
      R23 = Math.Sin(gamma) * Math.Sin(beta) * Math.Cos(alpha) + Math.Cos(gamma) * Math.Sin(alpha);
      R31 = Math.Sin(beta);
      R32 = - Math.Cos(beta) * Math.Sin(alpha);
      R33 = Math.Cos(beta) * Math.Cos(alpha);

      X2 = s * (R11 * X1 + R12 * Y1 + R13 * Z1) + tX;
      Y2 = s * (R21 * X1 + R22 * Y1 + R23 * Z1) + tY;
      Z2 = s * (R31 * X1 + R32 * Y1 + R33 * Z1) + tZ;
      geoc_cart_RD_Bessel_X = X2;
      geoc_cart_RD_Bessel_Y = Y2;
      geoc_cart_RD_Bessel_Z = Z2;

      /*
      The geocentric Cartesian Bessel coordinates of the point of interest must be converted back to ellipsoidal geographic
      Bessel coordinates. The ellipsoidal height is not given in the formula, as it is not used. The formula for latitude contains
      the latitude itself. The initial value for the latitude should be used to obtain an approximation of the latitude.
      This approximate value is then used to obtain an improved approximation. The latitude is computed iteratively until
      the difference between subsequent iterations becomes smaller than the precision threshold.
      Note: The ellipsoidal geographic coordinates of a point of interest obtained by datum transformation are pseudo Bessel coordinates.
      */
      X = X2;
      Y = Y2;
      Z = Z2;
      a_b1841 = B1841_a;
      f_b1841 = B1841_f;
      epsilon = epsilon_B1841_threshold;
      e_square_b1841 = f_b1841 * (2-f_b1841);

      /*
      Loop until you have reached the precision threshold. It is an UNTIL loop, thus you always enter the loop once.
      The check is done at the bottom. (In SAS syntax the check is listed at the top).
      */
      phi1 = ETRS89_lat_dec;                               // correct, it is an UNTIL loop, thus it is initialized in the loop
      i = 0;                                        // iterate counter.To check how many times you iterate
      do {
        phi = phi1;
        RN = a_b1841/Math.Sqrt(1 - (e_square_b1841* Math.Pow(Math.Sin(phi),2)));
        if (X > 0) {
            phi1 = Math.Atan((Z + e_square_b1841 * RN * Math.Sin(phi)) / Math.Sqrt(Math.Pow(X,2) + Math.Pow(Y,2)));
          }
        else {
          if (Math.Round(X,12) == 0 && Math.Round(Y,12) == 0 && Math.Round(Z,12) >= 0) { 
            phi1 = Math.PI / 2;           // +90 degrees
          }
          else {
            phi1 = -Math.PI / 2;     // -90 degrees
          }
          }
        i++;
      } while (Math.Abs(phi1-phi) >= epsilon);

      // The phi or lat value(phi1) has been calculated.Now calculate the labda or lon value
      if (X > 0 ) {
        labda = Math.Atan(Y/X);
      }
      else {
        if (X < 0 && Y >= 0) {
          labda = Math.Atan(Y/X) + Math.PI;    // +180 degrees;
        }
        else {
          if (X < 0 && Y < 0) {
            labda = Math.Atan(Y/X) - Math.PI;    // -180 degrees;
          }
          else {
            if (X == 0 && Y > 0) {
              labda = Math.PI/2;              //  +90 degrees;
            }
            else {
              if (X == 0 && Y < 0) {
                labda = -Math.PI/2;         //  -90 degrees;
              }
              else {
                if (X == 0 && Y == 0) {
                  labda = 0;
                }
              }
            }
          }
        }
      }
      geog_ellips_psdo_Bessel_lat = phi1;
      geog_ellips_psdo_Bessel_lon = labda;
    } // RD_datum_transformation()

    /// <summary>
    /// Correct the Pseudo Bessel coordinate into Real Bessel coordinates.
    /// </summary>
    /// <param name="pLat">Pseudo Bessel RD latitude in radian</param>
    /// <param name="pLon">Pseudo Bessel RD longitude in radian</param>
    private void RD_correction(double pLat, double pLon) {
      /*
      The ellipsoidal geographic coordinates of a point of interest obtained by datum transformation are pseudo Bessel coordinates.
      They must be corrected up to 0.25 m to obtain real Bessel coordinates. The horizontal ellipsoidal geographic coordinates of the 
      correction grid points are in real Bessel. Therefore, also the coordinates of the point of interest are needed in real Bessel to 
      determine the right correction. When transforming from ETRS89 to RD, the real Bessel coordinates are needed to correct the pseudo 
      Bessel coordinates to real Bessel coordinates. To solve this, the real Bessel coordinates are computed iteratively, until the 
      difference between subsequent iterations becomes smaller than the precision threshold.
      The correction RD file is an array, rdcorr2018. Indexing starts with 0.
 
      The following public variables are set:
      - geog_ellips_real_Bessel_lat: The geographic ellipsoidal real Bessel latitude in decimal degrees.
      - geog_ellips_real_Bessel_lon: The geographic ellipsoidal real Bessel longitude in decimal degrees.
      */
      double phi0;                    // the pseudo RD Bessel latitude, parameter pLat, and converted to degrees
      double phi;                     // the latitude in degrees
      double phi1;                    // the new corrected latitude in the loop(degrees)
      double labda0;                  // the pseudo RD Bessel longitude, parameter pLon, , and converted to degrees
      double labda;                   // the longitude in degrees
      double labda1;                  // the new corrected longitude in the loop(degrees)
      double phinorm;                 // the normalized longitude of point of interest(dimensionless)
      double labdanorm;               // the normalized longitude of point of interest(dimensionless)

      double RDcorrLat, RDcorrLon;
      double nlabda;
      int i_nw, i_ne, i_sw, i_se;
      double nw_phi=0, nw_labda=0;
      double ne_phi=0, ne_labda=0;
      double sw_phi=0, sw_labda=0;
      double se_phi=0, se_labda=0;
      double epsilon;

      bool phi_threshold_bln;       // boolean: latitude threshold is met or not met
      bool labda_threshold_bln;     // boolean: longitude threshold is met or not met
      bool inside_bound_correction_grid;

      // Initialize the loop, and convert to degrees;
      phi = pLat * 180 / Math.PI;
      labda = pLon * 180 / Math.PI;
      phi0 = phi;
      phi1 = phi;
      labda0 = labda;
      labda1 = labda;

      epsilon = epsilon_RD_threshold;
      phi_threshold_bln = false;         // loop exit criteria or the phi/latitude;
      labda_threshold_bln = false;       // loop exit criteria or the labda/longitude;

      // Here we go into the loop.The phi and labda correction are indenpent of each other
      do {  
       // Get the index used to retrieve RD correction values from the correction table
        phi = phi1;
        labda = labda1;

        phinorm = (phi-phi_min)/phi_delta;
        labdanorm = (labda-labda_min)/labda_delta;
        nlabda = 1 + ((labda_max-labda_min)/labda_delta);
     
        if (phi >= phi_min && phi <= phi_max && labda >= labda_min && labda <= labda_max) {
          inside_bound_correction_grid = true;

          i_nw = Convert.ToInt32(Math.Ceiling(phinorm)*nlabda + Math.Floor(labdanorm));
          i_ne = Convert.ToInt32(Math.Ceiling(phinorm)*nlabda + Math.Ceiling(labdanorm));
          i_sw = Convert.ToInt32(Math.Floor(phinorm)*nlabda + Math.Floor(labdanorm));
          i_se = Convert.ToInt32(Math.Floor(phinorm)*nlabda + Math.Ceiling(labdanorm));
 
          nw_phi = rdcorr2018[i_nw,2];
          ne_phi = rdcorr2018[i_ne,2];
          sw_phi = rdcorr2018[i_sw,2];
          se_phi = rdcorr2018[i_se,2];
          nw_labda = rdcorr2018[i_nw,3];
          ne_labda = rdcorr2018[i_ne,3];
          sw_labda = rdcorr2018[i_sw,3];
          se_labda = rdcorr2018[i_se,3];
        }
        else {
          inside_bound_correction_grid = false;
        }

        // Here we calculate lat and lon correction at point of interest in real RD Bessel;
        if (phi_threshold_bln == false) {
          if (inside_bound_correction_grid == true) {
            RDcorrLat = (phinorm - Math.Floor(phinorm)) * ((nw_phi * (Math.Floor(labdanorm) + 1 - labdanorm)) + ne_phi * (labdanorm - Math.Floor(labdanorm))) + (Math.Floor(phinorm) + 1 - phinorm) * ((sw_phi * (Math.Floor(labdanorm) + 1 - labdanorm)) + se_phi * (labdanorm - Math.Floor(labdanorm)));
          }
          else {
            RDcorrLat = c0;
          }
          phi1 = phi0 - RDcorrLat;
          if (Math.Abs(phi1-phi) < epsilon) {
            phi_threshold_bln = true;
          }
        }

        if (labda_threshold_bln == false) { 
          if (inside_bound_correction_grid == true) {
            RDcorrLon = (phinorm - Math.Floor(phinorm)) * ((nw_labda * (Math.Floor(labdanorm) + 1 - labdanorm)) + ne_labda * (labdanorm - Math.Floor(labdanorm))) + (Math.Floor(phinorm) + 1 - phinorm) * ((sw_labda * (Math.Floor(labdanorm) + 1 - labdanorm)) + se_labda * (labdanorm - Math.Floor(labdanorm)));
          }
          else {
            RDcorrLon = c0;
          }
          labda1 = labda0 - RDcorrLon;
          if (Math.Abs(labda1-labda) < epsilon) {
            labda_threshold_bln = true;
          }
        }
      } while ( phi_threshold_bln == false  && labda_threshold_bln == false);

      geog_ellips_real_Bessel_lat = phi1;
      geog_ellips_real_Bessel_lon = labda1;
    } // RD_correction

    /// <summary>
    /// Projection from ellipsoid to sphere
    /// </summary>
    /// <param name="pLat">Real Bessel Latitude on ellips, in decimal degrees</param>
    /// <param name="pLon">Real Bessel Longitude on ellips, in decimal degrees</param>
    private void RD_map_projection(double pLat, double pLon) {
      /*
      Projection from ellipsoid to sphere. The corrected ellipsoidal geographic Bessel coordinates of a point of interest must 
      be projected to obtain RD coordinates. Two steps:
      - Gauss conformal projection of coordinates on ellipsoid to coordinates on sphere.
      - Projection from sphere to plane.

      The following public variables are set:
      - RD_x_lon: The x-coordinate in meter.
      - RD_y_lat: The y-coordinate in meter.
      */
      double phi, phi0, PHI_C, PHI0_C,labda, labda0, LABDA_C, LABDA0_C;
      double k, x0, y0, a, f, e;
      double RM, RN, R_sphere;
      double q0, w0, q, w, m, n;
      double sin_psi_2, cos_psi_2, tan_psi_2;
      double sin_alpha, cos_alpha, r_distance;
      double x, y;                                          // the RD x y coordinates;

      phi = pLat;
      labda = pLon;

      // Gauss conformal projection of coordinates on ellipsoid to coordinates on sphere.
      // Convert the input values back to radian.
      phi = phi * Math.PI / 180;
      labda = labda * Math.PI / 180;

      // Get the parameters of RD Map projection and Bessel 1841 ellipsoid parameter;
      phi0 = phi0_amersfoort;
      labda0 = labda0_amersfoort;
      k = k_amersfoort;
      x0 = x0_amersfoort;
      y0 = y0_amersfoort;
      a = B1841_a;
      f = B1841_f;

      // Start with derived parameter calculation of the RD map projection;
      e =  Math.Sqrt(f*(2-f));
      q0 = Math.Log(Math.Tan((phi0 + const_90_degrees)/2)) - e/2 * Math.Log((1 + e * Math.Sin(phi0))/(1 - e * Math.Sin(phi0)));
      RN = a / Math.Sqrt(1 - (Math.Pow(e,2)*Math.Pow(Math.Sin(phi0),2)));
      RM = RN * (1 - Math.Pow(e,2)) / (1 - (Math.Pow(e,2) * Math.Pow(Math.Sin(phi0),2)));
      R_sphere = Math.Sqrt(RM * RN);
      PHI0_C = Math.Atan((Math.Sqrt(RM)/Math.Sqrt(RN)) * Math.Tan(phi0)); 
      LABDA0_C = labda0;
      w0 = Math.Log(Math.Tan((PHI0_C + const_90_degrees)/2));
      n = Math.Sqrt(1 + (Math.Pow(e,2) * Math.Pow(Math.Cos(phi0),4)/(1 - Math.Pow(e,2))));
      m = w0 - n * q0;

      /*
      Gauss conformal projection of coordinates on ellipsoid to coordinates on sphere. To prevent undefined results due 
      to taking the tangent of a number close to 90°, a tolerance should be used to test if the latitude is 90°. 
      Rounding the ellipsoidal coordinates in advance to 0.000 000 001° or 2*10−11 rad suffices.
      No rounding done and no 90° and -90° check. 
      */
      q = Math.Log(Math.Tan((phi + const_90_degrees)/2)) - (e/2) * (Math.Log((1 + e * Math.Sin(phi))/(1 - e * Math.Sin(phi))));
      w = n * q + m;
      PHI_C =  2 * Math.Atan(Math.Exp(w)) - const_90_degrees;
      LABDA_C = LABDA0_C + n* (labda - labda0);

      /*
      Projection from sphere to plane: the second step of the RD map projection of the point of interest is an oblique 
      stereographic conformal projection from sphere to a plane to obtain RD coordinates    
      To prevent arithmetic overflow due to division by a number close to zero, a tolerance should be used to test if the
      point of interest is close to the central point itself or the point opposite on the sphere. Rounding the spherical 
      coordinates in advance to 0.000 000 001° or 2*10−11 rad suffices. No rounding is done here.
      */
      sin_psi_2 = Math.Sqrt(Math.Pow(Math.Sin((PHI_C - PHI0_C)/2),2) + (Math.Pow(Math.Sin((LABDA_C - LABDA0_C)/2),2) * Math.Cos(PHI_C) * Math.Cos(PHI0_C)));
      cos_psi_2 = Math.Sqrt(1 - Math.Pow(sin_psi_2,2));
      tan_psi_2 = sin_psi_2/cos_psi_2;
      sin_alpha = Math.Sin(LABDA_C - LABDA0_C) * Math.Cos(PHI_C)/(2 * sin_psi_2 * cos_psi_2);
      cos_alpha = (Math.Sin(PHI_C) - Math.Sin(PHI0_C) + 2 * Math.Sin(PHI0_C)*Math.Pow(sin_psi_2,2))/(2* Math.Cos(PHI0_C) * sin_psi_2 * cos_psi_2);
      r_distance = 2 * k * R_sphere * tan_psi_2;

      if (PHI_C == PHI0_C && LABDA_C == LABDA0_C) {
        x = x0;
        y = y0;
      }
      else { 
        if ((PHI_C != PHI0_C || LABDA_C != LABDA0_C) && (PHI_C != -PHI0_C || LABDA_C != (const_180_degrees-LABDA0_C))) {
          x = r_distance* sin_alpha + x0;
          y = r_distance* cos_alpha + y0;
        }
        else {    // undefined;
          x = 0;
          y = 0;
        }
      }

      RD_x_lon = x;
      RD_y_lat = y;
    } // RD_map_projection()

    /// <summary>
    /// Height conversion. Supports both ways.
    /// </summary>
    /// <param name="pLat">The latitude in decimal degrees</param>
    /// <param name="pLon">The longitude in decimal degrees</param>
    /// <param name="pH">The height. In NAP meter, or ETRS89 meter</param>
    /// <param name="pType">The conversion type: 1: from ETRS89 to RD, 2: from RD to ETRS89</param>
    private void Height_transformation(double pLat, double pLon, double pH, int pType) {
      /*
      ETRS89 to RD:
      The ellipsoidal height is not used with RD coordinates as it is purely geometrical and has no physical meaning. The
      height transformation from ellipsoidal ETRS89 height of a point of interest to NAP height is based on the quasi-geoid
      model NLGEO2018. The quasi-geoid height at the point of interest is obtained by bilinear interpolation of a regular grid of
      quasi-geoid height values.
      RD to ETRS89:
      The physical NAP height of a point of interest can be transformed to the purely geometrical ellipsoidal ETRS89 height. The
      height transformation from NAP to ETRS89 is based on the quasi-geoid model NLGEO2018. The quasi-geoid height at the
      point of interest is obtained by bilinear interpolation of a regular grid of quasi-geoid height values
      The NAP file is an array, nlgeo2018. Indexing starts with 0.

      The following public variables are set:
      - NAP_height   : The NAP height in meter. When converting form ETRS89 to RD.
      - ETRS89_height: The ETRS height in meter. When converting form RD to ETRS89.
      */
      double phi, labda, height, conv_height;
      double phinorm, labdanorm;
      double nlabda;
      int i_nw, i_ne, i_sw, i_se;
      double nw_height, ne_height, sw_height, se_height;
      double etrs89_quasi_height;

      phi = pLat;
      labda = pLon;
      height = pH;

      phinorm = (phi-phi_min)/phi_delta;
      labdanorm = (labda-labda_min)/labda_delta;
      nlabda = 1 + ((labda_max-labda_min)/labda_delta);

      // Rounding needed for the test validation
      if (Math.Round(phi,9) >= phi_min && Math.Round(phi,9) <= phi_max && Math.Round(labda,9) >= labda_min && Math.Round(labda,9) <= labda_max) {

        i_nw = Convert.ToInt32(Math.Ceiling(phinorm)*nlabda + Math.Floor(labdanorm));
        i_ne = Convert.ToInt32(Math.Ceiling(phinorm)*nlabda + Math.Ceiling(labdanorm));
        i_sw = Convert.ToInt32(Math.Floor(phinorm)*nlabda + Math.Floor(labdanorm));
        i_se = Convert.ToInt32(Math.Floor(phinorm)*nlabda + Math.Ceiling(labdanorm));

        nw_height = nlgeo2018[i_nw,2];
        ne_height = nlgeo2018[i_ne,2];
        sw_height = nlgeo2018[i_sw,2];
        se_height = nlgeo2018[i_se,2]; 

        etrs89_quasi_height = (phinorm - Math.Floor(phinorm)) * ((nw_height * (Math.Floor(labdanorm) + 1 - labdanorm)) + ne_height * (labdanorm - Math.Floor(labdanorm))) + (Math.Floor(phinorm) + 1 - phinorm) * ((sw_height * (Math.Floor(labdanorm) + 1 - labdanorm)) + se_height * (labdanorm - Math.Floor(labdanorm)));
      }
      else {
        etrs89_quasi_height = Double.NaN;
      }
      if (pType == 1) {
        if (!Double.IsNaN(etrs89_quasi_height)) {
          conv_height = height - etrs89_quasi_height;
        }
        else {
          conv_height = Double.NaN;
        }
        NAP_height = conv_height;
      }
      else {
        if (!Double.IsNaN(etrs89_quasi_height)) {
          conv_height = height + etrs89_quasi_height;
        }
        else {
          conv_height = Double.NaN;
        }
        ETRS89_height = conv_height;;
      }
    } // Height_transformation()

    /// <summary>
    /// Transform RD coordinates to geographic ellipsoidal real Bessel coordinates
    /// </summary>
    /// <param name="pX">RD X coordinate in meter</param>
    /// <param name="pY">RD Y coordinate in meter</param>
    private void Inverse_map_projection(double pX, double pY) {
      /*
      Projection from plane to sphere to ellipsoid: RD coordinates of a point of interest must be converted to Bessel coordinates.
      Two steps:
      - Inverse oblique stereographic conformal projection from the RD projection plane to a sphere. 
      - Inverse Gauss conformal projection from the sphere to the Bessel ellipsoid to obtain Bessel coordinates.

      The following public variables are set:
      - geog_sphere_real_Bessel_lat: 
      - geog_sphere_real_Bessel_lon: 
      - geog_ellips_real_Bessel_lat: Geographic ellipsoidal real Bessel latitude in radian.
      - geog_ellips_real_Bessel_lon: Geographic ellipsoidal real Bessel longitude in radian.
      */
      double x, y;
      double phi, phi1, phi0, PHI_C, PHI0_C, labda_n, labda, labda0, LABDA_C = 0, LABDA0_C;
      double k, x0, y0, a, f, e;
      double RM, RN, R_sphere;
      double psi, sin_alpha, cos_alpha, r_distance;
      double Xnorm, Ynorm, Znorm;
      double epsilon;
      double q0, w0, q, w, m, n, i;

      x = pX;
      y = pY;

      // Get the parameters of RD Map projection and Bessel 1841 ellipsoid parameter
      phi0 = phi0_amersfoort; 
      labda0 = labda0_amersfoort; 
      k = k_amersfoort;
      x0 = x0_amersfoort;
      y0 = y0_amersfoort;
      a = B1841_a;
      f = B1841_f;
      epsilon = epsilon_B1841_threshold;

      // Start with derived parameter calculation of the RD map projection
      e =  Math.Sqrt(f*(2-f));
      RN = a / Math.Sqrt(1 - (Math.Pow(e,2) * Math.Pow(Math.Sin(phi0),2)));
      RM = RN * (1 - Math.Pow(e,2))/(1 - (Math.Pow(e,2) * Math.Pow(Math.Sin(phi0),2)));
      R_sphere = Math.Sqrt(RM * RN);
      PHI0_C = Math.Atan(Math.Sqrt(RM)/Math.Sqrt(RN) * Math.Tan(phi0)); 
      LABDA0_C = labda0;

      // Inverse oblique stereographic projection of coordinates on plane to coordinates on sphere
      r_distance = Math.Sqrt(Math.Pow(x - x0,2) + Math.Pow(y - y0,2));
      sin_alpha = (x - x0)/r_distance;
      cos_alpha = (y - y0)/r_distance;
      psi =  2 * Math.Atan(r_distance/(2 * k * R_sphere));
      if (x != x0 || y != y0) {
        Xnorm = Math.Cos(PHI0_C) * Math.Cos(psi) - cos_alpha * Math.Sin(PHI0_C) * Math.Sin(psi);
        Ynorm = sin_alpha * Math.Sin(psi);
        Znorm = cos_alpha * Math.Cos(PHI0_C) * Math.Sin(psi) + Math.Sin(PHI0_C) * Math.Cos(psi);
      }
      else {
        Xnorm = Math.Cos(PHI0_C);
        Ynorm = 0;
        Znorm = Math.Sin(PHI0_C);
      }
      PHI_C = Math.Asin(Znorm);
      if (Xnorm > 0) {
        LABDA_C = LABDA0_C + Math.Atan(Ynorm / Xnorm);
      }
      else {
        if (Xnorm < 0 && x >= x0) {
          LABDA_C = LABDA0_C + Math.Atan(Ynorm / Xnorm) + const_180_degrees;
        }
        else {
          if (Xnorm < 0 && x < x0) {
            LABDA_C = LABDA0_C + Math.Atan(Ynorm / Xnorm) - const_180_degrees;
          }
          else {
            if (Xnorm == 0 && x > x0) {  
              LABDA_C = LABDA0_C + const_90_degrees;
            }
            else {
              if (Xnorm == 0 && x < x0) {
                LABDA_C = LABDA0_C - const_90_degrees;
              }
              else {
                if (Xnorm == 0 && x == x0) {
                  LABDA_C = LABDA0_C;
                }
              }
            }
          }
        }
      }

      geog_sphere_real_Bessel_lat = PHI_C;
      geog_sphere_real_Bessel_lon = LABDA_C;

      /*
      Projection from sphere to ellipsoid: The second step of the inverse RD map projection is an inverse
      Gauss conformal projection from the sphere to the Bessel ellipsoid to obtain Bessel coordinates.
      Start with remaining derived parameter calculation of the RD map projection.
      */
      q0 = Math.Log(Math.Tan((phi0 + const_90_degrees)/2)) - (e/2) * Math.Log((1 + e * Math.Sin(phi0))/(1 - e * Math.Sin(phi0)));
      w0 = Math.Log(Math.Tan((PHI0_C + const_90_degrees)/2));
      n = Math.Sqrt(1 + (Math.Pow(e,2) * Math.Pow(Math.Cos(phi0),4)/(1 - Math.Pow(e,2))));
      m = w0 - n * q0;

      // Inverse Gauss conformal projection of coordinates on sphere to coordinates on ellipsoid
      w = Math.Log(Math.Tan((PHI_C + const_90_degrees)/2));
      q = (w - m)/n;    
 
      /*
      Loop until you have reached the precision threshold. It is an UNTIL loop, thus you always enter the loop once.
      The check is done at the bottom. (In SAS syntax the check is listed at the top).
      */
      phi1 = PHI_C;    // correct, it is an UNTIL loop, thus it is initialized in the loop
      i = 0;           // iterate counter.To check how many times you iterate
      do {;
        phi = phi1;
        if (PHI_C > -1 * const_90_degrees &&  PHI_C < const_90_degrees) { 
          phi1 = 2 * Math.Atan(Math.Exp(q + (e/2) * Math.Log((1 + e * Math.Sin(phi))/(1 - e * Math.Sin(phi))))) - const_90_degrees;
        }
        else {
          phi1 = PHI_C;
        i++;
        }
      } while (Math.Abs(phi1 - phi) >= epsilon);
    
      // The latitute has been calculated, now calculate the longitude;
      labda_n = ((LABDA_C - LABDA0_C)/n) + labda0;
      labda = labda_n + const_360_degrees * Math.Floor((const_180_degrees - labda_n)/const_360_degrees);

      geog_ellips_real_Bessel_lat = phi1;
      geog_ellips_real_Bessel_lon = labda;
    } // Inverse_map_projection()

    /// <summary>
    /// Correct the horizontal ellipsoidal geographic real Bessel coordinates to pseudo Bessel coordinates
    /// </summary>
    /// <param name="pLat">Geographic ellipsoidal real Bessel latitude in radian</param>
    /// <param name="pLon">Geographic ellipsoidal real Bessel longitude in radian</param>
    private void Inverse_correction(double pLat, double pLon) {
      /*
      The horizontal ellipsoidal geographic real Bessel coordinates of the point of interest must be corrected to pseudo Bessel coordinates
      using the interpolated correction grid value of the point of interest. No iteration is needed for the transformation from RD to ETRS89
      coordinates as the grid is given in real Bessel coordinates.
      The correction RD file is an array, rdcorr2018. Indexing starts with 0.

      The following public variables are set:
      - geog_ellips_psdo_Bessel_lat: Geographic ellipsoidal pseudo Bessel latitude in decimal degrees.
      - geog_ellips_psdo_Bessel_lon: Geographic ellipsoidal pseudo Bessel longitude in decimal degrees.
      */
      double RDcorrLat, RDcorrLon;
      double phi, phi1;
      double labda, labda1;
      double phinorm, labdanorm;
      double nlabda;
      int i_nw, i_ne, i_sw, i_se;
      double nw_phi, nw_labda;
      double ne_phi, ne_labda;
      double sw_phi, sw_labda;
      double se_phi, se_labda;

      phi = pLat * 180 / Math.PI;        // in degrees
      labda = pLon * 180 / Math.PI;

      phinorm = (phi-phi_min)/phi_delta;
      labdanorm = (labda-labda_min)/labda_delta;
      nlabda = 1 + ((labda_max-labda_min)/labda_delta);
     
      if (phi >= phi_min && phi <= phi_max && labda >= labda_min && labda <= labda_max ) { 

        i_nw = Convert.ToInt32(Math.Ceiling(phinorm) * nlabda + Math.Floor(labdanorm));
        i_ne = Convert.ToInt32(Math.Ceiling(phinorm) * nlabda + Math.Ceiling(labdanorm));
        i_sw = Convert.ToInt32(Math.Floor(phinorm) * nlabda + Math.Floor(labdanorm));
        i_se = Convert.ToInt32(Math.Floor(phinorm) * nlabda + Math.Ceiling(labdanorm));

        nw_phi = rdcorr2018[i_nw, 2];
        ne_phi = rdcorr2018[i_ne, 2];
        sw_phi = rdcorr2018[i_sw, 2];
        se_phi = rdcorr2018[i_se, 2];
        nw_labda = rdcorr2018[i_nw, 3];
        ne_labda = rdcorr2018[i_ne, 3];
        sw_labda = rdcorr2018[i_sw, 3];
        se_labda = rdcorr2018[i_se, 3];

        RDcorrLat = (phinorm - Math.Floor(phinorm)) * ((nw_phi * (Math.Floor(labdanorm) + 1 - labdanorm)) + ne_phi * (labdanorm - Math.Floor(labdanorm))) + (Math.Floor(phinorm) + 1 - phinorm) * ((sw_phi * (Math.Floor(labdanorm) + 1 - labdanorm)) + se_phi * (labdanorm - Math.Floor(labdanorm)));
        RDcorrLon = (phinorm - Math.Floor(phinorm)) * ((nw_labda * (Math.Floor(labdanorm) + 1 - labdanorm)) + ne_labda * (labdanorm - Math.Floor(labdanorm))) + (Math.Floor(phinorm) + 1 - phinorm) * ((sw_labda * (Math.Floor(labdanorm) + 1 - labdanorm)) + se_labda* (labdanorm - Math.Floor(labdanorm)));
      }
      else { 
        RDcorrLat = 0;
        RDcorrLon = 0;
      }
      phi1 = phi + RDcorrLat;
      labda1 = labda + RDcorrLon;

      geog_ellips_psdo_Bessel_lat = phi1;
      geog_ellips_psdo_Bessel_lon = labda1;
    } // Inverse_correction;

    /// <summary>
    /// Transform the corrected ellipsoidal geographic Bessel coordinates to ellipsoidal geographic ETRS89 coordinates
    /// </summary>
    /// <param name="pLat">Geographic ellipsoidal pseudo Bessel latitude in decimal degrees</param>
    /// <param name="pLon">Geographic ellipsoidal pseudo Bessel longitude in decimal degrees</param>
    private void Inverse_datum_transformation(double pLat, double pLon) { 
      /*
      The corrected ellipsoidal geographic Bessel coordinates of a point of interest must be transformed to ellipsoidal 
      geographic ETRS89 coordinates. Three steps:
      - The ellipsoidal geographic Bessel coordinates of a point of interest must be converted to geocentric Cartesian Bessel 
        coordinates to be able to apply a 3D similarity transformation.
      - The 3D similarity transformation itself.
      - Convert the geocentric ETR89 coordinates to ellipsoidal geographic ETRS89 coordinates. The latitude is computed iteratively. 
        The parameters of the GRS80 ellipsoid are needed for the conversion.

      The following public variables are set:
      - geoc_cart_ETRS89_X
      - geoc_cart_ETRS89_Y
      - geoc_cart_ETRS89_Z
      - geoc_cart_RD_Bessel_X
      - geoc_cart_RD_Bessel_Y
      - geoc_cart_RD_Bessel_Z
      - ETRS89_lat_dec degrees: The ETRS89 latitude in decimal degrees.
      - ETRS89_lon_dec degrees: The ETRS89 longitude in decimal degrees.
      */
      double a_b1841, f_b1841;
      double h;
      double RN, X, Y, Z, phi, labda;
      double e_square_b1841;
      double X1, Y1, Z1;
      double a, b, g, d;
      double tX, tY, tZ;
      double s;
      double R11, R12, R13, R21, R22, R23, R31, R32, R33;
      double X2, Y2, Z2;
      double a_grs80, f_grs80, epsilon ;
      double e_square_grs80;
      double i, phi1;    // phi1 the new calculated one;

      /* Convert the ellipsoidal geographic Bessel coordinates to geocentric Cartesian Bessel coordinates. */
      phi = pLat * Math.PI/180;               // in radian
      labda = pLon * Math.PI/180;
      a_b1841 = B1841_a;
      f_b1841 = B1841_f;
      h = RD_Bessel_h0;

      e_square_b1841 = f_b1841* (2-f_b1841);
      RN = a_b1841/Math.Sqrt(1 - (e_square_b1841 * Math.Pow(Math.Sin(phi),2)));
      X = (RN + h) * Math.Cos(phi) * Math.Cos(labda);
      Y = (RN + h) * Math.Cos(phi) * Math.Sin(labda);
      Z = ((RN * (1 - e_square_b1841)) + h) * Math.Sin(phi);

      geoc_cart_ETRS89_X = X;
      geoc_cart_ETRS89_Y = Y;
      geoc_cart_ETRS89_Z = Z;

      /*
      Rigorous 3D similarity transformation of geocentric Cartesian coordinates. The obtained geocentric Cartesian
      coordinates are in the geodetic datum of RD. The geodetic datum is often referred to as RD Bessel or just Bessel, 
      even though geocentric Cartesian coordinates do not use the Bessel ellipsoid.
      */
      X1 = X;
      Y1 = Y;
      Z1 = Z;
      a = alpha_inv;
      b = beta_inv;
      g = gamma_inv;
      d = delta_inv;
      tX = tX_inv;
      tY = tY_inv;
      tZ = tZ_inv;

      s = 1 + d;
      R11 = Math.Cos(g) * Math.Cos(b);
      R12 = Math.Cos(g) * Math.Sin(b) * Math.Sin(a)  + Math.Sin(g) * Math.Cos(a);
      R13 = -Math.Cos(g) * Math.Sin(b) * Math.Cos(a) + Math.Sin(g) * Math.Sin(a);
      R21 = -Math.Sin(g) * Math.Cos(b);
      R22 = -Math.Sin(g) * Math.Sin(b) * Math.Sin(a) + Math.Cos(g) * Math.Cos(a);
      R23 = Math.Sin(g) * Math.Sin(b) * Math.Cos(a) + Math.Cos(g) * Math.Sin(a);
      R31 = Math.Sin(b);
      R32 = -Math.Cos(b) * Math.Sin(a);
      R33 = Math.Cos(b) * Math.Cos(a);

      X2 = s * (R11 * X1 + R12 * Y1 + R13 * Z1) + tX;
      Y2 = s * (R21 * X1 + R22 * Y1 + R23 * Z1) + tY;
      Z2 = s * (R31 * X1 + R32 * Y1 + R33 * Z1) + tZ;

      geoc_cart_RD_Bessel_X = X2;
      geoc_cart_RD_Bessel_Y = Y2;
      geoc_cart_RD_Bessel_Z = Z2;

      /*
      After the 3D similarity transformation, the geocentric ETR89 coordinates of the point of interest must be converted back
      to ellipsoidal geographic ETRS89 coordinates. The latitude is computed iteratively. The parameters of the GRS80 ellipsoid
      are needed for the conversion
      */
      X = X2;
      Y = Y2;
      Z = Z2;
      a_grs80 = GRS80_a;
      f_grs80 = GRS80_f;
      epsilon = epsilon_GRS80_threshold;
      e_square_grs80 = f_grs80 * (2-f_grs80);

      /*
      Loop until you have reached the precision threshold. It is an UNTIL loop, thus you always enter the loop once.
      The check is done at the bottom. (In SAS syntax the check is listed at the top).
      */
      phi1 = geog_sphere_real_Bessel_lat;  // correct, it is an UNTIL loop, thus it is initialized in the loop
      i = 0;                               // iterate counter.To check how many time you iterate
      do {
      phi = phi1;
        RN = a_grs80/Math.Sqrt(1 - (e_square_grs80 * Math.Pow(Math.Sin(phi),2)));
        if (X > 0 ) { 
          phi1 = Math.Atan((Z + e_square_grs80 * RN * Math.Sin(phi)) / Math.Sqrt(Math.Pow(X,2) + Math.Pow(Y,2)));
        }
        else {  
          if (Math.Round(X,12) == 0 && Math.Round(Y,12) == 0 &&  Math.Round(Z,12) >= 0) {
            phi1 = const_90_degrees;
          }
          else {
            phi1 = -1 * const_90_degrees; 
          }
        }
        i++;
      } while (Math.Abs(phi1-phi) >= epsilon);

      // The phi or lat value(phi1) has been calculated.Now calculate the labda or lon value
      if (X > 0) {
        labda = Math.Atan(Y/X);
      }
      else {
        if (X <= 0 && Y >= 0) {
          labda = Math.Atan(Y / X) + const_180_degrees; 
        }
        else {
          if (X < 0 && Y < 0) {
            labda = Math.Atan(Y / X) - const_180_degrees;
          }
          else {
            if (X == 0 && Y > 0) {
              labda = const_90_degrees;
            }
            else {
              if (X == 0 && Y < 0) {
                labda = -1 * const_90_degrees;
              }
              else {
                if (X == 0 && Y == 0) {
                  labda = 0;
                }
              }
            }
          }
        }
      }
      phi1 = phi1 * 180/Math.PI;     // in degrees;
      labda = labda * 180/ Math.PI;

      ETRS89_lat_dec = phi1;
      ETRS89_lon_dec = labda;
    } // Inverse_datum_transformation;

    #endregion

    #region calling conversion functions

    /// <summary>
    /// Transform ETRS89 to RD
    /// </summary>
    /// <param name="pLat">ETRS89 latitude. Can be in decimals or D M S notation. pLat and pLon must have same notation</param>
    /// <param name="pLon">ETRS89 longitude. Can be in decimals or D M S notation. pLat and pLon must have same notation</param>
    /// <param name="pH">ETRS89 height in meter. Default no height transformation</param>
    public void ETRS89_to_RD(double pLat, double pLon, double pH=double.NaN) {
      /*
      The following public variables are set:
      RD_x_lon  : RD x-coordinate in meter.
      RD_y_lat  : RD y-coordinate in meter.
      NAP_height: NAP height in RD meter. When NaN then no NAP height
      */
      bool height_bln;

      if (Double.IsNaN(pH)) { height_bln = false; }
      else { height_bln = true; }
      ETRS89_height = pH;
      ETRS89_lat_dec = pLat;
      ETRS89_lon_dec = pLon;
      ETRS89_to_RD_overload(height_bln);
    } // ETRS89_to_RD()

    /// <summary>
    /// Transform ETRS89 to RD.
    /// </summary>
    /// <param name="pLat">ETRS89 latitude. Can be in decimals or D M S notation. pLat and pLon must have same notation</param>
    /// <param name="pLon">ETRS89 longitude. Can be in decimals or D M S notation. pLat and pLon must have same notation</param>
    /// <param name="pH">ETRS89 height in meter. Default no height transformation</param>
    public void ETRS89_to_RD(string pLat, string pLon, double pH=double.NaN) {
      /*
      The following public variables are set:
      RD_x_lon  : RD x-coordinate in meter.
      RD_y_lat  : RD y-coordinate in meter.
      NAP_height: NAP height in RD meter. When NaN then no NAP height.
      */
      bool height_bln;

      if (Double.IsNaN(pH)) { height_bln = false; }
      else { height_bln = true; }
      ETRS89_height = pH;
      ETRS89_lat_dec = Conv_degr_dms_to_degr_dec(pLat);
      ETRS89_lon_dec = Conv_degr_dms_to_degr_dec(pLon);
      ETRS89_to_RD_overload(height_bln);
    } // ETRS89_to_RD()

    /// <summary>
    /// Transform ETRS89 to RD (The overload function)
    /// </summary>
    /// <param name="pH">Indicates if a height transformation must be made</param>
    private void ETRS89_to_RD_overload(bool pH) {
      Conv_dec_to_rad(ETRS89_lat_dec, ETRS89_lon_dec);
      RD_datum_transformation(ETRS89_lat_rad, ETRS89_lon_rad);
      RD_correction(geog_ellips_psdo_Bessel_lat, geog_ellips_psdo_Bessel_lon);
      RD_map_projection(geog_ellips_real_Bessel_lat, geog_ellips_real_Bessel_lon);
      if (pH == true) { 
        Height_transformation(ETRS89_lat_dec, ETRS89_lon_dec, ETRS89_height, 1);
      }
      else {
        NAP_height = double.NaN;
      }
    } // ETRS89_to_RD_overload()

    /// <summary>
    /// Transform RD to ETRS89
    /// </summary>
    /// <param name="pX">RD x-coordinate in meter </param>
    /// <param name="pY">RD y-coordinate in meter</param>
    /// <param name="pH">NAP height in meter. Default no height transformation</param>
    public void RD_to_ETRS89(double pX, double pY, double pH=Double.NaN) { 
      /*
      The following public variables are set:
      ETRS89_lat_dec      : ETRS89 latitude in decimal degrees.
      ETRS89_lon_dec      : ETRS89 latitude in decimal degrees.
      ETRS89_height: ETRS89 height in meter. When NaN then no ETRS height.
      */
      RD_x_lon = pX;
      RD_y_lat = pY;
      NAP_height = pH;
      Inverse_map_projection(RD_x_lon,RD_y_lat);
      Inverse_correction(geog_ellips_real_Bessel_lat, geog_ellips_real_Bessel_lon);
      Inverse_datum_transformation(geog_ellips_psdo_Bessel_lat,geog_ellips_psdo_Bessel_lon);
      if (!Double.IsNaN(pH)) {
        Height_transformation(ETRS89_lat_dec, ETRS89_lon_dec, NAP_height,2);
      }
      else {
        ETRS89_height = Double.NaN;
      }
    } // RD_to_ETRS89()

    #endregion

  }
}

