using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XunLinMineRemoteControlWeb.AlipayCode
{
    public class Config
    {
        //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        // 合作身份者ID，签约账号，以2088开头由16位纯数字组成的字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        public static string partner = "2088221640407534";

        // 收款支付宝账号，以2088开头由16位纯数字组成的字符串，一般情况下收款账号就是签约账号
        public static string seller_id = partner;

        //商户的私钥,原始格式，RSA公私钥生成：https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.nBDxfy&treeId=58&articleId=103242&docType=1
        public static string private_key = "MIICXQIBAAKBgQDOhs7cXWx7oLBU3mVF8PIELBzsBE0iRkrh1tmOvKhc/2Z8IBX2" +
"VfDRKbLgSS2p7dmglWwqKl4CIel9CB5Bj83E1R1z8ldxpV/9RU1G52JEq+I9xNJA" +
"k9XQVdD4ZnzrUvaBwA8yjWFJ381aWQCOD+ep5qWirH3lr2lFhGcAOXv/3QIDAQAB" +
"AoGAAXPnTpuFoNT/iIlL8xL/0NGynmJlXIFcE3ycaSmgkY7mXKcpIGN4XzBI5tT8" +
"8j4PEtcaPK2xnZg5eTyuYc2uJ9oPZUA2Kib+ZLEz+UaaqOReYGC3zb+4l94oDGoL" +
"YRH1Yg2jjAXo/KV3wsl+T2Ud83ytJxgAIyvD//ShSJUXmWECQQDtJ0ptpVzV7BKR" +
"poneVhgALyMBStRzDcU74GNvs+owh+rUIMUGFNRf1fXmtNjhbmjypGsLLYWlRfB+" +
"1umURx85AkEA3vBvtMb7LY2ivCqjK4NKpFTXhK161INppNCcr9HTaUK2j/icefcv" +
"1UI34U39sMfF4VL557XnCO67Z7iCKu/BxQJBAM0NfgNUSfMGDgA1+VtvIY13NFM3" +
"un5I19Mv74p0C/gubVNBiln5xK+gtt/mWuiAqOm0VIHzuGzxDkL93DVDUbECQQCX" +
"EGUhIhPhci/rQRj/yx8w6yx+gu7QQZu8Sn9hw9R1Zvc950BghNasswoaaTaWK0gy" +
"vn8IO4Ip01q5lZTec0fhAkBVmCcDWjThqTcB3wMek30WFQJAEEkxDJUWkKYMUkKh" +
"lqE18LnAVnJyKjvE0fC96bICsRD3RkXc40DqYbcsC0sv";

        //支付宝的公钥，查看地址：https://b.alipay.com/order/pidAndKey.htm 
        public static string alipay_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";

        // 服务器异步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数,必须外网可以正常访问
        public static string notify_url = "http://www.xlore.net/Alipay/notify_url.aspx";

        // 页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        public static string return_url = "http://www.xlore.net/Alipay/return_url.aspx";

        // 签名方式
        public static string sign_type = "RSA";

        // 调试用，创建TXT日志文件夹路径，见AlipayCore.cs类中的LogResult(string sWord)打印方法。
        public static string log_path = HttpRuntime.AppDomainAppPath.ToString() + "Logs";

        // 字符编码格式 目前支持 gbk 或 utf-8
        public static string input_charset = "utf-8";

        // 支付类型 ，无需修改
        public static string payment_type = "1";

        // 调用的接口名，无需修改
        public static string service = "create_direct_pay_by_user";

        //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑


        //↓↓↓↓↓↓↓↓↓↓请在这里配置防钓鱼信息，如果没开通防钓鱼功能，请忽视不要填写 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        //防钓鱼时间戳  若要使用请调用类文件submit中的Query_timestamp函数
        public static string anti_phishing_key = "";

        //客户端的IP地址 非局域网的外网IP地址，如：221.0.0.1
        public static string exter_invoke_ip = "";

        //↑↑↑↑↑↑↑↑↑↑请在这里配置防钓鱼信息，如果没开通防钓鱼功能，请忽视不要填写 ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    }
}