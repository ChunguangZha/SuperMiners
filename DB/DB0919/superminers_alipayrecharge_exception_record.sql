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
-- Table structure for table `alipayrecharge_exception_record`
--

DROP TABLE IF EXISTS `alipayrecharge_exception_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `alipayrecharge_exception_record` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `out_trade_no` varchar(35) NOT NULL,
  `alipay_trade_no` varchar(45) NOT NULL,
  `user_name` varchar(64) DEFAULT NULL,
  `buyer_email` varchar(35) NOT NULL,
  `total_fee` float NOT NULL,
  `value_rmb` float NOT NULL,
  `pay_time` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `out_trade_no_UNIQUE` (`out_trade_no`),
  UNIQUE KEY `alipay_trade_no_UNIQUE` (`alipay_trade_no`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alipayrecharge_exception_record`
--

LOCK TABLES `alipayrecharge_exception_record` WRITE;
/*!40000 ALTER TABLE `alipayrecharge_exception_record` DISABLE KEYS */;
INSERT INTO `alipayrecharge_exception_record` VALUES (18,'2016091608015807291118123174664379','2016091621001004580275468828','fYazSO8ZZVs=','nndthink@hotmail.com',10,0,'2016-09-16 19:05:36'),(19,'201609171401520313112087488305018','2016091821001004580295888140','mQtiixLpx3E9I5QceXeCZQ==','nndthink@hotmail.com',40,0,'2016-09-18 11:24:33'),(20,'201609171746520442114732168475421','2016091821001004580296033131','mQtiixLpx3E9I5QceXeCZQ==','nndthink@hotmail.com',9,0,'2016-09-18 11:26:50'),(21,'201609181840040517112087488309902','2016091821001004580296770149','a1ygcKpcpLs4xgVbK5ssAA==','nndthink@hotmail.com',10,0,'2016-09-18 19:26:46'),(22,'2016091818435405491117671406325761','2016091821001004580296127543','a1ygcKpcpLs4xgVbK5ssAA==','nndthink@hotmail.com',3,0,'2016-09-18 19:27:44'),(23,'201609181259410307119114031979179','2016091821001004580296770244','a1ygcKpcpLs4xgVbK5ssAA==','nndthink@hotmail.com',5,0,'2016-09-18 19:28:22'),(24,'201609161706250818114454765447296','2016091821001004580295097051','a1ygcKpcpLs4xgVbK5ssAA==','nndthink@hotmail.com',4,0,'2016-09-18 19:34:13'),(25,'201609181127390669111256081068870','2016091821001004580296770837','mQtiixLpx3E9I5QceXeCZQ==','nndthink@hotmail.com',50,0,'2016-09-18 19:37:46');
/*!40000 ALTER TABLE `alipayrecharge_exception_record` ENABLE KEYS */;
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
