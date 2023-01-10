using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using History.Jugadores;
using System;

public class UIIngresoNombres : UIManager
{
    private Image imagenAvatar;
    public GameObject seleccionarAvatar;

    public int cantidaJugadores;


    [System.Serializable]
    public struct ReferenciasIngresoJugadores
    {
        public List<TMP_InputField> nombres;
        public List<Image> avatares;
        public Button botonContinuar;
    }


    public ReferenciasIngresoJugadores rIJugadores = new ReferenciasIngresoJugadores();

    public void ValidarNombres()
    {
        string nom1 = rIJugadores.nombres[0].text;
        string nom2 = rIJugadores.nombres[1].text;
        string nom3 = rIJugadores.nombres[2].text;
        string nom4 = rIJugadores.nombres[3].text;

        bool botonActivado = false;

        if (nom1.Length >= 4)
        {
            if (rIJugadores.nombres.FindAll(x => x.text == nom1).Count == 1)
                botonActivado = true;
            else
                botonActivado = false;
        }
        else botonActivado = false;

        #region Esto es Prueba
        //if (nom1.Length >= 4 && nom2.Length >= 4 && nom3.Length >= 4 && nom4.Length >= 4)
        //{

        //    if (
        //          rIJugadores.nombres.FindAll(x => x.text == nom1).Count == 1 &&
        //          rIJugadores.nombres.FindAll(x => x.text == nom2).Count == 1 &&
        //          rIJugadores.nombres.FindAll(x => x.text == nom3).Count == 1 &&
        //          rIJugadores.nombres.FindAll(x => x.text == nom4).Count == 1
        //        )
        //    {
        //        botonActivado = true;
        //    }
        //    else
        //    {
        //        botonActivado = false;
        //    }
        //}
        //else
        //{
        //    botonActivado = false;
        //}
        #endregion

        rIJugadores.botonContinuar.interactable = botonActivado;


    }

    public void CrearJugadores(Button boton)
    {
        Transicion(this.gameObject, boton, this.gameObject.GetComponent<Image>().color.a == 1 ? 0 : 1);
        List<Jugador> jugadores = Jugadores(cantidaJugadores);
        if (GameData.jugadores.Count != 0)
        {
            GameData.jugadores.Clear();
            GameData.expositores.Clear();
        }

        GameData.jugadores = new List<Jugador>(jugadores);
    }

    public void Retroseso(Button boton)
    {
        Transicion(this.gameObject, boton, this.gameObject.GetComponent<Image>().color.a == 1 ? 0 : 1);
        GameData.jugadores.Clear();
    }


    private List<Jugador> Jugadores(int cantidaJugadores)
    {
        List<Jugador> jugadores = new List<Jugador>();

        for (int i = 0; i < cantidaJugadores; i++)
        {
            if (rIJugadores.nombres[i].text != "")
            {
                Jugador jugador = new Jugador(rIJugadores.nombres[i].text, rIJugadores.avatares[i].sprite);
                jugadores.Add(jugador);
            }
        }

        return jugadores;

    }

    public void AbrirAvatar(Image imagen)
    {
        imagenAvatar = imagen;
    }

    public void SeleccionarAvatar(Image imagen)
    {
        imagenAvatar.sprite = imagen.sprite;
        seleccionarAvatar.SetActive(false);
    }
}
