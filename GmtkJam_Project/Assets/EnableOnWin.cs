using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Finish.Instance().BindOnFinishedEvent(()=>{gameObject.SetActive(true);});
        gameObject.SetActive(false);
    }

}
