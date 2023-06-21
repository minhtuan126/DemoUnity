using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Sdk.Exceptions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;
using System.Text;
using MoralisUnity;
using Nethereum.Hex.HexConvertors.Extensions;
using Popup;

#pragma warning disable CS1998
//namespace MoralisUnity.Kits.AuthenticationKit
//{
/// <summary>
/// Moralis "kits" each provide drag-and-drop functionality for developers.
/// Developers add a kit at edit-time to give additional runtime functionality for users.
///
/// The "AuthenticationKit" provides cross-platform
/// support for web3 authentication (See <see cref="AuthenticationKitPlatform"/>).
///
/// Usage:
/// Drag-and-drop the <see cref="AuthenticationKit.prefab"/> into any Unity Scene.
///
/// This <see cref="AuthenticationKit"/> class is the wrapper for the <see cref="AuthenticationKitController"/>.
///
/// </summary>
public class AuthenticationKit : SingletonPersistent<AuthenticationKit>
{

        //  Properties ------------------------------------
        public bool WillInitializeOnStart
        {
            get { return _willInitializeOnStart; }
        }

        [Header("Settings")] [SerializeField] private bool _willInitializeOnStart = true;

        //  Events ----------------------------------------

        /// <summary>
        /// Invoked when State==AuthenticationKitState.MoralisLoggedIn
        /// </summary>
        [Header("Events")] public UnityEvent OnConnected = new UnityEvent();

        /// <summary>
        /// Invoked when State==AuthenticationKitState.Disconnected
        /// </summary>
        public UnityEvent OnDisconnected = new UnityEvent();

        /// <summary>
        /// Invoked upon any change to the <see cref="AuthenticationKitState"/>
        /// </summary>
        public AuthenticationKitStateUnityEvent OnStateChanged = new AuthenticationKitStateUnityEvent();

        /// <summary>
        /// Get the current <see cref="AuthenticationKitState"/>
        /// </summary>
        public AuthenticationKitState State
        {
            get { return _stateObservable.Value; }
            private set { _stateObservable.Value = value; }
        }

        private AuthenticationKitStateObservable _stateObservable = new AuthenticationKitStateObservable();

        //  Fields ----------------------------------------
        [Header("3rd Party")] [SerializeField] public WalletConnect _walletConnect;
        bool isNeedReconnect = false;
        //  Properties ------------------------------------
        /// <summary>
        /// Get the current <see cref="AuthenticationKitPlatform"/>
        /// </summary>
        public AuthenticationKitPlatform AuthenticationKitPlatform
        {
            get
            {
#if UNITY_ANDROID
                return AuthenticationKitPlatform.Android;
#elif UNITY_IOS
                return AuthenticationKitPlatform.iOS;
#elif UNITY_WEBGL
                return AuthenticationKitPlatform.WebGL;
#else
                return AuthenticationKitPlatform.WalletConnect;
#endif
            }
        }

        //  Unity Methods ---------------------------------
        public AuthenticationKit()
        {
            // Any state changes here are likely too 'early'
            // to be observed externally. That is ok. Just FYI.
            State = AuthenticationKitState.PreInitialized;
            _stateObservable.OnValueChanged.AddListener(StateObservable_OnValueChanged);
        }

        protected async void Start()
        {
            // Add the EventSystem if there is none
            if (FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            }

            if (_willInitializeOnStart)
            {
                await InitializeAsync();
            }
            WalletConnect.Instance.CLearSession();
        }

        /// <summary>
        /// Initialize the <see cref="AuthenticationKitController"/>.
        /// </summary>
        /// <returns></returns>
        public async UniTask InitializeAsync()
        {
            State = AuthenticationKitState.Initializing;

            // Initialize Moralis
            Moralis.Start();

            State = AuthenticationKitState.Initialized;
        }


        //  Methods ---------------------------------------

        /// <summary>
        /// Connect to Web3.
        /// </summary>
        public async void Connect()
        {
            //Debug.Log("tuan check Begin connect");
            //if (isNeedReconnect)
            //{
            //    await InitializeAsync();
            //}
            State = AuthenticationKitState.WalletConnecting;
        }

