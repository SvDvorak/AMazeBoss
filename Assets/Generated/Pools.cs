using Entitas;

public static class Pools {

    static Pool[] _allPools;

    public static Pool[] allPools {
        get {
            if (_allPools == null) {
                _allPools = new [] { game, ui };
            }

            return _allPools;
        }
    }

    static Pool _game;

    public static Pool game {
        get {
            if (_game == null) {
                _game = new Pool(GameComponentIds.TotalComponents, 0, new PoolMetaData("Game Pool", GameComponentIds.componentNames));
                #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
                var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(_game, GameComponentIds.componentTypes);
                UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);
                #endif
            }

            return _game;
        }
    }

    static Pool _ui;

    public static Pool ui {
        get {
            if (_ui == null) {
                _ui = new Pool(UiComponentIds.TotalComponents, 0, new PoolMetaData("Ui Pool", UiComponentIds.componentNames));
                #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
                var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(_ui, UiComponentIds.componentTypes);
                UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);
                #endif
            }

            return _ui;
        }
    }
}