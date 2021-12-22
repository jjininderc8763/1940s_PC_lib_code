using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_start_lib : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator panel_fadein;
    public CharacterController fps_move;
    public Camera fps_cam;
    public static bool GameThisSceneStart=true;//�ǰe�ܹϮ��]
    public static bool GameToLastScene = false;//�ǰe�ܳӧQ��
    public static bool GameToNextScene = false;//�ǰe�U�@�ӳ���
    // Start is called before the first frame update
    void Start()
    {
        game_start();
        GameThisSceneStart = true;//�ǰe�ܹϮ��]
        GameToLastScene = false;//�ǰe�ܳӧQ��
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
    void game_start()//�����_�l 
    {
        panel_fadein.SetBool("fadeIn_start", GameThisSceneStart);
        fps_move.GetComponent<fpsmovement>().enabled = false;
        fps_cam.GetComponent<mouselook>().enabled = false;
    }
    void detectStartOrNot()//�����O�_��}�l�ާ@ 
    {
        if (panel_fadein.GetCurrentAnimatorStateInfo(0).IsName("stay"))
        {
            fps_move.GetComponent<fpsmovement>().enabled = true;
            fps_cam.GetComponent<mouselook>().enabled = true;
        }
    }

    [System.Obsolete]
    void TP_lastScene() //�ǰe�ܳӧQ��(���X���f�A�ǰeGameToLastScene == true)
    {
        if (GameToLastScene == true) 
        {
                panel_fadein.SetBool("fadeIn_start", true);
                panel_fadein.SetBool("fadeOut_start", true);
            if (panel_fadein.GetCurrentAnimatorStateInfo(0).IsName("fadeoutTo_an"))
            {                
                Application.LoadLevel(5);//�ǰe�ܳӧQ��(���X���f�A�ǰeGameToLastScene == true)
            }            
        }
    }
   // public AudioSource TP_audioEffect;
    void TP_NextScene() //�ǰe�U�@�ӳ���
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
