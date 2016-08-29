using System;
using Entitas;

namespace Assets.Input
{
    [Game]
    public class InputMoveComponent : IComponent
    {
        public TilePos Direction;
    }

    [Game]
    public class InputPullItemComponent : IComponent
    {
        public TilePos Direction;
    }

    [Game]
    public class InputItemInteractComponent : IComponent
    {
    }

    [Game]
    public class InputCurseSwitchComponent : IComponent
    {
    }

    [Game]
    public class InputQueueComponent : IComponent
    {
        public Action InputAction;
    }
}
