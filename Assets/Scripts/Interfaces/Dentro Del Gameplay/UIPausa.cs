using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIPausa : MonoBehaviour
{
    public void OnEnable()
    {

        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = false;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        }

        transform.DOScale(new Vector3(1,1,0),0.5f);
    }
    public void Desactivar()
    {
        foreach (var item in GameData.JugadorActivo.cartasMano)
        {
            item.GetComponent<CartaConcepto>().activarCarta = true;
            item.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = true;
        }


        transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(()=> gameObject.SetActive(false));
    }
    public void CambiarEscena(int id)
    {
        if(id == 0)
        {
            if (PlayerPrefs.GetString("Username") != "" || PlayerPrefs.GetString("Password") != "")
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(0);
                return;
            }
        }

        SceneManager.LoadScene(id);    

    }
}
