﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using SuperMinersWeb.AlipayCode;
using SuperMinersWeb.Wcf;
using MetaData;
using SuperMinersWeb.DataBaseCode;

/// <summary>
/// 功能：页面跳转同步通知页面
/// 版本：3.3
/// 日期：2012-07-10
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// ///////////////////////页面功能说明///////////////////////
/// 该页面可在本机电脑测试
/// 可放入HTML等美化页面的代码、商户业务逻辑程序代码
/// 该页面可以使用ASP.NET开发工具调试，也可以使用写文本函数LogResult进行调试
/// </summary>
public partial class return_url : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();

                bool verifyResult;
#if TestAlipay

            verifyResult = true;

#else

                verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"], " Return ");

#endif

                string userName = Request.QueryString["extra_common_param"];

                SuperMinersWeb.AlipayCode.Core.LogResult(userName, DateTime.Now.ToString() + " ------ Return End Pay 1.  verifyResult：" + verifyResult);


                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.QueryString["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request.QueryString["trade_no"];

                    //交易状态
                    string trade_status = Request.QueryString["trade_status"];


                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        DateTime timeNow = DateTime.Now;
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        string buyer_email = Request.QueryString["buyer_email"];
                        decimal total_fee;
                        if (!decimal.TryParse(Request.QueryString["total_fee"], out total_fee))
                        {
                            SuperMinersWeb.AlipayCode.Core.LogResult(userName, DateTime.Now.ToString() + " ------ Return End Pay 2 Failed, 充值金额错误.  userName：" + userName + "; out_trade_no=" + out_trade_no + ";trade_status=" + trade_status + ";total_fee=" + total_fee);

                            //打印页面
                            Response.Write("充值金额错误<br />");
                            return;
                        }
                        int result = WcfClient.Instance.CheckAlipayOrderBeHandled(userName, out_trade_no, trade_no, total_fee, buyer_email, timeNow.ToString());

                        SuperMinersWeb.AlipayCode.Core.LogResult(userName, DateTime.Now.ToString() + " ------ Return End Pay 2.1.  CheckAlipayOrderBeHandled：" + result);
                        if (result == OperResult.RESULTCODE_TRUE)
                        {
                            //表示该订单已经被处理过
                            //打印页面
                            Response.Write("操作成功<br />本页面将在3秒后关闭");
                            Response.Write("<script>setTimeout(' window.opener = null;window.close();',3000);</script>");

                            return;
                        }

                        result = WcfClient.Instance.AlipayCallback(userName, out_trade_no, trade_no, total_fee, buyer_email, timeNow.ToString());
                        if (result == OperResult.RESULTCODE_EXCEPTION)
                        {
                            result = WcfClient.Instance.AlipayCallback(userName, out_trade_no, trade_no, total_fee, buyer_email, timeNow.ToString());
                        }
                        SuperMinersWeb.AlipayCode.Core.LogResult(userName, DateTime.Now.ToString() + " ------ Return End Pay 3 Result: " + result + ".  userName：" + userName + "; out_trade_no=" + out_trade_no + ";trade_no=" + trade_no + ";trade_status=" + trade_status + ";total_fee=" + total_fee);

                        if (result != OperResult.RESULTCODE_TRUE)
                        {
                            DBOper.AddExceptionAlipayRechargeRecord(userName, out_trade_no, trade_no, total_fee, buyer_email, timeNow.ToString());
                            Response.Write("支付成功，但是服务器操作失败，请联系客服，并将以下信息发送给客服。\r\n商品订单号：" + out_trade_no + "，支付宝订单号：" + trade_no + "，付款账户：" + buyer_email);
                            return;
                        }
                    }
                    else
                    {
                        SuperMinersWeb.AlipayCode.Core.LogResult(userName, DateTime.Now.ToString() + " ------ Return End Pay 4 Failed.  userName：" + userName + "; out_trade_no=" + out_trade_no + ";trade_status=" + trade_status);

                        Response.Write("trade_status=" + trade_status);
                    }

                    //打印页面
                    Response.Write("操作成功<br />本页面将在3秒后关闭");
                    Response.Write("<script>setTimeout(' window.opener = null;window.close();',3000);</script>");

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("支付失败");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
        }
        catch (Exception exc)
        {
            SuperMinersWeb.AlipayCode.Core.LogResult("", DateTime.Now.ToString() + " ------ Return Exception: " + exc.Message + ". \r\n" + exc.Source);

        }
    }

    /// <summary>
    /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestGet()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.QueryString;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
        }

        return sArray;
    }
}