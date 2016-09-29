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
-- Table structure for table `rouletteawarditem`
--

DROP TABLE IF EXISTS `rouletteawarditem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rouletteawarditem` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AwardName` varchar(45) NOT NULL,
  `AwardNumber` int(11) NOT NULL DEFAULT '1',
  `RouletteAwardType` int(11) NOT NULL,
  `ValueMoneyYuan` float NOT NULL DEFAULT '0',
  `IsLargeAward` int(1) NOT NULL,
  `IsRealAward` int(1) NOT NULL,
  `WinProbability` float NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rouletteawarditem`
--

LOCK TABLES `rouletteawarditem` WRITE;
/*!40000 ALTER TABLE `rouletteawarditem` DISABLE KEYS */;
INSERT INTO `rouletteawarditem` VALUES (1,'100矿石',100,1,1,0,0,100),(2,'迅雷会员',1,8,10,1,1,10),(3,'1000金币',1000,2,1,0,0,100),(4,'10元话费',1,5,10,1,1,0),(5,'2点贡献值',2,3,2,0,0,50),(6,'300矿石',300,1,3,0,0,30),(7,'乐视会员',1,7,10,1,1,10),(8,'500金币',500,2,0.5,0,0,200),(9,'爱奇艺会员',1,6,10,1,1,10),(10,'500矿石',500,1,5,0,0,20),(11,'50元话费',1,5,50,1,1,0),(12,'再试一次',1,0,0,0,0,50);
/*!40000 ALTER TABLE `rouletteawarditem` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-30  2:15:58
