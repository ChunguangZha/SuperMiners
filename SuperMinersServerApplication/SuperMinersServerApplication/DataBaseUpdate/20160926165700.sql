ALTER TABLE `superminers`.`roulettewinnerrecord` DROP FOREIGN KEY `fkey_RouletteWinnerRecord_AwardItemID` , DROP FOREIGN KEY `fkey_RouletteWinnerRecord_UserID` ;
ALTER TABLE `superminers`.`roulettewinnerrecord` CHANGE COLUMN `GotInfo1` `GotInfo1` VARCHAR(100) NOT NULL DEFAULT ''  , CHANGE COLUMN `GotInfo2` `GotInfo2` VARCHAR(100) NOT NULL DEFAULT ''  , 
  ADD CONSTRAINT `fkey_RouletteWinnerRecord_AwardItemID`
  FOREIGN KEY (`AwardItemID` )
  REFERENCES `superminers`.`rouletteawarditem` (`id` )
  ON DELETE CASCADE
  ON UPDATE NO ACTION, 
  ADD CONSTRAINT `fkey_RouletteWinnerRecord_UserID`
  FOREIGN KEY (`UserID` )
  REFERENCES `superminers`.`playersimpleinfo` (`id` )
  ON DELETE CASCADE
  ON UPDATE NO ACTION;

-- ------------------------------------------------------------------------------------------------------------------------

ALTER TABLE `superminers`.`roulettewinnerrecord` ADD COLUMN `GotTime` DATETIME NULL  AFTER `IsGot` , ADD COLUMN `PayTime` DATETIME NULL  AFTER `IsPay` ;

-- -----------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20160926165700' WHERE `id`='1';


