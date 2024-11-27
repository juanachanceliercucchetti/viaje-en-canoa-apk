using UnityEngine;

public class CameraInitializer : MonoBehaviour
{
    private void Start()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        // Cuando se ejecuta en un dispositivo Android (APK), inicializa la posición Y de la cámara en 35
        transform.position = new Vector3(transform.position.x, 35.0f, transform.position.z);
        #endif
    }
}