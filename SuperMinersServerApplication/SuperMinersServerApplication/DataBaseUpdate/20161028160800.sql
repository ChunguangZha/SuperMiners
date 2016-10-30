CREATE TABLE `superminers`.`currentrouletteawarditemlist` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Index` INT UNSIGNED NOT NULL,
  `AwarditemID` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `index_UNIQUE` (`Index` ASC),
  INDEX `currentrouletteawarditemlist_FK_AwardItemID_idx` (`AwarditemID` ASC),
  CONSTRAINT `currentrouletteawarditemlist_FK_AwardItemID`
    FOREIGN KEY (`AwarditemID`)
    REFERENCES `superminers`.`rouletteawarditem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



-- ---------------------------------------------------------------------------------------------------------

update superminers.rouletteawarditem 
set RouletteAwardType=5 
where id in
( 
2,4,7,9,11
);

-- ---------------------------------------------------------------------------------------------------------

ALTER TABLE `superminers`.`rouletteawarditem` 
DROP COLUMN `IsRealAward`;



-- ------------------------------------------------------------------------------------------------------------

ALTER TABLE `superminers`.`rouletteawarditem` 
ADD COLUMN `IconBuffer` BLOB NULL AFTER `WinProbability`;



-- ------------------------------------------------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161028160800' WHERE `id`='1';


