using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class UpdateTaxiSpeed : MonoBehaviour
{
    private Rigidbody taxiRigidbody;
    private CarController carController;
    public TextMeshProUGUI speedText; // Referencia al TextMeshPro en el Canvas

    // Start is called before the first frame update
    void Start()
    {
        taxiRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();

        if (taxiRigidbody == null || carController == null)
        {
            Debug.LogError("Faltan componentes en el taxi. Asegúrate de que este script esté en el taxi con los componentes necesarios.");
        }

        if (speedText == null)
        {
            Debug.LogError("No se asignó un TextMeshProUGUI al script. Arrastra el componente desde el Canvas al inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (taxiRigidbody != null && speedText != null)
        {
            // Calcula la velocidad en km/h
            float speed = taxiRigidbody.velocity.magnitude * 3.6f;

            // Actualiza el texto del TextMeshProUGUI
            speedText.text = Mathf.RoundToInt(speed) + " km/h";
        }
    }
}
