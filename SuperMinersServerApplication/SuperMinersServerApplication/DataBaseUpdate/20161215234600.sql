CREATE TABLE `superminers`.`playerlastsellstonerecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `SellStoneOrderNumber` VARCHAR(35) NOT NULL,
  `SellTime` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  CONSTRAINT `PlayerLastSellStoneRecord_FK_userid`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- -----------------------------------------

ALTER TABLE `superminers`.`playerfortuneinfo` 
ADD COLUMN `StoneSellQuan` INT NOT NULL DEFAULT 0 AFTER `FreezingDiamonds`;


-- -----------------------------------------

