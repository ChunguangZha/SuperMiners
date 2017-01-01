CREATE TABLE `superminers`.`notfinishedstonedelegatesellorderinfo` (
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
  INDEX `NotFinishedStoneDelegateSellOrderInfo_FK_userID_idx` (`UserID` ASC),
  CONSTRAINT `NotFinishedStoneDelegateSellOrderInfo_FK_userID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);



-- ------------------------------------------------------------------------------

CREATE TABLE `superminers`.`finishedstonedelegatesellorderinfo` (
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
  INDEX `FinishedStoneDelegateSellOrderInfo_FK_userID_idx` (`UserID` ASC),
  CONSTRAINT `FinishedStoneDelegateSellOrderInfo_FK_userID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);



-- -----------------------------------------------------------------------------

CREATE TABLE `superminers`.`notfinishedstonedelegatebuyorderinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `OrderNumber` VARCHAR(35) NOT NULL,
  `UserID` INT UNSIGNED NOT NULL,
  `Price` FLOAT NOT NULL,
  `TradeStoneHandCount` INT NOT NULL,
  `PayType` INT NOT NULL,
  `FinishedStoneTradeHandCount` INT NOT NULL DEFAULT 0,
  `BuyState` INT NOT NULL,
  `DelegateTime` DATETIME NOT NULL,
  `IsSubOrder` INT(1) NOT NULL DEFAULT 0,
  `ParentOrderNumber` VARCHAR(35) NULL,
  `FinishedTime` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC),
  INDEX `NotFinishedStoneDelegateBuyOrderInfo_FK_userID_idx` (`UserID` ASC),
  CONSTRAINT `NotFinishedStoneDelegateBuyOrderInfo_FK_userID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);



-- -------------------------------------------------------------------------------


CREATE TABLE `superminers`.`finishedstonedelegatebuyorderinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `OrderNumber` VARCHAR(35) NOT NULL,
  `UserID` INT UNSIGNED NOT NULL,
  `Price` FLOAT NOT NULL,
  `TradeStoneHandCount` INT NOT NULL,
  `PayType` INT NOT NULL,
  `FinishedStoneTradeHandCount` INT NOT NULL DEFAULT 0,
  `BuyState` INT NOT NULL,
  `DelegateTime` DATETIME NOT NULL,
  `IsSubOrder` INT(1) NOT NULL DEFAULT 0,
  `ParentOrderNumber` VARCHAR(35) NULL,
  `FinishedTime` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC),
  INDEX `FinishedStoneDelegateBuyOrderInfo_FK_userID_idx` (`UserID` ASC),
  CONSTRAINT `FinishedStoneDelegateBuyOrderInfo_FK_userID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);



-- -------------------------------------------------------------------------------


CREATE TABLE `superminers`.`stonestackdailyrecordinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Day` DATETIME NOT NULL,
  `OpenPrice` FLOAT NOT NULL,
  `ClosePrice` FLOAT NOT NULL,
  `MinTradeSucceedPrice` FLOAT NOT NULL,
  `MaxTradeSucceedPrice` FLOAT NOT NULL,
  `TradeSucceedStoneHandSum` INT NOT NULL,
  `TradeSucceedRMBSum` FLOAT NOT NULL,
  `DelegateSellStoneSum` INT NOT NULL,
  `DelegateBuyStoneSum` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `Day_UNIQUE` (`Day` ASC));



-- --------------------------------------------------------------------------------


ALTER TABLE `superminers`.`finishedstonedelegatebuyorderinfo` 
ADD COLUMN `AwardGoldCoin` FLOAT NOT NULL DEFAULT 0 AFTER `FinishedTime`;



-- ------------------------------------------------------------------------------


ALTER TABLE `superminers`.`notfinishedstonedelegatebuyorderinfo` 
ADD COLUMN `AwardGoldCoin` FLOAT NOT NULL DEFAULT 0 AFTER `FinishedTime`;



-- ----------------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161231023000' WHERE `id`='1';


-- ------------------