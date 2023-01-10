using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInicio : UIManager
{
    [System.Serializable]
    public class ListJson
    {
        public List<string> Keys = new List<string>();
        public List<string> values = new List<string>();
    }
    public GameObject jugarUI;
    public ListJson listJson = new ListJson();

    public void Awake()
    {
        if (GameData.textosIdioma != null)
        {
            GameData.textosIdioma = GameSettings.JsonIdioma(true);
            listJson.Keys = new List<string>(GameData.textosIdioma.dic.Keys);
            foreach (var item in GameData.textosIdioma.dic)
            {
                listJson.values.AddRange(item.Value);
            }

        }
    }

    public void Update()
    {

    }

    public void CerrarSeccion()
    {
        if (PlayerPrefs.GetString("Username") != "" || PlayerPrefs.GetString("Password") != "")
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
    }

    public void ActivarAnimacion1(Button boton)
    {
        Transicion(this.gameObject, boton, this.gameObject.GetComponent<Image>().color.a == 1 ? 0 : 1);
    }
}
