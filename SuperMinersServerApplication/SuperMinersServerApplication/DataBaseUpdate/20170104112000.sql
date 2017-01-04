ALTER TABLE `superminers`.`raiderroundmetadatainfo` 
ADD COLUMN `JoinedPlayerCount` INT NULL AFTER `AwardPoolSumStones`;


-- --------------------------------------------


UPDATE `superminers`.`paramtable` SET `ParamValue`='20170104112000' WHERE `id`='1';


