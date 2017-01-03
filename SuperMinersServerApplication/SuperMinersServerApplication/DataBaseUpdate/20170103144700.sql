CREATE TABLE `superminers`.`raiderroundmetadatainfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `State` INT UNSIGNED NOT NULL,
  `StartTime` DATETIME NULL,
  `AwardPoolSumStones` INT NULL,
  `WinnerUserName` VARCHAR(64) NULL,
  `WinStones` INT NULL,
  `EndTime` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `in_UNIQUE` (`id` ASC));


-- ---------------------------------------

CREATE TABLE `superminers`.`raiderplayerbetinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `RaiderRoundID` INT UNSIGNED NOT NULL,
  `UserName` VARCHAR(64) NOT NULL,
  `BetStones` INT NOT NULL,
  `Time` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `RaiderPlayerBetInfo_FK_RaiderRoundInfoID_idx` (`RaiderRoundID` ASC),
  CONSTRAINT `RaiderPlayerBetInfo_FK_RaiderRoundInfoID`
    FOREIGN KEY (`RaiderRoundID`)
    REFERENCES `superminers`.`raiderroundmetadatainfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
    
-- -------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170103144700' WHERE `id`='1';

