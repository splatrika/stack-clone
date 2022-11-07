using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class MovingBlockConfiguration
    {
        public Rect StartRect { get; set; }
        public float MinSpeed { get; set; }
        public float Acceleration { get; set; }
        public float TimeAcceleration { get; set; }
        public Rect Bounds { get; set; }


        public MovingBlockConfiguration(
            Rect startRect,
            float minSpeed,
            float acceleration,
            float timeAcceleration,
            Rect bounds)
        {
            StartRect = startRect;
            MinSpeed = minSpeed;
            Acceleration = acceleration;
            TimeAcceleration = timeAcceleration;
            Bounds = bounds;
        }
    }
}
