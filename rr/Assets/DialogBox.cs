using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogBox : MonoBehaviour
{

    private int page = 0;
    public enum InfoType { INFO_IDLE, INFO_FINISH };

    public GameObject nameSpace;
    public GameObject dataSpace;
    public GameObject InfoSpace;

    public KeyCode key;

    [System.Serializable]
    public class OnFinishEvent : UnityEvent { }
    public OnFinishEvent onFinishEvent = new OnFinishEvent();

    [System.Serializable]
    public class DialogData
    {
        public string name;
        [TextArea]
        public string data;
        [HideInInspector]
        public InfoType infoType = InfoType.INFO_IDLE;
    }

    public DialogData[] dialogData;

    // Use this for initialization
    void Start()
    {
        page = 0;
        ChangeData();

        dialogData[dialogData.Length-1].infoType = InfoType.INFO_FINISH;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) == true)
        {
            if (page < dialogData.Length-1)
            {
                page++;
                ChangeData();
            }
            else{
                OnDialogDataFinished();
            }
        }
    }

    void ChangeData(){
        nameSpace.GetComponent<Text>().text = dialogData[page].name;
        dataSpace.GetComponent<Text>().text = dialogData[page].data;
        string info;

        if (dialogData[page].infoType == InfoType.INFO_IDLE)
            info = ("\"" + key.ToString() + "\"를 눌러 넘어가기");
        else if (dialogData[page].infoType == InfoType.INFO_FINISH)
            info = ("\"" + key.ToString() + "\"를 눌러 끝내기");
        else
            info = "";

        InfoSpace.GetComponent<Text>().text = info;
    }
        
    void OnDialogDataFinished(){
        onFinishEvent.Invoke();
    }
}
