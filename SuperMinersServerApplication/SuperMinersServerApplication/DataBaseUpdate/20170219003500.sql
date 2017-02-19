ALTER TABLE `superminers`.`playerfortuneinfo` 
ADD COLUMN `UserRemoteServerValidStopTime` DATETIME NULL AFTER `ShoppingCreditsFreezed`;

-- ------------------------------

ALTER TABLE `superminers`.`deletedplayerinfo` 
ADD COLUMN `UserRemoteServerValidStopTime` DATETIME NULL AFTER `ShoppingCreditsFreezed`;

-- ------------------------------

CREATE TABLE `superminers`.`userremoteserveritem` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `ServerType` INT UNSIGNED NOT NULL COMMENT '\n        Once,\n        OneMonth,\n        HalfYear,\n        OneYear,',
  `PayMoneyYuan` INT UNSIGNED NOT NULL,
  `ShopName` VARCHAR(45) NOT NULL,
  `Description` VARCHAR(150)  NOT NULL,  
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `ServerType_UNIQUE` (`ServerType` ASC));

-- ----------------------------

CREATE TABLE `superminers`.`userremoteserverbuyrecord` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `ServerType` INT NOT NULL COMMENT '        Once,\n        OneMonth,\n        HalfYear,\n        OneYear,',
  `PayMoneyYuan` INT UNSIGNED NOT NULL,
  `OrderNumber` VARCHAR(35) NOT NULL,
  `GetShoppingCredits` INT UNSIGNED NOT NULL,
  `BuyRemoteServerTime` DATETIME NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  INDEX `userremoteserverbuyrecord_FK_UserID_idx` (`UserID` ASC),
  CONSTRAINT `userremoteserverbuyrecord_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- -------------------------

CREATE TABLE `superminers`.`userremotehandleservicerecord` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `ServiceTime` DATETIME NOT NULL,
  `WorkerName` VARCHAR(150) NOT NULL,
  `ServiceContent` VARCHAR(2000) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  INDEX `UserRemoteHandleServiceRecord_FK_userID_idx` (`UserID` ASC),
  CONSTRAINT `UserRemoteHandleServiceRecord_FK_userID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- ------------------------------

CREATE TABLE `superminers`.`oldplayertransferregisterinfo` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserName` VARCHAR(150) NOT NULL,
  `AlipayAccount` VARCHAR(128) NOT NULL,
  `AlipayRealName` VARCHAR(50) NOT NULL,
  `Email` VARCHAR(128) NOT NULL,
  `SubmitTime` DATETIME NOT NULL,
  `isTransfered` INT(1) NOT NULL,
  `HandledTime` DATETIME NULL,
  `HandlerName` VARCHAR(150) NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC));

-- ------------------------------

ALTER TABLE `superminers`.`oldplayertransferregisterinfo` 
ADD UNIQUE INDEX `UserName_UNIQUE` (`UserName` ASC);

-- --------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170219003500' WHERE `id`='1';

