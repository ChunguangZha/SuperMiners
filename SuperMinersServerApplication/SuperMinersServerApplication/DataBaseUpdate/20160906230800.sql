ALTER TABLE `superminers`.`withdrawrmbrecord` 
ADD COLUMN `ValueYuan` FLOAT NOT NULL AFTER `WidthdrawRMB`;


--------------------------------------------------------------------------

ALTER TABLE `superminers`.`withdrawrmbrecord` 
CHANGE COLUMN `ValueYuan` `ValueYuan` INT NOT NULL ,
ADD COLUMN `AlipayOrderNumber` VARCHAR(45) NULL AFTER `AdminUserName`;


