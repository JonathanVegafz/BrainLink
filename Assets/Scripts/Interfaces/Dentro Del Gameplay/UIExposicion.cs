using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using History.Jugadores;
using System;

public class UIExposicion : UIManager
{
    public AudioSource sonidoConometro;
    public AudioSource sonidoIltimosSegundos;
    public GameObject granMaster;
    public GameObject imagenConcepto;
    public GameObject imagenPuente;
    public TMP_Text timerText;

    public TMP_Text cpText;

    public Expositor jugador;

    [HideInInspector]
    public float timerConcepto;
    [HideInInspector]
    public float timerPuente;
    private bool conceptos = true;
    public int indiceTema = 0;
    private int contadorTemas;
    public int contadorPuntes;
    [TextArea(0, 5)]
    public List<string> temas;



    void OnEnable()
    {
        
        sonidoConometro.Play();
        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = false;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        }

        timerConcepto = GameData.JugadorActivo.tiempoExposicion[0].tiempoTotal + 1f;

        indiceTema = 0;
        timerPuente = GameData.JugadorActivo.tiempoExposicion[1].tiempoTotal + 1f;
        contadorPuntes = 0;

        imagenConcepto.transform.GetChild(0).GetComponent<Image>().sprite = jugador.imagenesConceptos[0];
        imagenConcepto.transform.GetChild(1).GetComponent<Image>().sprite = jugador.sombras[0];    

        cpText.text = temas[indiceTema];
        indiceTema++;
    }

    public void OnFade()
    {
        this.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

        jugador = GameData.JugadorActivo;
        contadorTemas = jugador.conceptos.Count;
        conceptos = true;
        imagenConcepto.SetActive(true);
        imagenPuente.SetActive(false);
        imagenConcepto.transform.localScale = new Vector3(1,1,0);
        imagenPuente.transform.localScale = new Vector3(0, 0, 0);


        temas = new List<string>(jugador.conceptos);
        int total = temas.Count;

        for (int i = 0; i < total - 1; i++)
        {
            temas.Add(jugador.conceptos[i] + "\n-\n" + jugador.conceptos[i + 1]);
            jugador.puentes.Add(jugador.conceptos[i] + " - " + jugador.conceptos[i + 1]);
        }
    }


    public void Update()
    {

        if (conceptos)
        {
            timerConcepto -= Time.deltaTime;
            if(timerConcepto <= 1)
                SaltarTiempo();
            CalcularTiempo(timerConcepto);
        }
        else
        {
            timerPuente -= Time.deltaTime;
            CalcularTiempo(timerPuente);
            if(timerPuente <= 1)
                SaltarTiempo();
        }
    }

    public void SaltarTiempo()
    {

        sonidoConometro.Play();
        sonidoIltimosSegundos.Stop();
        if (conceptos)
        {
            if (indiceTema == contadorTemas)
            {
                imagenConcepto.transform.DOScale(new Vector3(0, 0, 0), 0.3f).OnComplete(() => {
                    imagenConcepto.SetActive(false);
                    imagenPuente.SetActive(true);
                    imagenPuente.transform.DOScale(new Vector3(1, 1, 0), 0.3f);
                });
                imagenPuente.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = jugador.imagenesConceptos[contadorPuntes];
                imagenPuente.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = jugador.sombras[contadorPuntes];

                imagenPuente.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = jugador.imagenesConceptos[contadorPuntes + 1];
                imagenPuente.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = jugador.sombras[contadorPuntes + 1];
                contadorPuntes++;

                conceptos = false;
            }
            else if (indiceTema < contadorTemas)
            {
                imagenConcepto.transform.GetChild(0).GetComponent<Image>().sprite = jugador.imagenesConceptos[indiceTema];
                imagenConcepto.transform.GetChild(1).GetComponent<Image>().sprite = jugador.sombras[indiceTema];
            }
            CambioTema();
            timerConcepto = GameData.JugadorActivo.tiempoExposicion[0].tiempoTotal + 1f;

        }
        else
        {

            if (indiceTema == temas.Count)
            {
                this.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => {
                    gameObject.SetActive(false);

                    List<string> nuevoTema = new List<string>(jugador.conceptos);
                    int total = nuevoTema.Count;

                    for (int i = 0; i < total - 1; i++)
                    {
                        nuevoTema.Add(jugador.conceptos[i] + " - " + jugador.conceptos[i + 1]);
                    }

                    //granMaster.GetComponent<UIGranMaster>().temas = new List<string>(jugador.puentes);
                    
                    granMaster.GetComponent<UIGranMaster>().temas = new List<string>(nuevoTema); //Asi se guarda en el diccionario de Temas
                    granMaster.SetActive(true);
                });
            }
            else if (indiceTema < temas.Count)
            {
                imagenPuente.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = jugador.imagenesConceptos[contadorPuntes];
                imagenPuente.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = jugador.sombras[contadorPuntes];

                imagenPuente.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = jugador.imagenesConceptos[contadorPuntes + 1];
                imagenPuente.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = jugador.sombras[contadorPuntes + 1];
                contadorPuntes++;
            }
            else
            {
                Debug.Log(indiceTema == temas.Count + 1);
            }
            timerPuente = GameData.JugadorActivo.tiempoExposicion[1].tiempoTotal + 1f;
            CambioTema();

        }
    }

    public void SiguienteTema()
    {
        CambioTema();    
    }

    private void CambioTema()
    {
        if (indiceTema < temas.Count)
        {
            cpText.text = temas[indiceTema];
            indiceTema++;
            sonidoConometro.DORestart();
        }
    }

    private void CalcularTiempo(float timer)
    {
        int minutos = (int)timer / 60;
        int segundos = (int)timer % 60;

        timerText.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');
    }
}
