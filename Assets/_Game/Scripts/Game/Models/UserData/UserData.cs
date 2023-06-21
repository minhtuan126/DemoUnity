using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Models;

[Serializable]
public class UserData
{
    public int id = 4;
    public string address = "0x2dc53a942c006f309c2b1c76683b3f27a3296313";
    public string name = null;
    public int food = 0;
    public int water = 0;
    //public int energy = 0;
    public float sound_setting = 0.5f;
    public float vfx_setting = 0.5f;
    public int number_game_coin = 0;
    public int avatar_id;
    public string created_at = "";
    public string updated_at = "";
    public string deleted_at = null;
    public string activation_token = null;
    public int isApproveTranferMPS = 0;
    public int IsApproveDragonBreed = 0;
    public int isApproveMarketPlace = 0;
    public int isApproveTranferBUSD = 0;
    public int isApproveBuyEnergy = 0;
    public string coin_updated_at = null;
    public int isApproveTranferBG = 0;
    public string r = null;
    public string s = null;
    public string v = null;
    public int totalFish;
    //
    public int energy;
    public int numberEggPieces;

    // Runtime data
    public FishModel SelectedFish
    {
        get { return fishList[currentFish]; }
    }
    public List<int> TokensID
    {
        get; protected set;
    }
    // Feed Fish
    public FishModel fish_information;


    [NonSerialized]
    public Sprite avatar;
    public List<FishModel> fishList;
    public List<FishModel> eggList;

    public int currentFish = 0;

    public int pearl
    {
        set
        {
            GameManager.oldPearl = this.energy;
            this.energy = value;
        }
    }

    public int eggPieces
    {
        set
        {
            GameManager.oldEggPiece = this.numberEggPieces;
            this.numberEggPieces = value;
        }
    }

    public FishModel OnNextFish()
    {
        currentFish++;
        if (currentFish >= fishList.Count)
            currentFish = 0;
        return SelectedFish;
    }

    public FishModel OnBackFish()
    {
        currentFish--;
        if (currentFish < 0)
            currentFish = fishList.Count - 1;
        return SelectedFish;
    }

    public void SetAllFish(List<FishModel> allFish, int[] tokensID)
    {
        this.TokensID = new List<int>(tokensID);

        if (allFish == null || allFish.Count == 0) return;

        fishList = new List<FishModel>();
        eggList = new List<FishModel>();

        foreach (var model in allFish)
        {
            if (model.gen == Gen.Egg)
            {
                eggList.Add(model);
            }
            else
            {
                fishList.Add(model);
            }
        }
    }


    public void AddNewFish(FishModel newFish)
    {
        if (newFish == null) return;

        this.TokensID.Add(newFish.tokenId);

        if (newFish.gen == Gen.Egg)
        {
            FishAssetInit.Instance.InitEgg(ref newFish);
            eggList.Add(newFish);
        }
        else
        {
            FishAssetInit.Instance.InitFish(ref newFish);
            fishList.Add(newFish);
        }
    }


    public void ReloadFish(FishModel newFish)
    {
        var i = fishList.FindIndex(x => x.tokenId == newFish.tokenId);
        if (i >= 0)
        {
            fishList[i].count_eat = newFish.count_eat;
        }
    }

    public void ReloadListFish(List<FishModel> arrFishModel)
    {
        foreach(var fish in arrFishModel)
        {
            var fishFound = GameManager.UserData.fishList.Find(x => x.tokenId == fish.tokenId);
            if(fishFound != null)
            {
                Debug.Log("Found "+fishFound.tokenId);
                fishFound.count_eat = fish.count_eat;
            }
        }
    }
    public void RemoveFish(int token_id)
    {
        var i = fishList.FindIndex(x => x.tokenId == token_id);
        if (i >= 0)
        {
            fishList.RemoveAt(i);
        }
    }
    public void RemoveEgg(int token_id)
    {
        var i = eggList.FindIndex(x => x.tokenId == token_id);
        if (i >= 0)
        {
            eggList.RemoveAt(i);
        }
    }

    public int GetTotalRemainingFeedTimes()
    {
        var numberFeed = 0;
        foreach (var fish in this.fishList)
        {
            numberFeed += fish.count_eat;
        }
        return numberFeed;
    }

    public int GetRemainingFeedTimesOfish(FishModel fish)
    {
        var value = fish.count_eat;
        return value;
    }

    public int GetAvailableFeedTimeOfUser()
    {
        var numberFeed = (int)Mathf.Floor(this.energy / 10);
        return  numberFeed;
    }

    public List<int> GetListFishToFeedAll()
    {
        List<int> arrTokenID = new List<int>();
        var arrFishAvaliable = GameManager.UserData.fishList.FindAll(x => x.count_eat != 0);
        foreach(var fishAva in arrFishAvaliable)
        {
            arrTokenID.Add(fishAva.tokenId);
        }
        return arrTokenID;
    }
}
public class UserDataAPI
{
    public UserData data;
}

public class DataStructFeedAll
{
    public int energy;
    public int numberEggPieces;
    public List<FishModel> fish_information;
}
public class DataFeedAll
{
    public DataStructFeedAll data;
}
