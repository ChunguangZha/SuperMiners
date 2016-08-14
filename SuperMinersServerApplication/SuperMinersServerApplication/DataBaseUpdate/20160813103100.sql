ALTER TABLE `superminers`.`minesbuyrecord` 
DROP FOREIGN KEY `userinfo_id_MinesBuyRecord_userid`;
ALTER TABLE `superminers`.`minesbuyrecord` 
ADD INDEX `userinfo_id_MinesBuyRecord_userid_idx` (`UserID` ASC),
DROP INDEX `UserID_UNIQUE` ;
ALTER TABLE `superminers`.`minesbuyrecord` 
ADD CONSTRAINT `userinfo_id_MinesBuyRecord_userid`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


-- ------------------------------------------------------------------------------

ALTER TABLE `superminers`.`minersbuyrecord` 
DROP FOREIGN KEY `userinfo_id_MinersBuyRecord_userid`;
ALTER TABLE `superminers`.`minersbuyrecord` 
ADD INDEX `userinfo_id_MinersBuyRecord_userid_idx` (`UserID` ASC),
DROP INDEX `UserID_UNIQUE` ;
ALTER TABLE `superminers`.`minersbuyrecord` 
ADD CONSTRAINT `userinfo_id_MinersBuyRecord_userid`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


-- --------------------------------------------------------------------------------

ALTER TABLE `superminers`.`tempminesbuyrecord` 
ADD COLUMN `OrderNumber` VARCHAR(35) NOT NULL AFTER `id`,
ADD UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC);


-- --------------------------------------------------------------------------------

ALTER TABLE `superminers`.`locksellstonesorder` 
CHANGE COLUMN `PayUrl` `PayUrl` VARCHAR(150) NOT NULL ;


