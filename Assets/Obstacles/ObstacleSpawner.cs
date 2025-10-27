using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    // Tipos de prefabs de los obstáculos
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] ObstacleFactory obstacleFactory;


    //// Posiciones válidas
    private List<RoadTile> validPositions;
    private List<GameObject> obstaculosEnTablero;

    // Referencia al RoadObject que gestiona las Tiles
    [SerializeField] RoadObject roadObject;

    // Límites
    [SerializeField] private float initialSpawnInterval = 15f; // Tiempo de aparición inicial
    [SerializeField] private float minSpawnInterval = 2f;      // Tiempo mínimo de aparición
    [SerializeField] private float spawnIntervalReduction = 1f; // Reducción del tiempo de aparición cada 20 segundos

    Bank bank;

    private float spawnInterval;
    private float spawnTime;
    private float elapsedTime;


    // Variable de control para verificar si sigue habiendo posiciones validas
    private bool hasValidPositions = true;

    void Start()
    {
        // Inicializar variables
        validPositions = roadObject.RoadTiles();
        spawnInterval = initialSpawnInterval;
        obstaculosEnTablero = new List<GameObject>();
        bank = FindObjectOfType<Bank>();

        // Suscribir al evento
        Obstacle.OnObstacleClicked += HandleObstacleClicked;
    }

    void OnDestroy()
    {
        Obstacle.OnObstacleClicked -= HandleObstacleClicked; // Desuscribir al evento
    }

    private void HandleObstacleClicked(Obstacle obstacle)
    {
        if (obstacle == null) return;

        // Si el banco tiene suficiente dinero para destruir el obstáculo
        int price = obstacle.GetPriceToDestroy(); // Obtener el precio para destruirlo
        if (bank.CurrentBalance>=price)
        {
            NoticeEvents.RaiseNotice($"Coste de {price.ToString()}. Obstáculo destruído.");
            
            obstacle.AssociatedTile.HasObstacle = false;
            bank.Withdraw(price);
            Destroy(obstacle.gameObject); // Destruir el obstáculo
        }
        else
        {
            NoticeEvents.RaiseNotice($"No tienes suficiente dinero para destruir este obstáculo.");
        }
    }


    void Update()
    {
        // Si no hay posiciones válidas, detener el proceso de generación
        if (!hasValidPositions)
        {
            Debug.Log("No hay posiciones válidas disponibles. Deteniendo generación de obstáculos.");
            return;
        }

        // Actualizar tiempo transcurrido
        elapsedTime += Time.deltaTime;
        spawnTime += Time.deltaTime;

        // Reducir el intervalo de aparición progresivamente después del primer minuto
        if (elapsedTime > 60f && elapsedTime % 20f < Time.deltaTime)
        {
            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalReduction, minSpawnInterval);
        }

        // Generar un obstáculo si se cumple el tiempo de aparición
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            GenerateObstacle();
        }

    }

    private void GenerateObstacle()
    {

        // Elegir aleatoriamente un índice en la lista de posiciones válidas
        RoadTile tile = roadObject.GetRandomTile();

        // Si ya tiene obstaculo, se busca otra
        while (tile.HasObstacle)
        {
            tile = roadObject.GetRandomTile();
        }


        // Actualizar el parametro en el tile
        tile.HasObstacle = true;

        // Elegir aleatoriamente el objeto que se va a crear
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Vector3 position = tile.transform.position;
        Quaternion rotation = tile.transform.rotation;

        GameObject randomObstacle = obstacleFactory.CreateObstacle(prefab, position, rotation);
        Obstacle obstacleComponent = randomObstacle.GetComponent<Obstacle>();
        obstacleComponent.AssociatedTile = tile;

        hasValidPositions = roadObject.CheckMorePossibleObstacles();

    }

}

