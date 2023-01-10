using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using History.Jugadores;
using History.Enum;
using HistoryHerramientas;
using HistoryHerramientas.Cartas;
using TMPro;
using System;
using DG.Tweening;
using HistoryHerramientas.Cartas_Eventos;

public static class GameData
{
    public static int turno = 0;

    public static string materia = ""; //Esto es una simulacion

    public static Color32 ColorMarco { 
        get 
        {
            if(materia == "Guerra Fria" /* poner en ingles*/)
            {
                return new Color32(250, 110, 33, 255);
            }
            else if(materia == "Genetica") //Genetica Poner en ingles
            {
                return new Color32(134, 181, 66, 255);
            }
            else if (materia == "Genero Narrativo") //Genetica Poner en ingles
            {
                return new Color32(80, 180, 200, 255);
            }
            else if (materia == "Pueblos Precolombinos") //Genetica Poner en ingles
            {
                return new Color32(250, 110, 33, 255);
            }
            else if(materia == "Aventura")//Genetica Poner en ingles
            {
                return new Color32(253, 198, 82, 255);
            }
            else //Genero No Literario
            {
                return new Color32(80, 180, 200, 255);
            }

        } 
    }

    public static BDCartas data = null;

    public static List<Jugador> jugadores = new List<Jugador>(); //Esto es una prueba (BORRAME)
    public static List<Expositor> expositores = new List<Expositor>() {
        //new Expositor("1", Resources.Load<Sprite>("evento_Tiempo"), Tipo_Jugador.Expositor),
        //new Expositor("2", Resources.Load<Sprite>("evento_Tiempo"), Tipo_Jugador.Expositor),
        //new Expositor("3", Resources.Load<Sprite>("evento_Tiempo"), Tipo_Jugador.Expositor)
    };
    public static int MaximoExpositores { get { return expositores.Count; } }

    public static GranMaster granMaster = new GranMaster();

    public static Expositor JugadorActivo { get { 
            return expositores[turno]; 
    } }

    public static List<Concepto> conceptosMateria = new List<Concepto>();

    public static Dictionary<string, List<Evento>> cartasEvento = new Dictionary<string, List<Evento>>();

    public static DictionaryJson textosIdioma = new DictionaryJson();

    //public static void ColocarJugadores(List<Jugador> jugadores)
    //{
    //    foreach (var jugador in jugadores)
    //    {
    //        if(jugador.tipoJugador == History.Enum.Tipo_Jugador.Expositor)
    //        {
    //            expositores.Add((Expositor) jugador);
    //        }
    //        else
    //        {
    //            granMaster = (GranMaster) jugador;
    //        }
    //    }
    //}

    //Solo se llama al inicio del juego (Partida)
    public static void ColocarConceptosYPreguntas()
    {
        foreach (var item in data.Conceptos)
        {
            Debug.Log(item.Key);    
        }

        conceptosMateria = new List<Concepto>(data.Conceptos[materia]);           
        //cartasEvento["Pregunta"] = new List<Evento>(data.Preguntas[materia]);
    }


    public static void ListTextFade(this List<TMP_Text> textos, int alfa, float duracion)
    {
        foreach (var item in textos)
        {
            item.DOFade(alfa, duracion);
        }
    }
    public static string GetDescripcion(this Evento evento, List<string> descripciones)
    {

        switch (evento.tipoEvento)
        {
            case HistoryHerramientas.Enum.Tipo_Evento.Tiempo:
                return descripciones[2];
            case HistoryHerramientas.Enum.Tipo_Evento.Bonus:
                return descripciones[0];
            case HistoryHerramientas.Enum.Tipo_Evento.Trampa:
                return descripciones[1];
            case HistoryHerramientas.Enum.Tipo_Evento.Pregunta:
                return descripciones[4];
            default:
                return descripciones[3];
        }

    }
}                                     
