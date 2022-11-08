using System;
using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class Tower : ITower, IResetable
    {
        public Block Last { get; private set; }
        public Block LastUncutted { get; private set; }
        public float PerfectDistance { get; }
        public bool IsFinished { get; private set; }

        private Rect _startRect { get; }
        private IColorService _colorService { get; }

        public event Action Finished;
        public event ITower.BlockAddedAction BlockAdded;
        public event Action Reseted;

        public Tower(
            TowerConfiguration configuration,
            IColorService colorService)
        {
            _colorService = colorService;

            Last = new Block(configuration.StartRect, configuration.StartColor);
            LastUncutted = Last;
            PerfectDistance = configuration.PerfectDistance;
            _startRect = configuration.StartRect;
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


        public void Reset()
        {
            Last = new Block(_startRect, _colorService.Next());
            LastUncutted = Last;
            IsFinished = false;
            Reseted?.Invoke();
        }
    }
}
