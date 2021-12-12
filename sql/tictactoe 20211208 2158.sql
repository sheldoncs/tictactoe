-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.5.39


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema tictactoe
--

CREATE DATABASE IF NOT EXISTS tictactoe;
USE tictactoe;

--
-- Definition of table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `__efmigrationshistory`
--

/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` (`MigrationId`,`ProductVersion`) VALUES 
 ('20211209095943_MyFirstMigration','3.1.16');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;


--
-- Definition of table `games`
--

DROP TABLE IF EXISTS `games`;
CREATE TABLE `games` (
  `Game_Id` int(11) NOT NULL AUTO_INCREMENT,
  `gameStatusStatus_Id` int(11) DEFAULT NULL,
  PRIMARY KEY (`Game_Id`),
  KEY `IX_games_gameStatusStatus_Id` (`gameStatusStatus_Id`),
  CONSTRAINT `FK_games_gameStatuses_gameStatusStatus_Id` FOREIGN KEY (`gameStatusStatus_Id`) REFERENCES `gamestatuses` (`Status_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `games`
--

/*!40000 ALTER TABLE `games` DISABLE KEYS */;
/*!40000 ALTER TABLE `games` ENABLE KEYS */;


--
-- Definition of table `gamestatuses`
--

DROP TABLE IF EXISTS `gamestatuses`;
CREATE TABLE `gamestatuses` (
  `Status_Id` int(11) NOT NULL AUTO_INCREMENT,
  `Game_Id` int(11) NOT NULL,
  `Outcome` text,
  PRIMARY KEY (`Status_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `gamestatuses`
--

/*!40000 ALTER TABLE `gamestatuses` DISABLE KEYS */;
/*!40000 ALTER TABLE `gamestatuses` ENABLE KEYS */;


--
-- Definition of table `placetokens`
--

DROP TABLE IF EXISTS `placetokens`;
CREATE TABLE `placetokens` (
  `token_id` int(11) NOT NULL AUTO_INCREMENT,
  `player_id` int(11) NOT NULL,
  `game_id` int(11) NOT NULL,
  `row` int(11) NOT NULL,
  `col` int(11) NOT NULL,
  PRIMARY KEY (`token_id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `placetokens`
--

/*!40000 ALTER TABLE `placetokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `placetokens` ENABLE KEYS */;


--
-- Definition of table `players`
--

DROP TABLE IF EXISTS `players`;
CREATE TABLE `players` (
  `Player_Id` int(11) NOT NULL AUTO_INCREMENT,
  `Game_Id` int(11) NOT NULL,
  `Name` text,
  `token_id` int(11) DEFAULT NULL,
  `GameStatusStatus_Id` int(11) DEFAULT NULL,
  `Game_Id1` int(11) DEFAULT NULL,
  PRIMARY KEY (`Player_Id`),
  KEY `IX_players_GameStatusStatus_Id` (`GameStatusStatus_Id`),
  KEY `IX_players_Game_Id1` (`Game_Id1`),
  KEY `IX_players_token_id` (`token_id`),
  CONSTRAINT `FK_players_gameStatuses_GameStatusStatus_Id` FOREIGN KEY (`GameStatusStatus_Id`) REFERENCES `gamestatuses` (`Status_Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_players_games_Game_Id1` FOREIGN KEY (`Game_Id1`) REFERENCES `games` (`Game_Id`),
  CONSTRAINT `FK_players_placeTokens_token_id` FOREIGN KEY (`token_id`) REFERENCES `placetokens` (`token_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `players`
--

/*!40000 ALTER TABLE `players` DISABLE KEYS */;
/*!40000 ALTER TABLE `players` ENABLE KEYS */;


--
-- Definition of table `winners`
--

DROP TABLE IF EXISTS `winners`;
CREATE TABLE `winners` (
  `Winner_Id` int(11) NOT NULL AUTO_INCREMENT,
  `Game_Id` int(11) NOT NULL,
  `Player_Id` int(11) NOT NULL,
  `GameStatusStatus_Id` int(11) DEFAULT NULL,
  PRIMARY KEY (`Winner_Id`),
  KEY `IX_winners_GameStatusStatus_Id` (`GameStatusStatus_Id`),
  CONSTRAINT `FK_winners_gameStatuses_GameStatusStatus_Id` FOREIGN KEY (`GameStatusStatus_Id`) REFERENCES `gamestatuses` (`Status_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `winners`
--

/*!40000 ALTER TABLE `winners` DISABLE KEYS */;
/*!40000 ALTER TABLE `winners` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
