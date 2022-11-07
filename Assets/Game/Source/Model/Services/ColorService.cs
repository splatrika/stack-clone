using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class ColorService : IColorService
    {
        public Color[] Colors { get; }
        public float Speed { get; }
        public float Time { get; private set; }


        public ColorService(ColorServiceConfiguration configuration)
        {
            Colors = configuration.Colors;
            Speed = configuration.Speed;
        }


        public Color Next()
        {
            var from = (int)Time;
            var to = from + 1;
            from %= Colors.Length;
            to %= Colors.Length;

            var fromColor = Colors[from];
            var toColor = Colors[to];
            var t = Time - (int)Time;
            var selected = Color.Lerp(fromColor, toColor, t);

            Time += Speed;

            return selected;
        }
    }
}
