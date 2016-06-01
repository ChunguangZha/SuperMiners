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
        /// <summary>
        /// 0：成功；1：用户名已经存在；2：同一IP注册用户数超限；3：注册失败; 4: 用户名长度不够
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        [OperationContract]
        int RegisterUser(string clientIP, string userName, string nickName, string password, string email, string qq, string invitationCode);

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckUserNameExist(string userName);

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckUserAlipayExist(string alipayAccount, string alipayRealName);

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckEmailExist(string email);

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示可以注册，1表示已经超出限制，不可以注册
        /// </summary>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckRegisterIP(string clientIP);
    }
}
