using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class libRecordBookState 
{
    public static bool[] position1_ankerBl=new bool[4];
    public static int nowBookTP_num = 0;
}
public class bookToTP : MonoBehaviour
{
    public Transform book1;
    public Transform book2;
    public Transform book3;
    public Transform book4;

    public Animator book_1_outline;
    public Animator book_2_outline;
    public Animator book_3_outline;
    public Animator book_4_outline;

    Vector3 book_1_putPosition = new Vector3(-0.05f, -1.9f, -3.18f);//Vector3(-0.0500000007,-1.89999998,-3.18000007)
    Vector3 book_2_putPosition = new Vector3(-0.71f, -1.75f, -4.3f);//Vector3(-0.709999979,-1.75,-4.30000019)
    Vector3 book_3_putPosition = new Vector3(0.66f, -1.95f, -3.35f);//Vector3(0.660000026,-1.95000005,-3.3599999)
    Vector3 book_4_putPosition = new Vector3(-0.11f, -1.9f, -4.5f);//Vector3(-0.109999999,-1.89999998,-4.5)
    Vector3 ro_1 = new Vector3(0,90,0);
    Vector3 ro_2 = new Vector3(0, 0, 0);
    Vector3 ro_3 = new Vector3(0, 180.6f, 0);
    Vector3 ro_4 = new Vector3(0, 90, 0);

    public Animator book_1_icon;
    public Animator book_2_icon;
    public Animator book_3_icon;
    public Animator book_4_icon;

    
    private Transform[] book = new Transform[5];
    private Animator[] bookOutline = new Animator[5];
    Vector3[] bookPutPosition = new Vector3[5];
    Vector3[] bookPutRotate = new Vector3[5];
    private Animator[] bookIcon = new Animator[5];

    bool tpToNextBl = false;//傳送至下一關 
    bool showOutlineBl = true;//出現outline提示
    bool takeBookBl = false;//拿取書本
    bool putBookBl = false;//書本放置
    public static bool tp_nextSceneStart = false;

    public Transform player;
    
    bool doorIsOpen = false;
    public Transform door;
    public Transform outDoorTpPoint;
    public AudioSource openDoorSound;
    public AudioSource TP_lastSceneSound;
    // Start is called before the first frame update
    private void Awake()
    {
        Resources.UnloadUnusedAssets();
    }
    void Start()
    {
        
        staticReset();
        Initiate_bookArray();
        recordBookPosition();


    }
    void recordBookPosition() 
    {
        if (libRecordBookState.nowBookTP_num >= 1) { book[0].position = bookPutPosition[0]; }
        if (libRecordBookState.nowBookTP_num >= 2) { book[1].position = bookPutPosition[1]; }
        if (libRecordBookState.nowBookTP_num >= 3) { book[2].position = bookPutPosition[2]; }
        if (libRecordBookState.nowBookTP_num >= 4) { book[3].position = bookPutPosition[3]; }
    }

    void Initiate_bookArray()
    {
        book[0] = book1;
        book[1] = book2;
        book[2] = book3;
        book[3] = book4;

        bookOutline[0] = book_1_outline;
        bookOutline[1] = book_2_outline;
        bookOutline[2] = book_3_outline;
        bookOutline[3] = book_4_outline;

        bookPutPosition[0] = book_1_putPosition;
        bookPutPosition[1] = book_2_putPosition;
        bookPutPosition[2] = book_3_putPosition;
        bookPutPosition[3] = book_4_putPosition;

        bookIcon[0] = book_1_icon;
        bookIcon[1] = book_2_icon;
        bookIcon[2] = book_3_icon;
        bookIcon[3] = book_4_icon;

        bookPutRotate[0] = ro_1;
        bookPutRotate[1] = ro_2;
        bookPutRotate[2] = ro_3;
        bookPutRotate[3] = ro_4;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        TP_lastScene();
        outSideAndTpToLast_start();
        debugLog();
        TP_nextScene();
        showOutlineEffect();
        takeBook();
        putBook();
        
        
    }
    void debugLog() 
    {
        Debug.Log(libRecordBookState.nowBookTP_num);
    }
    float distanceToBook()//玩家與未拿取的書本距離 
    {
        float distance = Vector3.Distance(book[libRecordBookState.nowBookTP_num].position, player.position);
        return distance;
    }
    void showOutlineEffect()//出現outline提示 
    {
        if (showOutlineBl)
        {
            if (distanceToBook() < 2)
            {
                bookOutline[libRecordBookState.nowBookTP_num].SetBool("showToBlink", true);
                bookOutline[libRecordBookState.nowBookTP_num].SetBool("blinkToHide", false);
            }
            else 
            {
                bookOutline[libRecordBookState.nowBookTP_num].SetBool("showToBlink", false);
                bookOutline[libRecordBookState.nowBookTP_num].SetBool("blinkToHide", true);
            }
                
        }
        else
        {
            bookOutline[libRecordBookState.nowBookTP_num].SetBool("showToBlink", false);
            bookOutline[libRecordBookState.nowBookTP_num].SetBool("blinkToHide", true);
        }
    }

