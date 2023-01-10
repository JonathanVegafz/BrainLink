using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class AnimacionPendulo : MonoBehaviour
{
    public UnityEvent OnPendulo = new UnityEvent();

    public List<TMP_Text> temasText = new List<TMP_Text>();

    public bool activado = false;

    public void OnEnable()
    {
        
        transform.DOScale(new Vector3(1.5f, 1.5f, 0), 0.5f).OnComplete(()=> 
        {
            if(activado == false)
            {
                NO();
                //Que paso
            }
            else
            {
                SI();
                //Que paso
            }
            
            OnPendulo.Invoke();
        });
    }

    private void OnDisable()
    {
        transform.GetChild(0).DORotate(new Vector3(0, 0, 0), 0.1f);
        transform.localScale = new Vector3(0, 0, 0);
    }

    private void NO()
    {
        Transform pendulo = transform.GetChild(0);

        GetComponent<AudioSource>().Play();
        pendulo.DORotate(new Vector3(0,0,-46), 0.3f).OnComplete(()=>
        {
            GetComponent<AudioSource>().Play();
            pendulo.DORotate(new Vector3(0,0,46), 0.3f).OnComplete(()=>
            {
                GetComponent<AudioSource>().Play();
                pendulo.DORotate(new Vector3(0, 0, -46), 0.3f).OnComplete(() =>
                {
                    GetComponent<AudioSource>().Play();
                    pendulo.DORotate(new Vector3(0, 0, 46), 0.3f).OnComplete(() =>
                    {
                        GetComponent<AudioSource>().Play();
                        pendulo.DORotate(new Vector3(0, 0, -46), 0.5f).OnComplete(()=>
                        {
                            GetComponent<AudioSource>().Play();
                            temasText.ListTextFade(1, 0.3f);
                        });
                    });
                });
            });
        });
        
    }

    private void SI()
    {
        Transform pendulo = transform.GetChild(0);

        GetComponent<AudioSource>().Play();
        pendulo.DORotate(new Vector3(0, 0, 46), 0.3f).OnComplete(() =>
        {
            GetComponent<AudioSource>().Play();
            pendulo.DORotate(new Vector3(0, 0, -46), 0.3f).OnComplete(() =>
            {
                GetComponent<AudioSource>().Play();
                pendulo.DORotate(new Vector3(0, 0, 46), 0.3f).OnComplete(() =>
                {
                    GetComponent<AudioSource>().Play();
                    pendulo.DORotate(new Vector3(0, 0, -46), 0.3f).OnComplete(() =>
                    {
                        GetComponent<AudioSource>().Play();
                        pendulo.DORotate(new Vector3(0, 0, 46), 0.5f).OnComplete(() => 
                        {
                            GetComponent<AudioSource>().Play();
                            temasText.ListTextFade(1, 0.3f);
                        });
                    });
                });
            });
        });

    }
}
