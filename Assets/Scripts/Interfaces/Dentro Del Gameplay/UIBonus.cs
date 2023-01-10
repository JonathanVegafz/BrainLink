using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using History.Jugadores;
using DG.Tweening;
using TMPro;

public class UIBonus : UIManager
{
    public Expositor jugadorActivo;
    public static GameObject uiBonus;
    public static UIBonus bonus;
    public bool puntajeTotal = false;
    public List<string> tema = new List<string>();
    public AnimacionPendulo pendulo;
    public static UnityEvent OnBonus = new UnityEvent();

    public void OnEnable()
    {
        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = false;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        }
        jugadorActivo = GameData.JugadorActivo;
        jugadorActivo.puntajes.ListaTemas = new List<Puntajes.Temas>(jugadorActivo.puntajes.temas.Values);
    }

    public void Desactivar()
    {
        transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
        {
            foreach (var item in GameData.JugadorActivo.cartasMano)
            {
                item.GetComponent<CartaConcepto>().activarCarta = true;
                item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = true;
            }

            for (int i = 0; i < transform.GetChild(1).childCount; i++)
            {
                transform.GetChild(1).GetChild(i).GetComponent<TMP_Text>().color = new Color(0,0,0,0);
            }


            gameObject.SetActive(false);
            //OnBonus.Invoke();
            for (int i = 0; i < Tablero.vectorTerminoTurno.Length; i++)
            {
                if(Tablero.vectorTerminoTurno[i] != null)
                {
                    print("Indice vector Termino turno: " + i);
                    Tablero.vectorTerminoTurno[i].Invoke();
                    Tablero.vectorTerminoTurno[i] = null;
                    return;
                }
            }
        });
        
    }

    public void GuardarReferencia()
    {
        uiBonus = gameObject;
        bonus = this; 
    }

    public void CambiarPuntaje(TMP_Text tema)
    {

        int puntaje = jugadorActivo.GetPuntaje(tema.text);
        if (puntajeTotal)
        {
            puntaje = 7;    
        }
        else
        {
            puntaje *= 2;
        }

        jugadorActivo.CambioPuntaje(tema.text, puntaje);
        jugadorActivo.puntajes.ListaTemas = new List<Puntajes.Temas>(jugadorActivo.puntajes.temas.Values);
    }
}
