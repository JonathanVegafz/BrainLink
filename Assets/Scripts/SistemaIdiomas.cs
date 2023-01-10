using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaIdiomas : MonoBehaviour
{
    public string key;

    public List<TMP_Text> textos;

    public void CambioIdioma()
    {
        for (int i = 0; i < textos.Count; i++)
        {
            textos[i].text = GameData.textosIdioma.dic[key][i];
        }
    }

    public void OnEnable()
    {
        if (GameData.textosIdioma.dic.Count != 0)
        {
            CambioIdioma();
        }
    }


}
