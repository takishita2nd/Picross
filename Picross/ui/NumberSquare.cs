using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class NumberSquare : ObjectBase
    {
        protected int _row;
        protected int _col;
        protected const int setPositionX = 200;
        protected const int setPositionY = 200;
        protected const int fontOffsetX = 19;
        protected const int fontOffsetY = 9;
        private string _value;

        public NumberSquare(int row, int col)
        {
            width = 32;
            height = 32;
            _value = string.Empty;
            _row = row;
            _col = col;
            _x = col * width + setPositionX;
            _y = row * height + setPositionY;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Texture = Resource.GetPicrossTexture();
            _backTexture.Position = new asd.Vector2DF(_x, _y);

            _valueText = new asd.TextObject2D();
            _valueText.Font = Resource.getFont();
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void SetValue(string value)
        {
            _value = value;
            _valueText.Text = _value;
        }

        public void UpdateTexture(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + width
                && pos.Y > _y && pos.Y < _y + height)
            {
                _backTexture.Texture = Resource.GetSelectedNumberTexture();
            }
            else
            {
                _backTexture.Texture = Resource.GetPicrossTexture();
            }
        }

        public int getValue()
        {
            return int.Parse(_value);
        }
    }
}
