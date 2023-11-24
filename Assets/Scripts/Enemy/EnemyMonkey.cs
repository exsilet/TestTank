using UI.Element;
using UnityEngine;

namespace Enemy
{
    public class EnemyMonkey : MonoBehaviour
    {
        [SerializeField] private int _speed;
        [SerializeField] private Transform _startTargetPoint;

        private bool _startGame;
        private StartBattle _startBattle;
        public bool Shoot;
        public State StartState;
        public State AttackState;
        public State SleepState;
        public State RandomMoveState;
        private State _currenState;


        private void Start()
        {
            MovementToGame();
            SetState(StartState);
        }

        private void Update()
        {
            if (!_currenState.IsFinished)
            {
                _currenState.Run();
            }
            else
            {
                if (!_startBattle.CurrentStartBattle)
                    MovementToGame();
                else if (!Shoot)
                {
                    SetState(AttackState);
                }
                else
                {
                    SetState(RandomMoveState);
                }
            }
        }

        public void Construct(StartBattle startBattle)
        {
            _startBattle = startBattle;
        }

        public void MoveTo(Vector3 targetPosition)
        {
            targetPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }

        private void SetState(State state)
        {
            _currenState = Instantiate(state);
            _currenState.Enemy = this;
            _currenState.Init();
        }

        private void MovementToGame()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _startTargetPoint.position, _speed * Time.deltaTime);
        }
    }
}