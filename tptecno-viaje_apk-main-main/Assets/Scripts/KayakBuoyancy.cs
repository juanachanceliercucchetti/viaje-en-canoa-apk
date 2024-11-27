using UnityEngine;

public class KayakBuoyancy : MonoBehaviour
{
    public float buoyancyForce = 30f; // Aumenta la fuerza de flotación
    public float waterLevel = 10f; // Nivel del agua

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Comprobamos si el kayak está debajo del nivel del agua
        if (transform.position.y < waterLevel)
        {
            // Calculamos la profundidad
            float depth = waterLevel - transform.position.y;
            float force = buoyancyForce * depth; // Fuerza de flotación proporcional a la profundidad
            rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
        }
    }
}