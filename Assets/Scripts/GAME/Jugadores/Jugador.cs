using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using History.Enum;


namespace History.Jugadores
{
    [System.Serializable]
    public class Jugador
    {
        public string nombre = "";
        public Tipo_Jugador tipoJugador = Tipo_Jugador.Expositor;
        public Sprite avatar;

        public Jugador()
        {

        }

        public Jugador(string nombre, Sprite avatar, Tipo_Jugador tipoJugador)
        {
            this.nombre = nombre;
            this.avatar = avatar;
            this.tipoJugador = tipoJugador;
        }

        public Jugador(string nombre, Sprite avatar)
        {
            this.nombre = nombre;
            this.avatar = avatar;
        }
    }
}
