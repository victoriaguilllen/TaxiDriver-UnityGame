using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    // Tipos de prefabs de los obst�culos
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] ObstacleFactory obstacleFactory;


    //// Posiciones v�lidas
    private List<RoadTile> validPositions;
    private List<GameObject> obstaculosEnTablero;

    // Referencia al RoadObject que gestiona las Tiles
    [SerializeField] RoadObject roadObject;

    // L�mites
    [SerializeField] private float initialSpawnInterval = 15f; // Tiempo de aparici�n inicial
    [SerializeField] private float minSpawnInterval = 2f;      // Tiempo m�nimo de aparici�n
    [SerializeField] private float spawnIntervalReduction = 1f; // Reducci�n del tiempo de aparici�n cada 20 segundos

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

        // Si el banco tiene suficiente dinero para destruir el obst�culo
        int price = obstacle.GetPriceToDestroy(); // Obtener el precio para destruirlo
        if (bank.CurrentBalance>=price)
        {
            NoticeEvents.RaiseNotice($"Coste de {price.ToString()}. Obst�culo destru�do.");
            
            obstacle.AssociatedTile.HasObstacle = false;
            bank.Withdraw(price);
            Destroy(obstacle.gameObject); // Destruir el obst�culo
        }
        else
        {
            NoticeEvents.RaiseNotice($"No tienes suficiente dinero para destruir este obst�culo.");
        }
    }


    void Update()
    {
        // Si no hay posiciones v�lidas, detener el proceso de generaci�n
        if (!hasValidPositions)
        {
            Debug.Log("No hay posiciones v�lidas disponibles. Deteniendo generaci�n de obst�culos.");
            return;
        }

        // Actualizar tiempo transcurrido
        elapsedTime += Time.deltaTime;
        spawnTime += Time.deltaTime;

        // Reducir el intervalo de aparici�n progresivamente despu�s del primer minuto
        if (elapsedTime > 60f && elapsedTime % 20f < Time.deltaTime)
        {
            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalReduction, minSpawnInterval);
        }

        // Generar un obst�culo si se cumple el tiempo de aparici�n
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            GenerateObstacle();
        }

    }

    private void GenerateObstacle()
    {

        // Elegir aleatoriamente un �ndice en la lista de posiciones v�lidas
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

