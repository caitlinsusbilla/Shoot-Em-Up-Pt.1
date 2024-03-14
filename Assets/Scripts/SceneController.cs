using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

    }

    public void onClickStart(){
        Debug.Log("clicked");
        SceneManager.LoadScene("SampleScene");
    }

    public void onClickCredit()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene("CreditScene");
        StartCoroutine(CreditTimer(5f)); 
    }

    IEnumerator CreditTimer(float delay)
    {
        Debug.Log("called");
        yield return new WaitForSeconds(delay);

        Debug.Log("Credit Timer Finished");
        SceneManager.LoadScene("MainMenu");
    }

}
