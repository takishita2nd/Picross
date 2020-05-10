using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class Label : ObjectBase
    {
        private string _text;
        private int fontOffsetX = 9;
        private int fontOffsetY = 4;
        private bool _isShow = false;

        public Label(int x, int y, string text)
        {
            width = 128;
            height = 32;
            _x = x;
            _y = y;
            _text = text;

            _valueText = new asd.TextObject2D();
            _valueText.Text = _text;
            _valueText.Font = null;
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void SetPriority(int priority)
        {
            _valueText.DrawingPriority = priority;
        }

        public void SetFontOffset(int x, int y)
        {
            fontOffsetX = x;
            fontOffsetY = y;
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void Show()
        {
            _isShow = true;
            _valueText.Font = Resource.getFont();
        }

        public void Hide()
        {
            _isShow = false; ;
            _valueText.Font = null;
        }
    }
}
