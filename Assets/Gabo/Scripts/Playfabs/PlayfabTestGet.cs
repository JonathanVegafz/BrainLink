using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

//Componentes requerido para que todo funcione correctamente.
[RequireComponent(typeof(TextMeshPro),typeof(ControlAccessPlayfab),typeof(EmailUtility))]
public class PlayfabTestGet : MonoBehaviour
{
    //Variables de acceso publicas
    public Transform contenedorLogo;
    public TMP_InputField txtUser, txtPass,txtRecoveryEmail,txtPassword1,txtPassword2;
    public ControlAccessPlayfab controlAccessPlayfab;
    public EmailUtility emailUtility;
    public Animator animScreen;
    public int escenaAcargar;
    public Image imagenLogo;
    public SoundExPlayfab soundPlayfab;
    public Image barEffectFillImage;
    public bool barEffectFill;
    public float waitTimeFillImage = 3.0f;
    //Variables Privadas
    private int opcion;
    private Dictionary<string,int> emailRecoverySave = new Dictionary<string, int>();
    private string temporalUser, temporalPasswor;
    private int exist = 0;
    private const string animScreenTrigger = "Salida";
    private int opcionAnim, soundInt;

    public void Start()
    {
        DOTween.Init();
        barEffectFill = false;
        UserLoadLogin();
        Connect();
    }
    void Update()
    {
        if (barEffectFill == true)
        {
            //Reduce fill amount over 30 seconds
            barEffectFillImage.fillAmount += 1.0f / waitTimeFillImage * Time.deltaTime;
        }
    }
    /// <summary>
    /// Inicio la conexion con la base de datos
    /// </summary>
    public void Connect()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "AF9A2";
        }

        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    public void ClientGetPublisherData()
    {
        SoundButon();
        //Si el campo usuario y clave de usuario estan vacios.
        if (txtUser.text == "" || txtPass.text == "")
        {
            controlAccessPlayfab.CounterSession -= 1;
            if (controlAccessPlayfab.CounterSession <= 0)
            {
                SoundError();
                ControlCounterSession();
            }
            else
            {

                SoundError();
                controlAccessPlayfab.Messages(opcion=1);
                MessageOn();
                //Debug.Log("Complete el formulario");
                //Debug.Log("Te quedan:" + controlAccessPlayfab.CounterSession + " intentos");
            }
        }
            //Si el campo usuario o Clave de usuario no estan vacios.
            if (txtUser.text != "" || txtPass.text != "")
            {
                PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            , result =>
            {
            //Si el resultado de la base de datos es nulo.
            if (result.Data == null)
                {
                    controlAccessPlayfab.CounterSession -= 1;
                    if (controlAccessPlayfab.CounterSession <= 0)
                    {
                        SoundError();
                        ControlCounterSession();
                    }
                    else
                    {
                        SoundError();
                        controlAccessPlayfab.Messages(opcion = 2);
                        MessageOn();
                    //Debug.Log("no existe ningun usuario con ese nombre");
                    //Debug.Log("Te quedan:" + controlAccessPlayfab.CounterSession + " intentos");
                }
                }
            //Si no existe en la base de datos el campo usuario y el campo clave de usuario.
            else if (!result.Data.ContainsKey(txtUser.text) || !result.Data[txtUser.text].Value.Equals(txtPass.text))
                {
                    controlAccessPlayfab.CounterSession -= 1;
                    if (controlAccessPlayfab.CounterSession <= 0)
                    {
                        SoundError();
                        ControlCounterSession();
                    }
                    else
                    {
                        SoundError();
                        controlAccessPlayfab.Messages(opcion = 3);
                        MessageOn();
                    //Debug.Log("El usuario o contraseña son incorrectos");
                    //Debug.Log("Te quedan:" + controlAccessPlayfab.CounterSession + " intentos");
                }
                }
            //Si existe el campo usuario y ademas si la clave de usuario introducida es igual a la que existe en la base de datos.
            else if (result.Data.ContainsKey(txtUser.text) && result.Data[txtUser.text].Value.Equals(txtPass.text))
                {
                    if (controlAccessPlayfab.CounterSession <= 0)
                    {
                        SoundError();
                        ControlCounterSession();
                    }
                    else
                    {
                    //Comprobamos que el usuario no haya pedido recuperar sus datos.
                    if (exist == 0)
                        {
                            temporalPasswor = txtPass.text;
                            temporalUser = txtUser.text;
                            UserLoginSave();
                            SoundWinner();
                            //UserDatabaseADD();
                            opcionAnim = 2;
                            LoadScene(escenaAcargar,opcionAnim);
                        }
                        else
                        {
                            //Si el usuario pidio recuperar sus datos lo enviamos a que cambie la clave de usuario.
                            SoundButon();
                            temporalUser = txtUser.text;
                            PasswordChangeON();
                        }
                    }

                }

            },
                error =>
                {
                    SoundError();
                    controlAccessPlayfab.Messages(opcion = 10);
                    MessageOn();
                });
        }
    }
    public void PasswordChangeMethod()
    {
        
        //Si el campo password y el campo rePassword no estan vacios.
        if (txtPassword1.text !="" && txtPassword2.text !="")
        {
            //Si las claves de usuario introducidas son iguales campo1 y campo2
            if(txtPassword1.text.Equals(txtPassword2.text))
            {
                PlayFabClientAPI.GetUserData(new GetUserDataRequest()
                    //Si existe algun error
        , result =>
        {
            //Si el usuario al que hacemos referencia existe en la base de datos
            if(result.Data.ContainsKey(temporalUser))
            {
                SoundButon();
                SetTitleData();
                //Debug.Log("Usuario modificado correctamente");
                exist = 0;
                emailRecoverySave.Remove(temporalUser);
                emailUtility.PasswordChangeEmail(temporalUser,txtPassword1.text);
                PlayerPrefs.SetString("Username", temporalUser);
                PlayerPrefs.SetString("Password", txtPassword1.text);
                PlayerPrefs.Save();
                temporalUser = "";
                controlAccessPlayfab.Messages(opcion = 6);
                MessageOn();
                PasswordChangeOFF();

            }
               //Si tenemos algun error
        },error =>
        {
            SoundError();
            controlAccessPlayfab.Messages(opcion = 9);
            MessageOn();
        });
            }
            //Si las claves introducidas en el campo1 y campo2 son diferentes
            else
            {
                SoundError();
                controlAccessPlayfab.Messages(opcion = 3);
                MessageOn();
            }
        }
        else
        {
            SoundError();
            controlAccessPlayfab.Messages(opcion = 1);
            MessageOn();
        }
    }
    /// <summary>
    /// Chequeamos que exista el email para la cuenta a recuperar y de ser asi procedemos a enviar un email con los datos correspondientes.
    /// </summary>
    public void ClientGetPublisherEmailRecovery()
    {
        //Si el campo de email esta vacion
        if (txtRecoveryEmail.text == "")
            {
                controlAccessPlayfab.CounterSession -= 1;
                if (controlAccessPlayfab.CounterSession <= 0)
                {
                    SoundError();
                    ControlCounterSession();
                }
                else
                {
                    SoundError();
                    controlAccessPlayfab.Messages(opcion = 1);
                    MessageOn();
                    
                }
            }
            //Si el campo de email no esta vacio.
            if (txtRecoveryEmail.text != "")
            {
                PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            , result =>
            {
                //Si la consulta no es valida
                if (result.Data == null)
                {
                    controlAccessPlayfab.CounterSession -= 1;
                    if (controlAccessPlayfab.CounterSession <= 0)
                    {
                        SoundError();
                        ControlCounterSession();
                    }
                    else
                    {
                        SoundError();
                        controlAccessPlayfab.Messages(opcion = 4);
                        MessageOn();
                }
                }
                //Si no se encuentra el email dentro de la base de datos
                else if (!result.Data.ContainsKey(txtRecoveryEmail.text))
                {
                    controlAccessPlayfab.CounterSession -= 1;
                    if (controlAccessPlayfab.CounterSession <= 0)
                    {
                        SoundError();
                        ControlCounterSession();
                    }
                    else
                    {
                        SoundError();
                        controlAccessPlayfab.Messages(opcion = 4);
                        MessageOn();
                    //Debug.Log("El usuario o contraseña son incorrectos");
                    //Debug.Log("Te quedan:" + controlAccessPlayfab.CounterSession + " intentos");
                }
                }
                //Si existe el email en la base de datos
                else if (result.Data.ContainsKey(txtRecoveryEmail.text))
                {
  
                    if (controlAccessPlayfab.CounterSession <= 0)
                    {
                        SoundError();
                        ControlCounterSession();
                    }
                    else
                    {
                        if (!emailRecoverySave.ContainsKey(txtRecoveryEmail.text))
                        {
                            if(result.Data.ContainsKey(txtRecoveryEmail.text))
                            {
                                emailRecoverySave.Add(txtRecoveryEmail.text,0);
                                exist = 1;
                            }
                            DesactivateRecovery();
                            emailUtility.RecoveryEmail(txtRecoveryEmail.text, result.Data[txtRecoveryEmail.text].Value.ToString());
                            SoundNotification();
                            controlAccessPlayfab.Messages(opcion = 5);
                            MessageOn();
                        }
                        
                    }
                }
            },
            //Si existe algun error
                error =>
                {
                    SoundError();
                    controlAccessPlayfab.Messages(opcion = 8);
                    MessageOn();
                });
            }
        }
    /// <summary>
    /// Buscamos los datos existentes en el servidor y los modificamos.
    /// </summary>
    public void SetTitleData()
    {

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
           {
               {temporalUser,txtPassword1.text}
           }
        },
            result => { SoundNotification();},
            error => {
            }
        );
    }
    
    /// <summary>
    /// Si la conexion con la base de datos es exitosa.
    /// </summary>
    /// <param name="result"></param>
    private void OnLoginSuccess(LoginResult result)
    {
        if (PlayerPrefs.GetString("Username") != "" || PlayerPrefs.GetString("Password") != "")
        {
            controlAccessPlayfab.userExist = 1;
            SoundWinner();
            opcionAnim = 3;
            AnimWait(escenaAcargar, opcionAnim);
        }
        else
        {
            
            opcionAnim = 1;
            AnimWait(escenaAcargar,opcionAnim);
        }
    }
    /// <summary>
    /// Si la conexion con la base de datos falla.
    /// </summary>
    /// <param name="error"></param>
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("No se pudo llamar a la api");
        Debug.LogError("Información de debug:");
        Debug.LogError(error.GenerateErrorReport());
        if (controlAccessPlayfab.userExist == 1)
        {
            SoundWinner();
            opcionAnim = 3;
            AnimWait(escenaAcargar, opcionAnim);
        }
        else
        {
            controlAccessPlayfab.Messages(opcion = 7);
            SoundError();
            MessageOn();
        }
    }
    /// <summary>
    /// Envio de la variable ErrorActive a script ControlAccessPlayfab (activa UI).
    /// </summary>
    private void MessageOn()
    {
        controlAccessPlayfab.OpenError();
    }
    /// <summary>
    /// Envio de la variable ErrorActive a script ControlAccessPlayfab (activa UI).
    /// </summary>
    public void MessageOFF()
    {
        SoundButon();
        controlAccessPlayfab.OpenErrorOFF();
    }
    /// <summary>
    /// Activamos el contador de blockeo para la conexion en el script ControlAccessPlayfab.
    /// </summary>
    private void ControlCounterSession()
    {
        controlAccessPlayfab.ActivateCounter = 1;
        controlAccessPlayfab.CountActivateSesion();
    }
    /// <summary>
    /// Desactivamos la interfaz UI de recuperar datos de usuario.
    /// </summary>
    public void DesactivateRecovery()
    {
        SoundButon();
        controlAccessPlayfab.RecoveryUserOFF();
    }
    /// <summary>
    /// Desactivamos la interfaz UI de cambiar clave de usuario.
    /// </summary>
    public void PasswordChangeOFF()
    {
       SoundButon();
       controlAccessPlayfab.PasswordChangeOFF();
    }
    /// <summary>
    /// Activamos la interfaz UI de cambiar clave de usuario.
    /// </summary>
    private void PasswordChangeON()
    {
        SoundButon();
        controlAccessPlayfab.PasswordChangeON();
    }
    private void FormPhaderON()
    {
        controlAccessPlayfab.FormPhaderON();
    }
       
    private void LoadScene(int scene,int activar)
    {
        StartCoroutine(animWaitScreen(scene,activar));
    }
    private void AnimWait(int scene,int activar)
    {
        StartCoroutine(animWaitScreen(scene, activar));
    }
    IEnumerator animWaitScreen(int scene,int activar)
    {
        if (activar == 1)
        {
            controlAccessPlayfab.LogoFullOFF();
            animScreen.SetBool("Salida", true);
            yield return new WaitForSeconds(1);
            animScreen.SetBool("Salida", false);
            FormPhaderON();
        }
        else if(activar == 2 || activar == 3)
        {
            controlAccessPlayfab.LogoFullOFF();
            controlAccessPlayfab.ContentorBarEffectON();
            barEffectFill = true;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                //Output the current progress
                string a = "";
                a = "Loading progress: " + (asyncOperation.progress * 100) + "%";
                if (asyncOperation.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(3.5f);
                    barEffectFill = false;
                    asyncOperation.allowSceneActivation = true;
                    
                }
                else
                {
                    barEffectFill = true;
                }
                yield return null;

            }
        }
        else if(activar == 0)
        {
            controlAccessPlayfab.ContentorBarEffectOFF();
            barEffectFill = false;
        }
    }
   
    /// <summary>
    /// al presionar el boton para hacer una tarea
    /// </summary>
    private void SoundButon()
    {
        soundInt = 0;
        soundPlayfab.AudioLogin(soundInt);
    }
    /// <summary>
    /// al salir un cuadro de dialogo con error
    /// </summary>
    private void SoundError()
    {
        soundInt = 2;
        soundPlayfab.AudioLogin(soundInt);
    }
    /// <summary>
    /// al tener exito para logearse ya sea con los datos de acceso o mediante el registro de acceso
    /// </summary>
    private void SoundWinner()
    {
        soundInt = 1;
        soundPlayfab.AudioLogin(soundInt);
    }
    /// <summary>
    /// en caso de haber notificaciones
    /// </summary>
    private void SoundNotification()
    {
        soundInt = 3;
        soundPlayfab.AudioLogin(soundInt);
    }
    public void RecoveryUser()
    {
       SoundButon();
       controlAccessPlayfab.RecoveryUser();
    }
    public void ExitLogin()
    {
        SoundButon();
        Application.Quit();
    }
    private void UserLoginSave()
    {
        if(temporalUser!="" && temporalPasswor!="")
        {
            PlayerPrefs.SetString("Username", temporalUser);
            PlayerPrefs.SetString("Password", temporalPasswor);
            PlayerPrefs.Save();
            controlAccessPlayfab.userExist = 1;
            temporalPasswor = "";
        }
        
    }
    private void UserLoadLogin()
    {
        if (PlayerPrefs.GetString("Username") == "" || PlayerPrefs.GetString("Password") == "")
        {
            controlAccessPlayfab.userExist = 0;
            controlAccessPlayfab.LoginObjectON();
        }
        else
        {
            controlAccessPlayfab.userExist = 1;
            controlAccessPlayfab.LoginObjectOFF();
        }
    }
    public void UserCloseSession()
    {
        if(PlayerPrefs.GetString("Username") !="" || PlayerPrefs.GetString("Password")!="")
        {
            PlayerPrefs.DeleteAll();
        }
    }
}