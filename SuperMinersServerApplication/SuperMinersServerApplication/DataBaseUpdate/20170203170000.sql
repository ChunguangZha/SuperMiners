ALTER TABLE `superminers`.`gamblestoneroundinfo` 
ADD COLUMN `WinColorItems` BINARY(68) NOT NULL AFTER `AllWinnedOutStone`;


-- --------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170203170000' WHERE `id`='1';

