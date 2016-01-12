using UnityEngine;
using System.Collections;
using System.Linq;
using Entitas;

public class InterfaceItems : MonoBehaviour
{
    public GameObject SpikesVisual;

	void Update ()
	{
	    var spikesCarried = Pools.pool.GetEntities(Matcher.SpikesCarried);

	    SpikesVisual.SetActive(spikesCarried.Any());
	}
}