using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class Palette
    {
        private asd.Vector2DF palettePosition;
        private const int width = 192;
        private const int height = 318;
        private asd.TextureObject2D _texture;
        private SquareObject[,] paletteSquareObjects = new SquareObject[3, 3];
        private SquareObject paletteZeroSquareObject = new SquareObject(-1, 0, "0");
        private SquareObject paletteBSSquareObject = new SquareObject(-1, 1, "←");
        private SquareObject paletteCRSquareObject = new SquareObject(-1, 2, "Ｃ");
        private EnterSquareObject paletteENSquareObject = new EnterSquareObject(-2, 2);
        private asd.TextObject2D _valueText;

        private bool _isShow = false;
        private const int priority = 11000;
        protected int fontOffsetX = 19;
        protected int fontOffsetY = 9;
        private string value;
        private bool _isClickEnter;

        public Palette()
        {
            _texture = new asd.TextureObject2D();
            _texture.DrawingPriority = priority;
            int value = 1;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col] = new SquareObject(row, col, value.ToString());
                    paletteSquareObjects[row, col].SetPriority(priority + 1);
                    value++;
                }
            }
            paletteBSSquareObject.SetFontOffset(14, 9);
            paletteBSSquareObject.SetPriority(priority + 1);
            paletteCRSquareObject.SetFontOffset(10, 9);
            paletteCRSquareObject.SetPriority(priority + 1);
            paletteZeroSquareObject.SetPriority(priority + 1);
            paletteENSquareObject.SetPriority(priority + 1);
            _valueText = new asd.TextObject2D();
            _valueText.Font = Resource.getPaletteFont();
            _valueText.DrawingPriority = priority + 1;
        }

        public void SetEngine()
        {
            asd.Engine.AddObject2D(_texture);
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    asd.Engine.AddObject2D(paletteSquareObjects[row, col].getBackTexture());
                    asd.Engine.AddObject2D(paletteSquareObjects[row, col].getTextObject());
                }
            }
            asd.Engine.AddObject2D(paletteZeroSquareObject.getBackTexture());
            asd.Engine.AddObject2D(paletteZeroSquareObject.getTextObject());
            asd.Engine.AddObject2D(paletteBSSquareObject.getBackTexture());
            asd.Engine.AddObject2D(paletteBSSquareObject.getTextObject());
            asd.Engine.AddObject2D(paletteCRSquareObject.getBackTexture());
            asd.Engine.AddObject2D(paletteCRSquareObject.getTextObject());
            asd.Engine.AddObject2D(paletteENSquareObject.getBackTexture());
            asd.Engine.AddObject2D(_valueText);
        }

        public void Show(asd.Vector2DF pos, string s)
        {
            value = s;
            palettePosition = new asd.Vector2DF(pos.X, pos.Y - 128);
            _texture.Position = palettePosition;
            _texture.Texture = Resource.getPaletteTexture();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col].SetPosition(pos);
                    paletteSquareObjects[row, col].Show();
                }
            }
            paletteZeroSquareObject.SetPosition(pos);
            paletteZeroSquareObject.Show();
            paletteBSSquareObject.SetPosition(pos);
            paletteBSSquareObject.Show();
            paletteCRSquareObject.SetPosition(pos);
            paletteCRSquareObject.Show();
            paletteENSquareObject.SetPosition(pos);
            paletteENSquareObject.Show();

            int x = (int)pos.X;
            int y = -2 * 64 + (int)pos.Y;
            _valueText.Position = new asd.Vector2DF(x + fontOffsetX, y + fontOffsetY);
            _valueText.Text = value;
            _isShow = true;
            _isClickEnter = false;
        }

        public void Hide()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col].Hide();
                }
            }
            paletteZeroSquareObject.Hide();
            paletteBSSquareObject.Hide();
            paletteCRSquareObject.Hide();
            paletteENSquareObject.Hide();
            _texture.Texture = null;
            _valueText.Text = "";
            _isShow = false;
        }

        public bool IsShow()
        {
            return _isShow;
        }

        public bool IsClick(asd.Vector2DF pos)
        {
            if (pos.X > palettePosition.X && pos.X < palettePosition.X + width
                && pos.Y > palettePosition.Y && pos.Y < palettePosition.Y + height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateTexture(asd.Vector2DF pos)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col].UpdateTexture(pos);
                }
            }
            paletteZeroSquareObject.UpdateTexture(pos);
            paletteBSSquareObject.UpdateTexture(pos);
            paletteCRSquareObject.UpdateTexture(pos);
            paletteENSquareObject.UpdateTexture(pos);
        }

        public void OnClick(asd.Vector2DF pos)
        {
            if (paletteZeroSquareObject.IsClick(pos))
            {
                if(_valueText.Text != "" && _valueText.Text != "0")
                {
                    if (_valueText.Text.Length < 2)
                    {
                        _valueText.Text += "0";
                    }
                }
            }
            else if (paletteBSSquareObject.IsClick(pos))
            {
                if(_valueText.Text.Length > 0)
                {
                    _valueText.Text = _valueText.Text.Remove(_valueText.Text.Length - 1);
                }
            }
            else if (paletteCRSquareObject.IsClick(pos))
            {
                _valueText.Text = "";
            }
            else if (paletteENSquareObject.IsClick(pos))
            {
                value = _valueText.Text;
                _isClickEnter = true;
            }
            else
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (paletteSquareObjects[row, col].IsClick(pos) == true)
                        {
                            if(_valueText.Text.Length < 2)
                            {
                                if(_valueText.Text == "0")
                                {
                                    _valueText.Text = paletteSquareObjects[row, col].GetValue();
                                }
                                else
                                {
                                    _valueText.Text += paletteSquareObjects[row, col].GetValue();
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool IsClickEnter()
        {
            return _isClickEnter;
        }

        public string GetValue()
        {
            return value;
        }
    }
}
