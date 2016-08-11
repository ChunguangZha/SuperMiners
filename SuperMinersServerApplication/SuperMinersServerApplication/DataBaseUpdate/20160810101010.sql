CREATE  TABLE `superminers`.`paramtable` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT ,
  `ParamName` VARCHAR(45) NOT NULL ,
  `ParamValue` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`id`) ,
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) ,
  UNIQUE INDEX `ParamName_UNIQUE` (`ParamName` ASC) );

--------------------------------------------------------------------
INSERT INTO `superminers`.`paramtable` (`ParamName`, `ParamValue`) VALUES ('DBVERSION', '20160811161616');

