ALTER TABLE `xunlingmine2`.`userremotehandleservicerecord` 
CHANGE COLUMN `ServiceContent` `ServiceContent` TEXT NOT NULL ;

-- -----------------------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170324190000' WHERE `id`='2';


