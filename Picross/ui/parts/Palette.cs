using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui.parts
{
    class Palette
    {
        public class CODE
        {
            public const string ZERO = "0";
            public const string BS = "BS";
            public const string CLR = "CLR";
        }

        private asd.Vector2DF palettePosition;
        private const int width = 192;
        private const int height = 256;
        private asd.TextureObject2D _texture;
        SquareObject[,] paletteSquareObjects = new SquareObject[3, 3];
        SquareObject paletteZeroSquareObject = new SquareObject(-1, 0, "0");
        SquareObject paletteBSSquareObject = new SquareObject(-1, 1, "←");
        SquareObject paletteCRSquareObject = new SquareObject(-1, 2, "Ｃ");
        private bool _isShow = false;

        public Palette()
        {
            _texture = new asd.TextureObject2D();
            int value = 1;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    paletteSquareObjects[row, col] = new SquareObject(row, col, value.ToString());
                    value++;
                }
            }
            paletteBSSquareObject.SetFontOffset(14, 9);
            paletteCRSquareObject.SetFontOffset(10, 9);
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
        }

        public void Show(asd.Vector2DF pos)
        {
            palettePosition = new asd.Vector2DF(pos.X, pos.Y - 64);
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
            _isShow = true;
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
            _texture.Texture = null;
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
        }

        public string GetClickValue(asd.Vector2DF pos)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (paletteSquareObjects[row, col].isClick(pos) == true)
                    {
                        return paletteSquareObjects[row, col].GetValue();
                    }
                }
            }
            if (paletteZeroSquareObject.isClick(pos))
            {
                return CODE.ZERO;
            }
            if (paletteBSSquareObject.isClick(pos))
            {
                return CODE.BS;
            }
            if (paletteCRSquareObject.isClick(pos))
            {
                return CODE.CLR;
            }

            return string.Empty;
        }
    }
}
