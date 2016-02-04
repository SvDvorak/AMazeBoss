using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
    public class LevelParser
    {
        private static readonly DescriptorResolver DescriptorResolver = new DescriptorResolver();

        public static string CreateLevelData()
        {
            var pool = Pools.pool;
            var mapObjects = pool.GetEntities(Matcher.AnyOf(Matcher.Tile, Matcher.Item));
            var fileMap = new FileMap(
                CreateFileCamera(pool),
                mapObjects
                    .Select(x => CreateFileMapObject(x))
                    .ToList());
            return JsonUtility.ToJson(fileMap);
        }

        private static FileCamera CreateFileCamera(Pool pool)
        {
            var cameraFocus = pool.GetEntities(Matcher.Camera).SingleEntity().savedFocusPoint;
            return new FileCamera(cameraFocus.Position.x, cameraFocus.Position.z);
        }

        private static FileMapObject CreateFileMapObject(Entity entity)
        {
            var pos = entity.position.Value;
            return new FileMapObject(entity.maintype.Value, entity.subtype.Value, pos.X, pos.Z, entity.rotation.Value, DescriptorResolver.ToDescriptors(entity));
        }

        public static void ReadLevelData(string json)
        {
            var pool = Pools.pool;

            var fileLevelObjects = JsonUtility
                .FromJson<FileMap>(json);
            fileLevelObjects
                .Tiles
                .ForEach(tile => CreateEntity(pool, tile));

            var alreadyHasCamera = pool.GetEntities(Matcher.Camera).Any();
            if (!alreadyHasCamera)
            {
                CreateEntity(pool, fileLevelObjects.Camera);
            }
        }

        private static void CreateEntity(Pool pool, FileCamera camera)
        {
            pool.CreateEntity()
                .AddResource("Camera")
                .AddFocusPoint(new Vector3(camera.FocusX, 0, camera.FocusZ))
                .AddSavedFocusPoint(new Vector3(camera.FocusX, 0, camera.FocusZ))
                .AddRotation(0);
        }

        private static void CreateEntity(Pool pool, FileMapObject mapGameObject)
        {
            var entity = pool
                .CreateEntity()
                .AddMaintype(mapGameObject.MainType)
                .AddSubtype(mapGameObject.Subtype)
                .AddPosition(new TilePos(mapGameObject.X, mapGameObject.Z))
                .AddRotation(mapGameObject.Rotation);

            DescriptorResolver.FromDescriptors(mapGameObject.Descriptors, entity);
        }
    }
}