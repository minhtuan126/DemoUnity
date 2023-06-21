using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using UnityEngine;
using Newtonsoft.Json;
using Random = UnityEngine.Random;
using UnityEngine.Networking;
using System.Text;
using Popup;

public class APIManager : SingletonPersistent<APIManager>
{
    public const string APIGameServer = "https://core.testnet.fishme.io/api/";
    public const string APIGetUserInfo = "get-infomartion";
    public const string APIChangInfo = "change-infomartion";
    public const string APIGetUserFish = "list-fish-all";
    public const string APIGetConfig = "game-config";
    public const string APISellFish = "sell-fish";
    public const string APIFeedFish = "fish-eat";
    public const string APIFeedFishFull = "action-eat-full-fish";
    public const string APIGetLeaderboard = "top-fish-leaderboard";
    public const string APIReloadFish = "reload-fish";
    public const string APIGetCencalSellFish = "cancel-sell-fish";
    public const string APIGetListSellFish = "list-sell-fish";
    public const string APIChangeNameFish = "change-name-fish";
    public const string APIBreedFish = "breed-fish";
    public const string APIHatchEgg = "hatch-fish";
    public const string APIExchangeEgg = "exchange-egg";
    public const string APIGetHashMessage = "message-sign";
    public const string APIRequestLogin = "sign-login";
    public const string APIGetRateBreedFish = "rate-breed-fish";
    public const string APISuccessExchangeEgg = "success-exchange-egg";
    public const string APIFeedAll = "action-eat-all-fish";
    public const string addressKey = "address";
    public const string tokenEggKey = "tokenEgg";
    public const string tokenIdKey = "tokenId";
    public const string tokenMotherIdKey = "motherID";
    public const string tokenFatherIdKey = "fatherID";
    public const string rKey = "r";
    public const string sKey = "s";
    public const string vKey = "v";

    public static string r;
    public static string s;
    public static string v;


    [SerializeField] private List<ApiRecord> ApiRecords;
    // [SerializeField]
    // private string _accountAdress = "0x2dc53A942c006f309c2B1c76683B3f27a3296313";

