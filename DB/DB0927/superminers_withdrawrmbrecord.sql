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
-- Table structure for table `withdrawrmbrecord`
--

DROP TABLE IF EXISTS `withdrawrmbrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `withdrawrmbrecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PlayerUserName` varchar(64) NOT NULL,
  `AlipayAccount` varchar(128) DEFAULT NULL,
  `AlipayRealName` varchar(30) DEFAULT NULL,
  `WidthdrawRMB` float NOT NULL,
  `ValueYuan` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `IsPayedSucceed` tinyint(1) NOT NULL COMMENT '1:true; 0:false',
  `AdminUserName` varchar(64) DEFAULT NULL,
  `AlipayOrderNumber` varchar(45) DEFAULT NULL,
  `PayTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `withdrawrmbrecord`
--

LOCK TABLES `withdrawrmbrecord` WRITE;
/*!40000 ALTER TABLE `withdrawrmbrecord` DISABLE KEYS */;
INSERT INTO `withdrawrmbrecord` VALUES (1,'mQtiixLpx3E9I5QceXeCZQ==',NULL,NULL,100,10,'2016-09-16 18:29:40',1,'sV5siY7eaz8=','','2016-09-17 15:03:36'),(2,'mQtiixLpx3E9I5QceXeCZQ==',NULL,NULL,100,10,'2016-09-17 09:17:31',1,'sV5siY7eaz8=','','2016-09-17 15:08:49'),(3,'vsoav0hnqitUiVBmL2IPWg==','2DGcfzq69NsvQXVA2mcRdg==','uxaDUqumA7B22MgplODJPw==',130,13,'2016-09-24 22:03:21',1,'','','2016-09-26 12:35:19');
/*!40000 ALTER TABLE `withdrawrmbrecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-27 13:37:49
