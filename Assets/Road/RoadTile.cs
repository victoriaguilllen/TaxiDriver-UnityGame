using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadTile : MonoBehaviour
{
    public float gridSize = 0.5f; // Tamaño de las subdivisiones dentro de la Tile
    public Vector3 tileSize = new Vector3(1, 1, 1); // Tamaño de la Tile

    public List<RoadTile> neighbors; // Lista de vecinos a este RoadTile
    private bool isHighlighted = false; // Estado del halo
    [SerializeField] float customNeighborDistance = 10.0f;

    private MeshCollider meshCollider;
    private BoxCollider[] boxColliders;
    public bool HasObstacle { get; set; } = false;

    public MeshCollider GetMeshCollider()
    {
        return meshCollider;
    }

    public float CustomNeighborDistance
    {
        get { return customNeighborDistance; }
        set { customNeighborDistance = value; }
    }

    void Start()
    {
        meshCollider = GetComponentInChildren<MeshCollider>();
        boxColliders = GetComponentsInChildren<BoxCollider>();
    }



    public Vector3 GetRandomPositionWithinCollider()
    {
        // Obtenemos el centro y los extents del MeshCollider
        Bounds bounds = meshCollider.bounds;

        // Generamos una posición aleatoria dentro de los límites del collider
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // Creamos la posición aleatoria
        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        if (CheckIsPlaceable(randomPosition)) { return randomPosition; }
        return GetRandomPositionWithinCollider();
    }


    // Método para verificar si una posición específica es válida
    public bool CheckIsPlaceable(Vector3 position)
    {
        foreach (BoxCollider boxCollider in boxColliders)
        {
            if (boxCollider.bounds.Contains(position))
            { return false; }

        }
        return true;
    }

    // Resaltar la tile en dorado
    public void HighlightTile()
    {
        if (!isHighlighted)
        {
            // Activa el halo asociado al GameObject del tile
            Behaviour halo = (Behaviour)GetComponent("Halo");
            if (halo != null)
            {
                halo.enabled = true; // Activa el halo
            }

            isHighlighted = true;
        }
    }


    // Apagar el resaltado
    public void UnhighlightTile()
    {
        if (isHighlighted)
        {
            // Activa el halo asociado al GameObject del tile
            Behaviour halo = (Behaviour)GetComponent("Halo");
            if (halo != null)
            {
                halo.enabled = false; // Desactiva el halo
            }

            isHighlighted = false;
        }
    }


    // Método para establecer los vecinos de este tile
    public void SetNeighbors(List<RoadTile> adjacentTiles)
    {
        neighbors = new List<RoadTile>(adjacentTiles);

    }

}
