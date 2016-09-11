CREATE  TABLE `superminers`.`expchangerecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT ,
  `UserID` INT UNSIGNED NOT NULL ,
  `AddExp` FLOAT NOT NULL ,
  `NewExp` FLOAT NOT NULL ,
  `Time` DATETIME NOT NULL ,
  `OperContent` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`id`) ,
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) ,
  INDEX `fk_expchangerecord_playersimpleinfo_userid` (`UserID` ASC) );


