using System;
using System.Collections.Generic;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Service.SaveLoad;
using Infrastructure.Service.StaticData;
using Infrastructure.StaticData.Enemy;
using Logic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }
        public event Action HeroCreated;
        
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = _assets.Instantiate(path: AssetPath.HeroPath, at: at.transform.position);
            HeroCreated?.Invoke();

            RegisterProgressWatchers(HeroGameObject);

            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            RegisterProgressWatchers(hud);
            return hud;
        }

        public GameObject CreateHudMenu()
        {
            GameObject hudMenu = _assets.Instantiate(AssetPath.HudMenuPath);
            RegisterProgressWatchers(hudMenu);
            return hudMenu;
        }

        public GameObject CreatEnemy(MonsterTypeID typeId, Transform parent)
        {
            EnemyStaticData monsterData = _staticData.ForTower(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = monsterData.MoveSpeed;

            var attack = monster.GetComponent<EnemyAttack>();
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            return monster;
        }

        public void Cleanup()
        {            
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
    }
}