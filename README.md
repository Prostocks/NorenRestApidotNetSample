# StarApi-dotnet

# TABLE

# OFCONTENTS

[INTRODUCTION](#_TOC_250047)

[About the API](#_TOC_250046)

[Initialize](#_TOC_250044)

[MakingRequests](#_TOC_250043)

[Login](#_TOC_250041)

[Logout](#_TOC_250040)

[LoginandUserDetails](#_TOC_250042)

[ForgotPassword](#_TOC_250039)

[ChangePassword](#_TOC_250038)

[UserDetails](#_TOC_250037)

[WatchLists](#_TOC_250036)

[GetWatchListNames](#_TOC_250035)

[GetWatchList](#_TOC_250034)

[SearchScrips](#_TOC_250033)

[AddScriptoWatchList](#_TOC_250032)

[DeleteScriptoWatchList](#_TOC_250031)

[GetSecurityInfo](#_TOC_250030)

[OrderandTrades](#_TOC_250029)

[PlaceOrder](#_TOC_250028)

[ModifyOrder](#_TOC_250027)

[CancelOrder](#_TOC_250026)

[ExitSNOOrder](#_TOC_250025)

[OrderMargin](#_TOC_250024)

[OrderBook](#_TOC_250023)

[MultiLegOrderBook](#_TOC_250022)

[SingleOrderHistory](#_TOC_250021)

[TradeBook](#_TOC_250020)

[ExchMsg](#_TOC_250019)

[OrderMargin](#_TOC_250018)

[PositionsBook](#_TOC_250017)

[ProductConversion](#_TOC_250016)

[HoldingsandLimits](#_TOC_250015)

[Holdings](#_TOC_250014)

[Limits](#_TOC_250013)

[MarketInfo](#_TOC_250012)

[GetIndexList](#_TOC_250011)

[GetTopListNames](#_TOC_250010)

[GetTopList](#_TOC_250009)

[GetTimePriceData(Chartdata)](#_TOC_250008)

[GetOptionChain](#_TOC_250007)

[Order Updates and MarketData Update](#_TOC_250006)

[Connect](#_TOC_250005)

[SubscribeMarketData](#_TOC_250004)

[UnSubscribeMarketData](#_TOC_250003)

[SubscribeMarketDataDepth](#_TOC_250002)

[UnsubscribeDepth](#_TOC_250001)

[SubscribeOrderUpdate](#_TOC_250000)

# Version

# History

| Date | Version | Changes | Details |
| --- | --- | --- | --- |
| 19-04-2021 | 1.0.0.1 | TouchlineBroker | TouchlineFeedadded.DocumentFormatupdated |
| 01-01-2021 | 1.0.0.0 | InitialRelease | BasedonNorenRestAPIv1.10.0 |

# INTRODUCTION: About the API

The Api is a dotNet wrapper of the StarWebAPI whichoffers a combination of Rest calls and WebSocket for the purposes of Trading.

API is developed on VisualStudio2019 and uses .NetStandard 2.0 
The dependency libraries are 
  Newtonsoft.Json 9.0.1
  Websocket.Client4.3.21
  
The namespace NorenRestApiWrapper and class NorenRestApi are of primary use and interest

### Initialize

To initialize the api the following are needed 

endPoint:The api end point as instructed by ProStocks
Appkey:The secretkey issued to you, **donot append the userid to it.**

### MakingRequests

We will be creating an object of NorenRestApi to make requests the callback is taken as an argument in the requestmethod.

```
LoginMessage loginMessage = new LoginMessage();**
loginMessage.uid = uid;
loginMessage.pwd = pwd;
loginMessage.factor2 = pan;
loginMessage.imei = "134243434";
loginMessage.source = "API";
loginMessage.appkey = appkey;

nApi.SendLogin(Program.OnAppLoginResponse, endPoint, loginMessage);
```

In the above example we are sending the Loginrequest,this method takes three arguments

1. Callback: this is the function where the application will be handling the response
2. Endpoint: NorenOMSaddress
3. MessageData:parametersoftherequestbeingmade.

The Callback is of signature

**public delegate void OnResponse(NorenResponseMsg Response,bool ok)**

A Typical callback will be handled as below

```
public static voidOnAppLoginResponse(NorenResponseMsg Response, bool ok)
{
   LoginResponse loginResp= Response as LoginResponse;

   if(loginResp.stat=="Ok")
   {
       //do all work here
   }
}
```

The Response is casted to expected DataType ie in this example being LoginResponse.
stat is checked to see if the request was successful.

# LoginandUserDetails

## Login

###### publicboolSendLogin(OnResponseresponse,stringendPoint,LoginMessagelogin)

##### RequestDetails:LoginMessage

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| endPoint |
 | TheServeripandport |

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| apkversion\* |
 | Applicationversion. |
| uid\* |
 | UserIdoftheloginuser |
| pwd\* |
 | Sha256oftheuserenteredpassword. |
| factor2\* |
 | DOBorPANasenteredbytheuser. |
| vc\* |
 | Vendorcodeprovidedbynorenteam,alongwithconnectionURLs |
| appkey\* |
 | Sha256ofuid|vendor\_key |
| imei\* |
 | Sendmacifuserslogsinfordesktop,imeiisfrommobile |
| ip\_address |
 | Optionalfield |

| source | WEB/MOB |
 |
| --- | --- | --- |

##### Example:

{\&quot;apkversion\&quot;:\&quot;1.0.0\&quot;,\&quot;uid\&quot;:\&quot;VIDYA\&quot;,\&quot;pwd\&quot;:\&quot;s3cur3Id\&quot;,\&quot;factor2\&quot;:\&quot;31-08-2017\&quot;,

\&quot;imei\&quot;:\&quot;134243434\&quot;,\&quot;source\&quot;:\&quot;MOB\&quot;}&quot;

##### ResponseDetails:LoginResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | LoginSuccessOrfailurestatus |
| susertoken |
 | Itwillbepresentonlyonloginsuccess.Thisdata tobesentin subsequent requests in jKey field and web socketconnectionwhileconnecting. |
| lastaccesstime |
 | Itwillbepresentonlyonloginsuccess. |
| spasswordreset | Y | IfYMandatorypasswordresettobeenforced.Otherwisethefieldwillbeabsent. |
| emsg |
 | ThiswillbepresentonlyifLoginfails. |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;20:18:47 19-05-2020&quot;,&quot;stat&quot;: &quot;Ok&quot;,

&quot;susertoken&quot;: &quot;3b97f4c67762259a9ded6dbd7bfafe2787e662b3870422ddd343a59895f423a0&quot;,&quot;lastaccesstime&quot;: &quot;1589899727&quot;

}

##### SampleFailureResponse:

{

&quot;request\_time&quot;: &quot;20:32:14 19-05-2020&quot;,&quot;stat&quot;: &quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;InvalidInput :WrongPassword&quot;

}

## Logout

###### publicboolSendLogout(OnResponseresponse)

##### RequestDetails:NoParams

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| - | - | - |

##### ResponseDetails:LogoutResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | LogoutSuccessOrfailurestatus |
| request\_time |
 | Itwillbepresentonlyonsuccessfullogout. |
| emsg |
 | ThiswillbepresentonlyifLogoutfails. |

##### SampleSuccessResponse:

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;request\_time&quot;:&quot;10:43:41 28-05-2020&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,&quot;emsg&quot;:&quot;ServerTimeout:&quot;

}

## ForgotPassword

###### publicboolSendForgotPassword(OnResponseresponse,stringendpoint,stringuser,stringpan,stringdob)

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| endpoint |
 | WebApiendpoint |

| user\* |
 | UserId |
| --- | --- | --- |
| pan\* |
 | Panoftheuser |
| dob\* |
 | Dateofbirth |

##### ResponseDetails:ForgotPasswordResponse.

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | PasswordresetisSuccessOrfailurestatus |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Thiswillbepresentonlyifpasswordresetfails.(&quot;InvalidUserorUserDetails&quot;) |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;10:52:5628-05-2020&quot;,&quot;stat&quot;:&quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;request\_time&quot;:&quot;17:42:1326-05-2020&quot;,&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;ErrorOccurred :Wrong userid oruser details&quot;

}

## ChangePassword

###### publicboolChangepwd(OnResponseresponse,Changepwdchangepwd)

##### RequestDetails:Changepwd

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| uid\* |
 | UserId |
| oldpwd\* |
 | Sha256ofoldpassword |
| pwd\* |
 | Newpasswordinplaintext |

##### ResponseDetails:ChangepwdResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | PasswordresetisSuccessOrfailurestatus |
| request\_time |
 | Responsereceivedtime. |
| dmsg |
 | Thiswillbepresentonlyincaseofsuccess.Numberofdaystoexpirywillbepresentinsame. |
| emsg |
 | Thiswillbepresentonlyifpasswordchangefails |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;10:20:0427-05-2020&quot;,&quot;stat&quot;:&quot;Ok&quot;,

&quot;dmag&quot;:&quot;PasswordChangeSuccess.Yournewpasswordwillexpirein15&quot;

}

##### SampleFailureResponse:

{

&quot;request\_time&quot;:&quot;10:21:0927-05-2020&quot;,&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Error Occurred : Password already used&quot;

}

## UserDetails

###### publicboolSendGetUserDetails(OnResponseresponse)

##### RequestDetails:NoParams

##### ResponseDetails:UserDetailsResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |

| stat | OkorNot\_Ok | Userdetailssuccessorfailureindication. |
| --- | --- | --- |
| exarr |
 | arrayofstringswithenabledexchangenames |
| orarr |
 | arrayofstringswithenabledpricetypesforuser |
| prarr |
 | arrayofProductObjwithenabledproducts,asdefinedbelow. |
| brkname |
 | Brokerid |
| brnchid |
 | Branchid |
| email |
 |
 |
| actid |
 |
 |
| uprev |
 | AlwaysitwillbeanINVESTOR,othertypesofusernotallowedtologinusingthisAPI. |
| request\_time |
 | Itwillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors. |

##### ProductObjformat

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| prd |
 | Productname |
| s\_prdt\_ali |
 | Productdisplayname |
| exch |
 | arrayofstringswithenabled,allowedexchangenames |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;20:20:04 19-05-2020&quot;,&quot;prarr&quot;: [

{ &quot;prd&quot;:&quot;C&quot;,

&quot;s\_prdt\_ali&quot; : &quot;Delivery&quot;,&quot;exch&quot;:[&quot;NSE&quot;,&quot;BSE&quot;]

},

{ &quot;prd&quot;:&quot;I&quot;,

&quot;s\_prdt\_ali&quot; : &quot;Intraday&quot;,

&quot;exch&quot; :[&quot;NSE&quot;, &quot;BSE&quot;, &quot;NFO&quot;]

},

, { &quot;prd&quot;:&quot;H&quot;,

&quot;s\_prdt\_ali&quot; : &quot;High Leverage&quot;,&quot;exch&quot;:[&quot;NSE&quot;,&quot;BSE&quot;,&quot;NFO&quot;]

},

{ &quot;prd&quot;:&quot;B&quot;,

&quot;s\_prdt\_ali&quot; : &quot;Bracket Order&quot;,&quot;exch&quot;:[&quot;NSE&quot;,&quot;BSE&quot;,&quot;NFO&quot;]

}

],

&quot;exarr&quot;: [

&quot;NSE&quot;,&quot;NFO&quot;

],

&quot;orarr&quot;: [

&quot;MKT&quot;,

&quot;LMT&quot;,

&quot;SL-LMT&quot;,

&quot;SL-MKT&quot;,

&quot;DS&quot;,

&quot;2L&quot;,

&quot;3L&quot;,&quot;4L&quot;

],

&quot;brkname&quot;:&quot;VIDYA&quot;,

&quot;brnchid&quot;: &quot;VIDDU&quot;,

&quot;email&quot;:[&quot;gururaj@gmail.com&quot;,](mailto:gururaj@gmail.com)&quot;actid&quot;: &quot;GURURAJ&quot;,

&quot;uprev&quot;:&quot;INVESTOR&quot;,

&quot;stat&quot;: &quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;: &quot;Not\_Ok&quot;,

&quot;emsg&quot;: &quot;Session Expired : Invalid Session Key&quot;

}

# WatchLists

## GetWatchListNames

###### publicboolSendGetMWList(OnResponseresponse)

##### Request Details : No ParamsResponseDetails:MWListResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | MWListsuccessorfailureindication. |
| values |
 | WatchListnamesasaarrayofstrings. |
| request\_time |
 | Itwillbepresentonlyinasuccessfulresponse. |
| emsg |
 | ThiswillbepresentonlyincaseoferrorsorNoWatchListsaresetyet. |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;12:34:52 21-05-2020&quot;,&quot;values&quot;: [

&quot;default&quot;,&quot;WL&quot;

],

&quot;stat&quot;: &quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;: &quot;Not\_Ok&quot;,

&quot;emsg&quot;: &quot;Session Expired : Invalid Session Key&quot;

}

## GetWatchList

###### publicboolSendGetMarketWatch(OnResponseresponse,stringwlname)

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| wlname\* |
 | NameoftheWatchlist,forwhichscriplistisrequired. |

##### ResponseDetails:MarketWatchResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Marketwatchsuccessorfailureindication. |
| values |
 | Arrayofobjects.(objectfieldsgiveninbelowtable) |
| request\_time |
 | Itwillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors.Thatis:1)InvalidInput:InvalidWatchListName2)SessionExpired |

| **Fields of object**** in ****values**** Array **|** Possible ****value** | **Description** |
| --- | --- | --- |
| exch | NSE, BSE,NFO... | Exchange |
| tsym |
 | Tradingsymbolofthescrip(contract) |
| token |
 | Tokenofthescrip(contract) |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;13:25:17 21-05-2020&quot;,&quot;values&quot;: [

{

&quot;exch&quot;: &quot;BSE&quot;,

&quot;token&quot;: &quot;972889&quot;,&quot;tsym&quot;:&quot;915PTCIF27&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;13&quot;,

&quot;tsym&quot;: &quot;ABB-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;22&quot;,

&quot;tsym&quot;: &quot;ACC-EQ&quot;

}

],

&quot;stat&quot;: &quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Invalid Input : Missing uid or wlname.&quot;

}

## SearchScrips

###### publicboolSendSearchScrip(OnResponseresponse,stringexch,stringsearchtxt)

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch |
 | Exchange (Select from &#39;exarr&#39; Array providedin User Detailsresponse) |
| stext\* |
 | SearchText |

##### ResponseDetails:SearchScripResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Marketwatchsuccessorfailureindication. |
| values |
 | ArrayofScripItemobjects.(objectfieldsgiveninbelowtable) |
| emsg |
 | Thiswillbepresentonlyincaseoferrors.Thatis:1)InvalidInput2)SessionExpired |

##### ScripItem

| **Fields of object**** in ****values**** Array **|** Possible ****value** | **Description** |
| --- | --- | --- |
| exch | NSE, BSE,NFO... | Exchange |
| tsym |
 | Tradingsymbolofthescrip(contract) |
| token |
 | Tokenofthescrip(contract) |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |

##### SampleSuccessResponse:

{

&quot;stat&quot;:&quot;Ok&quot;,&quot;values&quot;: [

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;18069&quot;,

&quot;tsym&quot;:&quot;REL100NAV-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;24225&quot;,

&quot;tsym&quot;: &quot;RELAXO-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;4327&quot;,

&quot;tsym&quot;:&quot;RELAXOFOOT-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;18068&quot;,

&quot;tsym&quot;:&quot;RELBANKNAV-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;2882&quot;,

&quot;tsym&quot;:&quot;RELCAPITAL-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;18070&quot;,

&quot;tsym&quot;:&quot;RELCONSNAV-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;18071&quot;,

&quot;tsym&quot;:&quot;RELDIVNAV-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;18072&quot;,

&quot;tsym&quot;:&quot;RELGOLDNAV-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;2885&quot;,

&quot;tsym&quot;: &quot;RELIANCE-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;15068&quot;,

&quot;tsym&quot;: &quot;RELIGARE-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;553&quot;,

&quot;tsym&quot;: &quot;RELINFRA-EQ&quot;

},

{

&quot;exch&quot;: &quot;NSE&quot;,

&quot;token&quot;: &quot;18074&quot;,

&quot;tsym&quot;:&quot;RELNV20NAV-EQ&quot;

}

]

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;No Data :&quot;

}

## AddScriptoWatchList

###### publicboolSendAddMultiScripsToMW(OnResponseresponse,stringwatchlist,stringscrips)

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| watchlist\* |
 | NameoftheWatchlist,forwhichscriplistisrequired. |
| scrips\* |
 | Listofscrips,exampleformatNSE|22#BSE|506734 |

##### ResponseDetails:StandardResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Watchlistupdatesuccessorfailureindication. |
| request\_time |
 | Itwillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors. |

|
 |
 | Thatis:1)InvalidInput2)SessionExpired |
| --- | --- | --- |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;13:50:40 21-05-2020&quot;,&quot;stat&quot;: &quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Session Expired : Invalid Session Key&quot;

}

## DeleteScriptoWatchList

**public bool SendDeleteMultiMWScrips**** ( ****OnResponse response**** , ****string watchlist**** , ****string**** scrips****)**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| watchlist\* |
 | NameoftheWatchlist,forwhichscriplistisrequired. |
| scrips\* |
 | Listofscrips,exampleformatNSE|22#BSE|506734 |

##### ResponseDetails:StandardResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Watchlistupdatesuccessorfailureindication. |
| request\_time |
 | Itwillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors.Thatis:1)InvalidInput2)SessionExpired |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;13:50:40 21-05-2020&quot;,&quot;stat&quot;: &quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Invalid Input : Missing uid or wlname or scrips.&quot;

}

## GetSecurityInfo

**public bool SendGetSecurityInfo**** ( ****OnResponse response**** , ****string exch**** , ****string token**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch |
 | Exchange |
| token |
 | ContractToken |

##### ResponseDetails:GetSecurityInfoResponse

Responsedatawillhavebelowfields.

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| request\_time |
 | Itwillbepresentonlyinasuccessfulresponse. |
| stat | OkorNot\_Ok | Marketwatchsuccessorfailureindication. |
| exch | NSE, BSE,NFO... | Exchange |
| tsym |
 | TradingSymbol |
| cname |
 | CompanyName |

| symnam |
 | SymbolName |
| --- | --- | --- |
| seg |
 | Segment |
| exd |
 | ExpiryDate |
| instname |
 | IntrumentName |
| strprc |
 | StrikePrice |
| optt |
 | OptionType |
| isin |
 | ISIN |
| ti |
 | TickSize |
| ls |
 | LotSize |
| pp |
 | Priceprecision |
| mult |
 | Multiplier |
| gp\_nd |
 | gn/gd\*pn/pd |
| prcunt |
 | PriceUnits |
| prcqqty |
 | PriceQuoteQty |
| trdunt |
 | TradeUnits |
| delunt |
 | DeliveryUnits |
| frzqty |
 | FreezeQty |
| gsmind |
 | scripupdateGsmInd |
| elmbmrg |
 | ElmBuyMargin |
| elmsmrg |
 | ElmSellMargin |
| addbmrg |
 | AdditionalLongMargin |
| addsmrg |
 | AdditionalShortMargin |
| splbmrg |
 | SpecialLongMargin |
| splsmrg |
 | SpecialShortMargin |
| delmrg |
 | DeliveryMargin |

| tenmrg |
 | TenderMargin |
| --- | --- | --- |
| tenstrd |
 | TenderStartDate |
| tenendd |
 | TenderEndEate |
| exestrd |
 | ExerciseStartDate |
| exeendd |
 | ExerciseEndDate |
| elmmrg |
 | ElmMargin |
| varmrg |
 | VarMargin |
| expmrg |
 | ExposureMargin |
| token |
 | ContractToken |
| prcftr\_d |
 | ((GN/GD)\*(PN/PD)) |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;17:43:38 31-10-2020&quot;,&quot;stat&quot;: &quot;Ok&quot;,

&quot;exch&quot;: &quot;NSE&quot;,

&quot;tsym&quot;: &quot;ACC-EQ&quot;,&quot;cname&quot;:&quot;ACCLIMITED&quot;,

&quot;symname&quot;: &quot;ACC&quot;,

&quot;seg&quot;: &quot;EQT&quot;,

&quot;instname&quot;: &quot;EQ&quot;,

&quot;isin&quot;:&quot;INE012A01025&quot;,&quot;pp&quot;: &quot;2&quot;,

&quot;ls&quot;: &quot;1&quot;,

&quot;ti&quot;: &quot;0.05&quot;,

&quot;mult&quot;: &quot;1&quot;,

&quot;prcftr\_d&quot;: &quot;(1 / 1 ) \* (1 / 1)&quot;,&quot;trdunt&quot;: &quot;[ACC.BO](http://acc.bo/)&quot;,

&quot;delunt&quot;: &quot;ACC&quot;,

&quot;token&quot;: &quot;22&quot;,

&quot;varmrg&quot;: &quot;40.00&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,&quot;request\_time&quot;:&quot;10:50:5410-12-2020&quot;,&quot;emsg&quot;:&quot;ErrorOccurred:5\&quot;nodata\&quot;&quot;

}

# OrderandTrades

## PlaceOrder

**public bool SendPlaceOrder**** ( ****OnResponse response**** , ****PlaceOrder order**** )**

##### RequestDetails:PlaceOrder

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| uid\* |
 | LoggedinUserId |
| actid\* |
 | LoginusersaccountID |
| exch\* | NSE/NFO/BSE/MCX | Exchange (Select from &#39;exarr&#39; Array providedin User Detailsresponse) |
| tsym\* |
 | Unique id of contract on which order to be placed. (use urlencodingtoavoidspecialcharerrorforsymbolslikeM&amp;M) |
| qty\* |
 | OrderQuantity |
| prc\* |
 | OrderPrice |
| trgprc |
 | OnlytobesentincaseofSL/SL-Morder. |
| dscqty |
 | Disclosedquantity(Max10%forNSE,and50%forMCX) |
| prd\* | C/M/H | Product name (Select from &#39;prarr&#39; Arrayprovided in UserDetails response, and if same is allowed for selected,exchange.Showproductdisplayname,forusertoselect,andsendcorrespondingprdinAPIcall) |
| trantype\* | B/S | B-\&gt;BUY,S-\&gt;SELL |
| prctyp\* | LMT/MKT/SL-LMT/SL-MKT/DS/2L/3L |
 |

| ret\* | DAY/EOS/IOC | Retentiontype(Showoptionsasperallowedexchanges) |
| --- | --- | --- |
| remarks |
 | Anytagbyusertomarkorder. |
| ordersource | MOB/WEB/TT | Usedtogenerateexchangeinfofields. |
| bpprc |
 | BookProfitPriceapplicableonlyifproductisselectedasB(Bracketorder) |
| blprc |
 | BooklossPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| trailprc |
 | TrailingPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| amo |
 | Yes,Ifnotsent,ofNot&quot;Yes&quot;,willbetreatedasRegularorder. |
| tsym2 |
 | Tradingsymbolofsecondleg,mandatoryforpricetype2Land 3L (use url encoding to avoid special char error forsymbolslikeM&amp;M) |
| trantype2 |
 | Transactiontypeofsecondleg,mandatoryforpricetype2Land3L |
| qty2 |
 | Quantityforsecondleg,mandatoryforpricetype2Land3L |
| prc2 |
 | Priceforsecondleg,mandatoryforpricetype2Land3L |
| tsym3 |
 | Trading symbol of third leg, mandatory for price type 3L(useurlencodingtoavoidspecialcharerrorforsymbolslikeM&amp;M) |
| trantype3 |
 | Transactiontypeofthirdleg,mandatoryforpricetype3L |
| qty3 |
 | Quantityforthirdleg,mandatoryforpricetype3L |
| prc3 |
 | Priceforthirdleg,mandatoryforpricetype3L |

##### Example:

{\&quot;uid\&quot;:\&quot;VIDYA\&quot;,\&quot;actid\&quot;:\&quot;CLIENT1\&quot;,\&quot;exch\&quot;:\&quot;NSE\&quot;,\&quot;tsym\&quot;:\&quot;ACC-EQ\&quot;,\&quot;qty\&quot;:\&quot;50\&quot;,

\&quot;price\&quot;:\&quot;1400\&quot;,\&quot;prd\&quot;:\&quot;H\&quot;,\&quot;trantype\&quot;:\&quot;B\&quot;,\&quot;prctyp\&quot;:\&quot;LMT\&quot;,\&quot;ret\&quot;:\&quot;DAY\&quot;}&quot;\

##### ResponseDetails:PlaceOrderResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Placeordersuccessorfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| norenordno |
 | ItwillbepresentonlyonsuccessfulOrderplacementtoOMS. |
| emsg |
 | ThiswillbepresentonlyifOrderplacementfails |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;: &quot;10:48:03 20-05-2020&quot;,&quot;stat&quot;: &quot;Ok&quot;,

&quot;norenordno&quot;: &quot;20052000000017&quot;

}

##### SampleErrorResponse:

{

&quot;stat&quot;: &quot;Not\_Ok&quot;,

&quot;request\_time&quot;: &quot;20:40:01 19-05-2020&quot;,&quot;emsg&quot;:&quot;ErrorOccurred:2\&quot;invalidinput\&quot;&quot;

}

## ModifyOrder

**public bool SendModifyOrder**** ( ****OnResponse response**** , ****ModifyOrder order**** )**

##### RequestDetails:ModifyOrder

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch\* |
 | Exchange |
| norenordno\* |
 | Norenordernumber,whichneedstobemodified |
| prctyp | LMT/MKT/SL-MKT /SL-LMT | Thiscanbemodified. |

| prc |
 | Modified/Newprice |
| --- | --- | --- |
| qty |
 | Modified/NewQuantity |
| tsym\* |
 | Unqueidofcontractonwhichorderwasplaced.Can&#39;tbemodified, must be the same as that of original order. (useurl encoding to avoid special char error for symbols likeM&amp;M) |
| ret | DAY/IOC/EOS | NewRetentiontypeoftheorder |
| trgprc |
 | NewtriggerpriceincaseofSL-MKTorSL-LMT |
| uid\* |
 | Useridoftheloggedinuser. |
| bpprc |
 | BookProfitPriceapplicableonlyifproductisselectedasB(Bracketorder) |
| blprc |
 | BooklossPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| trailprc |
 | TrailingPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |

##### ResponseDetails:ModifyOrderResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Modifyordersuccessorfailureindication. |
| result |
 | NorenOrdernumberoftheordermodified. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | ThiswillbepresentonlyifOrdermodificationfails |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;14:14:0826-05-2020&quot;,&quot;stat&quot;:&quot;Ok&quot;,

&quot;result&quot;:&quot;20052600000103&quot;

}

##### SampleFailureResponse:

{

&quot;request\_time&quot;:&quot;16:03:2928-05-2020&quot;,&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Rejected : ORA:Order not found&quot;

}

## CancelOrder

**public bool SendCancelOrder**** ( ****OnResponse response**** , ****string norenordno**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| norenordno\* |
 | Norenordernumber,whichneedstobemodified |

##### ResponseDetails:CancelOrderResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Cancelordersuccessorfailureindication. |
| result |
 | NorenOrdernumberofthecanceledorder. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | ThiswillbepresentonlyifOrdercancelationfails |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;14:14:1026-05-2020&quot;,&quot;stat&quot;:&quot;Ok&quot;,&quot;result&quot;:&quot;20052600000103&quot;

}

##### SampleFailureResponse:

{

&quot;request\_time&quot;:&quot;16:01:4828-05-2020&quot;,&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Rejected : ORA:Order not found to Cancel&quot;

}

## ExitSNOOrder

**public bool SendExitSNOOrder**** ( ****OnResponse response**** , ****string norenordno**** , ****string product**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| norenordno\* |
 | Norenordernumber,whichneedstobemodified |
| prd\* | H/B | AllowedforonlyHandBproducts(Coverorderandbracketorder) |

##### ResponseDetails:ExitSNOOrderResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Cancelordersuccessorfailureindication. |
| dmsg |
 | Displaymessage,(willbepresentonlyincaseofsuccess). |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | ThiswillbepresentonlyifOrdercancelationfails |

## OrderMargin

**public bool SendGetOrderMargin**** ( ****OnResponse response**** , ****OrderMargin ordermargin**** )**

##### RequestDetails:OrderMargin

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| uid\* |
 | LoggedinUserId |
| actid\* |
 | LoginusersaccountID |

| exch\* | NSE/NFO/BSE/MCX | Exchange (Select from &#39;exarr&#39; Array providedin User Detailsresponse) |
| --- | --- | --- |
| tsym\* |
 | Unique id of contract on which order to be placed. (use urlencodingtoavoidspecialcharerrorforsymbolslikeM&amp;M) |
| qty\* |
 | OrderQuantity |
| prc\* |
 | OrderPrice |
| trgprc |
 | OnlytobesentincaseofSL/SL-Morder. |
| prd\* | C/M/H | Product name (Select from &#39;prarr&#39; Arrayprovided in UserDetails response, and if same is allowed for selected,exchange.Showproductdisplayname,forusertoselect,andsendcorrespondingprdinAPIcall) |
| trantype\* | B/S | B-\&gt;BUY,S-\&gt;SELL |
| prctyp\* | LMT/MKT/SL-LMT/SL-MKT |
 |
| blprc |
 | BooklossPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| rorgqty |
 | Optional field. Application only for modify order,open orderquantity |
| fillshares |
 | Optional field. Application only for modify order,quantityalreadyfilled. |
| rorgprc |
 | Optional field. Application only for modify order,open orderprice |
| orgtrgprc |
 | Optional field. Application only for modify order,open ordertriggerprice |
| norenordno |
 | Optionalfield.ApplicationonlyforHorBordermodification |
| snonum |
 | Optionalfield.ApplicationonlyforHorBordermodification |

##### ResponseDetails:OrderMarginResponse

| **Fields** | **Possible** | **Description** |
| --- | --- | --- |

|
 | **value** |
 |
| --- | --- | --- |
| stat | OkorNot\_Ok | Placeordersuccessorfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| remarks |
 | Thisfieldwillbeavailableonlyonsuccess. |
| cash |
 | Totalcreditsavailablefororder |
| marginused |
 | Totalmarginused. |
| emsg |
 | ThiswillbepresentonlyifOrderplacementfails |

## OrderBook

**public bool SendGetOrderBook**** ( ****OnResponse response**** , ****string product**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| prd | H/M/... | Productname |

##### ResponseDetails:OrderBookResponselistofOrderBookItem

ResponsedatawillbeinArrayofobjectswithbelowfieldsincaseofsuccess.

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Orderbooksuccessorfailureindication. |
| exch |
 | ExchangeSegment |
| tsym |
 | Tradingsymbol/contractonwhichorderisplaced. |
| norenordno |
 | NorenOrderNumber |
| prc |
 | OrderPrice |
| qty |
 | OrderQuantity |
| prd |
 | Displayproductaliasname,usingprarrreturnedinuser |

|
 |
 | details. |
| --- | --- | --- |
| status |
 | Orderstatus |
| trantype | B/S | Transactiontypeoftheorder |
| prctyp | LMT/MKT | Pricetype |
| fillshares |
 | TotalTradedQuantityofthisorder |
| avgprc |
 | Averagetradepriceoftotaltradedquantity |
| rejreason |
 | Iforderisrejected,reasonintextform |
| exchordid |
 | ExchangeOrderNumber |
| cancelqty |
 | Canceledquantityfororderwhichisinstatuscancelled. |
| remarks |
 | AnymessageEnteredduringorderentry. |
| dscqty |
 | Orderdisclosedquantity. |
| trgprc |
 | Ordertriggerprice |
| ret | DAY/IOC/EOS | Ordervalidity |
| uid |
 |
 |
| actid |
 |
 |
| bpprc |
 | BookProfitPriceapplicableonlyifproductisselectedasB(Bracketorder) |
| blprc |
 | BooklossPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| trailprc |
 | TrailingPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| amo |
 | Yes/No |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |

| token |
 | ContractToken |
| --- | --- | --- |
| orddttm |
 |
 |
| ordenttm |
 |
 |
| extm |
 |
 |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Not\_Ok | Orderbookfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Errormessage |

##### SampleSuccessOutput:

_Successresponse:_

[

{

&quot;stat&quot; : &quot;Ok&quot;,

&quot;exch&quot; : &quot;NSE&quot; ,

&quot;tsym&quot; : &quot;ACC-EQ&quot; ,

&quot;norenordno&quot; : &quot;20062500000001223&quot;,

&quot;prc&quot; : &quot;127230&quot;,

&quot;qty&quot; : &quot;100&quot;,

&quot;prd&quot; : &quot;C&quot;,

&quot;status&quot;: &quot;Open&quot;,

&quot;trantype&quot; : &quot;B&quot;,

&quot;prctyp&quot; : &quot;LMT&quot;,

&quot;fillshares&quot; : &quot;0&quot;,

&quot;avgprc&quot; : &quot;0&quot;,

&quot;exchordid&quot; : &quot;250620000000343421&quot;,&quot;uid&quot;: &quot;VIDYA&quot;,

&quot;actid&quot; : &quot;CLIENT1&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;amo&quot;:&quot;Yes&quot;

},

{

&quot;stat&quot; : &quot;Ok&quot;,

&quot;exch&quot; : &quot;NSE&quot; ,

&quot;tsym&quot; : &quot;ABB-EQ&quot; ,

&quot;norenordno&quot; : &quot;20062500000002543&quot;,

&quot;prc&quot; : &quot;127830&quot;,

&quot;qty&quot; : &quot;50&quot;,

&quot;prd&quot; : &quot;C&quot;,

&quot;status&quot;: &quot;REJECT&quot;,

&quot;trantype&quot; : &quot;B&quot;,

&quot;prctyp&quot; : &quot;LMT&quot;,

&quot;fillshares&quot; : &quot;0&quot;,

&quot;avgprc&quot; : &quot;0&quot;,

&quot;rejreason&quot;:&quot;Insufficientfunds&quot;&quot;uid&quot;:&quot;VIDYA&quot;,

&quot;actid&quot; : &quot;CLIENT1&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;amo&quot; : &quot;No&quot;

}

]

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Session Expired : Invalid Session Key&quot;

}

## MultiLegOrderBook

**public bool SendGetMultiLegOrderBook**** ( ****OnResponse response**** , ****string product**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| prd | H/M/... | Productname |

##### ResponseDetails:MultiLegOrderBookResponselistofMultiLegOrderBookItem

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Orderbooksuccessorfailureindication. |
| exch |
 | ExchangeSegment |

| tsym |
 | Tradingsymbol/contractonwhichorderisplaced. |
| --- | --- | --- |
| norenordno |
 | NorenOrderNumber |
| prc |
 | OrderPrice |
| qty |
 | OrderQuantity |
| prd |
 | Displayproductaliasname,usingprarrreturnedinuserdetails. |
| status |
 | Orderstatus |
| trantype | B/S | Transactiontypeoftheorder |
| prctyp | LMT/MKT | Pricetype |
| fillshares |
 | TotalTradedQuantityofthisorder |
| avgprc |
 | Averagetradepriceoftotaltradedquantity |
| rejreason |
 | Iforderisrejected,reasonintextform |
| exchordid |
 | ExchangeOrderNumber |
| cancelqty |
 | Canceledquantityfororderwhichisinstatuscancelled. |
| remarks |
 | AnymessageEnteredduringorderentry. |
| dscqty |
 | Orderdisclosedquantity. |
| trgprc |
 | Ordertriggerprice |
| ret | DAY/IOC/EOS | Ordervalidity |
| uid |
 |
 |
| actid |
 |
 |
| bpprc |
 | BookProfitPriceapplicableonlyifproductisselectedasB(Bracketorder) |
| blprc |
 | BooklossPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| trailprc |
 | TrailingPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |

| amo |
 | Yes/No |
| --- | --- | --- |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |
| tsym2 |
 | Tradingsymbolofsecondleg,mandatoryforpricetype2Land3L |
| trantype2 |
 | Transactiontypeofsecondleg,mandatoryforpricetype2Land3L |
| qty2 |
 | Quantityforsecondleg,mandatoryforpricetype2Land3L |
| prc2 |
 | Priceforsecondleg,mandatoryforpricetype2Land3L |
| tsym3 |
 | Tradingsymbolofthirdleg,mandatoryforpricetype3L |
| trantype3 |
 | Transactiontypeofthirdleg,mandatoryforpricetype3L |
| qty3 |
 | Quantityforthirdleg,mandatoryforpricetype3L |
| prc3 |
 | Priceforthirdleg,mandatoryforpricetype3L |
| fillshares2 |
 | TotalTradedQuantityof2ndLeg |
| avgprc2 |
 | Averagetradepriceoftotaltradedquantityfor2ndleg |
| fillshares3 |
 | TotalTradedQuantityof3rdLeg |
| avgprc3 |
 | Averagetradepriceoftotaltradedquantityfor3rdleg |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Not\_Ok | Orderbookfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Errormessage |

## SingleOrderHistory

**public bool SendGetOrderHistory**** ( ****OnResponse response**** , ****string norenordno**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| norenordno\* |
 | NorenOrderNumber |

##### ResponseDetails:OrderHistoryResponselistofSingleOrdHistItem

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Orderbooksuccessorfailureindication. |
| exch |
 | ExchangeSegment |
| tsym |
 | Tradingsymbol/contractonwhichorderisplaced. |
| norenordno |
 | NorenOrderNumber |
| prc |
 | OrderPrice |
| qty |
 | OrderQuantity |
| prd |
 | Displayproductaliasname,usingprarrreturnedinuserdetails. |
| status |
 | Orderstatus |
| rpt |
 | ReportType(fill/completeetc) |
| trantype | B/S | Transactiontypeoftheorder |
| prctyp | LMT/MKT | Pricetype |
| fillshares |
 | TotalTradedQuantityofthisorder |
| avgprc |
 | Averagetradepriceoftotaltradedquantity |
| rejreason |
 | Iforderisrejected,reasonintextform |
| exchordid |
 | ExchangeOrderNumber |

| cancelqty |
 | Canceledquantityfororderwhichisinstatuscancelled. |
| --- | --- | --- |
| remarks |
 | AnymessageEnteredduringorderentry. |
| dscqty |
 | Orderdisclosedquantity. |
| trgprc |
 | Ordertriggerprice |
| ret | DAY/IOC/EOS | Ordervalidity |
| uid |
 |
 |
| actid |
 |
 |
| bpprc |
 | BookProfitPriceapplicableonlyifproductisselectedasB(Bracketorder) |
| blprc |
 | BooklossPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| trailprc |
 | TrailingPriceapplicableonlyifproductisselectedasHandB(HighLeverageandBracketorder) |
| amo |
 | Yes/No |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |
| token |
 | ContractToken |
| orddttm |
 |
 |
| ordenttm |
 |
 |
| extm |
 |
 |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Not\_Ok | Orderbookfailureindication. |

| request\_time |
 | Responsereceivedtime. |
| --- | --- | --- |
| emsg |
 | Errormessage |

##### SampleSuccessOutput:

[

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;norenordno&quot;:&quot;20121300065716&quot;,&quot;uid&quot;:&quot;DEMO1&quot;,

&quot;actid&quot;:&quot;DEMO1&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;tsym&quot;:&quot;ACCELYA-EQ&quot;,

&quot;qty&quot;:&quot;180&quot;,

&quot;trantype&quot;:&quot;B&quot;,

&quot;prctyp&quot;:&quot;LMT&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;token&quot;:&quot;7053&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;0.05&quot;,

&quot;prc&quot;:&quot;800.00&quot;,

&quot;avgprc&quot;:&quot;800.00&quot;,

&quot;discqty&quot;:&quot;0&quot;,

&quot;prd&quot;:&quot;M&quot;,

&quot;status&quot;:&quot;COMPLETE&quot;,&quot;rpt&quot;:&quot;Fill&quot;,

&quot;fillshares&quot;:&quot;180&quot;,

&quot;norentm&quot;:&quot;19:59:32 13-12-2020&quot;,

&quot;exch\_tm&quot;:&quot;00:00:00 01-01-1980&quot;,

&quot;remarks&quot;:&quot;WC TEST Order&quot;,&quot;exchordid&quot;:&quot;6858&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;norenordno&quot;:&quot;20121300065716&quot;,

&quot;uid&quot;:&quot;DEMO1&quot;,

&quot;actid&quot;:&quot;DEMO1&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;tsym&quot;:&quot;ACCELYA-EQ&quot;,

&quot;qty&quot;:&quot;180&quot;,

&quot;trantype&quot;:&quot;B&quot;,

&quot;prctyp&quot;:&quot;LMT&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;token&quot;:&quot;7053&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;0.05&quot;,

&quot;prc&quot;:&quot;800.00&quot;,

&quot;discqty&quot;:&quot;0&quot;,

&quot;prd&quot;:&quot;M&quot;,

&quot;status&quot;:&quot;OPEN&quot;,

&quot;rpt&quot;:&quot;New&quot;,

&quot;norentm&quot;:&quot;19:59:32 13-12-2020&quot;,

&quot;exch\_tm&quot;:&quot;00:00:00 01-01-1980&quot;,

&quot;remarks&quot;:&quot;WC TEST Order&quot;,&quot;exchordid&quot;:&quot;6858&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;norenordno&quot;:&quot;20121300065716&quot;,&quot;uid&quot;:&quot;DEMO1&quot;,

&quot;actid&quot;:&quot;DEMO1&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;tsym&quot;:&quot;ACCELYA-EQ&quot;,

&quot;qty&quot;:&quot;180&quot;,

&quot;trantype&quot;:&quot;B&quot;,

&quot;prctyp&quot;:&quot;LMT&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;token&quot;:&quot;7053&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;0.05&quot;,

&quot;prc&quot;:&quot;800.00&quot;,

&quot;discqty&quot;:&quot;0&quot;,

&quot;prd&quot;:&quot;M&quot;,

&quot;status&quot;:&quot;PENDING&quot;,

&quot;rpt&quot;:&quot;PendingNew&quot;,

&quot;norentm&quot;:&quot;19:59:32 13-12-2020&quot;,

&quot;remarks&quot;:&quot;WC TEST Order&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;norenordno&quot;:&quot;20121300065716&quot;,&quot;uid&quot;:&quot;DEMO1&quot;,

&quot;actid&quot;:&quot;DEMO1&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;tsym&quot;:&quot;ACCELYA-EQ&quot;,

&quot;qty&quot;:&quot;180&quot;,

&quot;trantype&quot;:&quot;B&quot;,

&quot;prctyp&quot;:&quot;LMT&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;token&quot;:&quot;7053&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;0.05&quot;,

&quot;prc&quot;:&quot;800.00&quot;,

&quot;prd&quot;:&quot;M&quot;,

&quot;status&quot;:&quot;PENDING&quot;,

&quot;rpt&quot;:&quot;NewAck&quot;,

&quot;norentm&quot;:&quot;19:59:32 13-12-2020&quot;,

&quot;remarks&quot;:&quot;WC TEST Order&quot;

}

]

## TradeBook

**public bool SendGetTradeBook**** ( ****OnResponse response**** , ****string account**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| actid\* |
 | AccountIdofloggedinuser |

##### ResponseDetails:TradeBookResponselistofTradeBookItem

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Orderbooksuccessorfailureindication. |
| exch |
 | ExchangeSegment |
| tsym |
 | Tradingsymbol/contractonwhichorderisplaced. |
| norenordno |
 | NorenOrderNumber |
| qty |
 | OrderQuantity |
| prd |
 | Displayproductaliasname,usingprarrreturnedinuserdetails. |
| trantype | B/S | Transactiontypeoftheorder |
| prctyp | LMT/MKT | Pricetype |
| fillshares |
 | TotalTradedQuantityofthisorder |
| avgprc |
 | Averagetradepriceoftotaltradedquantity |
| exchordid |
 | ExchangeOrderNumber |
| remarks |
 | AnymessageEnteredduringorderentry. |
| ret | DAY/IOC/EOS | Ordervalidity |
| uid |
 |
 |
| actid |
 |
 |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |

| ls |
 | Lotsize |
| --- | --- | --- |
| cstFrm |
 | CustomFirm |
| fltm |
 | FillTime |
| flid |
 | FillID |
| flqty |
 | FillQty |
| flprc |
 | FillPrice |
| ordersource |
 | OrderSource |
| token |
 | Token |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Not\_Ok | Orderbookfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Errormessage |

##### SampleSuccessOutput:

[

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;norenordno&quot;:&quot;20121300065715&quot;,&quot;uid&quot;:&quot;GURURAJ&quot;,

&quot;actid&quot;:&quot;GURURAJ&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;prctyp&quot;:&quot;LMT&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;prd&quot;:&quot;M&quot;,

&quot;flid&quot;:&quot;102&quot;,

&quot;fltm&quot;:&quot;01-01-1980 00:00:00&quot;,

&quot;trantype&quot;:&quot;S&quot;,

&quot;tsym&quot;:&quot;ACCELYA-EQ&quot;,

&quot;qty&quot;:&quot;180&quot;,

&quot;token&quot;:&quot;7053&quot;,

&quot;fillshares&quot;:&quot;180&quot;,

&quot;flqty&quot;:&quot;180&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;0.05&quot;,

&quot;prc&quot;:&quot;800.00&quot;,

&quot;flprc&quot;:&quot;800.00&quot;,

&quot;norentm&quot;:&quot;19:59:32 13-12-2020&quot;,

&quot;exch\_tm&quot;:&quot;00:00:00 01-01-1980&quot;,

&quot;remarks&quot;:&quot;WC TEST Order&quot;,&quot;exchordid&quot;:&quot;6857&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;norenordno&quot;:&quot;20121300065716&quot;,&quot;uid&quot;:&quot;GURURAJ&quot;,

&quot;actid&quot;:&quot;GURURAJ&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;prctyp&quot;:&quot;LMT&quot;,

&quot;ret&quot;:&quot;DAY&quot;,

&quot;prd&quot;:&quot;M&quot;,

&quot;flid&quot;:&quot;101&quot;,

&quot;fltm&quot;:&quot;01-01-1980 00:00:00&quot;,

&quot;trantype&quot;:&quot;B&quot;,

&quot;tsym&quot;:&quot;ACCELYA-EQ&quot;,

&quot;qty&quot;:&quot;180&quot;,

&quot;token&quot;:&quot;7053&quot;,

&quot;fillshares&quot;:&quot;180&quot;,

&quot;flqty&quot;:&quot;180&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;0.05&quot;,

&quot;prc&quot;:&quot;800.00&quot;,

&quot;flprc&quot;:&quot;800.00&quot;,

&quot;norentm&quot;:&quot;19:59:32 13-12-2020&quot;,

&quot;exch\_tm&quot;:&quot;00:00:00 01-01-1980&quot;,

&quot;remarks&quot;:&quot;WC TEST Order&quot;,&quot;exchordid&quot;:&quot;6858&quot;

}

]

## ExchMsg

**public bool SendGetExchMsg**** ( ****OnResponse response**** , ****ExchMsg exchmsg**** )**

##### RequestDetails:ExchMsg

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| uid\* |
 | LoggedinUserId |
| exch |
 | Exchange (Select from &#39;exarr&#39; Array providedin User Detailsresponse) |

##### ResponseDetails:ExchMsgResponselistofExchMsgItem

Responsedatawillbeasbelowfieldsincaseofsuccess.

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Ok | WhiExchMsgsuccessorfailureindication. |
| exchmsg |
 | Itwillbepresentonlyinasuccessfulresponse. |
| exchtm |
 | ExchangeTime |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible** | **Description** |
| --- | --- | --- |

|
 | **value** |
 |
| --- | --- | --- |
| stat | Not\_Ok | Orderbookfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Errormessage |

## OrderMargin

**public bool SendGetOrderMargin**** ( ****OnResponse response**** , ****OrderMargin ordermargin**** )**

##### RequestDetails:OrderMargin

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| actid\* |
 | LoginusersaccountID |
| exch\* | NSE/NFO/BSE/MCX | Exchange (Select from &#39;exarr&#39; Array providedin User Detailsresponse) |
| qty\* |
 | OrderQuantity |
| prc\* |
 | OrderPrice |
| trgprc |
 | OnlytobesentincaseofSL/SL-Morder. |
| prd\* | C/M/H | Product name (Select from &#39;prarr&#39; Arrayprovided in UserDetails response, and if same is allowed for selected,exchange.Showproductdisplayname,forusertoselect,andsendcorrespondingprdinAPIcall) |
| trantype\* | B/S | B-\&gt;BUY,S-\&gt;SELL |
| prctyp\* | LMT/MKT/SL-LMT/SL-MKT/DS/2L/3L |
 |
| orgqty |
 | OrgQuantity |

| orgprc |
 | OrgPrice |
| --- | --- | --- |
| token |
 | Uniqueidofcontractonwhichordertobeplaced. |
| flqty |
 | FillQuantity |
| srcuid |
 | SourceUserID |
| srcbkrid |
 | SourceBrokerID |

##### ResponseDetails:OrderMarginResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Placeordersuccessorfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| remarks | InsufficientBalance/OrderSuccess/Invalid scrip,RED is underReconciliation/SquareoffOrder | AnymessageEnteredduringorderentry. |
| cash | optional | TotalCash |
| marginused | optional | MarginUsed |

## PositionsBook

**public bool SendGetPositionBook**** ( ****OnResponse response**** , ****string account**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| actid\* |
 | Accountidoftheloggedinuser. |

##### ResponseDetails:PositionBookResponselistofPositionBookItem

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Positionbooksuccessorfailureindication. |
| exch |
 | Exchangesegment |
| tsym |
 | Tradingsymbol/contract. |
| token |
 | Contracttoken |
| uid |
 | UserId |
| actid |
 | AccountId |
| prd |
 | Productnametobeshown. |
| netqty |
 | NetPositionquantity |
| netavgprc |
 | Netpositionaverageprice |
| daybuyqty |
 | DayBuyQuantity |
| daysellqty |
 | DaySellQuantity |
| daybuyavgprc |
 | DayBuyaverageprice |
| daysellavgprc |
 | Daybuyaverageprice |
| daybuyamt |
 | DayBuyAmount |
| daysellamt |
 | DaySellAmount |
| cfbuyqty |
 | CarryForwardBuyQuantity |
| cforgavgprc |
 | OriginalAvg Price |
| cfsellqty |
 | CarryForwardSellQuantity |
| cfbuyavgprc |
 | CarryForwardBuyaverageprice |

| cfsellavgprc |
 | CarryForwardBuyaverageprice |
| --- | --- | --- |
| cfbuyamt |
 | CarryForwardBuyAmount |
| cfsellamt |
 | CarryForwardSellAmount |
| lp |
 | LTP |
| rpnl |
 | RealizedPNL |
| urmtom |
 | UnrealizedMTOM.(CanberecalculatedinLTPupdate:=netqty\*(lpfromwebsocket-netavgprc)\*prcftr |
| bep |
 | Breakevenprice |
| openbuyqty |
 |
 |
| opensellqty |
 |
 |
| openbuyamt |
 |
 |
| opensellamt |
 |
 |
| openbuyavgprc |
 |
 |
| opensellavgprc |
 |
 |
| mult |
 |
 |
| pp |
 |
 |
| prcftr |
 | gn\*pn/(gd\*pd). |
| ti |
 | Ticksize |
| ls |
 | Lotsize |
| request\_time |
 | Thiswillbepresentonlyinafailureresponse. |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |

| stat | Not\_Ok | Positionbookrequestfailureindication. |
| --- | --- | --- |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Errormessage |

##### SampleSuccessResponse:

[

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;uid&quot;:&quot;POORNA&quot;,

&quot;actid&quot;:&quot;POORNA&quot;,

&quot;exch&quot;:&quot;NSE&quot;,

&quot;tsym&quot;:&quot;ACC-EQ&quot;,

&quot;prarr&quot;:&quot;C&quot;,

&quot;pp&quot;:&quot;2&quot;,

&quot;ls&quot;:&quot;1&quot;,

&quot;ti&quot;:&quot;5.00&quot;,

&quot;mult&quot;:&quot;1&quot;,

&quot;prcftr&quot;:&quot;1.000000&quot;,

&quot;daybuyqty&quot;:&quot;2&quot;,

&quot;daysellqty&quot;:&quot;2&quot;,

&quot;daybuyamt&quot;:&quot;2610.00&quot;,&quot;daybuyavgprc&quot;:&quot;1305.00&quot;,&quot;daysellamt&quot;:&quot;2610.00&quot;,&quot;daysellavgprc&quot;:&quot;1305.00&quot;,&quot;cfbuyqty&quot;:&quot;0&quot;,

&quot;cfsellqty&quot;:&quot;0&quot;,

&quot;cfbuyamt&quot;:&quot;0.00&quot;,

&quot;cfbuyavgprc&quot;:&quot;0.00&quot;,

&quot;cfsellamt&quot;:&quot;0.00&quot;,

&quot;cfsellavgprc&quot;:&quot;0.00&quot;,

&quot;openbuyqty&quot;:&quot;0&quot;,

&quot;opensellqty&quot;:&quot;23&quot;,

&quot;openbuyamt&quot;:&quot;0.00&quot;,

&quot;openbuyavgprc&quot;:&quot;0.00&quot;,&quot;opensellamt&quot;:&quot;30015.00&quot;,&quot;opensellavgprc&quot;:&quot;1305.00&quot;,&quot;netqty&quot;:&quot;0&quot;,

&quot;netavgprc&quot;:&quot;0.00&quot;,

&quot;lp&quot;:&quot;0.00&quot;,

&quot;urmtom&quot;:&quot;0.00&quot;,

&quot;rpnl&quot;:&quot;0.00&quot;,

&quot;cforgavgprc&quot;:&quot;0.00&quot;

}

]

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,&quot;request\_time&quot;:&quot;14:14:1126-05-2020&quot;,&quot;emsg&quot;:&quot;ErrorOccurred:5\&quot;nodata\&quot;&quot;

}

## ProductConversion

**public bool SendGetOrderMargin**** ( ****OnResponse response**** , ****ProductConversion prdConv**** )**

##### RequestDetails:ProductConversion

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch\* |
 | Exchange |
| tsym\* |
 | Uniqueidofcontractonwhichorderwasplaced.Can&#39;tbemodified, must be the same as that of original order. (useurl encoding to avoid special char error for symbols likeM&amp;M) |
| qty\* |
 | Quantitytobeconverted. |
| uid\* |
 | Useridoftheloggedinuser. |
| actid\* |
 | Accountid |
| prd\* |
 | Producttowhichtheuserwantstoconvertposition. |
| prevprd\* |
 | Originalproductoftheposition. |
| trantype\* |
 | Transactiontype |
| postype\* | Day/CF | ConvertingDayorCarryforwardposition |
| ordersource | MOB | ForLogging |

##### ResponseDetails:ProductConversionResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |

| stat | OkorNot\_Ok | Positionconversionsuccessorfailureindication. |
| --- | --- | --- |
| emsg |
 | ThiswillbepresentonlyifPositionconversionfails. |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;10:52:1202-06-2020&quot;,&quot;stat&quot;:&quot;Ok&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;InvalidInput:InvalidPositionType&quot;

}

# HoldingsandLimits

## Holdings

**public bool SendGetHoldings**** ( ****OnResponse response**** , ****string account**** , ****string product**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| actid\* |
 | Accountidoftheloggedinuser. |
| prd\* |
 | Productname |

##### ResponseDetails:HoldingsResponselistofHoldingsItem

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Holdingrequestsuccessorfailureindication. |
| exch\_tsym |
 | Arrayofobjectsexch\_tsymobjectsasdefinedbelow. |
| holdqty |
 | Holdingquantity |

| colqty |
 | Collateralquantity |
| --- | --- | --- |
| btstqty |
 | BTSTquantity |
| btstcolqty |
 | BTSTCollateralquantity |
| usedqty |
 | Holdingusedtoday |
| upldprc |
 | Averagepriceuploadedalongwithholdings |

Exch\_tsymobject:

| **Fields of object**** in ****values**** Array **|** Possible ****value** | **Description** |
| --- | --- | --- |
| exch | NSE, BSE,NFO... | Exchange |
| tsym |
 | Tradingsymbolofthescrip(contract) |
| token |
 | Tokenofthescrip(contract) |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |

Responsedatawillbeasbelowfieldsincaseoffailure:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Not\_Ok | Positionbookrequestfailureindication. |
| request\_time |
 | Responsereceivedtime. |
| emsg |
 | Errormessage |

##### SampleSuccessResponse:

[

{

&quot;stat&quot;:&quot;Ok&quot;,&quot;exch\_tsym&quot;:[

{

&quot;exch&quot;:&quot;NSE&quot;,

&quot;token&quot;:&quot;13&quot;,

&quot;tsym&quot;:&quot;ABB-EQ&quot;

}

],

&quot;holdqty&quot;:&quot;2000000&quot;,&quot;colqty&quot;:&quot;200&quot;,

&quot;btstqty&quot;:&quot;0&quot;,

&quot;btstcolqty&quot;:&quot;0&quot;,

&quot;usedqty&quot;:&quot;0&quot;,

&quot;upldprc&quot; : &quot;1800.00&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,&quot;exch\_tsym&quot;:[

{

}

],

&quot;exch&quot;:&quot;NSE&quot;,

&quot;token&quot;:&quot;22&quot;,

&quot;tsym&quot;:&quot;ACC-EQ&quot;

&quot;holdqty&quot;:&quot;2000000&quot;,&quot;colqty&quot;:&quot;200&quot;,

&quot;btstqty&quot;:&quot;0&quot;,

&quot;btstcolqty&quot;:&quot;0&quot;,

&quot;usedqty&quot;:&quot;0&quot;,

&quot;upldprc&quot; : &quot;1400.00&quot;

}

]

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Invalid Input : Missing uid or actid or prd.&quot;

}

## Limits

**public bool SendGetLimits**** ( ****OnResponse response**** , ****string account**** , ****string product = &quot;&quot;**** , ****string segment = &quot;&quot;**** , ****string exchange = &quot;&quot;**** )**

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| actid\* |
 | Accountidoftheloggedinuser. |

| prd |
 | Productname |
| --- | --- | --- |
| seg | CM/FO/FX | Segment |
| exch |
 | Exchange |

##### ResponseDetails:LimitsResponse

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Limitsrequestsuccessorfailureindication. |
| actid |
 | Accountid |
| prd |
 | Productname |
| seg | CM/FO/FX | Segment |
| exch |
 | Exchange |
| CashPrimaryFields |
| cash |
 | CashMarginavailable |
| payin |
 | TotalAmounttransferredusingPayinstoday |
| payout |
 | Totalamountrequestedforwithdrawaltoday |
| CashAdditionalFields |
| brkcollamt |
 | PrevaluedCollateralAmount |
| unclearedcash |
 | UnclearedCash(Payinthroughcheques) |
| daycash |
 | Additional leverageamount / Amount added to handlesystemerrors-bybroker. |
| MarginUtilized |
| marginused |
 | Totalmargin/fundusedtoday |
| mtomcurper |
 | Mtomcurrentpercentage |
| MarginUsedcomponents |

| cbu |
 | CACBuyused |
| --- | --- | --- |
| csc |
 | CACSellCredits |
| rpnl |
 | CurrentrealizedPNL |
| unmtom |
 | Currentunrealizedmtom |
| marprt |
 | CoveredProductmargins |
| span |
 | Spanused |
| expo |
 | Exposuremargin |
| premium |
 | Premiumused |
| varelm |
 | VarElmMargin |
| grexpo |
 | GrossExposure |
| greexpo\_d |
 | GrossExposurederivative |
| scripbskmar |
 | Scripbasketmargin |
| addscripbskmrg |
 | Additionalscripbasketmargin |
| brokerage |
 | Brokerageamount |
| collateral |
 | Collateralcalculatedbasedonuploadedholdings |
| grcoll |
 | Valuationofuploadedholdingprehaircut |
| AdditionalRiskLimits |
| turnoverlmt |
 |
 |
| pendordvallmt |
 |
 |
| AdditionalRiskIndicators |
| turnover |
 | Turnover |
| pendordval |
 | PendingOrdervalue |
| Marginuseddetailedbreakupfields |
| rzpnl\_e\_i |
 | CurrentrealizedPNL(EquityIntraday) |
| rzpnl\_e\_m |
 | CurrentrealizedPNL(EquityMargin) |

| rzpnl\_e\_c |
 | CurrentrealizedPNL(EquityCashnCarry) |
| --- | --- | --- |
| rzpnl\_d\_i |
 | CurrentrealizedPNL(DerivativeIntraday) |
| rzpnl\_d\_m |
 | CurrentrealizedPNL(DerivativeMargin) |
| rzpnl\_f\_i |
 | CurrentrealizedPNL(FXIntraday) |
| rzpnl\_f\_m |
 | CurrentrealizedPNL(FXMargin) |
| rzpnl\_c\_i |
 | CurrentrealizedPNL(CommodityIntraday) |
| rzpnl\_c\_m |
 | CurrentrealizedPNL(CommodityMargin) |
| uzpnl\_e\_i |
 | CurrentunrealizedMTOM(EquityIntraday) |
| uzpnl\_e\_m |
 | CurrentunrealizedMTOM(EquityMargin) |
| uzpnl\_e\_c |
 | CurrentunrealizedMTOM(EquityCashnCarry) |
| uzpnl\_d\_i |
 | CurrentunrealizedMTOM(DerivativeIntraday) |
| uzpnl\_d\_m |
 | CurrentunrealizedMTOM(DerivativeMargin) |
| uzpnl\_f\_i |
 | CurrentunrealizedMTOM(FXIntraday) |
| uzpnl\_f\_m |
 | CurrentunrealizedMTOM(FXMargin) |
| uzpnl\_c\_i |
 | CurrentunrealizedMTOM(CommodityIntraday) |
| uzpnl\_c\_m |
 | CurrentunrealizedMTOM(CommodityMargin) |
| span\_d\_i |
 | SpanMargin(DerivativeIntraday) |
| span\_d\_m |
 | SpanMargin(DerivativeMargin) |
| span\_f\_i |
 | SpanMargin(FXIntraday) |
| span\_f\_m |
 | SpanMargin(FXMargin) |
| span\_c\_i |
 | SpanMargin(CommodityIntraday) |
| span\_c\_m |
 | SpanMargin(CommodityMargin) |
| expo\_d\_i |
 | ExposureMargin(DerivativeIntraday) |
| expo\_d\_m |
 | ExposureMargin(DerivativeMargin) |
| expo\_f\_i |
 | ExposureMargin(FXIntraday) |

| expo\_f\_m |
 | ExposureMargin(FXMargin) |
| --- | --- | --- |
| expo\_c\_i |
 | ExposureMargin(CommodityIntraday) |
| expo\_c\_m |
 | ExposureMargin(CommodityMargin) |
| premium\_d\_i |
 | Optionpremium(DerivativeIntraday) |
| premium\_d\_m |
 | Optionpremium(DerivativeMargin) |
| premium\_f\_i |
 | Optionpremium(FXIntraday) |
| premium\_f\_m |
 | Optionpremium(FXMargin) |
| premium\_c\_i |
 | Optionpremium(CommodityIntraday) |
| premium\_c\_m |
 | Optionpremium(CommodityMargin) |
| varelm\_e\_i |
 | VarElm(EquityIntraday) |
| varelm\_e\_m |
 | VarElm(EquityMargin) |
| varelm\_e\_c |
 | VarElm(EquityCashnCarry) |
| marprt\_e\_h |
 | CoveredProductmargins(EquityHighleverage) |
| marprt\_e\_b |
 | CoveredProductmargins(EquityBracketOrder) |
| marprt\_d\_h |
 | CoveredProductmargins(DerivativeHighleverage) |
| marprt\_d\_b |
 | CoveredProductmargins(DerivativeBracketOrder) |
| marprt\_f\_h |
 | CoveredProductmargins(FXHighleverage) |
| marprt\_f\_b |
 | CoveredProductmargins(FXBracketOrder) |
| marprt\_c\_h |
 | CoveredProductmargins(CommodityHighleverage) |
| marprt\_c\_b |
 | CoveredProductmargins(CommodityBracketOrder) |
| scripbskmar\_e\_i |
 | Scripbasketmargin(EquityIntraday) |
| scripbskmar\_e\_m |
 | Scripbasketmargin(EquityMargin) |
| scripbskmar\_e\_c |
 | Scripbasketmargin(EquityCashnCarry) |
| addscripbskmrg\_d\_i |
 | Additionalscripbasketmargin(DerivativeIntraday) |

| addscripbskmrg\_d\_m |
 | Additionalscripbasketmargin(DerivativeMargin) |
| --- | --- | --- |
| addscripbskmrg\_f\_i |
 | Additionalscripbasketmargin(FXIntraday) |
| addscripbskmrg\_f\_m |
 | Additionalscripbasketmargin(FXMargin) |
| addscripbskmrg\_c\_i |
 | Additionalscripbasketmargin(CommodityIntraday) |
| addscripbskmrg\_c\_m |
 | Additionalscripbasketmargin(CommodityMargin) |
| brkage\_e\_i |
 | Brokerage(EquityIntraday) |
| brkage\_e\_m |
 | Brokerage(EquityMargin) |
| brkage\_e\_c |
 | Brokerage(EquityCAC) |
| brkage\_e\_h |
 | Brokerage(EquityHighLeverage) |
| brkage\_e\_b |
 | Brokerage(EquityBracketOrder) |
| brkage\_d\_i |
 | Brokerage(DerivativeIntraday) |
| brkage\_d\_m |
 | Brokerage(DerivativeMargin) |
| brkage\_d\_h |
 | Brokerage(DerivativeHighLeverage) |
| brkage\_d\_b |
 | Brokerage(DerivativeBracketOrder) |
| brkage\_f\_i |
 | Brokerage(FXIntraday) |
| brkage\_f\_m |
 | Brokerage(FXMargin) |
| brkage\_f\_h |
 | Brokerage(FXHighLeverage) |
| brkage\_f\_b |
 | Brokerage(FXBracketOrder) |
| brkage\_c\_i |
 | Brokerage(CommodityIntraday) |
| brkage\_c\_m |
 | Brokerage(CommodityMargin) |
| brkage\_c\_h |
 | Brokerage(CommodityHighLeverage) |
| brkage\_c\_b |
 | Brokerage(CommodityBracketOrder) |

| request\_time |
 | Thiswillbepresentonlyinasuccessfulresponse. |
| --- | --- | --- |
| emsg |
 | Thiswillbepresentonlyinafailureresponse. |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;18:07:3129-05-2020&quot;,&quot;stat&quot;:&quot;Ok&quot;,&quot;cash&quot;:&quot;1500000000000000.00&quot;,

&quot;payin&quot;:&quot;0.00&quot;,

&quot;payout&quot;:&quot;0.00&quot;,

&quot;brkcollamt&quot;:&quot;0.00&quot;,

&quot;unclearedcash&quot;:&quot;0.00&quot;,

&quot;daycash&quot;:&quot;0.00&quot;,&quot;turnoverlmt&quot;:&quot;50000000000000.00&quot;,&quot;pendordvallmt&quot;:&quot;2000000000000000.00&quot;,&quot;turnover&quot;:&quot;3915000.00&quot;,&quot;pendordval&quot;:&quot;2871000.00&quot;,&quot;marginused&quot;:&quot;3945540.00&quot;,&quot;mtomcurper&quot;:&quot;0.00&quot;,

&quot;urmtom&quot;:&quot;30540.00&quot;,

&quot;grexpo&quot;:&quot;3915000.00&quot;,

&quot;uzpnl\_e\_i&quot;:&quot;15270.00&quot;,

&quot;uzpnl\_e\_m&quot;:&quot;61080.00&quot;,

&quot;uzpnl\_e\_c&quot;:&quot;-45810.00&quot;

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,&quot;emsg&quot;:&quot;ServerTimeout:&quot;

}

# MarketInfo

## GetIndexList

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |

| exch\* |
 | Exchange |
| --- | --- | --- |

##### ResponseDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | TopListNamessuccessorfailureindication. |
| values |
 | ArrayOfBasket,Criteriapair. |
| request\_time |
 | Thiswillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors. |

##### Basket,CriteriapairObject:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| idxname |
 | IndexName |
| token |
 | Indextokenusedtosubscribe |

##### SampleOutput:

{

&quot;request\_time&quot;:&quot;20:12:29 13-12-2020&quot;,&quot;values&quot;: [

{

&quot;idxname&quot;:&quot;HangSeng BeES-NAV&quot;,&quot;token&quot;:&quot;26016&quot;

},

{

&quot;idxname&quot;:&quot;India VIX&quot;,&quot;token&quot;:&quot;26017&quot;

},

{

&quot;idxname&quot;:&quot;Nifty 50&quot;,

&quot;token&quot;:&quot;26000&quot;

},

{

&quot;idxname&quot;:&quot;Nifty IT&quot;,&quot;token&quot;:&quot;26008&quot;

},

{

&quot;idxname&quot;:&quot;Nifty Next 50&quot;,&quot;token&quot;:&quot;26013&quot;

},

{

&quot;idxname&quot;:&quot;Nifty Bank&quot;,&quot;token&quot;:&quot;26009&quot;

},

{

&quot;idxname&quot;:&quot;Nifty 500&quot;,

&quot;token&quot;:&quot;26004&quot;

},

{

&quot;idxname&quot;:&quot;Nifty 100&quot;,

&quot;token&quot;:&quot;26012&quot;

},

{

&quot;idxname&quot;:&quot;Nifty Midcap 50&quot;,&quot;token&quot;:&quot;26014&quot;

},

{

&quot;idxname&quot;:&quot;Nifty Realty&quot;,&quot;token&quot;:&quot;26018&quot;

},

]

}

## GetTopListNames

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch\* |
 | Exchange |

##### ResponseDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | TopListNamessuccessorfailureindication. |
| values |
 | ArrayOfBasket,Criteriapair. |
| request\_time |
 | Thiswillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors. |

##### Basket,CriteriapairObject:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| bskt |
 | Basketname |
| crt |
 | criteria |

##### SampleSuccessResponse:

{

&quot;request\_time&quot;:&quot;13:08:2203-06-2020&quot;,&quot;values&quot;:[

{

&quot;bskt&quot;:&quot;NSEBL&quot;,

&quot;crt&quot;:&quot;VOLUME&quot;

},

{

&quot;bskt&quot;:&quot;NSEBL&quot;,

&quot;crt&quot;:&quot;LTP&quot;

},

{

&quot;bskt&quot;:&quot;NSEBL&quot;,

&quot;crt&quot;:&quot;VALUE&quot;

},

{

&quot;bskt&quot;:&quot;NSEEQ&quot;,

&quot;crt&quot;:&quot;VOLUME&quot;

},

{

&quot;bskt&quot;:&quot;NSEEQ&quot;,

&quot;crt&quot;:&quot;LTP&quot;

},

{

&quot;bskt&quot;:&quot;NSEEQ&quot;,

&quot;crt&quot;:&quot;VALUE&quot;

},

{

&quot;bskt&quot;:&quot;NSEALL&quot;,

&quot;crt&quot;:&quot;VOLUME&quot;

},

{

&quot;bskt&quot;:&quot;NSEALL&quot;,

&quot;crt&quot;:&quot;LTP&quot;

},

{

&quot;bskt&quot;:&quot;NSEALL&quot;,

&quot;crt&quot;:&quot;VALUE&quot;

}

]

}

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Session Expired : Invalid Session Key&quot;

}

## GetTopList

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch\* |
 | Exchange |
| tb\* | TorB | ToporBottom |
| bskt\* |
 | Basketname |

| crt\* |
 | criteria |
| --- | --- | --- |

##### ResponseDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | TopListsuccessorfailureindication. |
| values |
 | Arrayoftop/bottomcontractsobject |
| request\_time |
 | Thiswillbepresentonlyinasuccessfulresponse. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors. |

##### top/bottomcontractsobject:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| tsym |
 | Tradingsymbol |
| lp |
 | LTP |
| c |
 | PreviousCloseprice |
| v |
 | volume |
| value |
 | Totaltradedvalue |
| oi |
 | Openinterest |
| pc |
 | LTPpercentagechange |

##### SampleSuccessResponse:

[

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;request\_time&quot;:&quot;15:44:4503-06-2020&quot;,&quot;values&quot;:[

{

&quot;tsym&quot;:&quot;AIRAN-EQ&quot;,

&quot;lp&quot;:&quot;950.00&quot;,

&quot;c&quot;:&quot;915.00&quot;,

&quot;v&quot;:&quot;42705&quot;,

&quot;value&quot;:&quot;40185405.00&quot;,

&quot;oi&quot;:&quot;0&quot;,

&quot;Pc&quot;:&quot;3.83&quot;

},

{

&quot;tsym&quot;:&quot;SHRENIK-EQ&quot;,

&quot;lp&quot;:&quot;1850.00&quot;,

&quot;c&quot;:&quot;1785.00&quot;,

&quot;v&quot;:&quot;206846&quot;,

&quot;value&quot;:&quot;368806418.00&quot;,

&quot;oi&quot;:&quot;0&quot;,

&quot;Pc&quot;:&quot;3.64&quot;

},

{

&quot;tsym&quot;:&quot;REMSONSIND-EQ&quot;,

&quot;lp&quot;:&quot;6000.00&quot;,

&quot;c&quot;:&quot;5795.00&quot;,

&quot;v&quot;:&quot;3948&quot;,

&quot;value&quot;:&quot;22752324.00&quot;,

&quot;Oi&quot;:&quot;0&quot;,

&quot;pc&quot;:&quot;3.54&quot;

},

{

&quot;tsym&quot;:&quot;AXISNIFTY-EQ&quot;,

&quot;lp&quot;:&quot;106700.00&quot;,

&quot;c&quot;:&quot;103301.00&quot;,

&quot;v&quot;:&quot;422&quot;,

&quot;value&quot;:&quot;43825544.00&quot;,

&quot;oi&quot;:&quot;0&quot;,

&quot;Pc&quot;:&quot;3.29&quot;

}

]

}

]

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Invalid Input : Missing uid or exch or bskt or tb or crt&quot;

}

## GetTimePriceData(Chartdata)

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |

| exch\* |
 | Exchange |
| --- | --- | --- |
| token\* |
 |
 |

##### ResponseDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Not\_Ok | TPDatafailureindication. |
| emsg |
 | Thiswillbepresentonlyincaseoferrors. |

Responsedatawillbeasfollowsincaseforsuccess.

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | Ok | TPDatasuccessindication. |
| time |
 | DD/MM/CCYYhh:mm:ss |
| into |
 | Intervalopen |
| inth |
 | Intervalhigh |
| intl |
 | Intervallow |
| intc |
 | Intervalclose |
| intvwap |
 | Intervalvwap |
| intv |
 | Intervalvolume |
| v |
 | volume |
| intoi |
 | Intervaliochange |
| oi |
 | oi |

##### SampleSuccessResponse:

[

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;time&quot;:&quot;02-06-2020 15:46:23&quot;,&quot;into&quot;:&quot;0.00&quot;,

&quot;inth&quot;:&quot;0.00&quot;,

&quot;intl&quot;:&quot;0.00&quot;,

&quot;intc&quot;:&quot;0.00&quot;,

&quot;intvwap&quot;:&quot;0.00&quot;,

&quot;intv&quot;:&quot;0&quot;,

&quot;intoi&quot;:&quot;0&quot;,

&quot;v&quot;:&quot;980515&quot;,

&quot;oi&quot;:&quot;128702&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;time&quot;:&quot;02-06-2020 15:45:23&quot;,&quot;into&quot;:&quot;0.00&quot;,

&quot;inth&quot;:&quot;0.00&quot;,

&quot;intl&quot;:&quot;0.00&quot;,

&quot;intc&quot;:&quot;0.00&quot;,

&quot;intvwap&quot;:&quot;0.00&quot;,

&quot;intv&quot;:&quot;0&quot;,

&quot;intoi&quot;:&quot;0&quot;,

&quot;v&quot;:&quot;980515&quot;,

&quot;oi&quot;:&quot;128702&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;time&quot;:&quot;02-06-2020 15:44:23&quot;,&quot;into&quot;:&quot;0.00&quot;,

&quot;inth&quot;:&quot;0.00&quot;,

&quot;intl&quot;:&quot;0.00&quot;,

&quot;intc&quot;:&quot;0.00&quot;,

&quot;intvwap&quot;:&quot;0.00&quot;,

&quot;intv&quot;:&quot;0&quot;,

&quot;intoi&quot;:&quot;0&quot;,

&quot;v&quot;:&quot;980515&quot;,

&quot;oi&quot;:&quot;128702&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;time&quot;:&quot;02-06-2020 15:43:23&quot;,&quot;into&quot;:&quot;1287.00&quot;,

&quot;inth&quot;:&quot;1287.00&quot;,

&quot;intl&quot;:&quot;0.00&quot;,

&quot;intc&quot;:&quot;1287.00&quot;,

&quot;intvwap&quot;:&quot;128702.00&quot;,

&quot;intv&quot;:&quot;4&quot;,

&quot;intoi&quot;:&quot;128702&quot;,

&quot;v&quot;:&quot;980515&quot;,

&quot;oi&quot;:&quot;128702&quot;

},

{

&quot;stat&quot;:&quot;Ok&quot;,

&quot;time&quot;:&quot;02-06-2020 15:42:23&quot;,&quot;into&quot;:&quot;0.00&quot;,

&quot;inth&quot;:&quot;0.00&quot;,

&quot;intl&quot;:&quot;0.00&quot;,

&quot;intc&quot;:&quot;0.00&quot;,

&quot;intvwap&quot;:&quot;0.00&quot;,

&quot;intv&quot;:&quot;0&quot;,

&quot;intoi&quot;:&quot;0&quot;,

&quot;v&quot;:&quot;980511&quot;,

&quot;oi&quot;:&quot;128702&quot;

}

]

##### SampleFailureResponse:

{

&quot;stat&quot;:&quot;Not\_Ok&quot;,

&quot;emsg&quot;:&quot;Session Expired : Invalid Session Key&quot;

}

## GetOptionChain

##### RequestDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| uid\* |
 | LoggedinUserId |
| tsym\* |
 | Tradingsymbolofanyoftheoptionorfuture.Optionchainfor that underlying will be returned. (use url encoding toavoidspecialcharerrorforsymbolslikeM&amp;M) |
| exch\* |
 | Exchange (UI need to check if exchange in NFO / CDS /MCX/oranyotherexchangewhichhasoptions,ifnotdon&#39;tallow) |
| strprc\* |
 | Midpriceforoptionchainselection |

| cnt\* |
 | Number of strike to return on one side of the mid price forPUTandCALL.(examplecntis4,total16contracts willbereturned,ifcntisis5total20contractwillbereturned) |
| --- | --- | --- |

##### ResponseDetails:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| stat | OkorNot\_Ok | Marketwatchsuccessorfailureindication. |
| values |
 | Arrayofobjects.(objectfieldsgiveninbelowtable) |
| emsg |
 | Thiswillbepresentonlyincaseoferrors.Thatis:1)InvalidInput2)SessionExpired |

| **Fields of object**** in ****values**** Array **|** Possible ****value** | **Description** |
| --- | --- | --- |
| exch | NSE, BSE,NFO... | Exchange |
| tsym |
 | Tradingsymbolofthescrip(contract) |
| token |
 | Tokenofthescrip(contract) |
| optt |
 | OptionType |
| strprc |
 | Strikeprice |
| pp |
 | Priceprecision |
| ti |
 | Ticksize |
| ls |
 | Lotsize |

# OrderUpdatesandMarketDataUpdate

This Api allows you to receive updates receivethe marketdata and order updates in theapplicationcallbacksasanoption,todosoconnectasfollows.

Api.OnFeedCallback += Application.OnFeedHandler;Api.OnOrderCallback;+=Application.OnOrderHandler;

## Connect

_ **public** __**bool**__ **ConnectWatcher** _(_ **string** __**uri,**__ **OnFeed** __**marketdata**__ **Handler,** __**OnOrderFeed**__ **orderHandler)**_

##### Request:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| uri |
 | ServerEndPoint |
| OnFeed |
 | ApplicationCallbackforMarketData |
| OnOrderFeed |
 | CallbackforOrderUpdates |

## SubscribeMarketData

###### publicboolSubscribeToken(stringexch,stringtoken)

##### Request:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch | NSE,BSE,NFO... | Exchange |
| token |
 | ScripToken |

##### MarketDataUpdates:

Acceptfort,e,andtkotherfieldsmay/maynotbepresent.

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| t | tf | &#39;tf&#39;representstouchlinefeed |
| e | NSE,BSE,NFO.. | Exchangename |
| tk | 22 | ScripToken |
| lp |
 | LTP |
| pc |
 | Percentagechange |
| v |
 | volume |
| o |
 | Openprice |
| h |
 | Highprice |
| l |
 | Lowprice |
| c |
 | Closeprice |
| ap |
 | Averagetradeprice |

## UnSubscribeMarketData

###### publicboolUnSubscribeToken(stringexch,stringtoken)

##### Request:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch | NSE,BSE,NFO... | Exchange |
| token |
 | ScripToken |

## SubscribeMarketDataDepth

###### publicboolSubscribeTokenDepth(stringexch,stringtoken)

##### Request:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch | NSE,BSE,NFO... | Exchange |
| token |
 | ScripToken |

##### DepthsubscriptionUpdates:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| t | df | &#39;df&#39;representsdepthfeed |
| e | NSE,BSE,NFO.. | Exchangename |
| tk | 22 | ScripToken |
| lp |
 | LTP |
| pc |
 | Percentagechange |
| v |
 | volume |
| o |
 | Openprice |
| h |
 | Highprice |
| l |
 | Lowprice |
| c |
 | Closeprice |
| ap |
 | Averagetradeprice |
| ltt |
 | Lasttradetime |
| ltq |
 | Lasttradequantity |
| tbq |
 | TotalBuyQuantity |
| tsq |
 | TotalSellQuantity |
| bq1 |
 | BestBuyQuantity1 |
| bq2 |
 | BestBuyQuantity2 |
| bq3 |
 | BestBuyQuantity3 |
| bq4 |
 | BestBuyQuantity4 |

| bq5 |
 | BestBuyQuantity5 |
| --- | --- | --- |
| bp1 |
 | BestBuyPrice1 |
| bp2 |
 | BestBuyPrice2 |
| bp3 |
 | BestBuyPrice3 |
| bp4 |
 | BestBuyPrice4 |
| bp5 |
 | BestBuyPrice5 |
| bo1 |
 | BestBuyOrders1 |
| bo2 |
 | BestBuyOrders2 |
| bo3 |
 | BestBuyOrders3 |
| bo4 |
 | BestBuyOrders4 |
| bo5 |
 | BestBuyOrders5 |
| sq1 |
 | BestSellQuantity1 |
| sq2 |
 | BestSellQuantity2 |
| sq3 |
 | BestSellQuantity3 |
| sq4 |
 | BestSellQuantity4 |
| sq5 |
 | BestSellQuantity5 |
| sp1 |
 | BestSellPrice1 |
| sp2 |
 | BestSellPrice2 |
| sp3 |
 | BestSellPrice3 |
| sp4 |
 | BestSellPrice4 |
| sp5 |
 | BestSellPrice5 |
| so1 |
 | BestSellOrders1 |
| so2 |
 | BestSellOrders2 |
| so3 |
 | BestSellOrders3 |
| so4 |
 | BestSellOrders4 |

| so5 |
 | BestSellOrders5 |
| --- | --- | --- |
| lc |
 | LowerCircuitLimit |
| uc |
 | UpperCircuitLimit |
| 52h |
 | 52weekhighlowinotherexchanges,Lifetimehighlowinmcx |
| 52l |
 | 52weekhighlowinotherexchanges,Lifetimehighlowinmcx |

##### SampleMessage:

{

&quot;t&quot;:&quot;df&quot;,

&quot;e&quot;:&quot;NSE&quot;,

&quot;tk&quot;:&quot;22&quot;,

&quot;o&quot;:&quot;1166.00&quot;,

&quot;h&quot;:&quot;1179.00&quot;,

&quot;l&quot;:&quot;1145.35&quot;,

&quot;c&quot;:&quot;1152.65&quot;,

&quot;ap&quot;:&quot;1159.74&quot;,

&quot;v&quot;:&quot;819881&quot;,

&quot;tbq&quot;:&quot;120952&quot;,

&quot;tsq&quot;:&quot;131730&quot;,

&quot;bp1&quot;:&quot;1156.00&quot;,

&quot;sp1&quot;:&quot;1156.50&quot;,

&quot;bp2&quot;:&quot;1155.80&quot;,

&quot;sp2&quot;:&quot;1156.55&quot;,

&quot;bp3&quot;:&quot;1155.75&quot;,

&quot;sp3&quot;:&quot;1156.65&quot;,

&quot;bp4&quot;:&quot;1155.70&quot;,

&quot;sp4&quot;:&quot;1156.70&quot;,

&quot;bp5&quot;:&quot;1155.65&quot;,

&quot;sp5&quot;:&quot;1156.75&quot;,

&quot;bq1&quot;:&quot;4&quot;,

&quot;sq1&quot;:&quot;10&quot;,

&quot;bq2&quot;:&quot;67&quot;,

&quot;sq2&quot;:&quot;63&quot;,

&quot;bq3&quot;:&quot;83&quot;,

&quot;sq3&quot;:&quot;1&quot;,

&quot;bq4&quot;:&quot;139&quot;,

&quot;sq4&quot;:&quot;53&quot;,

&quot;bq5&quot;:&quot;393&quot;,

&quot;sq5&quot;:&quot;94&quot;

}

## UnsubscribeDepth

###### publicboolUnSubscribeTokenDepth(stringexch,stringtoken)

##### Request:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| exch | NSE,BSE,NFO... | Exchange |
| token |
 | ScripToken |

## SubscribeOrderUpdate

###### publicboolSubscribeOrders(OnOrderFeedorderFeed,stringaccount)

##### Request:

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| actid |
 | Accountidbasedonwhichorderupdatedtobesent. |

##### OrderUpdatesubscriptionUpdates:NorenOrderFeed

| **Fields** | **Possible**** value **|** Description** |
| --- | --- | --- |
| t | om | &#39;om&#39;representstouchlinefeed |
| norenordno |
 | NorenOrderNumber |
| uid |
 | UserId |
| actid |
 | AccountID |
| exch |
 | Exchange |
| tsym |
 | Tradingsymbol |
| qty |
 | Orderquantity |
| prc |
 | OrderPrice |
| prd |
 | Product |
| status |
 | Orderstatus(open,complete,rejectedetc) |
| reporttype |
 | Ordereventforwhichthismessageissentout.(fill,rejected,canceled) |
| trantype |
 | Ordertransactiontype,buyorsell |
| prctyp |
 | Orderpricetype(LMT,MKT,SL-LMT,SL-MKT) |
| ret |
 | Orderretentiontype(DAY,EOS,IOC,...) |
| fillshares |
 | Filledshares |

| avgprc |
 | Averagefillprice |
| --- | --- | --- |
| rejreason |
 | Orderrejectionreason,ifrejected |
| exchordid |
 | ExchangeOrderID |
| cancelqty |
 | Canceledquantity,incaseofcanceledorder |
| remarks |
 | Useraddedtag,whileplacingorder |
| dscqty |
 | Disclosedquantity |
| trgprc |
 | TriggerpriceforSLorders |

2
