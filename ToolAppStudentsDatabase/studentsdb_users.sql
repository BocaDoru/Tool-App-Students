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
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(30) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (11,'string','user@example.com','$2a$11$BUM2rXI1yvyyChNca4hcb.oqxsURcaFcQpXi1wgr37HQFrpIsKzt2'),(12,'Alex','bosuruletei@yhaoo.com','$2a$11$qWMYpIBqkzn7dlKLafw.ZuN6KbIetQXEgV2E/sDj5.IDLuGPtjS1G'),(13,'string1','user1@example.com','$2a$11$UlKv5EKHF8jx.p9EEOz9xeKMxrrNq4q02IjuzgEENF3KUlqGLNeG2'),(14,'mata','sugepula@luamiai.bota','$2a$11$gQTLC6fA0o85tHHJ4diGC.ivTU5KN9IgFt7KQhx9xNOgy/Csw3eD6'),(15,'string2','user2@gmail.com','$2a$11$sJyPmkg9Vty947oOQ07EUuQc/lZmDHM2OQHFvdksUAOJ/tfQzTl/O'),(18,'Alex1','sugepula@lu.doru','$2a$11$nAUi5smYFtm1HOSj66Msr./pqhDFJKYjD6/qD1hIN35ROG05bXYVu'),(19,'BocaDoru','Boca.Gh.Ioan@student.utcluj.ro','$2a$11$07VK5sI4Ikjy0yb6aH2Vy.CQg0jUKzYpfNRX48iZ7XlhExdbRJUSu'),(20,'string3','user3@example.com','$2a$11$BzewYpi./w8d2hJxj/LY.OytsOofaWNqn0ESgq2sha9zOlo2.cc4C');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
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
