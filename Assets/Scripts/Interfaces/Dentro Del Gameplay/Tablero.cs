using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using History.Jugadores;
using HistoryHerramientas.Cartas;
using TMPro;

public class Tablero : MonoBehaviour
{
    public List<GameObject> jugadores = new List<GameObject>();

    public Image imagenFondo;
    public bool avatarActivado = false;
    public AudioSource sonidoCambioJugador;
    public AudioSource sonidoCarta;
    public float time;
    public int turno;
    public GameObject prefabCarta;
    public GameObject botonPausa;
    public GameObject cartaEventos;
    public GameObject elegirConcepto;
    public Button botonEvento;
    public GameObject UIpuntaje;
    public Expositor expositor;
    public List<Expositor> expositores;
    public GameObject botonExponer;

    public static UnityEvent OnTerminoTurno = new UnityEvent();
    public static UnityEvent [] vectorTerminoTurno = new UnityEvent[3];

    [System.Serializable]
    public class ReferenciaJugadores
    {
        public Transform datosJugador;
        public Transform padre;
        public Transform dondeTieneIr;
    }
    public List<ReferenciaJugadores> rJugadores = new List<ReferenciaJugadores>();

    public List<Image> imagenesMarco = new List<Image>();


    public Transform mazo;
    public Canvas canvas_;
    public List<Concepto> conceptos = new List<Concepto>();
    public int indice;

    public int contadorClicks;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        foreach (var imagen in imagenesMarco)
        {
            imagen.color = GameData.ColorMarco;
        }

        if (OnTerminoTurno != null)
        {
            OnTerminoTurno.RemoveAllListeners();

            //Prueba
            for (int i = 0; i < vectorTerminoTurno.Length - 1; i++)
            {
                vectorTerminoTurno[i] = null;
            }
        }

        imagenFondo.sprite = Resources.Load<Sprite>("fondo_" + GameData.materia);
        for (int i = 0; i < GameData.expositores.Count; i++)
        {
            if (avatarActivado) //Esto solo para pruebas
            {
                rJugadores[i].datosJugador.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameData.expositores[i].avatar;
            }
            rJugadores[i].datosJugador.GetChild(1).GetComponent<TMP_Text>().text = GameData.expositores[i].nombre;
            rJugadores[i].datosJugador.GetChild(2).GetComponent<TMP_Text>().text = "0 Pts.";
            jugadores[i].SetActive(true);
        }

        conceptos = new List<Concepto>(GameData.conceptosMateria);
        expositores = GameData.expositores;
        int cotadorEventos = 0;

        if (GameSettings.CartasEventos)
        {
            cartaEventos.transform.DOScale(new Vector3(1, 1, 0), time);

            if (GameSettings.CartasEventoBonus)
            {
                cartaEventos.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                cotadorEventos++;
            }

            if (GameSettings.CartasEventoAyuda)
            {
                cartaEventos.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                cotadorEventos++;
            }

            if (GameSettings.CartasEventoPregunta)
            {
                cartaEventos.transform.GetChild(2).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                cotadorEventos++;
            }

            if (GameSettings.CartasEventoTiempo)
            {
                cartaEventos.transform.GetChild(3).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                cotadorEventos++;
            }

            if (GameSettings.CartasEventoTrampa)
            {
                cartaEventos.transform.GetChild(4).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                cotadorEventos++;
            }
        }
        if(GameSettings.CartasEventos == false || cotadorEventos == 0)
        {
            cartaEventos.SetActive(false);
            GameSettings.DesactivarEvento();

        }

