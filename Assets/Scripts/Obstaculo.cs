using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (gameManager.estado == 2)
        {
            return;
        }

        transform.position += Vector3.left * gameManager.velocidad * Time.deltaTime;

        if (transform.position.x < -13f)
        {
            Destroy(gameObject);
        }
    }
}
