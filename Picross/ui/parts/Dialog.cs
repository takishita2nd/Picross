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
        private TextBox _rowText = null;
        private TextBox _colText = null;
        private Label _label1 = null;
        private Label _label2 = null;
        private Palette _palette = null;
        private int _x = 300;
        private int _y = 300;
        private int _width = 400;
        private int _height = 200;
        private Action _action = null;
        private string _rowValue = "10";
        private string _colValue = "10";
        private TextBox _selectedTextBox = null;
        private string _selectedValue = string.Empty;
        private const int priority = 10000;

        public Dialog()
        {
            _isShow = false;
            _palette = new Palette();
            _palette.Hide();

            // 確定ボタン
            _button = new Button(436, 400, "確定");
            _button.SetFontOffset(46, 4);
            _button.SetPriority(priority + 1);
            _button.SetAction(() =>
            {
                if(_action != null)
                {
                    _action.Invoke();
                }
            });
            // ラベル
            _label1 = new Label(320, 330, "↓");
            _label1.SetPriority(priority + 1);
            _label2 = new Label(490, 330, "→");
            _label2.SetPriority(priority + 1);
            // Row入力エリア
            _rowText = new TextBox(350, 330, _rowValue);
            _rowText.SetPriority(priority + 1);
            // Col入力エリア
            _colText = new TextBox(520, 330, _colValue);
            _colText.SetPriority(priority + 1);

            _texture = new asd.TextureObject2D();
            _texture.Position = new asd.Vector2DF(_x, _y);
            _texture.DrawingPriority = priority;
        }

        public void SetEngine()
        {
            asd.Engine.AddObject2D(_texture);
            asd.Engine.AddObject2D(_label1.getTextObject());
            asd.Engine.AddObject2D(_label2.getTextObject());
            asd.Engine.AddObject2D(_rowText.getBackTexture());
            asd.Engine.AddObject2D(_rowText.getTextObject());
            asd.Engine.AddObject2D(_colText.getBackTexture());
            asd.Engine.AddObject2D(_colText.getTextObject());
            asd.Engine.AddObject2D(_button.getBackTexture());
            asd.Engine.AddObject2D(_button.getTextObject());
            _palette.SetEngine();
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public int GetRowValue()
        {
            return int.Parse(_rowValue);
        }

        public int GetColValue()
        {
            return int.Parse(_colValue);
        }

        public void Show()
        {
            _isShow = true;
            _texture.Texture = Resource.getDialogTexture();
            _label1.Show();
            _label2.Show();
            _rowText.Show();
            _colText.Show();
            _button.Show();
        }

        public void Hide()
        {
            _isShow = false;
            _texture.Texture = null;
            _label1.Hide();
            _label2.Hide();
            _rowText.Hide();
            _colText.Hide();
            _button.Hide();
        }

        public bool IsShow()
        {
            return _isShow;
        }

        public void UpdateTexture(asd.Vector2DF pos)
        {
            if (_palette.IsShow())
            {
                _palette.UpdateTexture(pos);
            }
            else
            {
                _rowText.UpdateTexture(pos);
                _colText.UpdateTexture(pos);
                _button.UpdateTexture(pos);
            }
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
            if (_palette.IsShow() == false)
            {
                if (_rowText.IsClick(pos))
                {
                    _selectedTextBox = _rowText;
                    _selectedValue = _rowValue;
                    _palette.Show(pos, _rowValue);
                }
                if (_colText.IsClick(pos))
                {
                    _selectedTextBox = _colText;
                    _selectedValue = _colValue;
                    _palette.Show(pos, _colValue);
                }
                if (_button.IsClick(pos))
                {
                    _button.OnClick();
                }
            }
            else
            {
                if (_palette.IsClick(pos))
                {
                    _palette.OnClick(pos);
                    if (_palette.IsClickEnter())
                    {
                        _palette.Hide();
                        _selectedValue = _palette.GetValue();
                        _selectedTextBox.SetText(_selectedValue);
                        if (_selectedTextBox.Equals(_rowText))
                        {
                            _rowValue = _selectedValue;
                        }
                        else
                        {
                            _colValue = _selectedValue;
                        }
                    }
                }
                else
                {
                    _palette.Hide();
                    _selectedTextBox = null;
                    _selectedValue = string.Empty;
                }
            }
        }
    }
}
