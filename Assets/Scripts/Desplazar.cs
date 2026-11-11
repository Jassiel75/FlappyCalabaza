using UnityEngine;

public class Desplazar : MonoBehaviour
{
    public float factor = 1f;
    public float ancho = 20f;
    public GameManager gameManager;

    void Update()
    {
        if (gameManager.estado == 2)
        {
            return;
        }

        transform.position += Vector3.left * gameManager.velocidad * factor * Time.deltaTime;

        if (transform.position.x <= -ancho)
        {
            transform.position += Vector3.right * ancho * 2f;
        }
    }
}
