using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using MyExtentions.Components.BarcodeSymbology.Code128;

namespace MyExtentions.Components.BarcodeSymbology
{

    /// <summary>
    /// The basic detailed details of the Barcode
    /// Datas required for the barcode and description.
    /// </summary>
    public struct BarcodeInfo
    {
        private string _Barcode;
        public string Barcode { get { return _Barcode; } set { _Barcode = value; } }
        private string _Description;
        public string Description { get { return _Description; } set { _Description = value; } }
        /// <summary>
        /// Initialize Barcode data
        /// </summary>
        /// <param name="Barcode">Barcode Value</param>
        /// <param name="Description">Display Text</param>
        public BarcodeInfo(String Barcode, String Description)
        {
            this._Barcode = Barcode;
            this._Description = Description;
        }
    }
    /// <summary>
    /// Information Relating to the Rentering of the barcod
    /// in a Graphic.
    /// </summary>
    public struct BarcodeRenterInfo
    {
        private Font _TextFont;
        /// <summary>
        /// Font of Display Text in barcode is any.
        /// </summary>
        public Font TextFont { get { return _TextFont; } set { _TextFont = value; } }
        private RectangleF _PrintClip;
        /// <summary>
        /// Printable Rectangle clip.
        /// </summary>
        public RectangleF PrintClip { get { return _PrintClip; } set { _PrintClip = value; } }
        private Margins _QuitZone;
        /// <summary>
        /// Quit zone around the barcode.
        /// </summary>
        public Margins QuitZone { get { return _QuitZone; } set { _QuitZone = value; } }
        private Margins _ClipMargin;
        private Rectangle BarcodeRectangle;
        private Point BarcodeUpperLeft;
        /// <summary>
        /// Margin arround the printing margin
        /// </summary>
        public Margins ClipMargin { get { return _ClipMargin; } set { _ClipMargin = value; } }

        /// <summary>
        /// Initialize the clip Margin unique in all directions
        /// </summary>
        /// <param name="value">Margin for Top, Bottom, Right and Left</param>
        internal void SetMarginAll(int value)
        {
            this._ClipMargin = new Margins(value, value, value, value);
        }

        internal void SetQuitAllZone(int value)
        {
            throw new NotImplementedException();
        }
        private Graphics _Graphics;
        /// <summary>
        /// Drawing Graphics
        /// </summary>
        public Graphics Graphics
        {
            get { return _Graphics; }
            set
            {
                this._Graphics = value;
                this.PrintClip = value.VisibleClipBounds;
                //this.ClipMargin = new Margins(0, 0, 0, 0);
                //this.QuitZone = new Margins(0, 0, 0, 0);
            }
        }

        private int[] _Patterns;
        public int[] Patterns { get { return _Patterns; } set { _Patterns = value; } }

        //Printable Width 
        internal int GetPrintableWidth()
        {
            try
            {
                return (int)
                   this._PrintClip.Width -
                       (this._ClipMargin.Left +
                       this._ClipMargin.Right +
                       this._QuitZone.Left +
                       this._QuitZone.Right);
            }
            catch (Exception)
            { return 0; }
        }

        internal int GetPrintableHeight()
        {
            try
            {
                int barTextHeight = this.GetBarcodeTextHight();
                int descriptionHeight = this.GetDescriptionHight();
                return (int)
                   this._PrintClip.Height -
                       (this._ClipMargin.Top +
                       this._ClipMargin.Bottom +
                       this._QuitZone.Top +
                       this._QuitZone.Bottom +
                       barTextHeight + descriptionHeight
                       );
            }
            catch (Exception)
            { return 0; }
        }

        internal int SumPatterns()
        {
            int sum = 0;
            foreach (int item in Patterns)
            {
                sum += item;
            }
            return sum;
        }

        private int _BarWieight;
        public int BarWieight { get { return _BarWieight; } set { _BarWieight = value; } }

        private Color _BarColor;

        public Color BarColor
        {
            get
            {
                if (this._BarColor == null) this._BarColor = Color.Black; return this._BarColor;
            }
            set
            {
                _BarColor = value;
            }
        }

