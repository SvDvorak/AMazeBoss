using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
    public class LevelLoader
    {
        private static readonly DescriptorResolver DescriptorResolver = new DescriptorResolver();

        public static Level CreateLevelData(Pool pool)
        {
            var mapObjects = pool.GetEntities(GameMatcher.GameObject).Where(x => !x.isPreview);
            var fileMap = new Level(
                mapObjects
                    .Select(x => CreateFileMapObject(x))
                    .ToList());

            return fileMap;
        }

        private static LevelObject CreateFileMapObject(Entity entity)
        {
            var pos = entity.position.Value;
            return new LevelObject(entity.maintype.Value, entity.subtype.Value, pos.X, pos.Z, entity.rotation.Value,
                DescriptorResolver.ToDescriptors(entity));
        }

        public static void ReadLevelData(Level level, Pool pool)
        {
            level
                .Tiles
                .ForEach(tile => CreateMapObject(pool, tile));

            var camera = pool.GetEntities(GameMatcher.Resource).SingleOrDefault(x => x.resource.Path == "Camera");
            if (camera == null)
            {
                pool.CreateEntity().AddResource("Camera").AddRotation(0).ReplaceTargetFocusPoint(Vector3.zero);
            }
        }

        private static void CreateMapObject(Pool pool, LevelObject mapGameObject)
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