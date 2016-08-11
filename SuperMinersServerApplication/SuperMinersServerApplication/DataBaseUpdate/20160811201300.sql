ALTER TABLE `superminers`.`minesbuyrecord` 
CHANGE COLUMN `Time` `CreateTime` DATETIME NOT NULL ,
ADD COLUMN `PayTime` DATETIME NOT NULL AFTER `CreateTime`;


-- ---------------------------------------------------------------------------------

CREATE TABLE `tempminesbuyrecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` int(10) unsigned NOT NULL,
  `SpendRMB` int(10) unsigned NOT NULL,
  `GainMinesCount` float unsigned NOT NULL,
  `GainStonesReserves` int(10) unsigned NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;




-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`tempminesbuyrecord` 
ADD INDEX `tempminesbuyrecord_UserID__playersimpleinfo_UserID_foreignK_idx` (`UserID` ASC);
ALTER TABLE `superminers`.`tempminesbuyrecord` 
ADD CONSTRAINT `tempminesbuyrecord_UserID__playersimpleinfo_UserID_foreignKey`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`tempminesbuyrecord` 
DROP FOREIGN KEY `tempminesbuyrecord_UserID__playersimpleinfo_UserID_foreignKey`;
ALTER TABLE `superminers`.`tempminesbuyrecord` 
ADD CONSTRAINT `tempminesbuyrecord_UserID__playersimpleinfo_UserID_foreignKey`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`sellstonesorder` 

COMMENT = '保存所有提交过的订单' ;


-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`minesbuyrecord` 
ADD COLUMN `OrderNumber` VARCHAR(35) NOT NULL AFTER `id`,
ADD UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC);


-- ---------------------------------------------------------------------------------

CREATE TABLE `superminers`.`alipayrechargerecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `out_trade_no` VARCHAR(35) NOT NULL,
  `alipay_trade_no` VARCHAR(45) NOT NULL,
  `buyer_email` VARCHAR(35) NOT NULL,
  `total_fee` FLOAT NOT NULL,
  `pay_time` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `out_trade_no_UNIQUE` (`out_trade_no` ASC),
  UNIQUE INDEX `alipay_trade_no_UNIQUE` (`alipay_trade_no` ASC),
  UNIQUE INDEX `buyer_email_UNIQUE` (`buyer_email` ASC));


-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`goldcoinrechargerecord` 
CHANGE COLUMN `Time` `CreateTime` DATETIME NOT NULL ,
ADD COLUMN `OrderNumber` VARCHAR(35) NOT NULL AFTER `id`,
ADD COLUMN `PayTime` DATETIME NOT NULL AFTER `CreateTime`,
ADD UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC);


/*---------------------------------------------------------------------------------*/

ALTER TABLE `superminers`.`goldcoinrechargerecord` 
DROP FOREIGN KEY `userinfo_id_goldcoinrechargerecord_userid`;
ALTER TABLE `superminers`.`goldcoinrechargerecord` 
DROP INDEX `UserID_UNIQUE` ,
DROP INDEX `id_UNIQUE` , 
COMMENT = 'userinfo_id_goldcoinrechargerecord_userid' ;


-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`goldcoinrechargerecord` 
ADD INDEX `userinfo_id_goldcoinrechargerecord_userid_idx` (`UserID` ASC), 
COMMENT =  '';
ALTER TABLE `superminers`.`goldcoinrechargerecord` 
ADD CONSTRAINT `userinfo_id_goldcoinrechargerecord_userid`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;

-- ---------------------------------------------------------------------------------

CREATE TABLE `tempgoldcoinrechargerecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(35) NOT NULL,
  `UserID` int(11) unsigned NOT NULL,
  `RechargeMoney` float unsigned NOT NULL,
  `GainGoldCoin` float unsigned NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `OrderNumber_UNIQUE` (`OrderNumber`),
  
  -- KEY `userinfo_id_goldcoinrechargerecord_userid_idx` (`UserID`),
  CONSTRAINT `userinfo_id_tempgoldcoinrechargerecord_userid` FOREIGN KEY (`UserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`tempgoldcoinrechargerecord` 
CHANGE COLUMN `RechargeMoney` `SpendRMB` FLOAT UNSIGNED NOT NULL ;


-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`goldcoinrechargerecord` 
CHANGE COLUMN `RechargeMoney` `SpendRMB` FLOAT UNSIGNED NOT NULL ;


-- ---------------------------------------------------------------------------------






