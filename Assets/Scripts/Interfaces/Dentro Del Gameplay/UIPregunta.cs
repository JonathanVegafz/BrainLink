using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class UIPregunta : UIManager
{
    public GameObject uiMaster;
    public TMP_Text preguntaText;
    public TMP_Text timerText;
    public AudioSource clipConometro;

    public string pregunta = "";

    public float timerInicial = 0;


    private void OnEnable()
    {
        this.transform.DOScale(new Vector3(1, 1, 0), 0.5f).OnComplete(()=> StartCoroutine(Timer()));
        preguntaText.text = pregunta;
    }

    IEnumerator Timer()
    {
        clipConometro.Play();
        while (true)
        {
            int minutos = (int)timerInicial / 60;
            int segundos = (int)timerInicial % 60;

            timerText.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');

            timerInicial -= Time.deltaTime;

            if(timerInicial <= 0)
            {
                this.transform.DOScale(new Vector3(0,0,0), 0.5f).OnComplete(()=>
                {
                    Desactivar();
                });
                break;
            }
            yield return null;
        }
    }

    public void Desactivar()
    {
        this.gameObject.SetActive(false);
        UIGranMaster.pregunta = pregunta;
        UIGranMaster.pregunta_ = true;
        uiMaster.SetActive(true);
    }
}
