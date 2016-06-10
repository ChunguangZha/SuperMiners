CREATE TABLE `superminers`.`buystonesrecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `OrderNumber` VARCHAR(200) NOT NULL,
  `SellerUserID` INT UNSIGNED NOT NULL,
  `SellStonesCount` INT NOT NULL,
  `Expense` FLOAT NOT NULL,
  `TradeRMB` FLOAT NOT NULL,
  `SellTime` DATETIME NOT NULL,
  `BuyerUserID` INT UNSIGNED NOT NULL,
  `BuyTime` DATETIME NOT NULL,
  `AwardGoldCoin` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `OrderNumber_UNIQUE` (`OrderNumber` ASC),
  INDEX `sellerUserID_idx` (`SellerUserID` ASC),
  INDEX `buyerUserID_idx` (`BuyerUserID` ASC),
  CONSTRAINT `sellerUserID`
    FOREIGN KEY (`SellerUserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE RESTRICT
    ON UPDATE NO ACTION,
  CONSTRAINT `buyerUserID`
    FOREIGN KEY (`BuyerUserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE RESTRICT
    ON UPDATE NO ACTION);
