using UnityEngine;

public class KayakController : MonoBehaviour
{
    public float moveSpeed = 5f;           // Velocidad de movimiento del kayak
    public float turnSpeed = 2f;           // Velocidad de giro del kayak
    public float brakeTorque = 2f;         // Factor de freno para reducir el giro residual suavemente
    public Vector3 controlOsc;             // Vector que almacena los datos OSC
    private Rigidbody rb;
    private AudioSource audioSource;       // Referencia al componente de AudioSource
    private float lastPosX;                // Para almacenar la última posición X

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();  // Obtén el componente de AudioSource
        rb.drag = 0.8f;                    // Drag ajustado para una inercia moderada en el avance
        rb.angularDrag = 2f;               // Angular drag ajustado para una inercia moderada en el giro
        lastPosX = transform.position.x;   // Guarda la posición inicial de X
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante basado en el eje X, sin moverse cuando X está entre -20 y 20
        float moveInput = 0f;

        if (controlOsc.x > 20)  // Avanza cuando el valor es mayor que 20
        {
            moveInput = (controlOsc.x - 20) / 30f;  // Normaliza el movimiento (avanzar)
        }
        else if (controlOsc.x < -20)  // Avanza cuando el valor es menor que -20
        {
            moveInput = (controlOsc.x + 20) / -30f;  // Normaliza el movimiento (avanzar)
        }

        // Movimiento hacia adelante
        Vector3 forwardMovement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        // Reproducir sonido si la posición X cambia
        if (Mathf.Abs(transform.position.x - lastPosX) > 0.01f)  // Verifica si ha cambiado la posición X
        {
            if (!audioSource.isPlaying)  // Reproduce el sonido solo si no está reproduciéndose
            {
                audioSource.Play();
            }
            lastPosX = transform.position.x;  // Actualiza la última posición X
        }

        // Rotación en el eje Y basada en el valor de controlOsc.z
        float turnInput = 0f;

        if (controlOsc.z >= -70 && controlOsc.z <= -20)  // Si Z está entre -70 y -20
        {
            turnInput = 80f;  // Rotación hacia la izquierda
        }
        else if (controlOsc.z >= 20 && controlOsc.z <= 70)  // Si Z está entre 20 y 70
        {
            turnInput = -80f;   // Rotación hacia la derecha
        }
        else
        {
            turnInput = 0f;    // Sin rotación si Z está fuera de los rangos anteriores
        }

        // Aplicar rotación en el eje Y
        if (turnInput != 0f)  // Si hay rotación significativa
        {
            float rotation = turnInput;  // Definir la rotación a aplicar
            Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotation, 0);
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSpeed));
        }
        else
        {
            // Desaceleración suave del giro cuando no hay entrada
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, brakeTorque * Time.fixedDeltaTime);
        }
    }

    // Método para asignar el vector de control OSC desde otro script
    public void asignarVector(Vector3 v) 
    {
        controlOsc = v;
    }
}
