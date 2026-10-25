using UnityEngine;

public class PelotaPinPong : MonoBehaviour
{
    public float velocidadInicial = 10f;
    public float aumentoPorRebote = 1f;   
    public float velocidadMaxima = 22f;  

    [Header("Seguridad Contra Bucles")]
    [Tooltip("La velocidad horizontal mínima en X para que no se quede rebotando recto arriba y abajo")]
    public float velocidadHorizontalMinima = 1.5f;

    private float velocidadActual;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LanzarPelota();
    }

    public void LanzarPelota()
    {
        velocidadActual = velocidadInicial;

        // Al sacar, siempre le damos una diagonal en X para que NO empiece en línea recta
        float dirX = Random.Range(0, 2) == 0 ? -2f : 2f; // Forzamos a que tenga movimiento de lado
        float dirY = Random.Range(0, 2) == 0 ? -1f : 1f;

        Vector2 direccion = new Vector2(dirX, dirY).normalized;
        rb.linearVelocity = direccion * velocidadActual; // Nota: o rb.velocity si estás en Unity 2022 o anterior
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        // 1. Aumentamos la velocidad en cada rebote
        velocidadActual = Mathf.Min(velocidadActual + aumentoPorRebote, velocidadMaxima);

        // 2. Comportamiento especial al chocar con las PALETAS (Jugadores)
        // Para que esto funcione, tus dos paletas deben tener el Tag "Player" en el Inspector
        if (colision.gameObject.CompareTag("Player"))
        {
            // Calculamos la distancia entre el centro de la paleta y el impacto de la pelota
            float posicionImpactoX = transform.position.x;
            float posicionPaletaX = colision.transform.position.x;
            
            // El ancho estimado de tu paleta (ajusta esto según lo grande que sea la paleta)
            float anchoPaleta = colision.collider.bounds.size.x; 

            // Determina la dirección del rebote (si pega a la derecha, sale hacia la derecha, etc.)
            float factorDeAngulo = (posicionImpactoX - posicionPaletaX) / (anchoPaleta / 2);

            // Dirección hacia arriba (si pegó en la de abajo) o hacia abajo (si pegó en la de arriba)
            float direccionY = (transform.position.y > colision.transform.position.y) ? 1f : -1f;

            // Creamos la nueva dirección con el ángulo inclinado
            Vector2 nuevaDireccion = new Vector2(factorDeAngulo * 1.5f, direccionY).normalized;
            rb.linearVelocity = nuevaDireccion * velocidadActual;
            return; // Nos saltamos el resto del cálculo porque ya hicimos el rebote de la paleta
        }

        // 3. Rebote contra los muros normales
        rb.linearVelocity = rb.linearVelocity.normalized * velocidadActual;

        // --- EL SEGURO CONTRA EL BUCLE INFINITO ---
        // Si después de cualquier rebote, la pelota casi no se mueve de lado (X muy cercana a 0)
        if (Mathf.Abs(rb.linearVelocity.x) < velocidadHorizontalMinima)
        {
            // Le damos un pequeño empujón a la izquierda o derecha al azar
            float empujonX = (rb.linearVelocity.x >= 0) ? velocidadHorizontalMinima : -velocidadHorizontalMinima;
            rb.linearVelocity = new Vector2(empujonX, rb.linearVelocity.y).normalized * velocidadActual;
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("ZonaMuerte"))
        {
            Debug.Log("¡Punto! Reiniciando pelota...");

            rb.linearVelocity = Vector2.zero;
            // Ojo: el centro es 2.15f como calculamos antes debido a tus muros asimétricos
            transform.position = new Vector3(2.15f, 0f, 0f); 
            Invoke(nameof(LanzarPelota), 1f);
        }
    }
}
