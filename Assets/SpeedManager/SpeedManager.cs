using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    private List<Radar> radars;

    public static Action<Taxi> OnSpeedViolationDetected; // Evento para notificar exceso de velocidad

    public void NotifySpeedViolation(Taxi taxi)
    {
        OnSpeedViolationDetected?.Invoke(taxi); // Llama a todos los suscriptores y les pasa el taxi
    }

    void Start()
    {
        radars = new List<Radar>(FindObjectsOfType<Radar>());
        if (radars.Count == 0)
        {
            Debug.LogWarning("No hay radares.");
        }
    }

    void Update()
    {
        foreach (Radar radar in radars)
        {
            Taxi detectedTaxi = radar.DetectTaxi();
            if (detectedTaxi != null)
            {
                bool isOverSpeeding = radar.MeasureSpeed(detectedTaxi);
                if (isOverSpeeding)
                {
                    NotifySpeedViolation(detectedTaxi);
                    Debug.Log($"Taxi {detectedTaxi.GetPlate()} captado por el radar {radar.name} excediendo la velocidad permitida.");
                }
            }
        }
    }


}
