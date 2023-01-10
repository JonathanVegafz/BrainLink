using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictionaryJson
{
    public Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

}

public static class GameSettings
{
    //Configuraciones Partida
    private static float tiempoMaxPreguntas = 10f;
    private static float tiempoMaxAyuda;
    private static float tiempoMaxInicialConceptos;
    private static float tiempoMaxInicialPuentes;
    private static float tiempoMaxDarPutajes;
    private static float tiempoMaxPorJugador;
    private static float tiempoMaxPorInterfas;
    private static int cartasIniciales = 2;
    private static bool jugadoresRamdom;
    private static bool cartasEventos = true;
    private static bool cartasEventoTiempo;
    private static bool cartasEventoTrampa = true;
    private static bool cartasEventoBonus = true;
    private static bool cartasEventoAyuda;
    private static bool cartasEventoPregunta;

    //Configuraciones Juego
    public static bool spanish = false;
    private static string jsonIdioma = "";

    public static DictionaryJson JsonIdioma(bool spanish_) 
    {
        spanish = spanish_;  
        if(spanish)
        {
            jsonIdioma = Resources.Load<TextAsset>("Json Español").text;
        }
        else
        {
            jsonIdioma = Resources.Load<TextAsset>("Json Ingles").text;
        }
        return JsonConvert.DeserializeObject<DictionaryJson>(jsonIdioma); 
    }

    public static string JsonCartas()
    {
        if (spanish)
        {
            return Resources.Load<TextAsset>("Cartas Español").text;
        }
        else
        {
            return Resources.Load<TextAsset>("Cartas Ingles").text;
        }

        
    }



    public static float TiempoMaxPreguntas { get { return tiempoMaxPreguntas; } } // El tiempo que pone en las preguntas (Cartas prguntas)
    public static float TiempoMaxAyuda { get { return tiempoMaxAyuda; } } // El tiempo que pone en la Ayuda (Cartas Ayuda)
    public static float TiempoMaxInicialConceptos { get { return tiempoMaxInicialConceptos; } }
    public static float TiempoMaxInicialPuentes { get { return tiempoMaxInicialPuentes; } }
    public static float TiempoMaxDarPutajes { get { return tiempoMaxDarPutajes; } } //Gran master Juega
    public static float TiempoMaxPorJugador { get { return tiempoMaxPorJugador; } } // El tiempo para el jugador
    public static float TiempoMaxPorInterfas { get { return tiempoMaxPorInterfas; } } // El tiempo por intefas
    public static int CartasIniciales { get { return cartasIniciales; } } // 2 A 6
    public static bool JugadoresRamdom { get { return jugadoresRamdom; } }
    public static bool CartasEventos { get { return cartasEventos; } } // S
    public static bool CartasEventoTiempo { get { return cartasEventoTiempo; } }
    public static bool CartasEventoTrampa { get { return cartasEventoTrampa; } }
    public static bool CartasEventoBonus { get { return cartasEventoBonus; } }
    public static bool CartasEventoAyuda { get { return cartasEventoAyuda; } }
    public static bool CartasEventoPregunta { get { return cartasEventoPregunta; } }

    public static void ModificarParametros(
            float tiempoMaxPreguntas,
            float tiempoMaxAyuda,
            float tiempoMaxInicialConceptos,
            float tiempoMaxInicialPuentes,
            float tiempoMaxDarPutajes,
            float tiempoMaxPorJugador,
            float tiempoMaxPorInterfas,
            int cartasIniciales,
            bool jugadoresRamdom,
            bool cartasEventos,
            bool cartasEventoTiempo,
            bool cartasEventoTrampa,
            bool cartasEventoBonus,
            bool cartasEventoAyuda,
            bool cartasEventoPregunta
        )
    {
        GameSettings.tiempoMaxPreguntas = tiempoMaxPreguntas;
        GameSettings.tiempoMaxAyuda = tiempoMaxAyuda;
        GameSettings.tiempoMaxInicialConceptos = tiempoMaxInicialConceptos;
        GameSettings.tiempoMaxInicialPuentes = tiempoMaxInicialPuentes;
        GameSettings.tiempoMaxDarPutajes = tiempoMaxDarPutajes;
        GameSettings.tiempoMaxPorJugador = tiempoMaxPorJugador;
        GameSettings.tiempoMaxPorInterfas = tiempoMaxPorInterfas;
        GameSettings.cartasIniciales = cartasIniciales;
        GameSettings.jugadoresRamdom = jugadoresRamdom;
        GameSettings.cartasEventos = cartasEventos;
        GameSettings.cartasEventoTiempo = cartasEventoTiempo;
        GameSettings.cartasEventoTrampa = cartasEventoTrampa;
        GameSettings.cartasEventoBonus = cartasEventoBonus;
        GameSettings.cartasEventoAyuda = cartasEventoAyuda;
        GameSettings.cartasEventoPregunta = cartasEventoPregunta;
    }

    public static void DesactivarEvento()
    {
        cartasEventos = false;   
    }


    public static void ReiniciarParametros()
    {
        tiempoMaxPreguntas = 0;
        tiempoMaxAyuda = 0;
        tiempoMaxInicialConceptos = 0;
        tiempoMaxInicialPuentes = 0;
        tiempoMaxDarPutajes = 0;
        tiempoMaxPorJugador = 0;
        tiempoMaxPorInterfas = 0;
        cartasIniciales = 0;
        jugadoresRamdom = false;
        cartasEventos = false;
        cartasEventoTiempo = false;
        cartasEventoTrampa = false;
        cartasEventoBonus = false;
        cartasEventoAyuda = false;
        cartasEventoPregunta = false;
    }
}
