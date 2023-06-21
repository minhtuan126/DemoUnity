using UnityEngine;
using UnityEngine.UI;
using WalletConnectSharp.Unity;
using WalletConnectSharp.Core.Models;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Numerics;

public class HomeUI : MonoBehaviour
{
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    [DllImport("__Internal")]
    private static extern string WaitingFinishLogin();

    [DllImport("__Internal")]
    private static extern void WebRequestAccountConnect();

    private int expirationTime;
    private string account;
#endif
    [SerializeField]
    private Button _startGameButton;
    [SerializeField]
    private GameObject _openEggAnim;

    //public MoralisController moralisController;
    public WalletConnect walletConnect;

    private bool _isStarting = false;
    // Start is called before the first frame update
    private async void Start()
    {
#if UNITY_WEBGL
        _startGameButton.onClick.AddListener(OnLogin);
#else
        //Nethereum.Hex.HexTypes.HexBigInteger newSalt = new Nethereum.Hex.HexTypes.HexBigInteger(long.Parse("1656780199748"));
        //Debug.Log(newSalt.ToHexByteArray().ToHex());
        //string hexValue = "0x" + long.Parse("1656780199748").ToString("X");
        //Debug.Log(hexValue);
        //_startGameButton.onClick.AddListener(OnLogin);
        //if (moralisController != null)
        //{
        //    await moralisController.Initialize();
        //}
        //else
        //{
        //    Debug.LogError("MoralisController not found.");
        //}
#endif
    }

#if UNITY_WEBGL
    async private void OnConnected()
    {
        try {
            account = ConnectAccount();
            while (account == "")
            {
                await new WaitForSeconds(1f);
                account = ConnectAccount();
            };
            // save 
            string data = await APIManager.Instance.GetHashMessage();
            string signatureRSV = await Web3GL.Sign(data);
            signatureRSV = signatureRSV.Substring(2, signatureRSV.Length - 2);
            APIManager.r = "0x" + signatureRSV.Substring(0, 64);
            APIManager.s = "0x" + signatureRSV.Substring(64, 64);
            APIManager.v = "0x" + signatureRSV.Substring(128, 2);
            // save account for next scene
            PlayerPrefs.SetString("Account", account);
            PlayerPrefs.SetString("IsLogout", "0");
            // reset login message
            SetConnectAccount("");
            if (account.Length == 42)
            {
                // save account
                PlayerPrefs.SetString("Account", account);
                ContractMgr.Instance.SetAccount(account);
                bool loginSuccess = false;
                loginSuccess = await ContractMgr.Instance.GetNumberFishAsync();
                // load next scene
                if (loginSuccess)
                    LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Game);
                else
                {
                    _openEggAnim.SetActive(false);
                    _startGameButton.interactable = true;
                }
            }
            // load next scene
        } catch(Exception ex) {
             _openEggAnim.SetActive(false);
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "Cannot connect to BSC server!",
                confirmText = "OK"
            }, true);
        } 
    }


    public void OnSkip()
    {
        // burner account for skipped sign in screen
        PlayerPrefs.SetString("Account", "");
        // move to next scene
    }
#endif
    private void OnStartGame()
    {
        if (_isStarting) return;
        _isStarting = true;

        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Game);
    }
    public void Logout()
    {
        Extensions.LogOut();
    }
    async public void OnLogin()
    {
        _openEggAnim.SetActive(true);
        _startGameButton.interactable = false;
#if UNITY_WEBGL
        try
        {
            if(PlayerPrefs.HasKey("IsLogout"))
            {
                var data = PlayerPrefs.GetString("IsLogout");
                if (data == "1")
                {
                    WebRequestAccountConnect();
                    string checking = WaitingFinishLogin();
                    while (checking == "")
                    {
                        await new WaitForSeconds(1f);
                        checking = WaitingFinishLogin();
                    };
                }
            }
            Web3Connect();
            OnConnected();
        }
        catch (System.Exception e)
        {
            _openEggAnim.SetActive(false);
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "Cannot connect to BSC server!",
                confirmText = "OK"
            }, true);
        }


        


        //}
