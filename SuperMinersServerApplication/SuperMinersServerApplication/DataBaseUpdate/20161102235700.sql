CREATE TABLE `superminers`.`rouletteconfig` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `RouletteLargeWinMultiple` FLOAT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));


-- ---------------------------------------------

INSERT INTO `superminers`.`rouletteconfig` (`RouletteLargeWinMultiple`) VALUES ('2.5');


-- -------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161102235700' WHERE `id`='1';


-- ------------------