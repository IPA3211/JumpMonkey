using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AdvencedButton : MonoBehaviour
    , IPointerClickHandler // 2
    , IPointerDownHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerUpHandler


{

    [Tooltip("Size will s")]
    [Range(0, 2)]
    public float onPushSize;
    [Range(0, 1)]
    public float spd_onPushSize;

    public Color onPushColor;
    [Range(0, 1)]
    public float spd_onPushColor;
    [Space]
    [Range(0, 2)]
    public float onHighlightSize;
    [Range(0, 1)]
    public float spd_onHighlightsize;

    public Color onHighlightColor;
    [Range(0, 1)]
    public float spd_onHighlightColor;

    [System.Serializable]
    public class OnClickEvent : UnityEvent { }
    public OnClickEvent onClickEvent = new OnClickEvent();

    private Vector3 idleScale;
    private bool isMouseOver;
    private bool isMouseDown;
    private bool isFadeOut;
    Color idleColor;
    Color currentColor;

    /// <summary>
    /// 
    /// under : foundation func of button
    /// 
    /// </summary>

    // Use this for initialization

    public void Reset()
    {
        onPushSize = 0.8f;
        spd_onPushSize = 0.2f;
        onPushColor = new Color(0.8f, 0.8f, 0.8f);
        spd_onPushColor = 0.2f;

        onHighlightSize = 1.2f;
        spd_onHighlightsize = 0.1f;
        onHighlightColor = new Color(1, 1, 0.8f);
        spd_onHighlightColor = 0.1f;
    }
    public void Awake()
    {
        idleScale = transform.localScale;
        isMouseOver = false;
        isMouseDown = false;
        isFadeOut = false;

        idleColor = gameObject.GetComponent<Image>().color;
        transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        FadeIn();
    }

    private void OnDisable()
    {
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = gameObject.GetComponent<Image>().color;
        if (isMouseOver == false && isMouseDown == false && isFadeOut == false)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, idleScale, spd_onHighlightsize);
            gameObject.GetComponent<Image>().color = Color.Lerp(currentColor, idleColor, spd_onHighlightColor);
        }
        if(isMouseOver == true && isFadeOut == false){
            if (!isMouseDown && !isFadeOut)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, idleScale * onHighlightSize, spd_onHighlightsize);
                gameObject.GetComponent<Image>().color = Color.Lerp(currentColor, onHighlightColor, spd_onHighlightColor);
            }
            else if (isMouseDown)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, idleScale * onPushSize, spd_onPushSize);
                gameObject.GetComponent<Image>().color = Color.Lerp(currentColor, onPushColor, spd_onPushColor);
            }

        }
        else if (isFadeOut)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, spd_onPushSize);
            gameObject.GetComponent<Image>().color =
                Color.Lerp(currentColor, new Color(currentColor.r, currentColor.g, currentColor.b, 0), spd_onHighlightColor);

            if (currentColor.a < 0.1)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void FadeOut() { isFadeOut = true; }
    public void FadeIn() { isFadeOut = false; }

    public void OnPointerClick(PointerEventData eventData)
    {
        //todo : write func when is clicked
        onClickEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isMouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMouseDown = false;
    }

}
