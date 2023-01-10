using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using HistoryHerramientas.Enum;
using History.Enum;
using History.Jugadores;
using System;

public class UIConfiguracionesPartida : UIManager
{
    #region VariablesPublicas
    public AudioSource sonidoMenu;
    public List<GameObject> botonesEventos = new List<GameObject>();
    public List<GameObject> sliderTiempo = new List<GameObject>();

    [System.Serializable]
    public struct ReferenciaMaxCartas
    {
        public Sprite imagenSeleccionado;
        public Sprite imagenDeseleccionado;
        public TMP_Text cantidadCartas;
        public List<GameObject> botonesCartas;
    }
    public ReferenciaMaxCartas referencia;
    #endregion

    #region Variables Privadas
    private bool jugadoresRamdom = false;

    public bool eventosActivados = false;
    private bool eventoTiempo = false;
    private bool eventoTrampa = false;
    private bool eventoBonus = false;
    private bool eventoAyuda = false;
    private bool eventoPregunta = false;
    private int cartasMax = 2;

    private float tiempoAyuda = 60f;
    private float tiempoPregunta = 60f;
    private float tiempoConcepto = 60f;
    private float tiempoPuente = 60f;
    private float tiempoDarPuntajes = 60f;

    private float tiempoInterfaz;
    private float tiempoMaxJugador;
    #endregion                       

    #region Tiempos
    public void TiempoIntefaz(TMP_InputField inputField)
    {
        tiempoInterfaz = float.Parse(inputField.text == "" ? "0" : inputField.text);
    }
    public void TiempoJugador(TMP_InputField inputField)
    {
        tiempoMaxJugador = float.Parse(inputField.text == "" ? "0" : inputField.text);
    }
    #endregion

    #region Otros
    public void MaxCartas(int numero)
    {
        referencia.cantidadCartas.text = numero.ToString();
        cartasMax = numero;
        numero -= 2;

        for (int i = 0; i < referencia.botonesCartas.Count; i++)
        {
            if (i <= numero)
            {
                referencia.botonesCartas[i].GetComponent<Image>().sprite = referencia.imagenSeleccionado;
            }
            else
            {
                referencia.botonesCartas[i].GetComponent<Image>().sprite = referencia.imagenDeseleccionado;
            }
        }
    }

