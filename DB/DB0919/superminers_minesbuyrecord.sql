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
-- Table structure for table `minesbuyrecord`
--

DROP TABLE IF EXISTS `minesbuyrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `minesbuyrecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(35) NOT NULL,
  `UserID` int(10) unsigned NOT NULL,
  `SpendRMB` int(10) unsigned NOT NULL,
  `GainMinesCount` float unsigned NOT NULL,
  `GainStonesReserves` int(10) unsigned NOT NULL,
  `CreateTime` datetime NOT NULL,
  `PayTime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `OrderNumber_UNIQUE` (`OrderNumber`),
  KEY `userinfo_id_MinesBuyRecord_userid_idx` (`UserID`),
  CONSTRAINT `userinfo_id_MinesBuyRecord_userid` FOREIGN KEY (`UserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `minesbuyrecord`
--

LOCK TABLES `minesbuyrecord` WRITE;
/*!40000 ALTER TABLE `minesbuyrecord` DISABLE KEYS */;
INSERT INTO `minesbuyrecord` VALUES (1,'2016091116352703211218123174667470',22,3000,1,100000,'2016-09-11 16:35:27','0001-01-01 00:00:00'),(2,'2016091506014601011218123174664129',22,6000,2,200000,'2016-09-15 06:01:46','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `minesbuyrecord` ENABLE KEYS */;
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
