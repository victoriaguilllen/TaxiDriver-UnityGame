using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Obstacle : MonoBehaviour
{
    // Evento estático para cuando un obstáculo es clicado
    public static event Action<Obstacle> OnObstacleClicked;

    // Información del obstáculo
    private string typeOfObstacle;
    private int negLivePoints;
    private float multiplyFactor;
    private int secAcffectedHighSpeed;
    private int priceToDestroy;
    public RoadTile AssociatedTile { get; set; }


    // Inicializa las variables
    public void Initialize(string typeOfObstacle, int negLivePoints, float multiplyFactor, int secAcffectedHighSpeed, int priceToDestroy)
    {
        this.typeOfObstacle = typeOfObstacle;
        this.negLivePoints = negLivePoints;
        this.multiplyFactor = multiplyFactor;
        this.secAcffectedHighSpeed = secAcffectedHighSpeed;
        this.priceToDestroy = priceToDestroy;
    }

    // Métodos getter
    public int GetTime() => secAcffectedHighSpeed;
    public int GetLivePoints() => negLivePoints;
    public float GetMultiplyFactor() => multiplyFactor;
    public string GetTypeOfObstacle() => typeOfObstacle;
    public int GetPriceToDestroy() => priceToDestroy;


    // Cuando el jugador hace clic en el obstáculo
    private void OnMouseDown()
    {
        // Se invoca el evento y se pasa el obstaculo actual
        OnObstacleClicked?.Invoke(this);
    }

}

