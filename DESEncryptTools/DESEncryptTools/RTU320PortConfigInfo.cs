using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESEncryptTools
{
    public class RTU320PortConfigInfo
    {
        public bool PortEnable = true;

        public string PortName = "Port1";

        public RTU320PortType PortType = RTU320PortType.Cu1;

        /// <summary>
        /// Cu 1/2 can be configured as 10/100/1000BaseT. SFP+ 1/2 can be configured as 100/1000BaseX or 10GE 
        /// </summary>
        public RTU320PortSelection PortSelection = RTU320PortSelection.e10_100_1000BaseT;

        #region 以下三项分别对应 PortSelection 的三种类型，取值时按类型三取一。

        public RTU320PortConfig_BaseT PortBaseT = new RTU320PortConfig_BaseT();

        public RTU320PortConfig_BaseX PortBaseX = new RTU320PortConfig_BaseX();

        public RTU320PortConfig_10GE Port10GE = new RTU320PortConfig_10GE();

        #endregion

        public RTU320PortConfig_IPMac PortIPMac = new RTU320PortConfig_IPMac();

    }

    public class RTU320PortConfig_IPMac
    {
        public RTU320Port_MacType MacType = RTU320Port_MacType.Default;

        public string Mac = "";

        public RTU320Port_IPType Mode = RTU320Port_IPType.IPv4;

        //IPv4时，取值为（DHCP或Static）；IPv6时，取值为（Auto或Static）
        public RTU320Port_IPAddressType IPAddressType = RTU320Port_IPAddressType.Static;

        #region 以下三值对应IPAddressType类型分别为 Static, DHCP, Auto，其中IPv4的Static和IPv6的Static共同一个类型。按类型三取一。
        public RTU320PortConfig_IPMac_Static IPAddress_Static = new RTU320PortConfig_IPMac_Static();

        public RTU320PortConfig_IPMac_IPv4DHCP IPAddress_DHCP = new RTU320PortConfig_IPMac_IPv4DHCP();

        public RTU320PortConfig_IPMac_IPv6Auto IPAddress_Auto = new RTU320PortConfig_IPMac_IPv6Auto();

        #endregion

        public bool VLANTag1Enable = false;
        /// <summary>
        /// 最小值为0，最大值为4095
        /// </summary>
        public int VLANID1 = 244;
        /// <summary>
        /// 最小值为0，最大值为7
        /// </summary>
        public int Priority1 = 0;


        public bool VLANTag2Enable = false;
        /// <summary>
        /// 最小值为0，最大值为4095
        /// </summary>
        public int VLANID2 = 244;
        /// <summary>
        /// 最小值为0，最大值为7
        /// </summary>
        public int Priority2 = 0;

    }

    public class RTU320PortConfig_IPMac_IPv6Auto
    {
        public bool PrimaryDNSEnable = false;

        public string PrimaryDNS = "";

        public bool SecondaryDNSEnable = false;

        public string SecondaryDNS = "";

    }

    public class RTU320PortConfig_IPMac_IPv4DHCP
    {
        public RTU320Port_DHCPModeType DHCPMode = RTU320Port_DHCPModeType.Broadcast;

        public RTU320Port_EnableType DHCPRenewal = RTU320Port_EnableType.Disable;

        public RTU320Port_EnableType GatewayandDNS = RTU320Port_EnableType.Enable;

    }

    public class RTU320PortConfig_IPMac_Static
    {
        public bool GatewayEnable = false;

        public string Gateway = "";

        public bool PrimaryDNSEnable = false;

        public string PrimaryDNS = "";

        public bool SecondaryDNSEnable = false;

        public string SecondaryDNS = "";

    }

    public class RTU320PortConfig_BaseT
    {
        public bool AutoNegotiation = false;

        /// <summary>
        /// AutoNegotiation=true时，该值有效。0表示Default-All；1表示Custom
        /// </summary>
        public int AdvertisementType = 0;

        /// <summary>
        /// AutoNegotiation=true时，该值有效。当AdvertisementType=Default-All时，该值为全部项；AdvertisementType=Custom时，该值为用户自行设置（至少要选择一项）。
        /// </summary>
        public RTU320Port_AdvertisementItem[] AutoNeg_On_Advertisement_Item = new RTU320Port_AdvertisementItem[]{
            RTU320Port_AdvertisementItem.e10M_Half,
            RTU320Port_AdvertisementItem.e100M_Half,
            RTU320Port_AdvertisementItem.e10M_Full,
            RTU320Port_AdvertisementItem.e100M_Full,
            RTU320Port_AdvertisementItem.e1000M_Full
        };

        /// <summary>
        /// 目前值不允许设置为1000M
        /// </summary>
        public RTU320Port_AdvertisementItem AutoNeg_Off_Advertisement_Item = RTU320Port_AdvertisementItem.e100M_Full;

        public RTU320Port_FlowControlType FlowControl = RTU320Port_FlowControlType.BothOn;

        public RTU320Port_MDIXType MDIX = RTU320Port_MDIXType.Auto;

    }

    public class RTU320PortConfig_BaseX
    {
        public bool AutoNegotiation = false;

        /// <summary>
        /// 100或者1000
        /// </summary>
        public int Speed = 1000;

        /// <summary>
        /// 只有AutoNegotiation=Off时，该值有效。
        /// </summary>
        public bool TransmitIgnoreLinkStatus = true;

        /// <summary>
        /// 取值范围在-150到+150之间
        /// </summary>
        public int ClockOffset = 0;

        public bool LaserOn = true;

    }

    public class RTU320PortConfig_10GE
    {
        public bool FlowControl = true;

        public bool TransmitIgnoreLinkStatus = true;

        public bool LinkFaultResponse = true;

        /// <summary>
        /// 取值范围在-150到+150之间
        /// </summary>
        public int ClockOffset = 0;

        public bool LaserOn = true;

    }

    public enum RTU320PortType
    {
        //SFP+ 1/2 can be configured as 100/1000BaseX or 10GE 
        SFP1,
        SFP2,

        //Cu 1/2 can be configured as 10/100/1000BaseT
        Cu1,
        Cu2
    }

    public enum RTU320PortSelection
    {
        e100_1000BaseX,
        e10GE,
        e10_100_1000BaseT
    }

    public enum RTU320Port_AdvertisementItem
    {
        e10M_Half,
        e100M_Half,
        e10M_Full,
        e100M_Full,
        e1000M_Full
    }

    public enum RTU320Port_FlowControlType
    {
        TXOn,
        RXOn,
        BothOn,
        Off
    }

    public enum RTU320Port_MDIXType
    {
        Off,
        On,
        Auto
    }

    public enum RTU320Port_IPType
    {
        IPv4,
        IPv6
    }

    public enum RTU320Port_MacType
    {
        Default,
        Manual
    }

    public enum RTU320Port_IPAddressType
    {
        Auto,
        DHCP,
        Static,
    }

    public enum RTU320Port_DHCPModeType
    {
        Broadcast,
        Unicast
    }

    public enum RTU320Port_EnableType
    {
        Enable,
        Disable
    }
}