        internal void DrawBarcode(System.Drawing.Graphics gr, Rectangle printArea)
        {
            gr.FillRectangle(Brushes.Gray, printArea);
            gr.DrawRectangle(Pens.Black, printArea);
            //this._ShowBarcodeText = false;
            //this._ShowDescription = false;
            this.BarcodeRectangle = this.GetBarcodeRectangle();
            Size size = new Size((int)this.GetPrintableWidth(), (int)this.GetPrintableHeight());
            gr.PageUnit = GraphicsUnit.Pixel;
            this._PrintClip = gr.VisibleClipBounds;
            //gr.DrawRectangle(Pens.Green, Rectangle.Round(this._PrintClip));

            using (SolidBrush Brush = new SolidBrush(this.BarColor))
            {
                this.BarcodeTextRectangle =
                        new RectangleF(this.GetBarcodeTextPoint(), this.BarcodeTextSize);
                this.DescriptionTextRectangle =
                        new RectangleF(this.GetDescriptionPoint(), this.DescriptionSize);
                int pattternLength = this.Patterns.Length;
                for (int i = 0; i < pattternLength; i++)
                {
                    int barwidth = this.Patterns[i];
                    int tempOffset = this.SumPatterns(0, i);
                    int spcwidth = this.Patterns[++i];
                    RectangleF bar = new RectangleF(0, 0, barwidth * this._BarWieight, size.Height);
                    bar.Offset(BarcodeUpperLeft);
                    bar.Offset(tempOffset * this._BarWieight, 0);
                    gr.FillRectangle(Brush, bar);
                }
                using (StringFormat FormatCenter = new StringFormat())
                {
                    FormatCenter.Alignment = StringAlignment.Center;
                    gr.DrawString(this.BarcodeInfo.Description, this._TextFont, Brush,
                        DescriptionTextRectangle, FormatCenter);
                    gr.DrawString(this.BarcodeInfo.Barcode, this._TextFont, Brush,
                        BarcodeTextRectangle, FormatCenter);
                }
            }
        }

        private PointF GetDescriptionPoint()
        {
            PointF descriptionPointF = new PointF(
                this._PrintClip.Left + this._ClipMargin.Left,
                this._PrintClip.Top + this._ClipMargin.Top);
            return descriptionPointF;
        }

        private PointF GetBarcodeTextPoint()
        {
            PointF descriptionPointF = new PointF(
                this._PrintClip.Left + this._ClipMargin.Left,
                this._PrintClip.Bottom - (this._ClipMargin.Left + GetBarcodeTextHight()));
            return descriptionPointF;
        }

        private int SumPatterns(int start, int end)
        {
            int sum = 0;
            int item;
            for (int i = start; i < end; i++)
            {
                try
                {
                    item = this.Patterns[i];
                }
                catch (Exception)
                {
                    return sum;
                }
                sum += item;
            }
            return sum;
        }

        private Rectangle GetBarcodeRectangle()
        {
            Rectangle PrintClip;
            PrintClip = Rectangle.Round(this._PrintClip);
            int adjestX = (int)(this._PrintClip.Width - (this.BarWieight * this.SumPatterns())) / 2;
            this.BarcodeUpperLeft = new Point(
                (int)this.PrintClip.X + this._ClipMargin.Left + this._QuitZone.Left,
                (int)this.PrintClip.Y + this._ClipMargin.Top + this._QuitZone.Top + (this.ShowDescription ? GetDescriptionHight() : 0));
            BarcodeUpperLeft.X += adjestX;
            Size size = new Size((int)this.GetPrintableWidth(), (int)this.GetPrintableHeight());
            return new Rectangle(BarcodeUpperLeft, size);
        }

        private int GetDescriptionHight()
        {
            if (this.BarcodeInfo.Description == null)
            {
                this.DescriptionSize = new SizeF(0F, 0F);
                return 0;
            }
            else
            {
                this.DescriptionSize = this._Graphics.MeasureString(this.BarcodeInfo.Description, this._TextFont,
                    (int)(this._PrintClip.Width - (this._ClipMargin.Left + this._ClipMargin.Right)));
                this.DescriptionSize.Width = (float)(this._PrintClip.Width - (this._ClipMargin.Left + this._ClipMargin.Right));
                return (int)this.DescriptionSize.Height;
            }
        }

        private int GetBarcodeTextHight()
        {
            if (this.BarcodeInfo.Barcode == null)
            {
                this.BarcodeTextSize = new SizeF(0F, 0F);
                return 0;
            }
            else
            {
                float width = (float)(this._PrintClip.Width - (this._ClipMargin.Left + this._ClipMargin.Right));
                this.BarcodeTextSize = this._Graphics.MeasureString(this.BarcodeInfo.Barcode, this._TextFont,
                    (int)width);
                this.BarcodeTextSize.Width = width;
                return (int)this.BarcodeTextSize.Height;
            }
        }

        public bool _ShowBarcodeText;
        public bool ShowBarcodeText { get { return _ShowBarcodeText; } set { _ShowBarcodeText = value; } }

        public bool _ShowDescription;
        public bool ShowDescription { get { return _ShowDescription; } set { _ShowDescription = value; } }

        internal BarcodeInfo _BarcodeInfo;
        internal BarcodeInfo BarcodeInfo { get { return _BarcodeInfo; } set { _BarcodeInfo = value; } }

        private SizeF DescriptionSize;

