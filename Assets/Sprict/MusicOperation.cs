using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicOperation : MonoBehaviour
{
    #region singleton

    public static MusicOperation instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private AudioClip[] BGMclip = null;

    //  BGM    

    [SerializeField]
    private AudioClip[] SEclip = null;

    //  SE
    private const int shotSE = 0;     

    [SerializeField]
    private int soundSouceNum = 10;

    public AudioSource bgmSpeaker = null;

    [NonSerialized]
    public List<AudioSource> seSpeaker = null;

    [SerializeField]
    private void Start()
    {
        seSpeaker = new List<AudioSource>();

        for (int i = 0; i < soundSouceNum; i++)
            seSpeaker.Add(gameObject.AddComponent<AudioSource>());
    }

    public void SetBGM(string nameBGM)
    {
        //switch (nameBGM)
        //{
        //    case "title_zone":
        //        bgmSpeaker.clip = BGMclip[titleBGM];
        //        break;
        //    case "selectStage_TheTranquilityOfTheWaterSurface":
        //        bgmSpeaker.clip = BGMclip[selectStageBGM];
        //        break;
        //    case "stage1_DeepScience":
        //        bgmSpeaker.clip = BGMclip[stage1BGM];
        //        break;
        //    default:
        //        break;
        //}
    }

    public void PlayBGM()
    {
        bgmSpeaker.Play();
    }

    public void StopBGM()
    {
        bgmSpeaker.Stop();
    }

    int count = 0;

    public void SetPlaySE(string nameSE)
    {
        if (seSpeaker.Count <= count) count = 0;

        switch (nameSE)
        {
            case "Shot":
                seSpeaker[count].clip = SEclip[shotSE];
                break;
            default:
                break;
        }

        seSpeaker[count++].Play();
    }
}
