CREATE TABLE `superminers`.`playerweixinuseropenid` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `WeiXinOpenID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC),
  UNIQUE INDEX `WeiXinOpenID_UNIQUE` (`WeiXinOpenID` ASC),
  CONSTRAINT `PlayerWeiXinUserOpenId_FK_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- -----------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161121221100' WHERE `id`='1';


