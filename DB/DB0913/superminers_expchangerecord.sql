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
-- Table structure for table `expchangerecord`
--

DROP TABLE IF EXISTS `expchangerecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expchangerecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` int(10) unsigned NOT NULL,
  `AddExp` float NOT NULL,
  `NewExp` float NOT NULL,
  `Time` datetime NOT NULL,
  `OperContent` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_expchangerecord_playersimpleinfo_userid` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expchangerecord`
--

LOCK TABLES `expchangerecord` WRITE;
/*!40000 ALTER TABLE `expchangerecord` DISABLE KEYS */;
INSERT INTO `expchangerecord` VALUES (1,41,1,1,'2016-09-11 11:15:03','玩家支付宝充值金币奖励'),(2,41,1,2,'2016-09-11 11:16:59','玩家支付宝充值金币奖励'),(3,41,1,303,'2016-09-11 11:58:21','玩家支付宝充值金币奖励'),(4,41,1,304,'2016-09-11 11:59:05','玩家支付宝充值金币奖励'),(5,41,0,304,'2016-09-11 12:01:11','玩家支付宝充值金币奖励'),(6,41,1,305,'2016-09-11 12:16:43','玩家支付宝充值金币奖励'),(7,41,1,306,'2016-09-11 14:28:47','玩家支付宝充值金币奖励'),(8,41,2,308,'2016-09-11 14:29:52','玩家支付宝充值金币奖励'),(9,41,10,318,'2016-09-11 14:32:06','玩家支付宝充值金币奖励'),(10,41,3,321,'2016-09-11 14:32:50','玩家支付宝充值金币奖励'),(11,41,2,323,'2016-09-11 14:33:25','玩家支付宝充值金币奖励'),(12,41,1,324,'2016-09-11 14:33:55','玩家支付宝充值金币奖励'),(13,41,1,325,'2016-09-11 14:34:35','玩家支付宝充值金币奖励'),(14,41,1,326,'2016-09-11 15:41:41','玩家支付宝充值金币奖励'),(15,41,300,626,'2016-09-11 15:47:54','玩家支付宝充值金币奖励'),(16,41,1,627,'2016-09-11 16:15:59','玩家支付宝充值金币奖励');
/*!40000 ALTER TABLE `expchangerecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-13 11:09:07
