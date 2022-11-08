using System;
using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class Tower : ITower
    {
        public Block Last { get; private set; }
        public Block LastUncutted { get; private set; }
        public float PerfectDistance { get; }
        public bool IsFinished { get; private set; }

        public event Action Finished;
        public event ITower.BlockAddedAction BlockAdded;


        public Tower(TowerConfiguration configuration)
        {
            Last = new Block(configuration.StartRect, configuration.StartColor);
            PerfectDistance = configuration.PerfectDistance;
        }


        public void AddBlock(Block block, out bool perfect, out bool finished)
        {
            if (IsFinished)
            {
                throw new InvalidOperationException("Tower already finished");
            }
            perfect = false;
            if (!block.Rect.Overlaps(Last.Rect))
            {
                IsFinished = true;
                Finished?.Invoke();
                finished = true;
                return;
            }
            var distance = Vector2.Distance(Last.Rect.center, block.Rect.center);
            if (distance <= PerfectDistance)
            {
                perfect = true;
                var updatedRect = block.Rect;
                updatedRect.center = Last.Rect.center;
                block = new Block(updatedRect, block.Color);
            }
            LastUncutted = block;
            Last = block.Cut(Last.Rect);
            BlockAdded?.Invoke(Last, perfect);
            finished = false;
        }
    }
}
