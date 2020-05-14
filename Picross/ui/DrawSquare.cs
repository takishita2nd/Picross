using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class DrawSquare : ObjectBase
    {
        protected int _row;
        protected int _col;
        protected const int setPositionX = 200;
        protected const int setPositionY = 200;

        public DrawSquare(int row, int col)
        {
            width = 32;
            height = 32;
            _row = row;
            _col = col;
            _x = col * width + setPositionX;
            _y = row * height + setPositionY;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Texture = Resource.GetPicrossTexture();
            _backTexture.Position = new asd.Vector2DF(_x, _y);
        }

        public void Paint()
        {
            _backTexture.Texture = Resource.GetPaintedPicrossTexture();
        }
    }
}
