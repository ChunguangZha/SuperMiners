ALTER TABLE `superminers`.`sellstonesorder` 
DROP FOREIGN KEY `buyeruserid_foreignkey`;
ALTER TABLE `superminers`.`sellstonesorder` 
DROP COLUMN `LockedTime`,
DROP COLUMN `LockedByUserID`,
DROP INDEX `buyeruserid_foreignkey_idx` , 
COMMENT = '�ñ�ֻ����δ��ɵĶ���' ;
ALTER TABLE `superminers`.`sellstonesorder` 
ADD INDEX `OrderNumber_index` (`OrderNumber` ASC);
