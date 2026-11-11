using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int estado;
    public float velocidad = 3f;
    public Text textoPuntos;
    public Text textoRecordInicio;
    public Text textoFinal;
    public GameObject panelInicio;
    public GameObject panelFin;
    public Generador generador;
    public AudioClip sonidoPunto;

    int puntos;
    int record;
    float tiempoFin;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        record = PlayerPrefs.GetInt("recordFlappy", 0);
        estado = 0;
        puntos = 0;
        textoPuntos.text = "0";
        textoPuntos.gameObject.SetActive(false);
        textoRecordInicio.text = "Record: " + record;
        panelInicio.SetActive(true);
        panelFin.SetActive(false);
        generador.enabled = false;
    }

    void Update()
    {
        if (estado == 2 && Time.time > tiempoFin + 0.8f && Presiono())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    public static bool Presiono()
    {
        bool teclado = Keyboard.current != null && (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame);
        bool raton = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        bool toque = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
        return teclado || raton || toque;
    }

    public void Comenzar()
    {
        estado = 1;
        panelInicio.SetActive(false);
        textoPuntos.gameObject.SetActive(true);
        generador.enabled = true;
    }

    public void SumarPunto()
    {
        if (estado != 1)
        {
            return;
        }

        puntos++;
        textoPuntos.text = puntos.ToString();
        velocidad = Mathf.Min(3f + puntos * 0.06f, 5f);
        audioSource.PlayOneShot(sonidoPunto, 0.6f);
    }

    public void Perder()
    {
        if (estado == 2)
        {
            return;
        }

        estado = 2;
        tiempoFin = Time.time;
        generador.enabled = false;

        bool nuevoRecord = puntos > record;

        if (nuevoRecord)
        {
            record = puntos;
            PlayerPrefs.SetInt("recordFlappy", record);
            PlayerPrefs.Save();
        }

        textoPuntos.gameObject.SetActive(false);
        textoFinal.text = "Puntos: " + puntos + "\nRecord: " + record + (nuevoRecord && puntos > 0 ? "\n¡Nuevo record!" : "");
        panelFin.SetActive(true);
    }
}
