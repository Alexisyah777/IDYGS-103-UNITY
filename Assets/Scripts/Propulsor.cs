using UnityEngine;

public class Propulsor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

        void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            spriteRenderer.color = Color.red; 
        }
       
        if (Input.GetKey(KeyCode.DownArrow))
        {
            spriteRenderer.color = Color.yellow; 
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.color = Color.orange; 
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            spriteRenderer.color = Color.orange;
        }
    }
}
