using Assets.Features.Level;
using UnityEngine;

namespace Assets.Level.Editor_
{
    public class LayoutLink : MonoBehaviour
    {
        public PuzzleLayout PuzzleLayout;
        public TilePos Position;

        public bool HasSetLink { get; private set; }

        public void SetLinkInfo(PuzzleLayout puzzleLayout, string type, TilePos position)
        {
            HasSetLink = true;
            PuzzleLayout = puzzleLayout;
            Position = position;

            SendMessage("LayoutLinkSet");
        }
    }
}
