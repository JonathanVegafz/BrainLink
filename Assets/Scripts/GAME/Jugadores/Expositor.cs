using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using History.Enum;


namespace History.Jugadores
{

    [System.Serializable]
    public class Expositor : Jugador
    {
        public int id;
        public int puntajeTotal = 0;

        public List<string> conceptos = new List<string>();
        public List<string> puentes = new List<string>();
        public List<Sprite> sombras = new List<Sprite>();
        public List<Sprite> imagenesConceptos = new List<Sprite>();
        public Puntajes puntajes = new Puntajes();
        public List<GameObject> cartasMano = new List<GameObject>(); 

        public TiempoExposicion[] tiempoExposicion = new TiempoExposicion[2]
        {
            new TiempoExposicion(),
            new TiempoExposicion()
        };

        public Expositor ()
        {

        }

        public Expositor(string nombre, Sprite avatar, Tipo_Jugador tipoJugador) : base(nombre, avatar, tipoJugador)
        {
            //Debug.Log("TiempoConcepto: " + GameSettings.TiempoMaxInicialConceptos);
            tiempoExposicion[0].tiempoTotal = GameSettings.TiempoMaxInicialConceptos; //Esto debe definirce desde la clase configuraciones de partida
            tiempoExposicion[0].tipoTema = TipoTema.Concepto;
            tiempoExposicion[1].tiempoTotal = GameSettings.TiempoMaxInicialPuentes; //Esto debe definirce desde la clase configuraciones de partida
            tiempoExposicion[1].tipoTema = TipoTema.Puente;
        }

        public void SetConceptos(string[] conceptos)
        {
            this.conceptos[0] = conceptos[0];
            this.conceptos[1] = conceptos[1];
            this.conceptos[2] = conceptos[2];
        }

        public void ReducirTiempo()
        {
            tiempoExposicion[0].tiempoTotal = 30; //Esto debe definirce desde la clase configuraciones de partida
            tiempoExposicion[1].tiempoTotal = 30; //Esto debe definirce desde la clase configuraciones de partida
        }

        //Sirve para las cartas de Tiempo
        public void CambioTiempoExposicion(float tiempo, bool aumentarTiempo, TipoTema tipoTema)
        {
            int id = tipoTema == TipoTema.Concepto ? 0 : 1;

            if (aumentarTiempo)
            {
                tiempoExposicion[id].tiempoTotal += tiempo;
            }
            else
            {
                tiempoExposicion[id].tiempoTotal -= tiempo;
                Debug.Log("Tiempo:" + tiempoExposicion[id].tiempoTotal + "-" + tipoTema);
            }
        }

        public int GetMayorPuntaje(TipoTema tipoTema)
        {

            List<Puntajes.Temas> temasAntiguos = new List<Puntajes.Temas>(puntajes.temas.Values);
            List<Puntajes.Temas> temas = new List<Puntajes.Temas>(temasAntiguos.FindAll((x)=> x.tipoTema == tipoTema));

            List<int> numerosPuntajes = new List<int>();

            foreach (var item in temas)
            {
                numerosPuntajes.Add(item.puntaje);
            }
            numerosPuntajes.Sort();
            numerosPuntajes.Reverse();


            int numeroMayor = numerosPuntajes[0];
            return numeroMayor;

        }

        public int GetPuntaje(string key)
        {

            return puntajes.temas[key].puntaje;           
        }

        public void AñadirPuntaje(string concepto, int puntaje, TipoTema tipoTema)
        {
            puntajeTotal += puntaje;
            puntajes.AñadirPuntaje(concepto, puntaje, tipoTema);
        }

        //Esto es para los bonus o trampa
        public void CambioPuntaje(string key, int nuevoPuntaje)
        {
            puntajes.puntajeTotal -= puntajes.temas[key].puntaje;
            puntajes.puntajeTotal += nuevoPuntaje;

            puntajes.temas[key].puntaje = nuevoPuntaje;
        }
    }
    public struct TiempoExposicion
    {
        public TipoTema tipoTema;
        public float tiempoTotal;
    }

     
    [System.Serializable]
    public class Puntajes
    {
        public int puntajeTotal = 0;

        [System.Serializable]
        public class Temas
        {
            public int puntaje = 0;
            public TipoTema tipoTema = TipoTema.Concepto;

            public Temas(int puntaje, TipoTema tipoTema)
            {
                this.puntaje = puntaje;
                this.tipoTema = tipoTema;
            }
        }
        public List<Temas> ListaTemas = new List<Temas>();

        public Dictionary<string, Temas> temas = new Dictionary<string, Temas>();


        public void AñadirPuntaje(string concepto, int puntaje, TipoTema tipoTema)
        {

            if (tipoTema == TipoTema.Puente)
            {
                temas.Add(concepto, new Temas(puntaje, tipoTema));
            }
            else
            {
                temas.Add(concepto, new Temas(puntaje, tipoTema));
            }

            puntajeTotal += puntaje;
        }
    }
}
