﻿#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};

// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.UInt32[]
struct UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA;
// Mono.Math.BigInteger
struct BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// Mono.Math.Prime.PrimalityTest
struct PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B;
// Mono.Math.Prime.Generator.PrimeGeneratorBase
struct PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372;
// System.Security.Cryptography.RandomNumberGenerator
struct RandomNumberGenerator_t4E862666A2F7D55324960670C7A1B4C2D40222F3;
// Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase
struct SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175;
// System.String
struct String_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C const RuntimeMethod* PrimalityTests_RabinMillerTest_m8CB7357EAAB8F33F542625238BEDA04D02D3FEE1_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct Il2CppArrayBounds;

// Mono.Math.BigInteger
struct BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08  : public RuntimeObject
{
	// System.UInt32 Mono.Math.BigInteger::length
	uint32_t ___length_0;
	// System.UInt32[] Mono.Math.BigInteger::data
	UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* ___data_1;
};

struct BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_StaticFields
{
	// System.UInt32[] Mono.Math.BigInteger::smallPrimes
	UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* ___smallPrimes_2;
	// System.Security.Cryptography.RandomNumberGenerator Mono.Math.BigInteger::rng
	RandomNumberGenerator_t4E862666A2F7D55324960670C7A1B4C2D40222F3* ___rng_3;
};

// Mono.Math.Prime.PrimalityTests
struct PrimalityTests_tDB9CA9A8AD178FFA67DA3B23F4C3E76D6B4F8F20  : public RuntimeObject
{
};

// Mono.Math.Prime.Generator.PrimeGeneratorBase
struct PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Char
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	// System.Char System.Char::m_value
	Il2CppChar ___m_value_0;
};

struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	// System.Byte[] System.Char::s_categoryForLatin1
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1_3;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase
struct SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175  : public PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372
{
};

// System.UInt32
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	// System.UInt32 System.UInt32::m_value
	uint32_t ___m_value_0;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=10
struct __StaticArrayInitTypeSizeU3D10_t1320800A84A593C0C545AA86F56DB35F62D3F3B2 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D10_t1320800A84A593C0C545AA86F56DB35F62D3F3B2__padding[10];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=18
struct __StaticArrayInitTypeSizeU3D18_tA6016B38246762611CCF93D6D91E8AB122C1D671 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D18_tA6016B38246762611CCF93D6D91E8AB122C1D671__padding[18];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20
struct __StaticArrayInitTypeSizeU3D20_t3FAE08F10A75268B585F7B017FEEB774F4E15776 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D20_t3FAE08F10A75268B585F7B017FEEB774F4E15776__padding[20];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=256
struct __StaticArrayInitTypeSizeU3D256_t2D0DEE853BDA1AD8D9861536BB032DC5710596AB 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D256_t2D0DEE853BDA1AD8D9861536BB032DC5710596AB__padding[256];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3
struct __StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F__padding[3];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3132
struct __StaticArrayInitTypeSizeU3D3132_tB22FE8E689088D29959DAC48B057906D10762D61 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D3132_tB22FE8E689088D29959DAC48B057906D10762D61__padding[3132];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=32
struct __StaticArrayInitTypeSizeU3D32_t98F70AA7E629F6A93A37486AFFD45279759B86C3 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D32_t98F70AA7E629F6A93A37486AFFD45279759B86C3__padding[32];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=48
struct __StaticArrayInitTypeSizeU3D48_t02BDA49745B091399292B3B370DB4FE5B9D6DDB5 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D48_t02BDA49745B091399292B3B370DB4FE5B9D6DDB5__padding[48];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=64
struct __StaticArrayInitTypeSizeU3D64_tF99BC823428B50B6B78303D57D4FC461CE1B7334 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D64_tF99BC823428B50B6B78303D57D4FC461CE1B7334__padding[64];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=9
struct __StaticArrayInitTypeSizeU3D9_tCF0B8F5E7316E215622AB02BE64B168AFC5FCCC7 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D9_tCF0B8F5E7316E215622AB02BE64B168AFC5FCCC7__padding[9];
	};
};

