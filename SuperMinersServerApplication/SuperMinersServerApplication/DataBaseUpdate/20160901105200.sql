ALTER TABLE `superminers`.`waittoawardexprecord` CHANGE COLUMN `AwardGoldCoin` `AwardLevel` INT(11) NOT NULL  ;-- -------------------------------------------------------------ALTER TABLE `superminers`.`registeruserconfig` ADD COLUMN `FirstAlipayRechargeGoldCoinAwardMultiple` FLOAT NOT NULL DEFAULT 0  AFTER `GiveToNewUserStones` ;

