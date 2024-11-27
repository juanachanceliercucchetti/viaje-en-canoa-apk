using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeightAdjuster : MonoBehaviour
{
    private void Start()
    {
        // Asegura que la c�mara de Cardboard empiece en una altura de 1.6 (o el valor deseado)
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 100.0f, Camera.main.transform.position.z);
    }
}