// <PrivateImplementationDetails>
struct U3CPrivateImplementationDetailsU3E_t55FC1D237A87005A39D205736AE27F4711007813  : public RuntimeObject
{
};

struct U3CPrivateImplementationDetailsU3E_t55FC1D237A87005A39D205736AE27F4711007813_StaticFields
{
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=64 <PrivateImplementationDetails>::001D686DB504E20C792EAA07FE09224A45FF328E24A80072D04D16ABC5C2B5D2
	__StaticArrayInitTypeSizeU3D64_tF99BC823428B50B6B78303D57D4FC461CE1B7334 ___001D686DB504E20C792EAA07FE09224A45FF328E24A80072D04D16ABC5C2B5D2_0;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3132 <PrivateImplementationDetails>::25E3E48132FBDBE9B7C0C6C54D7C10A5DE12A105AA3E5DE2A0DC808BF245B7A5
	__StaticArrayInitTypeSizeU3D3132_tB22FE8E689088D29959DAC48B057906D10762D61 ___25E3E48132FBDBE9B7C0C6C54D7C10A5DE12A105AA3E5DE2A0DC808BF245B7A5_1;
	// System.Int64 <PrivateImplementationDetails>::290C4A052C215D096172EB81AEE671FB3286E5C1DB5E73F96021FC09825DDB88
	int64_t ___290C4A052C215D096172EB81AEE671FB3286E5C1DB5E73F96021FC09825DDB88_2;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::32D0830B8EE1D49A66F395C8EA80E02BFC07C2A12A8EA8C8B484AF02108A1950
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___32D0830B8EE1D49A66F395C8EA80E02BFC07C2A12A8EA8C8B484AF02108A1950_3;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::35BF50EEF3270FD8CA09E66FC5B0481C5A151B14F6A634854E32F63633D49DCB
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___35BF50EEF3270FD8CA09E66FC5B0481C5A151B14F6A634854E32F63633D49DCB_4;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::3C0C410618682C4DF0474E034114CC8E562F05A512B521AC367571017BDFA75D
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___3C0C410618682C4DF0474E034114CC8E562F05A512B521AC367571017BDFA75D_5;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::44F5B1A2C48314502ACCBF186D1A2F9F7F176825898F32F1A2047B956194F174
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___44F5B1A2C48314502ACCBF186D1A2F9F7F176825898F32F1A2047B956194F174_6;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=48 <PrivateImplementationDetails>::4800FBFC4566EB02D1727A4B1C949CCBC7535C216A0766564C199308631B5DD6
	__StaticArrayInitTypeSizeU3D48_t02BDA49745B091399292B3B370DB4FE5B9D6DDB5 ___4800FBFC4566EB02D1727A4B1C949CCBC7535C216A0766564C199308631B5DD6_7;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20 <PrivateImplementationDetails>::533B8C444F951E83EFF7305E3807B66CE0005DE0A2D0A44873C130895A3BE6AA
	__StaticArrayInitTypeSizeU3D20_t3FAE08F10A75268B585F7B017FEEB774F4E15776 ___533B8C444F951E83EFF7305E3807B66CE0005DE0A2D0A44873C130895A3BE6AA_8;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=256 <PrivateImplementationDetails>::55D0BF716B334D123E0088CFB3F8E2FEA17AF5025BB527F95EEB09BA978EA329
	__StaticArrayInitTypeSizeU3D256_t2D0DEE853BDA1AD8D9861536BB032DC5710596AB ___55D0BF716B334D123E0088CFB3F8E2FEA17AF5025BB527F95EEB09BA978EA329_9;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::59BE5A634187B8A57216EFF5371A47732C05744B1C1A0A6382A6D5622C9FFDCE
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___59BE5A634187B8A57216EFF5371A47732C05744B1C1A0A6382A6D5622C9FFDCE_10;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=32 <PrivateImplementationDetails>::5DF6E0E2761359D30A8275058E299FCC0381534545F55CF43E41983F5D4C9456
	__StaticArrayInitTypeSizeU3D32_t98F70AA7E629F6A93A37486AFFD45279759B86C3 ___5DF6E0E2761359D30A8275058E299FCC0381534545F55CF43E41983F5D4C9456_11;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::5F8C6B3C66B972606D85E7651F67ADBD02E8316876884674E8328FA710747E5B
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___5F8C6B3C66B972606D85E7651F67ADBD02E8316876884674E8328FA710747E5B_12;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=10 <PrivateImplementationDetails>::60C6ED13AF98DBFEEDA8F8197FFFCC349BB04395CC81DF0D477CBC57BF5B398B
	__StaticArrayInitTypeSizeU3D10_t1320800A84A593C0C545AA86F56DB35F62D3F3B2 ___60C6ED13AF98DBFEEDA8F8197FFFCC349BB04395CC81DF0D477CBC57BF5B398B_13;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=10 <PrivateImplementationDetails>::64B3E7D737AFF47D4C3BBD81D2D06D697DDD8EB60F29E13E4425D19D8BBCA1F7
	__StaticArrayInitTypeSizeU3D10_t1320800A84A593C0C545AA86F56DB35F62D3F3B2 ___64B3E7D737AFF47D4C3BBD81D2D06D697DDD8EB60F29E13E4425D19D8BBCA1F7_14;
	// System.Int64 <PrivateImplementationDetails>::6772A9B8BF207A3CFE6EE68769D6985B69522183F24A2A3D41BC3B4602953426
	int64_t ___6772A9B8BF207A3CFE6EE68769D6985B69522183F24A2A3D41BC3B4602953426_15;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=9 <PrivateImplementationDetails>::684312AFB7719E57993D2826FFBAF7EA965614F20F91D999FB19B01E21AA62E6
	__StaticArrayInitTypeSizeU3D9_tCF0B8F5E7316E215622AB02BE64B168AFC5FCCC7 ___684312AFB7719E57993D2826FFBAF7EA965614F20F91D999FB19B01E21AA62E6_16;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=64 <PrivateImplementationDetails>::6AA56C4BCD208911792AD24C7681FEFB93BED51903AFC54860C9BD37E41E5A31
	__StaticArrayInitTypeSizeU3D64_tF99BC823428B50B6B78303D57D4FC461CE1B7334 ___6AA56C4BCD208911792AD24C7681FEFB93BED51903AFC54860C9BD37E41E5A31_17;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::796E63069E193A008CB4E85573AA1FE53C5F4E58B42A7F61FD0EEE1D89B5120B
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___796E63069E193A008CB4E85573AA1FE53C5F4E58B42A7F61FD0EEE1D89B5120B_18;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=64 <PrivateImplementationDetails>::7C8975E1E60A5C8337F28EDF8C33C3B180360B7279644A9BC1AF3C51E6220BF5
	__StaticArrayInitTypeSizeU3D64_tF99BC823428B50B6B78303D57D4FC461CE1B7334 ___7C8975E1E60A5C8337F28EDF8C33C3B180360B7279644A9BC1AF3C51E6220BF5_19;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::850D7367E4FB0766E2CBC3ACF5AB42B4E98348E58E5A789845D4FCCDB63D2AEE
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___850D7367E4FB0766E2CBC3ACF5AB42B4E98348E58E5A789845D4FCCDB63D2AEE_20;
	// System.Int64 <PrivateImplementationDetails>::992F16C986809AB68C7466CC3EC6F12B2506A962EA539753E5D84A2FB7FF8A24
	int64_t ___992F16C986809AB68C7466CC3EC6F12B2506A962EA539753E5D84A2FB7FF8A24_21;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::9A65C09A11757751BFED67A414E00B188DC4C7757FCB6CBD33A916DDE4A3D925
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___9A65C09A11757751BFED67A414E00B188DC4C7757FCB6CBD33A916DDE4A3D925_22;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=18 <PrivateImplementationDetails>::9ACEFCC0C950280B64AB9E045E38C34ABF71EC70A0DC61B9C621C6BFB4F78047
	__StaticArrayInitTypeSizeU3D18_tA6016B38246762611CCF93D6D91E8AB122C1D671 ___9ACEFCC0C950280B64AB9E045E38C34ABF71EC70A0DC61B9C621C6BFB4F78047_23;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::B1E34F4A11EE411B83415EF0B252A0B2BBCFCAC2E592865E09C12E4252C93A75
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___B1E34F4A11EE411B83415EF0B252A0B2BBCFCAC2E592865E09C12E4252C93A75_24;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::C92FAD7F348A682E7D5B7E74C76B5D019174EE7BC87545B25A1FDD49FBCC2D0B
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___C92FAD7F348A682E7D5B7E74C76B5D019174EE7BC87545B25A1FDD49FBCC2D0B_25;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::E17B8359E685992B0DE6242AAA24FCB7404173CBB7FF8646FF7D658139F41B5F
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___E17B8359E685992B0DE6242AAA24FCB7404173CBB7FF8646FF7D658139F41B5F_26;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=64 <PrivateImplementationDetails>::F83B332BE4E6A5A4B1C56AAF6DB52657DA495E149870057D8590AB9D7A6167AD
	__StaticArrayInitTypeSizeU3D64_tF99BC823428B50B6B78303D57D4FC461CE1B7334 ___F83B332BE4E6A5A4B1C56AAF6DB52657DA495E149870057D8590AB9D7A6167AD_27;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=3 <PrivateImplementationDetails>::FB6D7301FFDCB5FBA5807A19B4F0606947897C1105240B6BBA815352DBBE2064
	__StaticArrayInitTypeSizeU3D3_t5760B80EAD4D11FDBFDFCD57A808D0D9CEECC80F ___FB6D7301FFDCB5FBA5807A19B4F0606947897C1105240B6BBA815352DBBE2064_28;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// Mono.Math.Prime.PrimalityTest
struct PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B  : public MulticastDelegate_t
{
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.UInt32[]
struct UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA  : public RuntimeArray
{
	ALIGN_FIELD (8) uint32_t m_Items[1];

