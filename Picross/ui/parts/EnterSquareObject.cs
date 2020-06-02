using asd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class EnterSquareObject : ObjectBase
    {
        protected int _row;
        protected int _col;
        protected const int offsetX = 10;
        protected const int offsetY = 10;

        public EnterSquareObject(int row, int col)
        {
            width = 64;
            height = 64;
            _row = row;
            _col = col;
            _x = col * width + offsetX;
            _y = row * height + offsetY;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Position = new asd.Vector2DF(_x, _y);
        }

        public void SetPriority(int priority)
        {
            _backTexture.DrawingPriority = priority;
        }

        public void SetPosition(Vector2DF pos)
        {
            _x = _col * width + (int)pos.X;
            _y = _row * height + (int)pos.Y;

            _backTexture.Position = new asd.Vector2DF(_x, _y);
        }

        public void Show()
        {
        }

        public void Hide()
        {
            _backTexture.Texture = null;
        }

        public void UpdateTexture(Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + width
                && pos.Y > _y && pos.Y < _y + height)
            {
                _backTexture.Texture = Resource.getPaletteEnterSquareTexture();
            }
            else
            {
                _backTexture.Texture = null;
            }
        }
    }
}
