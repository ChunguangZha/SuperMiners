-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: superminers
-- ------------------------------------------------------
-- Server version	5.7.12-log

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
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expchangerecord`
--

LOCK TABLES `expchangerecord` WRITE;
/*!40000 ALTER TABLE `expchangerecord` DISABLE KEYS */;
INSERT INTO `expchangerecord` VALUES (1,12,1,15,'2016-09-11 15:58:26','玩家支付宝充值金币奖励'),(2,22,0,330,'2016-09-11 15:58:40','玩家支付宝充值金币奖励'),(3,12,2,17,'2016-09-11 15:59:08','玩家支付宝充值金币奖励'),(4,12,1,18,'2016-09-11 16:00:11','玩家支付宝充值金币奖励'),(5,22,0,330,'2016-09-11 16:01:23','玩家支付宝充值金币奖励'),(6,12,3,21,'2016-09-11 16:25:42','玩家支付宝充值金币奖励'),(7,12,1,22,'2016-09-11 16:26:21','玩家支付宝充值金币奖励'),(8,12,1,23,'2016-09-11 16:27:00','玩家支付宝充值金币奖励'),(9,12,1,24,'2016-09-11 16:29:53','玩家支付宝充值金币奖励'),(10,22,0,330,'2016-09-11 16:30:23','玩家支付宝充值金币奖励'),(11,12,1,25,'2016-09-11 16:31:00','玩家支付宝充值金币奖励'),(12,22,0,330,'2016-09-11 16:31:10','玩家支付宝充值金币奖励'),(13,12,1,26,'2016-09-11 16:31:57','玩家支付宝充值金币奖励'),(14,22,0,330,'2016-09-11 16:32:13','玩家支付宝充值金币奖励'),(15,22,300,630,'2016-09-11 16:36:38','玩家支付宝充值金币奖励'),(16,22,0,630,'2016-09-11 17:25:36','玩家支付宝充值金币奖励'),(17,22,0,630,'2016-09-11 17:26:24','玩家支付宝充值金币奖励'),(18,22,0,630,'2016-09-11 17:28:07','玩家支付宝充值金币奖励'),(19,22,0,630,'2016-09-11 18:25:06','玩家支付宝充值金币奖励'),(20,22,1,631,'2016-09-11 18:34:04','玩家支付宝充值金币奖励'),(21,12,1,27,'2016-09-11 19:17:51','玩家支付宝充值金币奖励'),(22,12,1,28,'2016-09-11 19:18:48','玩家支付宝充值金币奖励'),(23,12,2,30,'2016-09-11 19:19:29','玩家支付宝充值金币奖励'),(24,22,0,631,'2016-09-11 20:04:43','玩家支付宝充值金币奖励'),(25,12,1,31,'2016-09-11 20:29:20','玩家支付宝充值金币奖励'),(26,12,2,33,'2016-09-11 20:30:07','玩家支付宝充值金币奖励'),(27,12,2,35,'2016-09-11 20:52:15','玩家支付宝充值金币奖励'),(28,12,1,36,'2016-09-11 20:53:06','玩家支付宝充值金币奖励'),(29,12,1,37,'2016-09-11 20:58:23','玩家支付宝充值金币奖励'),(30,12,1,38,'2016-09-12 09:42:38','玩家支付宝充值金币奖励'),(31,21,50,55,'2016-09-12 13:24:47','玩家支付宝充值金币奖励'),(32,81,1,1,'2016-09-12 14:21:05','玩家支付宝充值金币奖励'),(33,22,600,1231,'2016-09-15 06:02:56','玩家支付宝充值金币奖励'),(34,12,1,39,'2016-09-17 13:21:19','玩家支付宝充值金币奖励'),(35,12,1,40,'2016-09-17 13:26:36','玩家支付宝充值金币奖励'),(36,12,1,41,'2016-09-17 13:28:42','玩家支付宝充值金币奖励'),(37,12,1,42,'2016-09-17 13:31:59','玩家支付宝充值金币奖励'),(38,12,1,43,'2016-09-18 22:24:31','玩家支付宝充值金币奖励'),(39,12,1,44,'2016-09-18 22:37:02','玩家支付宝充值金币奖励'),(40,272,50,50,'2016-09-20 18:54:04','玩家支付宝充值金币奖励'),(41,298,400,400,'2016-09-22 18:32:27','玩家支付宝充值金币奖励'),(42,21,5,10,'2016-09-23 03:00:45','邀请玩家获取1级奖励'),(43,165,5,25,'2016-09-24 16:07:18','邀请玩家获取1级奖励'),(44,165,5,30,'2016-09-24 16:29:17','邀请玩家获取1级奖励'),(45,165,5,35,'2016-09-24 16:33:34','邀请玩家获取1级奖励'),(46,165,5,40,'2016-09-24 16:47:43','邀请玩家获取1级奖励'),(47,165,5,40,'2016-09-25 18:18:53','邀请玩家获取1级奖励'),(48,319,5,5,'2016-09-27 11:11:24','邀请玩家获取1级奖励');
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

-- Dump completed on 2016-09-30  2:01:49
