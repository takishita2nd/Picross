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
        private static asd.Texture2D _buttonTexture = null;
        private static asd.Texture2D _dialogTexture = null;
        private static asd.Font _font = null;

        public static asd.Texture2D GetPicrossTexture()
        {
            if (_picrossTexture == null)
            {
                _picrossTexture = asd.Engine.Graphics.CreateTexture2D("square.png");
            }
            return _picrossTexture;
        }

        public static asd.Texture2D getButtonTexture()
        {
            if (_buttonTexture == null)
            {
                _buttonTexture = asd.Engine.Graphics.CreateTexture2D("button.png");
            }
            return _buttonTexture;
        }

        public static asd.Texture2D getDialogTexture()
        {
            if (_dialogTexture == null)
            {
                _dialogTexture = asd.Engine.Graphics.CreateTexture2D("dialog.png");
            }
            return _dialogTexture;
        }

        public static asd.Font getFont()
        {
            if (_font == null)
            {
                _font = asd.Engine.Graphics.CreateFont("font.aff");
            }
            return _font;
        }
    }
}
