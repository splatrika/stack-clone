using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class TowerConfiguration
    {
        public Rect StartRect { get; set; }
        public Color StartColor { get; set; }
        public float PerfectDistance { get; set; }


        public TowerConfiguration(
            Rect startRect,
            Color startColor,
            float perfectDistance)
        {
            StartRect = startRect;
            StartColor = startColor;
            PerfectDistance = perfectDistance;
        }
    }
}
