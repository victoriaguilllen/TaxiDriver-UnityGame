using UnityEngine;


public abstract class SolidObstacle : Obstacle
{
    protected void Awake()
    {
        string typeOfObstacle = FindTypeOfObstacle(); 
        Initialize(typeOfObstacle, 20, 0.8f, 10, 100); 
    }

    // Método virtual para que las subclases definan su tipo
    protected virtual string FindTypeOfObstacle()
    {
        return "SolidObstacle"; // Tipo predeterminado
    }
}
