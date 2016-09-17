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
-- Table structure for table `awardreferrerconfig`
--

DROP TABLE IF EXISTS `awardreferrerconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `awardreferrerconfig` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ReferLevel` int(10) unsigned NOT NULL,
  `AwardReferrerExp` float NOT NULL COMMENT '奖励推荐人贡献值',
  `AwardReferrerGoldCoin` float NOT NULL COMMENT '奖励推荐人金币数',
  `AwardReferrerMines` float NOT NULL,
  `AwardReferrerMiners` int(11) NOT NULL COMMENT '奖励推荐人矿工数',
  `AwardReferrerStones` float NOT NULL COMMENT '奖励推荐人矿石数',
  `AwardReferrerDiamond` float NOT NULL COMMENT '奖励推荐人钻石数',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `ReferLevel_UNIQUE` (`ReferLevel`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8 COMMENT='奖励推荐人配置';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `awardreferrerconfig`
--

LOCK TABLES `awardreferrerconfig` WRITE;
/*!40000 ALTER TABLE `awardreferrerconfig` DISABLE KEYS */;
INSERT INTO `awardreferrerconfig` VALUES (51,1,5,2500,0,0,0,0);
/*!40000 ALTER TABLE `awardreferrerconfig` ENABLE KEYS */;
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
