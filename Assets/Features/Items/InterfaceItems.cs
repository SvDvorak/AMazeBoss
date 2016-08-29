using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.Items
{
    public class InterfaceItems : MonoBehaviour
    {
        public GameObject SpikesVisual;

        void Update ()
        {
            var spikesCarried = Pools.game.GetEntities(GameMatcher.SpikesCarried);

            SpikesVisual.SetActive(spikesCarried.Any());
        }
    }
}