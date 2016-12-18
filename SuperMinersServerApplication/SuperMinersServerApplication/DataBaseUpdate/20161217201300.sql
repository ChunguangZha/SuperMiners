ALTER TABLE `superminers`.`alipayrechargerecord` 
ADD COLUMN `trade_type` INT NULL AFTER `out_trade_no`;


-- ----------------------------------------


update superminers.alipayrechargerecord set trade_type = substring(out_trade_no, 19,2);




-- -------------------------------------------

ALTER TABLE `superminers`.`alipayrechargerecord` 
CHANGE COLUMN `trade_type` `trade_type` INT(11) NOT NULL ;


-- --------------------------------------------

CREATE TABLE `deletedplayerinfo` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserName` varchar(64) NOT NULL,
  `NickName` varchar(128) DEFAULT NULL,
  `Password` varchar(30) NOT NULL,
  `GroupType` int(11) NOT NULL DEFAULT '0',
  `IsAgentReferred` int(1) NOT NULL DEFAULT '0',
  `AgentReferredLevel` int(11) NOT NULL DEFAULT '0',
  `AgentUserID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'only agent referred player, this field valid, default value is 0 mean invalid.',
  `Alipay` varchar(128) DEFAULT NULL,
  `AlipayRealName` varchar(50) DEFAULT NULL,
  `IDCardNo` varchar(18) DEFAULT NULL,
  `Email` varchar(128) DEFAULT NULL,
  `QQ` varchar(128) DEFAULT NULL,
  `RegisterIP` varchar(15) NOT NULL COMMENT '�û�ע��ʱʹ�õ�IP��ַ',
  `InvitationCode` varchar(100) NOT NULL DEFAULT '',
  `RegisterTime` datetime NOT NULL,
  `LastLoginTime` datetime DEFAULT NULL,
  `LastLogOutTime` datetime DEFAULT NULL,
  `ReferrerUserID` int(10) unsigned DEFAULT NULL,
  `LastLoginIP` varchar(25) DEFAULT NULL,
  `LastLoginMac` varchar(45) DEFAULT NULL,
  
  `Exp` float NOT NULL DEFAULT '0',
  `CreditValue` bigint(20) unsigned NOT NULL DEFAULT '0',
  `RMB` float NOT NULL DEFAULT '0',
  `FreezingRMB` float unsigned NOT NULL DEFAULT '0',
  `GoldCoin` float NOT NULL DEFAULT '0',
  `MinesCount` float NOT NULL DEFAULT '0',
  `StonesReserves` float NOT NULL DEFAULT '0' COMMENT '��ʯ��������ֵΪ�ۼ�ֵ����ÿ�ι����ɽ������ɽ����ֵ�ۼӵ���ֵ�ϡ�',
  `TotalProducedStonesCount` float NOT NULL DEFAULT '0' COMMENT '�ۼ��ܲ�����ʯ������StonesReserves�Ĳ�ֵ��Ϊ��ǰ�ɿ��ɵĿ�ʯ����',
  `MinersCount` float NOT NULL DEFAULT '0' COMMENT '����',
  `StockOfStones` float NOT NULL DEFAULT '0',
  `TempOutputStonesStartTime` datetime DEFAULT NULL,
  `TempOutputStones` float NOT NULL DEFAULT '0' COMMENT '��ʱ������ʯ�������ݿ��ֶΣ���¼��ҵ�ǰ���������Ŀ�ʯ������ҵ����ȡ��ʱ����÷����Ѳ���ֵ���浽���ݿ��С�',
  `FreezingStones` float unsigned NOT NULL DEFAULT '0',
  `StockOfDiamonds` float NOT NULL DEFAULT '0',
  `FreezingDiamonds` float NOT NULL DEFAULT '0',
  `StoneSellQuan` int(11) NOT NULL DEFAULT '0',
  `FirstRechargeGoldCoinAward` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`),
  UNIQUE KEY `Alipay_UNIQUE` (`Alipay`),
  UNIQUE KEY `IDCardNo_UNIQUE` (`IDCardNo`)
) ENGINE=InnoDB AUTO_INCREMENT=5408 DEFAULT CHARSET=utf8;






ALTER TABLE `superminers`.`deletedplayerinfo` 
CHANGE COLUMN `ReferrerUserID` `ReferrerUserName` VARCHAR(64) NULL ;


ALTER TABLE `superminers`.`deletedplayerinfo` 
ADD COLUMN `DeleteTime` DATETIME NOT NULL AFTER `FirstRechargeGoldCoinAward`;


-- -------------------------------------

ALTER TABLE `superminers`.`gameconfig` 
ADD COLUMN `MineReservesIsRandom` INT(1) NOT NULL DEFAULT 0 AFTER `TempStoneOutputValidHour`,
ADD COLUMN `MinStonesReservesPerMine` INT NOT NULL DEFAULT 50000 AFTER `StonesReservesPerMines`,
ADD COLUMN `MaxStonesReservesPerMine` INT NOT NULL DEFAULT 100000 AFTER `MinStonesReservesPerMine`;


-- --------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161217201300' WHERE `id`='1';



