using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Vehicle : MonoBehaviour
{
    private string typeOfVehicle;
    private float speed;
    [SerializeField] float threshold = 3;
    [SerializeField] string plate = "AAA2";

    // Getter del threshold
    public float Threshold  {get { return threshold; }
    }
    // Método para inicializar el vehículo
    public void Initialize(string typeOfVehicle)
    {
        this.typeOfVehicle = typeOfVehicle;
        speed = 0f; // Inicializamos la velocidad si no se pasa valor
    }

    // Override ToString() method with class information
    public override string ToString()
    {
        return $"{GetTypeOfVehicle()} with plate {GetPlate()}";
    }

    public string GetTypeOfVehicle()
    {
        return typeOfVehicle;
    }

    public string GetPlate()
    {
        return plate;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
