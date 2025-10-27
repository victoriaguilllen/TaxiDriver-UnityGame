using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Passenger : MonoBehaviour
{
    // destino del pasajero

    private RoadTile tile;
    private Vector3 initialPosition;

    public Vector3 Destination { get; private set; }

    public RoadTile Tile
    {
        get { return tile; }
        set { tile = value; }
    }


    private Vector3 origin;

    public bool isActive { get; set; }
    public int Precio { get; private set; }
    public Behaviour halo {  get; set; }


    // Start is called before the first frame update
    void OnEnable()
    {
        halo = (Behaviour)GetComponent("Halo");

        isActive = true;
        initialPosition = transform.position;
        Precio = Random.Range(10, 35);
    }

    public void Initialize(RoadTile tile, Vector3 destination)
    {
        Tile = tile;
        Destination = destination;
        halo.enabled = true;
    }

    // Interruptor para el halo
    public void SwitchHalo()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");

        halo.enabled = !halo.enabled;
    }


}