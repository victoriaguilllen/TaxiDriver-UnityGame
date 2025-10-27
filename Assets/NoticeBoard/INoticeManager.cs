using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoticeManager
{
    // M�todo para mostrar un mensaje importante
    void ShowNotice(string message);

    // M�todo para ocultar el mensaje
    void HideNotice();
}