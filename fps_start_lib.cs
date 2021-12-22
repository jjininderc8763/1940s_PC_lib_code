using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_start_lib : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator panel_fadein;
    public CharacterController fps_move;
    public Camera fps_cam;
    public static bool GameThisSceneStart=true;//傳送至圖書館
    public static bool GameToLastScene = false;//傳送至勝利門
    public static bool GameToNextScene = false;//傳送下一個場景
    // Start is called before the first frame update
    void Start()
    {
        game_start();
        GameThisSceneStart = true;//傳送至圖書館
        GameToLastScene = false;//傳送至勝利門
        GameToNextScene = false;
}

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        detectStartOrNot();
        TP_lastScene();
        TP_NextScene();
            //if (Input.GetKeyDown(KeyCode.E)) {
            //    GameToLastScene = true;
            //}
    }
    void game_start()//場景起始 
    {
        panel_fadein.SetBool("fadeIn_start", GameThisSceneStart);
        fps_move.GetComponent<fpsmovement>().enabled = false;
        fps_cam.GetComponent<mouselook>().enabled = false;
    }
    void detectStartOrNot()//偵測是否能開始操作 
    {
        if (panel_fadein.GetCurrentAnimatorStateInfo(0).IsName("stay"))
        {
            fps_move.GetComponent<fpsmovement>().enabled = true;
            fps_cam.GetComponent<mouselook>().enabled = true;
        }
    }

    [System.Obsolete]
    void TP_lastScene() //傳送至勝利門(走出門口，傳送GameToLastScene == true)
    {
        if (GameToLastScene == true) 
        {
                panel_fadein.SetBool("fadeIn_start", true);
                panel_fadein.SetBool("fadeOut_start", true);
            if (panel_fadein.GetCurrentAnimatorStateInfo(0).IsName("fadeoutTo_an"))
            {                
                Application.LoadLevel(5);//傳送至勝利門(走出門口，傳送GameToLastScene == true)
            }            
        }
    }
   // public AudioSource TP_audioEffect;
    void TP_NextScene() //傳送下一個場景
    {
        if (GameToNextScene == true)
        {
            //TP_audioEffect.Play();
            panel_fadein.SetBool("fadeIn_start", false);
            if (panel_fadein.GetCurrentAnimatorStateInfo(0).IsName("fadeIn_idle"))
            {
                fps_move.GetComponent<fpsmovement>().enabled = false;
                fps_cam.GetComponent<mouselook>().enabled = false;
                bookToTP.tp_nextSceneStart = true;
                GameToNextScene = false;
            }
        } 
    }
}
