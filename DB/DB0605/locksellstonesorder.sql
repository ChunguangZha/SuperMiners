CREATE TABLE `superminers`.`locksellstonesorder` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `SellOrderID` INT UNSIGNED NOT NULL,
  `LockedByUserID` INT UNSIGNED NOT NULL,
  `LockedTime` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `SellOrderID_UNIQUE` (`SellOrderID` ASC),
  UNIQUE INDEX `LockedByUserID_UNIQUE` (`LockedByUserID` ASC),
  CONSTRAINT `locksellstoneorder_sellorderid_sellstonesorder_id`
    FOREIGN KEY (`SellOrderID`)
    REFERENCES `superminers`.`sellstonesorder` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `locksellstoneorder_selluserid_playersimpleinfo_id`
    FOREIGN KEY (`LockedByUserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
