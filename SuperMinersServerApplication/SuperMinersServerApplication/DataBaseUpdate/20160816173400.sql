ALTER TABLE `superminers`.`alipayrechargerecord` 
DROP INDEX `buyer_email_UNIQUE` ;

-- ---------------------------------------------------------------------------------

ALTER TABLE `superminers`.`locksellstonesorder` 
CHANGE COLUMN `PayUrl` `PayUrl` VARCHAR(200) NOT NULL ;



