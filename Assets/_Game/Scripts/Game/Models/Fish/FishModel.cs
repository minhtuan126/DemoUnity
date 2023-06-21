using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameCore.Models
{
    public enum FishState
    {
        Idle = 0,
        Swimming = 1,
        Collecting = 2,
        Commbat = 4,
    }


    public enum FishEvent
    {
        SpawnNewFish,
        FinishGetUserData,
        SpawnAllFish,
        SelectMainFish,
        PlayAnimEggPiece,
        SpawnNewFishInstance,
        RemoveFishInstance
    }

    public enum EResourceEvent
    {
        UpdateEggPieces,
        UpdatePearl,
        UpdateAllResource,
        UpdateResource
    }



    public struct StatFloat
    {
        public float value;
        public float maxValue;
    }

    public struct StatInt
    {
        public int value;
        public int maxValue;
    }


    [Serializable]
    public class FishDataConfig
    {
        public Sprite avatar;
        public Gen gen;
    }

    [Serializable]
    public class FishModel : EntityModel, IFishEntity
    {
        public string address;
        public int level;
        public int progress;
        public int tokenId;
        public Rarity rarity;
        public int category;
        public Gen gen;
        public int gender;
        public double lasted_fish_eat;
        public int egg_pieces;
        public int count_ep;
        public int count_eat;
        public string birthday;
        public string created_at;
        public string updated_at;
        public float spd;
        public float atk;
        public float hp;
        public float available_egg_pieces;

        // Local Data
        [NonSerialized]
        public Sprite avatar;

        public override void Init(EntityConfig config)
        {

        }
    }

}
