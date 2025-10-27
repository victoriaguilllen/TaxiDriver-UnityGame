using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakener : Obstacle
{
    void Awake()
    {
        Initialize("Weakener", 0, 0.5f, 5, 75);
    }
}
