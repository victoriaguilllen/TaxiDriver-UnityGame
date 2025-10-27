using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NoticeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noticeText; // Referencia al componente TextMeshProUGUI
    [SerializeField] private float displayDuration = 5f; // Duración del mensaje en segundos

    private float timer;

    void OnEnable()
    {
        // Suscribirse al evento de notificaciones
        NoticeEvents.OnNotice += ShowNotice;
    }

    void OnDisable()
    {
        // Desuscribirse del evento de notificaciones
        NoticeEvents.OnNotice -= ShowNotice;
    }

    void Update()
    {
        // Ocultar el mensaje después de que pase el tiempo de visualización
        if (noticeText != null && noticeText.gameObject.activeSelf)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                HideNotice();
            }
        }
    }

    // Mostrar el mensaje en pantalla
    private void ShowNotice(string message)
    {
        if (noticeText == null)
        {
            Debug.LogWarning("No se ha asignado un TextMeshProUGUI en el NoticeManager.");
            return;
        }

        noticeText.text = message;
        noticeText.gameObject.SetActive(true);
        timer = displayDuration;
    }

    // Ocultar el mensaje
    private void HideNotice()
    {
        if (noticeText != null)
        {
            noticeText.gameObject.SetActive(false);
        }
    }
}
