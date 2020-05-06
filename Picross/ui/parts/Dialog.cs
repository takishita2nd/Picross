using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class Dialog
    {
        private asd.TextureObject2D _texture;
        private bool _isShow;
        private Button _button = null;
        private int _x = 300;
        private int _y = 300;
        private int _width = 400;
        private int _height = 200;
        private Action _action = null;

        public Dialog()
        {
            _isShow = false;
            _button = new Button(436, 450, "確定");
            _button.SetFontOffset(46, 4);
            _button.SetAction(() =>
            {
                if(_action != null)
                {
                    _action.Invoke();
                }
            });

            _texture = new asd.TextureObject2D();
            _texture.Position = new asd.Vector2DF(_x, _y);
        }

        public void SetEngine()
        {
            asd.Engine.AddObject2D(_texture);
            asd.Engine.AddObject2D(_button.getBackTexture());
            asd.Engine.AddObject2D(_button.getTextObject());
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void Show()
        {
            _isShow = true;
            _texture.Texture = Resource.getDialogTexture();
            _button.Show();
        }

        public void Hide()
        {
            _isShow = false;
            _texture.Texture = null;
            _button.Hide();
        }

        public bool IsShow()
        {
            return _isShow;
        }

        public void UpdateTexture(asd.Vector2DF pos)
        {
            _button.UpdateTexture(pos);
        }

        public bool IsClick(asd.Vector2DF pos)
        {
            if (pos.X > _x && pos.X < _x + _width
                && pos.Y > _y && pos.Y < _y + _height)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void OnClick(asd.Vector2DF pos)
        {
            if (_button.isClick(pos))
            {
                _button.OnClick();
            }
        }
    }
}
