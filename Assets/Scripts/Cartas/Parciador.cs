using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Newtonsoft.Json;
using HistoryHerramientas;
using HistoryHerramientas.Cartas_Eventos;
using HistoryHerramientas.Cartas;
using History.Enum;
using TMPro;
using DG.Tweening;
using History.Jugadores;

[CreateAssetMenu(fileName = "Parciador", menuName = "Parciador")]
public class Parciador : ScriptableObject 
{
    [TextArea(0, 50)]
    public string fromJson = "";
    public void ParciadorJson()
    {
        if(GameData.data != null)
        {
            GameData.data = new BDCartas();
            GameData.cartasEvento.Clear();
            GameData.conceptosMateria.Clear();
        }

        fromJson = GameSettings.JsonCartas();
        GameData.data = JsonConvert.DeserializeObject<BDCartas>(fromJson);

        foreach (var cartas in GameData.data.Eventos)
        {
            List<Evento> cartasEvento = new List<Evento>();

            foreach (var evento in cartas.Value)
            {
                //No va pregunta por que se cambia en tiempo de ejecucion
                switch (evento.tipoEvento)
                {
                    case HistoryHerramientas.Enum.Tipo_Evento.Tiempo: // COUNT = 0 
                        cartasEvento.Add(evento);
                        GuardarMetodosTiempo(evento,cartasEvento.Count);
                        break;
                    case HistoryHerramientas.Enum.Tipo_Evento.Bonus: // COUNT = 5 
                        cartasEvento.Add(evento);
                        GuardarMetodosBonus(evento, cartasEvento.Count);
                        break;
                    case HistoryHerramientas.Enum.Tipo_Evento.Trampa:
                        cartasEvento.Add(evento);
                        GuardarMetodosTrampa(evento, cartasEvento.Count); 
                        break;
                    case HistoryHerramientas.Enum.Tipo_Evento.Ayuda:
                        cartasEvento.Add(evento);
                        break;
                }
            }
            GameData.cartasEvento.Add(cartas.Key, cartasEvento);


        }
    }

    #region Trampa

