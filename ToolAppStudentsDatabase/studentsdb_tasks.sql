CREATE DATABASE  IF NOT EXISTS `studentsdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `studentsdb`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: studentsdb
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tasks`
--

DROP TABLE IF EXISTS `tasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tasks` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `Day` datetime NOT NULL,
  `Start` time NOT NULL,
  `End` time NOT NULL,
  `Duration` time NOT NULL,
  `Notification` time NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Color` binary(3) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `tasks_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tasks`
--

LOCK TABLES `tasks` WRITE;
/*!40000 ALTER TABLE `tasks` DISABLE KEYS */;
INSERT INTO `tasks` VALUES (1,11,'Task1','2025-02-23 00:00:00','10:00:00','12:00:00','02:00:00','01:00:00','asfdfg',_binary '©ô'),(2,11,'Task2','2025-02-23 00:00:00','12:00:00','14:00:00','02:00:00','01:00:00','asfdfg',_binary '©ô'),(3,11,'Task3','2025-02-23 00:00:00','08:00:00','16:00:00','08:00:00','01:00:00','asfdfg',_binary '‹\ÃJ'),(4,11,'Task3','2025-02-23 00:00:00','07:00:00','17:00:00','10:00:00','01:00:00','asfdfg',_binary 'ÿ˜\0'),(5,11,'Task3','2025-02-23 00:00:00','06:00:00','09:00:00','03:00:00','01:00:00','asfdfg',_binary '\éc'),(6,11,'Task3','2025-02-23 00:00:00','08:00:00','10:00:00','02:00:00','01:00:00','asfdfg',_binary 'g:·'),(7,12,'string','2025-02-25 00:00:00','10:00:00','12:00:00','02:00:00','01:00:00','string',_binary '©ô'),(8,11,'dsfg','2025-02-26 02:20:00','02:00:00','03:00:00','01:00:00','00:00:00','sfdg',_binary '©ô');
/*!40000 ALTER TABLE `tasks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-01 14:00:39
