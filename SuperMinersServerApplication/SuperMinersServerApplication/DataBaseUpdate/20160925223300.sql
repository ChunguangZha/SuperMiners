CREATE TABLE `superminers`.`rouletteawarditem` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `AwardName` VARCHAR(45) NOT NULL,
  `AwardNumber` INT NOT NULL DEFAULT 1,
  `RouletteAwardType` INT NOT NULL,
  `ValueMoneyYuan` FLOAT NOT NULL DEFAULT 0,
  `IsLargeAward` INT(1) NOT NULL,
  `IsRealAward` INT(1) NOT NULL,
  `WinProbability` FLOAT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));



-- ---------------------------------------------------------------------

CREATE TABLE `superminers`.`roulettewinnerrecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `AwardItemID` INT UNSIGNED NOT NULL,
  `WinTime` DATETIME NOT NULL,
  `IsGot` INT(1) NOT NULL,
  `IsPay` INT(1) NOT NULL,
  `GotInfo1` VARCHAR(45) NOT NULL DEFAULT '',
  `GotInfo2` VARCHAR(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE INDEX `ID_UNIQUE` (`id` ASC),
  INDEX `fkey_RouletteWinnerRecord_UserID_idx` (`UserID` ASC),
  INDEX `fkey_RouletteWinnerRecord_AwardItemID_idx` (`AwardItemID` ASC),
  CONSTRAINT `fkey_RouletteWinnerRecord_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkey_RouletteWinnerRecord_AwardItemID`
    FOREIGN KEY (`AwardItemID`)
    REFERENCES `superminers`.`rouletteawarditem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);




