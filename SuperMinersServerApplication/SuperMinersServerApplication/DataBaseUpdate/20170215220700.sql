ALTER TABLE `superminers`.`withdrawrmbrecord` 
DROP COLUMN `PlayerUserName`,
ADD COLUMN `PlayerUserID` INT UNSIGNED NOT NULL AFTER `id`,
ADD INDEX `withdrawrmbrecord_FK_UserID_idx` (`PlayerUserID` ASC);
ALTER TABLE `superminers`.`withdrawrmbrecord` 
ADD CONSTRAINT `withdrawrmbrecord_FK_UserID`
  FOREIGN KEY (`PlayerUserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE CASCADE;

-- -------------------------------

ALTER TABLE `superminers`.`raiderplayerbetinfo` 
CHANGE COLUMN `UserName` `UserID` INT UNSIGNED NOT NULL ,
ADD INDEX `raiderplayerbetinfo_FK_userID_idx` (`UserID` ASC);
ALTER TABLE `superminers`.`raiderplayerbetinfo` 
ADD CONSTRAINT `raiderplayerbetinfo_FK_userID`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE CASCADE;

-- ---------------------------------

ALTER TABLE `superminers`.`playersimpleinfo` 
ADD COLUMN `UserLoginName` VARCHAR(64) NOT NULL DEFAULT '' AFTER `id`,
ADD UNIQUE INDEX `UserLoginName_UNIQUE` (`UserLoginName` ASC);

-- ---------------------------------

ALTER TABLE `superminers`.`deletedplayerinfo` 
ADD COLUMN `UserLoginName` VARCHAR(64) NOT NULL DEFAULT '' AFTER `id`,
ADD UNIQUE INDEX `UserLoginName_UNIQUE` (`UserLoginName` ASC);

-- -------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170215220700' WHERE `id`='1';
