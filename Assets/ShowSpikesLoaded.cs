using Assets;
using UnityEngine;
using Entitas;

public class ShowSpikesLoaded : MonoBehaviour
{
    public GameObject Spikes;

    private Group _spikeTrapGroup;

    public void Start ()
    {
        _spikeTrapGroup = Pools.pool.GetGroup(Matcher.AllOf(Matcher.SpikeTrap, Matcher.View));
        _spikeTrapGroup.OnEntityUpdated += SpikeTrapChanged;
    }

    public void OnDestroy()
    {
        _spikeTrapGroup.OnEntityUpdated -= SpikeTrapChanged;
    }

    private void SpikeTrapChanged(Group trapGroup, Entity trap, int index, IComponent previouscomponent, IComponent newcomponent)
    {
        var spikesChild = trap.view.Value.GetChild("Spikes", true);

        if (trap.spikeTrap.IsLoaded && spikesChild == null)
        {
            var newSpikes = Instantiate(Spikes);
            newSpikes.transform.SetParent(trap.view.Value.transform, false);
        }
        else if (!trap.spikeTrap.IsLoaded && spikesChild != null)
        {
            Destroy(spikesChild);
        }
    }
}