	inline uint32_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint32_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint32_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint32_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint32_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint32_t value)
	{
		m_Items[index] = value;
	}
};



// System.Void Mono.Math.Prime.PrimalityTest::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PrimalityTest__ctor_m73483F9E5D166F74E0340F479376C61D9280266A (PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// Mono.Math.BigInteger Mono.Math.BigInteger::GenerateRandom(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* BigInteger_GenerateRandom_mA6D7DE4D0B18C143D555FFF251F5FC9BDC47A1DB (int32_t ___bits0, const RuntimeMethod* method) ;
// System.Void Mono.Math.BigInteger::SetBit(System.UInt32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BigInteger_SetBit_m3E67DE35B0E691FCB886C60252CAAFC3FCB92A39 (BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* __this, uint32_t ___bitNum0, const RuntimeMethod* method) ;
// System.UInt32 Mono.Math.BigInteger::op_Modulus(Mono.Math.BigInteger,System.UInt32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint32_t BigInteger_op_Modulus_m6A12610F6997190C6C35ED211AE4AEE01683E92F (BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* ___bi0, uint32_t ___ui1, const RuntimeMethod* method) ;
// System.Boolean Mono.Math.Prime.PrimalityTest::Invoke(Mono.Math.BigInteger,Mono.Math.Prime.ConfidenceFactor)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool PrimalityTest_Invoke_m7E9F9413908598A1270792B565D71288027AA552_inline (PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* __this, BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* ___bi0, int32_t ___confidence1, const RuntimeMethod* method) ;
// System.Void Mono.Math.BigInteger::Incr2()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BigInteger_Incr2_mE5DCADCC1DEDD4F3E48E326940D3C926E1A37808 (BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* __this, const RuntimeMethod* method) ;
// System.Void Mono.Math.Prime.Generator.PrimeGeneratorBase::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PrimeGeneratorBase__ctor_mECF0CD5B964A7E6FCE4F504719164114B8A678E9 (PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372* __this, const RuntimeMethod* method) ;
// System.Char System.String::get_Chars(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3 (String_t* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Int32 System.String::get_Length()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Mono.Math.Prime.ConfidenceFactor Mono.Math.Prime.Generator.PrimeGeneratorBase::get_Confidence()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t PrimeGeneratorBase_get_Confidence_m8A53DA3C670504B629434C990508D4B77642B875 (PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372* __this, const RuntimeMethod* method) 
{
	{
		return (int32_t)(2);
	}
}
// Mono.Math.Prime.PrimalityTest Mono.Math.Prime.Generator.PrimeGeneratorBase::get_PrimalityTest()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* PrimeGeneratorBase_get_PrimalityTest_m96C5E1866F96043982AF493BE7EAB5969F770E1D (PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PrimalityTests_RabinMillerTest_m8CB7357EAAB8F33F542625238BEDA04D02D3FEE1_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* L_0 = (PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B*)il2cpp_codegen_object_new(PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		PrimalityTest__ctor_m73483F9E5D166F74E0340F479376C61D9280266A(L_0, NULL, (intptr_t)((void*)PrimalityTests_RabinMillerTest_m8CB7357EAAB8F33F542625238BEDA04D02D3FEE1_RuntimeMethod_var), NULL);
		return L_0;
	}
}
// System.Int32 Mono.Math.Prime.Generator.PrimeGeneratorBase::get_TrialDivisionBounds()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t PrimeGeneratorBase_get_TrialDivisionBounds_m706A348C994861A2B92CE9156FE20DCF7474E286 (PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372* __this, const RuntimeMethod* method) 
{
	{
		return ((int32_t)4000);
	}
}
// System.Void Mono.Math.Prime.Generator.PrimeGeneratorBase::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PrimeGeneratorBase__ctor_mECF0CD5B964A7E6FCE4F504719164114B8A678E9 (PrimeGeneratorBase_tF3B59CDEC773B15DE6B497796F6776E493C9E372* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Mono.Math.BigInteger Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::GenerateSearchBase(System.Int32,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* SequentialSearchPrimeGeneratorBase_GenerateSearchBase_mD12A7AC0A052FA228E0F4918BBA1B2B59AD605CE (SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175* __this, int32_t ___bits0, RuntimeObject* ___context1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0 = ___bits0;
		il2cpp_codegen_runtime_class_init_inline(BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var);
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_1;
		L_1 = BigInteger_GenerateRandom_mA6D7DE4D0B18C143D555FFF251F5FC9BDC47A1DB(L_0, NULL);
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_2 = L_1;
		NullCheck(L_2);
		BigInteger_SetBit_m3E67DE35B0E691FCB886C60252CAAFC3FCB92A39(L_2, 0, NULL);
		return L_2;
	}
}
// Mono.Math.BigInteger Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::GenerateNewPrime(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* SequentialSearchPrimeGeneratorBase_GenerateNewPrime_m6AC834873702FE49B85FB261931CA31BC239FFCD (SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175* __this, int32_t ___bits0, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___bits0;
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_1;
		L_1 = VirtualFuncInvoker2< BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08*, int32_t, RuntimeObject* >::Invoke(9 /* Mono.Math.BigInteger Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::GenerateNewPrime(System.Int32,System.Object) */, __this, L_0, NULL);
		return L_1;
	}
}
// Mono.Math.BigInteger Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::GenerateNewPrime(System.Int32,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* SequentialSearchPrimeGeneratorBase_GenerateNewPrime_m6599A58FA9EBD14FEB9D18073419FF8341365B8B (SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175* __this, int32_t ___bits0, RuntimeObject* ___context1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* V_0 = NULL;
	uint32_t V_1 = 0;
	int32_t V_2 = 0;
	UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* V_3 = NULL;
	int32_t V_4 = 0;
	{
		int32_t L_0 = ___bits0;
		RuntimeObject* L_1 = ___context1;
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_2;
		L_2 = VirtualFuncInvoker2< BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08*, int32_t, RuntimeObject* >::Invoke(8 /* Mono.Math.BigInteger Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::GenerateSearchBase(System.Int32,System.Object) */, __this, L_0, L_1);
		V_0 = L_2;
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_3 = V_0;
		il2cpp_codegen_runtime_class_init_inline(BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var);
		uint32_t L_4;
		L_4 = BigInteger_op_Modulus_m6A12610F6997190C6C35ED211AE4AEE01683E92F(L_3, ((int32_t)-1060120681), NULL);
		V_1 = L_4;
		int32_t L_5;
		L_5 = VirtualFuncInvoker0< int32_t >::Invoke(6 /* System.Int32 Mono.Math.Prime.Generator.PrimeGeneratorBase::get_TrialDivisionBounds() */, __this);
		V_2 = L_5;
		UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* L_6 = ((BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_StaticFields*)il2cpp_codegen_static_fields_for(BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var))->___smallPrimes_2;
		V_3 = L_6;
	}

