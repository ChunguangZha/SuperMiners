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
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `waittoawardexprecord`
--

LOCK TABLES `waittoawardexprecord` WRITE;
/*!40000 ALTER TABLE `waittoawardexprecord` DISABLE KEYS */;
INSERT INTO `waittoawardexprecord` VALUES (1,'mQtiixLpx3E9I5QceXeCZQ==','hfnfBjvJddk=',1),(2,'mQtiixLpx3E9I5QceXeCZQ==','FTeYcIDJ/yL2XSu+OuvnKQ==',1),(3,'mQtiixLpx3E9I5QceXeCZQ==','1MAqfNxAbLAK7Wnqx9Anng==',1),(4,'mQtiixLpx3E9I5QceXeCZQ==','eOZar9rCQfmpw1+cWXQS6A==',1),(5,'mQtiixLpx3E9I5QceXeCZQ==','fuRB068WiVM=',1),(6,'Ke/q6xjAXOJJqaSjF3ekpA==','fBgPK99EBlbgGOCg6zInNA==',1),(7,'a1ygcKpcpLs4xgVbK5ssAA==','EfEFV7N7he4=',1),(8,'Ke/q6xjAXOJJqaSjF3ekpA==','3IVbxbCYraRj2Xd96Zx5Sg==',1),(9,'Ke/q6xjAXOJJqaSjF3ekpA==','7T/PVpb1q7w=',1),(10,'mQtiixLpx3E9I5QceXeCZQ==','fXu8D+ERE70=',1),(11,'Ke/q6xjAXOJJqaSjF3ekpA==','WlygY563dpDWPMjtlejFuQ==',1),(12,'Ke/q6xjAXOJJqaSjF3ekpA==','tw70VUdkABo=',1),(13,'Ke/q6xjAXOJJqaSjF3ekpA==','bLPDKyf0cUo=',1),(14,'Ke/q6xjAXOJJqaSjF3ekpA==','0UwA+gkllWo=',1),(15,'2meW23iTgMzdy09R6GQ88g==','89s4vrnwxxRjhyOCu6mY3g==',1),(16,'mQtiixLpx3E9I5QceXeCZQ==','FHnTUTx7drs=',1),(17,'a1ygcKpcpLs4xgVbK5ssAA==','6kacBd5zzsA+bK/x4BVdow==',1),(18,'a1ygcKpcpLs4xgVbK5ssAA==','uxHX8OZ1I6Q=',1),(19,'a1ygcKpcpLs4xgVbK5ssAA==','HG36VFC0gBB9PEXL3NOYAA==',1),(20,'a1ygcKpcpLs4xgVbK5ssAA==','/rFXegQ86+aIHyRnYVDr9Q==',1),(21,'Ke/q6xjAXOJJqaSjF3ekpA==','aMbgcFNr4kx+soVKDCr83w==',1),(22,'mQtiixLpx3E9I5QceXeCZQ==','YuV2447utwfkJQdRRUeUAQ==',1),(23,'mQtiixLpx3E9I5QceXeCZQ==','uNpSWAX14wjICNqVU/HyMA==',1),(24,'mQtiixLpx3E9I5QceXeCZQ==','2noIpwC3G3Q=',1),(25,'mQtiixLpx3E9I5QceXeCZQ==','fYazSO8ZZVs=',1),(26,'mQtiixLpx3E9I5QceXeCZQ==','yVcR2l7Fnmasfxh2pBTKhQ==',1),(27,'EsrBxwyw9pg=','s5sU0pule8ZulYbrfHSoCw==',1),(28,'EsrBxwyw9pg=','kClgIW4hZDO1c1rfAJ5nbA==',1),(29,'a1ygcKpcpLs4xgVbK5ssAA==','fID5iUgncV8=',1),(30,'eOZar9rCQfmpw1+cWXQS6A==','zhWHrGm6Z+/fUlHwRNT1eA==',1),(31,'tw70VUdkABo=','iuo1twcBs3p3Vkrkeuafjw==',1),(32,'hfnfBjvJddk=','UC2Yw+WBFz9S5sCYMChiOg==',1),(33,'eOZar9rCQfmpw1+cWXQS6A==','fcUFoiwNj/zCX9RiVrJ1kQ==',1),(34,'f+d6zWWZ8qQ6L2qpw7hf/w==','l4SaYSH3oBw=',1),(35,'f+d6zWWZ8qQ6L2qpw7hf/w==','j/Iqe08bXdM=',1),(36,'mQtiixLpx3E9I5QceXeCZQ==','cgSAEJf/nOLFeGFFI7XkeA==',1),(37,'mQtiixLpx3E9I5QceXeCZQ==','3lgaiL5LYLFZXpRtNUdKlw==',1),(38,'mQtiixLpx3E9I5QceXeCZQ==','FRVG9Otfrys=',1),(39,'mQtiixLpx3E9I5QceXeCZQ==','WaBHpDjpiaU=',1),(40,'f+d6zWWZ8qQ6L2qpw7hf/w==','L0ZwoTntclL/Af8G8e+g9Q==',1),(41,'f+d6zWWZ8qQ6L2qpw7hf/w==','QaQDfXu3yOI=',1),(42,'f+d6zWWZ8qQ6L2qpw7hf/w==','dNpVISUsN1U=',1),(43,'hfnfBjvJddk=','Q2GGKsIkWNHxAyu0oOuJ3g==',1),(44,'NR8ndEdLgfw=','qZsOp4499mg=',1),(45,'NR8ndEdLgfw=','9gLbrOYEMHw=',1),(46,'f+d6zWWZ8qQ6L2qpw7hf/w==','BDIkx1ZjPbcSXQYk2YkT3Q==',1),(47,'f+d6zWWZ8qQ6L2qpw7hf/w==','O4vsXgNJZKesCziYWu7SLg==',1),(48,'f+d6zWWZ8qQ6L2qpw7hf/w==','7BOYl4gDkMo=',1),(49,'iLTcGnn4xOcUh4DA4KIhwg==','hS745rs5oLc=',1),(50,'a1ygcKpcpLs4xgVbK5ssAA==','UF4enqpBlKMdEgU/XpXFwg==',1),(51,'f+d6zWWZ8qQ6L2qpw7hf/w==','Q10i7YqL86z14//ZC34Kjg==',1),(52,'f+d6zWWZ8qQ6L2qpw7hf/w==','0Bzd/CHAOgp7lqAqCVubvA==',1),(53,'f+d6zWWZ8qQ6L2qpw7hf/w==','YC4fcF60ZmM=',1),(54,'f+d6zWWZ8qQ6L2qpw7hf/w==','k3IZA24SdygCIk/mssdVPA==',1),(55,'f+d6zWWZ8qQ6L2qpw7hf/w==','gFxfilHuOO8JTcS5sB/vXg==',1),(56,'Ayt6d9C8KKfkBC1K+OQ03A==','NJSdyKIaJ7rGN7JitNge8g==',1),(57,'Ayt6d9C8KKfkBC1K+OQ03A==','sfWFDVhnFbkYanY4LEuYVg==',1),(58,'Ayt6d9C8KKfkBC1K+OQ03A==','yDsq+ub63eAXWdu3EKja4w==',1),(59,'sfWFDVhnFbkYanY4LEuYVg==','NiRpXoki9+XqYdnwD0JPTA==',1);
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

-- Dump completed on 2016-09-27 13:37:48
