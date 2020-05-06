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

            List<Button> buttons = new List<Button>();
            var sizeButton = new Button(10, 10, "サイズ変更");
            sizeButton.Show();
            asd.Engine.AddObject2D(sizeButton.getBackTexture());
            asd.Engine.AddObject2D(sizeButton.getTextObject());
            buttons.Add(sizeButton);

            for (int row = 0; row <10; row++)
            {
                List<DrawSquare> rowList = new List<DrawSquare>();
                for(int col = 0; col <10; col++)
                {
                    var square = new DrawSquare(row, col);
                    asd.Engine.AddObject2D(square.getBackTexture());
                    rowList.Add(square);
                }
                drawSquares.Add(rowList);
            }

            Dialog dialog = new Dialog();
            dialog.SetEngine();
            dialog.SetAction(() =>
            {
                dialog.Hide();
            });
            sizeButton.SetAction(() => {
                dialog.Show();
            });

            while (asd.Engine.DoEvents())
            {
                asd.Vector2DF pos = asd.Engine.Mouse.Position;
                if(!dialog.IsShow())
                {
                    foreach (Button button in buttons)
                    {
                        button.UpdateTexture(pos);
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
                        foreach (Button button in buttons)
                        {
                            if (button.isClick(pos))
                            {
                                button.OnClick();
                            }
                        }
                    }
                    else
                    {
                        if (dialog.IsClick(pos))
                        {
                            dialog.OnClick(pos);
                        }
                    }
                }
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}