IL_0022:
	{
		uint32_t L_7 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_7%(uint32_t)(int32_t)3)))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_8 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_8%(uint32_t)(int32_t)5)))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_9 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_9%(uint32_t)(int32_t)7)))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_10 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_10%(uint32_t)(int32_t)((int32_t)11))))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_11 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_11%(uint32_t)(int32_t)((int32_t)13))))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_12 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_12%(uint32_t)(int32_t)((int32_t)17))))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_13 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_13%(uint32_t)(int32_t)((int32_t)19))))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_14 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_14%(uint32_t)(int32_t)((int32_t)23))))
		{
			goto IL_009d;
		}
	}
	{
		uint32_t L_15 = V_1;
		if (!((int32_t)((uint32_t)(int32_t)L_15%(uint32_t)(int32_t)((int32_t)29))))
		{
			goto IL_009d;
		}
	}
	{
		V_4 = ((int32_t)10);
		goto IL_006d;
	}

IL_005b:
	{
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_16 = V_0;
		UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* L_17 = V_3;
		int32_t L_18 = V_4;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		uint32_t L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		il2cpp_codegen_runtime_class_init_inline(BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08_il2cpp_TypeInfo_var);
		uint32_t L_21;
		L_21 = BigInteger_op_Modulus_m6A12610F6997190C6C35ED211AE4AEE01683E92F(L_16, L_20, NULL);
		if (!L_21)
		{
			goto IL_009d;
		}
	}
	{
		int32_t L_22 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_add(L_22, 1));
	}

