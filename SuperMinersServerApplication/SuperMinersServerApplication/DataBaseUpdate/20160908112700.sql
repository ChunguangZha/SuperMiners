ALTER TABLE `superminers`.`playerfortuneinfo` ADD COLUMN `FirstRechargeGoldCoinAward` TINYINT(1) NOT NULL DEFAULT 0  AFTER `FreezingDiamonds` ;
-- ---------------------------------------------------------------

ALTER TABLE `superminers`.`alipayrechargerecord` ADD COLUMN `user_name` VARCHAR(64) NOT NULL DEFAULT ''  AFTER `alipay_trade_no` ;


-- -----------------------------------------------------

ALTER TABLE `superminers`.`alipayrechargerecord` ADD COLUMN `value_rmb` FLOAT NOT NULL DEFAULT 0  AFTER `total_fee` ;


