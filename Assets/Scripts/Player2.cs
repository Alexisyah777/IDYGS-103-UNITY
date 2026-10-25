using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float velocidad = 5f;
    public float limiteIzquierdo = -6.600141f; 
    public float limiteDerecho = 10.91146f;

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        }

        // Usamos localPosition para que coincida 100% con los números de tu Inspector
        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, limiteIzquierdo, limiteDerecho);
        transform.localPosition = pos;
    }
}
