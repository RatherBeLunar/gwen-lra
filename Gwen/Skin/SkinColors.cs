using System;
using System.Drawing;

namespace Gwen.Skin
{
    /// <summary>
    /// UI colors used by skins.
    /// </summary>
    public struct SkinColors
    {
        public struct _Text
        {
            public Color Foreground;
            public Color Contrast;
            public Color ContrastLow;
            public Color Highlight;
            public Color Inactive;
            public Color Disabled;
        }

        public struct _Tree
        {
            public Color Lines;
        }

        public struct _Properties
        {           
            public Color Border;
        }

        public Color ModalBackground;
        public Color TooltipText;

        public _Tree Tree;
        public _Properties Properties;
        public _Text Text;
        public Color Accent;
        public Color AccentHigh;
        public Color AccentLow;
        public Color Background;
        public Color BackgroundHighlight;
        public Color Foreground;
        public Color ForegroundHighlight;
    }
}
