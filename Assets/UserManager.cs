using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserManager : MonoBehaviour
{
    public UserInfo[] listofUsers;
    public UserButtonScript UBS;
    public UserInfo activeUser;
    public GameObject YesNoDialog;
    public BoxedMessage message;
    public AddDialogScript AddDialog;
    public PanelManager PM;
    public ProgressManager pm;
    public EnterPassword pwPanel;
    public TMP_Text usernameDisplay;
    public Button QuizForStudent;
    public Button QuizEditTeacher;
    
    private void Start()
    {
        listofUsers = DataSaver.LoadUser();
        LoadUsers();
    }
    public void LoadUsers() 
    {
        foreach (UserInfo  u in listofUsers) 
        {
            GameObject userbut = Instantiate(UBS.gameObject);
            UserButtonScript ubs = userbut.GetComponent<UserButtonScript>();
            if (u.teacher) 
            {
                userbut.GetComponent<UserButtonScript>().Name.text = u.username+" (Teacher)";
            }
            else 
            {
                userbut.GetComponent<UserButtonScript>().Name.text = u.username+" (Student)";
            }
            userbut.transform.SetParent(transform);
            userbut.transform.localScale = new Vector3(1, 1, 1);
            ubs.GetComponent<Button>().onClick.AddListener(delegate { SelectUser(u); });
            ubs.DeleteButton.GetComponent<Button>().onClick.AddListener(delegate { RemoveUserInfo(u.username, userbut); });
        }
    }
    public void SelectUser(UserInfo ui)
    {
        if (ui.teacher) 
        {
            pwPanel.gameObject.SetActive(true);
            pwPanel.UserToEnter = ui;
        }
        else 
        {
            activeUser = ui;
            usernameDisplay.text = "Welcome, "+ui.username;
            QuizForStudent.gameObject.SetActive(ui.excerciseDone);
            QuizEditTeacher.gameObject.SetActive(false);
            pm.setProgress();
            PM.ChangeSection(1);
            
        }
        
    }
    public void RemoveUserInfo(string userName, GameObject addbut)
    {
        // Instantiate the confirmation dialog prefab
        ConfirmationDialog dialog = Instantiate(YesNoDialog).GetComponent<ConfirmationDialog>();
            
        // Show a message in the dialog
        dialog.Show("Are you sure you want to remove this user?");

        // Check the result of the dialog
        StartCoroutine(WaitForConfirmationAndRemove(dialog, userName, addbut));
    }
    private IEnumerator WaitForConfirmationAndRemove(ConfirmationDialog dialog, string userName, GameObject addbut)
    {
        while (!dialog.GetConfirmationResult())
        {
            yield return null; // Wait for user input
        }

        // Process removal based on the confirmation result
        if (dialog.GetConfirmationResult())
        {
            // Find the index of the user with the specified userName
            int userIndex = -1;
            for (int i = 0; i < listofUsers.Length; i++)
            {
                if (string.Equals(listofUsers[i].username, userName, StringComparison.OrdinalIgnoreCase))
                {
                    userIndex = i;
                    break;
                }
            }

            // If the user was found, remove them from the array
            if (userIndex >= 0)
            {
                List<UserInfo> userList = new List<UserInfo>(listofUsers);
                userList.RemoveAt(userIndex);
                listofUsers = userList.ToArray();
              //  DataSaver.SaveUserInfo(GM.User);
                Destroy(addbut);
                DataSaver.SaveUserInfo(listofUsers);
            }
        }

        // Destroy the dialog
        Destroy(dialog.gameObject);
    }
    public void UserAddingList(UserInfo newUser) 
    {
        GameObject msg;
        if (string.IsNullOrWhiteSpace(newUser.username))
        {

            message.Message.text = "Username cannot be empty or contain only whitespace...";
             msg = Instantiate(message.gameObject);
            return;
        }

        if (listofUsers.Any(listofUsers => string.Equals(listofUsers.username, newUser.username, StringComparison.OrdinalIgnoreCase)))
        {
            message.Message.text = newUser.username + " user name already existed, try different Name...";
             msg = Instantiate(message.gameObject);
            return;
        }
        if(newUser.teacher && string.IsNullOrWhiteSpace(newUser.teacherPassword)) 
        {
            message.Message.text = "Teacher is required to assign a password for their accout...";
             msg = Instantiate(message.gameObject);
            return;
        }
      

        // Add the new UserInfo to the array
        Array.Resize(ref listofUsers, listofUsers.Length + 1);
        listofUsers[^1] = newUser;
        GameObject addbutton = Instantiate(UBS.gameObject);
        UserButtonScript ubs = addbutton.GetComponent<UserButtonScript>();
        if (newUser.teacher) 
        {
            addbutton.GetComponent<UserButtonScript>().Name.text = newUser.username+"(Teacher)";
        }
        else 
        {
            addbutton.GetComponent<UserButtonScript>().Name.text = newUser.username + "(Student)";
        }
        
        addbutton.transform.SetParent(this.transform);
        addbutton.transform.localScale = new Vector3(1, 1, 1);
        ubs.GetComponent<Button>().onClick.AddListener(delegate { SelectUser(newUser); });
        ubs.DeleteButton.GetComponent<Button>().onClick.AddListener(delegate { RemoveUserInfo(newUser.username, addbutton); });
        AddDialog.clear();
        AddDialog.gameObject.SetActive(false);
        message.Message.text = "New user created...";
        msg = Instantiate(message.gameObject);
        DataSaver.SaveUserInfo(listofUsers);

    }
    
}
