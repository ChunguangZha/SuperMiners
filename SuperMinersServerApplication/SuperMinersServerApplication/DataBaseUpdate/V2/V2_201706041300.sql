ALTER TABLE `xunlingmine2`.`stonefactoryprofitrmbchangedrecord` 
ADD COLUMN `ValidStoneCount` FLOAT NOT NULL DEFAULT 0 AFTER `OperRMB`;


-- ------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_201706041300' WHERE `id`='2';
