using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HistoryHerramientas.Cartas;
using TMPro;
using HistoryHerramientas.Cartas_Eventos;
using HistoryHerramientas.Enum;
using DG.Tweening;
//using System;

public class UIElegirEventos : UIManager
{
    //Esto es solo prueba
    public TMP_InputField carta1Evento;
    public TMP_InputField carta2Evento;            
    public TMP_InputField carta1Evento1;
    public TMP_InputField carta2Evento2;


    public Button botonSalir;
    public GameObject padre;
    public float speed;
    public RawImage rawImage;
    public GameObject uiPregunta;
    public Animator animator;

    public AudioSource audioRuleta;

    public AudioSource sonidosRuleta;

    public AudioClip clipBonus;
    public AudioClip clipTrampa;
    public AudioClip clipTiempo;
    public AudioClip clipPreguntas;
    public AudioClip clipAyuda;

    public GameObject ventana;

    public GameObject carta1;
    public GameObject carta2;

    public Button botonRuleta;


    public float tiempoEspera;

    public static Evento evento1;
    public static Evento evento2;

    int numeroRamdom;

    static Button botonCarta;
    static TMP_Text textoCarta;
    public static Animator animCarta;

    public int contadorEventos;

    int contador = 0;
    public static bool eventosActivados = false;
    public GameObject ruleta;
    [Header("Esto son Prueba")]
    public int idCarta;
    public int idEvento;

    private string yolo;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        contador = 0;

