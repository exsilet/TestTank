using UnityEngine;

namespace Player
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "MoveHorizontal";
        protected const string Vertical = "MoveVertical";
        private const string Button = "AttackTank";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonX()
        {
            return SimpleInput.GetButtonDown(Button);
        }

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}