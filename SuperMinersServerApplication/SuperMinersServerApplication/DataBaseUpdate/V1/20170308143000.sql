ALTER TABLE `superminers`.`oldplayertransferregisterinfo` 
CHANGE COLUMN `ID` `ID` INT(10) UNSIGNED NOT NULL ,
ADD COLUMN `NewServerUserLoginName` VARCHAR(150) NULL AFTER `Email`,
ADD COLUMN `NewServerPassword` VARCHAR(30) NULL AFTER `NewServerUserLoginName`;


-- -----------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170308143000' WHERE `id`='1';

