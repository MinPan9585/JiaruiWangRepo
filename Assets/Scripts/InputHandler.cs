using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public GameObject markImage;
    GameController gc;
    public Sprite[] reloadSprites;
    public Image reloadImage;
    //GameObject reloadPrefab;

    //public int everytimePushNum;
    public int keyListIndex;
    public List<int> pushNumList;
    public List<INPUTTYPE> currentTimeInputList;
    //public int pushNumCount;
    public int pushCount;//当次计数，用来判定玩家输入了几个
    public int totalPushCount;//总计数，用来判定玩家是否输入完了
    public INPUTTYPE currentInputType;
    bool hasJudged = false;
    //public bool canShowArrow;//输入条间隔显示
    public Queue<INPUTTYPE> inputQueue;

    public Sprite[] arrowSprites;
    public Sprite[] greenSprites;
    Image[] arrowImages;
    Transform[] arrowtrans;
    public GameObject[] arrowGOs;
    public Transform canvasTran;
    Color greenColor = new Color(0, 1, 0, 1);

    Vector3 startPos;
    Vector3 endPos;
    Vector3 judgeCenterPos;
    Transform signalTrans;
    Text judgeText;

    AudioSource audioS;
    public AudioClip[] sfx;

    private void Awake()
    {
        //reloadPrefab = Resources.Load<GameObject>("Prefabs/Reload");
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        pushNumList = new List<int>() { 4,4 };
        //everytimePushNum = 4;
        currentTimeInputList = new List<INPUTTYPE>();
        pushCount = 0;
        totalPushCount = 0;
        //canShowArrow = true;


        arrowImages = new Image[4];

        arrowtrans = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            arrowtrans[i] = canvasTran.GetChild(0).GetChild(i);
            arrowImages[i] = arrowtrans[i].GetComponent<Image>();
            //arrowGOs[i] = arrowtrans[i].gameObject;
        }

        //arrowSprites = new Sprite[4];
        //for(int i = 0; i < 4; i++)
        //{
        //    arrowSprites[i] = Resources.Load<Sprite>("Sprites/Arrows" + i.ToString());
        //}

        inputQueue = new Queue<INPUTTYPE>();
        RandomInputType();
        ShowInputKey();

        audioS = GetComponent<AudioSource>();
    }

    public void ShowInputKey()
    {
        if(keyListIndex >= pushNumList.Count)
        {
            Debug.Log("finish reload");
            gc.finishReload = true;
            this.transform.parent.gameObject.SetActive(false);
            return;
        }

        currentTimeInputList.Clear();
        for (int i = 0; i < 4; i++)
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
        //Debug.Log(arrowID);
        //Debug.Log(uiIndex);
        arrowImages[uiIndex].sprite = arrowSprites[arrowID];
    }

    //public void SetArrowColor(int uiIndex)
    //{
    //    arrowImages[uiIndex].color = new Color(0, 1, 0, 1); 
    //}

    public void SwitchSpriteColor(int uiIndex, int arrowID)
    {
        arrowImages[uiIndex].sprite = greenSprites[arrowID];
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
        if(inputType == INPUTTYPE.NONE || hasJudged)
        {
            print("none");
            return;
        }

        //Debug.Log(inputType);

        if(inputType == INPUTTYPE.SPACE)
        {
            if(pushCount >= currentTimeInputList.Count)//all buttons are pushed and are corrent
            {
                //give score and reset everything
                string judgeStr = "Correct!";
                keyListIndex++;
                markImage.SetActive(true);
                ResetState();
                
            }
            else // not correct or not finish all, miss
            {
                ResetState();
                string judgeStr = "Miss!";
            }
            //hasJudged = true;
        }
        else
        {
            if(pushCount >= currentTimeInputList.Count)//all buttons are pushed and are corrent
            {
                print("already finished, don't push key, push space");
                //hasJudged = true;
                return;
            }
            currentInputType = currentTimeInputList[pushCount];

            if(inputType == currentInputType)
            {
                print("correct");
                reloadImage.sprite = reloadSprites[totalPushCount];
                //SetArrowColor(pushCount);
                SwitchSpriteColor(pushCount, (int)currentInputType);
                totalPushCount++;
                pushCount++;
            }
            else
            {
                Debug.Log("Miss");
                string judgeStr = "Miss!";
                //pushCount = 0;
                //Instantiate(reloadPrefab);
                gc.redoReload = true;
                Destroy(this.transform.parent.gameObject);
                //reset everything
                //game over
                //ResetState();
                //Debug.Log("wrong, redo reload");
            }
        }
    }

    public void ResetState()
    {
        pushCount = 0;
        hasJudged = false;
        
        ShowInputKey();
    }


    public INPUTTYPE InputInfo()
    {
        
        INPUTTYPE input = INPUTTYPE.NONE;
        switch(Event.current.keyCode)
        {
            case KeyCode.W:
                input = INPUTTYPE.UP;
                audioS.PlayOneShot(sfx[0]);
                break;
            case KeyCode.S:
                input = INPUTTYPE.DOWN;
                audioS.PlayOneShot(sfx[0]);
                break;
            case KeyCode.A:
                input = INPUTTYPE.LEFT;
                audioS.PlayOneShot(sfx[0]);
                break;
            case KeyCode.D:
                input = INPUTTYPE.RIGHT;
                audioS.PlayOneShot(sfx[0]);
                break;
            case KeyCode.Space:
                input = INPUTTYPE.SPACE;
                audioS.PlayOneShot(sfx[1]);
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
    NONE,
    SPACE
}
