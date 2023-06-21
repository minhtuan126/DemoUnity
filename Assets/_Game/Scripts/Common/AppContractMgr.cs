using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Web3Api.Models;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using UnityEngine;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;
using MoralisUnity.Kits.AuthenticationKit;
using System.Threading;

public class AppContractMgr : SingletonPersistent<AppContractMgr>
{
    
    //public MoralisController moralisController;
    [Header("3rd Party")] [SerializeField] private WalletConnect walletConnect;

    //  Properties ------------------------------------
    public bool WillInitializeOnStart
    {
        get { return _willInitializeOnStart; }
    }

    [Header("Settings")] [SerializeField] private bool _willInitializeOnStart = true;

    
    public void OpenWallet()
    {
        walletConnect.OpenMobileWallet();
    }
    // Start is called before the first frame update
    private async void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //walletConnect.autoSaveAndResume = true;
        // Warning the _walletConnect.Connect() won't finish until a Wallet connection has been established
        await InitializeAsync();
        //if (moralisController != null)
        //{
        //    Debug.Log("Init moralis");
        //    await moralisController.Initialize();
        //}
        //else
        //{
        //    Debug.LogError("MoralisController not found.");
        //}
    }
    public async UniTask InitializeAsync()
    {
        // Initialize Moralis
        Moralis.Start();
        MoralisUser user = await Moralis.GetUserAsync();
        //await walletConnect.Connect();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
#if !UNITY_WEBGL
    public void HandleWalletConnected()
    {
        Debug.Log("Connection successful. Please sign message");
    }
    #region WALLET_CONNECT
    public async void Connect()
    {
        walletConnect.autoSaveAndResume = true;
        await walletConnect.Connect();
        var cancellationTokenSourceAndroid = new CancellationTokenSource();
        cancellationTokenSourceAndroid.CancelAfterSlim(TimeSpan.FromSeconds(15));
        await UniTask.WaitUntil(() => walletConnect.Session.ReadyForUserPrompt || walletConnect.Connected,
                                    PlayerLoopTiming.Update, cancellationTokenSourceAndroid.Token);
        //if (walletConnect.Connected)
        //{
        //    await Moralis.SetupWeb3();
        //} else
        if (walletConnect.Session.ReadyForUserPrompt) {
            walletConnect.OpenDeepLink();
        }
        
        //await walletConnect.Connect();
    }
    public async void WalletConnect_OnConnectedEventSession(WCSessionData data)
    {
        if (await Moralis.GetUserAsync() != null)
        {
            return;
        }
        Debug.Log("Wallet connection received");
        //MoralisInterface.SetupWeb3();
        // Extract wallet address from the Wallet Connect Session data object.
        string address = data.accounts[0].ToLower();
        Debug.Log(address);
        // string appId = MoralisInterface.GetClient().ApplicationId;
        long serverTime = 0;
        string signatureRSV = null;
        // Retrieve server time from Moralis Server for message signature
        // Dictionary<string, object> serverTimeResponse = await MoralisInterface.GetClient().Cloud.RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

        // if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
        //     !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
        // {
        //     Debug.Log("Failed to retrieve server time from Moralis Server!");
        // }
        string dataHash = await APIManager.Instance.GetHashMessage();
        Debug.Log($"Sending sign request for {address} ...");
         Debug.Log($"Datahash {dataHash} was returned.");
        // string appId = Moralis.DappId;
        //string signMessage = $"AAAAAAAAA";
        //string signMessage = $"{dataHash}";
        string signMessage = "0x" + Encoding.UTF8.GetBytes(dataHash).ToHex();
        try
        {
            signatureRSV = await walletConnect.Session.EthPersonalSign(address, signMessage);
            Debug.Log($"tuan Signature {signatureRSV} for {address} was returned.");
            signatureRSV = signatureRSV.Substring(2, signatureRSV.Length - 2);
            APIManager.r = "0x" + signatureRSV.Substring(0, 64);
            APIManager.s = "0x" + signatureRSV.Substring(64, 64);
            APIManager.v = "0x" + signatureRSV.Substring(128, 2);
            // Create moralis auth data from message signing response.
            //Dictionary<string, object> authData = new Dictionary<string, object> { { "id", address }, { "signature", response }, { "data", signMessage } };

            Debug.Log("Logging in user:." + APIManager.r);
            Debug.Log("Logging in user:." + APIManager.s);
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
            // Create Moralis auth data from message signing response.
            //Dictionary<string, object> authData = new Dictionary<string, object>
            //{
            //    { "id", address }, { "signature", signatureRSV }, { "data", signMessage }
            //};

            //// Attempt to login user.
            //MoralisUser user = await Moralis.LogInAsync(authData, data.chainId.Value);
            PlayerPrefs.SetString("Account", address);
            //ContractMgr.Instance.SetAccount(address);

            bool loginSuccess = false;
            loginSuccess = await ContractMgr.Instance.GetNumberFishAsync();

            // load next scene
            if (loginSuccess)
                LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Game);
            // UserLoggedInHandler();
        } catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public async void WalletConnect_OnDisconnectedEvent(WalletConnectUnitySession session)
    {
        Debug.Log("WalletConnect_OnDisconnectedEvent");
        // Disconnect();
    }
    // If something goes wrong Disconnect and start over
    public async void WalletConnect_OnConnectionFailedEvent(WalletConnectUnitySession session)
    {
        Debug.Log("WalletConnect_OnConnectionFailedEvent");

        // Only run if we are not already disconnecting
        Disconnect();
    }


    public async void Disconnect()
    {
        Debug.Log("Tuan check Disconnect");
        try
        {
            // Logout the Moralis User.
            await Moralis.LogOutAsync();
        }
        catch (Exception e)
        {
            // Send error to the log but not as an error as this is expected behavior from W.C.
            Debug.LogError($"Disconnect() failed. Error: {e.Message}");
        }

#if !UNITY_WEBGL
        try
        {
            // CLear out the session so it is re-establish on sign-in.
            walletConnect.CLearSession();

            // Disconnect the WalletConnect session
            await walletConnect.Session.DisconnectSession("Session Disconnected", false);
        }
        catch (Exception e)
        {
            //Reason for Aborted warning is unknown, but expected. 
            if (e.Message != "Aborted")
            {
                // Send error to the log but not as an error as this is expected behavior from W.C.
                Debug.LogWarning($"[WalletConnect] Error = {e.Message}");
            }
        }
#endif
    }
    // If there is a new WalletConnect session setup Web3
    public async void WalletConnect_OnNewSessionConnected(WalletConnectUnitySession session)
    {
        // Debug.Log("WalletConnect_OnNewSessionConnected");

        await Moralis.SetupWeb3();
    }

    // If there is a resumed WalletConnect session setup Web3
    public async void WalletConnect_OnResumedSessionConnected(WalletConnectUnitySession session)
    {
        // Debug.Log("WalletConnect_OnResumedSessionConnected");

        await Moralis.SetupWeb3();
    }
    //public UniTask<string> SignContract2(string newOwner, int idType, int amount, int gold)
    //{
    //    Debug.Log("Transfer Item");
    //    Debug.Log("New Owner: " + newOwner);
    //    Debug.Log("IdType: " + idType);
    //    Debug.Log("amount " + amount);
    //    Debug.Log("gold " + gold);

    //    var transactionTransferRequest = new TransactionSignedUnityRequest(netUrl, privateKey);
    //    transactionTransferRequest.UseLegacyAsDefault = true;

    //    var info = new TransferItemToAddressFunction() { NewOwner = newOwner, IdType = idType, Amount = amount, Gold = gold };


    //    yield return transactionTransferRequest.SignAndSendTransaction(info, itemStorageAddress);

    //    var transactionTransferHash = transactionTransferRequest.Result;


    //}

    public async UniTask<string> SignContract(string useraddress, string contractAddress, string data)
    {
        TransactionData transactiondata = new TransactionData();
        transactiondata.from = useraddress.ToLower();
        transactiondata.to = contractAddress.ToLower();
        transactiondata.chainId = 97;
        transactiondata.gasPrice = "0";
        transactiondata.value = "0";
        transactiondata.gas = "0";
        transactiondata.nonce = "0";
        transactiondata.data = data;
        string response = await walletConnect.Session.EthSignTransaction(transactiondata);
        return response;
    }
    public async void LogOut()
    {
        await walletConnect.Session.Disconnect();
        walletConnect.CLearSession();

        //await MoralisInterface.LogOutAsync();
    }

    #endregion

    private async void UserLoggedInHandler()
    {
        Debug.Log("Logged");
        // save account for next scene

    }

    public void WalletConnectSessionEstablished(WalletConnectUnitySession session)
    {
        InitializeWeb3();
    }

    private void InitializeWeb3()
    {
        //MoralisInterface.SetupWeb3();
    }
#endif
}
