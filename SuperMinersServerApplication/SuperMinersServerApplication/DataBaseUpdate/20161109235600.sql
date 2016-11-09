CREATE TABLE `superminers`.`playerlogininfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `LoginIP` VARCHAR(45) NOT NULL,
  `LoginMac` VARCHAR(100) NOT NULL,
  `LoginTime` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));


-- ---------------------------------------

ALTER TABLE `superminers`.`playerlogininfo` 
ADD COLUMN `UserID` INT UNSIGNED NOT NULL AFTER `LoginTime`,
ADD INDEX `playerlogininfo_FK_UserID_idx` (`UserID` ASC);
ALTER TABLE `superminers`.`playerlogininfo` 
ADD CONSTRAINT `playerlogininfo_FK_UserID`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


-- --------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161109235600' WHERE `id`='1';

