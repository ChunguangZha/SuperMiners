ALTER TABLE `xunlingmine2`.`playerstonefactoryaccountinfo` 
ADD COLUMN `AutoFeedSumTimes` INT NOT NULL DEFAULT 0 AFTER `EnableSlavesGroupCount`;


-- -----------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170607225000' WHERE `id`='2';

