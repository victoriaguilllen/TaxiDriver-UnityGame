using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class PoliceCar : Vehicle
{
    private bool isChasing = false;
    private Taxi targetTaxi;
    private NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent

    public delegate void PoliceCatchHandler(Taxi taxi);
    public event Action<Taxi> OnTaxiCaught;

    //variable para almacenar la posicion inicial del taxi
    private Vector3 initialPolicePosition;

    private void Start()
    {
        initialPolicePosition = transform.position; // Allmaceno la posicion inicial
    }

    private void Awake()
    {
        Initialize("Police Car");
        navMeshAgent = GetComponent<NavMeshAgent>(); // Obtener el componente NavMeshAgent
        
    }

    private void OnEnable()
    {
        SpeedManager.OnSpeedViolationDetected += StartChase;
    }

    private void OnDisable()
    {
        SpeedManager.OnSpeedViolationDetected -= StartChase;
    }

    private void Update()
    {
        if (isChasing && targetTaxi != null)
        {
            ChaseTaxi();
        }
    }

    private void StartChase(Taxi taxi)
    {
        if (!isChasing)
        {
            targetTaxi = taxi;
            isChasing = true;
            NoticeEvents.RaiseNotice($"{GetPlate()} comienza a perseguir al taxi {taxi.GetPlate()}.");

            // Establecer el destino del NavMeshAgent
            navMeshAgent.SetDestination(targetTaxi.transform.position);
        }
    }

    private void ChaseTaxi()
    {
        if (targetTaxi == null) return;

        // Establecer el destino nuevamente si el taxi se mueve
        if (navMeshAgent.destination != targetTaxi.transform.position)
        {
            navMeshAgent.SetDestination(targetTaxi.transform.position);
        }

        // Suavizar la rotación del PoliceCar para que mire en la dirección del movimiento
        Vector3 direction = targetTaxi.transform.position - transform.position;
        if (direction.sqrMagnitude > 0.1f) // Asegurarse de que no esté detenido
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
        }

        // Comprueba si el coche de policía está suficientemente cerca del taxi
        float distanceToTaxi = Vector3.Distance(transform.position, targetTaxi.transform.position);
        if (distanceToTaxi < 2.0f) // Distancia mínima para detener la persecución
        {
            NoticeEvents.RaiseNotice($"{GetPlate()} ha alcanzado al taxi {targetTaxi.GetPlate()}.");
            StopChase();
        }
    }

    private void StopChase()
    {
        isChasing = false;
        navMeshAgent.ResetPath(); // Detener el agente de navegación

        if (targetTaxi != null)
        {
            // Invocar el evento con el taxi que fue capturado
            OnTaxiCaught?.Invoke(targetTaxi);

            //El policia vuelve a su posicion inicial tras 2 segundos
            StartCoroutine(WaitAndReturnToInitialPosition());

        }

        targetTaxi = null; // El taxi ya no es el objetivo
    }

    private IEnumerator WaitAndReturnToInitialPosition()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(2f);

        // Volver a la posición inicial del policia
        transform.position = initialPolicePosition;

    }
}
