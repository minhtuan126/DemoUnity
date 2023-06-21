using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

public class FishAura : MonoBehaviour
{
    private readonly Color defaultColor = new Color(1,1,1,0);
    [SerializeField] private List<ParticleSystem> listPS = new List<ParticleSystem>();

    [SerializeField] private ParticleSystem psRing;
    [SerializeField] private ParticleSystem psSparkles;
    [SerializeField] private ParticleSystem psTrail;
    [SerializeField] private ParticleSystem.MainModule mainPSRing;
    [SerializeField] private ParticleSystem.MainModule mainPSSparkles;
    [SerializeField] private ParticleSystem.MainModule mainPSTrail;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // this.psRing = this.GetComponent<ParticleSystem>().main;
        // this.psSparkles = this.GetComponent<ParticleSystem>().main;
        // this.psTrail = this.GetComponent<ParticleSystem>().main;
        // this.psRing.startColor = this.defaultColor;
        // this.psSparkles.startColor = this.defaultColor;
        // this.psTrail.startColor = this.defaultColor;
    }
    public void SetColor(FishModel fish)
    {
                
        // this.mainPSRing = this.psRing.GetComponent<ParticleSystem>().main;
        // this.mainPSSparkles = this.psSparkles.GetComponent<ParticleSystem>().main;
        // this.mainPSTrail = this.psTrail.GetComponent<ParticleSystem>().main;
        // this.mainPSRing.startColor = this.defaultColor;
        // this.mainPSSparkles.startColor = this.defaultColor;
        // this.mainPSTrail.startColor = this.defaultColor;
        Color color = FishAura.GetColorByRarity(fish.rarity);
        // this.mainPSRing.startColor = color;
        // this.mainPSSparkles.startColor = color;
        // this.mainPSTrail.startColor = color;
        foreach(ParticleSystem ps in this.listPS)
        {
            var main  = ps.main;
            main.startColor = color;
        }
        // this.psRing.main.startColor = null;
    }

    public static Color GetColorByRarity(Rarity rarity)
    {
        switch(rarity)
        {
            case Rarity.Common: 
            
                return Color.white;

            case Rarity.Great: 
                return Color.green;

            case Rarity.Rare: 
                return Color.blue;

            case Rarity.Epic: 
                return Color.red;
            
            default:
                return new Color(1,1,1,0);
        }
    }
}
