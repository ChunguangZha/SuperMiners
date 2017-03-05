ALTER TABLE `xunlingmine2`.`withdrawrmbrecord` 
ADD CONSTRAINT `withdrawrmbrecord_FK_UserID`
  FOREIGN KEY (`PlayerUserID`)
  REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


-- -------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='20170303220000' WHERE `id`='1';
