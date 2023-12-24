using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectToInstantiate
{
    public GameObject objectPrefab;
    public Vector3 positionToInstantiate;
}

public class InstantiateObjectsWhenEntityDies : MonoBehaviour
{
    public ObjectToInstantiate[] objectsToInstantiate;

    public void InstantiateObjects()
    {
        foreach (ObjectToInstantiate objectToInstantiate in objectsToInstantiate)
        {
            Vector3 positionToInstantiate = transform.position + objectToInstantiate.positionToInstantiate;

            GameObject objectInstantiated = Instantiate(
                objectToInstantiate.objectPrefab,
                positionToInstantiate,
                objectToInstantiate.objectPrefab.transform.rotation
            );

            objectInstantiated.transform.parent = transform.parent;
        }
    }
}
