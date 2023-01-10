using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using History.Jugadores;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class UITrampa : UIManager
{
    public static int eventosActivados = 0;
    public Expositor jugadorActivo;
    public static GameObject uiTrampa;
    public static UITrampa trampa;
    public bool reiniciarPuntaje = false;
    public List<string> tema = new List<string>();
    [HideInInspector] public static UnityEvent OnTrampa = new UnityEvent();

    private void OnEnable()
    {
        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = false;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        }
        jugadorActivo = GameData.JugadorActivo;
        jugadorActivo.puntajes.ListaTemas = new List<Puntajes.Temas>(jugadorActivo.puntajes.temas.Values);
    }

    public void GuardarReferencia()
    {
        uiTrampa = gameObject;
        trampa = this;
    }

    public void Desactivar()
    {
        transform.DOScale(new Vector3(0,0,0), 0.5f).OnComplete(()=>
        {
            gameObject.SetActive(false);
            foreach (var item in GameData.JugadorActivo.cartasMano)
            {
                item.GetComponent<CartaConcepto>().activarCarta = true;
                item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = true;
            }
            //OnTrampa.Invoke();

            for (int i = 0; i < Tablero.vectorTerminoTurno.Length; i++)
            {
                if (Tablero.vectorTerminoTurno[i] != null)
                {
                    print("Indice vector Termino turno: " + i);
                    Tablero.vectorTerminoTurno[i].Invoke();
                    Tablero.vectorTerminoTurno[i] = null;
                    return;
                }
            }
        });
        
    }

    public void CambiarPuntaje(TMP_Text tema)
    {
        int puntaje = jugadorActivo.GetPuntaje(tema.text);
        if (reiniciarPuntaje)
        {
            puntaje = 0;
        }
        else
        {
            if(puntaje / 2 <= 0)
            {
                puntaje = 0;
            }
            else
            {
                puntaje /= 2;
            }
        }

        jugadorActivo.CambioPuntaje(tema.text, puntaje);
        jugadorActivo.puntajes.ListaTemas = new List<Puntajes.Temas>(jugadorActivo.puntajes.temas.Values);
    }
}
