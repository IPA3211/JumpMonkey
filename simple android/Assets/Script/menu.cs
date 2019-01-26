using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    bool isOff = true;
    int page;
    public GameObject circle;
    public GameObject btnNext;
    public GameObject btnPrev;
    public float drawSpeed;

    public GameObject[] objButtons = new GameObject[8];

    [System.Serializable]
    public class Button
    {
        //public bool enable;
        [HideInInspector]
        public Vector3 position;
        public string name;
        public AdvencedButton.OnClickEvent onClick;
    }
    public Button[] buttons;

    // Use this for initialization
    void Start()
    {
        isOff = false;
        btnNext.SetActive(false);
        btnPrev.SetActive(false);

        page = 0;
        ButtonSetting();
    }

    public void ButtonSetting()
    {
        for (int i = 0; 8 > i; i++)
        {
            if (i + 8 * page >= buttons.Length)
                break;
            objButtons[i].GetComponentInChildren<Text>().text = buttons[i + 8 * page].name;
            objButtons[i].GetComponent<AdvencedButton>().onClickEvent = buttons[i + 8 * page].onClick;
        }
    }

    public void pageUp(bool up)
    {
        if (up)
        {
            if (buttons.Length - (page+1) * 8 > 0)
            {
                page++;
                StartCoroutine("Reset");
            }
        }
        else
        {
            if (page != 0)
            {
                page--;
                StartCoroutine("Reset");
            }
        }

    }

    public void DrawCircle()
    {
        Debug.Log("Draw");
        StartCoroutine("Draw");
        StartCoroutine("ShowBtn");
    }

    public void HideCircle()
    {
        StartCoroutine("Hide");
    }

    IEnumerator Reset()
    {
        StartCoroutine("HideBtn");
        yield return new WaitForSeconds(1f);
        ButtonSetting();
        StartCoroutine("ShowBtn");

    }

    IEnumerator Draw()
    {
        gameObject.SetActive(true);
        Debug.Log("Draw in");
        while (circle.GetComponent<Image>().fillAmount < 0.9999)
        {
            circle.GetComponent<Image>().fillAmount = Mathf.Lerp(circle.GetComponent<Image>().fillAmount, 1, drawSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("Draw Out");
    }

    IEnumerator Hide()
    {
        isOff = true;
        StartCoroutine("HideBtn");


        while (circle.GetComponent<Image>().fillAmount > 0.0001)
        {
            circle.GetComponent<Image>().fillAmount = Mathf.Lerp(circle.GetComponent<Image>().fillAmount, 0, drawSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);

    }
    IEnumerator ShowBtn()
    {

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; 8 > i; i++)
        {
            if (i + 8 * page >= buttons.Length)
                break;
            objButtons[i].SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }

        if (buttons.Length - 8 > 0)
        {
            btnNext.SetActive(true);
            btnPrev.SetActive(true);
        }
        else
        {
            btnNext.SetActive(false);
            btnPrev.SetActive(false);
        }

    }
    IEnumerator HideBtn()
    {

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; 8 > i; i++)
        {
            objButtons[i].GetComponent<AdvencedButton>().FadeOut();
            yield return new WaitForSeconds(0.1f);
        }

        if (buttons.Length - 8 > 0 && !isOff)
        {
            btnNext.SetActive(true);
            btnPrev.SetActive(true);
        }
        else
        {
            btnNext.GetComponent<AdvencedButton>().FadeOut();
            btnPrev.GetComponent<AdvencedButton>().FadeOut();
        }

    }
}