        private SizeF BarcodeTextSize;
        private RectangleF BarcodeTextRectangle;
        private RectangleF DescriptionTextRectangle;
    }

    class Barcode : Code128Rendering, ICloneable
    {
        private int _barcodeType;
        private int _listId;
        private int _numberOfPrint;

        public delegate bool BarcodeCreation(String barcode);
        public event BarcodeCreation barcodeCreation;

        public Barcode(string barcode, string discription, string itemcode, float FontSize)
            : base(barcode, discription, itemcode)
        {
            this.initBarcode(barcode, discription, itemcode, FontSize);
        }
        public int BarcodeType
        {
            get
            {
                return _barcodeType;
            }
            set
            {
                this._barcodeType = value;
            }
        }
        public int ListId
        {
            get
            {
                return _listId;
            }
            set
            {
                this._listId = value;
            }
        }
        public int NumberOfPrint
        {
            get
            {
                return _numberOfPrint;
            }
            set
            {
                this._numberOfPrint = value;
            }
        }
        internal void intilalize(System.Data.DataRow item)
        {
            int i;
            Int32.TryParse(item[3].ToString(), out i);
            this.NumberOfPrint = i;

        }

        /*
        internal void CreateBarcode(string barcode, barcodePrinterForm barcodePrinterForm)
        {
            barcodeCreation.Invoke(barcode);
        }
         * */

        private BarcodeInfo _BarcodeInfo;
        private BarcodeRenterInfo _BarcodeRenterInfo;

        /// <summary>
        /// Barcode to be encoded
        /// </summary>
        public String BarcodeValue { set { this._BarcodeInfo.Barcode = value; } }
        /// <summary>
        /// Printable Description for the Barcode
        /// </summary>
        public String BarcodeDescription { set { this._BarcodeInfo.Description = value; } }
        /// <summary>
        /// Drawing Area Margin
        /// </summary>
        public int Margins { set { this._BarcodeRenterInfo.SetMarginAll(value); } }
        /// <summary>
        /// Quit Zone around the Barcode Image
        /// </summary>
        public int QuitZone { set { this._BarcodeRenterInfo.SetQuitAllZone(value); } }
        /// <summary>
        /// Get and Set the Drawable Clip
        /// </summary>
        public System.Drawing.RectangleF PrintClip
        {
            get { return this._BarcodeRenterInfo.PrintClip; }
            set { this._BarcodeRenterInfo.PrintClip = value; }
        }
        /// <summary>
        /// Sets the Drawing Graphic Unit for the Barcode
        /// </summary>
        public System.Drawing.Graphics Graphics { set { this._BarcodeRenterInfo.Graphics = value; } }

        public void initBarcode(string barcode, string discription, string itemcode, float FontSize)
        {
            this._BarcodeInfo.Barcode = barcode;
            this._BarcodeInfo.Description = this.DiscriptionSummary;
            Code128Content content = new Code128Content(this._BarcodeInfo.Barcode);
            int[] codes = content.Codes;
            this._BarcodeRenterInfo.Patterns = Code128Rendering.CreatePatterns(codes);
            foreach (int x in this._BarcodeRenterInfo.Patterns)
                Console.WriteLine(x.ToString());
            this._BarcodeRenterInfo.BarColor = Color.Black;
            this._BarcodeRenterInfo.ShowBarcodeText = true;
            this._BarcodeRenterInfo.ShowDescription = true;
            this._BarcodeRenterInfo.TextFont = new Font(SystemFonts.DefaultFont.FontFamily,
                FontSize, FontStyle.Regular);
            this._BarcodeRenterInfo.SetMarginAll(4);
            this._BarcodeRenterInfo.QuitZone = new System.Drawing.Printing.Margins(2, 2, 2, 2);
        }

        public void DrawBarcode(Graphics gr, Rectangle printArea)
        {
            this._BarcodeInfo.Description = this.DiscriptionSummary;
            gr.PageUnit = GraphicsUnit.Pixel;
            this._BarcodeRenterInfo.Graphics = gr;
            //this._BarcodeRenterInfo.PrintClip = printArea;
            this._BarcodeRenterInfo.BarcodeInfo = this._BarcodeInfo;
            int PrintingWidthAvilable = this._BarcodeRenterInfo.GetPrintableWidth();
            int sumPatterns = this._BarcodeRenterInfo.SumPatterns();
            this._BarcodeRenterInfo.BarWieight = (int)((float)PrintingWidthAvilable / (float)sumPatterns);
            if (this._BarcodeRenterInfo.BarWieight < 1)
                throw new Exception("Not enough width");
            // if width is zero, don't try to draw it
            //if (this._BarcodeRenterInfo.BarWieight > 0)
            //{
            this._BarcodeRenterInfo.DrawBarcode(gr, printArea);
            //}
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
