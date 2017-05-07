ALTER TABLE `xunlingmine2`.`playerfortuneinfo` 
CHANGE COLUMN `ShoppingCreditsEnabled` `ShoppingCreditsEnabled` FLOAT NOT NULL DEFAULT '0' ,
CHANGE COLUMN `ShoppingCreditsFreezed` `ShoppingCreditsFreezed` FLOAT NOT NULL DEFAULT '0' ;


-- ----------------------------------------

ALTER TABLE `xunlingmine2`.`virtualshoppingitem` 
CHANGE COLUMN `ValueRMB` `ValueShoppingCredits` FLOAT NOT NULL ;


-- ----------------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170422000000' WHERE `id`='2';
