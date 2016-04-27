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
  `Password` varchar(30) NOT NULL,
  `Alipay` varchar(128) DEFAULT NULL,
  `AlipayRealName` varchar(30) DEFAULT NULL,
  `RegisterIP` varchar(15) NOT NULL COMMENT '用户注册时使用的IP地址',
  `InvitationCode` varchar(100) NOT NULL DEFAULT '',
  `RegisterTime` datetime NOT NULL,
  `LastLoginTime` datetime DEFAULT NULL,
  `LastLogOutTime` datetime DEFAULT NULL,
  `ReferrerUserID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`),
  UNIQUE KEY `Alipay_UNIQUE` (`Alipay`),
  UNIQUE KEY `AlipayRealName_UNIQUE` (`AlipayRealName`),
  KEY `ReferrerUserID_foreignKey_idx` (`ReferrerUserID`),
  CONSTRAINT `ReferrerUserID_foreignKey` FOREIGN KEY (`ReferrerUserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playersimpleinfo`
--

LOCK TABLES `playersimpleinfo` WRITE;
/*!40000 ALTER TABLE `playersimpleinfo` DISABLE KEYS */;
INSERT INTO `playersimpleinfo` VALUES (1,'admin','admin','13812345678','小查','127.0.0.2','123','0000-00-00 00:00:00',NULL,NULL,9),(9,'FhffQYggjqQ=','FhffQYggjqQ=','FhffQYggjqQ=','FhffQYggjqQ=','::1','dh+1MG/h77zrLk1zXOFEPv/D420Vn6/m9HZTTp8XCnyNuHRRpfAzfg==','2016-04-17 22:55:56',NULL,NULL,NULL),(10,'6JRHq9nBtIo=','6JRHq9nBtIo=','6JRHq9nBtIo=','6JRHq9nBtIo=','::1','L2QsR1/DXA7jCd86ozodIfH8lstKLTfCTj7aTOLVktko0K56EvAkuQ==','2016-04-17 22:59:19',NULL,NULL,NULL),(11,'hfnfqcPDVQ0=','hfnfqcPDVQ0=','hfnfqcPDVQ0=','hfnfqcPDVQ0=','::1','F2qB4hA2fkM4NZHPyFHFsDbA6VOmwJykJnog0hjrrdi6Gaqi6CjFFA==','2016-04-19 00:13:28',NULL,NULL,NULL),(12,'abc','abc','','','127.0.0.1','','2016-04-25 22:22:47',NULL,NULL,1),(15,'WzlIKzHHWPY=','WzlIKzHHWPY=',NULL,NULL,'::1','CzJQ2x/1WUBkh8Db3lkjUB+I/s5R00JoAHTGV7yLiAM/8Sb1sTRC4w==','2016-04-26 21:11:11',NULL,NULL,NULL),(16,'tJAag0Hm+lQ=','tJAag0Hm+lQ=',NULL,NULL,'::1','4GkoQKKq2jCnQ6FOLr/I/dqQDPzMfp8sR2PVPdAjhiMbA7MThxTDdA==','2016-04-26 21:15:11',NULL,NULL,NULL),(17,'fuzjc6LaNAQ=','fuzjc6LaNAQ=',NULL,NULL,'::1','VyYKtebU6PcnzcuiCHNDNILqwjVK/0HGES4mOUezQM5joIO/34ygww==','2016-04-26 22:48:13',NULL,NULL,NULL);
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

-- Dump completed on 2016-04-27 21:13:25
