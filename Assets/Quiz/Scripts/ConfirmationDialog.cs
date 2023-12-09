using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationDialog : MonoBehaviour
{
    private bool confirmed = false;

    public TMP_Text messageText;
   
    public void Show(string message)
    {
       
        messageText.text = message;
        gameObject.SetActive(true);
    }

    public void OnConfirm()
    {
      
        confirmed = true;
        gameObject.SetActive(false);
        
    }

    public void OnCancel()
    {
       
        confirmed = false;
        gameObject.SetActive(false);
    }

    public bool GetConfirmationResult()
    {
        return confirmed;
    }
}
