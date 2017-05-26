ALTER TABLE `xunlingmine2`.`playerstonefactoryaccountinfo` 
ADD COLUMN `FreezingSlavesCount` INT NOT NULL DEFAULT 0 AFTER `LastDayValidStoneStack`,
ADD COLUMN `SlavesCount` INT NOT NULL DEFAULT 0 AFTER `FreezingSlavesCount`;

-- -----------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170526160000' WHERE `id`='2';

