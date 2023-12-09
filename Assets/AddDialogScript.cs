using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AddDialogScript : MonoBehaviour
{

    [SerializeField] UserInfo NewUser;
    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_Dropdown UserType;
    [SerializeField] TMP_InputField pword;
    [SerializeField] UserManager UM;
    private void Awake()
    {
        while (UM == null)
        {
            UM = FindAnyObjectByType<UserManager>();
        }

    }
    public void AddNewUser()
    {
        
        NewUser.username  =  username.text;
        NewUser.teacher = (UserType.value == 1) ? true : false;
        NewUser.excerciseDone = (UserType.value == 1) ? true : false;
        if (NewUser.teacher)
        {
            NewUser.teacherPassword = pword.text;
        }
        UM.UserAddingList(NewUser);
        
    }
    public void showHidePassword()
    {
        pword.gameObject.SetActive((UserType.value == 1) ? true : false);
    }
    public void clear()
    {
        username.text = "";
        pword.text = "";
        NewUser =new();
       
    }
}
