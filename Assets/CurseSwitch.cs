namespace Assets
{
    public class CurseSwitch : GameEvent
    {
        public readonly bool CurseBoss;

        public CurseSwitch(bool curseBoss)
        {
            CurseBoss = curseBoss;
        }
    }
}