    private void GuardarMetodosTrampa(Evento evento, int id)
    {
        switch (id)
        {
            case 1: //Esto debe cambiar
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarTrampa(UITrampa.uiTrampa, UITrampa.trampa, Tablero.vectorTerminoTurno, pendulo: true);
                }); //Pendulo
                break;
            case 2:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarTrampa(UITrampa.uiTrampa, UITrampa.trampa, Tablero.vectorTerminoTurno, false, false);
                }); //Concepto Reduce a la mitad
                break;
            case 3:

                evento.ColocarFuncionalidad(() =>
                {
                    ActivarTrampa(UITrampa.uiTrampa, UITrampa.trampa, Tablero.vectorTerminoTurno, false, true);
                });   //Concepto Se Vuelve 0 
                break;
            case 4:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarTrampa(UITrampa.uiTrampa, UITrampa.trampa, Tablero.vectorTerminoTurno, true, false);
                });  //Puente Reduce a la mitad 
                break;
            case 5:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarTrampa(UITrampa.uiTrampa, UITrampa.trampa, Tablero.vectorTerminoTurno, true, true);
                });   //Puente Se Vuelve 0 
                break;                               
        }
    }

    public void ActivarTrampa(GameObject uiTrampa, UITrampa trampa,UnityEvent [] evento_, bool puente = false, bool reinicioPuntaje = false, bool pendulo = false)
    {
        UnityEvent evento = new UnityEvent(); 


        if (pendulo)
        {
            //Pendulo
            #region Evento 1
            uiTrampa.transform.GetChild(2).gameObject.SetActive(false); //Concepto y puente
            uiTrampa.transform.GetChild(1).gameObject.SetActive(true); //Concepto o puente

            //evento.RemoveAllListeners(); //Borra el cambio de jugador
            trampa.reiniciarPuntaje = false;

            evento.AddListener(() => {
                uiTrampa.SetActive(true);
                uiTrampa.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() =>
                {
                    bool si = Random.Range(0, 2) == 0 ? false : true; // true == Concepto | false == Puente  
                    if (si) //Conceptos
                    {
                        uiTrampa.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][0];
                        uiTrampa.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                        int mayorPuntaje = GameData.JugadorActivo.GetMayorPuntaje(TipoTema.Concepto);
                        for (int i = 0; i < uiTrampa.transform.GetChild(1).childCount - 1; i++)
                        {
                            uiTrampa.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                            if (i < GameData.JugadorActivo.conceptos.Count)
                            {
                                uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.conceptos[i];
                                if (mayorPuntaje == GameData.JugadorActivo.GetPuntaje(GameData.JugadorActivo.conceptos[i]))
                                {
                                    uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                    continue;
                                }
                            }
                            uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;

                        }
                    }
                    else //Puentes
                    {
                        int mayorPuntaje = GameData.JugadorActivo.GetMayorPuntaje(TipoTema.Puente);
                        uiTrampa.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][1];
                        uiTrampa.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                        for (int i = 0; i < uiTrampa.transform.GetChild(1).childCount; i++)
                        {
                            uiTrampa.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                            if (i < GameData.JugadorActivo.puentes.Count)
                            {

                                uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puentes[i];
                                if (mayorPuntaje == GameData.JugadorActivo.GetPuntaje(GameData.JugadorActivo.puentes[i]))
                                {
                                    uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                    continue;
                                }
                            }
                            uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                        }

                    }

                    uiTrampa.transform.GetChild(2).GetComponent<AnimacionPendulo>().activado = si;

                    uiTrampa.transform.GetChild(2).gameObject.SetActive(true); //Pendulo

                });
            });

            #endregion
        }
        else
        {
            //Conceptos
            #region Evento 2 y 3 

            if (puente == false)
            {
                uiTrampa.transform.GetChild(2).gameObject.SetActive(false); //Concepto y puente
                uiTrampa.transform.GetChild(1).gameObject.SetActive(true); //Concepto o puente

                //evento.RemoveAllListeners(); //Borra el cambio de jugador
                trampa.reiniciarPuntaje = reinicioPuntaje; //Si es el puntaje total o no

                evento.AddListener(() =>
                {
                    uiTrampa.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][0];
                    uiTrampa.SetActive(true);
                    uiTrampa.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() =>
                    {
                        uiTrampa.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                        uiTrampa.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                        int mayorPuntaje = GameData.JugadorActivo.GetMayorPuntaje(TipoTema.Concepto);
                        for (int i = 0; i < uiTrampa.transform.GetChild(1).childCount - 1; i++)
                        {
                            uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                            uiTrampa.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                            if (i < GameData.JugadorActivo.conceptos.Count)
                            {
                                uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.conceptos[i];
                                if (mayorPuntaje == GameData.JugadorActivo.GetPuntaje(GameData.JugadorActivo.conceptos[i]))
                                {
                                    uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                    continue;
                                }
                            }
                            uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                        }
                    });
                });
                for (int i = 0; i < evento_.Length - 1; i++)
                {
                    if (evento_[i] == null)
                    {
                        evento_[i] = evento;
                        Debug.Log("En el indice: " + i + " Trampa");
                        return;
                    }
                }
                return;
            }

            #endregion

            //Puentes
            #region Evento 4 y 5
            uiTrampa.transform.GetChild(2).gameObject.SetActive(false); //Concepto y puente
            uiTrampa.transform.GetChild(1).gameObject.SetActive(true); //Concepto o puente

            //evento.RemoveAllListeners(); //Borra el cambio de jugador
            trampa.reiniciarPuntaje = reinicioPuntaje;

            evento.AddListener(() =>
            {

                uiTrampa.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][1];
                uiTrampa.SetActive(true);
                uiTrampa.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() =>
                {
                    uiTrampa.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                    uiTrampa.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                    int mayorPuntaje = GameData.JugadorActivo.GetMayorPuntaje(TipoTema.Puente);
                    for (int i = 0; i < uiTrampa.transform.GetChild(1).childCount - 1; i++)
                    {
                        uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                        uiTrampa.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                        if (i < GameData.JugadorActivo.puentes.Count)
                        {

                            uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puentes[i];
                            if (mayorPuntaje == GameData.JugadorActivo.GetPuntaje(GameData.JugadorActivo.puentes[i]))
                            {
                                uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                continue;
                            }
                        }
                        uiTrampa.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                    }

                });
            });
            #endregion
        }

        for (int i = 0; i < evento_.Length - 1; i++)
        {
            if (evento_[i] == null)
            {
                evento_[i] = evento;
                Debug.Log("En el indice: " + i + " Trampa");
                return;
            }
        }
    }
    #endregion

    #region Bonus
    private void GuardarMetodosBonus(Evento evento, int id)
    {
        switch (id)
        {

            case 1: //Esto debe cambiar
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarBonus(UIBonus.uiBonus, UIBonus.bonus, Tablero.vectorTerminoTurno, pendulo: true);
                }); //Pendulo
                break;
            case 2:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarBonus(UIBonus.uiBonus, UIBonus.bonus, Tablero.vectorTerminoTurno, false, false);
                });  //Concepto puntaje total
                break;
            case 3:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarBonus(UIBonus.uiBonus, UIBonus.bonus, Tablero.vectorTerminoTurno, false, true);
                }); //Concepto Doble del puntaje
                break;
            case 4:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarBonus(UIBonus.uiBonus, UIBonus.bonus, Tablero.vectorTerminoTurno, true, true);
                }); //Puente puntaje total
                break;
            case 5:
                evento.ColocarFuncionalidad(() =>
                {
                    ActivarBonus(UIBonus.uiBonus, UIBonus.bonus, Tablero.vectorTerminoTurno, true, false);
                }); //Puente Doble del puntaje
                break;
        }
    }

    private void ActivarBonus(GameObject uiBonus, UIBonus bonus, UnityEvent[] evento_, bool puente = false, bool puntajeTotal = false, bool pendulo = false)
    {
        UnityEvent evento = new UnityEvent();

        if (pendulo)
        {
            //Pendulo
            #region Evento 1
            uiBonus.transform.GetChild(1).gameObject.SetActive(true); //Concepto o puente

            //evento.RemoveAllListeners(); //Borra el cambio de jugador
            bonus.puntajeTotal = true;

            evento.AddListener(() => {
                uiBonus.SetActive(true);
                uiBonus.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(()=>
                {
                    bool si = Random.Range(0, 2) == 0 ? false : true; // true == Concepto | false == Puente

                    if (si) //Conceptos
                    {
                        uiBonus.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][0];
                        uiBonus.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                        for (int i = 0; i < uiBonus.transform.GetChild(1).childCount - 1; i++)
                        {
                            uiBonus.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                            if (i < GameData.JugadorActivo.conceptos.Count)
                            {
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.conceptos[i];
                            }
                            else
                            {
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                            }
                        }

                    }
                    else //Puentes
                    {
                        uiBonus.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][1];
                        uiBonus.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                        for (int i = 0; i < uiBonus.transform.GetChild(1).childCount - 1; i++)
                        {
                            uiBonus.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                            if (i < GameData.JugadorActivo.puentes.Count)
                            {
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puentes[i];
                            }
                            else
                            {
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                            }

                            //uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().DOFade(1, 0.3f);

                        }
                    }

                    uiBonus.transform.GetChild(2).GetComponent<AnimacionPendulo>().activado = si;

                    uiBonus.transform.GetChild(2).gameObject.SetActive(true); //Pendulo
                    
                });
            });

            #endregion
        }
        else
        {
            //Conceptos
            #region Evento 2 y 3 

            if(puente == false)
            {
                uiBonus.transform.GetChild(2).gameObject.SetActive(false); //Concepto y puente
                uiBonus.transform.GetChild(1).gameObject.SetActive(true); //Concepto o puente

                //evento.RemoveAllListeners(); //Borra el cambio de jugador
                bonus.puntajeTotal = puntajeTotal; //Si es el puntaje total o no

                evento.AddListener(() => {
                    uiBonus.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][0];
                    uiBonus.SetActive(true);
                    uiBonus.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() =>
                    {
                        uiBonus.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                        uiBonus.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                        for (int i = 0; i < uiBonus.transform.GetChild(1).childCount - 1; i++)
                        {
                            if (i < GameData.JugadorActivo.conceptos.Count)
                            {
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.conceptos[i];
                            }
                            else
                            {
                                uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                            }
                            uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                            uiBonus.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1,1,0), 0.5f);
                        }
                    });
                });
                for (int i = 0; i < evento_.Length - 1; i++)
                {
                    if (evento_[i] == null)
                    {
                        evento_[i] = evento;
                        Debug.Log("En el indice: " + i + " Bonus");
                        return;
                    }
                }
                return;
            }

            #endregion

            //Puentes
            #region Evento 4 y 5
            uiBonus.transform.GetChild(2).gameObject.SetActive(false); //Concepto y puente
            uiBonus.transform.GetChild(1).gameObject.SetActive(true); //Concepto o puente

            //evento.RemoveAllListeners(); //Borra el cambio de jugador
            bonus.puntajeTotal = puntajeTotal;

            evento.AddListener(() => {
                uiBonus.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = GameData.textosIdioma.dic["Temas"][1];
                uiBonus.SetActive(true);
                uiBonus.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(() =>
                {
                    uiBonus.transform.GetChild(1).GetChild(6).transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                    uiBonus.transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                    for (int i = 0; i < uiBonus.transform.GetChild(1).childCount - 1; i++)
                    {
                        if (i < GameData.JugadorActivo.puentes.Count)
                        {
                            uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = true;
                            uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().text = GameData.JugadorActivo.puentes[i];
                        }
                        else
                        {
                            uiBonus.transform.GetChild(1).GetChild(i).GetComponent<Button>().interactable = false;
                        }

                        uiBonus.transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().DOFade(1, 0.5f);
                        uiBonus.transform.GetChild(1).GetChild(i).transform.DOScale(new Vector3(1, 1, 0), 0.5f);

                    }

                });
            });
            #endregion
        }

        for (int i = 0; i < evento_.Length - 1; i++)
        {
            if (evento_[i] == null)
            {
                evento_[i] = evento;
                
                return;
            }
        }
    }
    #endregion

    #region Tiempo
    private void GuardarMetodosTiempo(Evento cartaTiempo ,int id)
    {
        switch (id)
        { 
            case 1:
                cartaTiempo.ColocarFuncionalidad(() => ActivarTiempo(UIElegirEventos.animCarta, GameData.JugadorActivo, 30, true, TipoTema.Puente)); 
                break;
            case 2:
                cartaTiempo.ColocarFuncionalidad(() => ActivarTiempo(UIElegirEventos.animCarta, GameData.JugadorActivo, 10, true, todos: true));
                break;
            case 3:
                cartaTiempo.ColocarFuncionalidad(() => ActivarTiempo(UIElegirEventos.animCarta, GameData.JugadorActivo, 10, jugadores: GameData.expositores));
                break;
            case 4:
                cartaTiempo.ColocarFuncionalidad(() => ActivarTiempo(UIElegirEventos.animCarta, GameData.JugadorActivo, jugadores: GameData.expositores, reinicio: true));
                break;
            case 5:
                cartaTiempo.ColocarFuncionalidad(() => ActivarTiempo(UIElegirEventos.animCarta, GameData.JugadorActivo, 30, true));
                break;
        }
    }
    private void ActivarTiempo(Animator anim, Expositor jugadorActivo, float tiempo = 0, bool sumar = false, 
        TipoTema tema = TipoTema.Concepto, bool todos = false, List<Expositor> jugadores = null, bool reinicio = false)
    {

        string tiempoText = "";
        if (reinicio != true)
        {
            if (jugadores != null)
            { 
                //Quita 10 segundos a todos los jugadores (1 evento)
                //int id = jugadores.IndexOf(jugadorActivo);
                tiempo *= GameData.MaximoExpositores;

                for (int i = 0; i < jugadores.Count; i++)
                {
                    jugadores[i].CambioTiempoExposicion(tiempo, false, TipoTema.Concepto);
                    jugadores[i].CambioTiempoExposicion(tiempo, false, TipoTema.Puente);
                }
                tiempoText = "-" + tiempo +"s.";
            }
            else
            {
                if (todos == false)
                {
                    jugadorActivo.CambioTiempoExposicion(tiempo, sumar, tema);   //Suma 30 segundo alguna expocicion (1)
                }
                else
                {
                    //Suma de todas las expociciones (2)
                    jugadorActivo.CambioTiempoExposicion(tiempo, sumar, TipoTema.Concepto);
                    jugadorActivo.CambioTiempoExposicion(tiempo, sumar, TipoTema.Puente);
                }

                tiempoText = "+" + tiempo + "s.";

            }
        }
        else
        {
            //Cambia los tiempos a 30 segundos
            tiempoText = "= 30";
            jugadorActivo.ReducirTiempo();
        }

        UIElegirEventos.ActivarTiempo(tiempoText);

    }
    #endregion
}