    public void JugarConJugadoresRamdom(Image imagen)
    {

        jugadoresRamdom = !jugadoresRamdom;

        imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, jugadoresRamdom == true ? 255 : 0);
    }
    #endregion

    #region Cartas Eventos
    public void UsarCartasEvetos(Image imagen)
    {
        //base.PrecinarBoton(eventosActivados, imagenBoton);
        eventosActivados = !eventosActivados;

        imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, eventosActivados == true ? 255 : 0);


        foreach (var eventos in botonesEventos)
        {
            base.ActivarDesactivarBoton(eventos.GetComponent<Image>(), eventosActivados);
                                                                          
            eventos.GetComponent<Button>().interactable = eventosActivados;
        }

        eventoAyuda = true;
        eventoBonus = true;
        eventoPregunta = true;
        eventoTiempo = true;
        eventoTrampa = true;

    }

    public void UsarCartaEvento(GameObject boton)
    {
        Tipo_Evento tipoEvento = (Tipo_Evento)System.Enum.Parse(typeof(Tipo_Evento), boton.name);

        if (eventosActivados)
        {
            switch (tipoEvento)
            {
                case Tipo_Evento.Tiempo:
                    eventoTiempo = !eventoTiempo;
                    boton.GetComponent<Image>().color = new Color(
                            boton.GetComponent<Image>().color.r,
                            boton.GetComponent<Image>().color.g,
                            boton.GetComponent<Image>().color.b,
                            eventoTiempo == true ? 255 : 0
                        );
                    if (eventoTiempo == true) boton.GetComponent<AudioSource>().Play();
                    else sonidoMenu.Play(); 
                    break;
                case Tipo_Evento.Bonus:
                    eventoBonus = !eventoBonus;
                    boton.GetComponent<Image>().color = new Color(
                            boton.GetComponent<Image>().color.r,
                            boton.GetComponent<Image>().color.g,
                            boton.GetComponent<Image>().color.b,
                            eventoBonus == true ? 255 : 0
                        );
                    if (eventoBonus == true) boton.GetComponent<AudioSource>().Play();
                    else sonidoMenu.Play();
                    break;
                case Tipo_Evento.Trampa:
                    eventoTrampa = !eventoTrampa;
                    boton.GetComponent<Image>().color = new Color(
                            boton.GetComponent<Image>().color.r,
                            boton.GetComponent<Image>().color.g,
                            boton.GetComponent<Image>().color.b,
                            eventoTrampa == true ? 255 : 0
                        );
                    if (eventoTrampa == true) boton.GetComponent<AudioSource>().Play();
                    else sonidoMenu.Play();
                    break;
                case Tipo_Evento.Pregunta:
                    eventoPregunta = !eventoPregunta;
                    boton.GetComponent<Image>().color = new Color(
                            boton.GetComponent<Image>().color.r,
                            boton.GetComponent<Image>().color.g,
                            boton.GetComponent<Image>().color.b,
                            eventoPregunta == true ? 255 : 0
                        );
                    if (eventoPregunta == true) boton.GetComponent<AudioSource>().Play();
                    else sonidoMenu.Play();
                    break;
                case Tipo_Evento.Ayuda:
                    eventoAyuda = !eventoAyuda;
                    boton.GetComponent<Image>().color = new Color(
                            boton.GetComponent<Image>().color.r,
                            boton.GetComponent<Image>().color.g,
                            boton.GetComponent<Image>().color.b,
                            eventoAyuda == true ? 255 : 0
                        );
                    if (eventoAyuda == true) boton.GetComponent<AudioSource>().Play();
                    else sonidoMenu.Play();
                    break;
            }
        }
    }
    #endregion

    #region Barra de Timpo

    public void TiempoConceptos(TMP_Text timerText)
    {
        tiempoConcepto = sliderTiempo[0].GetComponent<Slider>().value;
        BarraTiempo(tiempoConcepto, timerText);
    }

    public void TiempoDarPuntajes(TMP_Text timerText)
    {
        tiempoDarPuntajes = sliderTiempo[2].GetComponent<Slider>().value;
        BarraTiempo(tiempoDarPuntajes, timerText);
    }

    public void TiempoPuentes(TMP_Text timerText)
    {
        tiempoPuente = sliderTiempo[1].GetComponent<Slider>().value;
        BarraTiempo(tiempoPuente, timerText);
    }

    public void TiempoPregunta(TMP_Text timerText)
    {
        tiempoPregunta = sliderTiempo[3].GetComponent<Slider>().value;
        BarraTiempo(tiempoPregunta, timerText);
    }
    public void TiempoAyuda(TMP_Text timerText)
    {
        tiempoAyuda = sliderTiempo[4].GetComponent<Slider>().value;
        BarraTiempo(tiempoAyuda, timerText);
    }
    #endregion

    #region Comienzo Partida
    public void ComenzarPartida()
    {
        #region Esto ira mas adelante
        //if (jugadoresRamdom == true)
        //{
        //    GameData.jugadores = PonerLugares(GameData.jugadores);
        //}
        //else
        //{
        //    GameData.jugadores[3].tipoJugador = Tipo_Jugador.GranMaster;
        //}
        #endregion

        GuardarConfiguraciones();

        GameData.turno = 0;

        GameData.jugadores[GameData.jugadores.Count - 1].tipoJugador = Tipo_Jugador.GranMaster;

        UnityAction<int,bool> GuardarExpositores = (i, estado) =>
        {
            Expositor expositor = new Expositor(
               GameData.jugadores[i].nombre,
               GameData.jugadores[i].avatar,
               estado == false ? Tipo_Jugador.Expositor : Tipo_Jugador.Expositor_GranMaster
           ) ;
            GameData.expositores.Add(expositor);
        };

        if (GameData.jugadores.Count == 1)
        {
            GuardarExpositores(0, true);
            return;
        }
        print(GameData.jugadores.Count);
        for (int i = 0; i < GameData.jugadores.Count - 1; i++)
        {
            if (GameData.jugadores[i].tipoJugador != Tipo_Jugador.GranMaster)
                GuardarExpositores(i, false);
        }

        
    }
    private List<Jugador> PonerLugares(List<Jugador> jugadores)
    {
        List<Jugador> jugador = new List<Jugador>(jugadores);
        jugador.Clear();

        List<Jugador> copia = new List<Jugador>();

        int inice = 0;

        do
        {
            inice = UnityEngine.Random.Range(0, jugador.Count);

            copia.Add(jugador[inice]);
            jugador.RemoveAt(inice);
        } while (jugador.Count != 0);

        return copia;
    }

    private void GuardarConfiguraciones()
    {
        GameSettings.ModificarParametros(
                tiempoPregunta,
                tiempoAyuda,
                tiempoConcepto,
                tiempoPuente,
                tiempoDarPuntajes,
                tiempoMaxJugador,
                tiempoInterfaz,
                cartasMax,
                jugadoresRamdom,
                eventosActivados,
                eventoTiempo,
                eventoTrampa,
                eventoBonus,
                eventoAyuda,
                eventoPregunta
            );
    }
    #endregion
}

