using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : SolidObstacle
{
    // Sobrescribe para definir el tipo espec�fico de Fence
    protected override string FindTypeOfObstacle()
    {
        return "Fence";
    }
}