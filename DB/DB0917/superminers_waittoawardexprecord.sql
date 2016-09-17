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
-- Table structure for table `waittoawardexprecord`
--

DROP TABLE IF EXISTS `waittoawardexprecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `waittoawardexprecord` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ReferrerUserName` varchar(64) NOT NULL,
  `NewRegisterUserNme` varchar(64) NOT NULL,
  `AwardLevel` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `waittoawardexprecord`
--

LOCK TABLES `waittoawardexprecord` WRITE;
/*!40000 ALTER TABLE `waittoawardexprecord` DISABLE KEYS */;
INSERT INTO `waittoawardexprecord` VALUES (1,'mQtiixLpx3E9I5QceXeCZQ==','hfnfBjvJddk=',1),(2,'mQtiixLpx3E9I5QceXeCZQ==','FTeYcIDJ/yL2XSu+OuvnKQ==',1),(3,'mQtiixLpx3E9I5QceXeCZQ==','1MAqfNxAbLAK7Wnqx9Anng==',1),(4,'mQtiixLpx3E9I5QceXeCZQ==','eOZar9rCQfmpw1+cWXQS6A==',1),(5,'mQtiixLpx3E9I5QceXeCZQ==','fuRB068WiVM=',1),(6,'Ke/q6xjAXOJJqaSjF3ekpA==','fBgPK99EBlbgGOCg6zInNA==',1),(7,'a1ygcKpcpLs4xgVbK5ssAA==','EfEFV7N7he4=',1),(8,'Ke/q6xjAXOJJqaSjF3ekpA==','3IVbxbCYraRj2Xd96Zx5Sg==',1),(9,'Ke/q6xjAXOJJqaSjF3ekpA==','7T/PVpb1q7w=',1),(10,'mQtiixLpx3E9I5QceXeCZQ==','fXu8D+ERE70=',1),(11,'Ke/q6xjAXOJJqaSjF3ekpA==','WlygY563dpDWPMjtlejFuQ==',1),(12,'Ke/q6xjAXOJJqaSjF3ekpA==','tw70VUdkABo=',1),(13,'Ke/q6xjAXOJJqaSjF3ekpA==','bLPDKyf0cUo=',1),(14,'Ke/q6xjAXOJJqaSjF3ekpA==','0UwA+gkllWo=',1),(15,'2meW23iTgMzdy09R6GQ88g==','89s4vrnwxxRjhyOCu6mY3g==',1),(16,'mQtiixLpx3E9I5QceXeCZQ==','FHnTUTx7drs=',1),(17,'a1ygcKpcpLs4xgVbK5ssAA==','6kacBd5zzsA+bK/x4BVdow==',1),(18,'a1ygcKpcpLs4xgVbK5ssAA==','uxHX8OZ1I6Q=',1),(19,'a1ygcKpcpLs4xgVbK5ssAA==','HG36VFC0gBB9PEXL3NOYAA==',1),(20,'a1ygcKpcpLs4xgVbK5ssAA==','/rFXegQ86+aIHyRnYVDr9Q==',1),(21,'Ke/q6xjAXOJJqaSjF3ekpA==','aMbgcFNr4kx+soVKDCr83w==',1),(22,'mQtiixLpx3E9I5QceXeCZQ==','YuV2447utwfkJQdRRUeUAQ==',1),(23,'mQtiixLpx3E9I5QceXeCZQ==','uNpSWAX14wjICNqVU/HyMA==',1),(24,'mQtiixLpx3E9I5QceXeCZQ==','2noIpwC3G3Q=',1),(25,'mQtiixLpx3E9I5QceXeCZQ==','fYazSO8ZZVs=',1),(26,'mQtiixLpx3E9I5QceXeCZQ==','yVcR2l7Fnmasfxh2pBTKhQ==',1),(27,'EsrBxwyw9pg=','s5sU0pule8ZulYbrfHSoCw==',1),(28,'EsrBxwyw9pg=','kClgIW4hZDO1c1rfAJ5nbA==',1),(29,'a1ygcKpcpLs4xgVbK5ssAA==','fID5iUgncV8=',1),(30,'eOZar9rCQfmpw1+cWXQS6A==','zhWHrGm6Z+/fUlHwRNT1eA==',1);
/*!40000 ALTER TABLE `waittoawardexprecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-17 16:01:33
