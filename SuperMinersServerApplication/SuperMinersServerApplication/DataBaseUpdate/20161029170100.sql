CREATE TABLE `superminers`.`rouletteroundinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `AwardPoolSumStone` INT UNSIGNED NOT NULL,
  `WinAwardSumYuan` DOUBLE NOT NULL,
  `StartTime` DATETIME NOT NULL,
  `MustWinAwardItemID` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `RouletteRoundInfo_FK_MustWinAwardItemID_idx` (`MustWinAwardItemID` ASC),
  CONSTRAINT `RouletteRoundInfo_FK_MustWinAwardItemID`
    FOREIGN KEY (`MustWinAwardItemID`)
    REFERENCES `superminers`.`rouletteawarditem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ----------------------------------

ALTER TABLE `superminers`.`rouletteroundinfo` 
ADD COLUMN `Finished` INT(1) NOT NULL AFTER `MustWinAwardItemID`;


-- --------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161029170100' WHERE `id`='1';


-- -----------------------