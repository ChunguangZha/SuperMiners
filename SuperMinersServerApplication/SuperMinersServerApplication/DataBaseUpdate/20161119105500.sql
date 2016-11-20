
DROP TABLE IF EXISTS `playerlogininfo`;


CREATE TABLE `playerlogininfo` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` int(10) unsigned NOT NULL,
  `LastLoginIP` varchar(20) NOT NULL,
  `LastLoginMac` varchar(45) NOT NULL,
  `LastLoginTime` datetime NOT NULL,
  `LastLogoutTime` datetime DEFAULT NULL,
  `PreviousLoginIP` varchar(20) DEFAULT NULL,
  `PreviousLoginMac` varchar(45) DEFAULT NULL,
  `PreviousLoginTime` datetime DEFAULT NULL,
  `PreviousLogoutTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `playerlogininfo_FK_UserID_idx` (`UserID`),
  CONSTRAINT `playerlogininfo_FK_UserID` FOREIGN KEY (`UserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;


-- -------------------------------------------------

ALTER TABLE `superminers`.`playersimpleinfo` 
ADD COLUMN `LastLoginIP` VARCHAR(25) NULL AFTER `LockedLoginTime`,
ADD COLUMN `LastLoginMac` VARCHAR(45) NULL AFTER `LastLoginIP`;


-- -------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161119105500' WHERE `id`='1';


-- -------------------------------------------------
