using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class SquareObject : ObjectBase
    {
        public enum FontColor
        {
            Black,
            Red
        }

        protected int _row;
        protected int _col;
        private string _value;
        protected const int offsetX = 10;
        protected const int offsetY = 10;
        protected int fontOffsetX = 19;
        protected int fontOffsetY = 9;

        public SquareObject(int row, int col, string val)
        {
            width = 64;
            height = 64;
            _row = row;
            _col = col;
            _value = val;
            _x = col * width + offsetX;
            _y = row * height + offsetY;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Position = new asd.Vector2DF(_x, _y);

            _valueText = new asd.TextObject2D();
            _valueText.Font = Resource.getPaletteFont();
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);

        }

        public void SetPosition(asd.Vector2DF pos)
        {
            _x = _col * width + (int)pos.X;
            _y = _row * height + (int)pos.Y;

            _backTexture.Position = new asd.Vector2DF(_x, _y);
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void SetFontOffset(int x, int y)
        {
            fontOffsetX = x;
            fontOffsetY = y;
        }

        public void Show()
        {
            _valueText.Text = _value;
        }

        public void Hide()
        {
            _backTexture.Texture = null;
            _valueText.Text = "";
        }

        public void UpdateTexture(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + width
                && pos.Y > _y && pos.Y < _y + height)
            {
                _backTexture.Texture = Resource.getPaletteSquareTexture();
            }
            else
            {
                _backTexture.Texture = null;
            }
        }

        public string GetValue()
        {
            return _value;
        }

        public void SetFontColor(FontColor color)
        {
            switch (color)
            {
                case FontColor.Black:
                    _valueText.Font = Resource.getPaletteFont();
                    break;
                case FontColor.Red:
                    _valueText.Font = Resource.getFontRed();
                    break;
                default:
                    break;
            }
        }
    }
}
