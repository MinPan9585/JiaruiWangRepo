using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public int everytimePushNum;
    public int pushNumCountIndex;
    public List<int> pushNumList;
    public List<INPUTTYPE> currentTimeInputList;
    public int pushNumCount;
    public int pushCount;//当次计数，用来判定玩家输入了几个
    public INPUTTYPE currentInputType;
    public bool hasJudged;
    //public bool canShowArrow;//输入条间隔显示
    public Queue<INPUTTYPE> inputQueue;

    private void Awake()
    {
        pushNumList = new List<int>() { 4,4 };
        everytimePushNum = 4;
        currentTimeInputList = new List<INPUTTYPE>();
        pushCount = 1;
        //canShowArrow = true;
    }

    public void ShowInputKey()
    {
        if(pushNumCountIndex >= pushNumList.Count)
        {
            Debug.Log("game over");
            return;
        }
        currentTimeInputList.Clear();
        for (int i = 0; i < everytimePushNum; i++)
        {
            currentTimeInputList.Add(inputQueue.Dequeue());
        }
        int setUIIndex = 0;
        foreach(var item in currentTimeInputList)
        {
            SetArrowSprite(setUIIndex, System.Convert.ToInt32(item));
            setUIIndex++;
        }
    }

    public void SetArrowSprite(int uiIndex, int arrowID)
    {

    }

    public void RandomInputType()
    {
        //int addNum = 6;
        for(int i = 0; i < pushNumList.Count; i++)
        {
            for (int j = 0; j < pushNumList[i]; j++)
            {
                inputQueue.Enqueue((INPUTTYPE)Random.Range(0, 4));
            }
        }
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            if (Event.current.isKey)
            {
                Judge(InputInfo());
            }
        }
    }

    public void Judge(INPUTTYPE inputType)
    {
        if(inputType == INPUTTYPE.NONE)
        {
            return;
        }
        Debug.Log(inputType);
    }

    public INPUTTYPE InputInfo()
    {
        INPUTTYPE input = INPUTTYPE.NONE;
        switch(Event.current.keyCode)
        {
            case KeyCode.W:
                input = INPUTTYPE.UP;
                break;
            case KeyCode.S:
                input = INPUTTYPE.DOWN;
                break;
            case KeyCode.A:
                input = INPUTTYPE.LEFT;
                break;
            case KeyCode.D:
                input = INPUTTYPE.RIGHT;
                break;
        }
        return input;
    }
}

public enum INPUTTYPE
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
}