        /// <summary>
        /// User presses the retry button
        /// </summary>
        public void Retry()
        {
            // Based on which state we are one the Retry button does different things
            switch (State)
            {
                // If the Wallet is trying to connect
                case AuthenticationKitState.WalletConnecting:
                    switch (AuthenticationKitPlatform)
                    {
                        case AuthenticationKitPlatform.WalletConnect:
                            // With the QR code there is no need for a retry button
                            // TODO Add a manual refresh button if the QR is not working
                            break;
                        case AuthenticationKitPlatform.Android:
                            // Retry to open the DeepLink
                            _walletConnect.OpenDeepLink();
                            break;
                        case AuthenticationKitPlatform.iOS:
                            // Let users go back to the wallet select screen
                            State = AuthenticationKitState.WalletConnecting;
                            break;
                        case AuthenticationKitPlatform.WebGL:
                            // TODO Add a retry on a fail instead of disconnect and start over
                            break;
                        default:
                            SwitchDefaultException.Throw(AuthenticationKitPlatform);
                            break;
                    }

                    break;
                case AuthenticationKitState.WalletSigning:
                    switch (AuthenticationKitPlatform)
                    {
                        case AuthenticationKitPlatform.WalletConnect:
                            // TODO Add a retry option if the wallet fails. Chance of failure is small.
                            break;
                        case AuthenticationKitPlatform.Android:
                        case AuthenticationKitPlatform.iOS:
                            _walletConnect.OpenMobileWallet();
                            break;
                        case AuthenticationKitPlatform.WebGL:
                            // TODO Add a retry on a fail instead of disconnect and start over
                            break;
                        default:
                            SwitchDefaultException.Throw(AuthenticationKitPlatform);
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// LogIn to Web3 session.
        /// </summary>
        public async UniTask LoginWithWeb3()
        {
#if !UNITY_WEBGL
            new PlatformNotSupportedException();
#else
            string userAddr = "";

            // Try to sign and catch the Exception when a user cancels the request
            try
            {
                if (!Web3GL.IsConnected())
                {
                    await Moralis.SetupWeb3();
                    userAddr = Web3GL.Account();
                }
                else
                {
                    userAddr = Web3GL.Account();
                }
            }
            catch
            {
                // Disconnect and start over if a user cancels the connecting request or there is an error
                Disconnect();
                return;
            }

            State = AuthenticationKitState.WalletConnected;

            State = AuthenticationKitState.WalletSigning;

            if (string.IsNullOrWhiteSpace(userAddr))
            {
                Debug.LogError("Could not login or fetch account from web3.");
            }
            else
            {
                string address = Web3GL.Account().ToLower();
                string appId = Moralis.DappId;
                long serverTime = 0;

                // Retrieve server time from Moralis Server for message signature
                Dictionary<string, object> serverTimeResponse =
                    await Moralis.Cloud.RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

                if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
                    !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
                {
                    Debug.LogError("Failed to retrieve server time from Moralis Server!");
                }

                string signMessage = $"Moralis Authentication\n\nId: {appId}:{serverTime}";

                string signature = null;

                // Try to sign and catch the Exception when a user cancels the request
                try
                {
                    signature = await Web3GL.Sign(signMessage);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    // Disconnect and start over if a user cancels the singing request or there is an error
                    Disconnect();
                    return;
                }

                State = AuthenticationKitState.WalletSigned;

                State = AuthenticationKitState.MoralisLoggingIn;

                // Create Moralis auth data from message signing response.
                Dictionary<string, object> authData = new Dictionary<string, object>
                {
                    { "id", address }, { "signature", signature }, { "data", signMessage }
                };

                // Get chain Id
                int chainId = Web3GL.ChainId();

                // Attempt to login user.
                MoralisUser user = await Moralis.LogInAsync(authData, chainId);

                if (user != null)
                {
                    State = AuthenticationKitState.MoralisLoggedIn;
                }
            }
#endif
        }

        /// <summary>
        /// Handles WalletConnect connecting and setting up Web3
        /// </summary>
        /// <returns></returns>
        private async void WalletConnect_Connect()
        {
            // Enable auto save to remember the session for future use
            _walletConnect.autoSaveAndResume = true;

            // Warning the _walletConnect.Connect() won't finish until a user approved Wallet connection has been established
            await _walletConnect.Connect();

            // If WalletConnect is connected set the state to WalletConnected or Disconnect and start over
            if (_walletConnect.Connected)
            {
                State = AuthenticationKitState.WalletConnected;
            }
            else
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Handles WalletConnect signing when <see cref="WalletConnect"/> has a session connected.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private async void WalletConnect_SignAndLoginToMoralis(WalletConnectUnitySession session)
        {
            // Debug.Log($"WalletConnect_OnConnectedEventSession() wcSessionData = {session}");
            //Debug.Log("Tuan enter session data:" + session.Accounts[0].ToLower());
        // If there is already a Moralis user we can skip the sign and login and go straight to connected
        //if (await Moralis.GetUserAsync() != null)
        //{
        //    State = AuthenticationKitState.MoralisLoggedIn;
        //    return;
        //}
        if (session.ChainId!=97)
        {
            Disconnect();
            PopupManager.Instance.OnHideAllPopups();
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "Wrong network! Please switch to BSC testnet.",
                confirmText = "OK",
            }, true);
            return;
        }
        State = AuthenticationKitState.WalletSigning;
        Debug.Log("Tuan show chainId session data:" + session.ChainId);
        Debug.Log("Tuan enter session data:" + session.Accounts[0].ToLower());
            // Extract wallet address from the Wallet Connect Session data object.
            string address = session.Accounts[0].ToLower();
            string appId = Moralis.DappId;
            long serverTime = 0;

            // Retrieve server time from Moralis Server for message signature
            Dictionary<string, object> serverTimeResponse = await Moralis.Cloud
                .RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

            if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
                !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
            {
                Debug.LogError("Failed to retrieve server time from Moralis Server!");
            }

            string signMessage = $"Moralis Authentication\n\nId: {appId}:{serverTime}";
            //string dataHash = "aaaaa";
            //string signMessage = "0x" + Encoding.UTF8.GetBytes(dataHash).ToHex();
            string signature = null;

            // Try to sign and catch the Exception when a user cancels the request
            try
            {
                signature = await _walletConnect.Session.EthPersonalSign(address, signMessage);
                Debug.Log("tuan check:" + signature);
            }
            catch
            {
                // Disconnect and start over if a user cancels the singing request or there is an error
                Disconnect();
                return;
            }

            State = AuthenticationKitState.WalletSigned;

            State = AuthenticationKitState.MoralisLoggingIn;

            // Create Moralis auth data from message signing response.
            Dictionary<string, object> authData = new Dictionary<string, object>
            {
                { "id", address }, { "signature", signature }, { "data", signMessage }
            };
            ContractMgr.Instance.SetAccount(address);
            // Attempt to login user.
            MoralisUser user = await Moralis.LogInAsync(authData, session.ChainId);
            PlayerPrefs.SetString("Account", address);
            Debug.Log("Tuan check finish get user:" + address);

            if (user != null)
            {
                State = AuthenticationKitState.MoralisLoggedIn;
            }

        }
        public async void EnterGame()
        {
            //await _authenticationKit.SignMessageLogin();
            await SignMessageLogin();
            //_disconnectButton.gameObject.SetActive(true);
            bool loginSuccess = false;
            loginSuccess = await ContractMgr.Instance.GetNumberFishAsync();

            // load next scene
            if (loginSuccess)
                LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Game);
        }
        public async UniTask SignMessageLogin()
        {
            String signatureRSV = null;
            string dataHash = await APIManager.Instance.GetHashMessage();
            string signMessage1 = "0x" + Encoding.UTF8.GetBytes(dataHash).ToHex();

            Debug.Log("Tuan check account:" + ContractMgr.Instance.GetAccount());
            Debug.Log("Tuan check account:");
            //Debug.Log("Tuan enter session data:" + session.Accounts[0].ToLower());
            signatureRSV = await _walletConnect.Session.EthPersonalSign(ContractMgr.Instance.GetAccount(), signMessage1); ;
            signatureRSV = signatureRSV.Substring(2, signatureRSV.Length - 2);
            APIManager.r = "0x" + signatureRSV.Substring(0, 64);
            APIManager.s = "0x" + signatureRSV.Substring(64, 64);
            APIManager.v = "0x" + signatureRSV.Substring(128, 2);
            Debug.Log("tuan check:" + signatureRSV);
            //return "";
        }

        // If the user cancels the connect Disconnect and start over
        public async void WalletConnect_OnDisconnectedEvent(WalletConnectUnitySession session)
        {
            // Debug.Log("WalletConnect_OnDisconnectedEvent");

            // Only run if we are not already disconnecting
            if (!AuthenticationKitState.Disconnecting.Equals(State))
            {
                Disconnect();
            }
        }

        // If something goes wrong Disconnect and start over
        public async void WalletConnect_OnConnectionFailedEvent(WalletConnectUnitySession session)
        {
            // Debug.Log("WalletConnect_OnConnectionFailedEvent");

            // Only run if we are not already disconnecting
            if (!AuthenticationKitState.Disconnecting.Equals(State))
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Disconnect Moralis and WalletConnect.
        /// </summary>
        public async void Disconnect()
        {
            State = AuthenticationKitState.Disconnecting;
            isNeedReconnect = true;
        //WalletConnect.s_needReconnect = false;
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
                _walletConnect.CLearSession();

                // Disconnect the WalletConnect session
                await _walletConnect.Session.Disconnect();
                _walletConnect.CloseSession();
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

            State = AuthenticationKitState.Disconnected;
        }

        //  Event Handlers --------------------------------
        private async void StateObservable_OnValueChanged(AuthenticationKitState value)
        {
             Debug.Log("StateObservable_OnValueChanged " + value);
            // Order matters here.

            // 1. Broadcast
            OnStateChanged.Invoke(_stateObservable.Value);

            // 2. Step the state. Rarely.
            switch (_stateObservable.Value)
            {
                case AuthenticationKitState.WalletConnecting:

                    switch (AuthenticationKitPlatform)
                    {
                        case AuthenticationKitPlatform.Android:
                            Debug.Log("Enter android");
                            var cancellationTokenSourceAndroid = new CancellationTokenSource();
                            cancellationTokenSourceAndroid.CancelAfterSlim(TimeSpan.FromSeconds(15));

                            try
                            {
                            Debug.Log("tuan check begin connect android");
                            // Connect to the WalletConnect server
                            WalletConnect_Connect();

                                // Check if WalletConnect is ready in 15 seconds or else disconnect and start over
                                await UniTask.WaitUntil(() => _walletConnect.Session.ReadyForUserPrompt || _walletConnect.Connected,
                                    PlayerLoopTiming.Update, cancellationTokenSourceAndroid.Token);
                            Debug.Log("tuan check begin connect android");
                            if (_walletConnect.Session.ReadyForUserPrompt)
                                {
                                Debug.Log("tuan check begin connect ReadyForUserPrompt");
                                // Only works if a users has a app installed that handles "wc:" links
                                _walletConnect.OpenDeepLink();
                                }

                                // TODO check if the app is paused with OnApplicationPause to see if the link working
                            }
                            catch (OperationCanceledException ex)
                            {
                                Debug.Log("tuan check OperationCanceledException android");
                            if (ex.CancellationToken == cancellationTokenSourceAndroid.Token)
                                {
                                    // WalletConnect connection timeout so let's start over
                                    Disconnect();
                                }
                            }

                            break;
                        case AuthenticationKitPlatform.iOS:
                            var cancellationTokenSourceIOS = new CancellationTokenSource();
                            cancellationTokenSourceIOS.CancelAfterSlim(TimeSpan.FromSeconds(15));

                            try
                            {
                                // Connect to the WalletConnect server
                                WalletConnect_Connect();

                                // Check if WalletConnect is ready in 15 seconds or else disconnect and start over
                                await UniTask.WaitUntil(() => _walletConnect.Session.ReadyForUserPrompt || _walletConnect.Connected,
                                    PlayerLoopTiming.Update, cancellationTokenSourceIOS.Token);

                                // TODO check if the app is paused with OnApplicationPause to see if the link working
                            }
                            catch (OperationCanceledException ex)
                            {
                                if (ex.CancellationToken == cancellationTokenSourceIOS.Token)
                                {
                                    // WalletConnect connection timeout so let's start over
                                    Disconnect();
                                }
                            }
                            break;
                        case AuthenticationKitPlatform.WalletConnect:
                            // Connect to the WalletConnect server
                            WalletConnect_Connect();

                            break;
                        case AuthenticationKitPlatform.WebGL:
                            if (!Application.isEditor)
                            {
                                await LoginWithWeb3();
                            }

                            break;
                        default:
                            SwitchDefaultException.Throw(AuthenticationKitPlatform);
                            break;
                    }

                    break;

                case AuthenticationKitState.WalletConnected:

                    switch (AuthenticationKitPlatform)
                    {
                        case AuthenticationKitPlatform.Android:
                        case AuthenticationKitPlatform.iOS:
                        case AuthenticationKitPlatform.WalletConnect:

                            // If the Wallet connection has been accepted first Setup Web3
                            await Moralis.SetupWeb3();

                            // If there is a Wallet connected and we got a session
                            // try to Sign and Login to Moralis or else Disconnect and start over
                            if (_walletConnect.Session != null)
                            {
                                WalletConnect_SignAndLoginToMoralis(_walletConnect.Session);
                            }
                            else
                            {
                                Disconnect();
                            }

                            break;
                        case AuthenticationKitPlatform.WebGL:
                            // TODO Break up the LoginWithWeb3 method to a separate the connecting signing and logging
                            break;
                        default:
                            SwitchDefaultException.Throw(AuthenticationKitPlatform);
                            break;
                    }

                    break;

                case AuthenticationKitState.MoralisLoggedIn:

                    // Invoke OnConnected event
                    OnConnected.Invoke();
                    break;

                case AuthenticationKitState.Disconnected:

                    // Reset the UI so its like BEFORE we ever authenticated
                    await InitializeAsync();

                    // Invoke OnDisconnected event
                    OnDisconnected.Invoke();
                    break;

                default:
                    // Switch default is ok here since not all known conditions are declared above
                    break;
            }
        }
    }
//}