IL_006d:
	{
		int32_t L_23 = V_4;
		UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* L_24 = V_3;
		NullCheck(L_24);
		if ((((int32_t)L_23) >= ((int32_t)((int32_t)(((RuntimeArray*)L_24)->max_length)))))
		{
			goto IL_007d;
		}
	}
	{
		UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* L_25 = V_3;
		int32_t L_26 = V_4;
		NullCheck(L_25);
		int32_t L_27 = L_26;
		uint32_t L_28 = (L_25)->GetAt(static_cast<il2cpp_array_size_t>(L_27));
		int32_t L_29 = V_2;
		if ((((int64_t)((int64_t)(uint64_t)L_28)) <= ((int64_t)((int64_t)L_29))))
		{
			goto IL_005b;
		}
	}

IL_007d:
	{
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_30 = V_0;
		RuntimeObject* L_31 = ___context1;
		bool L_32;
		L_32 = VirtualFuncInvoker2< bool, BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08*, RuntimeObject* >::Invoke(10 /* System.Boolean Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::IsPrimeAcceptable(Mono.Math.BigInteger,System.Object) */, __this, L_30, L_31);
		if (!L_32)
		{
			goto IL_009d;
		}
	}
	{
		PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* L_33;
		L_33 = VirtualFuncInvoker0< PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* >::Invoke(5 /* Mono.Math.Prime.PrimalityTest Mono.Math.Prime.Generator.PrimeGeneratorBase::get_PrimalityTest() */, __this);
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_34 = V_0;
		int32_t L_35;
		L_35 = VirtualFuncInvoker0< int32_t >::Invoke(4 /* Mono.Math.Prime.ConfidenceFactor Mono.Math.Prime.Generator.PrimeGeneratorBase::get_Confidence() */, __this);
		NullCheck(L_33);
		bool L_36;
		L_36 = PrimalityTest_Invoke_m7E9F9413908598A1270792B565D71288027AA552_inline(L_33, L_34, L_35, NULL);
		if (!L_36)
		{
			goto IL_009d;
		}
	}
	{
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_37 = V_0;
		return L_37;
	}

