CREATE TABLE `xunlingmine2`.`playerstonefactoryaccountinfo` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `FactoryIsOpening` INT(1) NOT NULL,
  `FactoryLiveDays` INT NOT NULL DEFAULT 3,
  `Food` INT NOT NULL DEFAULT 0,
  `LastDayValidStoneStack` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  CONSTRAINT `PlayerStoneFactoryAccountInfo_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- -------------------------------------------------------

CREATE TABLE `xunlingmine2`.`stonefactoryonegroupslave` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `JoinInSlaveCount` INT NOT NULL,
  `LiveSlaveCount` INT NOT NULL,
  `ChargeTime` DATETIME NOT NULL,
  `isLive` INT(1) NOT NULL,
  `LifeDays` INT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  CONSTRAINT `StoneFactoryOneGroupSlave_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- -------------------------------------------------------

CREATE TABLE `xunlingmine2`.`stonefactoryprofitrmbchangedrecord` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `OperRMB` INT NOT NULL,
  `ProfitType` INT NOT NULL,
  `OperTime` DATETIME NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  CONSTRAINT `StoneFactoryProfitRMBChangedRecord_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- -------------------------------------------------------

CREATE TABLE `xunlingmine2`.`stonefactorystackchangerecord` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `JoinStoneStackCount` INT NOT NULL,
  `Time` DATETIME NOT NULL,
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  CONSTRAINT `StoneFactoryStackChangeRecord_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- -------------------------------------------------------

CREATE TABLE `xunlingmine2`.`stonefactorysystemdailyprofit` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `profitRate` FLOAT NOT NULL,
  `Day` DATETIME NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC));


-- -------------------------------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170525220000' WHERE `id`='2';
