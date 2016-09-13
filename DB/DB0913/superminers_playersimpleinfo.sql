CREATE DATABASE  IF NOT EXISTS `superminers` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `superminers`;
-- MySQL dump 10.13  Distrib 5.6.21, for Win64 (x86_64)
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
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playersimpleinfo`
--

LOCK TABLES `playersimpleinfo` WRITE;
/*!40000 ALTER TABLE `playersimpleinfo` DISABLE KEYS */;
INSERT INTO `playersimpleinfo` VALUES (41,'hfnfBjvJddk=','hfnfBjvJddk=','pg+++dAAGek2llGkRJq2kA==',NULL,NULL,NULL,NULL,'::1','gvIvunpCJ/t2vWuXRgjWqQ==','2016-06-01 22:16:27','2016-09-11 16:24:27','2016-09-11 16:24:34',NULL,0,NULL),(42,'dj1zERFgos8=','dj1zERFgos8=','dj1zERFgos8=',NULL,NULL,NULL,NULL,'::1','b6qZO9e58GicsrGmycOyTA==','2016-06-01 22:19:56','2016-07-26 14:33:31','2016-07-26 14:37:39',41,0,NULL),(43,'E7NhFVkdxe4=','E7NhFVkdxe4=','E7NhFVkdxe4=',NULL,NULL,NULL,NULL,'::1','b6qZO9e58GicCbddcumcLQ==','2016-06-01 22:22:16',NULL,NULL,41,0,NULL),(44,'zVFjUJTNuKU=','zVFjUJTNuKU=','zVFjUJTNuKU=',NULL,NULL,NULL,NULL,'::1','hI6qYCPdUj3AN/5MaOztxg==','2016-09-01 11:26:02',NULL,NULL,NULL,0,NULL),(45,'qLvG4R/pbGwuNb2+m6JCvA==','qLvG4R/pbGwuNb2+m6JCvA==','zVFjUJTNuKU=',NULL,NULL,NULL,NULL,'::1','keqiOmNkVRpyV2bty0EtgA==','2016-09-01 11:27:42',NULL,NULL,NULL,0,NULL),(46,'5R9phsVhAdI=','5R9phsVhAdI=','jlUp15NqQrZRQBBBkwGPXw==',NULL,NULL,NULL,NULL,'::1','fiwxaA8GVss=','2016-09-01 16:37:09',NULL,NULL,NULL,0,NULL),(47,'YNIdXCrITRjyG7OiA9pd1Q==','YNIdXCrITRjyG7OiA9pd1Q==','YNIdXCrITRjyG7OiA9pd1Q==',NULL,NULL,NULL,NULL,'::1','LoscxA6oLCaDh4qkiReiJA==','2016-09-01 17:18:08',NULL,NULL,NULL,0,NULL),(48,'TMuNjJip4mk=','TMuNjJip4mk=','TMuNjJip4mk=',NULL,NULL,NULL,NULL,'::1','smPzhcJdwrV6g6leTkxW1A==','2016-09-01 17:34:56',NULL,NULL,41,0,NULL),(49,'j87Mbaw4ZLE=','j87Mbaw4ZLE=','j87Mbaw4ZLE=',NULL,NULL,NULL,NULL,'::1','hV8oN8gkT4hBfice0bAvYw==','2016-09-01 17:38:24','2016-09-01 17:41:39','2016-09-01 17:42:32',41,0,NULL),(50,'Q8W6SnwYz9Y=','Q8W6SnwYz9Y=','YrWePR3nsrI=',NULL,NULL,NULL,NULL,'::1','v8K3x+IHPSKJ42YffIrEUg==','2016-09-01 17:47:11',NULL,NULL,41,0,NULL),(51,'qO7F2SAVXao=','qO7F2SAVXao=','qO7F2SAVXao=',NULL,NULL,NULL,NULL,'::1','RrRcxh/Yc4BlNa32oslNwg==','2016-09-01 17:49:33',NULL,NULL,50,0,NULL);
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

-- Dump completed on 2016-09-13 11:09:10
