using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmMSlider;
    [SerializeField] private Slider _sfxSlider;

    public Slider MasterSlider { get => _masterSlider; private set => _masterSlider = value; }
    public Slider BGMSlider { get => _bgmMSlider; private set => _bgmMSlider = value; }
    public Slider SFXSlider { get => _sfxSlider; private set => _sfxSlider = value; }

    public enum Sliders
    {
        MasterSlider,
        BgmSlider,
        SfxSlider
    }

    public override void Init()
    {
        base.Init();

        MasterSlider.value = Managers.Sound.VolumeData._masterVolume;
        BGMSlider.value = Managers.Sound.VolumeData._bgmVolume;
        SFXSlider.value = Managers.Sound.VolumeData._sfxVolume;

        //MasterSlider.gameObject.AddUIEvent(MasterVolume, Define.UIEvent.Slider);
        //BGMSlider.gameObject.AddUIEvent(BGMVolume, Define.UIEvent.Slider);
        //SFXSlider.gameObject.AddUIEvent(SFXVolume, Define.UIEvent.Slider);

        if (MasterSlider.value <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("Master", -80);
        }
        Managers.Sound.audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
        //Managers.Sound._audioSources[(int)Define.Sound.Master].volume = MasterSlider.value;


        if (BGMSlider.value <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("BGM", -80);
        }
        Managers.Sound.audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        //Managers.Sound._audioSources[(int)Define.Sound.BGM].volume = BGMSlider.value;

        if (SFXSlider.value <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("SFX", -80);
        }
        Managers.Sound.audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);
        //Managers.Sound._audioSources[(int)Define.Sound.SFX].volume = SFXSlider.value;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void CloseClicked(PointerEventData data)
    {
        ClosePopUPUI();
    }
    //public void MasterVolume(PointerEventData data)
    //{
    //    DataManager.singleTon.VolumeData._masterVolume = MasterSlider.value;
    //    if (DataManager.singleTon.VolumeData._masterVolume <= -40f)
    //    {
    //        Managers.Sound.audioMixer.SetFloat("Master", -80);
    //    }
    //    Managers.Sound.audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
    //    //Managers.Sound._audioSources[(int)Define.Sound.Master].volume = MasterSlider.value;
    //}
    //public void BGMVolume(PointerEventData data)
    //{
    //    DataManager.singleTon.VolumeData._bgmVolume = BGMSlider.value;

    //    if (DataManager.singleTon.VolumeData._bgmVolume <= -40f)
    //    {
    //        Managers.Sound.audioMixer.SetFloat("BGM", -80);
    //    }
    //    Managers.Sound.audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
    //    //Managers.Sound._audioSources[(int)Define.Sound.BGM].volume = BGMSlider.value;
    //}
    //public void SFXVolume(PointerEventData data)
    //{
    //    DataManager.singleTon.VolumeData._sfxVolume = SFXSlider.value;

    //    if (DataManager.singleTon.VolumeData._sfxVolume <= -40f)
    //    {
    //        Managers.Sound.audioMixer.SetFloat("SFX", -80);
    //    }
    //    Managers.Sound.audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);
    //    //Managers.Sound._audioSources[(int)Define.Sound.SFX].volume = SFXSlider.value;
    //}
}
