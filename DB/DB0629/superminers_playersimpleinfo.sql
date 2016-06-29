CREATE DATABASE  IF NOT EXISTS `superminers` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `superminers`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: localhost    Database: superminers
-- ------------------------------------------------------
-- Server version	5.6.21-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `playersimpleinfo`
--

DROP TABLE IF EXISTS `playersimpleinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `playersimpleinfo` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserName` varchar(64) NOT NULL,
  `NickName` varchar(128) DEFAULT NULL,
  `Password` varchar(30) NOT NULL,
  `Alipay` varchar(128) DEFAULT NULL,
  `AlipayRealName` varchar(30) DEFAULT NULL,
  `Email` varchar(128) DEFAULT NULL,
  `QQ` varchar(128) DEFAULT NULL,
  `RegisterIP` varchar(15) NOT NULL COMMENT '用户注册时使用的IP地址',
  `InvitationCode` varchar(100) NOT NULL DEFAULT '',
  `RegisterTime` datetime NOT NULL,
  `LastLoginTime` datetime DEFAULT NULL,
  `LastLogOutTime` datetime DEFAULT NULL,
  `ReferrerUserID` int(10) unsigned DEFAULT NULL,
  `LockedLogin` int(1) NOT NULL DEFAULT '0' COMMENT '1表示锁定，0表示非。被锁定的用户无法登录。',
  `LockedLoginTime` datetime DEFAULT NULL COMMENT '锁定时间。',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`),
  UNIQUE KEY `Alipay_UNIQUE` (`Alipay`),
  UNIQUE KEY `AlipayRealName_UNIQUE` (`AlipayRealName`),
  KEY `ReferrerUserID_foreignKey_idx` (`ReferrerUserID`),
  CONSTRAINT `ReferrerUserID_foreignKey` FOREIGN KEY (`ReferrerUserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playersimpleinfo`
--

LOCK TABLES `playersimpleinfo` WRITE;
/*!40000 ALTER TABLE `playersimpleinfo` DISABLE KEYS */;
INSERT INTO `playersimpleinfo` VALUES (41,'hfnfBjvJddk=','hfnfBjvJddk=','pg+++dAAGek2llGkRJq2kA==',NULL,NULL,NULL,NULL,'::1','gvIvunpCJ/t2vWuXRgjWqQ==','2016-06-01 22:16:27','2016-06-01 23:33:11','2016-06-01 23:59:31',NULL,0,NULL),(42,'dj1zERFgos8=','dj1zERFgos8=','dj1zERFgos8=',NULL,NULL,NULL,NULL,'::1','b6qZO9e58GicsrGmycOyTA==','2016-06-01 22:19:56',NULL,NULL,41,0,NULL),(43,'E7NhFVkdxe4=','E7NhFVkdxe4=','E7NhFVkdxe4=',NULL,NULL,NULL,NULL,'::1','b6qZO9e58GicCbddcumcLQ==','2016-06-01 22:22:16',NULL,NULL,41,0,NULL);
/*!40000 ALTER TABLE `playersimpleinfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-06-29 23:17:56
