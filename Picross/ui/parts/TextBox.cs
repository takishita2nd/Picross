using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class TextBox : ObjectBase
    {
        private string _text;
        private int fontOffsetX = 9;
        private int fontOffsetY = 4;
        protected bool enable = true;
        private Action _action = null;
        private bool _isShow = false;

        public TextBox(int x, int y, string text)
        {
            width = 128;
            height = 32;
            _x = x;
            _y = y;
            _text = text;

            _backTexture = new asd.TextureObject2D();
            _backTexture.Position = new asd.Vector2DF(_x, _y);

            _valueText = new asd.TextObject2D();
            _valueText.Text = _text;
            _valueText.Font = null;
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void SetFontOffset(int x, int y)
        {
            fontOffsetX = x;
            fontOffsetY = y;
            _valueText.Position = new asd.Vector2DF(_x + fontOffsetX, _y + fontOffsetY);
        }

        public void UpdateTexture(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + width
                && pos.Y > _y && pos.Y < _y + height)
            {
                _backTexture.Texture = Resource.getSelectedTextTexture();
            }
            else
            {
                _backTexture.Texture = Resource.getTextTexture();
            }
        }

        public void Show()
        {
            _isShow = true;
            _backTexture.Texture = Resource.getTextTexture();
            _valueText.Font = Resource.getFont();
        }

        public void Hide()
        {
            _isShow = false; ;
            _backTexture.Texture = null;
            _valueText.Font = null;
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void SetEnable(bool enable)
        {
            this.enable = enable;
        }

        public virtual void OnClick()
        {
            if (enable)
            {
                if (_action != null)
                {
                    _action.Invoke();
                }
            }
        }
    }
}
