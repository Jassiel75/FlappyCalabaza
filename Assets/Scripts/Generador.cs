using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject obstaculoPrefab;
    public GameManager gameManager;
    public float distancia = 5.5f;
    public float alturaMinima = -1f;
    public float alturaMaxima = 3f;

    float tiempo;

    void OnEnable()
    {
        tiempo = 0.5f;
    }

    void Update()
    {
        tiempo -= Time.deltaTime;

        if (tiempo <= 0)
        {
            tiempo = distancia / gameManager.velocidad;
            Vector3 posicion = new Vector3(transform.position.x, Random.Range(alturaMinima, alturaMaxima), 0);
            GameObject obstaculo = Instantiate(obstaculoPrefab, posicion, Quaternion.identity);
            obstaculo.GetComponent<Obstaculo>().gameManager = gameManager;
        }
    }
}
