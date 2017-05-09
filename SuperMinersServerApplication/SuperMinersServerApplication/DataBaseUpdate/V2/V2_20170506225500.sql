CREATE TABLE `xunlingmine2`.`postaddress` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `Province` VARCHAR(100) NOT NULL,
  `City` VARCHAR(100) NOT NULL,
  `County` VARCHAR(100) NOT NULL,
  `DetailAddress` VARCHAR(400) NOT NULL,
  `ReceiverName` VARCHAR(150) NOT NULL,
  `PhoneNumber` VARCHAR(12) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `PostAddress_UserID_FK_idx` (`UserID` ASC),
  CONSTRAINT `PostAddress_UserID_FK`
    FOREIGN KEY (`UserID`)
    REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ---------------------------

ALTER TABLE `xunlingmine2`.`diamondshoppingitem` 
ADD COLUMN `DetailText` VARCHAR(500) NOT NULL DEFAULT '' AFTER `ValueDiamonds`,
ADD COLUMN `DetailImageNames` VARCHAR(1000) NOT NULL DEFAULT '' AFTER `DetailText`;


-- ---------------------------


UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170506225500' WHERE `id`='2';


