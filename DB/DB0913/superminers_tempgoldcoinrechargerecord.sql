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
-- Table structure for table `tempgoldcoinrechargerecord`
--

DROP TABLE IF EXISTS `tempgoldcoinrechargerecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tempgoldcoinrechargerecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(35) NOT NULL,
  `UserID` int(11) unsigned NOT NULL,
  `SpendRMB` float unsigned NOT NULL,
  `GainGoldCoin` float unsigned NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `OrderNumber_UNIQUE` (`OrderNumber`),
  KEY `userinfo_id_tempgoldcoinrechargerecord_userid` (`UserID`),
  CONSTRAINT `userinfo_id_tempgoldcoinrechargerecord_userid` FOREIGN KEY (`UserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tempgoldcoinrechargerecord`
--

LOCK TABLES `tempgoldcoinrechargerecord` WRITE;
/*!40000 ALTER TABLE `tempgoldcoinrechargerecord` DISABLE KEYS */;
INSERT INTO `tempgoldcoinrechargerecord` VALUES (1,'201608161531190796224454765444878',41,1,1000,'2016-08-16 15:31:20'),(2,'201608161533350866224454765443776',41,1,1000,'2016-08-16 15:33:36'),(3,'201608161606130891224454765448121',41,1,1000,'2016-08-16 16:06:14'),(5,'201608161725110838224454765441154',41,5,5000,'2016-08-16 17:25:12'),(6,'201608161735390570224454765442344',41,5,5000,'2016-08-16 17:35:40'),(19,'201609111425070459224454765445906',41,10,1000,'2016-09-11 14:25:07');
/*!40000 ALTER TABLE `tempgoldcoinrechargerecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-13 11:09:16
