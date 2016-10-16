ALTER TABLE `superminers`.`playersimpleinfo` 
ADD COLUMN `IsAgentReferred` INT(1) NOT NULL DEFAULT 0 AFTER `GroupType`,
ADD COLUMN `AgentReferredLevel` INT NOT NULL DEFAULT 0 AFTER `IsAgentReferred`;


-- ----------------------------------------------------------------

CREATE TABLE `superminers`.`agentuserinfo` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `UserID` INT UNSIGNED NOT NULL,
  `TotalAwardRMB` FLOAT NOT NULL DEFAULT 0,
  `InvitationURL` VARCHAR(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC));


-- -------------------------------------------------------------------

ALTER TABLE `superminers`.`agentuserinfo` 
ADD CONSTRAINT `agentuserinfo_UserID_FK`
  FOREIGN KEY (`UserID`)
  REFERENCES `superminers`.`playersimpleinfo` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


-- -------------------------------------------------------------------

CREATE TABLE `superminers`.`agentawardrecord` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `AgentID` INT UNSIGNED NOT NULL,
  `PlayerID` INT UNSIGNED NOT NULL,
  `PlayerInchargeRMB` FLOAT NOT NULL,
  `AgentAwardRMB` FLOAT NOT NULL,
  `PlayerInchargeContent` VARCHAR(200) NOT NULL,
  `Time` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `AgentID_UNIQUE` (`AgentID` ASC),
  UNIQUE INDEX `PlayerID_UNIQUE` (`PlayerID` ASC),
  CONSTRAINT `AgentAwardRecord_AgentID_FK`
    FOREIGN KEY (`AgentID`)
    REFERENCES `superminers`.`agentuserinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `AgentAwardRecord_PlayerID_FK`
    FOREIGN KEY (`PlayerID`)
    REFERENCES `superminers`.`playersimpleinfo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ------------------------------------------------------------------------

ALTER TABLE `superminers`.`agentawardrecord` 
ADD COLUMN `AgentUserName` VARCHAR(64) NOT NULL AFTER `AgentID`,
ADD COLUMN `PlayerUserName` VARCHAR(64) NOT NULL AFTER `PlayerID`;


-- ----------------------------------------------------------------------------

ALTER TABLE `superminers`.`playersimpleinfo` 
ADD COLUMN `AgentUserID` INT UNSIGNED NOT NULL DEFAULT 0 COMMENT 'only agent referred player, this field valid, default value is 0 mean invalid.' AFTER `AgentReferredLevel`;


-- -----------------------------------------------------------------------------

ALTER TABLE `superminers`.`agentuserinfo` 
ADD UNIQUE INDEX `InvitationURL_UNIQUE` (`InvitationURL` ASC);


-- -----------------------------------------------------------------------------

UPDATE `superminers`.`paramtable` SET `ParamValue`='20161015225400' WHERE `id`='1';





