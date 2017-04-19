CREATE TABLE `xunlingmine2`.`virtualshoppingitem` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Remark` TEXT NOT NULL,
  `SellState` INT NOT NULL,
  `PlayerMaxBuyableCount` INT NOT NULL,
  `ValueRMB` FLOAT NOT NULL,
  `GainExp` FLOAT NOT NULL,
  `GainRMB` FLOAT NOT NULL,
  `GainGoldCoin` FLOAT NOT NULL,
  `GainMine_StoneReserves` FLOAT NOT NULL,
  `GainMiner` FLOAT NOT NULL,
  `GainStone` FLOAT NOT NULL,
  `GainDiamond` FLOAT NOT NULL,
  `GainShoppingCredits` FLOAT NOT NULL,
  `GainGravel` FLOAT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC));

-- ---------------------------------------------------

CREATE TABLE `xunlingmine2`.`diamondshoppingitem` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Type` INT NOT NULL,
  `Remark` TEXT NOT NULL,
  `SellState` INT NOT NULL,
  `ValueDiamonds` FLOAT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC));

-- ---------------------------------------------------

CREATE TABLE `xunlingmine2`.`playerbuyvirtualshoppingitemrecord` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `OrderNumber` VARCHAR(35) NOT NULL,
  `UserID` INT UNSIGNED NOT NULL,
  `VirtualShoppingItemID` INT UNSIGNED NOT NULL,
  `BuyTime` DATETIME NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  INDEX `PlayerBuyVirtualShoppingItemRecord_UserID_FK_idx` (`UserID` ASC),
  INDEX `PlayerBuyVirtualShoppingItemRecord_ItemID_FK_idx` (`VirtualShoppingItemID` ASC),
  CONSTRAINT `PlayerBuyVirtualShoppingItemRecord_UserID_FK`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `PlayerBuyVirtualShoppingItemRecord_ItemID_FK`
    FOREIGN KEY (`VirtualShoppingItemID`)
    REFERENCES `xunlingmine2`.`virtualshoppingitem` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- --------------------------------------------------

CREATE TABLE `xunlingmine2`.`playerbuydiamondshoppingitemrecord` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `OrderNumber` VARCHAR(35) NOT NULL,
  `UserID` INT UNSIGNED NOT NULL,
  `DiamondShoppingItemID` INT UNSIGNED NOT NULL,
  `BuyTime` DATETIME NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  INDEX `PlayerBuyDiamondShoppingItemRecord_UserID_FK_idx` (`UserID` ASC),
  INDEX `PlayerBuyDiamondShoppingItemRecord_ItemID_FK_idx` (`DiamondShoppingItemID` ASC),
  CONSTRAINT `PlayerBuyDiamondShoppingItemRecord_UserID_FK`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `PlayerBuyDiamondShoppingItemRecord_ItemID_FK`
    FOREIGN KEY (`DiamondShoppingItemID`)
    REFERENCES `xunlingmine2`.`diamondshoppingitem` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- -----------------------------------------------------

ALTER TABLE `xunlingmine2`.`playerbuyvirtualshoppingitemrecord` 
ADD COLUMN `OrderNumber` VARCHAR(35) NOT NULL AFTER `ID`,
ADD UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC);


-- -----------------------------------------------------

ALTER TABLE `xunlingmine2`.`playerbuydiamondshoppingitemrecord` 
CHANGE COLUMN `IsSend` `ShoppingState` INT(10) NOT NULL DEFAULT '0' ,
ADD COLUMN `OperTime` DATETIME NULL AFTER `OperAdmin`;

-- ------------------------------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170330220000' WHERE `id`='2';


