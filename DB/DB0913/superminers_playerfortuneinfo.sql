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
-- Table structure for table `playerfortuneinfo`
--

DROP TABLE IF EXISTS `playerfortuneinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `playerfortuneinfo` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `userId` int(10) unsigned NOT NULL,
  `Exp` float NOT NULL DEFAULT '0',
  `RMB` float NOT NULL DEFAULT '0',
  `FreezingRMB` float NOT NULL DEFAULT '0',
  `GoldCoin` float NOT NULL DEFAULT '0',
  `MinesCount` float NOT NULL DEFAULT '0',
  `StonesReserves` float NOT NULL DEFAULT '0' COMMENT '矿石储量。该值为累计值，即每次购买矿山，将矿山储量值累加到该值上。',
  `TotalProducedStonesCount` float NOT NULL DEFAULT '0' COMMENT '累计总产出矿石数。和StonesReserves的差值，为当前可开采的矿石数。',
  `MinersCount` float NOT NULL DEFAULT '0' COMMENT '矿工数',
  `StockOfStones` float NOT NULL DEFAULT '0',
  `TempOutputStonesStartTime` datetime DEFAULT NULL,
  `TempOutputStones` float NOT NULL DEFAULT '0' COMMENT '临时生产矿石数，数据库字段，记录玩家当前生产出来的矿石，在玩家点击收取，时会调用方法把产量值保存到数据库中。',
  `FreezingStones` float NOT NULL DEFAULT '0',
  `StockOfDiamonds` float NOT NULL DEFAULT '0',
  `FreezingDiamonds` float NOT NULL DEFAULT '0',
  `FirstRechargeGoldCoinAward` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `userId_UNIQUE` (`userId`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  CONSTRAINT `UserID` FOREIGN KEY (`userId`) REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playerfortuneinfo`
--

LOCK TABLES `playerfortuneinfo` WRITE;
/*!40000 ALTER TABLE `playerfortuneinfo` DISABLE KEYS */;
INSERT INTO `playerfortuneinfo` VALUES (31,41,627,0,0,16600,2.6,260000,1455.71,100,1446,'2016-09-11 16:24:08',0,960,0,0,1),(32,42,0,0,0,0,0.6,60000,0,70,0,'2016-07-26 14:33:31',0,0,0,0,0),(33,43,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0),(34,44,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0),(35,45,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0),(36,46,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0),(37,47,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0),(38,48,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0),(39,49,0,0,0,0,0.6,60000,0,70,0,'2016-09-01 17:41:39',0,0,0,0,0),(40,50,0,0,0,2500,0.6,60000,0,70,0,NULL,0,0,0,0,0),(41,51,0,0,0,0,0.6,60000,0,70,0,NULL,0,0,0,0,0);
/*!40000 ALTER TABLE `playerfortuneinfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-13 11:09:15
