using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorenRestApiWrapper;
using System.Threading;

namespace dotNetExample
{
    public class BaseResponseHandler
    {
        public AutoResetEvent ResponseEvent = new AutoResetEvent(false);

        public NorenResponseMsg baseResponse;

        public void OnResponse(NorenResponseMsg Response, bool ok)
        {
            baseResponse = Response;

            ResponseEvent.Set();
        }
    }
    class Program
    {
        #region dev  credentials

        public const string endPoint = "https://starapiuat.prostocks.com/NorenWClientTP/";
        public const string wsendpoint = "wss://starapiuat.prostocks.com/NorenWS/";
        public const string uid = "P1007";
        public const string actid = "P1007";
        public const string pwd = "Zxc@1234";
        public const string factor2 = dob;
        public const string pan = "";
        public const string dob = "10122001";
        public const string imei = "abc1234";
        public const string vc = "P1007_U";


        public const string appkey = "pssUATAPI26102021HJKL09IL2";
        public const string newpwd = "";
        #endregion 

        public static NorenRestApi nApi = new NorenRestApi();
        public static bool loggedin = false;


        public static void OnStreamConnect(NorenStreamMessage msg)
        {
            Program.loggedin = true;
            Console.WriteLine("feed handler : connected");

        }
        static void Main(string[] args)
        {
            LoginMessage loginMessage = new LoginMessage();
            loginMessage.apkversion = "1.0.0";
            loginMessage.uid = uid;
            loginMessage.pwd = pwd;
            loginMessage.factor2 = factor2;
            loginMessage.imei = imei;
            loginMessage.vc = vc;
            loginMessage.source = "API";
            loginMessage.appkey = appkey;
            BaseResponseHandler responseHandler = new BaseResponseHandler();

            nApi.SendLogin(responseHandler.OnResponse, endPoint, loginMessage);

            responseHandler.ResponseEvent.WaitOne();

            LoginResponse loginResponse = responseHandler.baseResponse as LoginResponse;
            Console.WriteLine("app handler :" + responseHandler.baseResponse.toJson());

            nApi.onStreamConnectCallback = Program.OnStreamConnect;
            //only after login success connect to websocket for market/order updates
            if (nApi.ConnectWatcher(wsendpoint, Program.OnFeed, null))
            { 
                //wait for connection
                Thread.Sleep(2000);
                //send subscription for reliance
                nApi.SubscribeToken("NSE", "2885");
            }

            Console.ReadLine();
        }

        public static void OnFeed(NorenFeed Feed)
        {
            NorenFeed feedmsg = Feed as NorenFeed;
            Console.WriteLine(Feed.toJson());
            if (feedmsg.t == "dk")
            {
                //acknowledgment
            }
            if (feedmsg.t == "df")
            {
                //feed
                Console.WriteLine($"Feed received: {Feed.toJson()}");
            }
        }
    }
}
