ALTER TABLE `xunlingmine2`.`playerfortuneinfo` 
ADD COLUMN `MakeAVowToGodTime_DayofYear` INT NOT NULL DEFAULT 0 AFTER `UserRemoteServiceValidTimes`,
ADD COLUMN `MakeAVowToGodTimesLastDay` INT NOT NULL DEFAULT 0 AFTER `MakeAVowToGodTime_DayofYear`;

-- -----------------------------

UPDATE `xunlingmine2`.`paramtable` SET `ParamValue`='V2_20170305185000' WHERE `id`='2';




