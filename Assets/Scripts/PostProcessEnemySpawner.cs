using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;


[CreateAssetMenu(menuName = "Set Spawn Enemy", fileName = "Tags")]
public class PostProcessEnemySpawner : DungeonGeneratorPostProcessBase
{

   private EnemyManager enemyManager;
   
   public override void Run(GeneratedLevel level, LevelDescription levelDescription)
   {
      var target = FindObjectsByType<EnemyManager>(FindObjectsSortMode.InstanceID);
      if (target.Length>1)
      {
         Debug.LogError("Trop d'enemy Manager");
      }

      enemyManager = target[0];
      var levelRootGameObject = level.RootGameObject;
      var spawnsPoint = levelRootGameObject.GetComponentsInChildren<SpawnerEnemy>();
      foreach (var spawnPoint in spawnsPoint)
      {
         spawnPoint.SetEnemyManager(enemyManager);
      }
   }
}
