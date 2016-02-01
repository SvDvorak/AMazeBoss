using System;
using Entitas;

namespace Assets.Input
{
    public class InputMoveComponent : IComponent
    {
        public TilePos Direction;
    }

    public class InputPullItemComponent : IComponent
    {
        public TilePos Direction;
    }

    public class InputItemInteractComponent : IComponent
    {
    }

    public class InputCurseSwitchComponent : IComponent
    {
    }

    public class InputQueueComponent : IComponent
    {
        public Action InputAction;
    }
}