        UITrampa.OnTrampa.AddListener(CambioJugador);
        UIBonus.OnBonus.AddListener(CambioJugador);
        vectorTerminoTurno[vectorTerminoTurno.Length - 1] = new UnityEvent();
        vectorTerminoTurno[vectorTerminoTurno.Length - 1].AddListener(CambioJugador);
        OnTerminoTurno.AddListener(CambioJugador);
        StartCoroutine(RepartirCarta());
    }


    // Update is called once per frame
    void Update()  
    {

        print(vectorTerminoTurno[2]);

        // Esto debo cambiarlo
        if (turno == 1)
        {
            rJugadores[2].datosJugador.GetChild(2).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puntajes.puntajeTotal + " Pts.";
        }
        else if(turno == 2)
        {
            rJugadores[1].datosJugador.GetChild(2).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puntajes.puntajeTotal + " Pts.";
        }
        else
        {
            rJugadores[0].datosJugador.GetChild(2).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puntajes.puntajeTotal + " Pts.";
        }
        
    }

    public void ActivarExposicion()
    {
        botonExponer.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() => botonExponer.GetComponent<Button>().interactable = true);
    }

    public void ActivarBotonEvento()
    {
        if(contadorClicks == GameSettings.CartasIniciales)
        {
            if (GameSettings.CartasEventos == false)
            {
                botonExponer.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() => botonExponer.GetComponent<Button>().interactable = true);
                return;
            }
            
            botonEvento.interactable = true;
        }
    }

    private void CambioJugador()
    {
        sonidoCambioJugador.Play();
        if (turno == GameData.MaximoExpositores - 1)
        {
            UIpuntaje.SetActive(true);
            UIpuntaje.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
            return;
        }

        for (int iN = 0; iN < GameData.MaximoExpositores; iN++)
        {
            int i = iN % GameData.MaximoExpositores;

            rJugadores[i].datosJugador.DOScale(new Vector3(0, 0, 0), 0.5f);
            rJugadores[i].padre.DOScale(new Vector3(0, 0, 0), 0.5f);                                                                           
        }

        //UITrampa.OnTrampa.RemoveAllListeners();
        //UITrampa.OnTrampa.AddListener(CambioJugador);

        contadorClicks = 0;

        //UIBonus.OnBonus.RemoveAllListeners();
        //UIBonus.OnBonus.AddListener(CambioJugador);

        Invoke("CambioJugadores", 0.5f);
    }

    public void CambioJugadores()
    {
        botonExponer.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        botonEvento.interactable = false;
        OnTerminoTurno.RemoveAllListeners();
        Vector3 temDatosP = rJugadores[0].datosJugador.position;
        Quaternion temDatosR = rJugadores[0].datosJugador.rotation;

        Vector3 temPadreP = rJugadores[0].padre.position;
        Quaternion temPadreR = rJugadores[0].padre.rotation;

        expositor = GameData.JugadorActivo;

        foreach (var carta in expositor.cartasMano)
        {
            carta.transform.GetChild(0).gameObject.SetActive(true);
            carta.transform.GetChild(1).gameObject.SetActive(false);
            carta.GetComponent<CartaConcepto>().cartaDesactivada = true;
        }

        foreach (var carta in GameData.expositores[turno + 1].cartasMano)
        {
            carta.transform.GetChild(0).gameObject.SetActive(false);
            carta.transform.GetChild(1).gameObject.SetActive(true);
            carta.GetComponent<CartaConcepto>().activarCarta = true;
        }

        for (int iN = 0; iN < GameData.MaximoExpositores; iN++)
        {
            int i = iN % GameData.MaximoExpositores;
            if (i != GameData.MaximoExpositores - 1)
            {
                rJugadores[i].datosJugador.position = rJugadores[i + 1].datosJugador.position;
                rJugadores[i].datosJugador.rotation = rJugadores[i + 1].datosJugador.rotation;
                rJugadores[i].padre.position = rJugadores[i + 1].padre.position;
                rJugadores[i].padre.rotation = rJugadores[i + 1].padre.rotation;
            }
            else
            {
                //Debug.Log("Ocurre");

                rJugadores[i].datosJugador.position = temDatosP;
                rJugadores[i].datosJugador.rotation = temDatosR;
                rJugadores[i].padre.position = temPadreP;
                rJugadores[i].padre.rotation = temPadreR;
            }
            rJugadores[i].datosJugador.DOScale(new Vector3(1, 1, 0), 0.5f);
            rJugadores[i].padre.DOScale(new Vector3(1, 1, 0), 0.5f);
        }

        vectorTerminoTurno[vectorTerminoTurno.Length - 1] = new UnityEvent();
        vectorTerminoTurno[vectorTerminoTurno.Length - 1].AddListener(CambioJugador);
        GameData.turno++;
        turno++;
    }

    IEnumerator RepartirCarta()
    {
        yield return new WaitForSeconds(0.7f);
        for (int iJugador = 0; iJugador < GameData.expositores.Count; iJugador++)
        {
            for (int i = 0; i < GameSettings.CartasIniciales; i++)
            {
                GameObject carta = Instantiate(prefabCarta, mazo.position, mazo.rotation, canvas_.transform);
                int numeroRandom = UnityEngine.Random.Range(0, conceptos.Count);
                Concepto concepto = conceptos[numeroRandom];
                carta.name = concepto.Atributos_.idImagen; //El nombre del GameObject
                carta.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(concepto.Atributos_.idImagen); // Imagen del frente
                carta.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Marco_" + concepto.materia); // El marco
                carta.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = concepto.conceptos[0]; //Concepto 1
                carta.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = concepto.conceptos[1]; //Concepto 2
                carta.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = concepto.conceptos[2]; //Concepto 3
                carta.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Icono_" + concepto.materia); //Icono
                carta.GetComponent<CartaConcepto>().cartaConcepto = concepto;
                carta.GetComponent<CartaConcepto>().OnClick.AddListener(() =>
                {
                    elegirConcepto.SetActive(true);
                    elegirConcepto.GetComponent<UIElegirConceptos>().UIFade();
                    contadorClicks++;
                });
                conceptos.Remove(concepto);
                if (iJugador == 1)
                {
                    carta.transform.DOLocalRotate(new Vector3(0, 0, -90), time);
                }
                else if (iJugador == 2)
                {
                    carta.transform.DOLocalRotate(new Vector3(0, 0, 90), time);
                }

                carta.transform.DOMove(rJugadores[iJugador].dondeTieneIr.position, time).OnComplete(() =>
                {
                    rJugadores[iJugador].dondeTieneIr = carta.transform;
                    carta.transform.parent = rJugadores[iJugador].padre;
                    GameData.expositores[iJugador].cartasMano.Add(carta);
                });
                indice = iJugador;
                sonidoCarta.Play();
                yield return new WaitForSeconds(0.2f); // Por carta
            }
            yield return new WaitForSeconds(0.1f);  // Por Jugador
        }

        for (int i = 0; i < GameData.JugadorActivo.cartasMano.Count; i++) // Da Vuelta la carta
        {
            GameObject carta = GameData.JugadorActivo.cartasMano[i];
            carta.GetComponent<Animation>().Play("CartaConcepto");
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.8f);

        foreach (var carta in GameData.JugadorActivo.cartasMano) // Espera un poco para poder tocar las cartas
        {
            carta.GetComponent<CartaConcepto>().activarCarta = true;
        }

        botonPausa.GetComponent<Button>().interactable = true;

        if (GameData.expositores.Count == 3)
        {
            Expositor tem = GameData.expositores[1];
            GameData.expositores[1] = GameData.expositores[2];
            GameData.expositores[2] = tem;
        }
        //Esto es para darlo vuelta por que da un Bugs si no se pone asi jejeje

    }
}
