ALTER TABLE `superminers`.`sellstonesorder` 
DROP FOREIGN KEY `buyeruserid_foreignkey`;
ALTER TABLE `superminers`.`sellstonesorder` 
DROP COLUMN `LockedTime`,
DROP COLUMN `LockedByUserID`,
DROP INDEX `buyeruserid_foreignkey_idx` , 
COMMENT = '该表只保存未完成的订单' ;
ALTER TABLE `superminers`.`sellstonesorder` 
ADD INDEX `OrderNumber_index` (`OrderNumber` ASC);
