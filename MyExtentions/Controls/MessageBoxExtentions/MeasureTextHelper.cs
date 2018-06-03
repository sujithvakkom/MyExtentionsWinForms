using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace  MyExtentions.Controls.MessageBoxExtentions
{
    internal class MeasureTextHelper
    {
        public static SizeF MeasureText(PropertyGrid owner, Graphics g, string text, Font font)
        {
            return MeasureTextSimple(owner, g, text, font, new SizeF(0, 0));
        }

        public static SizeF MeasureText(PropertyGrid owner, Graphics g, string text, Font font, int width)
        {
            return MeasureText(owner, g, text, font, new SizeF(width, 999999));
        }

        public static SizeF MeasureTextSimple(PropertyGrid owner, Graphics g, string text, Font font, SizeF size)
        {
            SizeF bindingSize;
            if (owner.UseCompatibleTextRendering)
            {
                bindingSize = g.MeasureString(text, font, size);
            }
            else
            {
                bindingSize = (SizeF)TextRenderer.MeasureText(g, text, font, Size.Ceiling(size), GetTextRendererFlags());
            }

            return bindingSize;
        }

        public static SizeF MeasureText(PropertyGrid owner, Graphics g, string text, Font font, SizeF size)
        {
            SizeF bindingSize;
            if (owner.UseCompatibleTextRendering)
            {
                bindingSize = g.MeasureString(text, font, size);
            }
            else
            {
                TextFormatFlags flags =
                    GetTextRendererFlags() |
                    TextFormatFlags.LeftAndRightPadding |
                    TextFormatFlags.WordBreak |
                    TextFormatFlags.NoFullWidthCharacterBreak;

                bindingSize = (SizeF)TextRenderer.MeasureText(g, text, font, Size.Ceiling(size), flags);
            }

            return bindingSize;
        }

        public static TextFormatFlags GetTextRendererFlags()
        {
            return TextFormatFlags.PreserveGraphicsClipping |
                    TextFormatFlags.PreserveGraphicsTranslateTransform;
        }
    }
}