    [System.Obsolete]
    void TP_nextScene()//傳送至下一關 
    {
        if (tpToNextBl)
        {
            libRecordBookState.nowBookTP_num += 1;
            fps_start_lib.GameToNextScene = true;
            tpToNextBl = false;

        }
        if (tp_nextSceneStart) 
        {
            Application.LoadLevel(libRecordBookState.nowBookTP_num);//傳送各場景
            //tp_nextSceneStart = false;
        }
        
    }
    void takeBook() //拿取書本
    {
        if (distanceToBook() < 2&&putBookBl==false) { takeBookBl=true; }
        if (takeBookBl&&Input.GetKeyDown(KeyCode.E))
        {
            bookIcon[libRecordBookState.nowBookTP_num].SetBool("show",true);//左下角出現ICON
            bookIcon[libRecordBookState.nowBookTP_num].SetBool("hide", false);//左下角出現ICON
            book[libRecordBookState.nowBookTP_num].gameObject.SetActive(false);//書本隱藏
            
            
        }
        if (bookIcon[libRecordBookState.nowBookTP_num].GetCurrentAnimatorStateInfo(0).IsName("leftDownCornerHint_stay"))
            {
                showOutlineBl = false;
                putBookBl = true;
                takeBookBl = false;
            }
    }
    public AudioSource putSound;
    public GameObject[] bookPutHint = new GameObject[4];
    void putBook()//書本放置 
    {
        float distanceFromPutPosition= Vector3.Distance(bookPutPosition[libRecordBookState.nowBookTP_num], player.position);
        if (putBookBl == true && distanceFromPutPosition <= 2)
        {
            bookPutHint[libRecordBookState.nowBookTP_num].SetActive(true);
            //放置處的設定提示(顯示)?
        }
        else 
        {
            bookPutHint[libRecordBookState.nowBookTP_num].SetActive(false);
            ////放置處的設定提示(隱藏)?
        }
        if (putBookBl&& Input.GetKeyDown(KeyCode.E)&& distanceFromPutPosition <= 2) //要加上玩家與放置處的距離
        {
            putSound.Play();
            bookPutHint[libRecordBookState.nowBookTP_num].SetActive(false);
            bookIcon[libRecordBookState.nowBookTP_num].SetBool("show", false);
            bookIcon[libRecordBookState.nowBookTP_num].SetBool("hide", true);
            book[libRecordBookState.nowBookTP_num].gameObject.SetActive(true);
            book[libRecordBookState.nowBookTP_num].position = bookPutPosition[libRecordBookState.nowBookTP_num];
            book[libRecordBookState.nowBookTP_num].rotation = Quaternion.Euler(bookPutRotate[libRecordBookState.nowBookTP_num]);
            libRecordBookState.position1_ankerBl[libRecordBookState.nowBookTP_num] = true;
           
            tpToNextBl = true; 
            putBookBl = false;
        }
        
    }
    void staticReset()//傳送下一場景後，參數重製 
    {
        tpToNextBl = false;//傳送至下一關 
        showOutlineBl = true;//出現outline提示
        takeBookBl = false;//拿取書本
        putBookBl = false;
        tp_nextSceneStart = false;
    }
    bool isDoorSoundPlay = false;
    public Animator doorOpneAn;
    void TP_lastScene() //
    {
        if (libRecordBookState.nowBookTP_num == 4&&Vector3.Distance(player.position,door.position)<=4f)
        {
            if (!isDoorSoundPlay) 
            {
                openDoorSound.Play();
                isDoorSoundPlay = true;
            }
            doorOpneAn.SetBool("open", true);//加上開門動畫
            doorIsOpen = true;//放在開完門後
            
        }
        
    }
    bool isLastSoundPlay = false;
    void outSideAndTpToLast_start() 
    {
        if (doorIsOpen == true&&Vector3.Distance(player.position,outDoorTpPoint.position)<=4f)//加上踏出門之後 
        {
            if (!isLastSoundPlay) 
            {
                TP_lastSceneSound.Play();
                isLastSoundPlay = true;
            }            
            fps_start_lib.GameToLastScene=true;//傳送至勝利們
        }
        
    }
    public Camera cam;
    public bool IsInView(Vector3 worldPos)
     {
         Transform camTransform = cam.transform;
         Vector2 viewPos = cam.WorldToViewportPoint(worldPos);
         Vector3 dir = (worldPos - camTransform.position).normalized;
         float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面
 
         if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
             return true;
         else
             return false;
     }
    
    //libRecordBookState.nowBookTP_num==4
    //
    
}
