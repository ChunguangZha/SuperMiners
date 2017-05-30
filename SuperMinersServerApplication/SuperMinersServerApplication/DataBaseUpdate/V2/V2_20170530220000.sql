ALTER TABLE `xunlingmine2`.`playerstonefactoryaccountinfo` 
ADD COLUMN `LastFeedSlaveTime` DATETIME NULL AFTER `Food`;

-- ------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170530220000' WHERE `id`='2';
