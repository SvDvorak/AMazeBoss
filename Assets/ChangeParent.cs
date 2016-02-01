using UnityEngine;

public class ChangeParent : MonoBehaviour
{
    public Transform RuntimeParent;

    public void Awake ()
	{
        if (RuntimeParent == null)
        {
            throw new FieldReferenceException(gameObject.name, "ToClone");
        }

        transform.SetParent(RuntimeParent);
	}
}