#else
        string data = await APIManager.Instance.GetHashMessage();
        string signatureRSV = await Web3Wallet.Sign(data);
        // get current timestamp
        // int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // // set expiration time
        // int expirationTime = timestamp + 60;
        // // set message
        // string message = expirationTime.ToString();
        // // sign message
        // string signature = await Web3Wallet.Sign(message);
        // // verify 
        Debug.Log("tuan data hash:" + data);
        string account = await EVM.Verify(data, signatureRSV);
        // int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42)
        {
            // string data = await APIManager.Instance.GetHashMessage();
            // string signatureRSV = await Web3Wallet.Sign(data);
            signatureRSV = signatureRSV.Substring(2, signatureRSV.Length - 2);
            APIManager.r = "0x" + signatureRSV.Substring(0, 64);
            APIManager.s = "0x" + signatureRSV.Substring(64, 64);
            APIManager.v = "0x" + signatureRSV.Substring(128, 2);
            // string result = await APIManager.Instance.RequestLogin(account, APIManager.v, APIManager.r, APIManager.s);
            // save account
            PlayerPrefs.SetString("Account", account);
            ContractMgr.Instance.SetAccount(account);

            bool loginSuccess = false;
            loginSuccess = await ContractMgr.Instance.GetNumberFishAsync();

            // load next scene
            if (loginSuccess)
                LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Game);
            else
            {
                _openEggAnim.SetActive(false);
                _startGameButton.interactable = true;
            }
        }
        else
        {
            _openEggAnim.SetActive(true);
        }
#endif
    }
#if !UNITY_WEBGL
    public void HandleWalletConnected()
    {
        Debug.Log("Connection successful. Please sign message");
    }

#region WALLET_CONNECT

    public async void WalletConnectHandler(WCSessionData data)
    {
        Debug.Log("Wallet connection received");
        // Extract wallet address from the Wallet Connect Session data object.
        string address = data.accounts[0].ToLower();
        Debug.Log(address);
        // string appId = MoralisInterface.GetClient().ApplicationId;
        long serverTime = 0;

        // Retrieve server time from Moralis Server for message signature
        // Dictionary<string, object> serverTimeResponse = await MoralisInterface.GetClient().Cloud.RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

        // if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
        //     !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
        // {
        //     Debug.Log("Failed to retrieve server time from Moralis Server!");
        // }
        string dataHash = await APIManager.Instance.GetHashMessage();
        Debug.Log($"Sending sign request for {address} ...");

        //string signMessage = $"Moralis Authentication\n\nId: {appId}:{dataHash}";
        string signatureRSV = await walletConnect.Session.EthPersonalSign(address, dataHash);

        Debug.Log($"Signature {signatureRSV} for {address} was returned.");
        signatureRSV = signatureRSV.Substring(2, signatureRSV.Length - 2);
        APIManager.r = "0x" + signatureRSV.Substring(0, 64);
        APIManager.s = "0x" + signatureRSV.Substring(64, 64);
        APIManager.v = "0x" + signatureRSV.Substring(128, 2);
        // Create moralis auth data from message signing response.
        //Dictionary<string, object> authData = new Dictionary<string, object> { { "id", address }, { "signature", response }, { "data", signMessage } };

        Debug.Log("Logging in user.");

        // Attempt to login user.
        // MoralisUser user = await MoralisInterface.LogInAsync(authData);

        //if (user != null)
        //{
        //    Debug.Log($"User {user.username} logged in successfully. ");
        //}
        //else
        //{
        //    Debug.Log("User login failed.");
        //}
        PlayerPrefs.SetString("Account", address);
        ContractMgr.Instance.SetAccount(address);

        bool loginSuccess = false;
        loginSuccess = await ContractMgr.Instance.GetNumberFishAsync();

        // load next scene
        if (loginSuccess)
            LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Game);
        else
        {
            _openEggAnim.SetActive(false);
            _startGameButton.interactable = true;
        }
        // UserLoggedInHandler();
    }

    //public async void LogOut()
    //{
    //    await walletConnect.Session.Disconnect();
    //    walletConnect.CLearSession();

    //    await MoralisInterface.LogOutAsync();
    //}

#endregion

    private async void UserLoggedInHandler()
    {
        Debug.Log("Logged");
        // save account for next scene
        
    }
#endif
}
