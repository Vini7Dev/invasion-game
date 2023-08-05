using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    xPositive,
    xNegative,
    yPositive,
    yNegative,
    zPositive,
    zNegative,
}

[System.Serializable]
public class ScenaryObject
{
    public Directions instantiateDirection = Directions.xPositive;
    public GameObject scenaryToInstantiate;
}

public class GenerateScenary : MonoBehaviour
{
    public ScenaryObject[] scenaryObjectsToInstantiate;

    void Start()
    {
        InstantiateScenary();
    }

    void InstantiateScenary()
    {
        for (int i = 0; i < scenaryObjectsToInstantiate.Length; i++)
        {
            ScenaryObject scenaryToInstantiate = scenaryObjectsToInstantiate[i];
            Vector3 instantiatePosition = GetInstantiatePosition(
                scenaryToInstantiate.instantiateDirection
            );
            Quaternion instantiateRotation = GetInstantiateRotation(
                scenaryToInstantiate.instantiateDirection
            );

            GameObject instanciatedScenaray = Instantiate(
                scenaryToInstantiate.scenaryToInstantiate,
                transform.position + instantiatePosition,
                transform.rotation * instantiateRotation
            );

            instanciatedScenaray.transform.parent = transform.parent;
        }

        Destroy(gameObject);
    }

    Vector3 GetInstantiatePosition(Directions direction)
    {
        switch (direction)
        {
            case Directions.xPositive:
                return new Vector3(2.5f, -2.5f, 0);
            case Directions.xNegative:
                return new Vector3(-2.5f, -2.5f, 0);
            case Directions.yPositive:
                return new Vector3(0, 0, 0);
            case Directions.yNegative:
                return new Vector3(0, -5, 0);
            case Directions.zPositive:
                return new Vector3(0, -2.5f, 2.5f);
            case Directions.zNegative:
                return new Vector3(0, -2.5f, -2.5f);
            default:
                return Vector3.zero;
        }
    }

    Quaternion GetInstantiateRotation(Directions direction)
    {
        switch (direction)
        {
            case Directions.xPositive:
                return Quaternion.Euler(0, 90, 0);
            case Directions.xNegative:
                return Quaternion.Euler(0, -90, 0);
            case Directions.yPositive:
                return Quaternion.Euler(-90, 0, 0);
            case Directions.yNegative:
                return Quaternion.Euler(90, 0, 0);
            case Directions.zPositive:
                return Quaternion.Euler(0, 0, 0);
            case Directions.zNegative:
                return Quaternion.Euler(0, 180, 0);
            default:
                return Quaternion.identity;
        }
    }
}