    public async UniTask<string> APIPost(string key, Dictionary<string, string> fields,  bool isShowAlertPopup = true)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(APIGameServer);
        sb.Append(key);
        try
        {
            var request = await UnityWebRequest.Post(sb.ToString(), fields);
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text.ToString());
            ApiRecord record;
            record.message = request.downloadHandler.text.ToString();
            record.isSuccess = request.result == UnityWebRequest.Result.Success;
            record.parameters = new List<KeyValue>();
            foreach (var item in fields)
            {
                record.parameters.Add(new KeyValue(item.Key,item.Value));
            }
            this.ApiRecords.Add(record);
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);

                string mess = "request.error";
                try
                {
                    ApiRecord error = JsonConvert.DeserializeObject<ApiRecord>(record.message);
                    mess = error.message;
                } catch (Exception)
                {
                    Debug.Log("Not parse");
                }
                if (isShowAlertPopup) {
                    PopupManager.Instance.OnShowPopup(PopupType.APIFail, new ConfirmPopupData()
                    {
                        status = mess,
                        confirmText = "OK",
                    }, false);
                }
                return null;
                // if (isShowAlertPopup)
                // {
                //     PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                //     {
                //         status = mess,
                //         confirmText = "OK"
                //     }, true);
                // }

            }
            else
            {
                // return null;
                // Debug.Log(request.downloadHandler.text);
                this.ApiRecords.Add(record);
                return request.downloadHandler.text;
            }
            
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            return null;
        }
        return null;
    }

    public async UniTask<string> APIPut(string key, Dictionary<string, string> fields, bool isShowAlertPopup = true)
    {
        ApiRecord record;
        StringBuilder sb = new StringBuilder();
        sb.Append(APIGameServer);
        sb.Append(key);
        // Debug
        record.parameters = new List<KeyValue>();
        foreach (var item in fields)
        {
            record.parameters.Add(new KeyValue(item.Key,item.Value));
        }
        //
        string data = Extension.GetPUTFormDataFromDictionary(fields);
        // Debug.Log(data);
        Debug.Log(data);
        byte[] encodedData = System.Text.Encoding.UTF8.GetBytes(data);

        try
        {
            var request = await UnityWebRequest.Put(sb.ToString(), encodedData);
            request.SetRequestHeader("content-type", "application/x-www-form-urlencoded");
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            await request.SendWebRequest();
            record.message = request.downloadHandler.text.ToString();
            record.isSuccess = request.result == UnityWebRequest.Result.Success;
            this.ApiRecords.Add(record);
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed");
                if (isShowAlertPopup)
                {
                    PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                    {
                        title = "NOTICE",
                        status = "Cannot feed fish!",
                        confirmText = "OK"
                    }, true);
                }
                return null;
            }
            else
            {
                Debug.Log("CCL:" + request.downloadHandler.text);
                return request.downloadHandler.text;
            }
        }
        catch (Exception e)
        {
            Debug.Log("CCLA:");
            record.isSuccess = false;
            Debug.LogWarning(e);
            return null;
        }

        return null;
    }

    public async UniTask<string> APIGet(string key)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(APIGameServer);
        sb.Append(key);
        try
        {
            var request = await UnityWebRequest.Get(sb.ToString());
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text.ToString());
            ApiRecord record;
            record.message = request.downloadHandler.text.ToString();
            record.isSuccess = request.result == UnityWebRequest.Result.Success;
            record.parameters = new List<KeyValue>();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
                PopupManager.Instance.OnShowPopup(PopupType.APIFail, new ConfirmPopupData()
                {
                    status = request.error,
                    confirmText = "OK",
                }, false);
            }
            else
            {
                return request.downloadHandler.text;
            }
            this.ApiRecords.Add(record);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
        return null;
    }

    public async UniTask<bool> FeedFish(FishModel fish)
    {
        Dictionary<string, string> formField = new Dictionary<string, string>();
        formField.Add("address", GameManager.UserData.address);
        formField.Add("tokenId", fish.tokenId.ToString());
        formField.Add("action", "rest");
        formField.Add(vKey, v);
        formField.Add(rKey, r);
        formField.Add(sKey, s);
        var strResponse = await this.APIPut(APIFeedFish, formField);
        if (strResponse != null)
        {
            try {
                UserDataAPI userInfo = JsonConvert.DeserializeObject<UserDataAPI>(strResponse);
                if (userInfo != null)
                {
                    GameManager.oldPearl = GameManager.UserData.energy;
                    GameManager.oldEggPiece = GameManager.UserData.numberEggPieces;
                    GameManager.UserData.pearl = userInfo.data.energy;
                    GameManager.UserData.eggPieces = userInfo.data.numberEggPieces;
                    //GameManager.UserData.fish_information = userInfo.data.fish_information;
                    GameManager.UserData.ReloadFish(userInfo.data.fish_information);
                    // MainGameUI.Instance.FieldUserData();
                }
                return true;
            } catch (Exception ex) {
                return false;
            }

        }
        else
        {
            return false;
        }

    }

    public async UniTask<bool> FeedFishFull(FishModel fish)
    {
        Dictionary<string, string> formField = new Dictionary<string, string>();
        formField.Add("address", GameManager.UserData.address);
        formField.Add("tokenId", fish.tokenId.ToString());
        formField.Add("action", "rest");
        formField.Add(vKey, v);
        formField.Add(rKey, r);
        formField.Add(sKey, s);
        var strResponse = await this.APIPost(APIFeedFishFull, formField);
        if (strResponse != null)
        {
            UserDataAPI userInfo = JsonConvert.DeserializeObject<UserDataAPI>(strResponse);
            if (userInfo != null)
            {
                GameManager.oldPearl = GameManager.UserData.energy;
                GameManager.oldEggPiece = GameManager.UserData.numberEggPieces;
                GameManager.UserData.pearl = userInfo.data.energy;
                GameManager.UserData.eggPieces = userInfo.data.numberEggPieces;
                GameManager.UserData.ReloadFish(userInfo.data.fish_information);
            }
            return true;
        }
        else
        {
            return false;
        }

    }

    public async UniTask<bool> FeedFishAll(List<int> arrTokenID)
    {
        Dictionary<string, string> formField = new Dictionary<string, string>();
        string tokenIDs = String.Join(",",arrTokenID);
        formField.Add("address", GameManager.UserData.address);
        formField.Add("tokenId", tokenIDs);
        formField.Add(vKey, v);
        formField.Add(rKey, r);
        formField.Add(sKey, s);
        var strResponse = await this.APIPost(APIFeedAll, formField);
        if (strResponse != null)
        {
            DataFeedAll userInfo = JsonConvert.DeserializeObject<DataFeedAll>(strResponse);
            if (userInfo != null)
            {
                GameManager.oldPearl = GameManager.UserData.energy;
                GameManager.oldEggPiece = GameManager.UserData.numberEggPieces;
                GameManager.UserData.pearl = userInfo.data.energy;
                GameManager.UserData.eggPieces = userInfo.data.numberEggPieces;
                GameManager.UserData.ReloadListFish(userInfo.data.fish_information);
            }
            return true;
        }
        else
        {
            return false;
        }

    }

    public async UniTask<bool> FeedFishAll(FishModel fish)
    {
        return true;
    }


    public async UniTask<UserData> GetUserInfo(string userDress)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(addressKey, userDress);
        var request = await APIPost(APIGetUserInfo, fromField);

        if (request != null)
        {
            UserDataAPI userInfo = JsonConvert.DeserializeObject<UserDataAPI>(request);
            if (userInfo != null)
                return userInfo.data;
        }
        return null;
    }

    public async UniTask<string> GetHashMessage()
    {
        var request = await APIGet(APIGetHashMessage);

        if (request != null)
        {
            DataString message = JsonConvert.DeserializeObject<DataString>(request);
            if (message != null)
                return message.data;
        }
        return null;
    }

    public async UniTask<string> RequestLogin(string userDress, string v, string r, string s)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(addressKey, userDress);
        fromField.Add(vKey, v);
        fromField.Add(rKey, r);
        fromField.Add(sKey, s);
        var request = await APIPost(APIRequestLogin, fromField, false);
        Debug.Log(request.ToString());
        if (request != null)
        {
            DataString message = JsonConvert.DeserializeObject<DataString>(request);
            if (message != null)
                return message.data;
        }
        return null;
    }

    public async UniTask<List<String>> HatchEgg(string userAddress, string tokenId)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(addressKey, userAddress);
        fromField.Add(tokenEggKey, tokenId);
        var request = await APIPost(APIHatchEgg, fromField);

        if (request != null)
        {
            try
            {
                DataHatchFish hatchInfo = JsonConvert.DeserializeObject<DataHatchFish>(request);
                if (hatchInfo != null)
                    return hatchInfo.data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        return null;
    }

    public async UniTask<List<String>> BreedFish(string userAddress, string motherId, string fatherId)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(addressKey, userAddress);
        fromField.Add(tokenFatherIdKey, fatherId);
        fromField.Add(tokenMotherIdKey, motherId);
        var request = await APIPost(APIBreedFish, fromField, false);
        if (request != null)
        {
            try
            {
                DataBreedFish hatchInfo = JsonConvert.DeserializeObject<DataBreedFish>(request);
                if (hatchInfo != null)
                {
                    Debug.Log(GameManager.UserData.energy);
                    Debug.Log(hatchInfo.user.energy);
                    GameManager.oldPearl = GameManager.UserData.energy;
                    GameManager.oldEggPiece = GameManager.UserData.numberEggPieces;
                    GameManager.UserData.pearl = hatchInfo.user.energy;
                    GameManager.UserData.eggPieces = hatchInfo.user.numberEggPieces;
                    return hatchInfo.data;
                }
            } catch(Exception ex)
            {
                Debug.Log("Errorrrrr");
                return null;
            }

        }
        return null;
    }

    public async UniTask<List<String>> ExchangeEgg(string userAddress)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(addressKey, userAddress);
        var request = await APIPost(APIExchangeEgg, fromField);
        if (request != null)
        {
            try
            {
                DataHatchFish hatchInfo = JsonConvert.DeserializeObject<DataHatchFish>(request);
                if (hatchInfo != null)
                    return hatchInfo.data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        return null;
    }

    public async UniTask<FishModel> ReloadFish(string tokenId)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(tokenIdKey, tokenId);
        var request = await APIPost(APIReloadFish, fromField);

        if (request != null)
        {
            FishModelData fishInfo = JsonConvert.DeserializeObject<FishModelData>(request);
            if (fishInfo != null)
                return fishInfo.data;
        }
        return null;
    }



    public async UniTask<List<FishModel>> GetUserFish(int[] fishTokens)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("-1");
        for (int i = 0; i < fishTokens.Length; i++)
        {
            sb.Append(",");
            sb.Append(fishTokens[i]);

        }
        Dictionary<string, string> formField = new Dictionary<string, string>();
        formField.Add("tokenId", sb.ToString());
        formField.Add("address", GameManager.UserData.address);
        var fishResult = await APIPost(APIGetUserFish, formField);
        if (!string.IsNullOrEmpty(fishResult))
        {
            FishAPIData fishData = JsonConvert.DeserializeObject<FishAPIData>(fishResult);
            return fishData.data;
        }
        return null;
    }

    public async UniTask GetGameConfig()
    {
        Dictionary<string, string> formField = new Dictionary<string, string>();
        var fishResult = await APIGet(APIGetConfig);

        if (!string.IsNullOrEmpty(fishResult))
        {
            GameConfigData config = JsonConvert.DeserializeObject<GameConfigData>(fishResult);
            GameData.Instance.gameConfig = config.data;
            GameData.Instance.epConfig = config.epConfig;
            DontDestroyOnLoad(GameData.Instance.gameObject);
        }

        return;
    }
    public async UniTask<List<UserData>> GetLeaderboard()
    {
        var request = await APIGet(APIGetLeaderboard);

        if (!string.IsNullOrEmpty(request))
        {
            ListUser users = JsonConvert.DeserializeObject<ListUser>(request);
            if (users != null)
                return  users.data;
        }
        return null;
    }

    // public async UniTask<> GetConfig()
    // {

    // }
    // public async UniTask<> SellFish()
    // {

    // }
    // public async UniTask<> GetCencalSellFish()
    // {

    // }
    // public async UniTask<> GetListSellFish()
    // {

    // }
    // public async UniTask<> ChangeNameFish()
    // {

    // }

    public async UniTask<FishModel> RequestAPIExchangeEgg(UserData userData)
    {
        return null;
    }

    public async UniTask<FishModel> RequestAPIMergeFish(UserData userData, int fishToken1, int fishToken2)
    {
        return null;
    }

    public async UniTask UpdateUserInfo(UserData userData)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add("address", userData.address);

        if (!string.IsNullOrEmpty(userData.name))
            fromField.Add("name", userData.name);
        fromField.Add("avatar_id", userData.avatar_id.ToString());
        await APIPost(APIChangInfo, fromField);
    }

    public async UniTask UpdateSoundSetting(UserData userData)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add("address", userData.address);
        fromField.Add("sound_setting", userData.sound_setting.ToString());
        fromField.Add("vfx_setting", userData.vfx_setting.ToString());
        await APIPost(APIChangInfo, fromField);
    }

    public async UniTask UpdateUserData(UserData userData)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add("address", userData.address);
        fromField.Add("name", userData.name);
        fromField.Add("sound_setting", userData.sound_setting.ToString());
        fromField.Add("vfx_setting", userData.vfx_setting.ToString());
        fromField.Add("address", userData.address);
        fromField.Add("avatar_id", userData.avatar_id.ToString());
        await APIPost(APIChangInfo, fromField);
    }

    public async UniTask<RatioBreed> GetRateBreedFish(int fatherID, int motherID)
    {
        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(tokenFatherIdKey, fatherID.ToString());
        fromField.Add(tokenMotherIdKey, motherID.ToString());
        var ratio = await APIPost(APIGetRateBreedFish, fromField);
        if (ratio != null)
        {
            DataRatioBreedFish ratioData = JsonConvert.DeserializeObject<DataRatioBreedFish>(ratio);
            if (ratioData.data != null)
                return ratioData.data;
        }
        return null;
    }

    public async UniTask<UserData> RequestSuccessExchangeEgg(string tokenID)
    {
        Dictionary<string, string> formField = new Dictionary<string, string>();
        formField.Add(addressKey, GameManager.UserData.address);
        formField.Add(tokenIdKey, tokenID);
        var request = await APIPost(APISuccessExchangeEgg, formField);

        if (request != null)
        {
            UserDataAPI userInfo = JsonConvert.DeserializeObject<UserDataAPI>(request);
            if (userInfo != null)
                return userInfo.data;
        }
        return null;
    }

    public async UniTask<ResourceStruct> UpdateResourceUser()
    {

        Dictionary<string, string> fromField = new Dictionary<string, string>();
        fromField.Add(addressKey, GameManager.UserData.address);
        StringBuilder sb = new StringBuilder();
        sb.Append(APIGameServer);
        sb.Append(APIGetUserInfo);
        try
        {
            var request = await UnityWebRequest.Post(sb.ToString(), fromField);
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                ResourceData resource = JsonConvert.DeserializeObject<ResourceData>(request.downloadHandler.text);
                if (resource != null)
                {
                    EventManager.Invoke(EResourceEvent.UpdateResource, resource.data);
                    return resource.data;
                }

            }
            else
            {
                PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                {
                    title = "NOTICE",
                    status = "Can not connect to network! Please reload game",
                    confirmText = "RELOAD",
                    confirmPopup = () => Extensions.LogOut(),
                }, true);
            }
        } catch (Exception ex)
        {
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "Can not connect to network! Please reload game",
                confirmText = "RELOAD",
                confirmPopup = () => Extensions.LogOut(),
            }, true);
        }
        return null;
    }

    public class FishAPIData
    {
        public List<FishModel> data;
    }

    public class ListUser
    {
        public List<UserData> data;
    }

    public class DataBreedFish
    {
        public UserData user;
        public List<string> data;
    }

    public class DataHatchFish
    {
        public List<string> data;
    }
    public class FishModelData
    {
        public FishModel data;
    }

    public class DataString
    {
        public string data;
    }
}


