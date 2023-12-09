using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoxedMessage : MonoBehaviour
{
    public TMP_Text Message;
    public Button DefaultButton;
    public void closeMessage()
    {
       
        Destroy(this.gameObject);
    }
}
