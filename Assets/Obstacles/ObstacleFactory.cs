using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleFactory: MonoBehaviour
{
    public GameObject CreateObstacle(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }
}

