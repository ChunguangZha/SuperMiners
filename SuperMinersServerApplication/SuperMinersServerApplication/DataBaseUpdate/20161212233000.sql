ALTER TABLE `superminers`.`playerfortuneinfo` 
ADD COLUMN `CreditValue` INT UNSIGNED NOT NULL DEFAULT 0 AFTER `Exp`;


-- -------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161212233000' WHERE `id`='1';


