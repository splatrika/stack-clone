using System;
using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class MovingBlock : IUpdatable
    {
        public Block Block => new Block(Rect, Color);
        public bool IsDestroyed { get; private set; }
        public Rect Rect => _rect;
        public Color Color { get; private set; }

        public float TotalSpeed => Speed + TimeSpeed;
        public float Speed { get; private set; }
        public float TimeSpeed { get; private set; }
        public float MinSpeed { get; }
        public float Acceleration { get; }
        public float TimeAcceleration { get; }

        public Vector2 Direction => _directions[_currentDirection];
        public float Time { get; private set; }
        public Vector2 MovementOffset { get; private set; }
        public Vector2 MovementCenter { get; }
        public Rect Bounds { get; }

        private Vector2[] _directions;
        private int _currentDirection;
        private Rect _rect;
        private ITower _tower { get; }
        private IColorService _colorService { get; }

        public event Action Destroyed;
        public event Action PushedToTower;


        public MovingBlock(
            MovingBlockConfiguration configuration,
            ITower tower,
            IColorService colorService)
        {
            _tower = tower;
            _colorService = colorService;

            _directions = new Vector2[] { Vector2.left, Vector2.up };
            MovementCenter = Vector2.zero;
            Color = _colorService.Next();
            _rect = configuration.StartRect;
            MinSpeed = configuration.MinSpeed;
            Speed = MinSpeed;
            Acceleration = configuration.Acceleration;
            TimeAcceleration = configuration.TimeAcceleration;
            Bounds = configuration.Bounds;
        }


        public void PushToTower()
        {
            if (IsDestroyed)
            {
                throw new InvalidOperationException("Block is destroyed");
            }

            var block = new Block(Rect, Color);
            _tower.AddBlock(block, out bool perfect, out bool finished);
            if (finished)
            {
                IsDestroyed = true;
                Destroyed?.Invoke();
                return;
            }
            Speed += perfect ? Acceleration : -Acceleration;
            Speed = Mathf.Max(MinSpeed, Speed);
            TimeSpeed += TimeAcceleration;
            Time = 0;

            _currentDirection++;
            _currentDirection %= _directions.Length;

            _rect = _tower.Last.Rect;
            Color = _colorService.Next();
            MovementOffset = _rect.center - MovementCenter;

            PushedToTower?.Invoke();
        }


        public void Update(float deltaTime)
        {
            if (IsDestroyed)
            {
                return;
            }
            UpdateMovement(deltaTime);
        }


        private void UpdateMovement(float deltaTime)
        {
            Time += deltaTime;
            // ping pong from -1 to 1
            var position = Mathf.PingPong(Time * TotalSpeed, 2) - 1;

            _rect.center = Direction * position * Bounds.size + MovementOffset;
        }
    }
}
