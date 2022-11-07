using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class ColorServiceConfiguration
    {
        public Color[] Colors { get; set; }
        public float Speed { get; set; }


        public ColorServiceConfiguration(Color[] colors, float speed)
        {
            Colors = colors;
            Speed = speed;
        }
    }
}
