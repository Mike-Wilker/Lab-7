using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    const int ESCAPE_FINE = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            Destroy(other.gameObject);
            FindObjectOfType<HUD>().money -= ESCAPE_FINE;
            if(FindObjectOfType<HUD>().money < 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                FindObjectOfType<HUD>().transform.Find(@"Bottom Panel/Money").GetComponent<Text>().text = new string('$', FindObjectOfType<HUD>().money);
            }
        }
    }
}
