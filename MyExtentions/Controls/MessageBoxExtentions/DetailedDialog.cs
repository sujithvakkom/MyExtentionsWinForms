using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyExtentions.Controls.MessageBoxExtentions
{
    using System.Runtime.Serialization.Formatters;
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using System.Windows.Forms.ComponentModel;
    using System.Windows.Forms.Design;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Drawing;
    using Microsoft.Win32;
    using Message = System.Windows.Forms.Message;
    using System.Drawing.Drawing2D;

    /// <include file='doc\GridErrorDlg.uex' path='docs/doc[@for="GridErrorDlg"]/*' />
    /// <devdoc>
    ///     Implements a dialog that is displayed when an unhandled exception occurs in
    ///     a thread. This dialog's width is defined by the summary message
    ///     in the top pane. We don't restrict dialog width in any way.  
    ///     Use caution and check at all DPI scaling factors if adding a new message
    ///     to be displayed in the top pane.
    /// </devdoc>
    public partial class DetailedDialog : Form
    {
        private Bitmap expandImage = null;
        private Bitmap collapseImage = null;
        private PropertyGrid ownerGrid;

        public Panel DetailsPanel
        {
            get
            {
                return this.details;
            }
        }

        public string Details
        {
            set
            {
                if (details.Controls.Count == 0)
                {
                    details.Padding = new Padding(5);
                    this.textBoxDetails.Text = value;
                    this.details.Controls.Add(this.textBoxDetails);
                    this.textBoxDetails.Dock = DockStyle.Fill;
                    this.details.Refresh();
                }
                this.details.Text = value;
            }
        }


        public string Message
        {
            set
            {
                this.lblMessage.Text = value;
            }
        }

        [
            SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters") // We use " " for the text so we leave a small amount of test.
            // So we don't have to localize it.
        ]
        public DetailedDialog(PropertyGrid owner)
        {
            ownerGrid = owner;
            expandImage = new Bitmap(typeof(ThreadExceptionDialog), "down.bmp");
            expandImage.MakeTransparent();
            /*
            if (DpiHelper.IsScalingRequired)
            {
                DpiHelper.ScaleBitmapLogicalToDevice(ref expandImage);
            }
             * */
            collapseImage = new Bitmap(typeof(ThreadExceptionDialog), "up.bmp");
            collapseImage.MakeTransparent();
            /*
            if (DpiHelper.IsScalingRequired)
            {
                DpiHelper.ScaleBitmapLogicalToDevice(ref collapseImage);
            }
             * */

            if (IsRTLResources)
            {
                this.RightToLeft = RightToLeft.Yes;
            }
            InitializeComponent();
            // 
            // textBoxDetails
            // 
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.textBoxDetails.Location = new System.Drawing.Point(258, 220);
            this.textBoxDetails.Multiline = true;
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.ReadOnly = true;
            this.textBoxDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDetails.Size = new System.Drawing.Size(100, 20);
            this.textBoxDetails.TabIndex = 7;
            //this.Controls.Add(this.textBoxDetails);
            /*
            foreach (Control c in this.Controls)
            {
                if (c.SupportsUseCompatibleTextRendering)
                {
                    c.UseCompatibleTextRenderingInt = ownerGrid.UseCompatibleTextRendering;
                }
            }
             * */

            pictureBox.Image = SystemIcons.Warning.ToBitmap();
            //detailsBtn.Text = " " + SR.GetString(SR.ExDlgShowDetails);
            detailsBtn.Text = " " + "Details";

            //details.AccessibleName = SR.GetString(SR.ExDlgDetailsText);

            details.AccessibleName = "DetailsText";

            //okBtn.Text = SR.GetString(SR.ExDlgOk);

            okBtn.Text = "OK";
            //cancelBtn.Text = SR.GetString(SR.ExDlgCancel);
            cancelBtn.Text = "Cancel";
            detailsBtn.Image = expandImage;
        }

        /// <include file='doc\GridErrorDlg.uex' path='docs/doc[@for="GridErrorDlg.DetailsClick"]/*' />
        /// <devdoc>
        ///     Called when the details button is clicked.
        /// </devdoc>
        private void DetailsClick(object sender, EventArgs devent)
        {
            int delta = details.Height + 8;

            if (details.Visible)
            {
                detailsBtn.Image = expandImage;
                Height -= delta;
            }
            else
            {
                detailsBtn.Image = collapseImage;
                details.Width = overarchingTableLayoutPanel.Width - details.Margin.Horizontal;
                Height += delta;
            }

            details.Visible = !details.Visible;
        }

        /// <devdoc>
        ///     Tells whether the current resources for this dll have been
        ///     localized for a RTL language.
        /// </devdoc>
        private static bool IsRTLResources
        {
            get
            {
                return false;
                //return SR.GetString(SR.RTL) != "RTL_False";
            }
        }

        private void OnButtonClick(object s, EventArgs e)
        {
            DialogResult = ((Button)s).DialogResult;
            Close();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                // make sure the details button is sized properly
                //
                using (Graphics g = CreateGraphics())
                {
                    SizeF sizef = MeasureTextHelper.MeasureText(this.ownerGrid, g, detailsBtn.Text, detailsBtn.Font);

                    //SizeF sizef = PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, g, detailsBtn.Text, detailsBtn.Font);
                    int detailsWidth = (int)Math.Ceiling(sizef.Width);
                    detailsWidth += detailsBtn.Image.Width;
                    detailsBtn.Width = (int)Math.Ceiling(detailsWidth * (ownerGrid.UseCompatibleTextRendering ? 1.15f : 1.4f));
                    detailsBtn.Height = okBtn.Height;
                }

                // Update the location of the TextBox details
                int x = details.Location.X;
                int y = detailsBtn.Location.Y + detailsBtn.Height + detailsBtn.Margin.Bottom;

                // Location is relative to its parent,
                // therefore, we need to take its parent into consideration
                Control parent = detailsBtn.Parent;
                while (parent != null && !(parent is Form))
                {
                    y += parent.Location.Y;
                    parent = parent.Parent;
                }

                details.Location = new System.Drawing.Point(x, y);

                if (details.Visible)
                {
                    DetailsClick(details, EventArgs.Empty);
                }
            }
            okBtn.Focus();
        }
    }


}