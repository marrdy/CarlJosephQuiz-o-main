using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnterPassword : MonoBehaviour
{
    public UserInfo UserToEnter;
    public PanelManager pm;
    public UserManager um;
    [SerializeField] TMP_InputField pword;
    [SerializeField] BoxedMessage message;
    public void enterpassword() 
    {
        GameObject msg;
        if (string.Equals(UserToEnter.teacherPassword, pword.text, StringComparison.Ordinal))
        {
            um.activeUser = UserToEnter;
            um.usernameDisplay.text = "Welcome, " + UserToEnter.username;
            um.QuizForStudent.gameObject.SetActive(false);
            um.QuizEditTeacher.gameObject.SetActive(true);
            pm.ChangeSection(1);

            gameObject.SetActive(false);
        }
        else
        {
            message.Message.text = "The password is Wrong...";
            msg = Instantiate(message.gameObject);
        }
    }
}
