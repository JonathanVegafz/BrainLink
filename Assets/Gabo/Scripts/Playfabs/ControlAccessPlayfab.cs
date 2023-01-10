 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class ControlAccessPlayfab : MonoBehaviour
{
    //Variables privadas
    private const float timerBlockConnection = 9000000f;
    private float timerLeft;
    private int activateCounter;
    private int counterSession;
    private int errorBlock;
    private string loadUser;
    //Variables Publicas
    public GameObject loginObject;
    public GameObject errorImagenObject;
    public GameObject blockImageObject;
    public GameObject blockImageRecovery;
    public GameObject passwordChangeImage;
    public GameObject ContentorBarEffect;
    public GameObject logoFull;
    public TMP_Text txtTimeBlockNumber;
    public bool english, spanish;
    public TMP_Text txtMenssageComplete;
    [HideInInspector]
    public string[] menssageBox;
    public int userExist;
    public Dictionary<string, string> userList = new Dictionary<string, string>();

    //Getter y setter
    public int ActivateCounter { get { return activateCounter; } set { activateCounter = value; } }
    public int CounterSession { get { return counterSession; } set { counterSession = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos las variables
        InicilizedVar();
        //Desactivo las interfaces UI al dar start
        InterfacesOFF();
    }
    private void Update()
    {
        //activamos la corrutina del contador
        if(errorBlock == 1)
        {
            StartCoroutine(CounterStartBlock());
        }
    }
    public void CountActivateSesion()
    {
        errorBlock = 1;
    }
    /// <summary>
    /// Activamos el contador de session para el bloqueo del usuario.
    /// </summary>
    IEnumerator CounterStartBlock()
    {
        if(ActivateCounter == 1)
        {
            timerLeft -= Time.time;
            //Debug.Log("Tiempo de bloqueo: "+ Mathf.Abs(timerLeft));
            BlockImageON();
            if (txtTimeBlockNumber != null)
            {
                txtTimeBlockNumber.text = "" + Mathf.Abs(timerLeft);
            }
            if (timerLeft <=0.0f)
            {
                counterSession = 3;
                ActivateCounter = 0;
            }
        }
        else
        {
            timerLeft = timerBlockConnection;
            BlockImageOFF();
            errorBlock = 0;
           
        }
        yield return null;
    }
    /// <summary>
    /// cuadro de dialogo de error de usuario abierto
    /// </summary>
    public void OpenError()
    {   
            if (!errorImagenObject.activeInHierarchy)
            {
                errorImagenObject.SetActive(true);
            }
    }
    /// <summary>
    /// cuadro de dialogo de error de usuario cerrado
    /// </summary>
    public void OpenErrorOFF()
    {
        if(errorImagenObject!=null)
        {
            if(errorImagenObject.activeInHierarchy)
            {
                errorImagenObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// //Cuadro UI Blockeo usuario OFF
    /// </summary>
    private void BlockImageON()
    {
        if (blockImageObject != null)
        {
            if (!blockImageObject.activeInHierarchy)
            {
                blockImageObject.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Cuadro UI Blockeo usuario On
    /// </summary>
    private void BlockImageOFF()
    {
        if (blockImageObject != null)
        {
            if (blockImageObject.activeInHierarchy)
            {
                blockImageObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Segun la opcion escogida el mensaje del cuadro de dialogo
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public int Messages(int option)
    {
        if (menssageBox[0] != null && menssageBox[1] != null && menssageBox[2] != null)
        {
            switch (option)
            {
                case 1:
                    txtMenssageComplete.text = menssageBox[0].ToString();
                    break;
                case 2:
                    txtMenssageComplete.text = menssageBox[1].ToString();
                    break;
                case 3:
                    txtMenssageComplete.text = menssageBox[2].ToString();
                    break;
                case 4:
                    txtMenssageComplete.text = menssageBox[3].ToString();
                    break;
                case 5:
                    txtMenssageComplete.text = menssageBox[4].ToString();
                    break;
                case 6:
                    txtMenssageComplete.text = menssageBox[5].ToString();
                    break;
                case 7:
                    txtMenssageComplete.text = menssageBox[6].ToString();
                    break;
                case 8:
                    txtMenssageComplete.text = menssageBox[7].ToString();
                    break;
                case 9:
                    txtMenssageComplete.text = menssageBox[8].ToString();
                    break;
                case 10:
                    txtMenssageComplete.text = menssageBox[9].ToString();
                    break;
            }
           
        }
            return option;
            
    }
    /// <summary>
    /// Cuadro UI desactivar clave de usuario
    /// </summary>
    public void RecoveryUserOFF()
    {
        if (blockImageRecovery != null)
        {
            if (blockImageRecovery.activeInHierarchy)
            {
                blockImageRecovery.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Cuadro UI recuperar clave de usuario
    /// </summary>
    public void RecoveryUser()
    {
        OpenErrorOFF();
        if (blockImageRecovery != null)
        {
            if (!blockImageRecovery.activeInHierarchy)
            {
                blockImageRecovery.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Cuadro UI de clave oFF
    /// </summary>
    public void PasswordChangeOFF()
    {
        if(passwordChangeImage !=null)
        {
            if (passwordChangeImage.activeInHierarchy)
            {
                passwordChangeImage.SetActive(false);
            }
        }
    }
    public void FormPhaderON()
    {
        LoginObjectON();
    }
    /// <summary>
    /// Cuadro UI de clave on
    /// </summary>
    public void PasswordChangeON()
    {
        if (passwordChangeImage != null)
        {
            if (!passwordChangeImage.activeInHierarchy)
            {
                passwordChangeImage.SetActive(true);
            }
        }
    }
    public void LogoFullON()
    {
        if(logoFull!=null)
        {
            if(!logoFull.activeInHierarchy)
            {
                logoFull.SetActive(true);
            }
        }
    }
    public void LogoFullOFF()
    {
        if(logoFull!=null)
        {
            if(logoFull.activeInHierarchy)
            {
                logoFull.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Inicializamos las variables
    /// </summary>
    private void InicilizedVar()
    {
        //Inicializamos la variable flotante 1 ves
        timerLeft = timerBlockConnection;
        //inicializamos el contador de vida
        counterSession = 3;
        //Iniciamos el contador activado en 0
        ActivateCounter = 0;
        //variable para el blockeo counter
        errorBlock = 0;
        //Inicializo el array de mensajes en 6 elementos
        menssageBox = new string[10];
    }
    /// <summary>
    /// Cargo todas las interfaces UI que se van a desactivar al inicio.
    /// </summary>
    private void InterfacesOFF()
    {
        LoginObjectOFF();
        LogoFullOFF();
        //animacion fill effect
        ContentorBarEffectOFF();
        //gameobject block padre
        if (blockImageObject != null)
        {
            if (blockImageObject.activeInHierarchy)
            {
                blockImageObject.SetActive(false);
            }
        }
        //gameobject error dialogo
        if (errorImagenObject != null)
        {
            if (errorImagenObject.activeInHierarchy)
            {
                errorImagenObject.SetActive(false);
            }
        }
        //gameobject recuperar clave y usuario
        if (blockImageRecovery != null)
        {
            if (blockImageRecovery.activeInHierarchy)
            {
                blockImageRecovery.SetActive(false);
            }
        }
        //gameobject cambiar clave de usuario
        if (passwordChangeImage != null)
        {
            if (passwordChangeImage.activeInHierarchy)
            {
                passwordChangeImage.SetActive(false);
            }
        }
        //mensajes de error
        if (english == false && spanish == true)
        {
            menssageBox[0] = "Complete el formulario";
            menssageBox[1] = "No existe ningun usuario con ese nombre";
            menssageBox[2] = "El usuario o contraseña son incorrectos";
            menssageBox[3] = "El email ingresado no existe";
            menssageBox[4] = "Hemos enviado un email con los datos para recuperar tu cuenta";
            menssageBox[5] = "Su clave de usuario fue actualizada correctamente";
            menssageBox[6] = "Necesitas conexion a internet para poder logearte y acceder";
            menssageBox[7] = "No se pudo recuperar los datos de este usuario";
            menssageBox[8] = "No se pudo cambiar la contraseña";
            menssageBox[9] = "No se pudo Iniciar la sesion para este usuario";
        }
        else if (english == true && spanish == false)
        {
            menssageBox[0] = "Fill out the form";
            menssageBox[1] = "There is no user with that name";
            menssageBox[2] = "The username or password is incorrect";
            menssageBox[3] = "The email entered does not exist";
            menssageBox[4] = "We have sent an email with the data to recover your account";
            menssageBox[5] = "Your user password was successfully updated";
            menssageBox[6] = "Necesitas conexion a internet para poder logearte y acceder";
            menssageBox[7] = "Failed to retrieve data for this user";
            menssageBox[8] = "Failed to retrieve data for this user";
            menssageBox[9] = "Could not login for this user";
        }
       
    }
    public void LoginObjectOFF()
    {
        if(loginObject!=null)
        {
            if (loginObject.activeInHierarchy)
            {
                loginObject.SetActive(false);
            }
        }
    }
    public void LoginObjectON()
    {
        if (loginObject != null)
        {
            if (!loginObject.activeInHierarchy)
            {
                loginObject.SetActive(true);
            }
        }
    }
    public void ContentorBarEffectON()
    {
        if(ContentorBarEffect!=null)
        {
            if(!ContentorBarEffect.activeInHierarchy)
            {
                ContentorBarEffect.SetActive(true);
            }
        }
    }
    public void ContentorBarEffectOFF()
    {
        if (ContentorBarEffect != null)
        {
            if (ContentorBarEffect.activeInHierarchy)
            {
                ContentorBarEffect.SetActive(false);
            }
        }
    }
}
