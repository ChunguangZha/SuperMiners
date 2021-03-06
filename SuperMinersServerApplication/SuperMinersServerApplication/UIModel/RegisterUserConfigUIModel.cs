﻿using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.UIModel
{
    public class RegisterUserConfigUIModel : BaseUIModel
    {
        public RegisterUserConfigUIModel()
        {
            IsChanged = false;
        }

        public bool IsChanged { get; set; }

        private int _userCountCreateByOneIP = 5;

        /// <summary>
        /// 同一IP地址，可以注册用户数。
        /// </summary>
        public int UserCountCreateByOneIP
        {
            get { return this._userCountCreateByOneIP; }
            set
            {
                if (value != this._userCountCreateByOneIP)
                {
                    this._userCountCreateByOneIP = value;
                    IsChanged = true;
                    NotifyPropertyChanged("UserCountCreateByOneIP");
                }
            }
        }

        /// <summary>
        /// 给新注册用户赠送贡献值
        /// </summary>
        private decimal _giveToNewUserExp = 1;

        /// <summary>
        /// 给新注册用户赠送贡献值
        /// </summary>
        public decimal GiveToNewUserExp
        {
            get { return this._giveToNewUserExp; }
            set
            {
                if (value != this._giveToNewUserExp)
                {
                    this._giveToNewUserExp = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GiveToNewUserExp");
                }
            }
        }

        private decimal _giveToNewUserGoldCoin = 5;

        /// <summary>
        /// 给新注册用户赠送金币数
        /// </summary>
        public decimal GiveToNewUserGoldCoin
        {
            get { return this._giveToNewUserGoldCoin; }
            set
            {
                if (value != this._giveToNewUserGoldCoin)
                {
                    this._giveToNewUserGoldCoin = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GiveToNewUserGoldCoin");
                }
            }
        }

        private decimal _giveToNewUserMines = 0;

        /// <summary>
        /// 给新注册用户赠送矿山数
        /// </summary>
        public decimal GiveToNewUserMines
        {
            get { return this._giveToNewUserMines; }
            set
            {
                if (value != this._giveToNewUserMines)
                {
                    this._giveToNewUserMines = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GiveToNewUserMines");
                }
            }
        }

        private int _giveToNewUserMiners = 0;

        /// <summary>
        /// 给新注册用户赠送矿工数
        /// </summary>
        public int GiveToNewUserMiners
        {
            get { return this._giveToNewUserMiners; }
            set
            {
                if (value != this._giveToNewUserMiners)
                {
                    this._giveToNewUserMiners = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GiveToNewUserMiners");
                }
            }
        }

        private decimal _giveToNewUserStones = 0;

        /// <summary>
        /// 给新注册用户赠送矿石数
        /// </summary>
        public decimal GiveToNewUserStones
        {
            get { return this._giveToNewUserStones; }
            set
            {
                if (value != this._giveToNewUserStones)
                {
                    this._giveToNewUserStones = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GiveToNewUserStones");
                }
            }
        }

        private float _FirstAlipayRechargeGoldCoinAwardMultiple;

        public float FirstAlipayRechargeGoldCoinAwardMultiple
        {
            get { return _FirstAlipayRechargeGoldCoinAwardMultiple; }
            set
            {
                if (value != this._FirstAlipayRechargeGoldCoinAwardMultiple)
                {
                    this._FirstAlipayRechargeGoldCoinAwardMultiple = value;
                    IsChanged = true;
                    NotifyPropertyChanged("FirstAlipayRechargeGoldCoinAwardMultiple");
                }
            }
        }


        public static RegisterUserConfigUIModel CreateFromDBObject(RegisterUserConfig parent)
        {
            if (parent == null)
            {
                return new RegisterUserConfigUIModel();
            }
            RegisterUserConfigUIModel uiConfig = new RegisterUserConfigUIModel()
            {
                GiveToNewUserExp = parent.GiveToNewUserExp,
                GiveToNewUserGoldCoin = parent.GiveToNewUserGoldCoin,
                GiveToNewUserMiners = parent.GiveToNewUserMiners,
                GiveToNewUserMines = parent.GiveToNewUserMines,
                GiveToNewUserStones = parent.GiveToNewUserStones,
                UserCountCreateByOneIP = parent.UserCountCreateByOneIP,
                FirstAlipayRechargeGoldCoinAwardMultiple = parent.FirstAlipayRechargeGoldCoinAwardMultiple
            };

            return uiConfig;
        }

        public RegisterUserConfig ToDBObject()
        {
            RegisterUserConfig uiConfig = new RegisterUserConfig()
            {
                GiveToNewUserExp = this.GiveToNewUserExp,
                GiveToNewUserGoldCoin = this.GiveToNewUserGoldCoin,
                GiveToNewUserMiners = this.GiveToNewUserMiners,
                GiveToNewUserMines = this.GiveToNewUserMines,
                GiveToNewUserStones = this.GiveToNewUserStones,
                UserCountCreateByOneIP = this.UserCountCreateByOneIP,
                FirstAlipayRechargeGoldCoinAwardMultiple = this.FirstAlipayRechargeGoldCoinAwardMultiple
            };

            return uiConfig;
        }
    }
}
