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
-- Table structure for table `buystonesrecord`
--

DROP TABLE IF EXISTS `buystonesrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `buystonesrecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(35) NOT NULL,
  `BuyerUserName` varchar(64) NOT NULL,
  `BuyTime` datetime NOT NULL,
  `AwardGoldCoin` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `OrderNumber_UNIQUE` (`OrderNumber`),
  KEY `foreign_OrderNumber_idx` (`OrderNumber`),
  CONSTRAINT `foreign_OrderNumber` FOREIGN KEY (`OrderNumber`) REFERENCES `sellstonesorder` (`OrderNumber`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `buystonesrecord`
--

LOCK TABLES `buystonesrecord` WRITE;
/*!40000 ALTER TABLE `buystonesrecord` DISABLE KEYS */;
INSERT INTO `buystonesrecord` VALUES (1,'2016091511052507611118438087478721','mQtiixLpx3E9I5QceXeCZQ==','2016-09-15 15:13:07',262),(2,'201609151025230292114454765443122','mQtiixLpx3E9I5QceXeCZQ==','2016-09-15 15:23:16',225),(3,'201609151322170435119114031971171','fYazSO8ZZVs=','2016-09-15 15:50:51',942),(4,'2016091513340309131116594421023774','fYazSO8ZZVs=','2016-09-15 15:55:06',750),(5,'201609151527070307111256081065344','Ke/q6xjAXOJJqaSjF3ekpA==','2016-09-16 07:57:21',3750),(6,'2016090414024807071119594781665320','EsrBxwyw9pg=','2016-09-16 13:17:51',202),(7,'2016091608020206821118123174665704','hfnfBjvJddk=','2016-09-16 13:18:07',750),(8,'2016091613182700751118438087474898','mQtiixLpx3E9I5QceXeCZQ==','2016-09-16 16:56:49',600),(9,'2016091515311807831110128627731813','mQtiixLpx3E9I5QceXeCZQ==','2016-09-16 16:57:40',1500),(10,'2016091608015403031118123174662896','mQtiixLpx3E9I5QceXeCZQ==','2016-09-16 16:57:51',1500),(11,'2016091608015807291118123174664379','fYazSO8ZZVs=','2016-09-16 19:01:15',1500),(12,'201609171401520313112087488305018','mQtiixLpx3E9I5QceXeCZQ==','2016-09-18 11:23:45',4000),(13,'201609171746520442114732168475421','mQtiixLpx3E9I5QceXeCZQ==','2016-09-18 11:25:57',900),(14,'201609181840040517112087488309902','a1ygcKpcpLs4xgVbK5ssAA==','2016-09-18 19:25:53',1000),(15,'2016091818435405491117671406325761','a1ygcKpcpLs4xgVbK5ssAA==','2016-09-18 19:27:06',300),(16,'201609181259410307119114031979179','a1ygcKpcpLs4xgVbK5ssAA==','2016-09-18 19:27:47',500),(17,'201609161706250818114454765447296','a1ygcKpcpLs4xgVbK5ssAA==','2016-09-18 19:33:27',400),(18,'201609181127390669111256081068870','mQtiixLpx3E9I5QceXeCZQ==','2016-09-18 19:37:02',5000);
/*!40000 ALTER TABLE `buystonesrecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-19 11:29:59