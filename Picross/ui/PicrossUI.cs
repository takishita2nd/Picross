﻿using System;
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

            for(int row = 0; row <10; row++)
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

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}
