using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace MyExtentions.Components.BarcodeSymbology.Code128
{
    #region Explanation

    /*
       * Here it is the ZLP 2844 Ptinter Optiomization
       * It prints 203DPI rounded but calculated is 203.2
       * So I think Better Image size for 3:2 aspect ratio will be 
       * 3048*2032 (Label Size)
       * barcode  should be with a quit zone 10 pt from both sides
       *    0       100                 2928    3028
       * ___________________________________________________________________________ X
       *    |---------------3048pt---------------*        |   
       *    |-100pt-|-------2828pt-------|-100pt-*       100pt  Margin
       * 100|       *                    *       *--------|------------------------|
       *    |       |                    |       *       600pt  Discription
       * 700|       *                    *       *        |------------------------|
       *    |       |                    |       *       1000pt Barcode
       *1700|       *                    *       *        |------------------------|
       *    |       |                    |       *       232pt  Bartext
       *1932|       *                    *       *--------|------------------------|
       *    |       |                    |       *       100pt  Margin
       *2032|       |                    |       *        |   
       * ---------------------------------------------------------------------------
       *    |
       *    Y
       * The Discription should be at   Rectangle(100,100,2928,700)
       * ie  
       * Rectangle
       * (XMargin,
       * Ymargin,
       * (Width-XMargin),
       * (Ymargin+Discription.Height))
       * 
       * there for the bars should be printed in    Rectangle(100,700,2928,1700)
       * ie  
       * Rectangle(XMargin,
       * (Ymargin+Discription.Height),
       * (Width-XMargin),
       * (Ymargin+Discription.Height+Barcode.Height))
       * 
       * The Bartext should be at   Rectangle(100,1700,2928,1932)
       * ie  
       * Rectangle(XMargin,
       * (Ymargin+Discription.Height+Barcode.Height),
       * (Width-XMargin),
       * (Ymargin+Discription.Height+Barcode.Height+Bartext.Height)))
          */
    #endregion
    public class Code128Rendering
    {
        private String _Itemcode;
        private String _Discription;
        private String _Barcode;
        private String _DiscriptionSummary;
        private Image _BarcodeImage;
        private int _BarWeight;
        private int _Width;
        private int _Height;
        private int _QuitZone;
        private Point _DiscriptionPoint;
        private Size _DiscriptionSize;
        private Point _BarcodeImagePoint;
        private Size _BarSize;
        private Point _BarcodeTextPoint;
        private Size _BarcodeTextSize;
        private StringFormat format;
        private Font font;
        private SolidBrush blackBrush;
        private Rectangle temp;
        private bool _DisplayItemcode;
        private bool _DisplayDiscription;

        #region Properties
        public bool DisplayItemcode
        {
            get
            {
                return _DisplayItemcode;
            }
            set
            {
                _DisplayItemcode = value;
                MakeBarcodeImage();
            }
        }

        public bool DisplayDiscription
        {
            get
            {
                return _DisplayDiscription;
            }
            set
            {
                _DisplayDiscription = value;
                MakeBarcodeImage();
            }
        }
        public String Barcode
        {
            set
            {
                _Barcode = value;
            }
        }
        public String DiscriptionSummary
        {
            set
            {
                _DiscriptionSummary = value;
            }
            get
            {
                _DiscriptionSummary = (_DisplayItemcode ? _Itemcode : "") + (_DisplayDiscription ? "\n" + _Discription : "");
                return _DiscriptionSummary;
            }
        }
        public int BarWeight
        {
            set
            {
            }
        }
        public int Width
        {
            set
            {
            }
        }
        public int Height
        {
            set
            {
            }
        }
        public int QuitZone
        {
            set
            {
            }
        }
        #endregion

        //Constructure
        public Code128Rendering(String barcode, String discription, String itemCode)
        {
            this._DisplayDiscription = true;
            this._DisplayItemcode = true;
            Barcode = barcode;
            _Discription = discription;
            _Itemcode = itemCode;
            generatePoints(3048, 2032, 100, 100, 600, 1000, 232);
        }

        public void generatePoints(int width,
            int height,
            int xMargin,
            int yMargin,
            int discriptionHeight,
            int barcodeHeight,
            int barcodeTextHeight)
        {
            _DiscriptionPoint = new Point(xMargin, yMargin);
            _BarcodeImagePoint = new Point(xMargin, (yMargin + discriptionHeight));
            _BarcodeTextPoint = new Point(xMargin, (yMargin + discriptionHeight + barcodeHeight));
            _DiscriptionSize = new Size(width - (xMargin + yMargin), discriptionHeight);
            _BarSize = new Size(width - (xMargin + yMargin), barcodeHeight);
            _BarcodeTextSize = new Size(width - (xMargin + yMargin), barcodeTextHeight);
            _Width = width;
            _Height = height;
        }

        #region Code patterns

        // in principle these rows should each have 6 elements
        // however, the last one -- STOP -- has 7. The cost of the
        // extra integers is trivial, and this lets the code flow
        // much more elegantly
        private static readonly int[,] cPatterns = {
                {2,1,2,2,2,2,0,0},  // 0
                {2,2,2,1,2,2,0,0},  // 1
                {2,2,2,2,2,1,0,0},  // 2
                {1,2,1,2,2,3,0,0},  // 3
                {1,2,1,3,2,2,0,0},  // 4
                {1,3,1,2,2,2,0,0},  // 5
                {1,2,2,2,1,3,0,0},  // 6
                {1,2,2,3,1,2,0,0},  // 7
                {1,3,2,2,1,2,0,0},  // 8
                {2,2,1,2,1,3,0,0},  // 9
                {2,2,1,3,1,2,0,0},  // 10
                {2,3,1,2,1,2,0,0},  // 11
                {1,1,2,2,3,2,0,0},  // 12
                {1,2,2,1,3,2,0,0},  // 13
                {1,2,2,2,3,1,0,0},  // 14
                {1,1,3,2,2,2,0,0},  // 15
                {1,2,3,1,2,2,0,0},  // 16
                {1,2,3,2,2,1,0,0},  // 17
                {2,2,3,2,1,1,0,0},  // 18
                {2,2,1,1,3,2,0,0},  // 19
                {2,2,1,2,3,1,0,0},  // 20
                {2,1,3,2,1,2,0,0},  // 21
                {2,2,3,1,1,2,0,0},  // 22
                {3,1,2,1,3,1,0,0},  // 23
                {3,1,1,2,2,2,0,0},  // 24
                {3,2,1,1,2,2,0,0},  // 25
                {3,2,1,2,2,1,0,0},  // 26
                {3,1,2,2,1,2,0,0},  // 27
                {3,2,2,1,1,2,0,0},  // 28
                {3,2,2,2,1,1,0,0},  // 29
                {2,1,2,1,2,3,0,0},  // 30
                {2,1,2,3,2,1,0,0},  // 31
                {2,3,2,1,2,1,0,0},  // 32
                {1,1,1,3,2,3,0,0},  // 33
                {1,3,1,1,2,3,0,0},  // 34
                {1,3,1,3,2,1,0,0},  // 35
                {1,1,2,3,1,3,0,0},  // 36
                {1,3,2,1,1,3,0,0},  // 37
                {1,3,2,3,1,1,0,0},  // 38
                {2,1,1,3,1,3,0,0},  // 39
                {2,3,1,1,1,3,0,0},  // 40
                {2,3,1,3,1,1,0,0},  // 41
                {1,1,2,1,3,3,0,0},  // 42
                {1,1,2,3,3,1,0,0},  // 43
                {1,3,2,1,3,1,0,0},  // 44
                {1,1,3,1,2,3,0,0},  // 45
                {1,1,3,3,2,1,0,0},  // 46
                {1,3,3,1,2,1,0,0},  // 47
                {3,1,3,1,2,1,0,0},  // 48
                {2,1,1,3,3,1,0,0},  // 49
                {2,3,1,1,3,1,0,0},  // 50
                {2,1,3,1,1,3,0,0},  // 51
                {2,1,3,3,1,1,0,0},  // 52
                {2,1,3,1,3,1,0,0},  // 53
                {3,1,1,1,2,3,0,0},  // 54
                {3,1,1,3,2,1,0,0},  // 55
                {3,3,1,1,2,1,0,0},  // 56
                {3,1,2,1,1,3,0,0},  // 57
                {3,1,2,3,1,1,0,0},  // 58
                {3,3,2,1,1,1,0,0},  // 59
                {3,1,4,1,1,1,0,0},  // 60
                {2,2,1,4,1,1,0,0},  // 61
                {4,3,1,1,1,1,0,0},  // 62
                {1,1,1,2,2,4,0,0},  // 63
                {1,1,1,4,2,2,0,0},  // 64
                {1,2,1,1,2,4,0,0},  // 65
                {1,2,1,4,2,1,0,0},  // 66
                {1,4,1,1,2,2,0,0},  // 67
                {1,4,1,2,2,1,0,0},  // 68
                {1,1,2,2,1,4,0,0},  // 69
                {1,1,2,4,1,2,0,0},  // 70
                {1,2,2,1,1,4,0,0},  // 71
                {1,2,2,4,1,1,0,0},  // 72
                {1,4,2,1,1,2,0,0},  // 73
                {1,4,2,2,1,1,0,0},  // 74
                {2,4,1,2,1,1,0,0},  // 75
                {2,2,1,1,1,4,0,0},  // 76
                {4,1,3,1,1,1,0,0},  // 77
                {2,4,1,1,1,2,0,0},  // 78
                {1,3,4,1,1,1,0,0},  // 79
                {1,1,1,2,4,2,0,0},  // 80
                {1,2,1,1,4,2,0,0},  // 81
                {1,2,1,2,4,1,0,0},  // 82
                {1,1,4,2,1,2,0,0},  // 83
                {1,2,4,1,1,2,0,0},  // 84
                {1,2,4,2,1,1,0,0},  // 85
                {4,1,1,2,1,2,0,0},  // 86
                {4,2,1,1,1,2,0,0},  // 87
                {4,2,1,2,1,1,0,0},  // 88
                {2,1,2,1,4,1,0,0},  // 89
                {2,1,4,1,2,1,0,0},  // 90
                {4,1,2,1,2,1,0,0},  // 91
                {1,1,1,1,4,3,0,0},  // 92
                {1,1,1,3,4,1,0,0},  // 93
                {1,3,1,1,4,1,0,0},  // 94
                {1,1,4,1,1,3,0,0},  // 95
                {1,1,4,3,1,1,0,0},  // 96
                {4,1,1,1,1,3,0,0},  // 97
                {4,1,1,3,1,1,0,0},  // 98
                {1,1,3,1,4,1,0,0},  // 99
                {1,1,4,1,3,1,0,0},  // 100
                {3,1,1,1,4,1,0,0},  // 101
                {4,1,1,1,3,1,0,0},  // 102
                {2,1,1,4,1,2,0,0},  // 103
                {2,1,1,2,1,4,0,0},  // 104
                {2,1,1,2,3,2,0,0},  // 105
                {2,3,3,1,1,1,2,0}   // 106
            };

        #endregion Code patterns

        private const int _QuietWidth = 10;

        /// <summary>
        /// Make an image of a Code128 barcode for a given string
        /// </summary>
        /// <param name="InputData">Message to be encoded</param>
        /// <param name="BarWeight">Base thickness for bar width (1 or 2 works well)</param>
        /// <param name="AddQuietZone">Add required horiz margins (use if output is tight)</param>
        /// <returns>An Image of the Code128 barcode representing the message</returns>
        /// 
        public void MakeBarcodeImage()
        {
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            font = new Font("arial", (int)(_BarcodeTextSize.Height * 0.75));
            blackBrush = new SolidBrush(System.Drawing.Color.Black);
            // get the Code128 codes to represent the message
            Code128Content content = new Code128Content(this._Barcode);
            int[] codes = content.Codes;
            _BarWeight = _BarSize.Width / ((codes.Length - 3) * 11 + 35);

            // get surface to draw on
            Image myimg = new System.Drawing.Bitmap(_Width, _Height);
            using (Graphics gr = Graphics.FromImage(myimg))
            {
                // set to white so we don't have to fill the spaces with white
                gr.FillRectangle(System.Drawing.Brushes.White, 0, 0, _Width, _Height);
                gr.DrawString(DiscriptionSummary, font, blackBrush, new Rectangle(_DiscriptionPoint, _DiscriptionSize), format);
                for (int codeidx = 0; codeidx < codes.Length; codeidx++)
                {
                    int code = codes[codeidx];
                    Console.WriteLine(code.ToString());
                    // take the bars two at a time: a black and a white
                    for (int bar = 0; bar < 8; bar += 2)
                    {
                        int barwidth = cPatterns[code, bar] * _BarWeight;
                        int spcwidth = cPatterns[code, bar + 1] * _BarWeight;

                        // if width is zero, don't try to draw it
                        if (barwidth > 0)
                        {
                            _BarSize.Width = barwidth;
                            temp = new Rectangle(_BarcodeImagePoint, _BarSize);
                            gr.FillRectangle(System.Drawing.Brushes.Black, temp);
                        }
                        // note that we never need to draw the space, since we 
                        // initialized the graphics to all white

                        // advance cursor beyond this pair
                        _BarcodeImagePoint.X += (barwidth + spcwidth);
                    }
                }
                temp = new Rectangle(_BarcodeTextPoint, _BarcodeTextSize);
                gr.DrawString(this._Barcode, font, blackBrush, temp, format);
            }
            _BarcodeImage = myimg;
        }
        public static explicit operator Image(Code128Rendering Obj)
        {
            Obj.generatePoints(3048, 2032, 100, 100, 600, 1000, 232);
            Obj.MakeBarcodeImage();
            return (Obj._BarcodeImage);
        }

        internal static int[] CreatePatterns(int[] codes)
        {
            ArrayList Patterns = new ArrayList();
            const int _BarWeight = 1;
            for (int codeidx = 0; codeidx < codes.Length; codeidx++)
            {
                int code = codes[codeidx];
                // take the bars two at a time: a black and a white
                for (int bar = 0; bar < 8; bar += 2)
                {
                    int barwidth = cPatterns[code, bar] * _BarWeight;
                    int spcwidth = cPatterns[code, bar + 1] * _BarWeight;
                    Patterns.Add(barwidth);
                    Patterns.Add(spcwidth);
                }
            }
            return Patterns.ToArray(typeof(int)) as int[];
        }
    }
}
