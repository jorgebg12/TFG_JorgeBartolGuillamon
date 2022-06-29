using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceptor : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material completeMaterial;
    Material defaulMaterial;

    [HideInInspector] public bool isCompleted = false;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        defaulMaterial = Resources.Load<Material>("Materials/defaultLightReceptor");
        completeMaterial = Resources.Load<Material>("Materials/raycastCollision");
    }
    public void CompletedPuzzle()
    {
        //Cambiar el color para ver que se ha completado
        meshRenderer.material = completeMaterial;
        //Que no se pueda mover las cosas

        //Desbloquear salida y cargar siguiente nivel
        //EventManager.OnCompleteLevel();
        isCompleted = true;
        EventManager.OnReceptorHit();
    }
}
