using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto = 7f;
    public float techo = 5.3f;
    public Sprite alasArriba;
    public Sprite alasAbajo;
    public AudioClip sonidoSalto;
    public AudioClip sonidoGolpe;
    public AudioClip sonidoCaida;
    public GameManager gameManager;

    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource audioSource;
    float tiempoAleteo;
    float alturaInicial;
    bool vivo = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        alturaInicial = transform.position.y;
        rb.simulated = false;
    }

    void Update()
    {
        if (!vivo)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90f), 8f * Time.deltaTime);
            return;
        }

        if (gameManager.estado == 0)
        {
            transform.position = new Vector3(transform.position.x, alturaInicial + Mathf.Sin(Time.time * 4f) * 0.25f, 0);
            sr.sprite = Mathf.Sin(Time.time * 12f) > 0 ? alasArriba : alasAbajo;

            if (GameManager.Presiono())
            {
                gameManager.Comenzar();
                rb.simulated = true;
                Saltar();
            }

            return;
        }

        if (GameManager.Presiono())
        {
            Saltar();
        }

        if (transform.position.y > techo)
        {
            transform.position = new Vector3(transform.position.x, techo, 0);
            rb.linearVelocity = new Vector2(0, Mathf.Min(rb.linearVelocity.y, 0));
        }

        tiempoAleteo -= Time.deltaTime;
        sr.sprite = tiempoAleteo > 0 ? alasAbajo : alasArriba;

        float angulo = Mathf.Clamp(rb.linearVelocity.y * 5f, -80f, 30f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angulo), 10f * Time.deltaTime);
    }

    void Saltar()
    {
        rb.linearVelocity = new Vector2(0, fuerzaSalto);
        tiempoAleteo = 0.15f;
        audioSource.PlayOneShot(sonidoSalto, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (!vivo)
        {
            return;
        }

        vivo = false;
        sr.sprite = alasAbajo;
        audioSource.PlayOneShot(sonidoGolpe, 0.8f);
        audioSource.PlayOneShot(sonidoCaida, 0.5f);
        rb.linearVelocity = new Vector2(0, 2f);
        GetComponent<Collider2D>().enabled = false;
        rb.gravityScale *= 1.5f;
        Destroy(gameObject, 3f);
        gameManager.Perder();
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (vivo && otro.name == "Punto")
        {
            gameManager.SumarPunto();
        }
    }
}
