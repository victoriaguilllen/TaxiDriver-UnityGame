using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PassengerPool : MonoBehaviour
{
    // Array para almacenar los prefabs
    [SerializeField] GameObject[] passengerPrefabs;


    [SerializeField][Range(0.1f, 120)] float spawnTimer = 120f;

    // Referencia al RoadObject que gestiona las Tiles
    [SerializeField] RoadObject roadObject;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnPassengers());
    }

    void PopulatePool()
    {
        if (passengerPrefabs.Length == 0)
        {
            Debug.LogWarning("PassengerPrefabs está vacío. Por favor, agrega prefabs al listado.");
            return;
        }

        pool = new GameObject[passengerPrefabs.Length];

        for (int i = 0; i < passengerPrefabs.Length; i++)
        {
            // Instancia un prefab aleatorio del array
            pool[i] = Instantiate(passengerPrefabs[i], transform);
            pool[i].SetActive(false);
            Passenger p = pool[i].GetComponent<Passenger>();
            p.isActive = false;
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < passengerPrefabs.Length; i++)
        {
            Passenger p = pool[i].GetComponent<Passenger>();
            if (!p.isActive)
            {
                // Asigna una posición aleatoria al objeto activado
                RoadTile selectedTile = roadObject.GetRandomTile();
                if (selectedTile != null)
                {
                    Vector3 position = selectedTile.GetRandomPositionWithinCollider();

                    // Origen del pasajero
                    pool[i].transform.position = position;
                    Vector3 destination = GenerateDestination();
                    p.isActive = true;
                    p.Initialize(selectedTile, destination);
                    

                    pool[i].SetActive(true);
                }
                // No hay pasajeros validos, se sale.
                return;
            }
        }
    }


    Vector3 GenerateDestination()
    {
        // Origen del viaje
        RoadTile selectedTile = roadObject.GetRandomTile();
        Vector3 position = selectedTile.GetRandomPositionWithinCollider();
        return position;
    }

    IEnumerator SpawnPassengers()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
