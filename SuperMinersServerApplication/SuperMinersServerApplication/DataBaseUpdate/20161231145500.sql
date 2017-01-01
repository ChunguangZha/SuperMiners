ALTER TABLE `superminers`.`notfinishedstonedelegatebuyorderinfo` 
ADD COLUMN `AlipayLink` VARCHAR(300) NULL AFTER `AwardGoldCoin`;


-- --------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161231145500' WHERE `id`='1';


-- --------------------------------------

