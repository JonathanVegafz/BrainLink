using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;

public class EmailUtility : MonoBehaviour
{
    /// <summary>
    /// Realizamos una petición ascincrona de la respuesta del servidor.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled " + token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
    }
    /// <summary>
    /// Se envia un email con los datos de usuario y contraseña designados en PlayfabTestGet.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public void RecoveryEmail(string email, string password)
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.office365.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "correo-institucional-180@outlook.com",
            "dinamix180");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(
            "correo-institucional-180@outlook.com",
            "Juego de cartas",
            System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress(email.ToString());
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = "Usuario: "+email.ToString()+ " password: "+password.ToString();
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "Datos para recuperar tu cuenta de usuario";
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "Recuperacion de datos del usuario";
        client.SendAsync(message, userState);
    }
    /// <summary>
    /// Enviamos un email avisando que la contraseña ha cambiado.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public void PasswordChangeEmail(string email, string password)
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.office365.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "correo-institucional-180@outlook.com",
            "dinamix180");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(
            "correo-institucional-180@outlook.com",
            "Juego de cartas",
            System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress(email.ToString());
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = "Usuario: " + email.ToString() + " password: " + password.ToString();
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "Has cambiado tu clave de usuario";
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "Nueva clave de usuario";
        client.SendAsync(message, userState);
    }
}
