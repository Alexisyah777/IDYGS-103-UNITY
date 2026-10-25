using UnityEngine;

public class VueloNave : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerza = 5f;
    private SpriteRenderer spriteRenderer; 
    private Rigidbody2D rigidbody2D;

    void Start()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
         rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {

            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
            rigidbody2D.AddForce(Vector2.up * fuerza);
        }
       
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
            rigidbody2D.AddForce(Vector2.down * fuerza);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
            rigidbody2D.AddForce(Vector2.right * fuerza);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
            rigidbody2D.AddForce(Vector2.left * fuerza);

        }
    }
}
