using Infrastructure.CameraLogic;
using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Logic;
using UI.Element;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private IState _stateImplementation;
        private Camera _camera;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject hud = _gameFactory.CreateHud();
            
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            //hero.transform.SetParent(Camera.main.transform);
            
            InitSpawners(hud, hero);
            CameraFollow(hero);
        }

        private void InitSpawners(GameObject hudBattle, GameObject hero)
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag("EnemySpawner"))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                spawner.GetComponent<EnemySpawner>().Construct(hudBattle.GetComponent<StartBattle>());
                _gameFactory.Register(spawner);
            }
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);

        }
        
        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}
