using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolControl : MonoBehaviour
{
    [SerializeField] string _volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer _m;
    [SerializeField] Slider _s;
    [SerializeField] float _multiplier = 30f;
    [SerializeField] private Toggle _t;
    private bool _disableToggleEvent;

    private void Awake()
    {
        _s.onValueChanged.AddListener(HandleSliderValueChanged);
        _t.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    private void HandleToggleValueChanged(bool enableSound)
    {
        if (_disableToggleEvent)
            return;

        if (enableSound)
            _s.value = _s.maxValue;
        else
            _s.value = _s.minValue;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _s.value);
    }

    private void HandleSliderValueChanged(float value)
    {
       _m.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier); 
       _disableToggleEvent = true;
       _t.isOn = _s.value > _s.minValue;
       _disableToggleEvent = false;
    }

    void Start()
    {
        _s.value = PlayerPrefs.GetFloat(_volumeParameter, _s.value);
    }
}
