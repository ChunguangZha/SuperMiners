CREATE TABLE `superminers`.`waittoawardexprecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `ReferrerUserName` VARCHAR(64) NOT NULL,
  `NewRegisterUserNme` VARCHAR(64) NOT NULL,
  `AwardGoldCoin` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));


