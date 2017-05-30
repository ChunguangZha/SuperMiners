ALTER TABLE `xunlingmine2`.`diamondshoppingitem` 
ADD COLUMN `Stocks` INT NOT NULL DEFAULT 0 AFTER `Type`;


-- ---------------------------


UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170528152000' WHERE `id`='2';
