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

use superminers;

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
) ENGINE=InnoDB AUTO_INCREMENT=725 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alipayrecharge_exception_record`
--

LOCK TABLES `alipayrecharge_exception_record` WRITE;
/*!40000 ALTER TABLE `alipayrecharge_exception_record` DISABLE KEYS */;
INSERT INTO `alipayrecharge_exception_record` VALUES (690,'2016121011083905001110558486193795','2016121221001004020234843071','xItFu+AVHNjujroK5mYIyw==','zllhsmw1989@163.com',270,0,'2016-12-12 09:42:54'),(699,'2016121212363703921110558486198426','2016121321001004530212469705','WK3eO2BfSzs=','18972920781',120,0,'2016-12-13 07:54:03'),(700,'2016121212362500231110558486196834','2016121321001004840236894965','6LQAYiiGLJhbsnoXObVfjg==','20794646@qq.com',100,0,'2016-12-13 08:12:58');
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

-- Dump completed on 2016-12-14 23:02:15
