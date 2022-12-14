using System;
using UnityEngine;

namespace Splatrika.StackClone.Model
{
    public class MovingBlock : IUpdatable, IResetable, IRunnable
    {
        public bool IsRunned { get; private set; }
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

        private Rect _startRect { get; }

        private ITower _tower { get; }
        private IColorService _colorService { get; }

        public event Action Destroyed;
        public event Action PushedToTower;
        public event Action Reseted;
        public event Action Runned;


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

            _startRect = configuration.StartRect;
        }


        public void PushToTower()
        {
            if (IsDestroyed)
            {
                throw new InvalidOperationException("Block is destroyed");
            }
            if (!IsRunned)
            {
                throw new InvalidOperationException("Block is not runned");
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
            if (IsDestroyed || !IsRunned)
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


        public void Run()
        {
            IsRunned = true;
            Runned?.Invoke();
        }


        public void Reset()
        {
            IsRunned = false;
            _currentDirection = 0;
            Time = 0;
            TimeSpeed = 0;
            Speed = MinSpeed;
            _rect = _startRect;
            Color = _colorService.Next();
            MovementOffset = _rect.center - MovementCenter;
            IsDestroyed = false;
            Reseted?.Invoke();
        }
    }
}
