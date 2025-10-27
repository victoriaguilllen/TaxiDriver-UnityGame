using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Taxi : Vehicle
{
    private enum TaxiState
    {
        LookingForPassenger, // Buscando pasajero
        GoingToPickUp,       // Viajando para recoger el pasajero
        TransportingPassenger, // Transportando al pasajero
        FinishingRide        // Finalizando el viaje
    }
    private TaxiState currentState;


    private List<RoadTile> path = null; // Ruta a seguir
    private RoadTile tile;

    private Passenger objective;
    private RoadObject roadObject;
    private Bank bank;

    private Passenger pickUp = null;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
        Initialize("Taxi");
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(45.0f);
        roadObject = FindObjectOfType<RoadObject>();
        RestartSituation();
    }

    void RestartSituation()
    {
        currentState = TaxiState.LookingForPassenger; // Comienza buscando pasajeros
        pickUp = null;
        objective = null;
        roadObject.UnhighlightAll();
    }

    void Update()
    {
        tile = roadObject.GetRoadTileAtPosition(transform.position);
        tile.UnhighlightTile();

        if (currentState == TaxiState.LookingForPassenger)
        {
            objective = FindClosestPassenger();
            if (objective != null)
            {
                path = roadObject.FindPath(tile, objective.Tile);
                currentState = TaxiState.GoingToPickUp; // Cambiar al estado de ir a recoger
            }

        }

        else if (currentState == TaxiState.GoingToPickUp)
        {
            FindClosestPassenger();
            if (pickUp != null)
            {
                roadObject.UnhighlightAll();
                InitiateJourney();
            }

        }

        else if (currentState == TaxiState.TransportingPassenger)
        {
            if (pickUp != null)
            {
                CheckFinishedJourney(); // Comprobar si se ha llegado al destino
            }
        }

        else if (currentState == TaxiState.FinishingRide)
        {
            FinishRide();
        }
    }


    Passenger FindClosestPassenger()
    {
        Passenger[] passengers = FindObjectsOfType<Passenger>();
        Passenger closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Passenger passenger in passengers)
        {
            if (passenger.isActive)
            {
                float targetDistance = Vector3.Distance(transform.position, passenger.transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = passenger;
                    maxDistance = targetDistance;
                }
            }

        }
        if (maxDistance <= Threshold)
        {
            pickUp = closestTarget;
        }
        return closestTarget;
    }


    void InitiateJourney()
    {
        path = roadObject.FindPath(roadObject.GetRoadTileAtPosition(transform.position), roadObject.GetRoadTileAtPosition(pickUp.Destination)); // Usar las coordenadas del taxi y el pasajero

        currentState = TaxiState.TransportingPassenger; // Cambiar estado a transporte de pasajero
        pickUp.gameObject.SetActive(false);
        NoticeEvents.RaiseNotice($"Has recogido a un pasajero. ¡Destino: {pickUp.Destination}!");
    }



    void CheckFinishedJourney()
    {
        float destinationDistance = Vector3.Distance(transform.position, pickUp.Destination);
        if (destinationDistance <= Threshold)
        {
            currentState = TaxiState.FinishingRide; // Llegó al destino, ahora finaliza el viaje
        }
    }
    void FinishRide()
    {
        pickUp.transform.position = pickUp.Destination;
        pickUp.gameObject.SetActive(true);

        pickUp.isActive = false;
        pickUp.SwitchHalo();

        // Se recibe el pago
        bank.Deposit(pickUp.Precio);

        // Mostrar mensaje de llegada
        NoticeEvents.RaiseNotice($"Has dejado al pasajero en su destino. Ganaste {pickUp.Precio}€!");

        // Reiniciar el estado del taxi
        RestartSituation();
    }
}
