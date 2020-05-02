using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class PicrossUI
    {
        private const int setPositionX = 200;
        private const int setPositionY = 200;

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
                for(int col = 0; col <10; col++)
                {
                    var square = new asd.TextureObject2D();
                    square.Texture = asd.Engine.Graphics.CreateTexture2D("square.png");
                    square.Position = new asd.Vector2DF(row * 32 + setPositionX, col * 32 + setPositionY);
                    asd.Engine.AddObject2D(square);
                }
            }

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}
