CREATE TABLE `superminers`.`gamblestoneroundinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `StartTime` DATETIME NOT NULL,
  `FinishedInningCount` INT NOT NULL DEFAULT 0,
  `EndTime` DATETIME NULL,
  `CurrentWinRedCount` INT NOT NULL DEFAULT 0,
  `CurrentWinGreenCount` INT NOT NULL DEFAULT 0,
  `CurrentWinBlueCount` INT NOT NULL DEFAULT 0,
  `CurrentWinPurpleCount` INT NOT NULL DEFAULT 0,
  `LastWinRedCount` INT NOT NULL DEFAULT 0,
  `LastWinGreenCount` INT NOT NULL DEFAULT 0,
  `LastWinBlueCount` INT NOT NULL DEFAULT 0,
  `LastWinPurpleCount` INT NOT NULL DEFAULT 0,
  `AllBetInStone` INT NOT NULL DEFAULT 0,
  `AllWinnedOutStone` INT NOT NULL DEFAULT 0,
  `TableName` VARCHAR(6) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));


-- -----------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170122174700' WHERE `id`='1';


