ALTER TABLE `superminers`.`playerfortuneinfo` 
ADD COLUMN `IsLongTermRemoteServiceUser` INT(1) UNSIGNED NOT NULL DEFAULT 0 AFTER `UserRemoteServerValidStopTime`,
ADD COLUMN `UserRemoteServiceValidTimes` INT NOT NULL DEFAULT 0 AFTER `IsLongTermRemoteServiceUser`;

-- ----------------------------------

ALTER TABLE `superminers`.`userremotehandleservicerecord` 
ADD COLUMN `AdminUserName` VARCHAR(150) NOT NULL AFTER `ServiceContent`;


-- ----------------------------------


UPDATE `superminers`.`paramtable` SET `ParamValue`='20170228235000' WHERE `id`='1';
