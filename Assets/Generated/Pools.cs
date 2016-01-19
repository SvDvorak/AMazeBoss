using Entitas;

public static class Pools {

    static Pool[] _allPools;

    public static Pool[] allPools {
        get {
            if (_allPools == null) {
                _allPools = new [] { pool };
            }

            return _allPools;
        }
    }

    static Pool _pool;

    public static Pool pool {
        get {
            if (_pool == null) {
                _pool = new Pool(ComponentIds.TotalComponents, 0, new PoolMetaData("Pool", ComponentIds.componentNames));
                #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
                var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(_pool, ComponentIds.componentTypes);
                UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);
                #endif
            }

            return _pool;
        }
    }
}