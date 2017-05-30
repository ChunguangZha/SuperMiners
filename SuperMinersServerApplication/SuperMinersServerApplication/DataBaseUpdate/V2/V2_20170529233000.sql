ALTER TABLE `xunlingmine2`.`stonefactorystackchangerecord` 
DROP FOREIGN KEY `StoneFactoryStackChangeRecord_FK_UserID`;
ALTER TABLE `xunlingmine2`.`stonefactorystackchangerecord` 
DROP INDEX `UserID_UNIQUE` ;

-- --------------------------

ALTER TABLE `xunlingmine2`.`stonefactorystackchangerecord` 
ADD INDEX `stonefactorystackchangerecord_FK_UserID_idx` (`UserID` ASC);
ALTER TABLE `xunlingmine2`.`stonefactorystackchangerecord` 
ADD CONSTRAINT `stonefactorystackchangerecord_FK_UserID`
  FOREIGN KEY (`UserID`)
  REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE CASCADE;


-- ----------------------------

ALTER TABLE `xunlingmine2`.`stonefactoryprofitrmbchangedrecord` 
DROP FOREIGN KEY `StoneFactoryProfitRMBChangedRecord_FK_UserID`;
ALTER TABLE `xunlingmine2`.`stonefactoryprofitrmbchangedrecord` 
DROP INDEX `UserID_UNIQUE` ;


-- -----------------------------

ALTER TABLE `xunlingmine2`.`stonefactoryprofitrmbchangedrecord` 
ADD INDEX `stonefactoryprofitrmbchangedrecord_FK_UserID_idx` (`UserID` ASC);
ALTER TABLE `xunlingmine2`.`stonefactoryprofitrmbchangedrecord` 
ADD CONSTRAINT `stonefactoryprofitrmbchangedrecord_FK_UserID`
  FOREIGN KEY (`UserID`)
  REFERENCES `xunlingmine2`.`playersimpleinfo` (`id`)
  ON DELETE CASCADE
  ON UPDATE CASCADE;

-- ----------------------------------

ALTER TABLE `xunlingmine2`.`virtualshoppingitem` 
ADD COLUMN `ItemType` INT NOT NULL DEFAULT 0 AFTER `Remark`;

-- --------------------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170529233000' WHERE `id`='2';

