﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MC.Sound;

public class Gauge : MonoBehaviour
{
    public float _GaugeValue;

    PlayerFSMManager player;
    SphereCollider sphere;
    BoxCollider box;
    ParticleSystem particle;

    float _time;
    public float CoolTime = 10f;
    private void Awake()
    {
        player = PlayerFSMManager.Instance;

        sphere = GetComponent<SphereCollider>();
        box = GetComponentInChildren<BoxCollider>();

        particle = GetComponentInChildren<ParticleSystem>();

        particle.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (!box.gameObject.activeSelf)
        {
            _time += Time.deltaTime;

            if (_time >= CoolTime)
            {
                _time = 0;
                box.gameObject.SetActive(true);
                sphere.enabled = true;
                particle.gameObject.SetActive(false);
            }
        }
        
        
    }
    
    public void GaugePlayer()
    {
        if (player.SpecialGauge + _GaugeValue > 100)
            player.SpecialGauge = 100;
        else
            player.SpecialGauge += _GaugeValue;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GaugePlayer();
            sphere.enabled = false;
            box.gameObject.SetActive(false);
            particle.gameObject.SetActive(true);
            var sound = MCSoundManager.Instance.objectSound.objectSFX;
            sound.PlaySound(this.gameObject, sound.itemGet);
        }
    }
}
