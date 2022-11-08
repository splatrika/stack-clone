using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public struct Block
    {
        public Rect Rect { get; }
        public Color Color { get; }


        public Block(Rect rect, Color color)
        {
            Rect = rect;
            Color = color;
        }


        public Block Cut(Rect b)
        {
            var a = Rect;
            var cuttedRect = new Rect();
            cuttedRect.xMax = Mathf.Min(a.xMax, b.xMax);
            cuttedRect.xMin = Mathf.Max(a.xMin, b.xMin);
            cuttedRect.yMax = Mathf.Min(a.yMax, b.yMax);
            cuttedRect.yMin = Mathf.Max(a.yMin, b.yMin);
            return new Block(cuttedRect, Color);
        }


        public Block InverseCut(Rect cutted)
        {
            var result = new Rect();
            result.xMin = (Rect.xMax == cutted.xMax)
                ? Rect.xMin
                : cutted.xMax;
            result.xMax = (Rect.xMin == cutted.xMin)
                ? Rect.xMax
                : cutted.xMin;
            result.yMin = (Rect.yMax == cutted.yMax)
                ? Rect.yMin
                : cutted.yMax;
            result.yMax = (Rect.yMin == cutted.yMin)
                ? Rect.yMax
                : cutted.yMin;
            if (Rect == cutted)
            {
                result = new Rect(0, 0, 0, 0);
            }
            return new Block(result, Color);
        }
    }
}
