using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsEnd : MonoBehaviour
{
    public float creditsLength = 5f;
    void Start()
    {
        StartCoroutine(Returnal());
    }

    private IEnumerator Returnal()
    {
        yield return new WaitForSeconds(creditsLength);
        SceneManager.LoadScene("Mainmenu");
    }
}
