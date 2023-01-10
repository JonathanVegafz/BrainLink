using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PlayFabRecoveryPassword : MonoBehaviour
{
    public TMP_InputField txtxUserEmail;
    public Button recuperar;
    private int counter = 5;
    private float time = 1000000000000000000000000000000f;
    [System.Obsolete]
    public void ConnectionStart()
    {
        StartCoroutine(GetText());
    }
    private void Update()
    {
        if (counter == 0)
        {
            time -= Time.time;
            Debug.Log("Has sido baneado debes esperar:" + Mathf.Abs(time) + " minutos");
            if (time <= 0.0f)
            {
                counter = 5;
            }
        }
        else if (counter > 0)
        {
            time = 1000000000000000000000000000000f;
        }
    }
    [System.Obsolete]
    IEnumerator GetText()
    {
        if (counter != 0)
        {
            UnityWebRequest www = UnityWebRequest.Get("https://gabrielhe180.000webhostapp.com/emailSend.php?userEmail=" + txtxUserEmail.text.ToString() + "&passEmail=1234");
                yield return www.Send();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (counter >= 1)
                    {
                        if (www.downloadHandler.text == "El usuario o password son incorrectos verifique los datos")
                        {
                            counter -= 1;
                            Debug.Log("Te quedan:" + counter + " intentos");
                        }
                        // Show results as text
                        Debug.Log(www.downloadHandler.text);

                        // Or retrieve results as binary data
                        byte[] results = www.downloadHandler.data;


                    }


                }
            }
        }
}
