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
-- Table structure for table `gameconfig`
--

DROP TABLE IF EXISTS `gameconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gameconfig` (
  `Yuan_RMB` float NOT NULL COMMENT '人民币兑换RMB',
  `RMB_GoldCoin` float NOT NULL COMMENT 'RMB兑换金币',
  `RMB_Mine` float NOT NULL,
  `GoldCoin_Miner` float NOT NULL,
  `Stones_RMB` float NOT NULL,
  `Diamonds_RMB` float NOT NULL,
  `StoneBuyerAwardGoldCoinMultiple` float NOT NULL,
  `OutputStonesPerHour` float NOT NULL COMMENT '每个矿工每小时生产矿石数',
  `TempStoneOutputValidHour` int(11) NOT NULL DEFAULT '24' COMMENT '临时生产矿石有效记录时间（小时），超出时间且没有收取，则不记生产。',
  `StonesReservesPerMines` float NOT NULL COMMENT '每座矿山的矿石储量',
  `ExchangeExpensePercent` float NOT NULL COMMENT '提现手续费比例数',
  `ExchangeExpenseMinNumber` float NOT NULL COMMENT '提现手续费手续费最小金额',
  `UserMaxHaveMinersCount` int(11) NOT NULL DEFAULT '50',
  `BuyOrderLockTimeMinutes` int(11) NOT NULL DEFAULT '30',
  `CanExchangeMinExp` int(11) NOT NULL DEFAULT '50',
  `CanDiscountMinExp` int(11) NOT NULL DEFAULT '100',
  `Discount` float NOT NULL DEFAULT '0.99'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gameconfig`
--

LOCK TABLES `gameconfig` WRITE;
/*!40000 ALTER TABLE `gameconfig` DISABLE KEYS */;
INSERT INTO `gameconfig` VALUES (10,150,3000,1000,10,1,0.1,0.104,24,100000,5,1,5000,30,50,100,0.99);
/*!40000 ALTER TABLE `gameconfig` ENABLE KEYS */;
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
