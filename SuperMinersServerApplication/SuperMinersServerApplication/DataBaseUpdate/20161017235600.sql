ALTER TABLE `superminers`.`admininfo` 
ADD COLUMN `GroupType` INT NOT NULL DEFAULT 0 AFTER `ActionPassword`;


-- ------------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161017235600' WHERE `id`='1';
