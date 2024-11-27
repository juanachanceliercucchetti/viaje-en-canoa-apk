using UnityEngine;

public class KayakController : MonoBehaviour
{
    public float moveSpeed = 5f;      // Velocidad de movimiento del kayak
    public float turnSpeed = 20f;     // Velocidad de giro del kayak
    public float slightForwardSpeed = 2f;  // Pequeño avance al girar
    public Vector3 controlOsc;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Captura el movimiento del kayak
        float moveInput = Input.GetAxis("Vertical");     // Entrada de avance (W/S o flechas arriba/abajo)
        float turnInput = Input.GetAxis("Horizontal");   // Entrada de giro (A/D o flechas izquierda/derecha)

        // Calcula el movimiento hacia adelante
        Vector3 forwardMovement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        // Aplica una leve fuerza de avance si hay giro
        if (turnInput != 0)
        {
            Vector3 slightForwardMovement = transform.forward * slightForwardSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + slightForwardMovement);
        }

        // Calcula y aplica la rotación del kayak
        float torque = turnInput * turnSpeed * Time.fixedDeltaTime;
        rb.AddTorque(0f, torque, 0f, ForceMode.VelocityChange); // Añadir torque sobre el eje Y
    }

    public void asignarVector(Vector3 v) {
        controlOsc = v;
    }
}
