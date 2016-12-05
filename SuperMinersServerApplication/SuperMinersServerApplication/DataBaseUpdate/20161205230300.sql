ALTER TABLE `superminers`.`playerweixinuseropenid` 
ADD COLUMN `BindTime` DATETIME NULL AFTER `WeiXinOpenID`;


-- ------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161205230300' WHERE `id`='1';

