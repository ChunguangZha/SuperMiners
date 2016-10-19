ALTER TABLE `superminers`.`withdrawrmbrecord` 
CHANGE COLUMN `IsPayedSucceed` `RMBWithdrawState` INT NOT NULL DEFAULT 0 COMMENT '1:true; 0:false' ;


-- ------------------------------------------------------------

ALTER TABLE `superminers`.`withdrawrmbrecord` 
ADD COLUMN `Message` VARCHAR(200) NULL AFTER `PayTime`;


-- --------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161019220400' WHERE `id`='1';

