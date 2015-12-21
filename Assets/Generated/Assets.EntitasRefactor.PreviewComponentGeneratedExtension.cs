namespace Entitas {
    public partial class Entity {
        static readonly Assets.EntitasRefactor.PreviewComponent previewComponent = new Assets.EntitasRefactor.PreviewComponent();

        public bool isPreview {
            get { return HasComponent(ComponentIds.Preview); }
            set {
                if (value != isPreview) {
                    if (value) {
                        AddComponent(ComponentIds.Preview, previewComponent);
                    } else {
                        RemoveComponent(ComponentIds.Preview);
                    }
                }
            }
        }

        public Entity IsPreview(bool value) {
            isPreview = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity previewEntity { get { return GetGroup(Matcher.Preview).GetSingleEntity(); } }

        public bool isPreview {
            get { return previewEntity != null; }
            set {
                var entity = previewEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isPreview = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPreview;

        public static IMatcher Preview {
            get {
                if (_matcherPreview == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Preview);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPreview = matcher;
                }

                return _matcherPreview;
            }
        }
    }
}
