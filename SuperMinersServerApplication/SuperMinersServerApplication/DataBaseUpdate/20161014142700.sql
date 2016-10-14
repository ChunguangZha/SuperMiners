ALTER TABLE `superminers`.`playersimpleinfo` 
ADD COLUMN `GroupType` INT NOT NULL DEFAULT 0 AFTER `Password`;


-- -----------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161014142700' WHERE `id`='1';
