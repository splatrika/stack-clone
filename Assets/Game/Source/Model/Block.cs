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
    }
}