        descripciones = new List<string>(GameData.textosIdioma.dic["Descripciones"]);

        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = false;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        }
        if (animaciones.Count != 0) animaciones.Clear();

        if (GameSettings.CartasEventoBonus)
        {
            animaciones.Add(animcionesCopia[0]);
            contadorEventos++;
        }
        if (GameSettings.CartasEventoTrampa)
        {
            animaciones.Add(animcionesCopia[1]);
            contadorEventos++;
        }
        if (GameSettings.CartasEventoTiempo)
        {
            animaciones.Add(animcionesCopia[2]);
            contadorEventos++;
        }
        if (GameSettings.CartasEventoAyuda)
        {
            animaciones.Add(animcionesCopia[3]);
            contadorEventos++;
        }
        if (GameSettings.CartasEventoPregunta)
        {
            animaciones.Add(animcionesCopia[4]);
            contadorEventos++;
        }
        padre.transform.DOScale(new Vector3(1,1,0), 0.5f);
    }



    List<string> descripciones = new List<string>();

    List<string> animcionesCopia = new List<string>(5)
    {
        "Salio Bonus",
        "Salio Trampa",
        "Salio Tiempo",
        "Salio Ayuda",
        "Salio Pregunta"
    };

    List<string> animaciones = new List<string>();

    

    bool primera;
    public void ActivarRuleta()
    {

        ruleta.SetActive(true);
        //numeroRamdom = 0;
        //numeroRamdom = UnityEngine.Random.Range(0, animaciones.Count);
        //string anim = animaciones[numeroRamdom];
        //animator.Play(anim);
        yolo = carta1Evento.text;
        animator.Play(yolo);
    }


    public void ActivarBotonSalir()
    { 
        contador++;
        if(contador == 2)
        {
            this.transform.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() => botonSalir.interactable = true) ;
        }
    }


    public void SonidoBib()
    {
        audioRuleta.Play();
    }

    IEnumerator EsperarTiempo()
    {
        yield return new WaitForSeconds(tiempoEspera);
        ruleta.SetActive(false);
        primera = false;
        animator.SetBool("Segunda", primera);
        animator.Play("Reinicio Ruleta");
        botonRuleta.interactable = false;
    }


    public void VentanaEmergente1()
    {
        //sonidoVentanaEmergente.Play();
        ventana.transform.GetChild(1).GetComponent<TMP_Text>().text = evento1.GetDescripcion(descripciones);
        ventana.SetActive(true);
        animator.Play("EntradaVentana");
        ventana.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("evento_" + evento1.tipoEvento.ToString());
    }

    public void VentanaEmergente2()
    {
        //sonidoVentanaEmergente.Play();
        ventana.transform.GetChild(1).GetComponent<TMP_Text>().text = evento2.GetDescripcion(descripciones);
        ventana.SetActive(true);
        animator.Play("EntradaVentana");
        ventana.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("evento_" + evento2.tipoEvento.ToString());
    }

    //Se ejecuta cuando la animacion Salio esta funcionando
    public void SacarRuleta()
    {
        //TocarSonidoRuleta(animaciones[numeroRamdom]);
        //List<Evento> cartasEvento = GameData.cartasEvento[EscribirEvento(animaciones[numeroRamdom])];
        //animaciones.Remove(animaciones[numeroRamdom]);
        TocarSonidoRuleta(yolo);
        List<Evento> cartasEvento = GameData.cartasEvento[EscribirEvento(yolo)];
        animaciones.Remove(yolo);

        if (primera)
        {
            #region Prueba
            ////evento2 = cartasEvento[Random.Range(0, cartasEvento.Count)];
            //evento2 = cartasEvento[1];
            //carta2.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>(evento2.Atributos_.idImagenBack);
            //carta2.transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = evento2.descripcion;
            ////animaciones.Add("Salio Pregunta");
            //carta2.GetComponent<Animator>().Play("Carta1");
            //if (carta2.transform.GetChild(2).GetComponent<Button>().onClick != null)
            //{
            //    carta2.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            //}

            //if (evento2.tipoEvento == Tipo_Evento.Tiempo)
            //{
            //    SacarElementos(carta2.transform.GetChild(2).GetComponent<Button>(), carta2.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>(), carta2.GetComponent<Animator>());
            //    evento2.ActivarEvento();
            //}
            //else if (evento2.tipoEvento == Tipo_Evento.Pregunta)
            //{
            //    carta2.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            //    {
            //        uiPregunta.GetComponent<UIPregunta>().pregunta = evento2.descripcion;
            //        uiPregunta.GetComponent<UIPregunta>().timerInicial = GameSettings.TiempoMaxPreguntas;
            //        uiPregunta.SetActive(true);
            //    });
            //}
            //else
            //{
            //    if (evento2.tipoEvento == Tipo_Evento.Trampa || evento2.tipoEvento == Tipo_Evento.Bonus)
            //    {
            //        carta2.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            //        {
            //            evento2.ActivarEvento();
            //            eventosActivados = false;
            //        });
            //    }
            //    else
            //    {
            //        //carta2.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => evento2.ActivarEvento());
            //    }


            //}
            #endregion

            evento2 = GuardarEvento(carta2, cartasEvento, int.Parse(carta2Evento2.text));
            
            animaciones.Clear();
            animaciones.Add("Salio Bonus");
            animaciones.Add("Salio Trampa");
            animaciones.Add("Salio Tiempo");
            animaciones.Add("Salio Ayuda");
            animaciones.Add("Salio Pregunta");
            StartCoroutine(EsperarTiempo());

        }
        else if (primera == false)
        {
            #region Prueba
            ////int numero = Random.Range(0, cartasEvento.Count);
            //int numero = 1;
            //evento1 = cartasEvento[numero];
            ////Cargar Carta
            //carta1.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>(evento1.Atributos_.idImagenBack);
            //carta1.transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = evento1.descripcion;
            //carta1.GetComponent<Animator>().Play("Carta1");
            //if (carta1.transform.GetChild(2).GetComponent<Button>().onClick != null)
            //{
            //    carta1.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            //}

            //if (evento1.tipoEvento == Tipo_Evento.Tiempo)
            //{
            //    SacarElementos(carta1.transform.GetChild(2).GetComponent<Button>(), carta1.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>(), carta1.GetComponent<Animator>());
            //    evento1.ActivarEvento();
            //}
            //else if (evento1.tipoEvento == Tipo_Evento.Pregunta)
            //{
            //    carta1.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            //    {
            //        uiPregunta.GetComponent<UIPregunta>().pregunta = evento1.descripcion;
            //        uiPregunta.GetComponent<UIPregunta>().timerInicial = GameSettings.TiempoMaxPreguntas;
            //        uiPregunta.SetActive(true);
            //    });
            //}
            //else
            //{
            //    if (evento1.tipoEvento == Tipo_Evento.Trampa || evento1.tipoEvento == Tipo_Evento.Bonus)
            //    {
            //        carta1.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            //        {
            //            evento1.ActivarEvento();
            //            eventosActivados = true;
            //        });
            //    }

            //    //carta1.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => evento1.ActivarEvento());
            //}
            #endregion
            evento1 = GuardarEvento(carta1, cartasEvento, int.Parse(carta1Evento1.text));
        }
    }    

    private Evento GuardarEvento(GameObject carta, List<Evento> cartasEvento, int idCarta = -1)
    {
        //Creacion de la lista de las cartas de evento

        //Asignacion de la carta de evento 
        Evento evento = idCarta <= -1 ? cartasEvento[Random.Range(0, cartasEvento.Count)] : cartasEvento[idCarta];


        //Debug.Log(evento);
        carta.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>(evento.Atributos_.idImagenBack);
        carta.transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = evento.descripcion;
        carta.GetComponent<Animator>().Play("Carta1");

        //Quita todo evento antes de agregar
        if (carta.transform.GetChild(2).GetComponent<Button>().onClick != null)
        {
            carta.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        }

        //Verifica cada carta de evento

        switch (evento.tipoEvento)
        {
            case Tipo_Evento.Tiempo:
                SacarElementos(carta.transform.GetChild(2).GetComponent<Button>(), carta.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>(), carta.GetComponent<Animator>());
                evento.ActivarEvento();
                break;
            case Tipo_Evento.Pregunta:
                carta.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                {
                    uiPregunta.GetComponent<UIPregunta>().pregunta = evento.descripcion;
                    uiPregunta.GetComponent<UIPregunta>().timerInicial = GameSettings.TiempoMaxPreguntas;
                    uiPregunta.SetActive(true);
                });
                break;
            case Tipo_Evento.Ayuda:

                break;
            default:
                carta.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                {
                    evento.ActivarEvento();
                    eventosActivados = !eventosActivados;
                });
                break;
        }
        return evento;
        
    }

    private void SacarElementos(Button boton, TMP_Text texto, Animator anim)
    {
        botonCarta = boton;
        textoCarta = texto;
        animCarta = anim;
    }

    public static void ActivarTiempo(string text)
    {
        if (botonCarta.onClick != null)
        {
            botonCarta.onClick.RemoveAllListeners();
        }
        
        botonCarta.onClick.AddListener(() => {
            animCarta.Play("Carta_Tiempo");
            textoCarta.text = text;
        });
    }


    public void SacarVentanaEmergente()
    {
        ventana.SetActive(false);
    }

    private void TocarSonidoRuleta(string value)
    {
        switch (value)
        {

            case "Salio Bonus":
                sonidosRuleta.clip = clipBonus;
                sonidosRuleta.Play();
                break;
            case "Salio Trampa":
                sonidosRuleta.clip = clipTrampa;
                sonidosRuleta.Play();
                break;
            case "Salio Tiempo":
                sonidosRuleta.clip = clipTiempo;
                sonidosRuleta.Play();
                break;
            case "Salio Ayuda":
                sonidosRuleta.clip = clipAyuda;
                sonidosRuleta.Play();
                break;
            default:
                sonidosRuleta.clip = clipPreguntas;
                sonidosRuleta.Play();
                break;
        }
    }

    private string EscribirEvento(string value)
    {
        switch (value)
        {
            case "Salio Bonus":
                return "Bonus";
            case "Salio Trampa":
                return "Trampa";
            case "Salio Tiempo":
                return "Tiempo";
            case "Salio Ayuda": 
                return "Ayuda";
            default:
                return "Pregunta";
        } 
    }

    //Se ejecuta cuando la animacion reiniciar esta funcionando
    public void Primera()
    { 
        //numeroRamdom = animaciones.IndexOf("Salio Trampa");

        if(contadorEventos == 1)
        {
            int numero = 0;
            DOTween.To(() => numero, (x) => numero = x, 1, tiempoEspera).OnComplete(() =>
            {
                ruleta.SetActive(false);
                botonRuleta.interactable = false;
            });   
        }
        else
        {
            primera = true;
            animator.SetBool("Segunda", primera);
            //numeroRamdom = UnityEngine.Random.Range(0, animaciones.Count);
            //numeroRamdom = 2;
            yolo = carta2Evento.text;
            animator.Play(yolo);
            //animator.Play(animaciones[numeroRamdom]);
        }
    }

    public void DescativarEventos()
    {
        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = true;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = true;
        }

        padre.transform.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => padre.SetActive(false));
    }


    private void OnDisable()
    {
        botonRuleta.interactable = true;
        carta1.transform.GetChild(0).gameObject.SetActive(false);
        carta1.transform.GetChild(1).GetComponent<Button>().interactable = false;
        carta1.transform.GetChild(4).localScale = new Vector3(0, 0, 0);
        carta1.transform.GetChild(4).GetComponent<Image>().color = new Color(255, 255, 255, 0);

        carta2.transform.GetChild(0).gameObject.SetActive(false);
        carta2.transform.GetChild(1).GetComponent<Button>().interactable = false;
        carta2.transform.GetChild(4).localScale = new Vector3(0, 0, 0);
        carta2.transform.GetChild(4).GetComponent<Image>().color = new Color(255, 255, 255, 0);
        
    }

}
