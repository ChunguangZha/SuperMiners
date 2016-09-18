ALTER TABLE `superminers`.`withdrawrmbrecord` 
ADD COLUMN `AlipayAccount` VARCHAR(128) NULL AFTER `PlayerUserName`,
ADD COLUMN `AlipayRealName` VARCHAR(30) NULL AFTER `AlipayAccount`;


