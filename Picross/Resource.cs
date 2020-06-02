using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross
{
    class Resource
    {
        private static asd.Texture2D _picrossTexture = null;
        private static asd.Texture2D _paintedpicrossTexture = null;
        private static asd.Texture2D _maskedpicrossTexture = null;
        private static asd.Texture2D _selectedNumberTexture = null;
        private static asd.Texture2D _buttonTexture = null;
        private static asd.Texture2D _textTexture = null;
        private static asd.Texture2D _SelectedTextTexture = null;
        private static asd.Texture2D _dialogTexture = null;
        private static asd.Texture2D _paletteTexture = null;
        private static asd.Texture2D _paletteSquareTexture = null;
        private static asd.Texture2D _paletteEnterSquareTexture = null;
        private static asd.Font _font = null;
        private static asd.Font _fonRedt = null;
        private static asd.Font _fontPalette = null;

        public static asd.Texture2D GetPicrossTexture()
        {
            if (_picrossTexture == null)
            {
                _picrossTexture = asd.Engine.Graphics.CreateTexture2D("square.png");
            }
            return _picrossTexture;
        }

        public static asd.Texture2D GetPaintedPicrossTexture()
        {
            if (_paintedpicrossTexture == null)
            {
                _paintedpicrossTexture = asd.Engine.Graphics.CreateTexture2D("paintsquare.png");
            }
            return _paintedpicrossTexture;
        }

        public static asd.Texture2D GetMaskedPicrossTexture()
        {
            if (_maskedpicrossTexture == null)
            {
                _maskedpicrossTexture = asd.Engine.Graphics.CreateTexture2D("masksquare.png");
            }
            return _maskedpicrossTexture;
        }

        public static asd.Texture2D GetSelectedNumberTexture()
        {
            if (_selectedNumberTexture == null)
            {
                _selectedNumberTexture = asd.Engine.Graphics.CreateTexture2D("selectSquare.png");
            }
            return _selectedNumberTexture;
        }

        public static asd.Texture2D getButtonTexture()
        {
            if (_buttonTexture == null)
            {
                _buttonTexture = asd.Engine.Graphics.CreateTexture2D("button.png");
            }
            return _buttonTexture;
        }

        public static asd.Texture2D getTextTexture()
        {
            if (_textTexture == null)
            {
                _textTexture = asd.Engine.Graphics.CreateTexture2D("text.png");
            }
            return _textTexture;
        }

        public static asd.Texture2D getSelectedTextTexture()
        {
            if (_SelectedTextTexture == null)
            {
                _SelectedTextTexture = asd.Engine.Graphics.CreateTexture2D("text2.png");
            }
            return _SelectedTextTexture;
        }

        public static asd.Texture2D getDialogTexture()
        {
            if (_dialogTexture == null)
            {
                _dialogTexture = asd.Engine.Graphics.CreateTexture2D("dialog.png");
            }
            return _dialogTexture;
        }

        public static asd.Texture2D getPaletteTexture()
        {
            if (_paletteTexture == null)
            {
                _paletteTexture = asd.Engine.Graphics.CreateTexture2D("palette.png");
            }
            return _paletteTexture;
        }

        public static asd.Texture2D getPaletteSquareTexture()
        {
            if (_paletteSquareTexture == null)
            {
                _paletteSquareTexture = asd.Engine.Graphics.CreateTexture2D("paletteSquare.png");
            }
            return _paletteSquareTexture;
        }

        public static asd.Texture2D getPaletteEnterSquareTexture()
        {
            if (_paletteEnterSquareTexture == null)
            {
                _paletteEnterSquareTexture = asd.Engine.Graphics.CreateTexture2D("enter.png");
            }
            return _paletteEnterSquareTexture;
        }

        public static asd.Font getFont()
        {
            if (_font == null)
            {
                _font = asd.Engine.Graphics.CreateFont("font.aff");
            }
            return _font;
        }

        public static asd.Font getFontRed()
        {
            if (_fonRedt == null)
            {
                _fonRedt = asd.Engine.Graphics.CreateFont("fontRed.aff");
            }
            return _fonRedt;
        }

        public static asd.Font getPaletteFont()
        {
            if (_fontPalette == null)
            {
                _fontPalette = asd.Engine.Graphics.CreateFont("paletteFont.aff");
            }
            return _fontPalette;
        }
    }
}