[System.Serializable]
public struct ApiRecord
{
    public bool isSuccess;
    public string message;
    public List<KeyValue> parameters;
    public ApiRecord(bool _isSuccess, string _message, List<KeyValue> _listKV)
    {
        this.isSuccess = _isSuccess;
        this.message = _message;
        parameters = _listKV;
    }

}

[System.Serializable]

public struct KeyValue
{
    public string key;
    public string value;
    public KeyValue(string _key, string _value)
    {
        key = _key;
        value = _value;
    }
}

[System.Serializable]
public class RatioBreed
{
    public int fee;
    public int common;
    public int great;
    public int rare;
    public int epic;
}

[System.Serializable]

public class DataRatioBreedFish
{
    public RatioBreed data;
}

[System.Serializable]
public class GameConfigData
{
    public GameConfigStruct data;
    public List<FishDefine> epConfig;
}

[System.Serializable]
public class GameConfigStruct
{
    public int id;
    public int fee;
    public int energy_price;
    public int nft_to_game_coin;
    public int hatching_egg_price;
    public int speed_up_price;
    public int time_to_next_feed;
    public int time_to_generate_coin;
    public int time_to_next_breed;
    public int sellerfee;
    public int buyerfee;
    public int busd_to_energy;
    public int time_to_energy;
    public int egg_pieces_to_egg;
    public int food_price;
    public int rate_success;
}

[System.Serializable]
public class FishDefine
{
    public int rarity;
    public int gen;
    public int limit_feed;
    public int egg_pieces;
    public int ep_limit;
}


[System.Serializable]
public class ResourceStruct
{
    public int energy;
    public int numberEggPieces;
}

[System.Serializable]
public class ResourceData
{
    public ResourceStruct data;
}