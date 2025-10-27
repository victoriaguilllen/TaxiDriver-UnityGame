using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class NoticeEvents
{
    // Delegado para manejar los mensajes
    public delegate void NoticeMessage(string message);

    // Evento estático para suscribirse
    public static event NoticeMessage OnNotice;

    // Método para invocar el evento
    public static void RaiseNotice(string message)
    {
        OnNotice?.Invoke(message); 
    }
}
