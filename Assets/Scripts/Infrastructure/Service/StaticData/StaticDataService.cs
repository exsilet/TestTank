using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Enemy;
using UnityEngine;

namespace Infrastructure.Service.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataTowerPath = "StaticData/Enemies";
        private Dictionary<MonsterTypeID, EnemyStaticData> _enemy;

        public void Load()
        {
            _enemy = Resources
                .LoadAll<EnemyStaticData>(StaticDataTowerPath)
                .ToDictionary(x => x.monsterTypeID, x => x);
        }

        public EnemyStaticData ForTower(MonsterTypeID typeID) =>
            _enemy.TryGetValue(typeID, out EnemyStaticData staticData) 
                ? staticData 
                : null;
    }
}