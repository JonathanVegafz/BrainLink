using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIGranMaster : UIManager
{
    public TMP_Text con_puenText;
    public TMP_Text tituloTemas;
    public AudioSource audio_;

    public List<string> temas;
    public static string pregunta;

    [HideInInspector] public static bool pregunta_;

    public int indice;

    public void Sonar()
    {
        audio_.Play();
    }

    private void OnEnable()
    {
        transform.DORotate(new Vector3(-0.01f, 0, 0), 0.5f);
        //transform.DOScale(new Vector3(1, 1, 0), 0.5f);
        if (pregunta_ == false)
        {
            indice = 0;
            tituloTemas.text = GameData.textosIdioma.dic["Temas"][0];
            con_puenText.text = temas[indice];
        }
        else
        {
            tituloTemas.text = GameData.textosIdioma.dic["Temas"][2];
            con_puenText.text = pregunta; 
        }
    }

    public void DarPuntaje(int puntaje)
    {
        if (pregunta_ == false)
        {
            
            if (indice < temas.Count - 1)
            {
                GameData.JugadorActivo.AñadirPuntaje(temas[indice], puntaje, indice < GameSettings.CartasIniciales ? History.Enum.TipoTema.Concepto : History.Enum.TipoTema.Puente);
                indice++;
            }
            else
            {
                CambioTema(puntaje);
                return;
            }
            Debug.Log(indice);
            if (indice < GameSettings.CartasIniciales)
            {
                tituloTemas.text = GameData.textosIdioma.dic["Temas"][0];
            }
            else
            {
                Debug.Log("Lo Logre");
                tituloTemas.text = GameData.textosIdioma.dic["Temas"][1];
            }
            con_puenText.text = temas[indice];
        }
        else
        {
            GameData.JugadorActivo.AñadirPuntaje(pregunta, puntaje, History.Enum.TipoTema.Pregunta);
            transform.DORotate(new Vector3(-90f, 0, 0), 0.5f).OnComplete(() => {
                this.gameObject.SetActive(false);
            });
        }
    }

    private void CambioTema(int puntaje)
    {
        GameData.JugadorActivo.AñadirPuntaje(temas[indice], puntaje, indice < GameSettings.CartasIniciales ? History.Enum.TipoTema.Concepto : History.Enum.TipoTema.Puente);
        //transform.DORotate(new Vector3(-90f, 0, 0), 0.5f).OnComplete(() => { 
        //    Tablero.OnTerminoTurno.Invoke();
        //    this.gameObject.SetActive(false);
        //});  
        //Prueba
        transform.DORotate(new Vector3(-90f, 0, 0), 0.5f).OnComplete(() => {
            this.gameObject.SetActive(false);
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

    public void OnDisable()
    {
        pregunta_ = false;
        temas.Clear();
    }
}
