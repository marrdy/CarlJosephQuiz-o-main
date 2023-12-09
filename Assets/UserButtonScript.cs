using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserButtonScript : MonoBehaviour
{
    public TMP_Text Name;
    public Button DeleteButton;
    
    public void sounds()
    {
        FindAnyObjectByType<SMScript>().playtrack("pop");
    }
}
