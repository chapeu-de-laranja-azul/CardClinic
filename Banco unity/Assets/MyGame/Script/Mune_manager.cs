using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;

public class Mune_manager : MonoBehaviour
{

    [Header("Login")]
    public GameObject login_canvas;
    public InputField login_email;
    public InputField login_password;
    public Button login_button;
    public Button login_newreg_button;

    [Space]
    [Header("Registre")]
    public GameObject register_canvas;
    public InputField register_email;
    public InputField register_password;
    public InputField register_repassword;
    public Button register_button;
    public Button register_back_button;

    [Space]
    [Header("Message")]
    public GameObject message_canvas;
    public Text message_text;
    public Button message_button;

    [Space]
    [Header("MenuGame")]
    public GameObject menugame_canvas;

    [Space]
    [Header("REST")]
    public Rest_Controller rest_script;

    string menu_email;
    string menu_password;

    private void Start()
    {
        menu_email = "";
        menu_password = "";

        //Login
        login_button.onClick.AddListener(Button_Login);
        login_newreg_button.onClick.AddListener(Button_Login_NewRegister);

        login_email.text = PlayerPrefs.GetString("PLAYER_EMAIL", "");
        login_password.text = PlayerPrefs.GetString("PLAYER_PASSWORD", "");

        //Message
        message_button.onClick.AddListener(Button_Message_Close);

        //Register
        register_button.onClick.AddListener(Button_registre);
        register_back_button.onClick.AddListener(Button_RegisterBack);

        MenuActive(login_canvas);
       
    }

    #region ################# FUNCTIONS #############

    //Função que Gerencia o canvas que será ativado
    void MenuActive(GameObject canvas)
    {

        login_canvas.gameObject.SetActive(login_canvas.name.Equals(canvas.name));
        register_canvas.gameObject.SetActive(register_canvas.name.Equals(canvas.name));
        message_canvas.gameObject.SetActive(message_canvas.name.Equals(canvas.name));
        menugame_canvas.gameObject.SetActive(menugame_canvas.name.Equals(canvas.name));

    }

    void GetMessage(Message p_msg)
    {
        //Função para receber os dados no formato Message
        if(p_msg.GetMessage() != "")
        {
            message_text.text = p_msg.GetMessage();
            message_canvas.gameObject.SetActive(true);
            return;
        }

        StartGamer();

    }

    void StartGamer()
    {

        if(menu_email != "" && menu_password != "")
        {
            // Gravar os dados no PlayerPrefs para o preenchimento automatico
            PlayerPrefs.SetString("PLAYER_EMAIL", menu_email);
            PlayerPrefs.SetString("PLAYER_PASSWORD", menu_password);
        }
        else
        {
            Debug.Log("Erro ao tentar salvar E-mail e Senha no PlayerPrefs");
        }

        //se estiver tudo certo iniciar jogo
        MenuActive(menugame_canvas);
    }

    string CryptPassword(string p_password)
    {
        MD5 md5 = MD5.Create();
        string result = "";
        byte[] data = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(p_password));

        for (int i = 0; i < data.Length; i++)
        {
            result += data[i].ToString("x2");
        }

        Debug.Log(result);

        return result;
    }

    #endregion

    #region ################ LOGIN ##############

    void Button_Login()
    {
        bool err = false;

        string email_temp = login_email.text;
        string pw_temp = login_password.text;

        if(email_temp == "")
        {
            message_text.text = "Digite um e-mail!";
            err = true;
        }

        if (pw_temp == "" && err == false)
        {
            message_text.text = "Digite uma senha!";
            err = true;
        }


        if (err == true)
        {
            message_canvas.gameObject.SetActive(true); 
            return;
        }

        menu_email = email_temp;
        menu_password = pw_temp;

        pw_temp = CryptPassword(pw_temp);

        //envio REST
        Login_send(email_temp, pw_temp);
    }

    //chama a janela de cadastro
    void Button_Login_NewRegister()
    {
        MenuActive(register_canvas);
    }

    #endregion

    #region ############ MESAGEM ###################

    //Fecha nossa janela de mensagem
    void Button_Message_Close()
    {

        message_canvas.gameObject.SetActive(false);

    }

    #endregion

    #region ############ REGISTER ###################

    void Button_registre()
    {
        bool err = false;

        string email_temp = register_email.text;
        string pw_temp = register_password.text;
        string rpw_temp = register_repassword.text;

        if(email_temp == "" && !err)
        {
            message_text.text = "Digite um e-mail";
            err = true;
        }

        if(pw_temp == "" && !err)
        {
            message_text.text = "Digite uma senha!";
            err = true;
        }

        if(rpw_temp == "" && !err)
        {
            message_text.text = "Digite a senha novamente!";
            err = true;
        }

        if(pw_temp != rpw_temp && !err)
        {
            message_text.text = "A senha não confere!";
            err = true;
        }

        if (err)
        {
            message_canvas.gameObject.SetActive(true);
            return;
        }

        menu_email = email_temp;
        menu_password = pw_temp;

        pw_temp = CryptPassword(pw_temp);

        //Aqui vai chamar a função de envio dos dados para o Servidor que vai ativar ou não a proxima janela
        Register_send(email_temp, pw_temp);
    }

    //função retornar ao login
    void Button_RegisterBack()
    {
        MenuActive(login_canvas);
    }

    #endregion

    #region ############ REST FUNCTIONS ###################

    void Login_send(string p_email, string p_password)
    {
        rest_script.SendRestGetLogin(p_email, p_password,GetMessage);
    }

    void Register_send(string p_email, string p_password)
    {
        rest_script.SendRestPostRegister(p_email, p_password, GetMessage);
    }

    #endregion

}
