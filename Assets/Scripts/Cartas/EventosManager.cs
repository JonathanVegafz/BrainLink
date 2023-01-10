using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HistoryHerramientas.Cartas_Eventos;
using HistoryHerramientas.Cartas;
using System;
using HistoryHerramientas.Enum;

namespace History.Eventos
{
    public class Tiempo : Evento
    {
        public Tiempo(string descripcion, Tipo_Evento tipoEvento, Atributos atributos) : base(descripcion, tipoEvento, atributos)
        {

        }
    }
}
