using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using MoralisUnity;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Newtonsoft.Json;
using Popup;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nethereum.RPC.Eth.DTOs;
using WalletConnectSharp.Unity;
using System.Text;
using Nethereum.ABI.Encoders;
using Nethereum.Hex.HexConvertors.Extensions;

public class ContractMgr : SingletonPersistent<ContractMgr>
{
    public enum ContractState
    {
        Fail = 0,
        Success = 1,
    }
    private const string USER_ADRESS = "UserAdress";

    [SerializeField]
    string chain = "binance";
    [SerializeField]
    string chainId = "97";
    [SerializeField]
    string network = "testnet";
    public string fishContract = "0x9c92A4535012FF2806E670678Fd8fA6B0Dfb2a84";
    string marketContract = "0x4C40264f211b768Ba4a19f67c9528c310A0cD8F3";
    [SerializeField]
    string account = "0x6b2be2106a7e883f282e2ea8e203f516ec5b77f7";
    [SerializeField]
    public ContractState statusSuccessContract = ContractState.Success;
    //[SerializeField]
    //string method = "balanceOf";//"getTokensByOwner";
    private static string fishContractAbi = "[ {\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"approved\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"newBaseUri\",\"type\":\"string\"}],\"name\":\"BaseUriChangedEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"fatherId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"motherId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"childId\",\"type\":\"uint256\"}],\"name\":\"BirthEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"oldDuration\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"newDuration\",\"type\":\"uint256\"}],\"name\":\"BreedDurationChangeEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint16\",\"name\":\"generation\",\"type\":\"uint16\"}],\"name\":\"HatchEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"IAmFishMinted\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"fatherId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"motherId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"timestamp\",\"type\":\"uint256\"}],\"name\":\"IntercrossingEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"Paused\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"indexed\":true,\"internalType\":\"bytes32\",\"name\":\"previousAdminRole\",\"type\":\"bytes32\"},{\"indexed\":true,\"internalType\":\"bytes32\",\"name\":\"newAdminRole\",\"type\":\"bytes32\"}],\"name\":\"RoleAdminChanged\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"}],\"name\":\"RoleGranted\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"}],\"name\":\"RoleRevoked\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"Unpaused\",\"type\":\"event\"},{\"stateMutability\":\"payable\",\"type\":\"fallback\"},{\"inputs\":[],\"name\":\"DEFAULT_ADMIN_ROLE\",\"outputs\":[{\"internalType\":\"bytes32\",\"name\":\"\",\"type\":\"bytes32\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"GENERATION_EGG\",\"outputs\":[{\"internalType\":\"uint16\",\"name\":\"\",\"type\":\"uint16\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"GENERATION_MAX\",\"outputs\":[{\"internalType\":\"uint16\",\"name\":\"\",\"type\":\"uint16\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"MINTER_ROLE\",\"outputs\":[{\"internalType\":\"bytes32\",\"name\":\"\",\"type\":\"bytes32\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"PAUSER_ROLE\",\"outputs\":[{\"internalType\":\"bytes32\",\"name\":\"\",\"type\":\"bytes32\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"fatherId\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"motherId\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"gene\",\"type\":\"uint256\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"breed\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"burn\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"_newBaseUri\",\"type\":\"string\"}],\"name\":\"changeBaseURI\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"contract IFishGeneManager\",\"name\":\"_newGeneManager\",\"type\":\"address\"}],\"name\":\"changeGeneManager\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_newSigner\",\"type\":\"address\"}],\"name\":\"changeSigner\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getApproved\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getFishAttributes\",\"outputs\":[{\"components\":[{\"internalType\":\"uint16\",\"name\":\"generation\",\"type\":\"uint16\"},{\"internalType\":\"enum FishAttributeModel.Rarity\",\"name\":\"rarity\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"birthday\",\"type\":\"uint256\"}],\"internalType\":\"struct FishAttributeModel.FishProperties\",\"name\":\"\",\"type\":\"tuple\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256[]\",\"name\":\"tokenIds\",\"type\":\"uint256[]\"}],\"name\":\"getFishInfoCollection\",\"outputs\":[{\"components\":[{\"internalType\":\"uint16\",\"name\":\"generation\",\"type\":\"uint16\"},{\"internalType\":\"enum FishAttributeModel.Rarity\",\"name\":\"rarity\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"birthday\",\"type\":\"uint256\"}],\"internalType\":\"struct FishAttributeModel.FishProperties[]\",\"name\":\"\",\"type\":\"tuple[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"}],\"name\":\"getRoleAdmin\",\"outputs\":[{\"internalType\":\"bytes32\",\"name\":\"\",\"type\":\"bytes32\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"getRoleMember\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"}],\"name\":\"getRoleMemberCount\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"getTokensByOwner\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"tokenIds\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"grantRole\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"hasRole\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"gen\",\"type\":\"uint256\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"hatch\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"contract IFishGeneManager\",\"name\":\"_geneManager\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_signer\",\"type\":\"address\"},{\"internalType\":\"string\",\"name\":\"_name\",\"type\":\"string\"},{\"internalType\":\"string\",\"name\":\"_symbol\",\"type\":\"string\"},{\"internalType\":\"string\",\"name\":\"_baseTokenURI\",\"type\":\"string\"}],\"name\":\"initialize\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_signer\",\"type\":\"address\"}],\"name\":\"isSigner\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"gene\",\"type\":\"uint256\"}],\"name\":\"mint\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"\",\"type\":\"bytes\"}],\"name\":\"onERC721Received\",\"outputs\":[{\"internalType\":\"bytes4\",\"name\":\"\",\"type\":\"bytes4\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"ownerOf\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"pause\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"paused\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"renounceRole\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"role\",\"type\":\"bytes32\"},{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"revokeRole\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"_data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"tokenByIndex\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"tokenOfOwnerByIndex\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"tokenURI\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"unpause\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"stateMutability\":\"payable\",\"type\":\"receive\"} ]";
    private static string marketContractAbi = "[ {\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"sellToken\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"sellTokenId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"sellValue\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"buyToken\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"buyTokenId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"buyValue\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"buyer\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"}],\"name\":\"Buy\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"sellToken\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"sellTokenId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"buyToken\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"buyTokenId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"}],\"name\":\"Cancel\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"tokenIds\",\"type\":\"uint256[]\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"}],\"name\":\"FishBatchOrderEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"}],\"name\":\"FishOrderEvent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"stateMutability\":\"payable\",\"type\":\"fallback\"},{\"inputs\":[],\"name\":\"beneficiary\",\"outputs\":[{\"internalType\":\"address payable\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"gen\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"price\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"buyToken\",\"type\":\"address\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"buy\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"internalType\":\"uint256[]\",\"name\":\"gens\",\"type\":\"uint256[]\"},{\"internalType\":\"uint256\",\"name\":\"price\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"buyToken\",\"type\":\"address\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"buyBatch\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[{\"components\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"sellAsset\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"buyAsset\",\"type\":\"tuple\"}],\"internalType\":\"struct ExchangeDomain.OrderKey\",\"name\":\"key\",\"type\":\"tuple\"}],\"name\":\"cancel\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"contract IIAmFish\",\"name\":\"newIAmFishToken\",\"type\":\"address\"}],\"name\":\"changeFishToken\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"components\":[{\"components\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"sellAsset\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"buyAsset\",\"type\":\"tuple\"}],\"internalType\":\"struct ExchangeDomain.OrderKey\",\"name\":\"key\",\"type\":\"tuple\"},{\"internalType\":\"uint256\",\"name\":\"selling\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"buying\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"sellerFee\",\"type\":\"uint256\"}],\"internalType\":\"struct ExchangeDomain.Order\",\"name\":\"order\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"internalType\":\"struct ExchangeDomain.Sig\",\"name\":\"sig\",\"type\":\"tuple\"},{\"internalType\":\"uint256\",\"name\":\"buyerFee\",\"type\":\"uint256\"},{\"components\":[{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"internalType\":\"struct ExchangeDomain.Sig\",\"name\":\"buyerFeeSig\",\"type\":\"tuple\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"buyer\",\"type\":\"address\"}],\"name\":\"exchange\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"exchangeState\",\"outputs\":[{\"internalType\":\"contract ExchangeState\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"iAmFishToken\",\"outputs\":[{\"internalType\":\"contract IIAmFish\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"contract IIAmFish\",\"name\":\"_iAmFishToken\",\"type\":\"address\"},{\"internalType\":\"contract ExchangeState\",\"name\":\"_state\",\"type\":\"address\"},{\"internalType\":\"contract ExchangeOrdersHolder\",\"name\":\"_ordersHolder\",\"type\":\"address\"},{\"internalType\":\"address payable\",\"name\":\"_beneficiary\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_buyerFeeSigner\",\"type\":\"address\"}],\"name\":\"initialize\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"marketSigner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"ordersHolder\",\"outputs\":[{\"internalType\":\"contract ExchangeOrdersHolder\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"components\":[{\"components\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"sellAsset\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"buyAsset\",\"type\":\"tuple\"}],\"internalType\":\"struct ExchangeDomain.OrderKey\",\"name\":\"key\",\"type\":\"tuple\"},{\"internalType\":\"uint256\",\"name\":\"selling\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"buying\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"sellerFee\",\"type\":\"uint256\"}],\"internalType\":\"struct ExchangeDomain.Order\",\"name\":\"order\",\"type\":\"tuple\"},{\"internalType\":\"uint256\",\"name\":\"fee\",\"type\":\"uint256\"}],\"name\":\"prepareBuyerFeeMessage\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"components\":[{\"components\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"salt\",\"type\":\"uint256\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"sellAsset\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"enum ExchangeDomain.AssetType\",\"name\":\"assetType\",\"type\":\"uint8\"}],\"internalType\":\"struct ExchangeDomain.Asset\",\"name\":\"buyAsset\",\"type\":\"tuple\"}],\"internalType\":\"struct ExchangeDomain.OrderKey\",\"name\":\"key\",\"type\":\"tuple\"},{\"internalType\":\"uint256\",\"name\":\"selling\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"buying\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"sellerFee\",\"type\":\"uint256\"}],\"internalType\":\"struct ExchangeDomain.Order\",\"name\":\"order\",\"type\":\"tuple\"}],\"name\":\"prepareMessage\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address payable\",\"name\":\"newBeneficiary\",\"type\":\"address\"}],\"name\":\"setBeneficiary\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newBuyerFeeSigner\",\"type\":\"address\"}],\"name\":\"setBuyerFeeSigner\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"state\",\"outputs\":[{\"internalType\":\"contract ExchangeState\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"stateMutability\":\"payable\",\"type\":\"receive\"} ]";
    string args = "[]";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetAccount(string account)
    {
        this.account = account;
    }
    public string GetAccount()
    {
        return this.account;
    }
    public async UniTask<bool> GetNumberFishAsync()
    {
        // string owner = account;
        string[] obj = { account };
        string args = JsonConvert.SerializeObject(obj);
        string method = "getTokensByOwner";
        try
        {
            var response = await EVM.Call(chain, network, fishContract, fishContractAbi, method, args);
            await APIManager.Instance.GetGameConfig();
            PlayerPrefs.SetString(USER_ADRESS, account);

            GameManager.UserData = await APIManager.Instance.GetUserInfo(account);
            Debug.Log("Check loginnnn:");
            Debug.Log(account);
            Debug.Log(APIManager.v);
            Debug.Log(APIManager.r);
            Debug.Log(APIManager.s);
            string result = await APIManager.Instance.RequestLogin( account, APIManager.v, APIManager.r, APIManager.s);
            
            if (result=="true") {
                SoundManager.SaveVolume();
                SoundManager.Instance.SetVolume();
                var token = JsonConvert.DeserializeObject<int[]>(response);
                var allFish = await APIManager.Instance.GetUserFish(token);
                GameManager.UserData.SetAllFish(allFish, token);

                DataReferece.Instance.UpdatedUserData();
                FishAssetInit.Instance.OnSpawnAllFish();
                return true;
            } else {
                PopupManager.Instance.OnHideAllPopups();
                PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                {
                    title = "NOTICE",
                    status = "Can not loggin to server",
                    confirmText = "OK",
                }, true);
                return false;
            }

        }
        catch (Exception e)
        {
            Debug.Log("Checking error data");
            Debug.Log(e.Message);
            PopupManager.Instance.OnHideAllPopups();
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "Can not connect BSC server!",
                confirmText = "OK",
            }, true);
            return false;
        }
    }
    public async UniTask<bool> BreedFish(string fatherId, string motherId, string salt, string gene, string v, string r, string s)
    {
        // smart contract method to call
        string method = "breed";
        // abi in json format
        // value in wei
        
        string[] obj = { fatherId, motherId, salt, gene, v, r, s };
        string args = JsonConvert.SerializeObject(obj);
        // create data to interact with smart contract
        string data = await EVM.CreateContractData(fishContractAbi, method, args);
        // connects to user's browser wallet (metamask) to update contract state
        try
        {
#if UNITY_WEBGL
            string value = "0";
            string gasLimit = "";
            // gas price OPTIONAL
            string gasPrice = "";
            string response = await Web3GL.SendContract(method, fishContractAbi, fishContract, args, value);
#else
            WalletConnect.Instance.OpenMobileWallet();
            //Nethereum.Hex.HexTypes.HexBigInteger newfatherId = new Nethereum.Hex.HexTypes.HexBigInteger(long.Parse(fatherId));
            //Nethereum.Hex.HexTypes.HexBigInteger newmotherId = new Nethereum.Hex.HexTypes.HexBigInteger(long.Parse(motherId));
            //Nethereum.Hex.HexTypes.HexBigInteger newSalt = new Nethereum.Hex.HexTypes.HexBigInteger(long.Parse(salt));
            //string tokenIdNew = $"0x{tokenID.ToString("X16").ToLower()}";
            object[] parameters = {
                "0x" + long.Parse(fatherId).ToString("X"), "0x" + long.Parse(motherId).ToString("X"), "0x" + long.Parse(salt).ToString("X"), gene, v, HexByteConvertorExtensions.HexToByteArray(r), HexByteConvertorExtensions.HexToByteArray(s)
            };
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(0);
            string resp = await Moralis.ExecuteContractFunction(fishContract, fishContractAbi, method, parameters, value, gas, gasPrice);
#endif
            statusSuccessContract = ContractState.Success;
            return true;
        }
        catch (Exception e)
        {
            statusSuccessContract = ContractState.Fail;
            Debug.LogException(e, this);
            return false;
        }
        return false;
    }
    public async UniTask<bool> HatchFish(string tokenID, string gene, string v, string r, string s)
    {
        // smart contract method to call
        string method = "hatch";
        // abi in json format
        // value in wei
        
        
        
        // connects to user's browser wallet (metamask) to update contract state
        try
        {
#if UNITY_WEBGL
            string value = "0";
            string gasLimit = "";
            // gas price OPTIONAL
            string gasPrice = "";
            string[] obj = { tokenID, gene, v, r, s };
            string args = JsonConvert.SerializeObject(obj);
            // create data to interact with smart contract
            string data = await EVM.CreateContractData(fishContractAbi, method, args);
            string response = await Web3GL.SendContract(method, fishContractAbi, fishContract, args, value);
#else
            WalletConnect.Instance.OpenMobileWallet();
            //Nethereum.Hex.HexTypes.HexBigInteger newTokenId = new Nethereum.Hex.HexTypes.HexBigInteger(long.Parse(tokenID));
            //string tokenIdNew = $"0x{tokenID.ToString("X16").ToLower()}";
            object[] parameters = {
                "0x" + long.Parse(tokenID).ToString("X"), gene, v, HexByteConvertorExtensions.HexToByteArray(r), HexByteConvertorExtensions.HexToByteArray(s)
            };
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(0);
            string resp = await Moralis.ExecuteContractFunction(fishContract, fishContractAbi, method, parameters, value, gas, gasPrice);
#endif
            statusSuccessContract = ContractState.Success;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            statusSuccessContract = ContractState.Fail;
            return false;
        }
    }
    public async UniTask<bool> ExchangEgg(string salt, string gene, string v, string r, string s)
    {
        WalletConnect.Instance.OpenMobileWallet();
        //Nethereum.Hex.HexTypes.HexBigInteger newSalt = new Nethereum.Hex.HexTypes.HexBigInteger(long.Parse(salt));
        string hexValue = "0x" + long.Parse(salt).ToString("X");
        object[] parameters = {
                hexValue, gene, "0", "0x0000000000000000000000000000000000000000", v, HexByteConvertorExtensions.HexToByteArray(r), HexByteConvertorExtensions.HexToByteArray(s)
            };
            // Set gas estimate
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(0);
            string resp = await Moralis.ExecuteContractFunction(marketContract, marketContractAbi, "buy", parameters, value, gas, gasPrice);
            return true;
    }

    public async UniTask<String> GetLastFish()
    {
        // string owner = account;
        try
        {
            int numberCount = 5;
            while (numberCount > 0)
            {
                bool waitingFish = false;
                string[] obj = { account };
                string args = JsonConvert.SerializeObject(obj);
                string method = "balanceOf";
                var numberFish = await EVM.Call(chain, network, fishContract, fishContractAbi, method, args);
                int lastFish = int.Parse(numberFish) - 1;
                string[] objlast = { account, lastFish.ToString() };
                string args_last = JsonConvert.SerializeObject(objlast);
                string method_getlast = "tokenOfOwnerByIndex";
                var lastFishToken = await EVM.Call(chain, network, fishContract, fishContractAbi, method_getlast, args_last);
                //GameManager.UserData.eggList;
                foreach (var item in GameManager.UserData.eggList)
                {
                    if (item.tokenId.ToString() == lastFishToken)
                    {
                        waitingFish = true;
                    }
                }
                foreach (var item in GameManager.UserData.fishList)
                {
                    if (item.tokenId.ToString() == lastFishToken)
                    {
                        waitingFish = true;
                    }
                }
                if (!waitingFish)
                    return lastFishToken.ToString();
                await UniTask.Delay(2000);
                numberCount--;
            }
        }
        catch (System.Exception)
        {
            return null;
        }

        return null;
    }
}

[System.Serializable]
public class ExchangEggFunction
{
    //salt, gene, "0", "0x0000000000000000000000000000000000000000", v, "ab2c", "ab1c"
    public string salt;
    public string gene;
    public string price;
    public string buyToken;
    public string v;
    public string r;
    public string s;
}
