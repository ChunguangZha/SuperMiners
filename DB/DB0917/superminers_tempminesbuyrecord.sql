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
-- Table structure for table `tempminesbuyrecord`
--

DROP TABLE IF EXISTS `tempminesbuyrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tempminesbuyrecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(35) NOT NULL,
  `UserID` int(10) unsigned NOT NULL,
  `SpendRMB` int(10) unsigned NOT NULL,
  `GainMinesCount` float unsigned NOT NULL,
  `GainStonesReserves` int(10) unsigned NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `OrderNumber_UNIQUE` (`OrderNumber`),
  KEY `tempminesbuyrecord_UserID__playersimpleinfo_UserID_foreignK_idx` (`UserID`),
  CONSTRAINT `tempminesbuyrecord_UserID__playersimpleinfo_UserID_foreignKey` FOREIGN KEY (`UserID`) REFERENCES `playersimpleinfo` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tempminesbuyrecord`
--

LOCK TABLES `tempminesbuyrecord` WRITE;
/*!40000 ALTER TABLE `tempminesbuyrecord` DISABLE KEYS */;
INSERT INTO `tempminesbuyrecord` VALUES (1,'2016091109141803471218123174663455',22,3000,1,100000,'2016-09-11 09:14:18'),(3,'2016091413063800711218438087478100',82,3000,1,100000,'2016-09-14 13:06:38'),(4,'2016091413073007831218438087475761',82,30000,10,1000000,'2016-09-14 13:07:31'),(5,'2016091413115400871218438087475923',82,30000,10,1000000,'2016-09-14 13:11:54');
/*!40000 ALTER TABLE `tempminesbuyrecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-17 16:01:34
