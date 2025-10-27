using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoticeManager
{
    // Método para mostrar un mensaje importante
    void ShowNotice(string message);

    // Método para ocultar el mensaje
    void HideNotice();
}