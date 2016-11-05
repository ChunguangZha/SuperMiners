ALTER TABLE `superminers`.`playersimpleinfo` 
ADD COLUMN `IDCardNo` VARCHAR(18) NULL AFTER `AlipayRealName`,
ADD UNIQUE INDEX `IDCardNo_UNIQUE` (`IDCardNo` ASC),
DROP INDEX `AlipayRealName_UNIQUE` ;


-- ----------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161105205900' WHERE `id`='1';


