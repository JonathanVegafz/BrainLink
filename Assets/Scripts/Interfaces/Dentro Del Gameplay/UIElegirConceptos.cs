using System.Collections;
using System.Collections.Generic;
using HistoryHerramientas.Cartas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using History.Jugadores;

public class UIElegirConceptos : UIManager
{
    public GameObject carta;

    public AudioSource audio_;
    public AudioSource audioCirculo;
    public AudioSource audioCuadrado;
    public AudioSource audioTriangulo;
    public float tiempo;
    public Animator animator;
    public Button botonSeguir;


    public Image imagenSombra;
    public Sprite[] sombras = new Sprite[3];

    public Image cuadradoSelector;
    public Image figura;
    public TMP_Text conceptoText;

    private static Concepto conceptoActivado = null;
    private static CartaConcepto cartaConcepto;
    private static GameObject gameConcepto;

    public void ActivarAnimacion()
    {
        string[] nombresAnimacion = new string[3]
        {
            "SalioAzul",
            "SalioAmarillo",
            "SalioRojo"
        };

        animator.Play(nombresAnimacion[Random.Range(0, nombresAnimacion.Length)]);

    }

    public void DesactivarBoton()
    {
        gameConcepto.transform.GetChild(1).GetComponent<Image>().DOFade(0.3f, 0.2f);
        gameConcepto.transform.GetChild(1).GetChild(0).GetComponent<Image>().DOFade(0.3f,0.2f);
        gameConcepto.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().DOFade(0.3f, 0.2f);
        gameConcepto.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().DOFade(0.3f, 0.2f);
        gameConcepto.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<TMP_Text>().DOFade(0.3f, 0.2f);
        gameConcepto.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().DOFade(0.3f, 0.2f);
        cartaConcepto.cartaDesactivada = true;
        this.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(()=> gameObject.SetActive(false));

        imagenSombra.DOFade(0, 0.5f); //Problema a la hora de activar de nuevo la carta
    }

    public void UIFade()
    {
        this.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(()=> ActivarAnimacion());
        cuadradoSelector.color = Color.white;
        conceptoText.text = "";
    }

    public void OnEnable()
    {
        carta.name = conceptoActivado.Atributos_.idImagen; //El nombre del GameObject
        carta.GetComponent<Image>().sprite = Resources.Load<Sprite>(conceptoActivado.Atributos_.idImagen); // Imagen del frente
        carta.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Marco_" + conceptoActivado.materia); // El marco
        carta.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = conceptoActivado.conceptos[0]; //Concepto 1
        carta.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = conceptoActivado.conceptos[1]; //Concepto 2
        carta.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = conceptoActivado.conceptos[2]; //Concepto 3
        carta.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Icono_" + conceptoActivado.materia); //Icono
        cartaConcepto.Salida();

        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = false;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        }

        //ActivarAnimacion();

    }
    private void OnDisable()
    {
        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = true;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = true;
        }
    }

    public void SeleccionarAzul()
    {
        audioTriangulo.Play();
        cuadradoSelector.color = new Color32(34, 140, 168, 255);
        botonSeguir.interactable = true;
        conceptoText.text = conceptoActivado.conceptos[0];
        imagenSombra.sprite = sombras[2];
        imagenSombra.DOFade(0.4f, 0.3f);

        GameData.JugadorActivo.imagenesConceptos.Add(Resources.Load<Sprite>(conceptoActivado.Atributos_.idImagen));
        GameData.JugadorActivo.sombras.Add(sombras[2]);
        GameData.JugadorActivo.conceptos.Add(conceptoActivado.conceptos[0]);

    }

    public void SeleccionarAmarillo()
    {
        audioCuadrado.Play();
        cuadradoSelector.color = new Color32(255, 160, 21, 255);
        botonSeguir.interactable = true;
        conceptoText.text = conceptoActivado.conceptos[1];
        imagenSombra.sprite = sombras[1];
        imagenSombra.DOFade(0.4f, 0.3f);

        GameData.JugadorActivo.imagenesConceptos.Add(Resources.Load<Sprite>(conceptoActivado.Atributos_.idImagen));
        GameData.JugadorActivo.sombras.Add(sombras[1]);
        GameData.JugadorActivo.conceptos.Add(conceptoActivado.conceptos[1]);
    }

    public void SonarBib()
    {
        audio_.Play();
    }

    public void SeleccionarRojo()
    {
        audioCirculo.Play();
        cuadradoSelector.color = new Color32(206, 33, 33, 255);
        botonSeguir.interactable = true;
        conceptoText.text = conceptoActivado.conceptos[2];
        imagenSombra.sprite = sombras[0];
        imagenSombra.DOFade(0.4f, 0.3f);

        GameData.JugadorActivo.imagenesConceptos.Add(Resources.Load<Sprite>(conceptoActivado.Atributos_.idImagen));
        GameData.JugadorActivo.sombras.Add(sombras[0]);
        GameData.JugadorActivo.conceptos.Add(conceptoActivado.conceptos[2]);
    }

    public static void ColocarConcepto(Concepto concepto, CartaConcepto cartaConcepto_, GameObject gameConcepto_)
    {
        conceptoActivado = concepto;
        cartaConcepto = cartaConcepto_;
        gameConcepto = gameConcepto_;

    }
}
