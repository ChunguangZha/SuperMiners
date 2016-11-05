using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToWeb.Contracts
{
    [ServiceContract]
    public interface IServiceToWeb
    {
        [OperationContract]
        bool Active();

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_SUCCEED; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        [OperationContract]
        int RegisterUser(string clientIP, string userName, string nickName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode);

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_SUCCEED; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="agentUserName"></param>
        /// <returns></returns>
        [OperationContract]
        int RegisterUserByAgent(string clientIP, string userName, string nickName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string agentUserName);

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_SUCCEED; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckUserNameExist(string userName);

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_SUCCEED; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckNickNameExist(string nickName);

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_SUCCEED; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckUserAlipayAccountExist(string alipayAccount);

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_SUCCEED; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckUserAlipayRealNameExist(string alipayRealName);

        [OperationContract]
        int CheckUserIDCardNoExist(string IDCardNo);

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_SUCCEED; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckEmailExist(string email);

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_SUCCEED; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckRegisterIP(string clientIP);

        [OperationContract]
        GameConfig GetGameConfig();

        [OperationContract]
        int AlipayCallback(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time);

        [OperationContract]
        int CheckAlipayOrderBeHandled(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time);
    }
}
