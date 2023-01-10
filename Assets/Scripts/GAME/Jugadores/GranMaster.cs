using History.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace History.Jugadores
{
    public class GranMaster : Jugador
    {
        public struct TiemposGranMaster
        {
            public float tiempoMaxPuntaje;
            public float tiempoAyuda;
        }
        public TiemposGranMaster tiemposGranMaster = new TiemposGranMaster();

        public GranMaster()
        {
            
        }

        public GranMaster(string nombre, Sprite avatar, Tipo_Jugador tipoJugador) : base(nombre, avatar, tipoJugador)
        {
            tiemposGranMaster.tiempoMaxPuntaje = GameSettings.TiempoMaxDarPutajes; //Esto debe definirce desde la clase configuraciones de partida
            tiemposGranMaster.tiempoAyuda = GameSettings.TiempoMaxAyuda; //Esto debe definirce desde la clase configuraciones de partida 
        }
    }
}
