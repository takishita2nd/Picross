using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class ObjectBase
    {
        protected int _x;
        protected int _y;
        protected asd.TextureObject2D _backTexture;
        protected asd.TextObject2D _valueText;
        protected int width;
        protected int height;
        private Action _action = null;

        public asd.TextureObject2D getBackTexture()
        {
            return _backTexture;
        }

        public asd.TextObject2D getTextObject()
        {
            return _valueText;
        }

        public bool IsClick(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + width
                && pos.Y > _y && pos.Y < _y + height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
