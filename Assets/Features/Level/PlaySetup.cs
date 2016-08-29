using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Level
{
    public class PlaySetup : MonoBehaviour
    {
        private Systems _systems;
        private Pool _gamePool;

        public void Start()
        {
            Random.seed = 42;
            SceneSetup.CurrentScene = "Play";
            SceneSetup.OnSceneChanging += OnSceneChanging;

            _gamePool = Pools.game;
            _systems = SceneSetup.CreateSystem();
            SetupPlaySystems(PuzzleLayout.Instance);

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public void OnDestroy()
        {
            _systems.ClearReactiveSystems();
            _gamePool.Reset();
            SceneSetup.OnSceneChanging -= OnSceneChanging;
        }

        private void OnSceneChanging()
        {
            _gamePool.SafeDeleteAll();
        }

        private void SetupPlaySystems(PuzzleLayout layout)
        {
            _systems.AddSystems(_gamePool, x => x
                .InputSystems()

                .UpdateSystems()

                .RenderSystems()
                .AnimationSystems()

                .LevelHandlingSystems(layout)

                .DestroySystems());
        }
    }
}