IL_009d:
	{
		uint32_t L_38 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add((int32_t)L_38, 2));
		uint32_t L_39 = V_1;
		if ((!(((uint32_t)L_39) >= ((uint32_t)((int32_t)-1060120681)))))
		{
			goto IL_00b1;
		}
	}
	{
		uint32_t L_40 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract((int32_t)L_40, ((int32_t)-1060120681)));
	}

IL_00b1:
	{
		BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* L_41 = V_0;
		NullCheck(L_41);
		BigInteger_Incr2_mE5DCADCC1DEDD4F3E48E326940D3C926E1A37808(L_41, NULL);
		goto IL_0022;
	}
}
// System.Boolean Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::IsPrimeAcceptable(Mono.Math.BigInteger,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SequentialSearchPrimeGeneratorBase_IsPrimeAcceptable_m2682AEC2B91FC89D1EB0799BFE5DA4C0F7F8C1D0 (SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175* __this, BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* ___bi0, RuntimeObject* ___context1, const RuntimeMethod* method) 
{
	{
		return (bool)1;
	}
}
// System.Void Mono.Math.Prime.Generator.SequentialSearchPrimeGeneratorBase::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequentialSearchPrimeGeneratorBase__ctor_mFE1A43FA14390E871ED0C9025B57B84D9A7AA754 (SequentialSearchPrimeGeneratorBase_t8A6B934B933C9FB775BAB1447E9B3B2F9C949175* __this, const RuntimeMethod* method) 
{
	{
		PrimeGeneratorBase__ctor_mECF0CD5B964A7E6FCE4F504719164114B8A678E9(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.UInt32 <PrivateImplementationDetails>::ComputeStringHash(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint32_t U3CPrivateImplementationDetailsU3E_ComputeStringHash_m5FD260C639BDBF6B570A90B23653DE3779848418 (String_t* ___s0, const RuntimeMethod* method) 
{
	uint32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		String_t* L_0 = ___s0;
		if (!L_0)
		{
			goto IL_002a;
		}
	}
	{
		V_0 = ((int32_t)-2128831035);
		V_1 = 0;
		goto IL_0021;
	}

IL_000d:
	{
		String_t* L_1 = ___s0;
		int32_t L_2 = V_1;
		NullCheck(L_1);
		Il2CppChar L_3;
		L_3 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_1, L_2, NULL);
		uint32_t L_4 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_multiply(((int32_t)((int32_t)L_3^(int32_t)L_4)), ((int32_t)16777619)));
		int32_t L_5 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_5, 1));
	}

IL_0021:
	{
		int32_t L_6 = V_1;
		String_t* L_7 = ___s0;
		NullCheck(L_7);
		int32_t L_8;
		L_8 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_7, NULL);
		if ((((int32_t)L_6) < ((int32_t)L_8)))
		{
			goto IL_000d;
		}
	}

IL_002a:
	{
		uint32_t L_9 = V_0;
		return L_9;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool PrimalityTest_Invoke_m7E9F9413908598A1270792B565D71288027AA552_inline (PrimalityTest_t98F5593C48D07FB794D7D850D117AF6A4729A35B* __this, BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08* ___bi0, int32_t ___confidence1, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, BigInteger_t890C3F24704442DA2A8C5B3F1E05E1588C7B5F08*, int32_t, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___bi0, ___confidence1, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength_4;
		return L_0;
	}
}
