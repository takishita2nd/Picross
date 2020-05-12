using Picross.ui.parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class PicrossUI
    {
        private List<List<DrawSquare>> drawSquares = new List<List<DrawSquare>>();
        private List<List<NumberSquare>> rowNumberSquare = new List<List<NumberSquare>>();
        private List<List<NumberSquare>> colNumberSquare = new List<List<NumberSquare>>();
        private NumberSquare selectedNumberSquare = null;
        private int selectedRowIndex;
        private int selectedColIndex;

        public PicrossUI()
        {
        }

        public void Run()
        {
            asd.Engine.Initialize("ピクロス解析ツール", 1000, 800, new asd.EngineOption());

            // 下地
            var background = new asd.GeometryObject2D();
            asd.Engine.AddObject2D(background);
            var bgRect = new asd.RectangleShape();
            bgRect.DrawingArea = new asd.RectF(0, 0, 1000, 800);
            background.Shape = bgRect;

            // ボタン
            List<Button> buttons = new List<Button>();
            var sizeButton = new Button(10, 10, "サイズ変更");
            sizeButton.Show();
            asd.Engine.AddObject2D(sizeButton.getBackTexture());
            asd.Engine.AddObject2D(sizeButton.getTextObject());
            buttons.Add(sizeButton);

            // 数字の入力するマス
            for (int row = 0; row < 10; row++)
            {
                NumberSquare square = new NumberSquare(row, -1);
                asd.Engine.AddObject2D(square.getBackTexture());
                asd.Engine.AddObject2D(square.getTextObject());
                List<NumberSquare> rowList = new List<NumberSquare>();
                rowList.Add(square);
                rowNumberSquare.Add(rowList);
            }
            for (int col = 0; col < 10; col++)
            {
                NumberSquare square = new NumberSquare(-1, col);
                asd.Engine.AddObject2D(square.getBackTexture());
                asd.Engine.AddObject2D(square.getTextObject());
                List<NumberSquare> colList = new List<NumberSquare>();
                colList.Add(square);
                colNumberSquare.Add(colList);
            }

            // 色を塗るマス
            for (int row = 0; row < 10; row++)
            {
                List<DrawSquare> rowList = new List<DrawSquare>();
                for (int col = 0; col < 10; col++)
                {
                    var square = new DrawSquare(row, col);
                    asd.Engine.AddObject2D(square.getBackTexture());
                    rowList.Add(square);
                }
                drawSquares.Add(rowList);
            }

            // サイズ変更ダイアログ
            Dialog dialog = new Dialog();
            dialog.SetEngine();
            dialog.SetAction(() =>
            {
                dialog.Hide();
                int row = dialog.GetRowValue();
                int col = dialog.GetColValue();
                while (row != drawSquares.Count)
                {
                    if (drawSquares.Count > row)
                    {
                        deleteRow();
                        deleteRowNumber();
                    }
                    else
                    {
                        addRow();
                        addRowNumber();
                    }
                }
                while (col != drawSquares[0].Count)
                {
                    if (drawSquares[0].Count > col)
                    {
                        deleteCol();
                        deleteColNumber();
                    }
                    else
                    {
                        addCol();
                        addColNumber();
                    }
                }
            });

            // パレット
            Palette palette = new Palette();
            palette.SetEngine();
            palette.Hide();

            // サイズ変更ボタン
            sizeButton.SetAction(() =>
            {
                dialog.Show();
            });

            while (asd.Engine.DoEvents())
            {
                asd.Vector2DF pos = asd.Engine.Mouse.Position;
                if (!dialog.IsShow())
                {
                    if (!palette.IsShow())
                    {
                        foreach (var rowList in rowNumberSquare)
                        {
                            foreach (var s in rowList)
                            {
                                s.UpdateTexture(pos);
                            }
                        }
                        foreach (var colList in colNumberSquare)
                        {
                            foreach (var s in colList)
                            {
                                s.UpdateTexture(pos);
                            }
                        }
                        foreach (Button button in buttons)
                        {
                            button.UpdateTexture(pos);
                        }
                    }
                    else
                    {
                        palette.UpdateTexture(pos);
                    }
                }
                else
                {
                    dialog.UpdateTexture(pos);
                }

                if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
                {
                    if (!dialog.IsShow())
                    {
                        if (!palette.IsShow())
                        {
                            int rowIndex;
                            int colIndex;
                            rowIndex = 0;
                            foreach (var rowList in rowNumberSquare)
                            {
                                colIndex = -1;
                                foreach (var s in rowList)
                                {
                                    if (s.IsClick(pos))
                                    {
                                        selectedNumberSquare = s;
                                        selectedRowIndex = rowIndex;
                                        selectedColIndex = colIndex;
                                        palette.Show(pos);
                                    }
                                    colIndex--;
                                }
                                rowIndex++;
                            }
                            colIndex = 0;
                            foreach (var colList in colNumberSquare)
                            {
                                rowIndex = -1;
                                foreach (var s in colList)
                                {
                                    if (s.IsClick(pos))
                                    {
                                        selectedNumberSquare = s;
                                        selectedRowIndex = rowIndex;
                                        selectedColIndex = colIndex;
                                        palette.Show(pos);
                                    }
                                    rowIndex--;
                                }
                                colIndex++;
                            }
                            foreach (Button button in buttons)
                            {
                                if (button.IsClick(pos))
                                {
                                    button.OnClick();
                                }
                            }
                        }
                        else
                        {
                            if (palette.IsClick(pos))
                            {
                                string v = palette.GetClickValue(pos);
                                updateTextValue(selectedNumberSquare, v);
                            }
                            else
                            {
                                palette.Hide();
                                if(selectedNumberSquare != null)
                                {
                                    if(selectedNumberSquare.GetStringValue() != string.Empty)
                                    {
                                        if (selectedRowIndex >= 0)
                                        {
                                            var square = new NumberSquare(selectedRowIndex, selectedColIndex - 1);
                                            asd.Engine.AddObject2D(square.getBackTexture());
                                            asd.Engine.AddObject2D(square.getTextObject());
                                            rowNumberSquare[selectedRowIndex].Add(square);
                                        }
                                        else if (selectedColIndex >= 0)
                                        {
                                            var square = new NumberSquare(selectedRowIndex - 1, selectedColIndex);
                                            asd.Engine.AddObject2D(square.getBackTexture());
                                            asd.Engine.AddObject2D(square.getTextObject());
                                            colNumberSquare[selectedColIndex].Add(square);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        dialog.OnClick(pos);
                    }
                }
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }

        private void deleteRow()
        {
            // マスの削除
            var rowList = drawSquares[drawSquares.Count - 1];
            foreach (var c in rowList)
            {
                asd.Engine.RemoveObject2D(c.getBackTexture());
            }
            drawSquares.RemoveAt(drawSquares.Count - 1);
        }

        private void addRow()
        {
            List<DrawSquare> rowList = new List<DrawSquare>();
            for (int c = 0; c < drawSquares[0].Count; c++)
            {
                var square = new DrawSquare(drawSquares.Count, c);
                asd.Engine.AddObject2D(square.getBackTexture());
                rowList.Add(square);
            }
            drawSquares.Add(rowList);
        }

        private void deleteCol()
        {
            // マスの削除
            foreach (var r in drawSquares)
            {
                asd.Engine.RemoveObject2D(r[r.Count - 1].getBackTexture());
                r.RemoveAt(r.Count - 1);
            }
        }

        private void addCol()
        {
            // マスの追加
            int rowindex = 0;
            int colindex = drawSquares[0].Count;
            foreach (var r in drawSquares)
            {
                var square = new DrawSquare(rowindex, colindex);
                asd.Engine.AddObject2D(square.getBackTexture());
                r.Add(square);
                rowindex++;
            }
        }

        private void deleteRowNumber()
        {
            // 数字のマスの削除
            var rowList = rowNumberSquare[rowNumberSquare.Count - 1];
            foreach (var s in rowList)
            {
                asd.Engine.RemoveObject2D(s.getBackTexture());
                asd.Engine.RemoveObject2D(s.getTextObject());
            }
            rowNumberSquare.RemoveAt(rowNumberSquare.Count - 1);
        }

        private void addRowNumber()
        {
            // 数字のマス追加
            var square = new NumberSquare(rowNumberSquare.Count, -1);
            asd.Engine.AddObject2D(square.getBackTexture());
            asd.Engine.AddObject2D(square.getTextObject());
            List<NumberSquare> list = new List<NumberSquare>();
            list.Add(square);
            rowNumberSquare.Add(list);
        }

        private void deleteColNumber()
        {
            // 数字のマスの削除
            var colList = colNumberSquare[colNumberSquare.Count - 1];
            foreach (var s in colList)
            {
                asd.Engine.RemoveObject2D(s.getBackTexture());
                asd.Engine.RemoveObject2D(s.getTextObject());
            }
            colNumberSquare.RemoveAt(colNumberSquare.Count - 1);
        }

        private void addColNumber()
        {
            // 数字のマス追加
            var square = new NumberSquare(-1, colNumberSquare.Count);
            asd.Engine.AddObject2D(square.getBackTexture());
            asd.Engine.AddObject2D(square.getTextObject());
            List<NumberSquare> list = new List<NumberSquare>();
            list.Add(square);
            colNumberSquare.Add(list);
        }

        private string updateTextValue(NumberSquare square, string v)
        {
            string value = square.GetStringValue();
            string text = string.Empty;
            switch (v)
            {
                case Palette.CODE.ZERO:
                    if (value.Length == 0)
                    {
                        text = string.Empty;
                    }
                    else if (value.Length < 2)
                    {
                        text = value + v;

                    }
                    else
                    {
                        text = value;
                    }
                    break;
                case Palette.CODE.BS:
                    if (value.Length != 0)
                    {
                        text = value.Remove(value.Length - 1);
                    }
                    break;
                case Palette.CODE.CLR:
                    text = string.Empty;
                    break;
                default:
                    if (value.Length < 2)
                    {
                        text = value + v;
                    }
                    else
                    {
                        text = value;
                    }
                    break;
            }
            square.SetValue(text);
            return text;

        }
    }
}

