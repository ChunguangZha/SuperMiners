CREATE TABLE `superminers`.`gamblestonedailyscheme` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Date` DATE NOT NULL,
  `ProfitStoneObjective` INT NOT NULL,
  `AllBetInStone` INT NOT NULL,
  `AllWinnedOutStone` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `Date_UNIQUE` (`Date` ASC));


-- ------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20170125174600' WHERE `id`='1';

