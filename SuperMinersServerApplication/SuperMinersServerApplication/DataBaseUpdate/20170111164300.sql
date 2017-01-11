CREATE TABLE `superminers`.`playergravelrequsetrecordinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `RequestDate` DATETIME NOT NULL,
  `IsResponsed` INT(1) NOT NULL,
  `ResponseDate` DATETIME NULL,
  `Gravel` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `PlayerGravelRequsetRecordInfo_FK_UserID_idx` (`UserID` ASC),
  CONSTRAINT `PlayerGravelRequsetRecordInfo_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- -----------------------------------------------------------------------

CREATE TABLE `superminers`.`graveldistributerecordinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `CreateDate` DATETIME NOT NULL,
  `AllPlayerCount` INT NOT NULL,
  `RequestPlayerCount` INT NOT NULL,
  `ResponseGravelCount` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `CreateDate_UNIQUE` (`CreateDate` ASC));


-- -----------------------------------------------------------------------

CREATE TABLE `superminers`.`playergravelinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `Gravel` INT UNSIGNED NOT NULL DEFAULT 0,
  `FirstGetGravelTime` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  CONSTRAINT `PlayerGravelInfo_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- ----------------------------------------------------------------------

ALTER TABLE `superminers`.`playergravelrequsetrecordinfo` 
ADD COLUMN `IsGoted` INT(1) NOT NULL DEFAULT 0 AFTER `Gravel`;


-- ----------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170111164300' WHERE `id`='1';
