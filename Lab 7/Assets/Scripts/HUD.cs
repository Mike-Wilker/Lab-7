using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    const int TURRET_PRICE = 2;
    const int SPIKES_PRICE = 1;
    const float MONEY_GAIN_DELAY = 40.0f;
    public const float END_TIME = 60.0f;
    public float researchRate = 4.0f;
    public int money = 4;
    public bool placing = false;
    public bool paused = false;
    public float moneyTimer = MONEY_GAIN_DELAY;
    public float gameTimer = 0.0f;
    void Update()
    {
        if (!placing && money >= 1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 128.0f, LayerMask.GetMask("Defense")) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider.GetComponent<Turret>())
                {
                    money--;
                    transform.Find("Bottom Panel/Money").GetComponent<Text>().text = new string('$', money);
                    Instantiate(Resources.Load(@"Prefabs\Double Turret"), hit.collider.transform.position, Quaternion.identity);
                    Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), hit.collider.transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Confirm"), Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1.25f));
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.GetComponent<Spikes>())
                {
                    money--;
                    transform.Find("Bottom Panel/Money").GetComponent<Text>().text = new string('$', money);
                    Instantiate(Resources.Load(@"Prefabs\Fire Trap"), hit.collider.transform.position, Quaternion.identity);
                    Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), hit.collider.transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Confirm"), Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1.25f));
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetButtonDown("Turret"))
        {
            OnClickTurret();
        }
        if (Input.GetButtonDown("Spikes"))
        {
            OnClickSpikes();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
        moneyTimer -= Time.deltaTime * researchRate;
        gameTimer += Time.deltaTime;
        if (gameTimer >= END_TIME)
        {
            SceneManager.LoadScene("Victory");
        }
        if (moneyTimer <= 0)
        {
            moneyTimer = MONEY_GAIN_DELAY;
            money++;
            transform.Find(@"Bottom Panel/Money").GetComponent<Text>().text = new string('$', money);
        }
        transform.Find(@"Top Panel/Completion Bar").GetComponent<Slider>().value = gameTimer / END_TIME;
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
        paused = true;
        Instantiate(Resources.Load(@"Menus\PauseMenu"), transform);
    }
    public void OnClickTurret()
    {
        if (money >= TURRET_PRICE &! placing &! paused)
        {
            placing = true;
            money -= TURRET_PRICE;
            transform.Find(@"Bottom Panel/Money").GetComponent<Text>().text = new string('$', money);
            Instantiate(Resources.Load(@"Prefabs\Preview_Turret"));
            Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), Camera.main.transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Confirm"), Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1.25f));
        }
        else
        {
            Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), Camera.main.transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\No"), Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1.25f));
        }
    }
    public void OnClickSpikes()
    {
        if (money >= SPIKES_PRICE &! placing &! paused)
        {
            placing = true;
            money -= SPIKES_PRICE;
            transform.Find(@"Bottom Panel/Money").GetComponent<Text>().text = new string('$', money);
            Instantiate(Resources.Load(@"Prefabs\Preview_Spikes"));
            Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), Camera.main.transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Confirm"), Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1.25f));
        }
        else
        {
            Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), Camera.main.transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\No"), Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1.25f));
        }
    }
    public void OnResearchSliderChanged()
    {
        researchRate = transform.Find(@"Bottom Panel/Research Slider").GetComponent<Slider>().value;
        switch(researchRate)
        {
            case 2.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Tiny Brain");
                break;
            case 3.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Normal Brain");
                break;
            case 4.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Big Brain");
                break;
            case 5.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Laser Brain");
                break;
            case 6.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Muscle Brain");
                break;
            case 7.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Galaxy Brain");
                break;
            case 8.0f:
                transform.Find(@"Bottom Panel/Research Level").GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Images\Ascended Brain");
                break;
            default:
                print("Woops");
                break;
        }
    }
}
