CREATE TABLE `stonedelegatebuyordercanceledinfo` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(35) NOT NULL,
  `UserID` int(10) unsigned NOT NULL,
  `Price` float NOT NULL,
  `TradeStoneHandCount` int(11) NOT NULL,
  `PayType` int(11) NOT NULL,
  `FinishedStoneTradeHandCount` int(11) NOT NULL DEFAULT '0',
  `BuyState` int(11) NOT NULL,
  `DelegateTime` datetime NOT NULL,
  `IsSubOrder` int(1) NOT NULL DEFAULT '0',
  `ParentOrderNumber` varchar(35) DEFAULT NULL,
  `FinishedTime` datetime DEFAULT NULL,
  `AwardGoldCoin` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id`),
  UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber`),
  INDEX `stonedelegatebuyordercanceledinfo_FK_userID_idx` (`UserID`),
  CONSTRAINT `stonedelegatebuyordercanceledinfo_FK_userID` FOREIGN KEY (`UserID`) 
  REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- -----------------------------

CREATE TABLE `superminers`.`stonedelegatesellordercanceledinfo` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `OrderNumber` VARCHAR(35) NOT NULL,
  `UserID` INT UNSIGNED NOT NULL,
  `Price` FLOAT NOT NULL,
  `TradeStoneHandCount` INT NOT NULL,
  `FinishedStoneTradeHandCount` INT NOT NULL DEFAULT 0,
  `SellState` INT NOT NULL,
  `DelegateTime` DATETIME NOT NULL,
  `IsSubOrder` INT(1) NOT NULL DEFAULT 0,
  `ParentOrderNumber` VARCHAR(35) NULL,
  `FinishedTime` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC),
  INDEX `stonedelegatesellordercanceledinfo_FK_userID_idx` (`UserID` ASC),
  CONSTRAINT `stonedelegatesellordercanceledinfo_FK_userID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- --------------------------------


ALTER TABLE `superminers`.`playerfortuneinfo` 
ADD COLUMN `ShoppingCreditsEnabled` INT NOT NULL DEFAULT 0 AFTER `FirstRechargeGoldCoinAward`,
ADD COLUMN `ShoppingCreditsFreezed` INT NOT NULL DEFAULT 0 AFTER `ShoppingCreditsEnabled`;

-- --------------------------------

ALTER TABLE `superminers`.`deletedplayerinfo` 
ADD COLUMN `ShoppingCreditsEnabled` INT NOT NULL DEFAULT 0 AFTER `FirstRechargeGoldCoinAward`,
ADD COLUMN `ShoppingCreditsFreezed` INT NOT NULL DEFAULT 0 AFTER `ShoppingCreditsEnabled`;


-- --------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170214113000' WHERE `id`='1';


