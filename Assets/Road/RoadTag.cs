using UnityEngine;

public class AssignTagToChildren : MonoBehaviour
{
    public string tagName = "Road"; 

    void Start()
    {
        // Asignamos la tag al GameObject principal
        gameObject.tag = tagName;

        // Asignamos la tag a todos los hijos
        foreach (Transform child in transform)
        {
            child.gameObject.tag = tagName;
        }
    }
}
