using Data;
using Infrastructure.Factory;
using Infrastructure.Service;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private const float MinimalDistance = 15;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if(IsInitialized() && IsHeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private void OnDestroy()
        {
            if(_gameFactory != null)
                _gameFactory.HeroCreated -= HeroCreated;
        }

        private bool IsInitialized() => 
            _heroTransform != null;

        private void HeroCreated() =>
            InitializeHeroTransform();

        private void InitializeHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool IsHeroNotReached() => 
            _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
    }
}