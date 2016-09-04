CREATE TABLE `superminers`.`withdrawrmbrecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `PlayerUserName` VARCHAR(64) NOT NULL,
  `WidthdrawRMB` FLOAT NOT NULL,
  `CreateTime` DATETIME NOT NULL,
  `AdminUserName` VARCHAR(64) NULL,
  `PayTime` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));


---------------------------------------------------------------------

ALTER TABLE `superminers`.`withdrawrmbrecord` 
ADD COLUMN `IsPayedSucceed` TINYINT(1) NOT NULL COMMENT '1:true; 0:false' AFTER `CreateTime`;


